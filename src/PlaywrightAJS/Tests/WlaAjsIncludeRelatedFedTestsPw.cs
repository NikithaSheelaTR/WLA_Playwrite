namespace WestlawAdvantage.Playwright.AJS.Tests;

using Microsoft.Playwright;
using NUnit.Framework;
using WestlawAdvantage.Playwright.AJS.Infrastructure;
using WestlawAdvantage.Playwright.AJS.Pages;

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

        // 1. Verify ALLFEDS disabled + 0 selected on fresh page
        // Shim: WaitForJurisdictionDisabled → IsTrue(IsDisabled && count.Contains("0 selected"))
        await surveysPage.Jurisdictions.WaitForJurisdictionDisabled(JurisIncludeRelatedFed);
        Assert.That(
            await surveysPage.Jurisdictions.IsJurisdictionSelectionDisabled(JurisIncludeRelatedFed)
            && (await surveysPage.Jurisdictions.GetSelectedCountText()).Contains("0 selected"),
            Is.True,
            "Include related federal is not disabled when no juris selected.");

        // 2. Select All → ALLFEDS becomes checked
        // Shim: SelectJurisdiction(SelectAll) [clears + selects] → WaitForJurisdictionSelected
        await surveysPage.Jurisdictions.SelectJurisdiction(JurisSelectAll);
        await surveysPage.Jurisdictions.WaitForJurisdictionSelected(JurisIncludeRelatedFed);
        Assert.That(
            await surveysPage.Jurisdictions.IsJurisdictionSelected(JurisIncludeRelatedFed),
            Is.True,
            "Include related federal is not selected clicking Select All.");

        // 3. Select CA only (clears all first) → ALLFEDS enabled but unchecked
        // Shim: SelectJurisdiction(JurisCA) [clears + selects CA] → WaitForJurisdictionEnabled
        await surveysPage.Jurisdictions.SelectJurisdiction(JurisCA);
        await surveysPage.Jurisdictions.WaitForJurisdictionEnabled(JurisIncludeRelatedFed);
        Assert.That(
            await surveysPage.Jurisdictions.IsJurisdictionSelectionDisabled(JurisIncludeRelatedFed),
            Is.False,
            "Include related federal is not enabled when a juris is selected.");

        // 4. Add ALLFEDS to existing selection → count 2 (CA + ALLFEDS)
        // Shim: SelectJurisdiction(false, ALLFEDS) [no clear, just check] → WaitForSelectedCountToContain
        await surveysPage.Jurisdictions.AddJurisdiction(JurisIncludeRelatedFed);
        await surveysPage.Jurisdictions.WaitForSelectedCountToContain("2 selected");
        Assert.That(
            (await surveysPage.Jurisdictions.GetSelectedCountText()).Contains("2 selected"),
            Is.True,
            "Selected count does not increase by 1 when Include related federal is selected.");

        // 5. Deselect CA → ALLFEDS auto-disabled + 0 selected
        // Shim: SelectJurisdiction(false, JurisCA) [toggle/uncheck CA] → WaitForJurisdictionDisabled + WaitForSelectedCountToContain
        await surveysPage.Jurisdictions.DeselectJurisdiction(JurisCA);
        await surveysPage.Jurisdictions.WaitForJurisdictionDisabled(JurisIncludeRelatedFed);
        await surveysPage.Jurisdictions.WaitForSelectedCountToContain("0 selected");
        Assert.That(
            await surveysPage.Jurisdictions.IsJurisdictionSelectionDisabled(JurisIncludeRelatedFed)
            && (await surveysPage.Jurisdictions.GetSelectedCountText()).Contains("0 selected"),
            Is.True,
            "Include related federal is not disabled when the only selected juris is unselected.");

        // 6. Select CA + MN + ALLFEDS, deselect MN → ALLFEDS stays selected
        // Shim: SelectJurisdiction(CA, MN, ALLFEDS) → SelectJurisdiction(false, MN) → WaitForJurisdictionSelected
        await surveysPage.Jurisdictions.SelectJurisdiction(JurisCA, JurisMN, JurisIncludeRelatedFed);
        await surveysPage.Jurisdictions.DeselectJurisdiction(JurisMN);
        await surveysPage.Jurisdictions.WaitForJurisdictionSelected(JurisIncludeRelatedFed);
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
    /// KEY WAIT IMPROVEMENTS vs SHIM:
    ///   Thread.Sleep(1000) → removed (Playwright auto-waits for button actionability)
    ///   SafeMethodExecutor.WaitUntil(!ProgressLabel.Displayed) → await WaitForSurveyComplete()
    /// </summary>
    [Test]
    public async Task AjsIncludeRelatedFedFolderingTest()
    {
        const string SurveyQuestion = "What's the minimum wage?";
        const string AjsTestFolder  = "WlaAjsTestFolder";

        var surveysPage = await NavigateToLandingPage();
        await surveysPage.WlaQueryBox.EnterQuestion(SurveyQuestion);
        await surveysPage.WlaQueryBox.SelectIncludeCases();
        await surveysPage.Jurisdictions.SelectJurisdiction(JurisCA, JurisIncludeRelatedFed);
        surveysPage = await surveysPage.ClickCreateSurveyAndWait();

        // Verify all four result section headings appear before saving
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

        // Open Save to Folder dialog — create the test folder inline if it doesn't exist yet.
        // No separate PrepareTestFolder step: folder is created on first run, reused on subsequent runs.
        //
        // SHIM locators (confirmed):
        //   Dialog root:   //div[@id='coid_lightboxOverlay' and not(contains(@class,'co_hideState'))]//div[contains(@class,'co_folderAction')]
        //   Folder item:   .//div[contains(@class,'co_tree_element') and contains(@class,'co_tree_position')]//*[text()='{name}']
        //   New Folder lnk: //a[@class='co_saveToNewFolder']
        //   Name input:    input#cobalt_ro_folder_action_textbox
        //   OK button:     button[contains(@class,'co_dropdownBox_ok')]
        //   Save button:   input[contains(@class,'co_saveToDoSave')]  ← input, not button
        var dialog = await surveysPage.Toolbar.ClickSaveToFolder();

        // Find folder by exact text — works regardless of the tree's HTML structure
        // (tried role='treeitem' and class-based XPath; neither matched the actual DOM)
        var folderItem = Page.GetByText(AjsTestFolder, new PageGetByTextOptions { Exact = true }).First;

        if (!await folderItem.IsVisibleAsync())
        {
            // First run — create the folder via the dialog's New Folder link
            // Shim: //a[@class='co_saveToNewFolder']
            var newFolderLink = Page.Locator("xpath=//a[@class='co_saveToNewFolder']");
            await newFolderLink.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
            await newFolderLink.ClickAsync();

            var nameInput = Page.Locator("#cobalt_ro_folder_action_textbox");
            await nameInput.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await nameInput.FillAsync(AjsTestFolder);
            await Page.Locator("button[class*='co_dropdownBox_ok']").First.ClickAsync();

            // Wait for folder to appear after creation
            await folderItem.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
        }

        await folderItem.ClickAsync();
        // Save button is an <input> element, not a <button> — confirmed from shim
        await Page.Locator("xpath=//input[contains(@class,'co_saveToDoSave')]").ClickAsync();
        await dialog.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Hidden,
            Timeout = 10000
        });

        // Navigate to folder via header Folders tab
        // SHIM: surveysPage.Header.ClickHeaderTab<EdgeRecentFoldersDialog>(EdgeHeaderTabs.Folders)
        var foldersPanel = await ClickHeaderFoldersTab();
        var folderLink = foldersPanel.Locator(
            $"a:has-text('{AjsTestFolder}'), button:has-text('{AjsTestFolder}')").First;
        await folderLink.ClickAsync();

        // Click "View this folder" button
        var viewFolderBtn = Page.Locator(
            "button:has-text('View this folder'), a:has-text('View this folder')").First;
        await viewFolderBtn.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 10000
        });
        await viewFolderBtn.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        // Click the survey entry in the folder grid
        // SHIM: folderPage.FolderGrid.ClickGridItemByName<AiJurisdictionalSurveysPage>(SurveyQuestion)
        var surveyItem = Page.Locator(
            $"[role='grid'] a:has-text('{SurveyQuestion}'), " +
            $"[class*='folderGrid'] a:has-text('{SurveyQuestion}')").First;
        await surveyItem.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 15000
        });
        await surveyItem.ClickAsync();

        // Wait for survey to load from folder
        surveysPage = new AiJurisdictionalSurveysPagePw(Page);
        await surveysPage.WaitForSurveyComplete(timeoutMs: 30000);

        // Re-verify all four headings after reopening from folder
        Assert.That(
            await surveysPage.WlaSurveyResult.StateStatutesRegulationsHeading(JurisCAName).IsVisibleAsync(),
            Is.True,
            "State Statutes heading not displayed after reopening from folder.");
        Assert.That(
            await surveysPage.WlaSurveyResult.FederalStatutesRegulationsHeading.IsVisibleAsync(),
            Is.True,
            "Federal Statutes heading not displayed after reopening from folder.");
        Assert.That(
            await surveysPage.WlaSurveyResult.CasesStateHeading.IsVisibleAsync(),
            Is.True,
            "Cases State heading not displayed after reopening from folder.");
        Assert.That(
            await surveysPage.WlaSurveyResult.CasesFederalHeading.IsVisibleAsync(),
            Is.True,
            "Cases Federal heading not displayed after reopening from folder.");
    }

    // ─────────────────────────────────────────────────────────────────────────
    // TEST 3: Copy Link
    // Requires: survey, copy link, sign out, sign back in, open link in new tab
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Verify jurisdiction filters and labels are preserved when opening a copied link.
    ///
    /// KEY WAIT IMPROVEMENTS vs SHIM:
    ///   WaitUntil(CopiedLinkSuccessLabel.Displayed) → await WaitForCopiedLinkSuccess()
    ///   Clipboard.GetText() (Windows Forms, CI-broken) → navigator.clipboard.readText()
    ///   BrowserPool.CreateTab/ActivateTab → Context.NewPageAsync()
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
        var jurisLabelsOriginal      = await surveysPage.WlaSurveyResult.GetAllJurisdictionLabels();

        Assert.That(
            jurisFilterNamesOriginal.SequenceEqual(jurisLabelsOriginal),
            Is.True,
            "Jurisdiction filter names do not match jurisdiction labels on result.");

        var originalTitle   = await Page.TitleAsync();
        var originalHeading = await surveysPage.PageHeaderLabel.TextContentAsync() ?? string.Empty;

        // Copy the link
        // NATIVE: navigator.clipboard.readText() — works because Playwright grants clipboard
        //         permissions; Clipboard.GetText() (Windows Forms) breaks in headless/Linux CI.
        await surveysPage.Toolbar.ClickCopyLink();
        await surveysPage.WaitForCopiedLinkSuccess();

        var copiedLink = await Page.EvaluateAsync<string>("() => navigator.clipboard.readText()");
        Assert.That(copiedLink, Is.Not.Null.And.Not.Empty, "Copied link should not be empty.");

        // Sign out and sign back in
        // SHIM: DefaultSignOnManager.SignOff() + SignOnBack().ClickContinueButton()
        await SignOut();
        await SignBackIn();

        // Open the copied link in a new tab — new tab inherits the authenticated context
        // SHIM: BrowserPool.CurrentBrowser.CreateTab(WlaTab) + ActivateTab
        var newPage = await Context.NewPageAsync();
        await newPage.GotoAsync(copiedLink);
        await newPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        // Verify browser title matches original
        Assert.That(
            await newPage.TitleAsync(),
            Is.EqualTo(originalTitle),
            "Browser tab title is not correct after opening copied link.");

        // Wait for the survey to render on the new tab
        var newTabPage = new AiJurisdictionalSurveysPagePw(newPage);
        await newTabPage.WaitForSurveyComplete(timeoutMs: 30000);

        // Verify page heading matches
        var newTabHeading = await newTabPage.PageHeaderLabel.TextContentAsync() ?? string.Empty;
        Assert.That(
            newTabHeading.Trim(),
            Is.EqualTo(originalHeading.Trim()),
            "Page heading is not correct after opening copied link.");

        // Verify jurisdiction filter names match original
        var jurisFiltersNewTab = await newTabPage.ContentType.GetJurisdictionContentTypes();
        Assert.That(
            jurisFiltersNewTab.SequenceEqual(jurisFilterNamesOriginal),
            Is.True,
            "Jurisdiction filter names not preserved when opening copied link.");

        // Verify jurisdiction result labels match original
        var jurisLabelsNewTab = await newTabPage.WlaSurveyResult.GetAllJurisdictionLabels();
        Assert.That(
            jurisLabelsNewTab.SequenceEqual(jurisLabelsOriginal),
            Is.True,
            "Jurisdiction labels not preserved when opening copied link.");
    }

    // ─────────────────────────────────────────────────────────────────────────
    // TEST 4: History
    // Requires: survey, navigate to history, reopen from history
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Verify survey appears in history and retains timestamp + jurisdiction data when reopened.
    ///
    /// KEY WAIT IMPROVEMENTS vs SHIM:
    ///   WaitUntil(BrowserPool.Refresh + historyTable check, 30s) →
    ///     WaitForFunctionAsync(body contains question, 30s) — no page refresh loop needed.
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

        // Navigate to History via header tab, then click "View all"
        // SHIM: Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History)
        //       → recentHistoryDialog.ViewAllLink.Click<EdgeCommonHistoryPage>()
        var historyPanel = await ClickHeaderHistoryTab();
        var viewAllLink  = historyPanel.Locator("a:has-text('View all'), a:has-text('View All')").First;
        await viewAllLink.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        // Poll until the survey entry appears (up to 30 s — history indexing may lag)
        // SHIM: WaitUntil(() => BrowserPool.Refresh + historyTable.First().IsTextLinkDisplayed(...))
        await Page.WaitForFunctionAsync(
            "question => document.body.innerText.includes(question)",
            SurveyQuestion,
            new PageWaitForFunctionOptions { Timeout = 30000, PollingInterval = 2000 });

        // Click the survey entry in the history table
        var historyEntry = Page.Locator(
            $"[role='grid'] a:has-text('{SurveyQuestion}'), " +
            $"[class*='historyGrid'] a:has-text('{SurveyQuestion}'), " +
            $"table a:has-text('{SurveyQuestion}')").First;
        await historyEntry.ClickAsync();

        // Wait for the reopened survey to fully load
        surveysPage = new AiJurisdictionalSurveysPagePw(Page);
        await surveysPage.WaitForSurveyComplete(timeoutMs: 30000);

        // Verify timestamp is preserved
        var timestampReopened = await surveysPage.SurveyResult.GetTimestamp();
        Assert.That(
            timestampReopened,
            Is.EqualTo(timestampOriginal),
            "Survey timestamp not preserved when reopening from history.");

        // Verify jurisdiction filter names match
        var jurisFiltersReopened = await surveysPage.ContentType.GetJurisdictionContentTypes();
        Assert.That(
            jurisFiltersReopened.SequenceEqual(jurisFiltersOriginal),
            Is.True,
            "Jurisdiction filter names not preserved when reopening from history.");

        // Verify jurisdiction result labels match
        var jurisLabelsReopened = await surveysPage.WlaSurveyResult.GetAllJurisdictionLabels();
        Assert.That(
            jurisLabelsReopened.SequenceEqual(jurisLabelsOriginal),
            Is.True,
            "Jurisdiction labels not preserved when reopening from history.");
    }

    // ─────────────────────────────────────────────────────────────────────────
    // TEST 5: Delivery (Download PDF)
    // Requires: survey, open download dialog, download PDF, verify content
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Verify downloaded PDF contains correct cover page info including federal jurisdiction.
    ///
    /// KEY WAIT IMPROVEMENTS vs SHIM:
    ///   FileUtil.WaitForFileDownload(folder, fileName) (filesystem poll) →
    ///     Page.RunAndWaitForDownloadAsync() — Playwright signals completion directly.
    ///   ChromeOptions download.default_directory pref → download.SaveAsAsync(path)
    /// </summary>
    [Test]
    public async Task AjsIncludeRelatedFedDeliveryTest()
    {
        const string SurveyQuestion = "What is lemon law?";

        var surveysPage = await NavigateToLandingPage();
        await surveysPage.WlaQueryBox.EnterQuestion(SurveyQuestion);
        await surveysPage.WlaQueryBox.SelectIncludeCases();
        await surveysPage.Jurisdictions.SelectJurisdiction(JurisCA, JurisIncludeRelatedFed);
        surveysPage = await surveysPage.ClickCreateSurveyAndWait();

        // Wait for timestamp to appear (result is fully rendered)
        await surveysPage.SurveyResult.TimeStampLabel.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 10000
        });

        // Open delivery dropdown → Download dialog, configure PDF + cover page, capture download
        // SHIM: DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download)
        //       → LayoutAndLimitsTab.SetIncludeSectionOption(CoverPage)
        //       → TheBasicsTab.FormatDropdown.SelectOption(Pdf)
        //       → ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton()
        var download = await Page.RunAndWaitForDownloadAsync(async () =>
        {
            var dialog = await surveysPage.Toolbar.SelectDeliveryDownload();

            // Set format to PDF
            var formatSelect = dialog.Locator(
                "select[aria-label*='Format' i], " +
                "[data-automation='format-select'], " +
                "select[id*='format' i], " +
                "select[name*='format' i]").First;
            if (await formatSelect.CountAsync() > 0)
                await formatSelect.SelectOptionAsync(new SelectOptionValue { Label = "PDF" });

            // Enable cover page option
            var coverPageCheckbox = dialog.Locator(
                "[data-automation='cover-page'] input, " +
                "input[aria-label*='cover page' i], " +
                "input[id*='coverPage' i]").First;
            if (await coverPageCheckbox.CountAsync() > 0 && !await coverPageCheckbox.IsCheckedAsync())
                await coverPageCheckbox.CheckAsync();

            // Click the final Download button
            // SHIM: ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton()
            await dialog.Locator("button:has-text('Download')").Last.ClickAsync();

            // Handle two-step "Ready for Delivery" dialog if shown
            var readyDialog = Page.Locator(
                "[role='dialog']:has-text('Ready for Delivery'), " +
                "[data-automation='ready-for-delivery-dialog']");
            if (await readyDialog.IsVisibleAsync())
                await readyDialog.Locator("button:has-text('Download')").ClickAsync();
        });

        var savedPath = Path.Combine(Path.GetTempPath(), download.SuggestedFilename);
        await download.SaveAsAsync(savedPath);

        Assert.That(download.SuggestedFilename, Does.EndWith(".pdf"),
            "Downloaded file is not a PDF.");

        // Extract and verify PDF text content
        // SHIM: PdfTextExtractor.ExtractTextFromPdf(savedPath) from Framework.Common.UI
        // NATIVE: UglyToad.PdfPig reads the PDF without any native dependencies
        var pdfText = ExtractPdfText(savedPath);

        Assert.That(pdfText, Does.Contain("Westlaw AI Jurisdictional Surveys results"),
            "PDF does not contain expected header text.");
        Assert.That(pdfText, Does.Contain($"Question:  {SurveyQuestion}"),
            "PDF does not contain the survey question.");
        Assert.That(pdfText, Does.Contain("Content:  Statutes, regulations, and cases"),
            "PDF does not contain expected content type.");
        Assert.That(pdfText, Does.Contain("Jurisdiction:  Federal, California"),
            "PDF does not contain expected jurisdiction.");
        Assert.That(pdfText, Does.Contain($"Delivered:  {DateTime.Now:MMMM d, yyyy}"),
            "PDF does not contain expected delivery date.");

        var textNoWhitespace = pdfText.Replace(" ", "").Replace("\r\n", "").Replace("\n", "");
        Assert.That(textNoWhitespace, Does.Contain("FederalStatutesandregulations"),
            "PDF missing Federal Statutes and Regulations heading.");
        Assert.That(textNoWhitespace, Does.Contain("CaliforniaStatestatutesandregulations"),
            "PDF missing California State Statutes and Regulations heading.");
        Assert.That(textNoWhitespace, Does.Contain("CasesState"),
            "PDF missing Cases State heading.");
    }

    // ── PDF helper ────────────────────────────────────────────────────────────

    /// <summary>
    /// Extracts all text from a PDF file using PdfPig (pure .NET, no native deps).
    /// Replaces: PdfTextExtractor.ExtractTextFromPdf(path) from Framework.Common.UI
    /// </summary>
    private static string ExtractPdfText(string pdfPath)
    {
        using var pdf = UglyToad.PdfPig.PdfDocument.Open(pdfPath);
        var sb = new System.Text.StringBuilder();
        foreach (var page in pdf.GetPages())
            sb.Append(string.Join(" ", page.GetWords().Select(w => w.Text))).Append(' ');
        return sb.ToString();
    }
}
