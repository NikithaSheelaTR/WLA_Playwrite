namespace WestlawAdvantage.Playwright.AJS.Tests;

using Microsoft.Playwright;
using NUnit.Framework;
using WestlawAdvantage.Playwright.AJS.Infrastructure;

/// <summary>
/// Native Playwright version of WlaAjsIncludeRelatedFedTests.
///
/// ORIGINAL FILE:
///   Seven-Kingdoms/.../AJSForWLAdvantage/WlaAjsIncludeRelatedFedTests.cs
///
/// WHAT CHANGED vs SHIM VERSION:
///   1. [TestClass]/[TestMethod] (MSTest) → [TestFixture]/[Test] (NUnit)
///   2. All test methods are async Task
///   3. this.TestCaseVerify.IsTrue(...) → Assert.That(...)
///   4. Thread.Sleep(1000) → removed entirely
///   5. SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed)
///      → await surveysPage.WaitForSurveyComplete()
///   6. surveysPage.CreateSurveyButtonTop.Click&lt;AiJurisdictionalSurveysPage&gt;()
///      → await surveysPage.ClickCreateSurveyAndWait()
///   7. Clipboard.GetText() → await Page.EvaluateAsync("navigator.clipboard.readText()")
///   8. BrowserPool.CurrentBrowser.CreateTab / ActivateTab → Context.NewPageAsync()
///   9. Shadow DOM JS executor → native Playwright pierce locator
///
/// WHAT STAYED THE SAME:
///   - Test names, test logic, assertion messages
///   - Jurisdiction codes (CA-CS, ALLFEDS, MN-CS, Select All)
///   - Survey questions
///   - Verification points and expected values
///
/// ⚠️ LOCATOR STATUS:
///   All locators are scaffolded from component/class names in existing code.
///   EVERY locator needs to be verified against the live WLA DOM before running.
///
/// RECOMMENDED ORDER TO TACKLE:
///   1. AjsIncludeRelatedFedJurisSelectorTest  — no survey needed, pure UI
///   2. AjsIncludeRelatedFedFolderingTest       — needs survey + folder
///   3. AjsIncludeRelatedFedHistoryTest         — needs survey + history nav
///   4. AjsIncludeRelatedFedDeliveryTest        — needs survey + file download
/// </summary>
[TestFixture]
[Category("WestlawAdvantage")]
[Category("AJS")]
[Category("IncludeRelatedFederal")]
public class WlaAjsIncludeRelatedFedTestsPw : PlaywrightAjsBaseTest
{
    private const string JurisCA                = "CA-CS";
    private const string JurisCAName            = "California";
    private const string JurisMN                = "MN-CS";
    private const string JurisSelectAll         = "Select All";
    private const string JurisIncludeRelatedFed = "ALLFEDS";

