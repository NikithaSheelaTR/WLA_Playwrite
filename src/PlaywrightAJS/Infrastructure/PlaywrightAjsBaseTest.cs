namespace WestlawAdvantage.Playwright.AJS.Infrastructure;

using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using WestlawAdvantage.Playwright.AJS.Pages;

/// <summary>
/// Base class for all AJS native-Playwright tests.
///
/// KEY DIFFERENCES vs the Selenium/Shim approach (WlaAjsBaseTest):
///   - No Selenium IWebDriver -- uses IPage directly
///   - No SyncHelper.RunSync -- everything is async/await
///   - No BrowserPool -- Playwright NUnit manages browser lifecycle
///   - No Thread.Sleep -- replaced by awaiting actual element states
///   - No SafeMethodExecutor -- replaced by Playwright built-in wait
///
/// SIGN-ON FLOW:
///   1. Navigate to /routing (pre-sign-in) and apply FAC/IAC overrides, Save (noRedirect=False)
///      WLN stores the routing key in a cookie and redirects to sign-on page.
///   2. Sign in with username + password.
///   3. WLN creates authenticated session and reads routing cookie -> applies FAC/IAC.
///   4. Land on /Search/Home.html?bhcp=1 (WLN Classic home). Wait for NetworkIdle.
///   5. NavigateToLandingPage() navigates to /Advantage/Home?bhcp=1 and clicks AJS feature card.
/// </summary>
public class PlaywrightAjsBaseTest : PageTest
{
    // ── Configuration ────────────────────────────────────────────────────────
    protected string BaseUrl =>
        Environment.GetEnvironmentVariable("WLA_BASE_URL")
        ?? "https://1.next.demo.westlaw.com";

    protected string PasswordVertical =>
        Environment.GetEnvironmentVariable("WLA_PASSWORD_VERTICAL") ?? "WlnGrowth";

    protected string PasswordPool =>
        Environment.GetEnvironmentVariable("WLA_PASSWORD_POOL") ?? "WestlawAdvantage";

    // Checked-out credential -- set in SetUp, released in TearDown
    private PasswordPoolCredential? _credential;

    protected string TestUsername => _credential?.Username ?? throw new InvalidOperationException("Credential not checked out");
    protected string TestPassword => _credential?.Password ?? throw new InvalidOperationException("Credential not checked out");
    protected string TestClientId => _credential?.ClientId ?? "Wla Test";

    // IAC overrides applied via /routing before sign-in
    protected virtual string[] InfrastructureAccessControlsOn =>
    [
        "IAC-AIJS-INCLUDE-RELATED-FEDERAL",
        "IAC-AI-50SS-PROFILE13"
    ];

