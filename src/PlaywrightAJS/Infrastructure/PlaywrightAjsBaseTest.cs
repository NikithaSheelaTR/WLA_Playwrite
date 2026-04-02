namespace WestlawAdvantage.Playwright.AJS.Infrastructure;

using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using WestlawAdvantage.Playwright.AJS.Pages;

/// <summary>
/// Base class for all AJS native-Playwright tests.
/// 
/// KEY DIFFERENCES vs the Selenium/Shim approach (WlaAjsBaseTest):
///   - No Selenium IWebDriver — uses IPage directly
///   - No SyncHelper.RunSync — everything is async/await
///   - No BrowserPool — Playwright NUnit manages browser lifecycle
///   - No Thread.Sleep — replaced by awaiting actual element states
///   - No SafeMethodExecutor — replaced by Playwright built-in wait
/// 
/// HOW TO RUN:
///   dotnet test src/PlaywrightAJS/PlaywrightAJS.csproj
///   -- or with headed browser for debugging:
///   HEADED=1 dotnet test src/PlaywrightAJS/PlaywrightAJS.csproj
/// </summary>
public class PlaywrightAjsBaseTest : PageTest
{
    // ── Configuration ───────────────────────────────────────────────────────
    // Set these via environment variables or update for your environment.
    // The test user must have IAC-AIJS-INCLUDE-RELATED-FEDERAL + IAC-AI-50SS-PROFILE13 access.

    protected string BaseUrl =>
        Environment.GetEnvironmentVariable("WLA_BASE_URL")
        ?? "https://qed.next.westlaw.com";

    protected string TestUsername =>
        Environment.GetEnvironmentVariable("WLA_TEST_USER")
        ?? throw new InvalidOperationException("Set WLA_TEST_USER environment variable");

    protected string TestPassword =>
        Environment.GetEnvironmentVariable("WLA_TEST_PASS")
        ?? throw new InvalidOperationException("Set WLA_TEST_PASS environment variable");

    protected string TestClientId =>
        Environment.GetEnvironmentVariable("WLA_CLIENT_ID")
        ?? "Wla Test";

    // ── Playwright context options ───────────────────────────────────────────
    public override BrowserNewContextOptions ContextOptions()
    {
        return new BrowserNewContextOptions
        {
            IgnoreHTTPSErrors = true,
            ViewportSize = new ViewportSize { Width = 1920, Height = 1080 },
            AcceptDownloads = true,
        };
    }

    // ── Sign-on ──────────────────────────────────────────────────────────────
    /// <summary>
    /// Sign in to WLA before each test.
    /// 
    /// TODO: Replace the steps below with your actual CIAM sign-on flow.
    /// You can record the sign-on steps using Playwright Codegen:
    ///   npx playwright codegen https://qed.next.westlaw.com
    /// Then paste the generated locators here.
    /// 
    /// The original WlaBaseTest.OnManageCredential() fetches credentials
    /// from a password database (UserDbCredential). For this spike, 
    /// environment variables are used instead.
    /// </summary>
    [SetUp]
    public async Task SignIn()
    {
        await Page.GotoAsync($"{BaseUrl}/Advantage/Home");

        // ── Step 1: Username / OnePass page ──────────────────────────────
        // TODO: Verify these locators against the actual CIAM sign-on page.
        // Use Page.Pause() to inspect, or Codegen to re-record.
        var usernameInput = Page.Locator("input[type='text'][name='username'], input[id='username'], input[placeholder*='username' i]");
        await usernameInput.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 30000 });
        await usernameInput.FillAsync(TestUsername);
        await Page.Locator("button[type='submit'], button:has-text('Continue'), button:has-text('Next')").First.ClickAsync();

        // ── Step 2: Password ──────────────────────────────────────────────
        var passwordInput = Page.Locator("input[type='password']");
        await passwordInput.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 15000 });
        await passwordInput.FillAsync(TestPassword);
        await Page.Locator("button[type='submit'], button:has-text('Sign In'), button:has-text('Continue')").First.ClickAsync();

        // ── Step 3: Client ID page (if shown) ────────────────────────────
        // The client ID page may or may not appear depending on the user.
        var clientIdInput = Page.Locator("input[id='clientId'], input[placeholder*='client' i], [data-automation='client-id-input']");
        var clientIdVisible = await clientIdInput.IsVisibleAsync().ContinueWith(t => t.Result);
        if (clientIdVisible)
        {
            await clientIdInput.FillAsync(TestClientId);
            await Page.Locator("button:has-text('Continue'), button:has-text('Submit'), button[type='submit']").First.ClickAsync();
        }

        // ── Wait for WLA home page ────────────────────────────────────────
        await Page.WaitForURLAsync(url => url.Contains("/Advantage/Home") || url.Contains("/Advantage/Search"),
            new PageWaitForURLOptions { Timeout = 60000 });
    }

    // ── Navigation helper ────────────────────────────────────────────────────
    /// <summary>
    /// Navigate to the AJS landing page.
    /// Replaces WlaAjsBaseTest.NavigateToLandingPage().
    ///
    /// ORIGINAL (shim version):
    ///   var homePage = this.GetHomePage&lt;AdvantageHomePage&gt;();
    ///   jurisdictionalSurveysPage = homePage.FeaturesIncludedPanel
    ///       .GetWidgetLinkByTitle("AI Jurisdictional Surveys")
    ///       .Click&lt;AiJurisdictionalSurveysPage&gt;();
    ///   BrowserPool.CurrentBrowser.CreateTab(JurisdictionalSurveysTab);
    ///   BrowserPool.CurrentBrowser.ActivateTab(JurisdictionalSurveysTab);
    ///   Thread.Sleep(2000);   ← removed
    ///   jurisdictionalSurveysPage.ClosePendoMessage();
    ///   SafeMethodExecutor.ExecuteUntil(() => jurisdictionalSurveysPage.PageDescription.Displayed);  ← replaced
    ///
    /// NEW (native Playwright):
    ///   Click the card, await the page to settle, no sleep needed.
    /// </summary>
    protected async Task<AiJurisdictionalSurveysPagePw> NavigateToLandingPage()
    {
        const string JurisdictionalLabel = "AI Jurisdictional Surveys";

        // Find and click the AJS feature card on the WLA home page.
        // TODO: Verify this locator — inspect the home page DOM to find the card.
        // Likely patterns: a card/link with text "AI Jurisdictional Surveys"
        var featureCard = Page.Locator($"[data-analytics*='aijs'], [data-automation*='aijs'], a:has-text('{JurisdictionalLabel}'), button:has-text('{JurisdictionalLabel}')").First;
        await featureCard.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 15000 });
        await featureCard.ClickAsync();

        var ajsPage = new AiJurisdictionalSurveysPagePw(Page);
        await ajsPage.WaitForPageReady();

        return ajsPage;
    }

    /// <summary>
    /// Prepare the AJS test folder in Research Organizer.
    /// Replaces WlaAjsBaseTest.PrepareTestFolder().
    /// 
    /// TODO: Implement this using native Playwright once the folder
    /// page objects are available. For now it's a stub.
    /// </summary>
    protected async Task PrepareTestFolder(string folderName = "WlaAjsTestFolder")
    {
        // TODO: Navigate to folders page, create folder if not exists or clear it
        // For the locator rewrite spike, implement the foldering test last —
        // start with AjsIncludeRelatedFedJurisSelectorTest first.
        await Task.CompletedTask;
        TestContext.WriteLine($"[TODO] PrepareTestFolder({folderName}) — implement with native Playwright");
    }
}