    // ─────────────────────────────────────────────────────────────────────────
    // TEST 1: Jurisdiction Selector
    // START HERE — no survey generation required, pure UI state verification
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Verify ALLFEDS checkbox enable/disable behavior as jurisdictions are selected/deselected.
    ///
    /// KEY WAIT IMPROVEMENTS:
    ///   SHIM: No waits between state changes — relies on Selenium's synchronous DOM
    ///         access, which can race against React re-renders.
    ///   NATIVE: Each IsCheckedAsync/IsDisabledAsync auto-waits for the element to be
    ///           stable. CheckAsync/UncheckAsync verify the state actually changed.
    /// </summary>
    [Test]
    public async Task AjsIncludeRelatedFedJurisSelectorTest()
    {
        var surveysPage = await NavigateToLandingPage();

        // 1. ALLFEDS disabled + 0 selected when nothing selected
        Assert.That(
            await surveysPage.Jurisdictions.IsJurisdictionSelectionDisabled(JurisIncludeRelatedFed),
            Is.True,
            "Include related federal is not disabled when no juris selected.");

        Assert.That(
            await surveysPage.Jurisdictions.GetSelectedCountText(),
            Does.Contain("0 selected"),
            "Include related federal is not disabled when no juris selected (count).");

        // 2. Select All → ALLFEDS becomes checked
        await surveysPage.Jurisdictions.SelectJurisdiction(JurisSelectAll);

        Assert.That(
            await surveysPage.Jurisdictions.IsJurisdictionSelected(JurisIncludeRelatedFed),
            Is.True,
            "Include related federal is not selected clicking Select All.");

        // 3. CA selected → ALLFEDS enabled
        await surveysPage.Jurisdictions.SelectJurisdiction(JurisCA);

        Assert.That(
            await surveysPage.Jurisdictions.IsJurisdictionSelectionDisabled(JurisIncludeRelatedFed),
            Is.False,
            "Include related federal is not enabled when a juris is selected.");

        // 4. Select ALLFEDS explicitly → count shows 2 (CA + ALLFEDS)
        // ORIGINAL: SelectJurisdiction(false, JurisIncludeRelatedFed) = deselect/reselect pattern
        // NATIVE: deselect then select for clarity
        await surveysPage.Jurisdictions.DeselectJurisdiction(JurisIncludeRelatedFed);
        await surveysPage.Jurisdictions.SelectJurisdiction(JurisIncludeRelatedFed);

        Assert.That(
            await surveysPage.Jurisdictions.GetSelectedCountText(),
            Does.Contain("2 selected"),
            "Selected count does not increase by 1 when Include related federal is selected.");

        // 5. Deselect CA → ALLFEDS disabled again + 0 selected
        await surveysPage.Jurisdictions.DeselectJurisdiction(JurisCA);

        Assert.That(
            await surveysPage.Jurisdictions.IsJurisdictionSelectionDisabled(JurisIncludeRelatedFed),
            Is.True,
            "Include related federal is not disabled when the only selected juris is unselected.");

        Assert.That(
            await surveysPage.Jurisdictions.GetSelectedCountText(),
            Does.Contain("0 selected"));

        // 6. Select CA + MN + ALLFEDS, deselect MN → ALLFEDS stays selected
        await surveysPage.Jurisdictions.SelectJurisdiction(JurisCA, JurisMN, JurisIncludeRelatedFed);
        await surveysPage.Jurisdictions.DeselectJurisdiction(JurisMN);

        Assert.That(
            await surveysPage.Jurisdictions.IsJurisdictionSelected(JurisIncludeRelatedFed),
            Is.True,
            "Include related federal is not selected when unselecting one of many selected juris.");
    }

    // ─────────────────────────────────────────────────────────────────────────
    // TEST 2: Foldering
    // Requires: survey generation, save to folder, navigate to folder, reopen
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Verify survey saved to folder retains federal/state result headings when reopened.
    ///
    /// KEY WAIT IMPROVEMENTS:
    ///   SHIM: Thread.Sleep(1000) before CreateSurvey click (timing hack)
    ///         SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed)
    ///         SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed, 3000)
    ///   NATIVE: await WaitForSurveyComplete() — watches DOM mutation directly,
    ///           fires exactly when spinner disappears, no polling.
    ///
    /// TODO: Implement PrepareTestFolder() in PlaywrightAjsBaseTest before running this test.
    /// TODO: Implement folder navigation (save dialog, folder page, grid click).
    /// </summary>
    [Test]
    public async Task AjsIncludeRelatedFedFolderingTest()
    {
        const string SurveyQuestion = "What's the minimum wage?";
        const string AjsTestFolder  = "WlaAjsTestFolder";

        await PrepareTestFolder(AjsTestFolder);

        var surveysPage = await NavigateToLandingPage();
        await surveysPage.WlaQueryBox.EnterQuestion(SurveyQuestion);
        await surveysPage.WlaQueryBox.SelectIncludeCases();
        await surveysPage.Jurisdictions.SelectJurisdiction(JurisCA, JurisIncludeRelatedFed);

        // ORIGINAL: Thread.Sleep(1000) — removed. Playwright will auto-wait for button actionability.
        surveysPage = await surveysPage.ClickCreateSurveyAndWait();

        // Verify all four result section headings appear
        Assert.That(
            await surveysPage.WlaSurveyResult.StateStatutesRegulationsHeading(JurisCAName).IsVisibleAsync(),
            Is.True,
            "State Statutes and Regulations heading not displayed.");

        Assert.That(
            await surveysPage.WlaSurveyResult.FederalStatutesRegulationsHeading.IsVisibleAsync(),
            Is.True,
            "Federal Statutes and Regulations heading not displayed.");

        Assert.That(
            await surveysPage.WlaSurveyResult.CasesStateHeading.IsVisibleAsync(),
            Is.True,
            "Cases State heading not displayed.");

        Assert.That(
            await surveysPage.WlaSurveyResult.CasesFederalHeading.IsVisibleAsync(),
            Is.True,
            "Cases Federal heading not displayed.");

        // Save to folder
        // TODO: Implement full folder save interaction once locators are verified.
        // ORIGINAL:
        //   var saveToFolderDialog = surveysPage.Toolbar.SaveToFolderButton.Click<SaveToFolderDialog>();
        //   saveToFolderDialog.FolderTreeComponent.SelectFolderByName(AjsTestFolder);
        //   saveToFolderDialog.ClickSaveButton<AiJurisdictionalSurveysPage>();
        var dialog = await surveysPage.Toolbar.ClickSaveToFolder();
        // TODO: Select folder in tree, click Save — add full dialog interaction here

        // Navigate to folder, reopen survey, verify headings again
        // TODO: Implement folder navigation page object.
        // ORIGINAL:
        //   var recentFolderDialog = surveysPage.Header.ClickHeaderTab<EdgeRecentFoldersDialog>(EdgeHeaderTabs.Folders);
        //   var folderPage = recentFolderDialog.ClickFolderByName(AjsTestFolder).ClickViewThisFolderButton();
        //   surveysPage = folderPage.FolderGrid.ClickGridItemByName<AiJurisdictionalSurveysPage>(SurveyQuestion);
        //   SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed, 3000);

        // Placeholder assertion — replace with real folder navigation
        Assert.Inconclusive("Folder navigation not yet implemented. Implement PrepareTestFolder and folder dialog interactions.");
    }

