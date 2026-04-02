namespace WestlawAdvantage.Tests.AJSForWLAdvantage
{
    using Framework.Common.UI.Products.Shared.Dialogs.Delivery;
    using Framework.Common.UI.Products.Shared.Dialogs.Foldering;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.WestlawAdvantage.Pages;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
    using Framework.Common.UI.Products.WestLawNext.Enums.Delivery;
    using Framework.Common.UI.Raw.EnhancementTests.Utils;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Header;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.IO;
    using OpenQA.Selenium;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// Test 'Include related federal' behaviors on juris selector, foldering, history, copy link, delivery etc.
    /// </summary>
    [TestClass]
    public class WlaAjsIncludeRelatedFedTests : WlaAjsBaseTest
    {
        private const string FeatureTestCategoryWlaAjsIncludeRelatedFed = "IncludeRelatedFederal";

        /// <summary>
        /// Test Include related federal option behavior when selecting jurisdictions (Story 2270815).
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(FeatureTestCategoryWlaAjsIncludeRelatedFed)]
        [TestProperty(EnvironmentConstants.InfrastructureAccessControlsOn, "IAC-AIJS-INCLUDE-RELATED-FEDERAL,IAC-AI-50SS-PROFILE13")]
        public void AjsIncludeRelatedFedJurisSelectorTest()
        {
            const string JurisCA = "CA-CS", JurisMN = "MN-CS", JurisSelectAll = "Select All", JurisIncludeRelatedFed = "ALLFEDS";

            var surveysPage = NavigateToLandingPage();

            surveysPage.Jurisdictions.WaitForJurisdictionDisabled(JurisIncludeRelatedFed);
            this.TestCaseVerify.IsTrue(
                "Verify Include related federal is disabled when no juris is selected",
                surveysPage.Jurisdictions.IsJurisdictionSelectionDisabled(JurisIncludeRelatedFed) &&
                surveysPage.Jurisdictions.SelectedCountLabel.Text.Contains("0 selected"),
            "Include related federal is not disabled when no juris selected.");

            surveysPage.Jurisdictions.SelectJurisdiction(JurisSelectAll);

            surveysPage.Jurisdictions.WaitForJurisdictionSelected(JurisIncludeRelatedFed);
            this.TestCaseVerify.IsTrue(
                "Verify Include related federal is selected clicking Select All",
                surveysPage.Jurisdictions.IsJurisdictionSelected(JurisIncludeRelatedFed),
            "Include related federal is not selected clicking Select All.");

            surveysPage.Jurisdictions.SelectJurisdiction(JurisCA);

            surveysPage.Jurisdictions.WaitForJurisdictionEnabled(JurisIncludeRelatedFed);
            this.TestCaseVerify.IsFalse(
                "Verify Include related federal is enabled when a juris is selected",
                surveysPage.Jurisdictions.IsJurisdictionSelectionDisabled(JurisIncludeRelatedFed),
            "Include related federal is not enabled when a juris is selected.");

            surveysPage.Jurisdictions.SelectJurisdiction(false, JurisIncludeRelatedFed);

            surveysPage.Jurisdictions.WaitForSelectedCountToContain("2 selected");
            this.TestCaseVerify.IsTrue(
                "Verify selected count increases by 1 when Include related federal is selected",
                surveysPage.Jurisdictions.SelectedCountLabel.Text.Contains("2 selected"),
            "Selected count does not increase by 1 when Include related federal is selected.");

            surveysPage.Jurisdictions.SelectJurisdiction(false, JurisCA);

            surveysPage.Jurisdictions.WaitForJurisdictionDisabled(JurisIncludeRelatedFed);
            surveysPage.Jurisdictions.WaitForSelectedCountToContain("0 selected");
            this.TestCaseVerify.IsTrue(
                "Verify Include related federal is disabled when the only selected juris is unselected",
                surveysPage.Jurisdictions.IsJurisdictionSelectionDisabled(JurisIncludeRelatedFed) &&
                surveysPage.Jurisdictions.SelectedCountLabel.Text.Contains("0 selected"),
            "Include related federal is not disabled when the only selected juris is unselected.");

            surveysPage.Jurisdictions.SelectJurisdiction(JurisCA, JurisMN, JurisIncludeRelatedFed);
            surveysPage.Jurisdictions.SelectJurisdiction(false, JurisMN);

            surveysPage.Jurisdictions.WaitForJurisdictionSelected(JurisIncludeRelatedFed);
            this.TestCaseVerify.IsTrue(
                "Verify Include related federal is selected when unselecting one of many selected juris",
                surveysPage.Jurisdictions.IsJurisdictionSelected(JurisIncludeRelatedFed),
            "Include related federal is not selected when unselecting one of many selected juris.");
        }

        /// <summary>
        /// Test Include related federal option behavior when viewing result from Folder (Task 2282029).
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(FeatureTestCategoryWlaAjsIncludeRelatedFed)]
        [TestProperty(EnvironmentConstants.InfrastructureAccessControlsOn, "IAC-AIJS-INCLUDE-RELATED-FEDERAL,IAC-AI-50SS-PROFILE13")]
        public void AjsIncludeRelatedFedFolderingTest()
        {
            const string SurveyQuestion = "What's the minimum wage?";
            const string JurisCA = "CA-CS", JurisCAName = "California", JurisIncludeRelatedFed = "ALLFEDS";

            PrepareTestFolder();

            var surveysPage = NavigateToLandingPage();
            surveysPage.WlaQueryBox.EnterQuestion(SurveyQuestion);
            surveysPage.WlaQueryBox.SelectIncludeCases();
            surveysPage.Jurisdictions.SelectJurisdiction(JurisCA, JurisIncludeRelatedFed);
            SafeMethodExecutor.WaitUntil(() => surveysPage.CreateSurveyButtonTop.Displayed, timeoutFromSec: 5);
            surveysPage = surveysPage.CreateSurveyButtonTop.Click<AiJurisdictionalSurveysPage>();
            SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed);

            WaitForSurveyResultsLoaded(surveysPage);
            WaitForAllHeadingsDisplayed(surveysPage, JurisCAName);

            SafeMethodExecutor.WaitUntil(() =>
            {
                try
                {
                    return surveysPage.WlaSurveyResult.StateStatutesRegulationsHeading(JurisCAName).Displayed
                        && surveysPage.WlaSurveyResult.FederalStatutesRegulationsHeading.Displayed
                        && surveysPage.WlaSurveyResult.CasesStateHeading.Displayed
                        && surveysPage.WlaSurveyResult.CasesFederalHeading.Displayed;
                }
                catch (Exception)
                { 
                    return false;
                }
            },
           timeoutFromSec: 60);

            

            this.TestCaseVerify.IsTrue(
                "Verify State Statutes and Regulations heading displayed",
                surveysPage.WlaSurveyResult.StateStatutesRegulationsHeading(JurisCAName).Displayed,
            "State Statutes and Regulations heading not displayed.");

            this.TestCaseVerify.IsTrue(
                "Verify Federal Statutes and Regulations heading displayed",
                surveysPage.WlaSurveyResult.FederalStatutesRegulationsHeading.Displayed,
            "Federal Statutes and Regulations heading not displayed.");

            this.TestCaseVerify.IsTrue(
                "Verify Cases State heading displayed",
                surveysPage.WlaSurveyResult.CasesStateHeading.Displayed,
            "Cases State heading not displayed.");

            this.TestCaseVerify.IsTrue(
                "Verify Cases Federal heading displayed",
                surveysPage.WlaSurveyResult.CasesFederalHeading.Displayed,
            "Cases Federal heading not displayed.");

            var saveToFolderDialog = surveysPage.Toolbar.SaveToFolderButton.Click<SaveToFolderDialog>();
            SafeMethodExecutor.WaitUntil(() =>
            {
                try
                {
                    return saveToFolderDialog.FolderTreeComponent.IsFolderExist(AjsTestFolder);
                }
                catch (WebDriverException)
                {
                    return false;
                }
            }, timeoutFromSec: 60);
            saveToFolderDialog.FolderTreeComponent.SelectFolderByName(AjsTestFolder);
            saveToFolderDialog.ClickSaveButton<AiJurisdictionalSurveysPage>();

            var recentFolderDialog = surveysPage.Header.ClickHeaderTab<EdgeRecentFoldersDialog>(EdgeHeaderTabs.Folders);
            var folderPage = recentFolderDialog.ClickFolderByName(AjsTestFolder).ClickViewThisFolderButton();
            surveysPage = folderPage.FolderGrid.ClickGridItemByName<AiJurisdictionalSurveysPage>(SurveyQuestion);
            SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed, timeoutFromSec: 30);

            WaitForSurveyResultsLoaded(surveysPage);
            WaitForAllHeadingsDisplayed(surveysPage, JurisCAName);

            this.TestCaseVerify.IsTrue(
                "Verify headings displayed when viewing survey from folder",
                surveysPage.WlaSurveyResult.StateStatutesRegulationsHeading(JurisCAName).Displayed &&
                surveysPage.WlaSurveyResult.FederalStatutesRegulationsHeading.Displayed &&
                surveysPage.WlaSurveyResult.CasesStateHeading.Displayed &&
                surveysPage.WlaSurveyResult.CasesFederalHeading.Displayed,
            "Headings not displayed when viewing survey from folder.");
        }

        /// <summary>
        /// Test Include related federal option behavior when using copylink (Task 2282023).
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(FeatureTestCategoryWlaAjsIncludeRelatedFed)]
        [TestProperty(EnvironmentConstants.InfrastructureAccessControlsOn, "IAC-AIJS-INCLUDE-RELATED-FEDERAL,IAC-AI-50SS-PROFILE13")]
        public void AjsIncludeRelatedFedCopyLinkTest()
        {
            const string SurveyQuestion = "What are the implications for running a red light?";
            const string JurisCA = "CA-CS", JurisMN = "MN-CS", JurisIncludeRelatedFed = "ALLFEDS";
            const string JurisCAName = "California", JurisMNName = "Minnesota";
            const string CopyLinkTab = "CopyLink page";

            var surveysPage = NavigateToLandingPage();
            surveysPage.WlaQueryBox.EnterQuestion(SurveyQuestion);
            surveysPage.WlaQueryBox.SelectIncludeCases();
            surveysPage.Jurisdictions.SelectJurisdiction(JurisCA, JurisMN, JurisIncludeRelatedFed);
            SafeMethodExecutor.WaitUntil(() => surveysPage.CreateSurveyButtonTop.Displayed, timeoutFromSec: 5);
            surveysPage = surveysPage.CreateSurveyButtonTop.Click<AiJurisdictionalSurveysPage>();
            SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed);

            // Wait for results to fully load BEFORE reading filter names
            WaitForSurveyResultsLoaded(surveysPage);

            // Wait for all jurisdiction headings to be rendered before reading labels.
            // Federal section renders first; state sections appear asynchronously after.
            // WaitForAllHeadingsDisplayed ensures each state's h4 heading is in the DOM.
            WaitForAllHeadingsDisplayed(surveysPage, JurisCAName);
            WaitForAllHeadingsDisplayed(surveysPage, JurisMNName);

            var jurisFilterNamesOriginal = surveysPage.ContentType.JurisdictionContentTypes
                .Select(NormalizeWhitespace).ToList();
            var labels = surveysPage.WlaSurveyResult.GetAllJurisdictionLabels();
            var jurisLabelsOnResultOriginal = labels.Select(NormalizeWhitespace).Where(text => !string.IsNullOrWhiteSpace(text)).ToList();

            this.TestCaseVerify.IsTrue(
               "Verify jurisdiction filters names match with jurisdiction labels on result",
                jurisFilterNamesOriginal.OrderBy(x => x).SequenceEqual(jurisLabelsOnResultOriginal.OrderBy(x => x)),
               $"Jurisdiction filters names not match with jurisdiction labels on result. Filters: [{string.Join(", ", jurisFilterNamesOriginal)}] Labels: [{string.Join(", ", jurisLabelsOnResultOriginal)}]");

            var originalPageHeading = surveysPage.PageHeaderLabel.Text;

            surveysPage.Toolbar.ResearchCopyLinkButton.Click();
            SafeMethodExecutor.WaitUntil(() => surveysPage.CopiedLinkSuccessLabel.Displayed, timeoutFromSec: 10);
            string copiedLink = null;
            SafeMethodExecutor.WaitUntil(() =>
            {
                // Primary: read from the shim interceptor's stored value
                var shimResult = DriverExtensions.ExecuteScript(
                    "return window.__shimClipboardData || ''");
                var shimText = shimResult?.ToString();
                if (!string.IsNullOrEmpty(shimText) && shimText.StartsWith("http"))
                {
                    copiedLink = shimText;
                    return true;
                }
                // Fallback: read directly from the Clipboard API (requires clipboard-read permission)
                try
                {
                    var clipResult = DriverExtensions.ExecuteScript(
                        "return navigator.clipboard.readText()");
                    var clipText = clipResult?.ToString();
                    if (!string.IsNullOrEmpty(clipText) && clipText.StartsWith("http"))
                    {
                        copiedLink = clipText;
                        return true;
                    }
                }
                catch (Exception)
                {
                    // Clipboard API unavailable or denied — continue polling
                }
                return false;
            }, timeoutFromSec: 10);

            if (string.IsNullOrEmpty(copiedLink) || !copiedLink.StartsWith("http"))
            {
                this.TestCaseVerify.IsTrue(
                    "Verify copied link was retrieved from clipboard",
                    false,
                    $"Failed to retrieve copied link from clipboard. Value: '{copiedLink}'");
                return;
            }

            // Close the surveys tab and switch to home page before signing off
            BrowserPool.CurrentBrowser.CloseTab(JurisdictionalSurveysTab);
            BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);
            SafeMethodExecutor.WaitUntil(() => BrowserPool.CurrentBrowser.Title != null, timeoutFromSec: 5);

            this.DefaultSignOnManager.SignOff();

            // Wait for the sign-off page to fully load before re-signing on.
            // This ensures the session is fully terminated and the routing page
            // will be ready for interaction on the next sign-on attempt.
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
            SafeMethodExecutor.WaitUntil(() => BrowserPool.CurrentBrowser.Title != null, timeoutFromSec: 10);

            // Retry sign-on to handle transient routing page timeouts.
            // The routing page Save button has a 5s Playwright click timeout
            // which can be exceeded after a sign-off/sign-on cycle.
            AdvantageHomePage homePage = null;
            SafeMethodExecutor.WaitUntil(() =>
            {
                try
                {
                    homePage = this.SignOnBack().ClickContinueButton<AdvantageHomePage>();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }, timeoutFromSec: 60);

            SafeMethodExecutor.WaitUntil(() => BrowserPool.CurrentBrowser.Title != null, timeoutFromSec: 10);

            // Navigate to the copied link in a new tab using the framework's tab management
            DriverExtensions.ExecuteScript("window.open(arguments[0]);", copiedLink);

            SafeMethodExecutor.WaitUntil(() =>
                BrowserPool.CurrentBrowser.TabHandles.Count > 1, timeoutFromSec: 10);

            BrowserPool.CurrentBrowser.CreateTab(CopyLinkTab);
            BrowserPool.CurrentBrowser.ActivateTab(CopyLinkTab);

            SafeMethodExecutor.WaitUntil(() => BrowserPool.CurrentBrowser.Title != null, timeoutFromSec: 10);

            var surveysPageCopiedLinkPage = new AiJurisdictionalSurveysPage();
            SafeMethodExecutor.WaitUntil(() => !surveysPageCopiedLinkPage.ProgressLabel.Displayed, timeoutFromSec: 120);

            WaitForSurveyResultsLoaded(surveysPageCopiedLinkPage);

            this.TestCaseVerify.IsTrue(
               "Verify page heading is correct on copy link page",
               surveysPageCopiedLinkPage.PageHeaderLabel.Text.Equals(originalPageHeading),
               "Heading is not correct after opening copied link");

            // Wait for all jurisdiction headings on the copy link page before reading labels
            WaitForAllHeadingsDisplayed(surveysPageCopiedLinkPage, JurisCAName);
            WaitForAllHeadingsDisplayed(surveysPageCopiedLinkPage, JurisMNName);

            var jurisFilterNamesCopyLink = surveysPageCopiedLinkPage.ContentType.JurisdictionContentTypes
                .Select(NormalizeWhitespace).ToList();
            var copyLinkLabels = surveysPageCopiedLinkPage.WlaSurveyResult.GetAllJurisdictionLabels();
            var jurisLabelsOnResultCopyLink = copyLinkLabels
                .Select(NormalizeWhitespace)
                .Where(text => !string.IsNullOrWhiteSpace(text)).ToList();

            this.TestCaseVerify.IsTrue(
               "Verify jurisdiction label names on copy link page are same as those on original result",
                jurisLabelsOnResultCopyLink.SequenceEqual(jurisLabelsOnResultOriginal),
               "Jurisdiction label names on copy link page are not same as those on original result.");

            this.TestCaseVerify.IsTrue(
               "Verify jurisdiction filters names on copy link page are same as those on original result",
                jurisFilterNamesCopyLink.SequenceEqual(jurisFilterNamesOriginal),
               "Jurisdiction filters names on copy link page are not same as those on original result.");
        }

        /// <summary>
        /// Test Include related federal option behavior when viewing result from History (Task 2282027).
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(FeatureTestCategoryWlaAjsIncludeRelatedFed)]
        [TestCategory("PlaywrightEC2Test")]
        [TestProperty(EnvironmentConstants.InfrastructureAccessControlsOn, "IAC-AIJS-INCLUDE-RELATED-FEDERAL,IAC-AI-50SS-PROFILE13")]
        public void AjsIncludeRelatedFedHistoryTest()
        {
            const string SurveyQuestion = "What are the implications for running a red light?";
            const string JurisCA = "CA-CS", JurisMN = "MN-CS", JurisIncludeRelatedFed = "ALLFEDS";

            var surveysPage = NavigateToLandingPage();
            surveysPage.WlaQueryBox.EnterQuestion(SurveyQuestion);
            surveysPage.WlaQueryBox.SelectIncludeCases();
            surveysPage.Jurisdictions.SelectJurisdiction(JurisCA, JurisMN, JurisIncludeRelatedFed);
            SafeMethodExecutor.WaitUntil(() => surveysPage.CreateSurveyButtonTop.Displayed, timeoutFromSec: 5);
            surveysPage = surveysPage.CreateSurveyButtonTop.Click<AiJurisdictionalSurveysPage>();
            SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed);

            WaitForSurveyResultsLoaded(surveysPage);

            SafeMethodExecutor.WaitUntil(
                () => !string.IsNullOrWhiteSpace(surveysPage.SurveyResult.TimeStampLabel.Text),
                timeoutFromSec: 10);

            var timeStampLabelOriginal = NormalizeWhitespace(surveysPage.SurveyResult.TimeStampLabel.Text);

            SafeMethodExecutor.WaitUntil(
                () => surveysPage.ContentType.JurisdictionContentTypes.Any(),
                timeoutFromSec: 10);

            var jurisFilterNamesOriginal = surveysPage.ContentType.JurisdictionContentTypes
                .Select(NormalizeWhitespace).ToList();
            var labels = surveysPage.WlaSurveyResult.GetAllJurisdictionLabels();
            var jurisLabelsOnResultOriginal = labels
                .Select(NormalizeWhitespace)
                .Where(text => !string.IsNullOrWhiteSpace(text)).ToList();

            // Wait for survey to appear in history
            var recentHistoryDialog = surveysPage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);
            var historyPage = recentHistoryDialog.ViewAllLink.Click<EdgeCommonHistoryPage>();
            SafeMethodExecutor.WaitUntil(
                () =>
                {
                    historyPage = BrowserPool.CurrentBrowser.Refresh<EdgeCommonHistoryPage>();
                    var items = historyPage.HistoryTable.GetGridItems();
                    return items.Any() && items.First().IsTextLinkDisplayed(SurveyQuestion);
                },
                timeoutFromSec: 30);

            var firstHistoryItem = historyPage.HistoryTable.GetGridItems().First();

            this.TestCaseVerify.IsTrue(
                "Verify Jurisdictional Surveys entry displayed in history",
                firstHistoryItem.Title.Equals(SurveyQuestion),
                "Jurisdictional Surveys entry not displayed in History");

            surveysPage = firstHistoryItem.ClickLinkByText<AiJurisdictionalSurveysPage>(SurveyQuestion);
            SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed);

            WaitForSurveyResultsLoaded(surveysPage);

            SafeMethodExecutor.WaitUntil(
                () => !string.IsNullOrWhiteSpace(surveysPage.SurveyResult.TimeStampLabel.Text),
                timeoutFromSec: 10);

            SafeMethodExecutor.WaitUntil(
                () => surveysPage.ContentType.JurisdictionContentTypes.Any(),
                timeoutFromSec: 10);

            var timeStampLabelHistory = NormalizeWhitespace(surveysPage.SurveyResult.TimeStampLabel.Text);
            var jurisFilterNamesHistory = surveysPage.ContentType.JurisdictionContentTypes
                .Select(NormalizeWhitespace).ToList();
            var labelsHistory = surveysPage.WlaSurveyResult.GetAllJurisdictionLabels();
            var jurisLabelsOnResultHistory = labelsHistory
                .Select(NormalizeWhitespace)
                .Where(text => !string.IsNullOrWhiteSpace(text)).ToList();
            var questionDisplayedHistory = NormalizeWhitespace(surveysPage.SurveyResult.QuestionLabel.Text);

            this.TestCaseVerify.IsTrue(
                "Verify viewing survey from history: query and timestamp match",
                NormalizeWhitespace(SurveyQuestion).Equals(questionDisplayedHistory)
                && timeStampLabelOriginal.Equals(timeStampLabelHistory),
                $"Viewing survey from history: query and timestamp not match. " +
                $"Question expected: {SurveyQuestion} Question displayed: {questionDisplayedHistory} " +
                $"Timestamp expected: {timeStampLabelOriginal} Timestamp displayed: {timeStampLabelHistory}");

            this.TestCaseVerify.IsTrue(
                "Verify viewing survey from history: juris filter and label match",
                jurisFilterNamesOriginal.SequenceEqual(jurisFilterNamesHistory)
                && jurisLabelsOnResultOriginal.SequenceEqual(jurisLabelsOnResultHistory),
                $"Viewing survey from history: juris filter and label not match. " +
                $"Juris filter expected: [{string.Join(", ", jurisFilterNamesOriginal)}] Juris filter displayed: [{string.Join(", ", jurisFilterNamesHistory)}] " +
                $"Juris label expected: [{string.Join(", ", jurisLabelsOnResultOriginal)}] Juris label displayed: [{string.Join(", ", jurisLabelsOnResultHistory)}]");
        }

        /// <summary>
        /// Test Include related federal option behavior when viewing result from delivery (Task 2292717).
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(FeatureTestCategoryWlaAjsIncludeRelatedFed)]
        [TestProperty(EnvironmentConstants.InfrastructureAccessControlsOn, "IAC-AIJS-INCLUDE-RELATED-FEDERAL,IAC-AI-50SS-PROFILE13")]
        public void AjsIncludeRelatedFedDeliveryTest()
        {
            const string SurveyQuestion = "What is lemon law?";
            const string JurisCA = "CA-CS", JurisIncludeRelatedFed = "ALLFEDS";
            const string DeliveryDateFormat = "MM-dd-yyyy";

            FileUtil.DeleteFilesInFolderByMask(FolderToSave, "*.*");

            var surveysPage = NavigateToLandingPage();
            surveysPage.WlaQueryBox.EnterQuestion(SurveyQuestion);
            surveysPage.WlaQueryBox.SelectIncludeCases();
            surveysPage.Jurisdictions.SelectJurisdiction(JurisCA, JurisIncludeRelatedFed);
            Thread.Sleep(1000);
            surveysPage = surveysPage.CreateSurveyButtonTop.Click<AiJurisdictionalSurveysPage>();
            SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed);

            WaitForSurveyResultsLoaded(surveysPage);

            SafeMethodExecutor.WaitUntil(() => surveysPage.SurveyResult.TimeStampLabel.Displayed);
            var downloadDialog = surveysPage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);

            downloadDialog.LayoutAndLimitsTab.SetIncludeSectionOption(LayoutAndLimitsInclude.CoverPage);

            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<AiJurisdictionalSurveysPage>();

            // Use regex to match the downloaded file regardless of appended numbers
            var datePattern = DateTime.Now.ToString(DeliveryDateFormat).Replace("-", "\\-");
            var filePattern = $@"Westlaw Advantage - AI Jurisdictional Survey - {datePattern}(\s*\(\d+\))?\.pdf$";

            var filePath = FileUtil.GetTheDownloadedFileName(filePattern, FolderToSave);

            this.TestCaseVerify.IsTrue(
                "Verify downloaded PDF file exists",
                !string.IsNullOrEmpty(filePath) && File.Exists(filePath),
                $"Downloaded PDF file not found matching pattern: {filePattern}");

            var text = PdfTextExtractor.ExtractTextFromPdf(filePath);

            this.TestCaseVerify.IsTrue(
               "Verify cover page captured Include federal info correctly",
               text.Contains("Westlaw AI Jurisdictional Surveys results")
               && text.Contains($"Question:  {SurveyQuestion}")
               && text.Contains("Content:  Statutes, regulations, and cases")
               && text.Contains("Jurisdiction:  Federal, California")
               && text.Contains($"Survey generated:  {DateTime.Now.ToString("dd MMMM yyyy")}"),
               "Cover page not captured Include federal info correctly");

            var textWithoutWhitespaces = text.Replace(" ", string.Empty).Replace("\r\n", string.Empty);

            this.TestCaseVerify.IsTrue(
              "Verify Federal Statutes and regulations header in delivery",
              textWithoutWhitespaces.Contains("Federal Statutes and regulations".Replace(" ", string.Empty)),
              "Federal Statutes and regulations header not displayed in delivery");

            this.TestCaseVerify.IsTrue(
              "Verify State statutes and regulations header in delivery",
              textWithoutWhitespaces.Contains("California State statutes and regulations".Replace(" ", string.Empty)),
              "State statutes and regulations header not displayed in delivery");

            this.TestCaseVerify.IsTrue(
              "Verify Cases State header in delivery",
              textWithoutWhitespaces.Contains("Cases State".Replace(" ", string.Empty)),
              "Cases State header not displayed in delivery");
        }

        /// <summary>
        /// Waits for all four survey result headings to be displayed.
        /// Uses SafeMethodExecutor.WaitUntil so timeout does not throw — the assertions
        /// after this call will report which specific heading was missing.
        /// </summary>
        /// <param name="surveysPage">The surveys page instance.</param>
        /// <param name="stateName">The state name for the state statutes heading.</param>
        /// <param name="timeoutFromSec">Maximum time to wait in seconds.</param>
        private void WaitForAllHeadingsDisplayed(AiJurisdictionalSurveysPage surveysPage, string stateName, int timeoutFromSec = 60)
        {
            // Federal heading is already waited for in WaitForSurveyResultsLoaded.
            // Wait for the state-specific and cases headings which render after federal.
            SafeMethodExecutor.WaitUntil(
                () => surveysPage.WlaSurveyResult.StateStatutesRegulationsHeading(stateName).Displayed,
                timeoutFromSec: timeoutFromSec);
            SafeMethodExecutor.WaitUntil(
                () => surveysPage.WlaSurveyResult.CasesStateHeading.Displayed,
                timeoutFromSec: timeoutFromSec);
            SafeMethodExecutor.WaitUntil(
                () => surveysPage.WlaSurveyResult.CasesFederalHeading.Displayed,
                timeoutFromSec: timeoutFromSec);
        }

        /// <summary>
        /// Normalizes all Unicode whitespace characters (non-breaking spaces, narrow no-break spaces, etc.)
        /// to regular ASCII spaces, and trims the result.
        /// </summary>
        private static string NormalizeWhitespace(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Replace any Unicode whitespace character with a regular space
            var normalized = System.Text.RegularExpressions.Regex.Replace(input, @"[\s\u00A0\u202F\u2007\u2060]+", " ");
            return normalized.Trim();
        }
    }
}