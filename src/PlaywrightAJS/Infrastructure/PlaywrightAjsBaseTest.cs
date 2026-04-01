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

        // ── Step 1: Username ──────────────────────────────────────────────
        // Locators from CommonSignOnPage.cs:
        //   By.CssSelector("#Username,#coid_website_userNameTextbox")
        var usernameInput = Page.Locator("#Username, #coid_website_userNameTextbox");
        await usernameInput.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 30000 });
        await usernameInput.FillAsync(TestUsername);
        await Page.Locator("button[type='submit']").First.ClickAsync();

        // ── CIAM workaround pause (matches WestlawSignOnManager.cs WaitForJavaScript(3000)) ──
        await Page.WaitForTimeoutAsync(3000);

        // ── Step 2: Password ──────────────────────────────────────────────
        // Locators from CommonSignOnPage.cs:
        //   By.CssSelector("#Password,#coid_website_passwordTextbox")
        var passwordInput = Page.Locator("#Password, #coid_website_passwordTextbox");
        await passwordInput.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 15000 });
        await passwordInput.FillAsync(TestPassword);
        await Page.Locator("button[type='submit']").First.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        // ── Step 3: Cookie settings dialog (if shown) ────────────────────
        // CommonSignOnPage.cs checks for CookieSettingButton then AllowAllCookies.
        var cookieSettingBtn = Page.Locator("button:has-text('Cookie Settings')");
        if (await cookieSettingBtn.IsVisibleAsync())
        {
            await cookieSettingBtn.ClickAsync();
            var allowAllBtn = Page.Locator("button:has-text('Allow All'), button:has-text('Accept All')");
            await allowAllBtn.First.ClickAsync(new LocatorClickOptions { Timeout = 5000 });
        }

        // ── Step 4: Client ID page (if shown) ────────────────────────────
        // Locators from CommonClientIdPage.cs:
        //   By.XPath("//input[contains(@class,'co_clientIDTextbox') or @id='co_clientIDTextbox'] | //input[@id='clientidr']")
        //   By.XPath("//input[@id='co_clientIDContinueButton'] | //input[@value='Submit']")
        var clientIdInput = Page.Locator("xpath=//input[contains(@class,'co_clientIDTextbox') or @id='co_clientIDTextbox'] | //input[@id='clientidr']");
        if (await clientIdInput.IsVisibleAsync())
        {
            await clientIdInput.FillAsync(TestClientId);
            await Page.Locator("xpath=//input[@id='co_clientIDContinueButton'] | //input[@value='Submit']").First.ClickAsync();
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

        // Shim: homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle("AI Jurisdictional Surveys")
        // The home page widget is a link with the title text. Try the most specific selector
        // first (aria label / title attr), fall back to text match.
        // If this fails: run Page.Pause() on the home page and inspect the card element.
        var featureCard = Page.Locator($"[title='{JurisdictionalLabel}'], a:has-text('{JurisdictionalLabel}'), button:has-text('{JurisdictionalLabel}')").First;
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