    // ─────────────────────────────────────────────────────────────────────────
    // TEST 3: Copy Link
    // Requires: survey, copy link, sign out, sign back in, open link in new tab
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Verify jurisdiction filters and labels are preserved when opening a copied link.
    ///
    /// KEY WAIT IMPROVEMENTS:
    ///   SHIM: SafeMethodExecutor.WaitUntil(() => surveysPage.CopiedLinkSuccessLabel.Displayed, timeoutFromSec: 10)
    ///         → polls via FindElement with 2s cap
    ///   NATIVE: await WaitForCopiedLinkSuccess() → WaitForAsync(Visible) on the locator directly
    ///
    ///   SHIM: Clipboard.GetText() (Windows Forms) — does not work in headless/Linux CI
    ///   NATIVE: Page.EvaluateAsync("() => navigator.clipboard.readText()") — works in Playwright
    ///           because Playwright grants clipboard permissions in context options.
    ///
    /// TODO: Implement sign-out and sign-back-in helpers before running this test.
    /// TODO: Verify clipboard read works with the granted permissions in ContextOptions.
    /// </summary>
    [Test]
    public async Task AjsIncludeRelatedFedCopyLinkTest()
    {
        const string SurveyQuestion = "What are the implications for running a red light?";

        var surveysPage = await NavigateToLandingPage();
        await surveysPage.WlaQueryBox.EnterQuestion(SurveyQuestion);
        await surveysPage.WlaQueryBox.SelectIncludeCases();
        await surveysPage.Jurisdictions.SelectJurisdiction(JurisCA, JurisMN, JurisIncludeRelatedFed);
        surveysPage = await surveysPage.ClickCreateSurveyAndWait();

        // Capture original state
        var jurisFilterNamesOriginal = await surveysPage.ContentType.GetJurisdictionContentTypes();
        var jurisLabelsOriginal = await surveysPage.WlaSurveyResult.GetAllJurisdictionLabels();

        Assert.That(
            jurisFilterNamesOriginal.SequenceEqual(jurisLabelsOriginal),
            Is.True,
            "Jurisdiction filters names not match with jurisdiction labels on result.");

        var originalTitle   = await Page.TitleAsync();
        var originalHeading = await surveysPage.PageHeaderLabel.TextContentAsync() ?? string.Empty;

        // Copy the link
        await surveysPage.Toolbar.ClickCopyLink();
        await surveysPage.WaitForCopiedLinkSuccess();

        // ORIGINAL: Clipboard.GetText() — Windows Forms, doesn't work in CI/headless
        // NATIVE: navigator.clipboard.readText() — works because Playwright grants clipboard permissions
        var copiedLink = await Page.EvaluateAsync<string>("() => navigator.clipboard.readText()");
        Assert.That(copiedLink, Is.Not.Null.And.Not.Empty, "Copied link should not be empty.");

        // TODO: Sign out and sign back in
        // ORIGINAL:
        //   this.DefaultSignOnManager.SignOff();
        //   var homePage = this.SignOnBack().ClickContinueButton<AdvantageHomePage>();
        // NATIVE: Implement SignOut() and re-navigate in PlaywrightAjsBaseTest

        // Open copied link in a new tab
        // ORIGINAL: BrowserPool.CurrentBrowser.CreateTab(WlaTab) + ActivateTab
        // NATIVE: Context.NewPageAsync() — clean, no window handle juggling
        var newPage = await Context.NewPageAsync();
        await newPage.GotoAsync(copiedLink);
        await newPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        // Verify title and heading match original
        Assert.That(
            await newPage.TitleAsync(),
            Is.EqualTo(originalTitle),
            "Browser tab title is not correct after opening copied link.");

        // TODO: Create page object on newPage and verify jurisdiction filters match
        // Placeholder:
        Assert.Inconclusive("Sign-out/sign-back-in and new-tab page object not yet implemented.");
    }

