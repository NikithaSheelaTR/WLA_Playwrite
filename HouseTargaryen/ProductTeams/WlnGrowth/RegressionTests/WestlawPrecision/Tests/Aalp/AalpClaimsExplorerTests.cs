namespace WestlawPrecision.Tests.Aalp
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using Framework.Common.UI.Products.Shared.Dialogs.Delivery;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Enums.RI;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage.HomePageTabs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.AALP;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.AALP;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
    using Framework.Common.UI.Products.WestLawNext.Dialogs.Header;
    using Framework.Common.UI.Products.WestLawNext.Enums.Delivery;
    using Framework.Common.UI.Products.WestLawNext.Utils;
    using Framework.Common.UI.Raw.EnhancementTests.Utils;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Header;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.DataModel.Security;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.DataModel.Security.Specialized;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.Extensions;
    using Framework.Core.Utils.IO;
    using global::WestlawPrecision.Utilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    [TestClass]
    public class AalpClaimsExplorerTests : AalpBaseTest
    {
        private const string FeatureTestCategory = "ClaimsExplorer";

        /// <summary>
        /// Test case: 1889548, 1888553, 1895899, 1895617, 1895632, 1896555
        /// Verify common functionality: info dialogs, landing page
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        public void AiClaimsExplorerCommonTest()
        {            
            const string HowAiWorksDialogTitle = "How Claims Explorer works";           

            string checkLandingPage = "Verify: Claims Explorer page is opened";
            string checkQuestionTextboxMainPlaceholder = "Verify: Main question textbox placeholder is as expected";
            string checkClaimsExplorerTileDescription = "Verify: 'Claims Explorer' tile description is as expected";
            string checkWelcomeLandingPageText = "Verify: Welcome text is as expected";
            string checkHowAiWorksTitleAndDescription = "Verify: 'How Claims Explorer works' dialog title and description are as expected";
            string checkTipsDialog = "Verify: Tips dialog contains text";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            homePage = homePage.Header.CompartmentDropdown.ArrowButton.Click<PrecisionHomePage>();

            var productName = homePage.Header.CompartmentDropdown.SelectedOptionLink.Text;

            this.TestCaseVerify.AreEqual(
                checkClaimsExplorerTileDescription,
                "Find potential claims for your fact pattern and highlight specific statutory, common law, and constitutional causes of action for further research.",
                homePage.FeaturesIncludedPanel.GetWidgetTextByTitle(ClaimsExplorerHeadingLabel),
                "'Claims Explorer' tile description is NOT as expected");

            var claimsExplorerPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(ClaimsExplorerHeadingLabel).Click<AiClaimsExplorerPage>();
            BrowserPool.CurrentBrowser.CreateTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            this.TestCaseVerify.IsTrue(
                checkLandingPage,
                BrowserPool.CurrentBrowser.Url.Contains("usageFeature=AIClaimsFinder")
                && BrowserPool.CurrentBrowser.Title.Equals($"Claims Explorer | {productName}")
                && claimsExplorerPage.Toolbar.HeadingLabel.Text.Equals(ClaimsExplorerHeadingLabel),
                "Claims Explorer page is NOT opened");

            this.TestCaseVerify.IsTrue(
               checkQuestionTextboxMainPlaceholder,             
               claimsExplorerPage.QueryBox.TitleLabel.Text.Equals("Enter facts"),
               "Main question textbox placeholder is NOT as expected");

            this.TestCaseVerify.IsTrue(
                checkWelcomeLandingPageText,
                claimsExplorerPage.Chat.LandingPageLabel.Displayed,
                "Welcome text is NOT as expected");

            var howAiWorksDialog = claimsExplorerPage.Toolbar.HowAiWorksButton.Click<HowAiAssistedResearchWorksDialog>();

            this.TestCaseVerify.IsTrue(
                checkHowAiWorksTitleAndDescription,
                howAiWorksDialog.TitleLabel.Text.Equals(HowAiWorksDialogTitle),         
                "'How Claims Explorer works' dialog title and description are NOT as expected");

            claimsExplorerPage = howAiWorksDialog.CloseButton.Click<AiClaimsExplorerPage>();

            var tipsForBestResultsDialog = claimsExplorerPage.Toolbar.TipsBestResultsButton.Click<TipsForBestResultsDialog>();

            this.TestCaseVerify.IsTrue(
                checkTipsDialog,
                tipsForBestResultsDialog.DescriptionLabel.Text.Contains(ClaimsExplorerHeadingLabel),
                "Tips dialog doesn't contain text");
        }

        /// <summary>
        /// Test case: 1888500, 1896579, 1905588, 1908947, 2148102
        /// Verify jurisdiction and new claims search buttons, Disclaimer Information
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        public void AiClaimsExplorerJurisdictionAndNewClaimsSearchTest()
        {         
            const string Question = "Plaintiff is a former employee of defendant whose father suffered a stroke that left him with permanent disabilities, " +
                "and plaintiff spends a lot of time taking care of him. During the pandemic, plaintiff’s father’s doctor advised he stay home and that plaintiff " +
                "stay home to help him without going out so he didn’t get infected. Plaintiff was told he’d have to use accumulated sick leave but that taking " +
                "the time off would “reflect poorly” on him and could lead to a pay decrease or termination. When he took time off, he began to hear about negative " +
                "comments made about him, and he was eventually laid off. He complained to human resources, but was still terminated.";
            const string DisclaimerInformation = "Claims Explorer results use generative AI and should be verified for accuracy.";

            string checkLandingPage = "Verify: Claims Explorer page is opened";
            string checkSaveButtonIsEnabledForOneJuris = "Verify: Save button is enabled for one jurisdiction";
            string checkSaveButtonIsDisabledForTwoJuris = "Verify: Save button is disabled for two jurisdictions";
            string checkSaveButtonIsDisabledForAllJurisdictions = "Verify: Save button is disabled for all jurisictions";
            string checkJurisdictionAfterQuestion = "Verify: Jurisdiction is retained after asking a question";
            string checkQueryBoxIsNotDisplayed = "Verify: Unable to ask follow-up qustions";
            string checkJurisdictionAfterRelogin = "Verify: Jurisdiction is retained after relogin";
            string checkDisclaimerInfo = "Verify: Disclaimer information is disaplyed";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            homePage.Header.OpenJurisdictionDialog<EdgeJurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<PrecisionHomePage>();

            var claimsExplorerPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(ClaimsExplorerHeadingLabel).Click<AiClaimsExplorerPage>();
            BrowserPool.CurrentBrowser.CreateTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            this.TestCaseVerify.IsTrue(
               checkLandingPage,
               BrowserPool.CurrentBrowser.Url.Contains("usageFeature=AIClaimsFinder")
               && claimsExplorerPage.Toolbar.HeadingLabel.Text.Equals(ClaimsExplorerHeadingLabel),
               "Claims Explorer page is NOT opened");

            var aiJurisdictionDialog = claimsExplorerPage.Toolbar.JurisdictionButton.Click<AiJurisdictionDialog>();
            aiJurisdictionDialog.SelectJurisdictionsClaimsExplorer(true, Jurisdiction.California).ClickSaveButtonClaimsExplorer();

            this.TestCaseVerify.IsTrue(
                checkSaveButtonIsEnabledForOneJuris,
                aiJurisdictionDialog.SaveButton.Enabled,
                "Save button is NOT enabled for one jurisdiction");

            aiJurisdictionDialog.SelectJurisdictionsClaimsExplorer(true, Jurisdiction.NewYork,Jurisdiction.California).ClickSaveButtonClaimsExplorer();

            this.TestCaseVerify.IsFalse(
                checkSaveButtonIsDisabledForTwoJuris,
                aiJurisdictionDialog.SaveButton.Enabled,
                "Save button is NOT disabled for two jurisdictions");

            aiJurisdictionDialog.SelectJurisdictionsClaimsExplorer(true, Jurisdiction.NewYork, Jurisdiction.California).ClickSaveButtonClaimsExplorer();

            this.TestCaseVerify.IsFalse(
                checkSaveButtonIsDisabledForAllJurisdictions,
                aiJurisdictionDialog.SaveButton.Enabled,
                "Save button is NOT disabled for all jurisdictions");

            aiJurisdictionDialog.SelectJurisdictionsClaimsExplorer(true, Jurisdiction.California).ClickSaveButtonClaimsExplorer();

            claimsExplorerPage = claimsExplorerPage.QueryBox.QuestionTextbox.SetText<AiClaimsExplorerPage>(Question);
            claimsExplorerPage = claimsExplorerPage.QueryBox.SendQuestionButton.Click<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkDisclaimerInfo,
                claimsExplorerPage.Chat.DisclaimerInformationLabel.Text.Equals(DisclaimerInformation),
                "Disclaimer information is not displayed correctly");

            this.TestCaseVerify.IsTrue(
                checkJurisdictionAfterQuestion,   
                claimsExplorerPage.Toolbar.JurisdictionLabel.Text.Equals("CA, All Fed."),      
                "Jurisdiction is NOT retained after asking a question");

            var federalTab = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<FederalTabComponent>(ClaimsExplorerAnswerTab.Federal);
            this.TestCaseVerify.IsTrue(
                checkQueryBoxIsNotDisplayed,
                !claimsExplorerPage.QueryBox.IsDisplayed()
                && federalTab.NewClaimsSearchButton.Text.Equals("New claims search"),
                "Query box is displayed");

            this.DefaultSignOnManager.SignOff();
            homePage = this.SignOnBack().ClickContinueButton<PrecisionHomePage>();

            claimsExplorerPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(ClaimsExplorerHeadingLabel).Click<AiClaimsExplorerPage>();
            BrowserPool.CurrentBrowser.CreateTab("NewClaimsExplorerTab");
            BrowserPool.CurrentBrowser.ActivateTab("NewClaimsExplorerTab");

            this.TestCaseVerify.IsTrue(
                checkJurisdictionAfterRelogin,
                claimsExplorerPage.Toolbar.JurisdictionButton.Text.Equals("CA, All Fed."),
                "Jurisdiction is retained after re-login");
        }

        /// <summary>
        /// Test case: 1885514
        /// Verify daily limit
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        [TestProperty("ResearchDailyLimit", "1")]
        [TestProperty(EnvironmentConstants.InfrastructureAccessControlsOn, "IAC-AI-ASSISTANT-USAGE-LIMITS-DEBUG")]
        public void AiClaimsExplorerLimitTest()
        {
            const string Question = "How close must a toilet paper dispenser be to a toilet under the ADA?";

            string checkLandingPage = "Verify: Claims Explorer page is opened";
            string checkQuestionLimitMessage = "Verify: Question limit message is displayed after the limit exceeding the limit";
            string checkLandingPageQuestionLimitMessage = "Verify: Landing page quesion limit message is displayed after returning to Claims Finder";

            var homePage = this.GetHomePage<PrecisionHomePage>();

            var tooltab = homePage.BrowseTabPanel.SetActiveTab<PrecisionToolsTabPanel>(PrecisionBrowseTab.Tools);
            var claimsExplorerPage = tooltab.ToolsItems.First(item => item.HeaderLink.Text.Contains(ClaimsExplorerHeadingLabel)).HeaderLink.Click<AiClaimsExplorerPage>();

            BrowserPool.CurrentBrowser.CreateTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            claimsExplorerPage = claimsExplorerPage.UsageDebug.BackDateExpiryButton.Click<AiClaimsExplorerPage>();

            this.TestCaseVerify.IsTrue(
               checkLandingPage,
               BrowserPool.CurrentBrowser.Url.Contains("usageFeature=AIClaimsFinder")
               && claimsExplorerPage.Toolbar.HeadingLabel.Text.Equals(ClaimsExplorerHeadingLabel),
               "Claims Explorer page is NOT opened");

            claimsExplorerPage = claimsExplorerPage.QueryBox.QuestionTextbox.SetText<AiClaimsExplorerPage>(Question);
            claimsExplorerPage = claimsExplorerPage.QueryBox.SendQuestionButton.Click<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            var limitMessage = $"You've reached your daily limit of {claimsExplorerPage.UsageDebug.DailyLimitLabel.Text} questions. This limit resets every night at 12:00 a.m. Central time. You can still access your prior AI research via your History.";

            this.TestCaseVerify.IsTrue(
               checkQuestionLimitMessage,
               claimsExplorerPage.QueryBox.QuestionLimitLabel.Text.Replace(" ", string.Empty).Contains(limitMessage.Replace(" ", string.Empty))
               && !claimsExplorerPage.QueryBox.QuestionTextbox.Displayed,
               "Question limit message is NOT displayed after the limit exceeding the limit");

            BrowserPool.CurrentBrowser.CloseTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

            // Bug 2111910 - Uncomment when fixed:
            //var sessionPauseDialog = new SessionPauseDialog();
            //sessionPauseDialog.ClickContinueSessionButton<EdgeHomePage>();

            claimsExplorerPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(ClaimsExplorerHeadingLabel).Click<AiClaimsExplorerPage>();
            BrowserPool.CurrentBrowser.CreateTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

           this.TestCaseVerify.IsTrue(
               checkLandingPageQuestionLimitMessage,
               claimsExplorerPage.Chat.ChatSummaryLabel.Text.Replace(" ", string.Empty).Contains(limitMessage.Replace(" ", string.Empty))
               && !claimsExplorerPage.QueryBox.IsDisplayed(),
               "Landing page quesion limit message is NOT displayed after returning to Claims Finder");

            claimsExplorerPage.UsageDebug.BackDateExpiryButton.Click();
        }        

        /// <summary>
        /// Test case #1894680, 1897259
        /// Verify history events
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        public void AiClaimsExplorerHistoryEventTest()
        {
            const string Question = "Plaintiff is a former employee of defendant whose father suffered a stroke that left him with permanent disabilities, " +
               "and plaintiff spends a lot of time taking care of him. During the pandemic, plaintiff’s father’s doctor advised he stay home and that plaintiff " +
               "stay home to help him without going out so he didn’t get infected. Plaintiff was told he’d have to use accumulated sick leave but that taking " +
               "the time off would “reflect poorly” on him and could lead to a pay decrease or termination. When he took time off, he began to hear about negative " +
               "comments made about him, and he was eventually laid off. He complained to human resources, but was still terminated.";
            const string SearchType = "ClaimsExplorer";
            const string SelectedJurisdiction = "CaliforniaAllFederal";
            const string ConversationDateFormat = "MMM d, yyyy hh:mm tt";
            const string HistoryEventDateFormat = "M/d/yyyh:mm tt";

            var trimmedQuestion = Question.Substring(Question.IndexOf("terminated")).Replace(" ", string.Empty);

            string checkLandingPage = "Verify: Claims Explorer page is opened";
            string checkHistoryTab = "Verify: Description is correct on the History tab";
            string checkHistoryTabLeadsToClaimsExplorer = "Verify: Event from the History tab leads to Claims Explorer page";
            string checkHistoryPage = "Verify: Description is correct on History page";
            string checkDelivery = "Verify: Description is correct in the Delivered history";
            string checkHistoryPageLeadsToClaimsExplorer = "Verify: Event from the History page leads to Claims Explorer page";
            string checkSignOffPage = "Verify: Description is correct on the Sign off page";
            string checkClientIdPage = "Verify: Description is correct on the Client Id page";      
            string checkClientIdLeadsToClaimsExplorer = "Verify: Event from the Client ID page leads to Claims Explorer page";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            homePage = homePage.Header.CompartmentDropdown.ArrowButton.Click<PrecisionHomePage>();

            var productName = homePage.Header.CompartmentDropdown.SelectedOptionLink.Text;

            if (!homePage.GetStartedPanel.GetStartedOptionsLinks.Any(link => link.Text.Contains(ClaimsExplorerHeadingLabel)))
            {
                var getStartedDialog = homePage.GetStartedPanel.CustomizeGetStartedPanelButton.Click<PrecisionGetStartedDialog>();
                getStartedDialog.SelectionsPanel.ClearAllButton.Click();
                var getStartedToolsTab = getStartedDialog.TabPanel.SetActiveTab<PrecisionGetStartedToolsTabComponent>(PrecisionGetStartedBrowseTab.Tools);
                getStartedToolsTab.SetCheckboxByOptionName(ClaimsExplorerHeadingLabel);
                homePage = getStartedDialog.SaveButton.Click<PrecisionHomePage>();
            }

            var claimsExplorerPage = homePage.GetStartedPanel.GetStartedOptionsLinks.First().Click<AiClaimsExplorerPage>();
            BrowserPool.CurrentBrowser.CreateTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            this.TestCaseVerify.IsTrue(
               checkLandingPage,
               BrowserPool.CurrentBrowser.Url.Contains("usageFeature=AIClaimsFinder")
               && claimsExplorerPage.Toolbar.HeadingLabel.Text.Equals(ClaimsExplorerHeadingLabel),
               "Claims Explorer page is NOT opened");

            var aiJurisdictionDialog = claimsExplorerPage.Toolbar.JurisdictionButton.Click<AiJurisdictionDialog>();
            aiJurisdictionDialog.SelectJurisdictionsClaimsExplorer(true, Jurisdiction.California).ClickSaveButtonClaimsExplorer();

            claimsExplorerPage = claimsExplorerPage.QueryBox.QuestionTextbox.SetText<AiClaimsExplorerPage>(Question);
            claimsExplorerPage = claimsExplorerPage.QueryBox.SendQuestionButton.Click<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.DateLabel.Displayed);

            // History tab
            var recentHistoryDialog = claimsExplorerPage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);
            SafeMethodExecutor.WaitUntil(() => recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemLink.Displayed);

            var conversationDate = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.DateLabel.Text;
            var formattedConversationDate = DateTime.ParseExact(conversationDate, ConversationDateFormat, CultureInfo.InvariantCulture);

            if (!recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemDateLabel.Text.Equals(formattedConversationDate))
            {
                SafeMethodExecutor.WaitUntil(() =>
                {
                    claimsExplorerPage = BrowserPool.CurrentBrowser.Refresh<AiClaimsExplorerPage>();

                    conversationDate = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.DateLabel.Text;
                    formattedConversationDate = DateTime.ParseExact(conversationDate, ConversationDateFormat, CultureInfo.InvariantCulture);

                    recentHistoryDialog = claimsExplorerPage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);

                    var historyEventDate = recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemDateLabel.Text;
                    var upperBoundHistoryEventDate = formattedConversationDate.AddMinutes(1).ToString(HistoryEventDateFormat);

                    return historyEventDate.Equals(formattedConversationDate.ToString(HistoryEventDateFormat)) || historyEventDate.Equals(upperBoundHistoryEventDate);
                }, timeoutFromSec: 60, pollingIntervalInMilliseconds: 2000);
            }

            this.TestCaseVerify.IsTrue(
                checkHistoryTab,
                recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemLink.Text.Contains(Question.Substring(0, Question.IndexOf("father")))
                && recentHistoryDialog.GetRecentSearchesItems().First().MetaData.Replace(" ", string.Empty).Contains($"{SearchType}{SelectedJurisdiction}"),
                "Description is NOT correct on the History tab");

            //History event click
            claimsExplorerPage = recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemLink.Click<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkHistoryTabLeadsToClaimsExplorer,
                claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.QuestionLabel.Text.Remove(0, claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.QuestionLabel.Text.IndexOf("says:") + 7).Equals(Question)
                && claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.IsDisplayed(ClaimsExplorerAnswerTab.State)
                && claimsExplorerPage.Toolbar.JurisdictionLabel.Text.Equals("CA, All Fed."),
                "Event from the History tab doesn't lead to Claims Explorer page");

            claimsExplorerPage = claimsExplorerPage.ConversationHistory.ExpandButton.Click<AiClaimsExplorerPage>();

            //History page ('Go to full history' link)
            var historyPage = claimsExplorerPage.ConversationHistory.GoToFullHistoryLink.Click<EdgeCommonHistoryPage>();
            BrowserPool.CurrentBrowser.CreateTab(HistoryPageTab);
            BrowserPool.CurrentBrowser.ActivateTab(HistoryPageTab);
       
            this.TestCaseVerify.IsTrue(
                checkHistoryPage,
                historyPage.HistoryTable.GetGridItems().First().Title.Contains(Question.Substring(0, Question.IndexOf("father")))
                && historyPage.HistoryTable.GetGridItems().First().Summary.Replace(" ", string.Empty).Equals($"{SearchType}{SelectedJurisdiction}"),
                "Description is NOT correct on History page");
       
            // Delivery
            var historyEventsCount = historyPage.HistoryTable.GetGridItems().Count.ToString();

            var downloadDialog = historyPage.EdgeToolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);

            downloadDialog.TheBasicsTab.NumberToDeliver.SelectOption<DownloadDialog>(downloadDialog.TheBasicsTab.NumberToDeliver.Options.FirstOrDefault(option => option.Contains(historyEventsCount)));

            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<PrecisionCommonSearchResultPage>();

            var fileName = historyEventsCount.Equals(1) 
                ? $"{productName} - List of {historyEventsCount} item from {GetUserInfo().FirstName} {GetUserInfo().LastName} All History.pdf" 
                : $"{productName} - List of {historyEventsCount} items from {GetUserInfo().FirstName} {GetUserInfo().LastName} All History.pdf";

            FileUtil.WaitForFileDownload(this.FolderToSave, fileName);
            var text = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(this.FolderToSave, fileName)).Replace("\r\n", string.Empty).Replace(" ", string.Empty);

            this.TestCaseVerify.IsTrue(
                checkDelivery,
                text.Contains($"{trimmedQuestion}{SearchType}"),
                "Description is NOT correct in the Delivered history");

            //History event click
            claimsExplorerPage = historyPage.HistoryTable.GetGridItems().First().ClickLinkByText<AiClaimsExplorerPage>(historyPage.HistoryTable.GetGridItems().First().Title);
            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkHistoryPageLeadsToClaimsExplorer,
                claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.QuestionLabel.Text.Remove(0, claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.QuestionLabel.Text.IndexOf("says:") + 7).Equals(Question)
                && claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.IsDisplayed(ClaimsExplorerAnswerTab.State)
                && claimsExplorerPage.Toolbar.JurisdictionLabel.Text.Equals("CA, All Fed."),
                "Event from the History page doesn't lead to Claims Explorer page");

            //Sign off page
            var signOffPage = historyPage.Header.ClickHeaderTab<EdgeProfileSettingsDialog>(EdgeHeaderTabs.PreferencesAndSignOut).ClickSignOff();
            var lastEventTitle = signOffPage.SignOffSessionDetailsComponent.GetSessionItems().First().GetDescriptionText().Replace("\r\n", string.Empty).Replace(" ", string.Empty);

            this.TestCaseVerify.IsTrue(
                checkSignOffPage,
                lastEventTitle.Contains($"{trimmedQuestion}{SearchType}{SelectedJurisdiction}"),
                "Description is NOT correct on the Sign off page");

            //Client ID page
            var clientIdPage = this.SignOnBack();
            lastEventTitle = clientIdPage.RecentResearchPane.RecentResearchList.First().Text.Replace("\r\n", string.Empty).Replace(" ", string.Empty);

            this.TestCaseVerify.IsTrue(
                checkClientIdPage,
                lastEventTitle.Contains($"{trimmedQuestion}{SearchType}{SelectedJurisdiction}"),
                "Category page name is NOT displayed instead of jurisdiction on the Client Id page");

            claimsExplorerPage = clientIdPage.RecentResearchPane.RecentResearchList.First().ClickTitleLink<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            var stateTab = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<StateTabComponent>(ClaimsExplorerAnswerTab.State);
            this.TestCaseVerify.IsTrue(
                checkClientIdLeadsToClaimsExplorer,
                claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.QuestionLabel.Text.Remove(0, claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.QuestionLabel.Text.IndexOf("says:") + 7).Equals(Question)
                && claimsExplorerPage.Toolbar.JurisdictionLabel.Text.Equals("CA, All Fed.")
                && stateTab.NewClaimsSearchButton.Displayed,
                "Event from the Client ID page doesn't lead to Claims Explorer page");
        }

        /// <summary>
        /// Test case: 1909377, 1909875, 1909582
        /// Verify accordion view
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        public void AiClaimsExplorerAccordionViewTest()
        {
            const string Question = "Plaintiff is a former employee of defendant whose father suffered a stroke that left him with permanent disabilities, " +
                "and plaintiff spends a lot of time taking care of him. During the pandemic, plaintiff’s father’s doctor advised he stay home and that plaintiff " +
                "stay home to help him without going out so he didn’t get infected. Plaintiff was told he’d have to use accumulated sick leave but that taking " +
                "the time off would “reflect poorly” on him and could lead to a pay decrease or termination. When he took time off, he began to hear about negative " +
                "comments made about him, and he was eventually laid off. He complained to human resources, but was still terminated.";

            string checkHeadingIsExpandedByDefault = "Verify: Heading is expanded by default";
            string checkHeadingIsCollapsed = "Verify: Heading is collapsed after click";
            string checkNoUnexpectedHeadingsCollapses = "Verify: Headings aren't collapsed unexpectedly due to the filter switch";
            string checkHeadingStateIsSavedAfterTabSwitch = "Verify: Heading state is saved when returning from another tab";
            List<string> headingNames = new List<string>();
            int count = 0;

            var claimsExplorerPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(ClaimsExplorerHeadingLabel).Click<AiClaimsExplorerPage>();
            BrowserPool.CurrentBrowser.CreateTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            var aiJurisdictionDialog = claimsExplorerPage.Toolbar.JurisdictionButton.Click<AiJurisdictionDialog>();
            aiJurisdictionDialog.SelectJurisdictionsClaimsExplorer(true, Jurisdiction.California).ClickSaveButtonClaimsExplorer();

            claimsExplorerPage = claimsExplorerPage.QueryBox.QuestionTextbox.SetText<AiClaimsExplorerPage>(Question);
            claimsExplorerPage = claimsExplorerPage.QueryBox.SendQuestionButton.Click<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            var federalTabComponent = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<FederalTabComponent>(ClaimsExplorerAnswerTab.Federal);

            this.TestCaseVerify.IsTrue(
                checkHeadingIsExpandedByDefault,
                federalTabComponent.Headings.First().HeadingAccordionButton.GetAttribute("aria-expanded").Equals("true"),
                "Heading is NOT expanded by default");

            claimsExplorerPage = federalTabComponent.Headings.First().HeadingAccordionButton.Click<AiClaimsExplorerPage>();

            this.TestCaseVerify.IsFalse(
                checkHeadingIsCollapsed,
                federalTabComponent.Headings.First().HeadingAccordionButton.GetAttribute("aria-expanded").Equals("true"),
                "Heading is NOT collapsed after click");

            var stateTabComponent = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<StateTabComponent>(ClaimsExplorerAnswerTab.State);

            stateTabComponent = stateTabComponent.SupportedFilterButton.Click<StateTabComponent>();

            foreach (var item in stateTabComponent.Headings.ToList())
            {
                headingNames.Add(item.HeadingAccordionButton.GetAttribute("aria-label"));
            }

            stateTabComponent = stateTabComponent.AdditionalFactsNeededFilterButton.Click<StateTabComponent>();
            
            foreach (var item in stateTabComponent.Headings.ToList())
            {
               if( !headingNames.Contains(item.HeadingAccordionButton.GetAttribute("aria-label")))
               {
                    claimsExplorerPage = item.HeadingAccordionButton.Click<AiClaimsExplorerPage>();
                    count++;
               }

               if (count == 2)
               {
                    break;
               }
            }

            stateTabComponent = stateTabComponent.SupportedFilterButton.Click<StateTabComponent>();

            this.TestCaseVerify.IsTrue(
                checkNoUnexpectedHeadingsCollapses,
                stateTabComponent.Headings.First().HeadingAccordionButton.GetAttribute("aria-expanded").Equals("true")
                && stateTabComponent.Headings.ElementAt(1).HeadingAccordionButton.GetAttribute("aria-expanded").Equals("true"),
                "Headings are collapsed unexpectedly due to the filter switch");

            federalTabComponent = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<FederalTabComponent>(ClaimsExplorerAnswerTab.Federal);

            this.TestCaseVerify.IsFalse(
                checkHeadingStateIsSavedAfterTabSwitch,
                federalTabComponent.Headings.First().HeadingAccordionButton.GetAttribute("aria-expanded").Equals("true"),
                "Heading state is NOT saved when returning from another tab");
        }

        /// <summary>
        /// Test case: 1895667, 1899802, 1899486
        /// Verify filtering on federal and state tabs
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        public void AiClaimsExplorerPillsFilteringTest()
        {           
            const string Question = "Plaintiff is a former employee of defendant whose father suffered a stroke that left him with permanent disabilities, " +
                "and plaintiff spends a lot of time taking care of him. During the pandemic, plaintiff’s father’s doctor advised he stay home and that plaintiff " +
                "stay home to help him without going out so he didn’t get infected. Plaintiff was told he’d have to use accumulated sick leave but that taking " +
                "the time off would “reflect poorly” on him and could lead to a pay decrease or termination. When he took time off, he began to hear about negative " +
                "comments made about him, and he was eventually laid off. He complained to human resources, but was still terminated.";

            string checkCounterBadges = "Verify: Counter badges numbers are equal to 'All' pills numbers";
            string checkFiltersOrder = "Verify: Filters order is correct";
            string checkAllByDefault = "Verify: 'All' filter pill is selected by default, items count equals to 'All' pill number";
            string checkNumbersSum = "Verify: 'All' number equals to the sum of 'Supported' and 'Additional facts needed'";
            string checkSupportedCount = "Verify: 'Supported' items count equals to 'Supported' pill number";
            string checkSupportedOnly = "Verify: Only 'Supported' items are displayed for 'Supported' filter";
            string checkAllContainsSupported = "Verify: 'All' items contains all 'Supported' items";
            string checkAdditionalFactsCount = "Verify: 'Additional facts needed' items count equals to 'Additional facts needed' pill number";
            string checkAdditionalFactsOnly = "Verify: Only 'Additional facts needed' items are displayed for 'Additional facts needed' filter";
            string checkAllContainsAdditionalFacts = "Verify: 'All' items contains all 'Additional facts needed' items";

            var claimsExplorerPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(ClaimsExplorerHeadingLabel).Click<AiClaimsExplorerPage>();
            BrowserPool.CurrentBrowser.CreateTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            var aiJurisdictionDialog = claimsExplorerPage.Toolbar.JurisdictionButton.Click<AiJurisdictionDialog>();
            aiJurisdictionDialog.SelectJurisdictionsClaimsExplorer(true, Jurisdiction.California).ClickSaveButtonClaimsExplorer();

            claimsExplorerPage = claimsExplorerPage.QueryBox.QuestionTextbox.SetText<AiClaimsExplorerPage>(Question);
            claimsExplorerPage = claimsExplorerPage.QueryBox.SendQuestionButton.Click<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            var federalTabComponent = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<FederalTabComponent>(ClaimsExplorerAnswerTab.Federal);
            var federalAllPillNumber = federalTabComponent.AllFilterButton.Text.ConvertCountToInt();

            var stateTabComponent = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<StateTabComponent>(ClaimsExplorerAnswerTab.State);
            var allItems = stateTabComponent.Headings.SelectMany(item => item.SubHeadings).Select(subItem => subItem.HeadingLabel.Text).ToList();
            var allButtons = stateTabComponent.FilterButtons.Select(button => button.Text).ToList();
            var stateAllPillNumber = stateTabComponent.AllFilterButton.Text.ConvertCountToInt();
            var supportedPillNumber = stateTabComponent.SupportedFilterButton.Text.ConvertCountToInt();
            var additionalFactsPillNumber = stateTabComponent.AdditionalFactsNeededFilterButton.Text.ConvertCountToInt();

            this.TestCaseVerify.IsTrue(
                checkCounterBadges,
                claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.TabHeaderLabel(ClaimsExplorerAnswerTab.Federal).Text.ConvertCountToInt().Equals(federalAllPillNumber)
                && claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.TabHeaderLabel(ClaimsExplorerAnswerTab.State).Text.ConvertCountToInt().Equals(stateAllPillNumber),
                "Counter badges numbers are NOT equal to 'All' pills numbers");

            var expectedFiltersOrder = new List<string> 
            { 
                $"All ({stateAllPillNumber})",
                $"Supported ({supportedPillNumber})" ,
                $"Additional facts needed ({additionalFactsPillNumber})"             
            };

            this.TestCaseVerify.IsTrue(
                checkFiltersOrder,
                allButtons.SequenceEqual(expectedFiltersOrder),
                "Filters order is NOT correct");

            this.TestCaseVerify.IsTrue(
                checkAllByDefault,
                allItems.ToList().TrueForAll(item => item.Contains("Supported") || item.Contains("Additional facts needed"))
                && allItems.Count.Equals(stateAllPillNumber),
                "''All' filter pill is NOT selected by default, items count doesn't equal to 'All' pill number");

            this.TestCaseVerify.IsTrue(
                checkNumbersSum,
                stateAllPillNumber.Equals(supportedPillNumber + additionalFactsPillNumber),
                "'All' number doesn't equal to the sum of 'Supported' and 'Additional facts needed'");

            stateTabComponent = stateTabComponent.SupportedFilterButton.Click<StateTabComponent>();

            var supportedItems = stateTabComponent.Headings.SelectMany(item => item.SubHeadings).Select(subItem => subItem.HeadingLabel.Text).ToList();

            this.TestCaseVerify.IsTrue(
                checkSupportedCount,
                supportedItems.Count.Equals(supportedPillNumber),
                "'Supported' items count doesn't equal to 'Supported' pill number");

            this.TestCaseVerify.IsTrue(
                checkSupportedOnly,
                supportedItems.TrueForAll(item => item.Contains("Supported")),
                "NOT only 'Supported' items are displayed for 'Supported' filter");

            this.TestCaseVerify.IsTrue(
                checkAllContainsSupported,
                allItems.Where(item => item.Contains("Supported")).ToList().SequenceEqual(supportedItems),
                "'All' items doesn't contain all 'Supported' items");

            stateTabComponent = stateTabComponent.AdditionalFactsNeededFilterButton.Click<StateTabComponent>();

            var additionalFactsNeededItems = stateTabComponent.Headings.SelectMany(item => item.SubHeadings).Select(subItem => subItem.HeadingLabel.Text).ToList();

            this.TestCaseVerify.IsTrue(
                checkAdditionalFactsCount,
                additionalFactsNeededItems.Count.Equals(additionalFactsPillNumber),
                "'Additional facts needed' items count doesn't equal to 'Additional facts needed' pill number");

            this.TestCaseVerify.IsTrue(
                checkAdditionalFactsOnly,
                additionalFactsNeededItems.TrueForAll(item => item.Contains("Additional facts needed")),
                "NOT only 'Additional facts needed' items are displayed for 'Additional facts needed' filter");

            this.TestCaseVerify.IsTrue(
                checkAllContainsAdditionalFacts,
                allItems.Where(item => item.Contains("Additional facts needed")).ToList().SequenceEqual(additionalFactsNeededItems),
                "'All' items doesn't contain all 'Additional facts needed' items");
        }

        /// <summary>
        /// Test case: 1899326, 1899803, 1900217, 1899803, 1899805, 1913150
        /// Verify pdf download delivery
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        [TestCategory(TeamMatzekAdvantageSmokeCategory)]
        public void AiClaimsExplorerDeliveryTest()
        {
            const string NoAnswerQuestion = "Where do I file a foreign subpoena in Northern Marianna Islands court case?";
            //Need to use question that returns Federal and State tabs
            const string Question = "You are an associate attorney writing an email summary for your supervising attorney who is determining whether to take on a client and wants you to summarize the relevant claims and issues. The potential client is a 60 year old woman who needs a cane to walk was fired abruptly and without explanation from her job in Minnesota. What possible claims does she have?";
            const string GuidRegex = @"Document/(N[^/]+)/";

            string checkZeroState = "Verify: Zero state is displayed in the answer for the zero state question";
            string checkDeliveryDisabled = "Verify: Delivery is disabled for question gives no answer";
            string checkDownloadDialog = "Verify: Download options are correct";
            string checkCoverPage = "Verify: Cover page has correct info";
            string checkJurisdictionEntries = "Verify: Jurisdiction appears twice";
            string checkAskedQuestion = "Verify: Asked question is present";
            string checkBadges = "Verify: Badges are present";
            string checkDeliveredTabsHeaders = "Verify: Tabs headers are displayed in the delivered document";
            string checkDeliveredLinks = "Verify: All documents with links are contain links in the delivered document";
            string checkKcFlags = "Verify: KC flags are present";
            string checkEmailDialog = "Verify: Email options are correct";
            string checkEmailDeliveryWorks = "Verify: Email delivery works";
            string checkPrintDialog = "Verify: Print options are correct";
            string checkPrintDeliveryWorks = "Verify: Print delivery works";

            FileUtil.DeleteFilesInFolderByMask(FolderToSave, "*.*");

            var claimsExplorerPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(ClaimsExplorerHeadingLabel).Click<AiClaimsExplorerPage>();
            BrowserPool.CurrentBrowser.CreateTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            var aiJurisdictionDialog = claimsExplorerPage.Toolbar.JurisdictionButton.Click<AiJurisdictionDialog>();
            aiJurisdictionDialog.SelectJurisdictionsClaimsExplorer(true, Jurisdiction.California).ClickSaveButtonClaimsExplorer();

            claimsExplorerPage = claimsExplorerPage.QueryBox.QuestionTextbox.SetText<AiClaimsExplorerPage>(NoAnswerQuestion);
            claimsExplorerPage = claimsExplorerPage.QueryBox.SendQuestionButton.Click<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkZeroState,
                claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ErrorAnswerLabel.Text.Equals("Your current fact pattern did not return any claims in the chosen jurisdiction(s), please edit your fact pattern and try again.")
                && !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.IsDisplayed(ClaimsExplorerAnswerTab.Federal)
                && !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.IsDisplayed(ClaimsExplorerAnswerTab.State),
                "Zero state is NOT displayed in the answer for the zero state question");

            this.TestCaseVerify.IsFalse(
                checkDeliveryDisabled,
                claimsExplorerPage.Toolbar.DeliveryDropdown.IsDeliveryDropdownDisplayed(),
                "Delivery is NOT disabled for question gives no answer");
           
            claimsExplorerPage = claimsExplorerPage.Toolbar.NewResearchButton.Click<AiClaimsExplorerPage>();

            aiJurisdictionDialog = claimsExplorerPage.Toolbar.JurisdictionButton.Click<AiJurisdictionDialog>();
            aiJurisdictionDialog.SelectJurisdictionsClaimsExplorer(true, Jurisdiction.California).ClickSaveButtonClaimsExplorer();
            claimsExplorerPage = claimsExplorerPage.QueryBox.QuestionTextbox.SetText<AiClaimsExplorerPage>(Question);
            claimsExplorerPage = claimsExplorerPage.QueryBox.SendQuestionButton.Click<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            var federalItemsCount = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.TabHeaderLabel(ClaimsExplorerAnswerTab.Federal).Text.ConvertCountToInt();
            var stateItemsCount = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.TabHeaderLabel(ClaimsExplorerAnswerTab.State).Text.ConvertCountToInt();

            var federalTabComponent = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<FederalTabComponent>(ClaimsExplorerAnswerTab.Federal);
            var federalItems = federalTabComponent.Headings.Select(heading => heading.DocumentLink.GetAttribute("href")).ToList().Select(link => Regex.Match(link, GuidRegex).Groups[1].Value).ToList();
            
            var stateTabComponent = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<StateTabComponent>(ClaimsExplorerAnswerTab.State);
            var stateItems = stateTabComponent.Headings.Select(heading => heading.DocumentLink.GetAttribute("href")).ToList().Select(link => Regex.Match(link, GuidRegex).Groups[1].Value).ToList();

            var downloadDialog = claimsExplorerPage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog.LayoutAndLimitsTab.SetIncludeSectionOption(LayoutAndLimitsInclude.CoverPage);

            this.TestCaseVerify.IsTrue(
               checkDownloadDialog,
               downloadDialog.GetDialogTitle().Equals("Download claims results")
               && SafeMethodExecutor.ExecuteUntil(() => downloadDialog.TheBasicsTab.FormatDropdown.SelectedOption.Equals(DeliveryFormat.Pdf), timeoutFromSec: 15)
               && SafeMethodExecutor.ExecuteUntil(() => downloadDialog.LayoutAndLimitsTab.CoverPageComment.Displayed, timeoutFromSec: 15),          
               "Download options are NOT correct");

            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<AiAssistedResearchPage>();
            var fileName = $"Westlaw Precision - Westlaw Claims Explorer - {DateTime.Now.ToString(DeliveryDateFormat)}.pdf";            
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            var text = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, fileName));
            var textWithoutWhitespaces = text.Replace(" ", string.Empty).Replace("\r\n", string.Empty);

           this.TestCaseVerify.IsTrue(
               checkCoverPage,
               textWithoutWhitespaces.Contains("Westlaw Claims Explorer".Replace(" ", string.Empty))
               && textWithoutWhitespaces.Contains($"Research delivered: {DateTime.Now.ToString("MMMM d, yyyy")}".Replace(" ", string.Empty))
               && textWithoutWhitespaces.Contains($"Response generated: {DateTime.Now.ToString("MMMM d, yyyy")}".Replace(" ", string.Empty))
               && textWithoutWhitespaces.Contains($"Delivered by: {this.GetUserInfo().FirstName} {this.GetUserInfo().LastName}".Replace(" ", string.Empty))
               && textWithoutWhitespaces.Contains($"Client ID: {this.GetUserInfo().ClientId.ToUpper()}".Replace(" ", string.Empty))
               && textWithoutWhitespaces.Contains("Jurisdictions: California, All Federal".Replace(" ", string.Empty))
               && textWithoutWhitespaces.Contains("Comment:"),
               "Cover page wrong info");

            this.TestCaseVerify.IsTrue(
                checkJurisdictionEntries,
                text.Select((count, item) => text.Substring(item)).Count(sub => sub.StartsWith("Jurisdictions:  California, All Federal")).Equals(2),
                "Jurisdiction doesn't appear twice");

            this.TestCaseVerify.IsTrue(
                checkAskedQuestion,
                textWithoutWhitespaces.Contains($"Fact pattern {Question}".Replace(" ", string.Empty).Replace("\r\n", string.Empty)),     
                "Asked question is NOT present");

            this.TestCaseVerify.IsTrue(
                checkBadges,
                //textWithoutWhitespaces.Contains($"Supported")
                textWithoutWhitespaces.Contains($"Additional facts needed".Replace(" ", string.Empty).Replace("\r\n", string.Empty)),
                "Badges are NOT present");

            this.TestCaseVerify.IsTrue(
                checkDeliveredTabsHeaders,
                textWithoutWhitespaces.Contains($"Federal({federalItemsCount})")
                && textWithoutWhitespaces.Contains($"State({stateItemsCount})"),
                "Tabs headers are NOT displayed in the delivered document");

            var links = PdfTextExtractor.GetLinks(Path.Combine(FolderToSave, fileName));

            this.TestCaseVerify.IsTrue(
                checkDeliveredLinks,
                links.Where(item => item.Contains("Document/")).ToList().Select(item => Regex.Match(item, GuidRegex).Groups[1].Value).ToList().Any(item => federalItems.Contains(item))
                && links.Where(item => item.Contains("Document/")).ToList().Select(item => Regex.Match(item, GuidRegex).Groups[1].Value).ToList().Any(item => stateItems.Contains(item)),
                "NOT all documents with links are contain links in the delivered document");

            //KC flag verification
            this.TestCaseVerify.IsTrue(
              checkKcFlags,
              links.Any(link => link.Contains("RelatedInformation/Flag")),
              "KC flags are NOT present");

            // Email dialog
            if (this.Settings.GetValue(EnvironmentConstants.IsFedRamp).ToLower().Equals("no"))
            {
                var emailDialog = claimsExplorerPage.Toolbar.DeliveryDropdown.SelectOption<EmailDialog>(DeliveryMethod.Email);
                emailDialog.EnterEmailText("email@noemail.com");

                this.TestCaseVerify.IsTrue(
                    checkEmailDialog,
                    emailDialog.GetDialogTitle().Equals("Email claims results")
                    && emailDialog.RecipientsTab.IsEmailToTextboxDisplayed()
                    && emailDialog.RecipientsTab.IsEmailSubjectTextboxDisplayed()
                    && emailDialog.RecipientsTab.IsEmailNoteTextboxDisplayed()
                    && emailDialog.LayoutAndLimitsTab.CoverPageComment.Displayed
                    && emailDialog.LayoutAndLimitsTab.IsIncludeSectionOptionSelected(LayoutAndLimitsInclude.CoverPage),
                    "Email options are not correct");

                emailDialog.RecipientsTab.FormatDropdown.SelectOption(DeliveryFormat.Pdf);
                var readyToEmailDialog = emailDialog.ClickEmailButton<ReadyForDeliveryDialog>();

                this.TestCaseVerify.IsTrue(
                    checkEmailDeliveryWorks,
                    readyToEmailDialog.IsTextPresented("Ready For Email"),
                    "Email delivery doesn't work");

                readyToEmailDialog.WaitForEmailDialogToDisappear();
            }

            // Print dialog
            var printDialog = claimsExplorerPage.Toolbar.DeliveryDropdown.SelectOption<PrintDialog>(DeliveryMethod.Print);

            this.TestCaseVerify.IsTrue(
                checkPrintDialog,
                printDialog.GetDialogTitle().Equals("Print claims results")
                && printDialog.LayoutAndLimitsTab.IsIncludeSectionOptionSelected(LayoutAndLimitsInclude.CoverPage),
                "Print options are NOT correct");

            printDialog.ClickPrintButton();

            bool printDialogIsClosed = printDialog.CloseBrowserPrintDialog(this.TestExecutionContext.TestClient.Id,
               () => this.TestCaseVerify.IsTrue(
                        checkPrintDeliveryWorks,
                        printDialog.IsLoadingPrintLightBoxDisplayed(50000),
                        "Print delivery doesn't work"));
        }

        /// <summary>
        /// Test case: 1909321
        /// Verify docs download delivery
        /// </summary>
        [TestMethod]
        [TestCategory("Bug 1956507")]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        public void AiClaimsExplorerDocxDeliveryTest()
        {
            const string Question = "You are an associate attorney writing an email summary for your supervising attorney who is determining whether to take on a client and wants you to summarize the relevant claims and issues. The potential client is a 60 year old woman who needs a cane to walk was fired abruptly and without explanation from her job in Minnesota. What possible claims does she have?";
            const string GuidRegex = @"Document/(N[^/]+)/";

            string checkCoverPage = "Verify: Cover page has correct info";
            string checkJurisdictionEntries = "Verify: Jurisdiction appears twice";
            string checkAskedQuestion = "Verify: Asked question is present";
            string checkBadges = "Verify: Badges are present";
            string checkDeliveredTabsHeaders = "Verify: Tabs headers are displayed in the delivered document";
            string checkDeliveredLinks = "Verify: All documents with links are contain links in the delivered document";

            var claimsExplorerPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(ClaimsExplorerHeadingLabel).Click<AiClaimsExplorerPage>();
            BrowserPool.CurrentBrowser.CreateTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            var aiJurisdictionDialog = claimsExplorerPage.Toolbar.JurisdictionButton.Click<AiJurisdictionDialog>();
            aiJurisdictionDialog.SelectJurisdictionsClaimsExplorer(true, Jurisdiction.California).ClickSaveButtonClaimsExplorer();

            claimsExplorerPage = claimsExplorerPage.QueryBox.QuestionTextbox.SetText<AiClaimsExplorerPage>(Question);
            claimsExplorerPage = claimsExplorerPage.QueryBox.SendQuestionButton.Click<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            var federalItemsCount = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.TabHeaderLabel(ClaimsExplorerAnswerTab.Federal).Text.ConvertCountToInt();
            var stateItemsCount = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.TabHeaderLabel(ClaimsExplorerAnswerTab.State).Text.ConvertCountToInt();

            var federalTabComponent = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<FederalTabComponent>(ClaimsExplorerAnswerTab.Federal);
            var federalItems = federalTabComponent.Headings.Select(heading => heading.DocumentLink.GetAttribute("href")).ToList().Select(link => Regex.Match(link, GuidRegex).Groups[1].Value).ToList();

            var stateTabComponent = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<StateTabComponent>(ClaimsExplorerAnswerTab.State);
            var stateItems = stateTabComponent.Headings.Select(heading => heading.DocumentLink.GetAttribute("href")).ToList().Select(link => Regex.Match(link, GuidRegex).Groups[1].Value).ToList();

            var downloadDialog = claimsExplorerPage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);

            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Docx);
            downloadDialog.LayoutAndLimitsTab.SetIncludeSectionOption(LayoutAndLimitsInclude.CoverPage);
            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<AiAssistedResearchPage>();
            var fileName = $"Westlaw Precision - Westlaw Claims Explorer - {DateTime.Now.ToString(DeliveryDateFormat)}.docx";
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            var text = DocxTextExtractor.ExtractTextFromWord(Path.Combine(FolderToSave, fileName));

            var textWithoutWhitespaces = text.Replace(" ", string.Empty);

            this.TestCaseVerify.IsTrue(
               checkCoverPage,
               textWithoutWhitespaces.Contains("Westlaw Claims Explorer".Replace(" ", string.Empty))
               && textWithoutWhitespaces.Contains($"Research delivered: {DateTime.Now.ToString("MMMM d, yyyy")}".Replace(" ", string.Empty))
               && textWithoutWhitespaces.Contains($"Response generated: {DateTime.Now.ToString("MMMM d, yyyy")}".Replace(" ", string.Empty))
               && textWithoutWhitespaces.Contains($"Delivered by: {this.GetUserInfo().FirstName} {this.GetUserInfo().LastName}".Replace(" ", string.Empty))
               && textWithoutWhitespaces.Contains($"Client ID: {this.GetUserInfo().ClientId.ToUpper()}".Replace(" ", string.Empty))
               && textWithoutWhitespaces.Contains("Jurisdictions: California, All Federal".Replace(" ", string.Empty))
               && textWithoutWhitespaces.Contains("Comment:"),
               "Cover page wrong info");

            this.TestCaseVerify.IsTrue(
                checkJurisdictionEntries,
                text.Select((count, item) => text.Substring(item)).Count(sub => sub.StartsWith("Jurisdictions: California, All Federal")).Equals(2),
                "Jurisdiction doesn't appear twice");

            this.TestCaseVerify.IsTrue(
                checkAskedQuestion,
                textWithoutWhitespaces.Contains($"Factpattern{Question}".Replace(" ", string.Empty)),
                "Asked question is NOT present");

            this.TestCaseVerify.IsTrue(
                checkBadges,
                //textWithoutWhitespaces.Contains($"Supported")
                textWithoutWhitespaces.Contains($"Additional facts needed".Replace(" ", string.Empty)),
                "Badges are NOT present");

            this.TestCaseVerify.IsTrue(
                checkDeliveredTabsHeaders,
                textWithoutWhitespaces.Contains($"Federal({federalItemsCount})")
                && textWithoutWhitespaces.Contains($"State({stateItemsCount})"),
                "Tabs headers are NOT displayed in the delivered document");

            var links = DocxTextExtractor.GetLinks(Path.Combine(FolderToSave, fileName));

            this.TestCaseVerify.IsTrue(
                checkDeliveredLinks,
                links.Where(item => item.Contains("Document/")).ToList().Select(item => Regex.Match(item, GuidRegex).Groups[1].Value).ToList().Any(item => federalItems.Contains(item))
                && links.Where(item => item.Contains("Document/")).ToList().Select(item => Regex.Match(item, GuidRegex).Groups[1].Value).ToList().Any(item => stateItems.Contains(item)),
                "NOT all documents with links are contain links in the delivered document");
        }

        /// <summary>
        /// Test case: 1898091
        /// Verify jurisdiction is retained for conversation
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        public void AiClaimsExplorerJurisdictionChangingTest()
        {
            const string FirstQuestion = "A contract was executed in CA, between my client, CCI, and the defendant. CCI, a manufacturer of electric vehicle charging stations, sought " +
                                    "automation services from the defendant. After signing the contract, it became clear that the promised system could not be delivered as " +
                                    "initially envisioned by the sales team. CCI informed the defendants and sought to rescind the contract, citing both joint mistake and obvious " +
                                    "misrepresentation by the defendant’s sales team. Despite this, Defendant demanded payment of the contract balance and refused to refund CCI's " +
                                    "deposit. Defendant does business in California but does not have necessary certificate of qualification. What claims, if any, may CCI bring?";
            const string SecondQuestion = "My client's neighbor stole their golf cart, rammed it into a couple saplings on my client's property, then drove it into a pond.";

            string checkJurisdictionForPreviousConversation = "Verify: Jurisdiction is not changed for previous conversation";

            var claimsExplorerPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(ClaimsExplorerHeadingLabel).Click<AiClaimsExplorerPage>();
            BrowserPool.CurrentBrowser.CreateTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            claimsExplorerPage.Toolbar.JurisdictionButton.Click<AiJurisdictionDialog>().SelectJurisdictionsClaimsExplorer(true, Jurisdiction.California).ClickSaveButtonClaimsExplorer();
            claimsExplorerPage = claimsExplorerPage.QueryBox.QuestionTextbox.SetText<AiClaimsExplorerPage>(FirstQuestion);
            claimsExplorerPage = claimsExplorerPage.QueryBox.SendQuestionButton.Click<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            claimsExplorerPage = claimsExplorerPage.Toolbar.NewResearchButton.Click<AiClaimsExplorerPage>();

            claimsExplorerPage.QueryBox.JurisdictionButton.Click<AiJurisdictionDialog>().SelectJurisdictionsClaimsExplorer(true, Jurisdiction.California).ClickSaveButtonClaimsExplorer();
            claimsExplorerPage = claimsExplorerPage.QueryBox.QuestionTextbox.SetText<AiClaimsExplorerPage>(SecondQuestion);
            claimsExplorerPage = claimsExplorerPage.QueryBox.SendQuestionButton.Click<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            claimsExplorerPage = claimsExplorerPage.ConversationHistory.ExpandButton.Click<AiClaimsExplorerPage>();
            claimsExplorerPage = claimsExplorerPage.ConversationHistory.Conversations.ElementAt(1).ConversationButton.Click<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkJurisdictionForPreviousConversation,
                claimsExplorerPage.Toolbar.JurisdictionLabel.Text.Equals("California"),
                "Jurisdiction is changed for previous conversation");            
        }

        /// <summary>
        /// Test case: 1899367, 1906196, 1909368
        /// Verify citation links work
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        public void AiClaimsExplorerCitationLinksTest()
        {      
            const string Question = "Plaintiff is a former employee of defendant whose father suffered a stroke that left him with permanent disabilities, " +
                "and plaintiff spends a lot of time taking care of him. During the pandemic, plaintiff’s father’s doctor advised he stay home and that plaintiff " +
                "stay home to help him without going out so he didn’t get infected. Plaintiff was told he’d have to use accumulated sick leave but that taking " +
                "the time off would “reflect poorly” on him and could lead to a pay decrease or termination. When he took time off, he began to hear about negative " +
                "comments made about him, and he was eventually laid off. He complained to human resources, but was still terminated.";

            string checkLandingPage = "Verify: Claims Explorer page is opened";
            string checkDocumentLink = "Verify: Document is opened";
            string checkReturnToForDocumentLink = "Verify: Return To button works for Document link";
            string checkKeyCiteFlagLink = "Verify: History or Negative Treatmentis is opened";
            string checkReturnToForKeyCiteFlag = "Verify: Return To button works for KeyCite flag link";

            var claimsExplorerPage = this.GetHomePage<PrecisionHomePage>().Header.ExpandToolsFlyoutButton.Click<PrecisionToolsDialog>().ToolLinks.First(link => link.Text.Contains(ClaimsExplorerHeadingLabel)).Click<AiClaimsExplorerPage>();
            BrowserPool.CurrentBrowser.CreateTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            this.TestCaseVerify.IsTrue(
               checkLandingPage,
               BrowserPool.CurrentBrowser.Url.Contains("usageFeature=AIClaimsFinder")
               && claimsExplorerPage.Toolbar.HeadingLabel.Text.Equals(ClaimsExplorerHeadingLabel),
               "Claims Explorer page is NOT opened");

            var aiJurisdictionDialog = claimsExplorerPage.Toolbar.JurisdictionButton.Click<AiJurisdictionDialog>();
            aiJurisdictionDialog.SelectJurisdictionsClaimsExplorer(true, Jurisdiction.California).ClickSaveButtonClaimsExplorer();

            claimsExplorerPage = claimsExplorerPage.QueryBox.QuestionTextbox.SetText<AiClaimsExplorerPage>(Question);
            claimsExplorerPage = claimsExplorerPage.QueryBox.SendQuestionButton.Click<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            var stateTab = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<StateTabComponent>(ClaimsExplorerAnswerTab.State);
            var documentPage = stateTab.Headings.First().DocumentLink.Click<EdgeCommonDocumentPage>();

            this.TestCaseVerify.IsTrue(
               checkDocumentLink,
               documentPage.IsDocumentLoaded(),
               "Document is not opened");

            claimsExplorerPage = documentPage.FixedHeader.ClickReturnToListButton<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
               checkReturnToForDocumentLink,
               stateTab.Headings.First().DocumentLink.IsInView,
               "Return To button doesn't work for Document link");

            var negativeTreatmentPage = stateTab.Headings.First(doc => doc.KeyCiteFlagLink.Displayed).KeyCiteFlagLink.Click<EdgeNegativeTreatmentPage>();

            this.TestCaseVerify.IsTrue(
               checkKeyCiteFlagLink,
               negativeTreatmentPage.RiTabs.GetSelectedTab().Equals(RiTab.NegativeTreatment)
               || negativeTreatmentPage.RiTabs.GetSelectedTab().Equals(RiTab.History),
               "History or Negative Treatmentis is not opened");

            claimsExplorerPage = documentPage.FixedHeader.ClickReturnToListButton<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
               checkReturnToForKeyCiteFlag,
               stateTab.Headings.First(doc => doc.KeyCiteFlagLink.Displayed).DocumentLink.IsInView,
               "Return To button doesn't work for KeyCite flag link");
        }

        /// <summary>
        /// Test case: 1910171
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        public void AiClaimsExplorerIntentResolutionTest()
        {
            const string Question = "weather";
            const string IntentResolverMessage = "This question appears to be outside the scope of AI-Assisted Research, which uses Westlaw materials and generative AI to address legal research questions. Try asking about primary law materials.";

            string checkIntentResolver = "Verify: Intent resolver appears";

            var claimsExplorerPage = this.GetHomePage<PrecisionHomePage>().Header.ExpandToolsFlyoutButton.Click<PrecisionToolsDialog>().ToolLinks.First(link => link.Text.Contains(ClaimsExplorerHeadingLabel)).Click<AiClaimsExplorerPage>();
            BrowserPool.CurrentBrowser.CreateTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            var aiJurisdictionDialog = claimsExplorerPage.Toolbar.JurisdictionButton.Click<AiJurisdictionDialog>();
            aiJurisdictionDialog.SelectJurisdictionsClaimsExplorer(true, Jurisdiction.California).ClickSaveButtonClaimsExplorer();

            claimsExplorerPage = claimsExplorerPage.QueryBox.QuestionTextbox.SetText<AiClaimsExplorerPage>(Question);
            claimsExplorerPage = claimsExplorerPage.QueryBox.SendQuestionButton.Click<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
              checkIntentResolver,
              claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ResubmitButton.Displayed
              && claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.EditButton.Displayed
              && claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.SummaryLabel.Text.Equals(IntentResolverMessage),
              "Intent resolver doesn't appear");
        }

        /// <summary>
        /// Test case: 1904618
        /// Verify OOP banner on Claims Explorer supporting materials
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        [TestProperty("OutOfPlanUser", "On")]
        public void AiClaimsExplorerOutOfPlanBannerTest()
        {
            const string Question = "Plaintiff is a former employee of defendant whose father suffered a stroke that left him with permanent disabilities, " +
                "and plaintiff spends a lot of time taking care of him. During the pandemic, plaintiff’s father’s doctor advised he stay home and that plaintiff " +
                "stay home to help him without going out so he didn’t get infected. Plaintiff was told he’d have to use accumulated sick leave but that taking " +
                "the time off would “reflect poorly” on him and could lead to a pay decrease or termination. When he took time off, he began to hear about negative " +
                "comments made about him, and he was eventually laid off. He complained to human resources, but was still terminated.";

            string checkOutOfPlanBanners = "Verify: Claims Explorer supporting materials contain OOP banners";

            var claimsExplorerPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(ClaimsExplorerHeadingLabel).Click<AiClaimsExplorerPage>();
            BrowserPool.CurrentBrowser.CreateTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            var aiJurisdictionDialog = claimsExplorerPage.Toolbar.JurisdictionButton.Click<AiJurisdictionDialog>();
            aiJurisdictionDialog.SelectJurisdictionsClaimsExplorer(true, Jurisdiction.California).ClickSaveButtonClaimsExplorer();

            claimsExplorerPage = claimsExplorerPage.QueryBox.QuestionTextbox.SetText<AiClaimsExplorerPage>(Question);
            claimsExplorerPage = claimsExplorerPage.QueryBox.SendQuestionButton.Click<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            var federalTab = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<FederalTabComponent>(ClaimsExplorerAnswerTab.Federal);

            var isFederalOop = federalTab.Headings.Any(item => item.OutOfPlanLabel.Displayed);

            var stateTab = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<StateTabComponent>(ClaimsExplorerAnswerTab.State);

            var isStateOop = stateTab.Headings.Any(item => item.OutOfPlanLabel.Displayed);

            this.TestCaseVerify.IsTrue(
               checkOutOfPlanBanners,
               isFederalOop
               || isStateOop,
               "Claims Explorer supporting materials don't contain OOP banners");
        }

        /// <summary>
        /// Test case: 1911333
        /// Verify Limit concurrent searches
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        public void AiClaimsExplorerConcurrentSearchesLimitTest()
        {
            const string Question = "My client's neighbor stole their golf cart, rammed it into a couple saplings on my client's property, then drove it into a pond.";

            string checkLimitConcurrentSearchesWarning = "Verify: Limit concurrent searches warning message is displayed for >1 parallel searches";

            var claimsExplorerPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(ClaimsExplorerHeadingLabel).Click<AiClaimsExplorerPage>();
            BrowserPool.CurrentBrowser.CreateTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            var aiJurisdictionDialog = claimsExplorerPage.Toolbar.JurisdictionButton.Click<AiJurisdictionDialog>();
            aiJurisdictionDialog.SelectJurisdictionsClaimsExplorer(true, Jurisdiction.California).ClickSaveButtonClaimsExplorer();

            var claimsExplorerLandingPageUrl = BrowserPool.CurrentBrowser.Url;

            claimsExplorerPage = claimsExplorerPage.QueryBox.QuestionTextbox.SetText<AiClaimsExplorerPage>(Question);
            claimsExplorerPage = claimsExplorerPage.QueryBox.SendQuestionButton.Click<AiClaimsExplorerPage>();

            BrowserTabManager.Instance.OpenUrlInNewTab($"{AiAssistedResearchTab}Second", claimsExplorerLandingPageUrl);
            BrowserTabManager.Instance.SetTabActive($"{AiAssistedResearchTab}Second");

            claimsExplorerPage = claimsExplorerPage.QueryBox.QuestionTextbox.SetText<AiClaimsExplorerPage>(Question);
            claimsExplorerPage = claimsExplorerPage.QueryBox.SendQuestionButton.Click<AiClaimsExplorerPage>();

            this.TestCaseVerify.AreEqual(
                checkLimitConcurrentSearchesWarning,
                "You can submit this query after one of your last AI searches receives a response.",
                claimsExplorerPage.QueryBox.ConcurrentSearchesLimitInfobox.Text,
                "Limit concurrent searches warning message is NOT displayed for >1 parallel searches");
        }

        /// <summary>
        /// Verify 'Find Defenses' links behavior for 2K characters
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        [TestProperty(EnvironmentConstants.InfrastructureAccessControlsOn, "IAC-AI-CE-FIND-DEFENSES-QUERY-UPDATE")]
        public void AiClaimsExplorerFindDefensesLink2KTest()
        {
            const string Question2000Characters = "My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so.My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so.My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so.My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so.My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so.My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so.My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so.My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so.My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so.My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so.My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so.My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so.My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so.My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so.My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so.My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so.My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so. My employer refused to all";
            
            const string BuildedQuery2000CharactersActionableDataPattern = "I have a potential claim under {0} which is actionable under {1}. What are some potential defenses for this claim?";
            const string BuildedQuery2000CharactersNoActionableDataPattern = "I have a potential claim {0} {1}. What are some potential defenses for this claim?";
            const string BuildedQuery2000CharactersCommonLawPattern = "I have a potential claim for {0}. What are some potential defenses to this claim?";

            string checkStatutoryOver2000BuildedNoActionableDataQuery = "Verify: Statutory builded query without actionable data over 2000 characters is as expected";
            string checkActionableDataLinkOpensADocumentPage = "Verify: Actionable data link opens a document page";
            string checkStatutoryOver2000BuildedWithActionableDataQuery = "Verify: Statutory builded query with actionable data over 2000 characters is as expected";
            string checkCommonLawOver2000BuildedQuery = "Verify: Common Law builded query over 2000 characters is as expected";
            string checkConstitutionalOver2000BuildedQuery = "Verify: Constitutional builded query over 2000 characters is as expected";
            
            var claimsExplorerPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(ClaimsExplorerHeadingLabel).Click<AiClaimsExplorerPage>();
            BrowserPool.CurrentBrowser.CreateTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            var aiJurisdictionDialog = claimsExplorerPage.Toolbar.JurisdictionButton.Click<AiJurisdictionDialog>();
            aiJurisdictionDialog.SelectJurisdictionsClaimsExplorer(true, Jurisdiction.California).ClickSaveButtonClaimsExplorer();

            claimsExplorerPage = claimsExplorerPage.QueryBox.QuestionTextbox.SetText<AiClaimsExplorerPage>(Question2000Characters);
            claimsExplorerPage = claimsExplorerPage.QueryBox.SendQuestionButton.Click<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            SafeMethodExecutor.WaitUntil(() => claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.IsDisplayed(ClaimsExplorerAnswerTab.State));
            
            var stateTabComponent = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<StateTabComponent>(ClaimsExplorerAnswerTab.State);

            var itemNoActionableData = stateTabComponent.Headings.First(item => !item.ActionableDataLink.Displayed);
            var title = this.ExtractTitle(itemNoActionableData.SubHeadings.First().HeadingLabel.Text);
            var aiAssistantPage = itemNoActionableData.FindDefensesLink.Click<AiAssistedResearchPage>();

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            DriverExtensions.WaitForJavaScript();

            this.TestCaseVerify.IsTrue(
                checkStatutoryOver2000BuildedNoActionableDataQuery,
                this.ExtractQuestion(aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text).Equals(string.Format(BuildedQuery2000CharactersNoActionableDataPattern, "under", title)),
                "Statutory builded query without actionable data over 2000 characters is NOT as expected");

            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            BrowserPool.CurrentBrowser.CloseTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            DriverExtensions.WaitForJavaScript();

            var itemWithActionableData = stateTabComponent.Headings.First(item => item.ActionableDataLink.Displayed);
            var actionableDataTitle = itemWithActionableData.ActionableDataLink.Text;

            var documentPage = itemWithActionableData.ActionableDataLink.Click<EdgeCommonDocumentPage>();

            this.TestCaseVerify.IsTrue(
                checkActionableDataLinkOpensADocumentPage,
                documentPage.IsDocumentLoaded(),
                "Actionable data link doesn't open a document page");

            claimsExplorerPage = documentPage.FixedHeader.ClickReturnToListButton<AiClaimsExplorerPage>();

            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            stateTabComponent = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<StateTabComponent>(ClaimsExplorerAnswerTab.State);

            itemWithActionableData = stateTabComponent.Headings.First(item => item.ActionableDataLink.Displayed);
            title = this.ExtractTitle(itemWithActionableData.SubHeadings.First().HeadingLabel.Text);
            actionableDataTitle = itemWithActionableData.ActionableDataLink.Text;
            itemWithActionableData.FindDefensesLink.ScrollToElement();

            aiAssistantPage = itemWithActionableData.FindDefensesLink.Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            DriverExtensions.WaitForJavaScript();

            this.TestCaseVerify.IsTrue(
                checkStatutoryOver2000BuildedWithActionableDataQuery,
                this.ExtractQuestion(aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text).Equals(string.Format(BuildedQuery2000CharactersActionableDataPattern, title, actionableDataTitle)),
                "Statutory builded query with actionable data over 2000 characters is NOT as expected");

            BrowserPool.CurrentBrowser.CloseTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            DriverExtensions.WaitForJavaScript();

            title = this.ExtractTitle(stateTabComponent.Headings.First(item => item.HeadingAccordionButton.Text.Equals("Common law")).SubHeadings.First().HeadingLabel.Text);
            aiAssistantPage = stateTabComponent.Headings.First(item => item.HeadingAccordionButton.Text.Equals("Common law")).FindDefensesLink.Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            DriverExtensions.WaitForJavaScript();

            this.TestCaseVerify.IsTrue(
                checkCommonLawOver2000BuildedQuery,
                this.ExtractQuestion(aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text).Equals(string.Format(BuildedQuery2000CharactersCommonLawPattern, title)),
                "Common Law builded query over 2000 characters is NOT as expected");

            BrowserPool.CurrentBrowser.CloseTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            DriverExtensions.WaitForJavaScript();

            SafeMethodExecutor.WaitUntil(() => stateTabComponent.Headings.First(item => item.HeadingAccordionButton.Text.Equals("Constitutional")).SubHeadings.First().HeadingLabel.Displayed);

            title = this.ExtractTitle(stateTabComponent.Headings.First(item => item.HeadingAccordionButton.Text.Equals("Constitutional")).SubHeadings.First().HeadingLabel.Text);
            aiAssistantPage = stateTabComponent.Headings.First(item => item.HeadingAccordionButton.Text.Equals("Constitutional")).FindDefensesLink.Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            DriverExtensions.WaitForJavaScript();

            this.TestCaseVerify.IsTrue(
                checkConstitutionalOver2000BuildedQuery,
                this.ExtractQuestion(aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text).Equals(string.Format(BuildedQuery2000CharactersNoActionableDataPattern, "under", title)),
                "Constitutional builded query over 2000 characters is NOT as expected");
        }

        /// <summary>
        /// Verify 'Find Defenses' links behavior for query under 2K characters
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        [TestProperty(EnvironmentConstants.InfrastructureAccessControlsOn, "IAC-AI-CE-FIND-DEFENSES-QUERY-UPDATE")]
        public void AiClaimsExplorerFindDefensesLinkUnder2KTest()
        {
            const string QuestionUnder2000Characters = "My employer refused to allow me to serve as an election judge, and even docked my pay when I used vacation to do so.";
            const string BuildedQueryUnder2000CharactersActionableDataPattern = "I have a potential claim under {0} based on this fact pattern: {1}. What are some potential defenses based on these facts? {2} is actionable under {3}, so please also include any defenses based on {4}.";
            const string BuildedQueryUnder2000CharactersNoActionableDataPattern = "I have a potential claim {0} {1} based on this fact pattern: {2}. What are some potential defenses based on these facts?";
            
            string checkStatutoryUnder2000BuildedNoActionableDataQuery = "Verify: Statutory builded query without actionable data under 2000 characters is as expected";
            string checkStatutoryUnder2000BuildedWithActionableDataQuery = "Verify: Statutory builded query with actionable data under 2000 characters is as expected";
            string checkCommonLawUnder2000BuildedQuery = "Verify: Common Law builded query under 2000 characters is as expected";
            string checkConstitutionalUnder2000BuildedQuery = "Verify: Constitutional builded query under 2000 characters is as expected";
            string checkLimitConcurrentSearchesWarning = "Verify: Limit concurrent searches warning message is displayed for >3 parallel searches";

            var claimsExplorerPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(ClaimsExplorerHeadingLabel).Click<AiClaimsExplorerPage>();
            BrowserPool.CurrentBrowser.CreateTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            DriverExtensions.WaitForJavaScript();

            var aiJurisdictionDialog = claimsExplorerPage.Toolbar.JurisdictionButton.Click<AiJurisdictionDialog>();
            aiJurisdictionDialog.SelectJurisdictionsClaimsExplorer(true, Jurisdiction.California).ClickSaveButtonClaimsExplorer();

            claimsExplorerPage = claimsExplorerPage.Toolbar.NewResearchButton.Click<AiClaimsExplorerPage>();

            claimsExplorerPage = claimsExplorerPage.QueryBox.QuestionTextbox.SetText<AiClaimsExplorerPage>(QuestionUnder2000Characters);
            claimsExplorerPage = claimsExplorerPage.QueryBox.SendQuestionButton.Click<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            var stateTabComponent = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<StateTabComponent>(ClaimsExplorerAnswerTab.State);

            var itemNoActionableData = stateTabComponent.Headings.First(item => !item.ActionableDataLink.Displayed);
            var title = this.ExtractTitle(itemNoActionableData.SubHeadings.First().HeadingLabel.Text);
            var aiAssistantPage = itemNoActionableData.FindDefensesLink.Click<AiAssistedResearchPage>();

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            DriverExtensions.WaitForJavaScript();

            this.TestCaseVerify.IsTrue(
                checkStatutoryUnder2000BuildedNoActionableDataQuery,
                this.ExtractQuestion(aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text).Equals(string.Format(BuildedQueryUnder2000CharactersNoActionableDataPattern, "under", title, QuestionUnder2000Characters)),
                "Statutory builded query without actionable data under 2000 characters is NOT as expected");

            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            BrowserPool.CurrentBrowser.CloseTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            var itemWithActionableData = stateTabComponent.Headings.First(item => item.ActionableDataLink.Displayed);
            title = this.ExtractTitle(itemWithActionableData.SubHeadings.First().HeadingLabel.Text);
            var actionableDataTitle = itemWithActionableData.ActionableDataLink.Text;

            aiAssistantPage = itemWithActionableData.FindDefensesLink.Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            DriverExtensions.WaitForJavaScript();

            this.TestCaseVerify.IsTrue(
                checkStatutoryUnder2000BuildedWithActionableDataQuery,
                this.ExtractQuestion(aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text).Equals(string.Format(BuildedQueryUnder2000CharactersActionableDataPattern, title, QuestionUnder2000Characters, title, actionableDataTitle, actionableDataTitle)),
                "Statutory builded query with actionable data under 2000 characters is NOT as expected");

            BrowserPool.CurrentBrowser.CloseTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            title = this.ExtractTitle(stateTabComponent.Headings.First(item => item.HeadingAccordionButton.Text.Equals("Common law")).SubHeadings.First().HeadingLabel.Text);
            aiAssistantPage = stateTabComponent.Headings.First(item => item.HeadingAccordionButton.Text.Equals("Common law")).FindDefensesLink.Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            DriverExtensions.WaitForJavaScript();

            this.TestCaseVerify.IsTrue(
                checkCommonLawUnder2000BuildedQuery,
                this.ExtractQuestion(aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text).Equals(string.Format(BuildedQueryUnder2000CharactersNoActionableDataPattern, "for", title, QuestionUnder2000Characters)),
                "Common Law builded query under 2000 characters is NOT as expected");

            BrowserPool.CurrentBrowser.CloseTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            title = this.ExtractTitle(stateTabComponent.Headings.First(item => item.HeadingAccordionButton.Text.Equals("Constitutional")).SubHeadings.First().HeadingLabel.Text);
            aiAssistantPage = stateTabComponent.Headings.First(item => item.HeadingAccordionButton.Text.Equals("Constitutional")).FindDefensesLink.Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            DriverExtensions.WaitForJavaScript();

            this.TestCaseVerify.IsTrue(
                checkConstitutionalUnder2000BuildedQuery,
                this.ExtractQuestion(aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text).Equals(string.Format(BuildedQueryUnder2000CharactersNoActionableDataPattern, "under", title, QuestionUnder2000Characters)),
                "Constitutional builded query under 2000 characters is NOT as expected");

            BrowserPool.CurrentBrowser.CloseTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            aiAssistantPage = stateTabComponent.Headings.First().FindDefensesLink.Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            aiAssistantPage = stateTabComponent.Headings.ElementAt(1).FindDefensesLink.Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab($"{AiAssistedResearchTab}Second");
            BrowserPool.CurrentBrowser.ActivateTab($"{AiAssistedResearchTab}Second");
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            aiAssistantPage = stateTabComponent.Headings.ElementAt(2).FindDefensesLink.Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab($"{AiAssistedResearchTab}Third");
            BrowserPool.CurrentBrowser.ActivateTab($"{AiAssistedResearchTab}Third");
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            aiAssistantPage = stateTabComponent.Headings.ElementAt(3).FindDefensesLink.Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab($"{AiAssistedResearchTab}Fourth");
            BrowserPool.CurrentBrowser.ActivateTab($"{AiAssistedResearchTab}Fourth");
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            this.TestCaseVerify.AreEqual(
                checkLimitConcurrentSearchesWarning,
                "You can submit this query after one of your last AI searches receives a response.",
                stateTabComponent.Headings.ElementAt(3).FindDefensesConcurrentSearchesLimitInfobox.Text,
                "Limit concurrent searches warning message is NOT displayed for >3 parallel searches");
        }

        protected override void OnManageCredential()
        {
            if (this.TestContext.Properties["OutOfPlanUser"] != null && this.TestContext.Properties["OutOfPlanUser"].Equals("On"))
            {
                var userCredential = new UserDbCredential(
                    this.TestContext,
                    PasswordVertical.WlnGrowth,
                    "IndigoPremiumOOP")
                { ClientId = "OopAalpTest" };

                CredentialPool.RegisterUser(userCredential);

                this.DefaultSignOnContext = new WlnSignOnContext<IUserInfo>(this.TestExecutionContext, userCredential.ToWlnUserInfo());
            }
            else
            {
                base.OnManageCredential();
            }
        }

        protected override void InitializeRoutingPageSettings()
        {
            base.InitializeRoutingPageSettings();

            this.Settings.AppendValues(
                EnvironmentConstants.CategoryPageCollectionSet,
                SettingUpdateOption.Append,
                "w_cb_catpagesqa_cs");

            if (this.TestContext.Properties["ResearchDailyLimit"] != null && this.TestContext.Properties["ResearchDailyLimit"].Equals("1"))
            {
                this.Settings.AppendValues(
                    EnvironmentConstants.AIClaimsFinderLimit,
                    SettingUpdateOption.Append, 
                    "1");
            }
        }

        private string ExtractQuestion(string text) => text.Remove(0, text.IndexOf("says:") + 7);

        private string ExtractTitle(string text) => text.Substring(0, text.IndexOf("\r\n"));
    }
}

