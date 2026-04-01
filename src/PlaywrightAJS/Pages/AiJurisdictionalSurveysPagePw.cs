namespace WestlawAdvantage.Playwright.AJS.Pages;

using Microsoft.Playwright;
using WestlawAdvantage.Playwright.AJS.Pages.Components;

/// <summary>
/// Native Playwright page object for the AI Jurisdictional Surveys page.
///
/// SHIM VERSION EQUIVALENT: AiJurisdictionalSurveysPage (Framework.Common.UI)
///
/// KEY DIFFERENCES:
///   - Constructor takes IPage, not IWebDriver
///   - All properties return ILocator (lazy, not IWebElement)
///   - All methods are async Task / async Task&lt;T&gt;
///   - No "Click&lt;T&gt;()" pattern — click is await locator.ClickAsync()
///   - No SafeMethodExecutor — use WaitForAsync() with explicit state
///   - WaitForSurveyComplete() replaces SafeMethodExecutor.WaitUntil(() => !ProgressLabel.Displayed)
///
/// LOCATOR DISCOVERY:
///   Run the app and use Playwright Inspector to find exact locators:
///     Page.Pause();  // opens Inspector — click elements to see their selectors
///   Or use Codegen:
///     npx playwright codegen https://qed.next.westlaw.com
/// </summary>
public class AiJurisdictionalSurveysPagePw
{
    private readonly IPage _page;

    // ── Page-level locators ──────────────────────────────────────────────────
    // TODO: Verify each locator by inspecting the actual DOM.
    // The data-automation attributes below are educated guesses based on the
    // component names in Framework.Common.UI. Adjust as needed.

    /// <summary>
    /// The spinning/loading indicator shown while a survey is being generated.
    /// Replaces: surveysPage.ProgressLabel.Displayed
    /// Original XPath equivalent: element with "progress" in automation attr or class
    /// </summary>
    public ILocator ProgressLabel =>
        _page.Locator("[data-automation='progress-indicator'], .progress-indicator, [class*='progressLabel']");

    /// <summary>
    /// The page description text shown on the landing page (before any survey is run).
    /// Used to confirm the page has loaded.
    /// Replaces: SafeMethodExecutor.ExecuteUntil(() => PageDescription.Displayed)
    /// </summary>
    public ILocator PageDescription =>
        _page.Locator("[data-automation='page-description'], .page-description, [class*='pageDescription']");

    /// <summary>
    /// The "Create Survey" button at the top of the form.
    /// Replaces: surveysPage.CreateSurveyButtonTop.Click&lt;AiJurisdictionalSurveysPage&gt;()
    /// </summary>
    public ILocator CreateSurveyButtonTop =>
        _page.Locator("[data-automation='create-survey-button-top'], button:has-text('Create')").First;

    /// <summary>
    /// The main page heading label.
    /// Replaces: surveysPage.PageHeaderLabel.Text
    /// </summary>
    public ILocator PageHeaderLabel =>
        _page.Locator("h1, [data-automation='page-header'], [class*='pageHeader']").First;

    /// <summary>
    /// Success label shown after copying a link.
    /// Replaces: surveysPage.CopiedLinkSuccessLabel.Displayed
    /// </summary>
    public ILocator CopiedLinkSuccessLabel =>
        _page.Locator("[data-automation='copy-link-success'], [class*='copyLink'][class*='success']");

    // ── Components ────────────────────────────────────────────────────────────
    public JurisdictionsComponentPw Jurisdictions { get; }
    public WlaQueryBoxComponentPw WlaQueryBox { get; }
    public AjsResultComponentPw WlaSurveyResult { get; }
    public AjsSurveyResultMetaPw SurveyResult { get; }
    public AjsToolbarComponentPw Toolbar { get; }
    public AjsContentTypeComponentPw ContentType { get; }

    public AiJurisdictionalSurveysPagePw(IPage page)
    {
        _page = page;
        Jurisdictions   = new JurisdictionsComponentPw(page);
        WlaQueryBox     = new WlaQueryBoxComponentPw(page);
        WlaSurveyResult = new AjsResultComponentPw(page);
        SurveyResult    = new AjsSurveyResultMetaPw(page);
        Toolbar         = new AjsToolbarComponentPw(page);
        ContentType     = new AjsContentTypeComponentPw(page);
    }

    // ── Page-level actions ────────────────────────────────────────────────────

    /// <summary>
    /// Wait for the AJS landing page to be fully ready.
    /// Replaces: SafeMethodExecutor.ExecuteUntil(() => PageDescription.Displayed, timeoutFromSec: 10)
    ///
    /// Native Playwright: just await — no polling loop, no Thread.Sleep.
    /// Playwright retries the visibility check automatically.
    /// </summary>
    public async Task WaitForPageReady()
    {
        await PageDescription.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 15000
        });
    }

    /// <summary>
    /// Wait for the survey generation to complete (progress spinner disappears).
    ///
    /// SHIM VERSION:
    ///   SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed);
    ///   ← This polls every ~500ms and translates to Playwright CountAsync each iteration.
    ///   ← Subject to timing issues because each poll uses the 2s-capped FindElement.
    ///
    /// NATIVE PLAYWRIGHT:
    ///   await WaitForAsync(Hidden) — Playwright watches the DOM mutation directly.
    ///   No polling. No timeout tuning. Fires exactly when the element disappears.
    /// </summary>
    public async Task WaitForSurveyComplete(int timeoutMs = 180000)
    {
        // First wait for the progress indicator to APPEAR (it may not be there yet)
        // then wait for it to DISAPPEAR. This avoids the race where we check for
        // Hidden before the spinner has even shown up.
        try
        {
            await ProgressLabel.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 5000 // short — if it never appears, survey was instant
            });
        }
        catch (TimeoutException)
        {
            // Progress indicator never appeared — survey completed instantly, continue
            return;
        }

        // Now wait for it to go away
        await ProgressLabel.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Hidden,
            Timeout = timeoutMs
        });
    }

    /// <summary>
    /// Click Create Survey and wait for completion.
    /// Replaces: surveysPage = surveysPage.CreateSurveyButtonTop.Click&lt;AiJurisdictionalSurveysPage&gt;()
    ///           SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed)
    /// </summary>
    public async Task<AiJurisdictionalSurveysPagePw> ClickCreateSurveyAndWait()
    {
        await CreateSurveyButtonTop.ClickAsync();
        await WaitForSurveyComplete();
        return this;
    }

    /// <summary>
    /// Dismiss the Pendo onboarding overlay if present.
    /// Replaces: jurisdictionalSurveysPage.ClosePendoMessage()
    /// Safe to call even if no overlay is shown.
    /// </summary>
    public async Task WaitForCopiedLinkSuccess()
    {
        await CopiedLinkSuccessLabel.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 10000
        });
    }

    public async Task ClosePendoMessage()
    {
        var closeBtn = _page.Locator("._pendo-close-guide, [data-automation='pendo-close'], [aria-label='Close']");
        if (await closeBtn.CountAsync() > 0)
        {
            await closeBtn.First.ClickAsync(new LocatorClickOptions { Timeout = 3000 });
        }
    }
}