    // ─────────────────────────────────────────────────────────────────────────
    // TEST 4: History
    // Requires: survey, navigate to history, reopen from history
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Verify survey appears in history and retains timestamp + jurisdiction data when reopened.
    ///
    /// KEY WAIT IMPROVEMENTS:
    ///   SHIM: SafeMethodExecutor.WaitUntil with 30s polling loop + BrowserPool.Refresh
    ///         to check if survey appears in history.
    ///   NATIVE: await Page.WaitForFunctionAsync() or poll with Playwright's WaitUntilAsync pattern.
    ///           No BrowserPool needed — use Page.ReloadAsync().
    ///
    /// TODO: Implement header tab navigation and history page object.
    /// </summary>
    [Test]
    public async Task AjsIncludeRelatedFedHistoryTest()
    {
        const string SurveyQuestion = "What are the implications for running a red light?";

        var surveysPage = await NavigateToLandingPage();
        await surveysPage.WlaQueryBox.EnterQuestion(SurveyQuestion);
        await surveysPage.WlaQueryBox.SelectIncludeCases();
        await surveysPage.Jurisdictions.SelectJurisdiction(JurisCA, JurisMN, JurisIncludeRelatedFed);
        surveysPage = await surveysPage.ClickCreateSurveyAndWait();

        // Capture original state before navigating to history
        var timestampOriginal    = await surveysPage.SurveyResult.GetTimestamp();
        var jurisFiltersOriginal = await surveysPage.ContentType.GetJurisdictionContentTypes();
        var jurisLabelsOriginal  = await surveysPage.WlaSurveyResult.GetAllJurisdictionLabels();

        // TODO: Navigate to history page and wait for entry to appear.
        // ORIGINAL (shim):
        //   var recentHistoryDialog = surveysPage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);
        //   var historyPage = recentHistoryDialog.ViewAllLink.Click<EdgeCommonHistoryPage>();
        //   SafeMethodExecutor.WaitUntil(
        //       () => {
        //           historyPage = BrowserPool.CurrentBrowser.Refresh<EdgeCommonHistoryPage>();
        //           return historyPage.HistoryTable.GetGridItems().First().IsTextLinkDisplayed(SurveyQuestion);
        //       }, timeoutFromSec: 30);
        //
        // NATIVE: Navigate to history URL, poll for the entry with WaitForFunctionAsync or retry loop.
        //   Example:
        //   await Page.GotoAsync($"{BaseUrl}/history");
        //   await Page.WaitForFunctionAsync(
        //       "question => document.body.innerText.includes(question)",
        //       SurveyQuestion,
        //       new PageWaitForFunctionOptions { Timeout = 30000, PollingInterval = 2000 }
        //   );

        // Placeholder
        Assert.Inconclusive("History page navigation not yet implemented.");
    }

