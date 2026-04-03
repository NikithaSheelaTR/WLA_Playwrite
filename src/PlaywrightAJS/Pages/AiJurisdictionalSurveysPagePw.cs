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
        _page.Locator("xpath=//saf-status[@message='Creating your survey. It will appear in History when completed.']");

    /// <summary>
    /// The page description text shown on the landing page (before any survey is run).
    /// Used to confirm the page has loaded.
    /// Replaces: SafeMethodExecutor.ExecuteUntil(() => PageDescription.Displayed)
    /// </summary>
    public ILocator PageDescription =>
        _page.Locator("h3#fiftyStateSearchCriteria");

    /// <summary>
    /// The "Create Survey" button at the top of the form.
    /// Replaces: surveysPage.CreateSurveyButtonTop.Click&lt;AiJurisdictionalSurveysPage&gt;()
    /// </summary>
    public ILocator CreateSurveyButtonTop =>
        _page.Locator("saf-button#Create-Survey-Button-1");

    /// <summary>
    /// The main page heading label.
    /// Replaces: surveysPage.PageHeaderLabel.Text
    /// </summary>
    public ILocator PageHeaderLabel =>
        _page.Locator("h1#fiftyStateHeading, h2#fiftyStateHeading").First;

    /// <summary>
    /// Success label shown after copying a link.
    /// Replaces: surveysPage.CopiedLinkSuccessLabel.Displayed
    /// </summary>
    public ILocator CopiedLinkSuccessLabel =>
        _page.Locator("div.saf-alert__content");

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
        await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        // Fail fast if the account doesn't have AJS access — shows a 404/error page
        var notFoundMsg = _page.Locator("text=Sorry, we can't find the page you're looking for");
        if (await notFoundMsg.IsVisibleAsync())
        {
            var shot = Path.Combine(Path.GetTempPath(), $"wla_ajs_noaccess_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            await File.WriteAllBytesAsync(shot, await _page.ScreenshotAsync());
            throw new InvalidOperationException(
                $"AJS page returned a 404/not-found error. " +
                $"The checked-out credential does not have AIJurisdictionalSurveys feature access. " +
                $"URL: {_page.Url}  screenshot={shot}");
        }

        // Dismiss Pendo onboarding overlay before waiting for page content.
        // The overlay can delay rendering of the page description element.
        await ClosePendoMessage();

        // Wait for the AJS landing page description heading.
        // Confirmed shim XPath: //h3[@id='fiftyStateSearchCriteria']
        // Fallback: the jurisdictions selected-count label (also confirmed by shim).
        try
        {
            await PageDescription.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 30000
            });
        }
        catch (TimeoutException)
        {
            // Capture screenshot to diagnose what is actually on screen
            var shot = Path.Combine(Path.GetTempPath(), $"wla_ajs_pageready_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            await File.WriteAllBytesAsync(shot, await _page.ScreenshotAsync());

            // Try fallback: jurisdictions selected-count label
            var fallback = _page.Locator("span[class*='jurisdictionCardSelectedCount']");
            var fallbackVisible = await fallback.IsVisibleAsync();
            if (!fallbackVisible)
                throw new InvalidOperationException(
                    $"AJS landing page did not become ready (h3#fiftyStateSearchCriteria not found, " +
                    $"jurisdictions label also absent). URL: {_page.Url}  screenshot={shot}");
        }
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
    public async Task WaitForSurveyComplete(int timeoutMs = 300000)
    {
        // Try to detect the spinner and wait for it to disappear.
        // If the spinner locator doesn't match (wrong message text, shadow DOM, etc.)
        // we fall through to the definitive result-heading wait below.
        try
        {
            await ProgressLabel.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 5000 // short — if it never appears, skip spinner strategy
            });
            // Spinner appeared — wait for it to go away
            await ProgressLabel.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Hidden,
                Timeout = timeoutMs
            });
        }
        catch (TimeoutException)
        {
            // Spinner never appeared or locator didn't match — fall through
        }

        // Definitive signal: wait for the first result section heading to be visible.
        // This works whether the spinner matched or not, and whether the survey
        // was "instant" or took 3 minutes.
        // Shim XPath: //h4[contains(@class,'resultsSummaryHeading')]
        await _page
            .Locator("h4[class*='resultsSummaryHeading']")
            .First
            .WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
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
        // Use Pendo-specific selectors only — [aria-label='Close'] is too broad and matches
        // other saf-button elements (toolbars, headers) that are not Pendo guides.
        var closeBtn = _page.Locator("._pendo-close-guide, [data-automation='pendo-close']");
        if (await closeBtn.IsVisibleAsync())
        {
            try
            {
                await closeBtn.First.ClickAsync(new LocatorClickOptions { Timeout = 3000 });
            }
            catch (TimeoutException)
            {
                // Pendo guide dismissed itself or was not clickable — safe to continue
            }
        }
    }
}