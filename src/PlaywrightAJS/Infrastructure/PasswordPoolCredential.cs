namespace WestlawAdvantage.Playwright.AJS.Infrastructure;

using Microsoft.Data.SqlClient;
using System.Transactions;

/// <summary>
/// Checks out / checks in test credentials from the shared password pool database.
/// Mirrors the logic in Framework.Core's PasswordUtils.cs and UserDbCredential.cs
/// without requiring MSTest's TestContext.
///
/// Connection strings are identical to PasswordUtils.cs:
///   Prod: cr-pp.1129.aws-int.thomsonreuters.com  (tries first)
///   Dev:  eg-dbasqldv-a04.tlr.thomson.com         (fallback)
///
/// Usage:
///   await using var cred = await PasswordPoolCredential.CheckOutAsync("WlnGrowth", "WestlawAdvantage");
///   // cred.Username / cred.Password now available
///   // DisposeAsync() checks the credential back in automatically
/// </summary>
public sealed class PasswordPoolCredential : IAsyncDisposable
{
    // ── Database connection strings (same as PasswordUtils.cs) ───────────────
    private static readonly string ProdDatabase =
        "Server=cr-pp.1129.aws-int.thomsonreuters.com;User ID=cobaltqeduser;Password="
        + (Environment.GetEnvironmentVariable("PROD_DB_VALUE") ?? "c@baltQEd!")
        + ";database=password_pool;TrustServerCertificate=True;";

    private static readonly string DevDatabase =
        "Server=eg-dbasqldv-a04.tlr.thomson.com;User ID=cobaltqed;Password=c@baltQEd!;"
        + "database=password_pool;TrustServerCertificate=True;";

    // ── Properties ────────────────────────────────────────────────────────────
    public string Username { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public string ClientId { get; } = "Wla Test";

    private string _testRunId = string.Empty;
    private string _connectionString = string.Empty;

    private PasswordPoolCredential() { }

    // ── Checkout ──────────────────────────────────────────────────────────────
    /// <summary>
    /// Checks out one available credential from the password pool.
    /// Tries production DB first, falls back to dev — same as PasswordUtils.GetPasswordDbDataContext().
    /// </summary>
    /// <param name="vertical">PasswordVertical value, e.g. "WlnGrowth"</param>
    /// <param name="pool">Password pool name, e.g. "WestlawAdvantage"</param>
    /// <param name="testName">Test name for the CHECKED_OUT_BY field (defaults to current NUnit test)</param>
    public static async Task<PasswordPoolCredential> CheckOutAsync(
        string vertical  = "WlnGrowth",
        string pool      = "WestlawAdvantage",
        string? testName = null)
    {
        testName ??= NUnit.Framework.TestContext.CurrentContext.Test.FullName;

        var connStr = await ResolveConnectionStringAsync();

        var testRunId = DateTime.UtcNow.ToString("MMddyyyyhhmmss") + new Random().Next(1000, 9999);
        var now       = DateTime.UtcNow;
        var expires   = now.AddMinutes(20);

        // Atomic checkout — same ROWLOCK + SERIALIZABLE pattern as PasswordUtils.cs
        using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.Serializable },
            TransactionScopeAsyncFlowOption.Enabled))
        {
            await using var conn = new SqlConnection(connStr);
            await conn.OpenAsync();

            const string checkoutSql =
                "UPDATE TOP(1) PASSWORD_POOL WITH (ROWLOCK) " +
                "SET AVAILABLE = 0, TESTRUNID = @runId, CHECK_OUT_TIME = @now, " +
                "    CHECKED_OUT_BY = @machine, TEST_USING_PWD = @test, EXPIRATION_TIME = @exp " +
                "WHERE VERTICAL = @vertical AND POOL = @pool AND AVAILABLE = 1";

            await using var cmd = new SqlCommand(checkoutSql, conn);
            cmd.Parameters.AddWithValue("@runId",    testRunId);
            cmd.Parameters.AddWithValue("@now",      now);
            cmd.Parameters.AddWithValue("@machine",  Environment.MachineName);
            cmd.Parameters.AddWithValue("@test",     testName);
            cmd.Parameters.AddWithValue("@exp",      expires);
            cmd.Parameters.AddWithValue("@vertical", vertical);
            cmd.Parameters.AddWithValue("@pool",     pool);
            await cmd.ExecuteNonQueryAsync();

            scope.Complete();
        }

        // Retrieve the checked-out record
        await using var conn2 = new SqlConnection(connStr);
        await conn2.OpenAsync();

        // BLOCK_CIAM=true in LocalTestConfig.xml → shim uses Email field for sign-in
        // Fetch both EMAIL (for CIAM sign-in) and ONEPASS_USERNAME (fallback)
        const string selectSql =
            "SELECT TOP 1 EMAIL, ONEPASS_PASSWORD, ONEPASS_USERNAME " +
            "FROM PASSWORD_POOL " +
            "WHERE TESTRUNID = @runId AND AVAILABLE = 0 " +
            "ORDER BY CHECK_OUT_TIME DESC";

        await using var sel = new SqlCommand(selectSql, conn2);
        sel.Parameters.AddWithValue("@runId", testRunId);
        await using var reader = await sel.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
            throw new InvalidOperationException(
                $"No credential available in pool '{pool}' / vertical '{vertical}'. " +
                "All accounts may be checked out — wait or check them back in.");

        var email    = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
        var password = reader.GetString(1);
        var username = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);

        return new PasswordPoolCredential
        {
            // Use Email when set (BLOCK_CIAM=true flow), fall back to ONEPASS_USERNAME
            Username          = !string.IsNullOrEmpty(email) ? email : username,
            Password          = password,
            _testRunId        = testRunId,
            _connectionString = connStr
        };
    }

    // ── Checkin (called by DisposeAsync) ──────────────────────────────────────
    public async ValueTask DisposeAsync()
    {
        if (string.IsNullOrEmpty(_testRunId)) return;

        try
        {
            await using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            const string checkinSql =
                "UPDATE PASSWORD_POOL " +
                "SET AVAILABLE = 1, TESTRUNID = NULL, CHECK_OUT_TIME = NULL, " +
                "    CHECKED_OUT_BY = NULL, TEST_USING_PWD = NULL, EXPIRATION_TIME = NULL " +
                "WHERE TESTRUNID = @runId";

            await using var cmd = new SqlCommand(checkinSql, conn);
            cmd.Parameters.AddWithValue("@runId", _testRunId);
            await cmd.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            // Log but don't throw from Dispose — credential will expire after 20 min anyway
            Console.Error.WriteLine($"[PasswordPoolCredential] Check-in failed: {ex.Message}");
        }
    }

    // ── DB connection resolution (prod → dev fallback) ────────────────────────
    private static async Task<string> ResolveConnectionStringAsync()
    {
        foreach (var connStr in new[] { ProdDatabase, DevDatabase })
        {
            try
            {
                await using var conn = new SqlConnection(connStr);
                await conn.OpenAsync();
                return connStr;
            }
            catch { /* try next */ }
        }
        throw new InvalidOperationException(
            "Could not connect to either the Production or Development password pool database.");
    }
}
