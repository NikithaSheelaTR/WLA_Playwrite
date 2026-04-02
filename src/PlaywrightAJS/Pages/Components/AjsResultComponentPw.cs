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
    public ILocator FederalStatutesRegulationsHeading =>
        _page.Locator(
            "h5.resultsSummarySubHeading:has-text('Statutes and regulations')" +
            ":near(h4.resultsSummaryHeading:has-text('Federal'))"
        );

    /// <summary>
    /// "State statutes and regulations" heading for a given state display name.
    /// Replaces: surveysPage.WlaSurveyResult.StateStatutesRegulationsHeading(JurisCAName).Displayed
    ///
    /// The state name (e.g., "California") appears in the h4 heading.
    /// </summary>
    public ILocator StateStatutesRegulationsHeading(string stateDisplayName) =>
        _page.Locator(
            $"h5.resultsSummarySubHeading:has-text('Statutes and regulations')" +
            $":near(h4.resultsSummaryHeading:has-text('{stateDisplayName}'))"
        );

    /// <summary>
    /// "State" sub-heading under the "Cases" section.
    /// Replaces: surveysPage.WlaSurveyResult.CasesStateHeading.Displayed
    ///
    /// SHIM XPath:
    ///   //h5[contains(@class,'casesSectionHeading') and normalize-space(.)='Cases']
    ///   /ancestor::*[contains(@class,'casesSection')][1]
    ///   //h6[contains(@class,'resultsSummaryCases') and normalize-space(.)='State']
    /// </summary>
    public ILocator CasesStateHeading =>
        _page.Locator(
            "[class*='casesSection'] h6.resultsSummaryCases:has-text('State'), " +
            "xpath=//h5[contains(@class,'casesSectionHeading') and normalize-space(.)='Cases']" +
            "/ancestor::*[contains(@class,'casesSection')][1]" +
            "//h6[contains(@class,'resultsSummaryCases') and normalize-space(.)='State']"
        ).First;

    /// <summary>
    /// "Federal" sub-heading under the "Cases" section.
    /// Replaces: surveysPage.WlaSurveyResult.CasesFederalHeading.Displayed
    ///
    /// SHIM XPath (same structure, different text):
    ///   .../h6[normalize-space(.)='Federal']
    /// </summary>
    public ILocator CasesFederalHeading =>
        _page.Locator(
            "[class*='casesSection'] h6.resultsSummaryCases:has-text('Federal'), " +
            "xpath=//h5[contains(@class,'casesSectionHeading') and normalize-space(.)='Cases']" +
            "/ancestor::*[contains(@class,'casesSection')][1]" +
            "//h6[contains(@class,'resultsSummaryCases') and normalize-space(.)='Federal']"
        ).First;

    /// <summary>
    /// Returns all jurisdiction label texts from the result.
    /// Replaces: surveysPage.WlaSurveyResult.GetAllJurisdictionLabels()
    ///   .Where(text => !string.IsNullOrWhiteSpace(text)).ToList()
    ///
    /// SHIM: called GetAllJurisdictionLabels() → each label is an IWebElement.Text
    ///       → shim evaluates innerText per element
    ///
    /// NATIVE: AllTextContentsAsync() fetches all in one call — faster.
    ///
    /// TODO: Find the exact locator for jurisdiction labels in the result area.
    /// </summary>
    public async Task<List<string>> GetAllJurisdictionLabels()
    {
        var allTexts = await _page
            .Locator("[data-automation='jurisdiction-label'], [class*='jurisdictionLabel'], [class*='jurisLabel']")
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

    public ILocator TimeStampLabel =>
        _page.Locator("[data-automation='timestamp'], [class*='timeStamp'], [class*='timestamp']");

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