    // ─────────────────────────────────────────────────────────────────────────
    // TEST 4: Delivery (Download PDF)
    // Requires: survey, open download dialog, download PDF, verify content
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Verify downloaded PDF contains correct cover page info including federal jurisdiction.
    ///
    /// KEY WAIT IMPROVEMENTS:
    ///   SHIM: FileUtil.WaitForFileDownload(FolderToSave, fileName) — polls filesystem
    ///   NATIVE: Page.RunAndWaitForDownloadAsync() — Playwright signals when download completes.
    ///           No filesystem polling. Works in headless and CI.
    ///
    ///   SHIM: download.default_directory ChromeOptions pref → Playwright download handler
    ///   NATIVE: context.RunAndWaitForDownloadAsync() gives you the download object directly.
    ///           Call download.SaveAsAsync(path) to save to a specific location.
    ///
    /// TODO: Implement full download dialog interactions (format selection, click download).
    /// </summary>
    [Test]
    public async Task AjsIncludeRelatedFedDeliveryTest()
    {
        const string SurveyQuestion     = "What is lemon law?";
        const string DeliveryDateFormat = "MM-dd-yyyy";

        var surveysPage = await NavigateToLandingPage();
        await surveysPage.WlaQueryBox.EnterQuestion(SurveyQuestion);
        await surveysPage.WlaQueryBox.SelectIncludeCases();
        await surveysPage.Jurisdictions.SelectJurisdiction(JurisCA, JurisIncludeRelatedFed);
        surveysPage = await surveysPage.ClickCreateSurveyAndWait();

        // Wait for timestamp to appear (confirms result is fully rendered)
        await surveysPage.SurveyResult.TimeStampLabel.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 10000
        });

        // Open download dialog
        // TODO: Implement full dialog — format selection (PDF), layout options (cover page)
        // ORIGINAL:
        //   var downloadDialog = surveysPage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
        //   downloadDialog.LayoutAndLimitsTab.SetIncludeSectionOption(LayoutAndLimitsInclude.CoverPage);
        //   downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);
        //   downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<AiJurisdictionalSurveysPage>();
        //
        // NATIVE — use RunAndWaitForDownloadAsync to capture the download object:
        //   var download = await Page.RunAndWaitForDownloadAsync(async () =>
        //   {
        //       await surveysPage.Toolbar.SelectDeliveryDownload();
        //       // ... set PDF format, cover page option ...
        //       await Page.Locator("button:has-text('Download')").Last.ClickAsync();
        //   });
        //   var tempPath = await download.PathAsync();
        //   var savedPath = Path.Combine(Path.GetTempPath(), download.SuggestedFilename);
        //   await download.SaveAsAsync(savedPath);

        // Placeholder — implement download dialog then uncomment verification below
        Assert.Inconclusive(
            "Download dialog interactions not yet implemented. " +
            "Implement toolbar delivery dropdown → PDF format → download button → " +
            "Page.RunAndWaitForDownloadAsync() → verify PDF text content.");

        // Once implemented, verify PDF content:
        // var expectedFileName = $"Westlaw Advantage - AI Jurisdictional Survey - {DateTime.Now.ToString(DeliveryDateFormat)}.pdf";
        // var text = PdfTextExtractor.ExtractTextFromPdf(savedPath);
        // Assert.That(text, Does.Contain("Westlaw AI Jurisdictional Surveys results"));
        // Assert.That(text, Does.Contain($"Question:  {SurveyQuestion}"));
        // Assert.That(text, Does.Contain("Content:  Statutes, regulations, and cases"));
        // Assert.That(text, Does.Contain("Jurisdiction:  Federal, California"));
        // Assert.That(text, Does.Contain($"Delivered:  {DateTime.Now:MMMM d, yyyy}"));
        // var textNoWhitespace = text.Replace(" ", "").Replace("\r\n", "");
        // Assert.That(textNoWhitespace, Does.Contain("FederalStatutesandregulations".Replace(" ", "")));
        // Assert.That(textNoWhitespace, Does.Contain("CaliforniaStatestatutesandregulations".Replace(" ", "")));
        // Assert.That(textNoWhitespace, Does.Contain("CasesState".Replace(" ", "")));
    }

    // ── Helper ────────────────────────────────────────────────────────────────

    /// <summary>
    /// Extension: wait for copy-link success label to appear.
    /// Called in AjsIncludeRelatedFedCopyLinkTest.
    /// </summary>
    private async Task WaitForCopiedLinkSuccess(Pages.AiJurisdictionalSurveysPagePw surveysPage)
    {
        await surveysPage.CopiedLinkSuccessLabel.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 10000
        });
    }
}
