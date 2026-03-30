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
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;

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

            this.TestCaseVerify.IsTrue(
                "Verify Include related federal is disabled when no juris is selected",
                surveysPage.Jurisdictions.IsJurisdictionSelectionDisabled(JurisIncludeRelatedFed) &&
                surveysPage.Jurisdictions.SelectedCountLabel.Text.Contains("0 selected"),
            "Include related federal is not disabled when no juris selected.");

            surveysPage.Jurisdictions.SelectJurisdiction(JurisSelectAll);

            this.TestCaseVerify.IsTrue(
                "Verify Include related federal is selected clicking Select All",
                surveysPage.Jurisdictions.IsJurisdictionSelected(JurisIncludeRelatedFed),
            "Include related federal is not selected clicking Select All.");

            surveysPage.Jurisdictions.SelectJurisdiction(JurisCA);

            this.TestCaseVerify.IsFalse(
                "Verify Include related federal is enabled when a juris is selected",
                surveysPage.Jurisdictions.IsJurisdictionSelectionDisabled(JurisIncludeRelatedFed),
            "Include related federal is not enabled when a juris is selected.");

            surveysPage.Jurisdictions.SelectJurisdiction(false, JurisIncludeRelatedFed);

            this.TestCaseVerify.IsTrue(
                "Verify selected count increases by 1 when Include related federal is selected",
                surveysPage.Jurisdictions.SelectedCountLabel.Text.Contains("2 selected"),
            "Selected count does not increase by 1 when Include related federal is selected.");

            surveysPage.Jurisdictions.SelectJurisdiction(false, JurisCA);

            this.TestCaseVerify.IsTrue(
                "Verify Include related federal is disabled when the only selected juris is unselected",
                surveysPage.Jurisdictions.IsJurisdictionSelectionDisabled(JurisIncludeRelatedFed) &&
                surveysPage.Jurisdictions.SelectedCountLabel.Text.Contains("0 selected"),
            "Include related federal is not disabled when the only selected juris is unselected.");

            surveysPage.Jurisdictions.SelectJurisdiction(JurisCA, JurisMN, JurisIncludeRelatedFed);
            surveysPage.Jurisdictions.SelectJurisdiction(false, JurisMN);

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
            Thread.Sleep(1000);
            surveysPage = surveysPage.CreateSurveyButtonTop.Click<AiJurisdictionalSurveysPage>();
            SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed);

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
            saveToFolderDialog.FolderTreeComponent.SelectFolderByName(AjsTestFolder);
            saveToFolderDialog.ClickSaveButton<AiJurisdictionalSurveysPage>();

            var recentFolderDialog = surveysPage.Header.ClickHeaderTab<EdgeRecentFoldersDialog>(EdgeHeaderTabs.Folders);
            var folderPage = recentFolderDialog.ClickFolderByName(AjsTestFolder).ClickViewThisFolderButton();
            surveysPage = folderPage.FolderGrid.ClickGridItemByName<AiJurisdictionalSurveysPage>(SurveyQuestion);
            SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed, 3000);

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
            const string WlaTab = "Westlaw Advantage";
            const string WlaAjsBrowserTitle = "AI Jurisdictional Surveys | Westlaw Advantage";

            var surveysPage = NavigateToLandingPage();
            surveysPage.WlaQueryBox.EnterQuestion(SurveyQuestion);
            surveysPage.WlaQueryBox.SelectIncludeCases();
            surveysPage.Jurisdictions.SelectJurisdiction(JurisCA, JurisMN, JurisIncludeRelatedFed);
            Thread.Sleep(1000);
            surveysPage = surveysPage.CreateSurveyButtonTop.Click<AiJurisdictionalSurveysPage>();
            SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed);

            var jurisFilterNamesOriginal = surveysPage.ContentType.JurisdictionContentTypes;
            // Do not include empty or whitespace-only labels.
            var labels = surveysPage.WlaSurveyResult.GetAllJurisdictionLabels();
            var jurisLabelsOnResultOriginal = labels.Where(text => !string.IsNullOrWhiteSpace(text)).ToList();

            this.TestCaseVerify.IsTrue(
               "Verify jurisdiction filters names match with jurisdiction labels on result",
                jurisFilterNamesOriginal.SequenceEqual(jurisLabelsOnResultOriginal),
               "Jurisdiction filters names not match with jurisdiction labels on result");

            var originalBrowserTitle = BrowserPool.CurrentBrowser.Title;
            var originalPageHeading = surveysPage.PageHeaderLabel.Text;

            surveysPage.Toolbar.ResearchCopyLinkButton.Click();
            SafeMethodExecutor.WaitUntil(() => surveysPage.CopiedLinkSuccessLabel.Displayed, timeoutFromSec: 10);
            var copiedLink = Clipboard.GetText();

            this.DefaultSignOnManager.SignOff();

            var homePage = this.SignOnBack().ClickContinueButton<AdvantageHomePage>();
            BrowserPool.CurrentBrowser.CreateTab(WlaTab);
            BrowserPool.CurrentBrowser.ActivateTab(WlaTab);

            var surveysPageCopiedLinkPage = this.GetHomePage<AdvantageHomePage>()
                   .OpenNewTabUsingJavascript<AiJurisdictionalSurveysPage>(WlaAjsBrowserTitle, copiedLink);
            SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed);

            this.TestCaseVerify.IsTrue(
               "Verify browser tab title is correct on copy link page",
               BrowserPool.CurrentBrowser.Title.Equals(originalBrowserTitle),
               "Browser tab title is not correct after opening copied link");

            this.TestCaseVerify.IsTrue(
               "Verify page heading is correct on copy link page",
               surveysPageCopiedLinkPage.PageHeaderLabel.Text.Equals(originalPageHeading),
               "Heading is not correct after opening copied link");

            var jurisFilterNamesCopyLink = surveysPageCopiedLinkPage.ContentType.JurisdictionContentTypes;
            var copyLinkLabels = surveysPageCopiedLinkPage.WlaSurveyResult.GetAllJurisdictionLabels();
            var jurisLabelsOnResultCopyLink = copyLinkLabels.Where(text => !string.IsNullOrWhiteSpace(text)).ToList();

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
            Thread.Sleep(1000);
            surveysPage = surveysPage.CreateSurveyButtonTop.Click<AiJurisdictionalSurveysPage>();
            SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed);

            var timeStampLabelOriginal = surveysPage.SurveyResult.TimeStampLabel.Text;
            var jurisFilterNamesOriginal = surveysPage.ContentType.JurisdictionContentTypes;
            var labels = surveysPage.WlaSurveyResult.GetAllJurisdictionLabels();
            var jurisLabelsOnResultOriginal = labels.Where(text => !string.IsNullOrWhiteSpace(text)).ToList();

            // Wait for survey to appear in history by periodically refreshing and checking the table.
            var recentHistoryDialog = surveysPage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);
            var historyPage = recentHistoryDialog.ViewAllLink.Click<EdgeCommonHistoryPage>();
            SafeMethodExecutor.WaitUntil(
                () =>
                {
                    historyPage = BrowserPool.CurrentBrowser.Refresh<EdgeCommonHistoryPage>();
                    return historyPage.HistoryTable.GetGridItems().First().IsTextLinkDisplayed(SurveyQuestion);
                },
                timeoutFromSec: 30);

            var firstHistoryItem = historyPage.HistoryTable.GetGridItems().First();

            this.TestCaseVerify.IsTrue(
                "Verify Jurisdictional Surveys entry displayed in history",
                firstHistoryItem.Title.Equals(SurveyQuestion),
                "Jurisdictional Surveys entry not displayed in History");

            surveysPage = firstHistoryItem.ClickLinkByText<AiJurisdictionalSurveysPage>(SurveyQuestion);
            SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed);

            var timeStampLabelHistory = surveysPage.SurveyResult.TimeStampLabel.Text;
            var jurisFilterNamesHistory = surveysPage.ContentType.JurisdictionContentTypes;
            var labelsHistory = surveysPage.WlaSurveyResult.GetAllJurisdictionLabels();
            var jurisLabelsOnResultHistory = labelsHistory.Where(text => !string.IsNullOrWhiteSpace(text)).ToList();
            var questionDisplayedHistory = surveysPage.SurveyResult.QuestionLabel.Text;

            this.TestCaseVerify.IsTrue(
                "Verify viewing survey from history: query and timestamp match",
                questionDisplayedHistory.Equals(SurveyQuestion)
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
            

            SafeMethodExecutor.WaitUntil(() => surveysPage.SurveyResult.TimeStampLabel.Displayed);
            var downloadDialog = surveysPage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog.LayoutAndLimitsTab.SetIncludeSectionOption(LayoutAndLimitsInclude.CoverPage);

            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);

            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<AiJurisdictionalSurveysPage>();
            var fileName = $"Westlaw Advantage - AI Jurisdictional Survey - {DateTime.Now.ToString(DeliveryDateFormat)}.pdf";
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            var text = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, fileName));        

            this.TestCaseVerify.IsTrue(
               "Verify cover page captured Include federal info correctly",
               text.Contains("Westlaw AI Jurisdictional Surveys results")
               && text.Contains($"Question:  {SurveyQuestion}")
               && text.Contains("Content:  Statutes, regulations, and cases")
               && text.Contains("Jurisdiction:  Federal, California")
               && text.Contains($"Delivered:  {DateTime.Now.ToString("MMMM d, yyyy")}"),
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
    }
}