    // ── Playwright context options ────────────────────────────────────────────
    public override BrowserNewContextOptions ContextOptions()
    {
        var isCI = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("JENKINS_URL"));
        return new BrowserNewContextOptions
        {
            IgnoreHTTPSErrors = true,
            ViewportSize = isCI ? new ViewportSize { Width = 1916, Height = 1000 } : null,
            ScreenSize   = isCI ? null : new ScreenSize { Width = 1920, Height = 1080 },
            AcceptDownloads = true,
        };
    }

    // ── Credential lifecycle ──────────────────────────────────────────────────
    [TearDown]
    public async Task CheckInCredential()
    {
        if (_credential != null)
        {
            await _credential.DisposeAsync();
            _credential = null;
        }
    }

    // ── Sign-on ───────────────────────────────────────────────────────────────
    [SetUp]
    public async Task SignIn()
    {
        _credential = await PasswordPoolCredential.CheckOutAsync(PasswordVertical, PasswordPool);
        TestContext.WriteLine($"[CREDENTIAL] Checked out: {TestUsername}");

        Page.SetDefaultTimeout(90000);

        // Step 1: Apply routing settings pre-sign-in
        // Navigate to /routing, set FAC/IAC overrides, Save (noRedirect=False).
        // WLN saves routing key to cookie and redirects to sign-on page with tracetoken.
        await ApplyRoutingSettingsPreSignIn();

        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        TestContext.WriteLine($"[SIGNIN] After routing save. URL: {Page.Url}");

        // Step 2: Username
        const string UsernameLocator =
            "#Username, #coid_website_userNameTextbox, " +
            "input[name='username'], input[name='loginfmt'], " +
            "input[type='email']";
        var usernameInput = Page.Locator(UsernameLocator).First;
        await usernameInput.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 30000
        });
        await usernameInput.FillAsync(TestUsername);
        TestContext.WriteLine($"[SIGNIN] Entered username. URL: {Page.Url}");

        // Step 3: Password -- single-page vs two-step
        const string PasswordLocator =
            "#Password, #coid_website_passwordTextbox, " +
            "input[name='password'], input[type='password']";

        var passwordAlreadyVisible = await Page.Locator(PasswordLocator).First.IsVisibleAsync();
        if (!passwordAlreadyVisible)
        {
            await Page.Locator(
                "button[type='submit'], button[data-action-button-primary='true'], input[type='submit']").First.ClickAsync();
            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            TestContext.WriteLine("[SIGNIN] Submitted username (two-step flow)");
        }
        else
        {
            TestContext.WriteLine("[SIGNIN] Single-page sign-on -- filling password directly");
        }

        var passwordInput = Page.Locator(PasswordLocator).First;
        await passwordInput.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 20000 });
        await passwordInput.ClickAsync();
        await passwordInput.PressSequentiallyAsync(TestPassword, new LocatorPressSequentiallyOptions { Delay = 50 });

        await Page.Locator(
            "button[type='submit'], button[data-action-button-primary='true'], input[type='submit']").First.ClickAsync();
        TestContext.WriteLine("[SIGNIN] Submitted password");
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        TestContext.WriteLine($"[SIGNIN] After password submit. URL: {Page.Url}");

        // Step 4: Cookie settings dialog (if shown)
        var cookieSettingBtn = Page.Locator("button:has-text('Cookie Settings')");
        if (await cookieSettingBtn.IsVisibleAsync())
        {
            await cookieSettingBtn.ClickAsync();
            await Page.Locator("button:has-text('Allow All'), button:has-text('Accept All')").First
                .ClickAsync(new LocatorClickOptions { Timeout = 5000 });
        }

        // Step 5: Client ID page (if shown)
        var clientIdInput = Page.Locator(
            "xpath=//input[contains(@class,'co_clientIDTextbox') or @id='co_clientIDTextbox'] | //input[@id='clientidr']");
        if (await clientIdInput.IsVisibleAsync())
        {
            await clientIdInput.FillAsync(TestClientId);
            await Page.Locator(
                "xpath=//input[@id='co_clientIDContinueButton'] | //input[@value='Submit']").First.ClickAsync();
        }

        // Step 5b: Stay on WLN Classic home -- wait for session to fully initialize.
        // The shim stays on /Search/Home.html?bhcp=1 after sign-in.
        // NavigateToLandingPage() navigates to WLA home from here.
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        if (Page.Url.Contains("bhcp=1") || Page.Url.Contains("/Search/Home.html"))
        {
            TestContext.WriteLine($"[SIGNIN] WLN Classic home. URL: {Page.Url}");
            try
            {
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
            }
            catch (TimeoutException) { /* background polls -- safe to continue */ }
            TestContext.WriteLine($"[SIGNIN] WLN session initialized. URL: {Page.Url}");
        }

        // Step 6: Verify authenticated
        if (!Page.Url.Contains("/Advantage/Home") && !Page.Url.Contains("/Advantage/Search")
            && !Page.Url.Contains("/Search/Home.html"))
        {
            var screenshotPath = Path.Combine(Path.GetTempPath(), $"wla_signin_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            await File.WriteAllBytesAsync(screenshotPath, await Page.ScreenshotAsync());
            throw new InvalidOperationException(
                $"Sign-in did not reach WLA home. Current URL: {Page.Url}\nScreenshot: {screenshotPath}");
        }
        TestContext.WriteLine($"[SIGNIN] Signed in. URL: {Page.Url}");
    }

    // ── Apply routing settings pre-sign-in ────────────────────────────────────
    /// <summary>
    /// Navigate to /routing BEFORE sign-in, set FAC/IAC overrides, then Save (noRedirect=False).
    /// WLN stores the routing key in a cookie and redirects to sign-on page.
    /// On COSI callback, WLN reads the routing cookie and applies FAC/IAC to the authenticated session.
    /// </summary>
    private async Task ApplyRoutingSettingsPreSignIn()
    {
        await Page.GotoAsync($"{BaseUrl}/routing",
            new PageGotoOptions { Timeout = 30000 });
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        TestContext.WriteLine($"[ROUTING] Routing page loaded. URL: {Page.Url}");

        var saveBtnCount = await Page.Locator("#coid_website_routingSaveButton").CountAsync();
        TestContext.WriteLine($"[ROUTING] Save button count: {saveBtnCount}");

        // BlockCiam=True -> forces classic OnePass sign-on instead of CIAM/Auth0
        var blockCiam = Page.Locator("#BlockCiam");
        if (await blockCiam.CountAsync() > 0)
        {
            await blockCiam.SelectOptionAsync("True");
            TestContext.WriteLine("[ROUTING] Set BlockCiam=True");
        }
        else
            TestContext.WriteLine("[ROUTING] BlockCiam dropdown not found");

        // EnableCiam=False -- belt-and-suspenders
        var enableCiam = Page.Locator("#EnableCiam");
        if (await enableCiam.CountAsync() > 0)
        {
            await enableCiam.SelectOptionAsync("False");
            TestContext.WriteLine("[ROUTING] Set EnableCiam=False");
        }
        else
            TestContext.WriteLine("[ROUTING] EnableCiam dropdown not found");

        // Expand Feature Selection section if collapsed
        var featureLink = Page.Locator("#co_website_resourceInfoTypeLink");
        var featureContainer = Page.Locator(".co_website_resourceInfoTypeConfiguration");
        var featureLinkCount = await featureLink.CountAsync();
        var containerVisible = await featureContainer.IsVisibleAsync();
        TestContext.WriteLine($"[ROUTING] Feature link count: {featureLinkCount}, Container visible: {containerVisible}");

        if (featureLinkCount > 0 && !containerVisible)
        {
            try
            {
                await featureLink.ClickAsync(new LocatorClickOptions { Timeout = 5000 });
                await featureContainer.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 5000
                });
                TestContext.WriteLine("[ROUTING] Expanded Feature Selection section");
            }
            catch (TimeoutException)
            {
                TestContext.WriteLine("[ROUTING] WARNING: Feature Selection section did not expand");
            }
        }

        // Diagnostic: log FAC element state
        var facDiag = await Page.EvaluateAsync<string>(@"() => {
            const ids = [
                'co_website_resourceInfoTypes_AIResearchFiftyStates',
                'co_website_resourceInfoTypes_IndigoPremiumF1',
                'co_website_resourceInfoTypes_IgnoreAuthorizationBlocks'
            ];
            return ids.map(id => {
                const el = document.getElementById(id);
                if (!el) return id + '=NOT_FOUND';
                return id + ': disabled=' + el.disabled + ' value=' + el.value + ' options=' + el.options.length;
            }).join(' | ');
        }");
        TestContext.WriteLine($"[ROUTING] FAC diag: {facDiag}");

        // Set FAC values
        var facIds = new[]
        {
            ("#co_website_resourceInfoTypes_AIResearchFiftyStates", "GRANT"),
            ("#co_website_resourceInfoTypes_IndigoPremiumF1",       "GRANT"),
            ("#co_website_resourceInfoTypes_IgnoreAuthorizationBlocks", "GRANT"),
        };
        foreach (var (selector, value) in facIds)
        {
            var el = Page.Locator(selector);
            if (await el.CountAsync() > 0)
            {
                await Page.EvaluateAsync($@"() => {{
                    const el = document.querySelector('{selector}');
                    if (el) el.disabled = false;
                }}");
                try
                {
                    await el.SelectOptionAsync(value, new LocatorSelectOptionOptions { Timeout = 3000 });
                    TestContext.WriteLine($"[ROUTING] Set {selector}={value} (native)");
                }
                catch (Exception ex)
                {
                    await Page.EvaluateAsync($@"() => {{
                        const el = document.querySelector('{selector}');
                        if (el) {{
                            el.value = '{value}';
                            el.dispatchEvent(new Event('change', {{ bubbles: true }}));
                        }}
                    }}");
                    TestContext.WriteLine($"[ROUTING] Set {selector}={value} (JS fallback, err: {ex.Message[..Math.Min(60, ex.Message.Length)]})");
                }
            }
            else
                TestContext.WriteLine($"[ROUTING] {selector} NOT FOUND in DOM");
        }

        // Set IAC textarea
        if (InfrastructureAccessControlsOn.Length > 0)
        {
            var iacInput = Page.Locator("#InfrastructureAccessControls");
            if (await iacInput.CountAsync() > 0)
            {
                var iacValue = string.Join(",", InfrastructureAccessControlsOn);
                await iacInput.FillAsync(iacValue);
                TestContext.WriteLine($"[ROUTING] Set IACs: {iacValue}");
            }
            else
                TestContext.WriteLine("[ROUTING] WARNING: IAC textarea not found");
        }

        // Pre-save verification: confirm FAC values are still set and not reset by JS/React
        var facPreSave = await Page.EvaluateAsync<string>(@"() => {
            const ids = [
                'co_website_resourceInfoTypes_AIResearchFiftyStates',
                'co_website_resourceInfoTypes_IndigoPremiumF1',
                'co_website_resourceInfoTypes_IgnoreAuthorizationBlocks'
            ];
            return ids.map(id => {
                const el = document.getElementById(id);
                if (!el) return id + '=NOT_FOUND';
                return id.replace('co_website_resourceInfoTypes_','') + '=VALUE:' + el.value + ' DISABLED:' + el.disabled + ' NAME:' + el.name;
            }).join(' | ');
        }");
        TestContext.WriteLine($"[ROUTING] Pre-save FAC state: {facPreSave}");

        // Dump all named form fields to see exactly what will be submitted
        var formDump = await Page.EvaluateAsync<string>(@"() => {
            const els = document.querySelectorAll('input[name], select[name], textarea[name]');
            const relevant = [];
            for (const el of els) {
                const v = el.value;
                if (v || el.tagName === 'SELECT') {
                    relevant.push(el.name + '=' + (v || '(empty)') + (el.disabled ? '[DISABLED]' : ''));
                }
            }
            return relevant.join(' | ');
        }");
        TestContext.WriteLine($"[ROUTING] Form fields: {formDump}");

        // Click Save (noRedirect=False -- redirect to sign-on page)
        var saveBtn = Page.Locator("#coid_website_routingSaveButton");
        if (await saveBtn.CountAsync() > 0)
        {
            await saveBtn.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });
            await saveBtn.ClickAsync(new LocatorClickOptions { Timeout = 10000 });
            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            TestContext.WriteLine($"[ROUTING] After save. URL: {Page.Url}");
        }
        else
            TestContext.WriteLine("[ROUTING] WARNING: Save button not found -- routing NOT applied");
    }

    // ── Navigation helper ─────────────────────────────────────────────────────
    /// <summary>
    /// Navigate to the AJS landing page.
    ///
    /// SHIM EQUIVALENT (WlaAjsBaseTest.NavigateToLandingPage):
    ///   var homePage = GetHomePage&lt;AdvantageHomePage&gt;();          // already on WLA home after sign-in
    ///   homePage.FeaturesIncludedPanel
    ///       .GetWidgetLinkByTitle("AI Jurisdictional Surveys")    // XPath: //*[@class='Athens-features-card']//*[text()='AI Jurisdictional Surveys']
    ///       .Click&lt;AiJurisdictionalSurveysPage&gt;();
    ///
    /// Shim's WLA home URL (WlaBaseTest.F1HomePageLink):
    ///   /Advantage/Home?transitionType=Default&contextData=(sc.Default)&bhcp=1&firstPage=true
    ///
    /// bhcp=1 initializes WLA from the current WLN session (routing grants already applied).
    /// firstPage=true ensures Athens feature-card tiles render (without it the account may
    /// show "AI Deep Research" view instead of the tile grid).
    /// </summary>
    protected async Task<AiJurisdictionalSurveysPagePw> NavigateToLandingPage()
    {
        TestContext.WriteLine($"[AJS] NavigateToLandingPage. Current URL: {Page.Url}");

        // Navigate to WLA home -- same URL as shim's F1HomePageLink.
        // bhcp=1 seeds WLA session from the authenticated WLN session.
        // firstPage=true forces Athens feature-card tile view.
        var wlaHomeUrl = $"{BaseUrl}/Search/Home.html?transitionType=Default&contextData=(sc.Default)&bhcp=1";
        TestContext.WriteLine($"[AJS] Navigating to WLA home: {wlaHomeUrl}");
        try
        {
            await Page.GotoAsync(wlaHomeUrl, new PageGotoOptions
            {
                WaitUntil = WaitUntilState.DOMContentLoaded,
                Timeout = 60000
            });
        }
        catch (PlaywrightException ex) when (ex.Message.Contains("ERR_ABORTED") || ex.Message.Contains("net::"))
        {
            TestContext.WriteLine($"[AJS] WLA home nav threw (redirect): {ex.Message.Split('\n')[0]}");
            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded, new PageWaitForLoadStateOptions { Timeout = 30000 });
        }
        TestContext.WriteLine($"[AJS] After WLA home nav. URL: {Page.Url}");

        // Handle session-start overlay if present.
        // Appears when no WLA session exists yet -- bhcp=1 triggers the overlay instead of
        // initializing silently. Click "Start new session" to create the WLA session, then
        // navigate back to WLA home with bhcp=1 to sync routing grants into it.
        var selectPrevBtn = Page.Locator("button:has-text('Select a previous Client ID')");
        try
        {
            await selectPrevBtn.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 8000
            });
            TestContext.WriteLine("[AJS] Session-start overlay detected");

            // Log buttons for diagnostics
            var overlayBtns = await Page.EvaluateAsync<string[]>(@"() =>
                Array.from(document.querySelectorAll('button')).map(b => b.innerText.trim().replace(/\n/g,' '))");
            foreach (var b in overlayBtns ?? [])
                TestContext.WriteLine($"[AJS] Overlay button: '{b}'");

            var startBtn = Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions
            {
                Name = "Start new session",
                Exact = false
            });
            await startBtn.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 8000
            });
            await startBtn.ClickAsync();
            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            TestContext.WriteLine($"[AJS] Session started. URL: {Page.Url}");

            // WLA session now exists -- navigate back with bhcp=1 to sync routing grants.
            TestContext.WriteLine($"[AJS] Re-navigating to WLA home with bhcp=1 to sync routing grants");
            try
            {
                await Page.GotoAsync(wlaHomeUrl, new PageGotoOptions
                {
                    WaitUntil = WaitUntilState.DOMContentLoaded,
                    Timeout = 30000
                });
            }
            catch (PlaywrightException ex2) when (ex2.Message.Contains("ERR_ABORTED") || ex2.Message.Contains("net::"))
            {
                await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded, new PageWaitForLoadStateOptions { Timeout = 20000 });
            }
            TestContext.WriteLine($"[AJS] After re-nav with bhcp=1. URL: {Page.Url}");
        }
        catch (TimeoutException)
        {
            TestContext.WriteLine("[AJS] No session-start overlay -- proceeding");
        }

        // Wait for the page to settle after session initialization
        try { await Page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 8000 }); } catch (TimeoutException) { }

        // Navigate directly to the AJS page.
        // The AJS tile on the home page opens in a new tab (target="_blank"), which would require
        // tab management. Instead, navigate directly -- the WLA session is now initialized with
        // routing grants via bhcp=1, so direct navigation works.
        // Confirmed URL from home page link dump: /AIJurisdictionalSurveys (not /Advantage/AIJurisdictionalSurveys)
        var ajsUrl = $"{BaseUrl}/AIJurisdictionalSurveys?transitionType=Default&contextData=(sc.Default)";
        TestContext.WriteLine($"[AJS] Navigating directly to AJS: {ajsUrl}");
        try
        {
            await Page.GotoAsync(ajsUrl, new PageGotoOptions
            {
                WaitUntil = WaitUntilState.DOMContentLoaded,
                Timeout = 30000
            });
        }
        catch (PlaywrightException ex3) when (ex3.Message.Contains("ERR_ABORTED") || ex3.Message.Contains("net::"))
        {
            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded, new PageWaitForLoadStateOptions { Timeout = 20000 });
        }

        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        TestContext.WriteLine($"[AJS] Before WaitForPageReady. URL: {Page.Url}");

        var ajsPage = new AiJurisdictionalSurveysPagePw(Page);
        await ajsPage.WaitForPageReady();
        return ajsPage;
    }

    // ── Sign-out / sign-back-in ───────────────────────────────────────────────
    /// <summary>
    /// Signs out of WLA. Used in copy-link test to verify permanent links survive a session change.
    /// </summary>
    protected async Task SignOut()
    {
        await Page.GotoAsync($"{BaseUrl}/Advantage/SignOut",
            new PageGotoOptions { Timeout = 30000 });
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        TestContext.WriteLine("[SIGNOUT] Signed out.");
    }

    /// <summary>
    /// Signs back in with the already-checked-out credential (no routing step needed).
    /// </summary>
    protected async Task SignBackIn()
    {
        await Page.GotoAsync($"{BaseUrl}/Advantage/Home",
            new PageGotoOptions { Timeout = 60000 });
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        const string SignBackInUsernameLocator =
            "#Username, #coid_website_userNameTextbox, " +
            "input[name='username'], input[name='loginfmt'], input[type='email']";
        var usernameInput2 = Page.Locator(SignBackInUsernameLocator).First;
        await usernameInput2.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 30000
        });
        await usernameInput2.FillAsync(TestUsername);

        const string SignBackInPasswordLocator =
            "#Password, #coid_website_passwordTextbox, " +
            "input[name='password'], input[type='password']";
        var pwdVisibleBeforeSubmit = await Page.Locator(SignBackInPasswordLocator).First.IsVisibleAsync();
        if (!pwdVisibleBeforeSubmit)
        {
            await Page.Locator(
                "button[type='submit'], button[data-action-button-primary='true'], input[type='submit']").First.ClickAsync();
            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }

        var passwordInput2 = Page.Locator(SignBackInPasswordLocator).First;
        await passwordInput2.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 20000
        });
        await passwordInput2.ClickAsync();
        await passwordInput2.PressSequentiallyAsync(TestPassword,
            new LocatorPressSequentiallyOptions { Delay = 50 });
        await Page.Locator(
            "button[type='submit'], button[data-action-button-primary='true'], input[type='submit']").First.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        var clientIdInput = Page.Locator(
            "xpath=//input[contains(@class,'co_clientIDTextbox') or @id='co_clientIDTextbox'] | //input[@id='clientidr']");
        if (await clientIdInput.IsVisibleAsync())
        {
            await clientIdInput.FillAsync(TestClientId);
            await Page.Locator(
                "xpath=//input[@id='co_clientIDContinueButton'] | //input[@value='Submit']")
                .First.ClickAsync();
        }

        await Page.WaitForURLAsync(
            url => url.Contains("/Advantage/Home")
                || url.Contains("/Advantage/Search")
                || url.Contains("/Search/Home.html"),
            new PageWaitForURLOptions { Timeout = 60000 });
        TestContext.WriteLine($"[SIGNIN] Signed back in. URL: {Page.Url}");
    }

    // ── Header tab navigation ─────────────────────────────────────────────────
    protected async Task<ILocator> ClickHeaderFoldersTab()
    {
        var tab = Page.Locator(
            "[data-automation='header-tab-folders'], " +
            "[aria-label='Folders'], " +
            "[class*='headerTab'] button:has-text('Folders'), " +
            "nav button:has-text('Folders')").First;
        await tab.ClickAsync();
        var panel = Page.Locator(
            "[data-automation='recent-folders-panel'], " +
            "[class*='recentFolders'], " +
            "[role='dialog'][aria-label*='folder' i]");
        await panel.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 10000
        });
        return panel;
    }

    protected async Task<ILocator> ClickHeaderHistoryTab()
    {
        var tab = Page.Locator(
            "[data-automation='header-tab-history'], " +
            "[aria-label='History'], " +
            "[class*='headerTab'] button:has-text('History'), " +
            "nav button:has-text('History')").First;
        await tab.ClickAsync();
        var panel = Page.Locator(
            "[data-automation='recent-history-panel'], " +
            "[class*='recentHistory'], " +
            "[role='dialog'][aria-label*='history' i]");
        await panel.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 10000
        });
        return panel;
    }

    // ── Test folder helper ────────────────────────────────────────────────────
    protected async Task PrepareTestFolder(string folderName = "WlaAjsTestFolder")
    {
        await Page.GotoAsync($"{BaseUrl}/Advantage/Folder",
            new PageGotoOptions { Timeout = 30000 });
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        // Check if folder already exists in the folder tree
        // Shim XPath: .//div[contains(@class,'co_tree_element')]//*[text()='{folderName}']
        var existingFolder = Page.Locator(
            $"xpath=//div[contains(@class,'co_tree_element')]//*[normalize-space(text())='{folderName}']");
        if (await existingFolder.IsVisibleAsync())
        {
            TestContext.WriteLine($"[FOLDER] '{folderName}' already exists.");
            return;
        }

        // Click "New folder" button
        // Shim XPath: .//*[contains(@class,'icon_addFolder-blue')] |
        //             //*[@id='co_ro_folder_menu']/li[@class='co_createNewFolder']/button
        var newFolderBtn = Page.Locator(
            "[class*='icon_addFolder-blue'], " +
            "#co_ro_folder_menu li.co_createNewFolder button").First;
        await newFolderBtn.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 10000
        });
        await newFolderBtn.ClickAsync();

        // Enter folder name in the dialog
        // Shim CSS: div.co_overlayBox_content form input#cobalt_ro_folder_action_textbox
        var nameInput = Page.Locator("#cobalt_ro_folder_action_textbox");
        await nameInput.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 5000
        });
        await nameInput.FillAsync(folderName);

        // Click OK button
        // Shim XPath: //button[contains(@class,'co_dropdownBox_ok')]
        var okBtn = Page.Locator("button[class*='co_dropdownBox_ok']").First;
        await okBtn.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        TestContext.WriteLine($"[FOLDER] Created folder '{folderName}'.");
    }
}
