namespace WestlawAdvantage.Playwright.AJS.Pages.Components;

using Microsoft.Playwright;

/// <summary>
/// Native Playwright component for the AJS survey result section.
///
/// SHIM VERSION EQUIVALENT: WlaAjsResultComponent (Framework.Common.UI)
///
/// The original used ILabel wrappers around By.XPath locators.
/// Native Playwright uses ILocator directly — no wrapper class needed.
///
/// SHIM XPath patterns used:
///   FederalStatutesRegulationsHeading:
///     //h4[contains(@class,'resultsSummaryHeading') and normalize-space(.)='Federal']
///     /following-sibling::h5[contains(@class,'resultsSummarySubHeading') and normalize-space(.)='Statutes and regulations']
///
///   CasesStateHeading:
///     //h5[contains(@class,'casesSectionHeading') and normalize-space(.)='Cases']
///     /ancestor::*[contains(@class,'casesSection')][1]
///     //h6[contains(@class,'resultsSummaryCases') and normalize-space(.)='State']
///
/// Native Playwright can use CSS :has() and :has-text() to express the same thing
/// more readably. The XPath is preserved as a fallback in comments.
///
/// TODO: Run the tests with Page.Pause() after a survey completes to inspect
///       the actual heading structure and verify/adjust these locators.
/// </summary>
public class AjsResultComponentPw
{
    private readonly IPage _page;

    public AjsResultComponentPw(IPage page) => _page = page;

    // ── Result section headings ───────────────────────────────────────────────

    /// <summary>
    /// "Statutes and regulations" sub-heading under the "Federal" heading.
    /// Replaces: surveysPage.WlaSurveyResult.FederalStatutesRegulationsHeading.Displayed
    ///
    /// SHIM XPath:
    ///   //h4[contains(@class,'resultsSummaryHeading') and normalize-space(.)='Federal']
    ///   /following-sibling::h5[contains(@class,'resultsSummarySubHeading')
    ///     and normalize-space(.)='Statutes and regulations']
    ///
    /// Playwright CSS equivalent using :has-text():
    /// </summary>
    // Shim XPath: //h4[contains(@class,'resultsSummaryHeading') and normalize-space(.)='Federal']
    //             /following-sibling::h5[contains(@class,'resultsSummarySubHeading')
    //               and normalize-space(.)='Statutes and regulations']
    public ILocator FederalStatutesRegulationsHeading =>
        _page.Locator("xpath=//h4[contains(@class,'resultsSummaryHeading') and normalize-space(.)='Federal']/following-sibling::h5[contains(@class,'resultsSummarySubHeading') and normalize-space(.)='Statutes and regulations']");

    /// <summary>
    /// "State statutes and regulations" heading for a given state display name.
    /// Replaces: surveysPage.WlaSurveyResult.StateStatutesRegulationsHeading(JurisCAName).Displayed
    ///
    /// The state name (e.g., "California") appears in the h4 heading.
    /// Shim XPath mask: .//h4[contains(@class,'resultsSummaryHeading') and normalize-space(.)='{0}']
    ///   /following-sibling::h5[contains(@class,'resultsSummarySubHeading')
    ///     and normalize-space(.)='State statutes and regulations']
    /// </summary>
    public ILocator StateStatutesRegulationsHeading(string stateDisplayName) =>
        _page.Locator($"xpath=//h4[contains(@class,'resultsSummaryHeading') and normalize-space(.)='{stateDisplayName}']/following-sibling::h5[contains(@class,'resultsSummarySubHeading') and normalize-space(.)='State statutes and regulations']");

    /// <summary>
    /// "State" sub-heading under the "Cases" section.
    /// Replaces: surveysPage.WlaSurveyResult.CasesStateHeading.Displayed
    ///
    /// Shim XPath: //h5[contains(@class,'casesSectionHeading') and normalize-space(.)='Cases']
    ///   /ancestor::*[contains(@class,'casesSection')][1]
    ///   //h6[contains(@class,'resultsSummaryCases') and normalize-space(.)='State']
    /// </summary>
    public ILocator CasesStateHeading =>
        _page.Locator("xpath=//h5[contains(@class,'casesSectionHeading') and normalize-space(.)='Cases']/ancestor::*[contains(@class,'casesSection')][1]//h6[contains(@class,'resultsSummaryCases') and normalize-space(.)='State']");

    /// <summary>
    /// "Federal" sub-heading under the "Cases" section.
    /// Replaces: surveysPage.WlaSurveyResult.CasesFederalHeading.Displayed
    ///
    /// Shim XPath (same structure, different text):
    ///   .../h6[normalize-space(.)='Federal']
    /// </summary>
    public ILocator CasesFederalHeading =>
        _page.Locator("xpath=//h5[contains(@class,'casesSectionHeading') and normalize-space(.)='Cases']/ancestor::*[contains(@class,'casesSection')][1]//h6[contains(@class,'resultsSummaryCases') and normalize-space(.)='Federal']");

    /// <summary>
    /// Returns all jurisdiction label texts from the result.
    /// Shim: By.XPath("//h4[contains(@class,'resultsSummaryHeading')]") → IWebElement.Text per element
    /// Native: AllTextContentsAsync() — one call for all.
    /// </summary>
    public async Task<List<string>> GetAllJurisdictionLabels()
    {
        var allTexts = await _page
            .Locator("xpath=//h4[contains(@class,'resultsSummaryHeading')]")
            .AllTextContentsAsync();
        return allTexts.Where(t => !string.IsNullOrWhiteSpace(t)).ToList();
    }
}

/// <summary>
/// Survey result metadata — timestamp, question label, etc.
/// Replaces: surveysPage.SurveyResult.TimeStampLabel, surveysPage.SurveyResult.QuestionLabel
/// TODO: Verify locators by inspecting the result area DOM.
/// </summary>
public class AjsSurveyResultMetaPw
{
    private readonly IPage _page;

    public AjsSurveyResultMetaPw(IPage page) => _page = page;

    // Shim: By.XPath("//time[contains(@class,'resultsTimeStamp')]")
    public ILocator TimeStampLabel =>
        _page.Locator("xpath=//time[contains(@class,'resultsTimeStamp')]");

    public ILocator QuestionLabel =>
        _page.Locator("[data-automation='survey-question'], [class*='questionLabel'], h2[class*='question']");

    public async Task<string> GetTimestamp() =>
        await TimeStampLabel.TextContentAsync() ?? string.Empty;

    public async Task<string> GetQuestion() =>
        await QuestionLabel.TextContentAsync() ?? string.Empty;
}

/// <summary>
/// Content type / jurisdiction filter chips shown above the result.
/// Replaces: surveysPage.ContentType.JurisdictionContentTypes
/// TODO: Verify locators.
/// </summary>
public class AjsContentTypeComponentPw
{
    private readonly IPage _page;

    public AjsContentTypeComponentPw(IPage page) => _page = page;

    /// <summary>
    /// Returns the ordered list of jurisdiction filter names shown in the result header.
    /// Replaces: surveysPage.ContentType.JurisdictionContentTypes (IList&lt;string&gt;)
    /// Used in CopyLink and History tests to verify filter names are preserved.
    /// TODO: Find exact locator for the filter chip labels.
    /// </summary>
    public async Task<List<string>> GetJurisdictionContentTypes()
    {
        var texts = await _page
            .Locator(
                "[data-automation='juris-filter-chip'], " +
                "[class*='jurisdictionFilter'] [class*='label'], " +
                "[class*='contentTypeFilter'] span"
            )
            .AllTextContentsAsync();
        return texts.Where(t => !string.IsNullOrWhiteSpace(t)).ToList();
    }
}