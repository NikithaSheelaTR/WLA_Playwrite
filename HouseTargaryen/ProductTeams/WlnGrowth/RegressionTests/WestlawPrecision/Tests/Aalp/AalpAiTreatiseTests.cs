namespace WestlawPrecision.Tests.Aalp
{
    using Framework.Common.UI.Products.Shared.Dialogs.Delivery;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.NewTypeAhead;
    using Framework.Common.UI.Products.WestlawEdge.Pages.SearchResult.ContentTypeSearchResultPages;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.AALP;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
    using Framework.Common.UI.Products.WestLawNext.Enums.Delivery;
    using Framework.Common.UI.Raw.EnhancementTests.Utils;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Header;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.NewTypeAhead;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Toolbars;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Framework.Common.UI.Products.WestlawEdge.Components.Preferences;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.HomePage;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Preferences;
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using System.Globalization;
    using System.Data;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Foldering;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Folders;

    /// <summary>
    /// Treatise tests
    /// </summary>
    [TestClass]
    public class AalpAiTreatiseTests : AalpBaseTest
    {
        private const string FeatureTestCategory = "AiTreatise";

        private const string SearchQuery = "Civil Procedure Before Trial (The Rutter Group, California Practice Guide)";

        private const string RutterGroupContentName = "Rutter Group";

        private const string OConnorsContentName = "O'Connor's";

        private const string SearchAndSummarizeOConnors = "Search & Summarize O'Connor's";

        private const string OConnorsSearchQuery = "O'Connor's Texas Causes of Action";

        /// <summary>
        /// Test case #1878340, 1880523
        /// Verify delivery
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        [TestCategory(TeamMatzekAdvantageSmokeCategory)]
        public void AiTreatiseDeliveryTest()
        {
            const string Question = "Can parties orally stipulate to change the date of a deposition?";

            string checkContentEntries = "Verify: Content appears twice";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            homePage = homePage.Header.CompartmentDropdown.ArrowButton.Click<PrecisionHomePage>();

            homePage.Header.CompartmentDropdown.SelectedOptionLink.WaitDisplayed(500);

            var productName = homePage.Header.CompartmentDropdown.SelectedOptionLink.Text;

            var typeahead = homePage.Header.EnterSearchQuery<TrdTypeAheadDialog>(SearchQuery);
            var categoryPage = typeahead.OtherComponent.ClickOnSuggestionByIndex<EdgeContentTypeSearchResultPage>(NewTypeAheadCategories.ContentPages, 0);
            var aiAssistantPage = categoryPage.Toolbar.ClickToolbarElement<AiAssistedResearchPage>(EdgeToolbarElements.SearchAndSummarizeRutter);

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            //Delivery
            var downloadDialog = aiAssistantPage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);
            downloadDialog.LayoutAndLimitsTab.SetIncludeSectionOption(LayoutAndLimitsInclude.CoverPage);
            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<AiAssistedResearchPage>();
            var fileName = $"{productName} - Westlaw Search And Summarize Rutter - {DateTime.Now.ToString(DeliveryDateFormat)}.pdf";
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            var text = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, fileName));

            this.TestCaseVerify.AreEqual(
                checkContentEntries,
                2,
                text.Select((count, item) => text.Substring(item)).Count(sub => sub.StartsWith($"Content:  Rutter Group")),
                "Content doesn't appear twice");
        }

        /// <summary>
        /// Test case #1875311, 1878365, 1878094,2131639
        /// Task #2140432
        /// Verify history events
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        public void AiTreatiseHistoryEventTest()
        {
            const string Question = "What are various avenues to attack the plaintiff’s pleadings?";
            const string SearchType = "Search & Summarize";
            const string ConversationDateFormat = "MMM d, yyyy hh:mm tt";
            const string HistoryEventDateFormat = "M/d/yyyh:mm tt";

            string checkCategoryPageLabel = "Verify: Category page name is displayed instead of jurisdiction";
            string checkRutterGroupDisplay = "Verify: Rutter Group is displayed instead of jurisdiction";
            string checkContentOnHistoryLeftPane = "Verify: Category pane name is displayed for conversation on History left pane";
            string checkHistoryTab = "Verify: Category page name is displayed instead of jurisdiction on the History tab";
            string checkHistoryTabLeadsToAiResearch = "Verify: Category page event from the History tab leads to AI-Assisted Research page";
            string checkHistoryPage = "Verify: Category page name is displayed instead of jurisdiction on the History page";
            string checkHistoryPageLeadsToAiResearch = "Verify: Category page event from the History page leads to AI-Assisted Research page";
            string checkDelivery = "Verify: Category page name is displayed instead of jurisdiction on the Delivered history";
            string checkHistoryPageViaGoToFullHistory = "Verify: Category page name is displayed instead of jurisdiction on the History page (via 'Go to full history' link)";
            string checkGoToFullistoryPageLeadsToAiResearch = "Verify: Category page event from the History page (via 'Go to full history' link) leads to AI-Assisted Research page";
            string checkSignOffPage = "Verify: Category page name is displayed instead of jurisdiction on the Sign off page";
            string checkClientIdPage = "Verify: Category page name is displayed instead of jurisdiction on the Client Id page";
            string checkClientIdEventClickLeadsToAiResearch = "Verify: Category page event from the Client ID page leads to AI-Assisted Research page";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            homePage = homePage.Header.CompartmentDropdown.ArrowButton.Click<PrecisionHomePage>();

            var productName = homePage.Header.CompartmentDropdown.SelectedOptionLink.Text;

            var typeahead = homePage.Header.EnterSearchQuery<TrdTypeAheadDialog>(SearchQuery);
            var categoryPage = typeahead.OtherComponent.ClickOnSuggestionByIndex<EdgeContentTypeSearchResultPage>(NewTypeAheadCategories.ContentPages, 0);
            var aiAssistantPage = categoryPage.Toolbar.ClickToolbarElement<AiAssistedResearchPage>(EdgeToolbarElements.SearchAndSummarizeRutter);

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            this.TestCaseVerify.IsTrue(
                checkCategoryPageLabel,
                aiAssistantPage.Toolbar.CategoryPageLabel.Text.Replace("Content: ", string.Empty).Equals(RutterGroupContentName)
                && !aiAssistantPage.Toolbar.JurisdictionButton.Displayed,
                "Category page name is NOT displayed instead of jurisdiction");

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            var folderName = $"{this.GetUserInfo().FirstName}'s Research";

            var saveToFolderDialog = aiAssistantPage.Toolbar.SaveToFolderButton.Click<EdgeSaveToFolderDialog>();
            saveToFolderDialog.FolderTreeComponent.SelectFolderByName(folderName);
            aiAssistantPage = saveToFolderDialog.ClickSaveButton<AiAssistedResearchPage>();

            var folderingPopUp = aiAssistantPage.Header.ClickHeaderTab<EdgeRecentFoldersDialog>(EdgeHeaderTabs.Folders).ClickFolderByName(folderName);

            this.TestCaseVerify.IsTrue(
                checkRutterGroupDisplay,
                folderingPopUp.RecentFolderDocumentItem.First(item => item.TitleLink.Text.Equals(Question)).IsTextPresented(RutterGroupContentName),
                "Jurisdiction is displayed instead of Rutter Group");

            var folderPage = folderingPopUp.ViewAllLink.Click<EdgeResearchOrganizerPage>();
            folderPage = folderPage.FolderGrid.FolderGridItems[Question].ActionsMenu.SelectOption<EdgeResearchOrganizerPage>(ActionsMenuOption.Delete);

            aiAssistantPage = BrowserPool.CurrentBrowser.GoBack<AiAssistedResearchPage>();
            aiAssistantPage = BrowserPool.CurrentBrowser.GoBack<AiAssistedResearchPage>();

            aiAssistantPage = aiAssistantPage.ConversationHistory.ExpandButton.Click<AiAssistedResearchPage>();

            this.TestCaseVerify.IsTrue(
                checkContentOnHistoryLeftPane,
                aiAssistantPage.ConversationHistory.Conversations.First().ContentLabel.Text.Equals($"Content: {RutterGroupContentName}"),
                "Category pane name is NOT displayed for conversation on History left pane");

            // History tab
            var recentHistoryDialog = aiAssistantPage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);
            SafeMethodExecutor.WaitUntil(() => recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemLink.Displayed);

            var conversationDate = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ConversationDateLabel.Text;
            var formattedConversationDate = DateTime.ParseExact(conversationDate, ConversationDateFormat, CultureInfo.InvariantCulture);

            if (!recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemDateLabel.Text.Equals(formattedConversationDate))
            {
                SafeMethodExecutor.WaitUntil(() =>
                {
                    aiAssistantPage = BrowserPool.CurrentBrowser.Refresh<AiAssistedResearchPage>();

                    conversationDate = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ConversationDateLabel.Text;
                    formattedConversationDate = DateTime.ParseExact(conversationDate, ConversationDateFormat, CultureInfo.InvariantCulture);

                    recentHistoryDialog = aiAssistantPage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);

                    var historyEventDate = recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemDateLabel.Text;
                    var upperBoundHistoryEventDate = formattedConversationDate.AddMinutes(1).ToString(HistoryEventDateFormat);

                    return historyEventDate.Equals(formattedConversationDate.ToString(HistoryEventDateFormat)) || historyEventDate.Equals(upperBoundHistoryEventDate);
                }, timeoutFromSec: 60, pollingIntervalInMilliseconds: 2000);
            }

            this.TestCaseVerify.IsTrue(
                checkHistoryTab,
                recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemLink.Text.Equals(Question)
                && recentHistoryDialog.GetRecentSearchesItems().First().MetaData.Contains($"{SearchType}{RutterGroupContentName}"),
                "Category page name is NOT displayed instead of jurisdiction on the History tab");

            aiAssistantPage = recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemLink.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkHistoryTabLeadsToAiResearch,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.Remove(0, aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.IndexOf("says:") + 7).Equals(Question)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Count.Equals(1)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed
                && aiAssistantPage.Toolbar.CategoryPageLabel.Text.Replace("Content: ", string.Empty).Equals(RutterGroupContentName),
                "Category page event from the History tab doesn't lead to AI-Assisted Research page");

            // History page
            recentHistoryDialog = aiAssistantPage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);
            var historyPage = recentHistoryDialog.ViewAllLink.Click<EdgeCommonHistoryPage>();

            this.TestCaseVerify.IsTrue(
                checkHistoryPage,
                historyPage.HistoryTable.GetGridItems().First().Title.Equals(Question)
                && historyPage.HistoryTable.GetGridItems().First().Summary.Equals($"{SearchType}{RutterGroupContentName}"),
                "Category page name is NOT displayed instead of jurisdiction on the History page");

            // Delivery
            var historyEventsCount = historyPage.HistoryTable.GetGridItems().Count.ToString();

            var downloadDialog = historyPage.EdgeToolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);

            downloadDialog.TheBasicsTab.NumberToDeliver.SelectOption<DownloadDialog>(downloadDialog.TheBasicsTab.NumberToDeliver.Options.FirstOrDefault(option => option.Contains(historyEventsCount)));

            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<PrecisionCommonSearchResultPage>();

            var fileName = historyEventsCount.Equals(1) ? $"{productName} - List of {historyEventsCount} item from {this.GetUserInfo().FirstName} {this.GetUserInfo().LastName} All History.pdf" : $"{productName} - List of {historyEventsCount} items from {this.GetUserInfo().FirstName} {this.GetUserInfo().LastName} All History.pdf";

            FileUtil.WaitForFileDownload(this.FolderToSave, fileName);

            var text = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(this.FolderToSave, fileName)).Replace("\r\n", string.Empty).Replace(" ", string.Empty);

            this.TestCaseVerify.IsTrue(
                checkDelivery,
                text.Contains($"{Question}{SearchType}{RutterGroupContentName}".Replace("\r\n", string.Empty).Replace(" ", string.Empty)),
                "Category page name is NOT displayed instead of jurisdiction on the Delivered history");

            aiAssistantPage = historyPage.HistoryTable.GetGridItems().First().ClickLinkByText<AiAssistedResearchPage>(historyPage.HistoryTable.GetGridItems().First().Title);
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkHistoryPageLeadsToAiResearch,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.Remove(0, aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.IndexOf("says:") + 7).Equals(Question)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Count.Equals(1)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed
                && aiAssistantPage.Toolbar.CategoryPageLabel.Text.Replace("Content: ", string.Empty).Equals(RutterGroupContentName),
                "Category page event from the History page doesn't lead to AI-Assisted Research page");

            // History page via "Go to full history" link
            aiAssistantPage = aiAssistantPage.ConversationHistory.ExpandButton.Click<AiAssistedResearchPage>();

            historyPage = aiAssistantPage.ConversationHistory.GoToFullHistoryLink.Click<EdgeCommonHistoryPage>();

            BrowserPool.CurrentBrowser.CreateTab(HistoryPageTab);
            BrowserPool.CurrentBrowser.ActivateTab(HistoryPageTab);

            SafeMethodExecutor.WaitUntil(() => historyPage.EdgeToolbar.DeliveryDropdown.IsDisplayed());

            this.TestCaseVerify.IsTrue(
                checkHistoryPageViaGoToFullHistory,
                historyPage.HistoryTable.GetGridItems().First().Title.Equals(Question)
                && historyPage.HistoryTable.GetGridItems().First().Summary.Equals($"{SearchType}{RutterGroupContentName}"),
                "Category page name is NOT displayed instead of jurisdiction on the History page (via 'Go to full history' link)");

            aiAssistantPage = historyPage.HistoryTable.GetGridItems().First().ClickLinkByText<AiAssistedResearchPage>(historyPage.HistoryTable.GetGridItems().First().Title);
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkGoToFullistoryPageLeadsToAiResearch,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.Remove(0, aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.IndexOf("says:") + 7).Equals(Question)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Count.Equals(1)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed
                && aiAssistantPage.Toolbar.CategoryPageLabel.Text.Replace("Content: ", string.Empty).Equals(RutterGroupContentName),
                "Category page event from the History page (via 'Go to full history' link) doesn't lead to AI-Assisted Research page");

            //Sign off page
            var signOffPage = historyPage.Header.ClickHeaderTab<EdgeProfileSettingsDialog>(EdgeHeaderTabs.PreferencesAndSignOut).ClickSignOff();
            var lastEventTitle = signOffPage.SignOffSessionDetailsComponent.GetSessionItems().First().GetDescriptionText().Replace("\r\n", string.Empty);

            this.TestCaseVerify.IsTrue(
                checkSignOffPage,
                lastEventTitle.Contains($"{Question}{SearchType}{RutterGroupContentName}"),
                "Category page name is NOT  instead of jurisdiction on the Sign off page");

            //Client ID page
            var clientIdPage = this.SignOnBack();
            lastEventTitle = clientIdPage.RecentResearchPane.RecentResearchList.First().Text.Replace(" ", string.Empty);

            this.TestCaseVerify.IsTrue(
                checkClientIdPage,
                lastEventTitle.Replace("\r\n", string.Empty).Replace(" ", string.Empty).Contains($"{Question}{SearchType}{RutterGroupContentName}".Replace(" ", string.Empty)),
                "Category page name is NOT displayed instead of jurisdiction on the Client Id page");

            aiAssistantPage = clientIdPage.RecentResearchPane.RecentResearchList.First().ClickTitleLink<AiAssistedResearchPage>();

            this.TestCaseVerify.IsTrue(
                checkClientIdEventClickLeadsToAiResearch,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.Remove(0, aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.IndexOf("says:") + 7).Equals(Question)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Count.Equals(1)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed
                && aiAssistantPage.Toolbar.CategoryPageLabel.Text.Replace("Content: ", string.Empty).Equals(RutterGroupContentName),
                "Category page event from the Client ID page doesn't lead to AI-Assisted Research page");
        }

        /// <summary>
        /// Test case #1875311, 1878365, 1878094, 2119016
        /// Verify Rutter Group history events
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        public void AiTreatiseRutterGroupHistoryEventTest()
        {
            const string FirstQuestion = "Can parties orally stipulate to change the date of a deposition?";
            const string SecondQuestion = "What are the pros and cons of Civil Procedure Rutter Group?";
            const string SearchType = "Search & Summarize";
            const string ConversationDateFormat = "MMM d, yyyy hh:mm tt";
            const string HistoryEventDateFormat = "M/d/yyyh:mm tt";

            string checkCategoryPageLabel = "Verify: Category page name is displayed instead of jurisdiction";
            string checkContentOnHistoryLeftPane = "Verify: Content pane name is displayed as 'Rutter Group' for conversations on History left pane";
            string checkHistoryTab = "Verify: Category page name is displayed instead of jurisdiction on the History tab";
            string checkHistoryTabLeadsToAiResearch = "Verify: Category page event from the History tab leads to AI-Assisted Research page";
            string checkHistoryPage = "Verify: Category page name is displayed instead of jurisdiction on the History page";
            string checkHistoryPageLeadsToAiResearch = "Verify: Category page event from the History page leads to AI-Assisted Research page";
            string checkDelivery = "Verify: Category page name is displayed instead of jurisdiction on the Delivered history";
            string checkHistoryPageViaGoToFullHistory = "Verify: Category page name is displayed instead of jurisdiction on the History page (via 'Go to full history' link)";
            string checkGoToFullistoryPageLeadsToAiResearch = "Verify: Category page event from the History page (via 'Go to full history' link) leads to AI-Assisted Research page";
            string checkSignOffPage = "Verify: Category page name is displayed instead of jurisdiction on the Sign off page";
            string checkClientIdPage = "Verify: Category page name is displayed instead of jurisdiction on the Client Id page";
            string checkClientIdEventClickLeadsToAiResearch = "Verify: Category page event from the Client ID page leads to AI-Assisted Research page";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            homePage = homePage.Header.CompartmentDropdown.ArrowButton.Click<PrecisionHomePage>();

            var productName = homePage.Header.CompartmentDropdown.SelectedOptionLink.Text;

            var browsePage = homePage.BrowseTabPanel.SetActiveTab<AllContentTabPanel>(PrecisionBrowseTab.ContentTypes)
                .ClickBrowseCategory<EdgeContentTypeSearchResultPage>(ContentType.SecondarySources)
                .ClickLinkByText<EdgeContentTypeSearchResultPage>("Rutter Group")
                .ClickLinkByText<EdgeContentTypeSearchResultPage>(SearchQuery);

            var aiAssistantPage = browsePage.Toolbar.ClickToolbarElement<AiAssistedResearchPage>(EdgeToolbarElements.SearchAndSummarizeRutter);

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            this.TestCaseVerify.IsTrue(
                checkCategoryPageLabel,
                aiAssistantPage.Toolbar.CategoryPageLabel.Text.Replace("Content: ", string.Empty).Equals(RutterGroupContentName)
                && !aiAssistantPage.Toolbar.JurisdictionButton.Displayed,
                "Category page name is NOT displayed instead of jurisdiction");

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(FirstQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage = aiAssistantPage.Toolbar.NewResearchButton.Click<AiAssistedResearchPage>();

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(SecondQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage = aiAssistantPage.ConversationHistory.ExpandButton.Click<AiAssistedResearchPage>();

            this.TestCaseVerify.IsTrue(
                checkContentOnHistoryLeftPane,
                aiAssistantPage.ConversationHistory.Conversations.All(historyEvent => historyEvent.ContentLabel.Text.Contains($"Content: {RutterGroupContentName}")),
                "Content pane name on the left history pane is not displayed as expected");

            // History tab
            var recentHistoryDialog = aiAssistantPage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);
            SafeMethodExecutor.WaitUntil(() => recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemLink.Displayed);

            var conversationDate = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ConversationDateLabel.Text;
            var formattedConversationDate = DateTime.ParseExact(conversationDate, ConversationDateFormat, CultureInfo.InvariantCulture);

            if (!recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemDateLabel.Text.Equals(formattedConversationDate))
            {
                SafeMethodExecutor.WaitUntil(() =>
                {
                    aiAssistantPage = BrowserPool.CurrentBrowser.Refresh<AiAssistedResearchPage>();

                    conversationDate = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ConversationDateLabel.Text;
                    formattedConversationDate = DateTime.ParseExact(conversationDate, ConversationDateFormat, CultureInfo.InvariantCulture);

                    recentHistoryDialog = aiAssistantPage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);

                    var historyEventDate = recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemDateLabel.Text;
                    var upperBoundHistoryEventDate = formattedConversationDate.AddMinutes(1).ToString(HistoryEventDateFormat);

                    return historyEventDate.Equals(formattedConversationDate.ToString(HistoryEventDateFormat)) || historyEventDate.Equals(upperBoundHistoryEventDate);
                }, timeoutFromSec: 60, pollingIntervalInMilliseconds: 2000);
            }

            this.TestCaseVerify.IsTrue(
                checkHistoryTab,
                recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemLink.Text.Equals(SecondQuestion)
                && recentHistoryDialog.GetRecentSearchesItems().First().MetaData.Contains($"{SearchType}{RutterGroupContentName}"),
                "Category page name is NOT displayed instead of jurisdiction on the History tab");

            aiAssistantPage = recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemLink.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkHistoryTabLeadsToAiResearch,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.Remove(0, aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.IndexOf("says:") + 7).Equals(SecondQuestion)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Count.Equals(1)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed
                && aiAssistantPage.Toolbar.CategoryPageLabel.Text.Replace("Content: ", string.Empty).Equals(RutterGroupContentName),
                "Category page event from the History tab doesn't lead to AI-Assisted Research page");

            // History page
            recentHistoryDialog = aiAssistantPage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);
            var historyPage = recentHistoryDialog.ViewAllLink.Click<EdgeCommonHistoryPage>();

            this.TestCaseVerify.IsTrue(
                checkHistoryPage,
                historyPage.HistoryTable.GetGridItems().First().Title.Equals(SecondQuestion)
                && historyPage.HistoryTable.GetGridItems().First().Summary.Equals($"{SearchType}{RutterGroupContentName}")
                && historyPage.HistoryTable.GetGridItems().ElementAt(1).Title.Equals(FirstQuestion)
                && historyPage.HistoryTable.GetGridItems().ElementAt(1).Summary.Equals($"{SearchType}{RutterGroupContentName}"),
                "Category page name is NOT displayed instead of jurisdiction on the History page");

            // Delivery
            var historyEventsCount = historyPage.HistoryTable.GetGridItems().Count.ToString();

            var downloadDialog = historyPage.EdgeToolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);

            downloadDialog.TheBasicsTab.NumberToDeliver.SelectOption<DownloadDialog>(downloadDialog.TheBasicsTab.NumberToDeliver.Options.FirstOrDefault(option => option.Contains(historyEventsCount)));

            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<PrecisionCommonSearchResultPage>();

            var fileName = historyEventsCount.Equals(1) ? $"{productName} - List of {historyEventsCount} item from {GetUserInfo().FirstName} {GetUserInfo().LastName} All History.pdf" : $"{productName} - List of {historyEventsCount} items from {GetUserInfo().FirstName} {GetUserInfo().LastName} All History.pdf";

            FileUtil.WaitForFileDownload(this.FolderToSave, fileName);

            var text = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(this.FolderToSave, fileName)).Replace("\r\n", string.Empty).Replace(" ", string.Empty);

            this.TestCaseVerify.IsTrue(
                checkDelivery,
                text.Contains($"{SecondQuestion}{SearchType}{RutterGroupContentName}".Replace(" ", string.Empty)),
                "Category page name is NOT displayed instead of jurisdiction on the Delivered history");

            aiAssistantPage = historyPage.HistoryTable.GetGridItems().First().ClickLinkByText<AiAssistedResearchPage>(historyPage.HistoryTable.GetGridItems().First().Title);
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkHistoryPageLeadsToAiResearch,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.Remove(0, aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.IndexOf("says:") + 7).Equals(SecondQuestion)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Count.Equals(1)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed
                && aiAssistantPage.Toolbar.CategoryPageLabel.Text.Replace("Content: ", string.Empty).Equals(RutterGroupContentName),
                "Category page event from the History page doesn't lead to AI-Assisted Research page");

            // History page via "Go to full history" link
            aiAssistantPage = aiAssistantPage.ConversationHistory.ExpandButton.Click<AiAssistedResearchPage>();

            historyPage = aiAssistantPage.ConversationHistory.GoToFullHistoryLink.Click<EdgeCommonHistoryPage>();

            BrowserPool.CurrentBrowser.CreateTab(HistoryPageTab);
            BrowserPool.CurrentBrowser.ActivateTab(HistoryPageTab);

            SafeMethodExecutor.WaitUntil(() => historyPage.EdgeToolbar.DeliveryDropdown.IsDisplayed());

            this.TestCaseVerify.IsTrue(
                checkHistoryPageViaGoToFullHistory,
                historyPage.HistoryTable.GetGridItems().First().Title.Equals(SecondQuestion)
                && historyPage.HistoryTable.GetGridItems().First().Summary.Equals($"{SearchType}{RutterGroupContentName}"),
                "Category page name is NOT displayed instead of jurisdiction on the History page (via 'Go to full history' link)");

            aiAssistantPage = historyPage.HistoryTable.GetGridItems().First().ClickLinkByText<AiAssistedResearchPage>(historyPage.HistoryTable.GetGridItems().First().Title);
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkGoToFullistoryPageLeadsToAiResearch,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.Remove(0, aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.IndexOf("says:") + 7).Equals(SecondQuestion)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Count.Equals(1)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed
                && aiAssistantPage.Toolbar.CategoryPageLabel.Text.Replace("Content: ", string.Empty).Equals(RutterGroupContentName),
                "Category page event from the History page (via 'Go to full history' link) doesn't lead to AI-Assisted Research page");

            //Sign off page
            var signOffPage = historyPage.Header.ClickHeaderTab<EdgeProfileSettingsDialog>(EdgeHeaderTabs.PreferencesAndSignOut).ClickSignOff();
            var lastEventTitle = signOffPage.SignOffSessionDetailsComponent.GetSessionItems().First().GetDescriptionText().Replace("\r\n", string.Empty);

            this.TestCaseVerify.IsTrue(
                checkSignOffPage,
                lastEventTitle.Contains($"{SecondQuestion}{SearchType}{RutterGroupContentName}"),
                "Category page name is NOT  instead of jurisdiction on the Sign off page");

            //Client ID page
            var clientIdPage = this.SignOnBack();
            lastEventTitle = clientIdPage.RecentResearchPane.RecentResearchList.First().Text.Replace(" ", string.Empty);

            this.TestCaseVerify.IsTrue(
                checkClientIdPage,
                lastEventTitle.Replace("\r\n", string.Empty).Replace(" ", string.Empty).Contains($"{SecondQuestion}{SearchType}{RutterGroupContentName}".Replace(" ", string.Empty)),
                "Category page name is NOT displayed instead of jurisdiction on the Client Id page");

            aiAssistantPage = clientIdPage.RecentResearchPane.RecentResearchList.First().ClickTitleLink<AiAssistedResearchPage>();

            this.TestCaseVerify.IsTrue(
                checkClientIdEventClickLeadsToAiResearch,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.Remove(0, aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.IndexOf("says:") + 7).Equals(SecondQuestion)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Count.Equals(1)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed
                && aiAssistantPage.Toolbar.CategoryPageLabel.Text.Replace("Content: ", string.Empty).Equals(RutterGroupContentName),
                "Category page event from the Client ID page doesn't lead to AI-Assisted Research page");
        }

        /// <summary>
        /// Test case: 1883411, 2116345
        /// Verify supporting materials
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        public void AiTreatiseSupportingMaterialsTest()
        {
            const string Question = "May interrogatories be served on a non-party?";
            const string FollowUpQuestion = "Can a non-party be deposed?";
            const string ContentType = "The Rutter Group California Practice Guide: Civil Procedure Before Trial";
            const string ExpectedSystemMessage = "Generated by AI. Not legal or tax advice. A qualified professional must verify accuracy and legal compliance.";

            string checkPageHeading = "Verify: Page heading is correct";
            string checkMainAnswerTreatiseContent = "Verify: Main answer consists of the Treatises content";
            string checkFollowUpAnswerTreatiseContent = "Verify: Follow-up answer consists of the Treatises content";
            string checkNewDisclaimerMessage = "Verify: New disclaimer system message displayed";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            var typeahead = homePage.Header.EnterSearchQuery<TrdTypeAheadDialog>(SearchQuery);
            var categoryPage = typeahead.OtherComponent.ClickOnSuggestionByIndex<EdgeContentTypeSearchResultPage>(NewTypeAheadCategories.ContentPages, 0);
            var aiAssistantPage = categoryPage.Toolbar.ClickToolbarElement<AiAssistedResearchPage>(EdgeToolbarElements.SearchAndSummarizeRutter);

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            this.TestCaseVerify.IsTrue(
               checkPageHeading,
               aiAssistantPage.Toolbar.HeadingLabel.Text.Equals("Search & Summarize Rutter")
               && aiAssistantPage.Chat.LandingPageLabel.Text.Equals($"Jump-start your work with Search & Summarize Rutter\r\nWelcome, {CurrentUser.UserName}\r\nAsk a legal research question just the way you think about it, as if you were giving instructions to a colleague. Please see our tips for best results for questions this feature does not yet support and suggestions for improving results.\r\nSearch & Summarize Rutter uses generative AI and can occasionally produce inaccuracies, so it should always be used as part of a research process in connection with additional research where primary sources are checked to fully understand the nuance of the issues and further improve accuracy.\r\nRead how the AI works and get tips for best results."),
               "Heading is NOT correct");

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkNewDisclaimerMessage,
                aiAssistantPage.Chat.ChatSummaryLabel.Text.Contains(ExpectedSystemMessage),
                "New disclaimer system message not displayed");

            this.TestCaseVerify.IsTrue(
                checkMainAnswerTreatiseContent,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SupportingMaterialsItems.All(item => item.MetadataLabel.Text.Contains(ContentType)),
                "Main answer doesn't consist of the Treatises content");

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(FollowUpQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkFollowUpAnswerTreatiseContent,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).SupportingMaterials.SupportingMaterialsItems.All(item => item.MetadataLabel.Text.Contains(ContentType)),
                "Follow-up answer doesn't consist of the Treatises content");
        }

        /// <summary>
        /// Test case: 2113532,2113554
        /// Verify Inline titles and cites are displayed
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        public void AiTreatiseInlineTitlesAndCitesTest()
        {
            const string Question = "Can parties orally stipulate to change the date of a deposition?";

            string checkContentLabelText = "Verify content label text";
            string checkInlineTitleLink = "Verify: Inline title opens document page";
            string checkIncludeCitationsIsSelected = "Verify: Include citations checkbox is selected in Delivery modal if it is on in Profile preferences";
            string checkInlineCitesInDelivery = "Verify: Inline cites are present in delivery";
            string checkCheckboxForFollowUpQuestion = "Verify: Show inline titles checkbox is displayed for follow-up question";
            string checkInlineCitesAreNotPresentInDelivery = "Verify: Inline cites are not present in delivered document";
            string checkCheckboxOffState = "Verify: Show inline titles aren't displayed when feature is turned off";
            string checkIncludeCitationsIsDeselected = "Verify: Include citations checkbox is deselected in Delivery modal if it is off in Profile preferences";
            string checkInlineCitesAreNotDisplayedInDelivery = "Verify: Inline cites are not displayed in delivered document";
            //string checkContentEntriesOnAiAssistedResearchPage = "Verify: Content: rutter group appears twice for Treatise item";

            // 1. Login to WPA
            var homePage = this.GetHomePage<PrecisionHomePage>();

            //Turn on citations in Profile preferences
            var preferencesDialog = homePage.Header.ClickHeaderTab<EdgeProfileSettingsDialog>(EdgeHeaderTabs.PreferencesAndSignOut).ClickWestlawPreferences<EdgePreferencesDialog>();
            var featuresTab = preferencesDialog.TabPanel.SetActiveTab<EdgeFeaturesTabComponent>(EdgePreferencesDialogTabs.Features);

            if (!featuresTab.IsCheckboxSelected(EdgeFeaturesTab.AiAssistedResearchCitations))
            {
                featuresTab.SetFeature(EdgeFeaturesTab.AiAssistedResearchCitations, true);
            }

            homePage = preferencesDialog.SaveButton.Click<PrecisionHomePage>();
            homePage = homePage.Header.OpenJurisdictionDialog().SelectDefaultJurisdiction().SaveButton.Click<PrecisionHomePage>();

            //2. Click on Content Types and Secondary sources, Rutter Group and select the "Search Query"
            var browsePage = homePage.BrowseTabPanel.SetActiveTab<AllContentTabPanel>(PrecisionBrowseTab.ContentTypes)
                .ClickBrowseCategory<EdgeContentTypeSearchResultPage>(ContentType.SecondarySources)
                .ClickLinkByText<EdgeContentTypeSearchResultPage>(RutterGroupContentName)
                .ClickLinkByText<EdgeContentTypeSearchResultPage>(SearchQuery);

            var aiAssistantPage = browsePage.Toolbar.ClickToolbarElement<AiAssistedResearchPage>(EdgeToolbarElements.SearchAndSummarizeRutter);

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                 checkContentLabelText,
                 aiAssistantPage.Toolbar.CategoryPageLabel.Text.Equals($"Content: {RutterGroupContentName}"),
                 "Content label text is not correct");

            var documentPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().InlineTitlesLinks.First().Click<PrecisionCommonDocumentPage>();

            this.TestCaseVerify.IsTrue(
                checkInlineTitleLink,
                documentPage.IsDocumentLoaded(),
                "Inline title doesn't open document page");

            aiAssistantPage = BrowserPool.CurrentBrowser.GoBack<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed);

            //Delivery, checkbox is selected 
            var answerLabel = this.CleanTextForCompare(aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Text);

            var downloadDialog = aiAssistantPage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);

            //Verify whether citation checkbox is checked in Download
            this.TestCaseVerify.IsTrue(
                checkIncludeCitationsIsSelected,
                downloadDialog.LayoutAndLimitsTab.IsIncludeSectionOptionSelected(LayoutAndLimitsInclude.IncludeCitationsInTheResponse),
                "Include citations checkbox is not selected");

            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<AiAssistedResearchPage>();
            var fileName = $"Westlaw Precision - Westlaw Search And Summarize Rutter - {DateTime.Now.ToString(DeliveryDateFormat)}.pdf";
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            var text = this.CleanTextForCompare(PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, fileName)));

            this.TestCaseVerify.IsTrue(
                checkInlineCitesInDelivery,
                text.Contains(answerLabel),
                "Inline cites are missing in delivery");

            // Bug 2121833
            //this.TestCaseVerify.AreEqual(
            //     checkContentEntriesOnAiAssistedResearchPage,
            //     2,
            //     text.Select((count, item) => text.Substring(item)).Count(sub => sub.StartsWith($"Content:{RutterGroupContentName.Replace(" ", string.Empty)}")),
            //     "Content doesn't appear twice for Treatise item");

            //Ask follow-up question
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                 checkCheckboxForFollowUpQuestion,
                 aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).InlineTitlesLinks.Any(),
                 "Show inline titles checkbox is NOT displayed for follow-up question");

            //Delivery, deselect the Include citation checkbox          
            downloadDialog = aiAssistantPage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);
            downloadDialog.LayoutAndLimitsTab.SetIncludeSectionOption(LayoutAndLimitsInclude.IncludeCitationsInTheResponse, false);
            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<AiAssistedResearchPage>();
            fileName = $"Westlaw Precision - Westlaw Search And Summarize Rutter - {DateTime.Now.ToString(DeliveryDateFormat)} (1).pdf";
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            text = this.CleanTextForCompare(PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, fileName)));

            this.TestCaseVerify.IsFalse(
                checkInlineCitesAreNotPresentInDelivery,
                text.Contains(this.CleanTextForCompare(answerLabel)),
                "Inline cites are still present in delivery");

            //Turn off citations in Profile preferences
            preferencesDialog = aiAssistantPage.Header.ClickHeaderTab<EdgeProfileSettingsDialog>(EdgeHeaderTabs.PreferencesAndSignOut).ClickWestlawPreferences<EdgePreferencesDialog>();
            featuresTab = preferencesDialog.TabPanel.SetActiveTab<EdgeFeaturesTabComponent>(EdgePreferencesDialogTabs.Features);
            featuresTab.SetFeature(EdgeFeaturesTab.AiAssistedResearchCitations, false);
            aiAssistantPage = preferencesDialog.SaveButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ExpandButton.Click<AiAssistedResearchPage>();

            var isInlineTitlesMainQuestion = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().InlineTitlesLinks.Any();

            aiAssistantPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().CollapseButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).ExpandButton.Click<AiAssistedResearchPage>();

            this.TestCaseVerify.IsFalse(
                checkCheckboxOffState,
                isInlineTitlesMainQuestion
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).InlineTitlesLinks.Any(),
                "Show inline titles are displayed when feature is turned off");

            //Delivery, citation checkbox is deselected
            answerLabel = this.CleanTextForCompare(aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).AnswerLabel.Text);

            downloadDialog = aiAssistantPage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);

            this.TestCaseVerify.IsFalse(
                checkIncludeCitationsIsDeselected,
                downloadDialog.LayoutAndLimitsTab.IsIncludeSectionOptionSelected(LayoutAndLimitsInclude.IncludeCitationsInTheResponse),
                "Include citations checkbox is selected");

            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<AiAssistedResearchPage>();
            fileName = $"Westlaw Precision - Westlaw Search And Summarize Rutter - {DateTime.Now.ToString(DeliveryDateFormat)} (2).pdf";
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            text = this.CleanTextForCompare(PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, fileName)));

            this.TestCaseVerify.IsTrue(
                checkInlineCitesAreNotDisplayedInDelivery,
                text.Contains(this.CleanTextForCompare(answerLabel)),
                "Inline cites are still present in delivery");
        }

        /// <summary>
        /// Test case: 2118136
        /// Verify AI model text appears as expected for RutterGroup
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        public void AITreatiseRutterGroupCommonTest()
        {
            const string HowAiWorksDialogHeader = "How Search & Summarize Rutter works";
            const string HowAIWorksDialogDescription = "Search&SummarizeRutteruseslargelanguagemodels-atypeofgenerativeAI-andfocusesthemodelsonlyonthelanguageoftheRutterGroupPracticeGuides.PleasenotethatthreeRutterGroupPracticeGuideswillnotbeusedbythemodelsorincludedinaresponse:1)CaliforniaLaw&MotionModelForms,2)LPILegalProfessional'sHandbook,and3)PublicSectorEmploymentLitigation.PortionsfromRutterGroupPracticeGuidesareusedintheresponsesandmayincludetheactuallanguagefromthesourceorreferencestoprimarylaw.Linksareincludedtoreadthefulltextofthesourcedocuments.Evenwiththeseandotherprecautions,Search&SummarizeRuttercanoccasionallyproduceinaccuracies,soitshouldalwaysbeusedaspartofaresearchprocessinconnectionwithadditionalresearchtofullyunderstandthenuanceoftheissuesandfurtherimproveaccuracy.TheAI-generatedsummaryabovethelistofdocumentscanbeextraordinarilyusefulforgettinganoverviewoftheissuesandpointers,butitshouldneverbeusedtoadviseaclient,writeabrieformotionforacourt,orotherwisebereliedonwithoutdoingfurtherresearch.Useittoacceleratethoroughresearch.Don'tuseitasareplacementforthoroughresearch.Also,becauseSearch&SummarizeRutterisrelyingonatreatisethatisupdatedperiodicallybutnotdaily,keepinmindthatstatementsoflawshouldbecheckedforcurrencyasthoughusinganysecondarysourcecontent.VisittheAICourtRulespagetoreviewthecourtrules,ordersthatupdatecourtrules,proposedandadoptedlegislation,andselectordersfromindividualjudgesthataddresstheuseofartificialintelligenceinlegalresearchanddrafting.PleaseconsultallapplicablerulesofpracticepriortofilingdocumentsthatrelyonAI-basedtools.ReviewAICourtRulesandOrdersopensinanewtab";

            string checkAiAssistantFeatureHeader = "Verify: How AI Works Title is displayed as expected";
            string checkAiAssistantFeatureDescription = "Verify: How AI Works description is as expected";
            string checkAiAssistantFeatureHeaderFromChatArea = "Verify: How AI Works Title in the chat Area is displayed as expected";
            string checkAiAssistantFeatureDescriptionFromChatArea = "Verify: How AI Works description in the chat Area is as expected";

            var typeahead = this.GetHomePage<PrecisionHomePage>().Header.EnterSearchQuery<TrdTypeAheadDialog>(SearchQuery);
            var categoryPage = typeahead.OtherComponent.ClickOnSuggestionByIndex<EdgeContentTypeSearchResultPage>(NewTypeAheadCategories.ContentPages, 0);
            var aiAssistantPage = categoryPage.Toolbar.ClickToolbarElement<AiAssistedResearchPage>(EdgeToolbarElements.SearchAndSummarizeRutter);

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            var howAiWorksDialog = aiAssistantPage.Chat.HowAiWorksLink.Click<HowAiAssistedResearchWorksDialog>();

            this.TestCaseVerify.IsTrue(
                checkAiAssistantFeatureHeader,
                howAiWorksDialog.TitleLabel.Text.Equals(HowAiWorksDialogHeader),
                "'How the AI works' Feature Title is not as expected");

            this.TestCaseVerify.IsTrue(
                checkAiAssistantFeatureDescription,
                this.RemoveNewLinesAndExtraSpaces(howAiWorksDialog.DescriptionLabel.Text).Equals(HowAIWorksDialogDescription),
                "'How the AI works' Feature Description content is not as expected");

            aiAssistantPage = howAiWorksDialog.CloseButton.Click<AiAssistedResearchPage>();

            //checking the second how AI model works link
            howAiWorksDialog = aiAssistantPage.Toolbar.HowAiWorksButton.Click<HowAiAssistedResearchWorksDialog>();

            this.TestCaseVerify.IsTrue(
                checkAiAssistantFeatureHeaderFromChatArea,
                howAiWorksDialog.TitleLabel.Text.Equals(HowAiWorksDialogHeader),
                "'How the AI works' Feature Title content is not as expected");

            this.TestCaseVerify.IsTrue(
                checkAiAssistantFeatureDescriptionFromChatArea,
                this.RemoveNewLinesAndExtraSpaces(howAiWorksDialog.DescriptionLabel.Text).Equals(HowAIWorksDialogDescription),
                "'How the AI works' Feature Description content in the chat area is not as expected ");
        }
        /// <summary>
        /// Test case: 2126295, 2131035, 2132648, 2131037
        /// Verify O'Connor's access point on Every Document
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        public void AiTreatiseOConnorsCommonTest()
        {
            const string CommentariesSearchQuery = "O'Connor's Texas Causes of Action";
            const string CommentariesFollowUpSearchQuery = "Preliminary Materials";
            const string FormsSearchQuery = "Texas Forms Real Estate";
            const string FormsFollowUpSearchQuery = "About the Author";
            const string CodesAndRulesSearchQuery = "O'Connor's Texas CPRC Plus";
            const string CodesAndRulesFollowUpSearchQuery = "O'Connor's Texas CPRC & Related Statutes & Rules";
            const string HowAiWorksDialogHeader = "How Search & Summarize O'Connor's works";
            const string HowAIWorksDialogDescription = "Search&SummarizeO'Connor'suseslargelanguagemodels-atypeofgenerativeAI-andfocusesthemodelsonlyonthelanguageofO'Connor'sCommentariesandForms.PleasenotethatSearch&SummarizeO'Connor'sdoesnotleverageO'Connor'sCodes&Rulescontent.PortionsfromO'Connor'sCommentariesandFormsareusedintheresponsesandmayincludetheactuallanguagefromthesourceorreferencestoprimarylaw.Linksareincludedtoreadthefulltextofthesourcedocuments.Evenwiththeseandotherprecautions,Search&SummarizeO'Connor'scanoccasionallyproduceinaccuracies,soitshouldalwaysbeusedaspartofaresearchprocessinconnectionwithadditionalresearchtofullyunderstandthenuanceoftheissuesandfurtherimproveaccuracy.TheAI-generatedsummaryabovethelistofdocumentscanbeextraordinarilyusefulforgettinganoverviewoftheissuesandpointers,butitshouldneverbeusedtoadviseaclient,writeabrieformotionforacourt,orotherwisebereliedonwithoutdoingfurtherresearch.Useittoacceleratethoroughresearch.Don'tuseitasareplacementforthoroughresearch.Also,becauseSearch&SummarizeO'Connor'sisrelyingonatreatisethatisupdatedperiodicallybutnotdaily,keepinmindthatstatementsoflawshouldbecheckedforcurrencyasthoughusinganysecondarysourcecontent.VisittheAICourtRulespagetoreviewthecourtrules,ordersthatupdatecourtrules,proposedandadoptedlegislation,andselectordersfromindividualjudgesthataddresstheuseofartificialintelligenceinlegalresearchanddrafting.PleaseconsultallapplicablerulesofpracticepriortofilingdocumentsthatrelyonAI-basedtools.ReviewAICourtRulesandOrdersopensinanewtab";
            const string TipsForBestResultsDialogDescription = "Writeaqueryfocusedonthecontentyou’dexpecttofindinO'Connor'sCommentariesandFormsO'Connor'sCommentariesandFormsaretheonlycontentbeingusedtogeneratearesponsetoaquery.Search&SummarizeO'Connor'sdoesnotleverageO'Connor'sCodes&Rulescontent.Therefore,avoidqueriesaboutcontentoutsidethescopeofthatcoverageoronthelawofjurisdictionsoutsideTexas,Federal,andselectCalifornialaw,suchasquestionsaboutcodeannotationsorquestionsthataskthingslike“InNewYorkStateCourts...”AlwayscheckthecurrencyofstatementsoflawBecauseSearch&SummarizeO'Connor'sisrelyingonatreatisethatisupdatedperiodicallybutnotdaily,keepinmindthatstatementsoflawinthesummaryandintheunderlyingdocumentsshouldbecheckedforcurrencyasthoughusinganysecondarysourcecontent.Writeaclear,concise,andfocusedquerythatis1-2sentenceslongConsideraddinginformation,suchasaspecificruleofcivilprocedure,amotion,oraproceduralprocesstoprovidebettercontextforthequery.Query:Whendoesapartyhavearighttodemandatrial?Betterquery:Doesapartyhavearighttodemandatrialevenifajudicialarbitrationawardhasbeenentered?ProviderelevantinformationDonotincludeextrabackgroundfactsthatarenotmaterialtothequestion.Eachterminaqueryshouldbeessentialtofindingrelevantinformation.Query:Myclientwasservedwithasummonsandcomplaint.Hewastoonervoustotellmeaboutitandnowwefearhemayfaceadefaultjudgmentasaresultofthedelay.Hedoesnotrememberwhenhewasserved.Ifadefaultjudgmenthasbeenentered,whatcanIdotogetthisdefaultsetaside?Betterquery:Ifadefaultjudgmenthasbeenentered,whatcanIdotogetitsetaside?Donotwriteaqueryinthestyleofaprompt,command,orinstructionWehavedonethepromptingandinstructionworkforyouandhavedesignedAI-AssistedResearchtoworkbestwithlegalresearchqueriesthatclearlyidentifytheinformationyouwouldliketofind.Forexample,itisbettertoavoidincludingextrawordsorphraseslike\"findallrulesthat…\"thatbookendthequerybutarenotsubstantive.Instead,itisbettertoconstructaquerywithwordsorphrasesyouwouldexpecttofindincases,statutes,orregulationsoncivilprocedure.Query:Youareanexperiencedattorneywhoisanexpertlegalresearcher,andyouneedtofindandsummarizealloftherulesonansweringacomplaintanddefaultjudgment.Betterquery:Whataretherequirementsforapplyingforentryofdefaultifadefendantdoesnotansweracomplaint?FocusonasingleissueorquestionWhereitiseasytodistinguishseparateissues,askaboutthoseissuesinseparatequestionsdedicatedtotheissues,ratherthancombiningthem.Query:Whoisallowedtoserveasummons,andcanasummonsbeservedonaminordefendant?Betterquery:Whoisallowedtoserveasummons?Betterquery:Canyouserveasummonsonaminordefendant?";

            string checkSecondarySourcesTipsForBestResultsDialogDescription = "Verify: 'Tips For Best Results' Dialogue description is as expected when opened from Secondary Sources";
            string checkSecondarySourcesTipsForBestResultsDialogDescriptionFromToolbar = "Verify: 'Tips For Best Results' Dialogue description in the Toolbar is as expected when opened from Secondary Sources";
            string checkAiAssistantFeatureHeaderAndDescription = "Verify: 'How AI Works' Title and descrition are as expected";
            string checkAiAssistantFeatureHeaderAndDescriptionFromChatArea = "Verify: 'How AI Works' Title and description in the chat Area are as expected";
            string checkCommentariesSearchSummaryButtonDisplayed = "Verify: Search & Summarize O'Connor's button is displayed as expected for Commentaries";
            string checkSearchSummaryOConnorsLandingPageContentText = "Verify: Search & Summarize O'Connor's Landing Page header, label and label content is correct";
            string checkSearchSummaryOConnorsContentText = "Verify: Search & Summarize O'Connor's Category Page label text is correct";
            string checkTipsForBestResultsDialogDescription = "Verify: 'Tips For Best Results' Dialogue description is as expected";
            string checkTipsForBestResultsDialogDescriptionFromToolbar = "Verify: 'Tips For Best Results' Dialogue description in the Toolbar is as expected";
            string checkCommentariesNestedDocumentButtonDisplayed = "Verify: Search & Summarize O'Connor's button in a nested document is displayed as expected for Commentaries";
            string checkFormsSearchSummaryButtonDisplayed = "Verify: Search & Summarize O'Connor's button is displayed as expected for forms";
            string checkFormsNestedDocumentSearchSummaryButtonDisplayed = "Verify: Search & Summarize O'Connor's button in a nested document is displayed as expected for forms";
            string checkCodesAndRulesSearchSummaryButtonNotDisplayed = "Verify: Search & Summarize O'Connor's button is not displayed for Codes and Rules";
            string checkCodesAndRulesNestedDocumentSearchSummaryButtonNotDisplayed = "Verify: Search & Summarize O'Connor's button in a nested document is not displayed for Codes and Rules";

            //OConnor Commentaries
            var secondarySourcesContentTypePage = this.GetHomePage<PrecisionHomePage>().BrowseTabPanel.SetActiveTab<AllContentTabPanel>(PrecisionBrowseTab.ContentTypes)
                .ClickBrowseCategory<EdgeContentTypeSearchResultPage>(ContentType.SecondarySources);

            var aiAssistantPage = secondarySourcesContentTypePage.ClickLinkByText<AiAssistedResearchPage>($"{SearchAndSummarizeOConnors}");

            //Checking the first Tips For Best Results link opened from SecondarySources under Tools & Resources
            var tipsForBestResultsDialog = aiAssistantPage.Chat.TipsForBestResultLink.Click<TipsForBestResultsDialog>();

            this.TestCaseVerify.IsTrue(
                checkSecondarySourcesTipsForBestResultsDialogDescription,
                this.RemoveNewLinesAndExtraSpaces(tipsForBestResultsDialog.DescriptionLabel.Text).Equals(TipsForBestResultsDialogDescription),
                "'Tips For Best Results' Dialogue description is NOT as expected when opened from SecondarySources");

            aiAssistantPage = tipsForBestResultsDialog.CloseButton.Click<AiAssistedResearchPage>();

            //checking the second Tips For Best Results link in the Toolbar opened from SecondarySources under Tools & Resources
            tipsForBestResultsDialog = aiAssistantPage.Toolbar.TipsBestResultsButton.Click<TipsForBestResultsDialog>();

            this.TestCaseVerify.IsTrue(
                  checkSecondarySourcesTipsForBestResultsDialogDescriptionFromToolbar,
                this.RemoveNewLinesAndExtraSpaces(tipsForBestResultsDialog.DescriptionLabel.Text).Equals(TipsForBestResultsDialogDescription),
                "'Tips For Best Results' Dialogue description in the Toolbar is NOT as expected when opened from SecondarySources");

            secondarySourcesContentTypePage = BrowserPool.CurrentBrowser.GoBack<EdgeContentTypeSearchResultPage>();
            var browsePage = secondarySourcesContentTypePage.ClickLinkByText<EdgeContentTypeSearchResultPage>(OConnorsContentName)
                .ClickLinkByText<EdgeContentTypeSearchResultPage>(CommentariesSearchQuery);

            this.TestCaseVerify.IsTrue(
                checkCommentariesSearchSummaryButtonDisplayed,
                browsePage.Toolbar.GetToolbarElementText(EdgeToolbarElements.SearchAndSummarizeOConnors).Equals(SearchAndSummarizeOConnors),
                "'Search & Summarize O'Connor's button' is not displayed as expected for Commentaries");

            //SearchAndSummarizeOConnors landing page label & content text, Category page label text verification
            aiAssistantPage = browsePage.Toolbar.ClickToolbarElement<AiAssistedResearchPage>(EdgeToolbarElements.SearchAndSummarizeOConnors);

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            this.TestCaseVerify.IsTrue(
                checkSearchSummaryOConnorsLandingPageContentText,
                aiAssistantPage.Toolbar.HeadingLabel.Text.Equals(SearchAndSummarizeOConnors)
                && aiAssistantPage.Chat.LandingPageLabel.Text.Contains(SearchAndSummarizeOConnors)
                && aiAssistantPage.Chat.LandingPageContentLabel.Text.Contains(SearchAndSummarizeOConnors),
                "Search & Summarize O'Connor's Landing Page header, label and label content text is incorrect");

            this.TestCaseVerify.IsTrue(
                checkSearchSummaryOConnorsContentText,
                aiAssistantPage.Toolbar.CategoryPageLabel.Displayed
                && aiAssistantPage.Toolbar.CategoryPageLabel.Text.Equals("Content: O'Connor's"),
                "Search & Summarize O'Connor's Category Page label text is incorrect");

            var howAiWorksDialog = aiAssistantPage.Chat.HowAiWorksLink.Click<HowAiAssistedResearchWorksDialog>();

            this.TestCaseVerify.IsTrue(
                checkAiAssistantFeatureHeaderAndDescription,
                howAiWorksDialog.TitleLabel.Text.Equals(HowAiWorksDialogHeader)
                && this.RemoveNewLinesAndExtraSpaces(howAiWorksDialog.DescriptionLabel.Text).Equals(HowAIWorksDialogDescription),
                "'How AI Works' Title and description are NOT as expected");

            aiAssistantPage = howAiWorksDialog.CloseButton.Click<AiAssistedResearchPage>();

            //checking the second how AI model works link
            howAiWorksDialog = aiAssistantPage.Toolbar.HowAiWorksButton.Click<HowAiAssistedResearchWorksDialog>();

            this.TestCaseVerify.IsTrue(
                checkAiAssistantFeatureHeaderAndDescriptionFromChatArea,
                howAiWorksDialog.TitleLabel.Text.Equals(HowAiWorksDialogHeader)
                && this.RemoveNewLinesAndExtraSpaces(howAiWorksDialog.DescriptionLabel.Text).Equals(HowAIWorksDialogDescription),
                "'How AI Works' Title and description in the chat Area are NOT as expected");

            //Checking the first Tips For Best Results link
            aiAssistantPage = howAiWorksDialog.CloseButton.Click<AiAssistedResearchPage>();

            tipsForBestResultsDialog = aiAssistantPage.Chat.TipsForBestResultLink.Click<TipsForBestResultsDialog>();

            this.TestCaseVerify.IsTrue(
                checkTipsForBestResultsDialogDescription,
                this.RemoveNewLinesAndExtraSpaces(tipsForBestResultsDialog.DescriptionLabel.Text).Equals(TipsForBestResultsDialogDescription),
                "'Tips For Best Results' Dialogue description is NOT as expected");

            aiAssistantPage = tipsForBestResultsDialog.CloseButton.Click<AiAssistedResearchPage>();

            //checking the second Tips For Best Results link in the Toolbar
            tipsForBestResultsDialog = aiAssistantPage.Toolbar.TipsBestResultsButton.Click<TipsForBestResultsDialog>();

            this.TestCaseVerify.IsTrue(
                checkTipsForBestResultsDialogDescriptionFromToolbar,
                this.RemoveNewLinesAndExtraSpaces(tipsForBestResultsDialog.DescriptionLabel.Text).Equals(TipsForBestResultsDialogDescription),
                "'Tips For Best Results' Dialogue description in the Toolbar is NOT as expected");

            BrowserPool.CurrentBrowser.CloseTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(BrowsePageTab);

            browsePage = browsePage.ClickLinkByText<EdgeContentTypeSearchResultPage>(CommentariesFollowUpSearchQuery);

            this.TestCaseVerify.IsTrue(
                checkCommentariesNestedDocumentButtonDisplayed,
                browsePage.Toolbar.GetToolbarElementText(EdgeToolbarElements.SearchAndSummarizeOConnors).Equals(SearchAndSummarizeOConnors),
                "Nested document 'Search & Summarize O'Connor's button' is not displayed as expected for Commentaries");

            //Oconner Forms
            browsePage = BrowserPool.CurrentBrowser.GoBack<EdgeContentTypeSearchResultPage>();
            browsePage = BrowserPool.CurrentBrowser.GoBack<EdgeContentTypeSearchResultPage>();
            browsePage = browsePage.ClickLinkByText<EdgeContentTypeSearchResultPage>(FormsSearchQuery);

            this.TestCaseVerify.IsTrue(
                checkFormsSearchSummaryButtonDisplayed,
                browsePage.Toolbar.GetToolbarElementText(EdgeToolbarElements.SearchAndSummarizeOConnors).Equals(SearchAndSummarizeOConnors),
                "'Search & Summarize O'Connor's button' is not as expected for Forms");

            browsePage = browsePage.ClickLinkByText<EdgeContentTypeSearchResultPage>(FormsFollowUpSearchQuery);

            this.TestCaseVerify.IsTrue(
                checkFormsNestedDocumentSearchSummaryButtonDisplayed,
                browsePage.Toolbar.GetToolbarElementText(EdgeToolbarElements.SearchAndSummarizeOConnors).Equals(SearchAndSummarizeOConnors),
                "Nested document 'Search & Summarize O'Connor's button' is not as expected for Forms");

            //Oconner Codes and Rules
            browsePage = BrowserPool.CurrentBrowser.GoBack<EdgeContentTypeSearchResultPage>();
            browsePage = BrowserPool.CurrentBrowser.GoBack<EdgeContentTypeSearchResultPage>();
            browsePage = browsePage.ClickLinkByText<EdgeContentTypeSearchResultPage>(CodesAndRulesSearchQuery);

            this.TestCaseVerify.IsFalse(
                checkCodesAndRulesSearchSummaryButtonNotDisplayed,
                browsePage.Toolbar.IsToolbarElementDisplayed(EdgeToolbarElements.SearchAndSummarizeOConnors),
                "'Search & Summarize O'Connor's button' is displayed for Codes and Rules");

            browsePage = browsePage.ClickLinkByText<EdgeContentTypeSearchResultPage>(CodesAndRulesFollowUpSearchQuery);

            this.TestCaseVerify.IsFalse(
                checkCodesAndRulesNestedDocumentSearchSummaryButtonNotDisplayed,
                browsePage.Toolbar.IsToolbarElementDisplayed(EdgeToolbarElements.SearchAndSummarizeOConnors),
                "Nested document 'Search & Summarize O'Connor's button' is displayed for Codes and Rules");
        }

        /// <summary>
        /// Test case: 2131847, 2133868, 2199183
        /// Verify Content name is displayed as on the left history pane
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        public void AiTreatiseOConnorsHistoryEventsTest()
        {
            //const string FirstQuestion = "Compare holding of rogers by & through standley v. retrum, 170 ariz. 399 (ct. app. 1991) and hill v. safford unified sch. dist., 191 ariz. 110 (ct. app. 1997) on the topic of duty of teachers and administrators";
            const string SecondQuestion = "May interrogatories be served on a non-party?";
            const string ConversationDateFormat = "MMM d, yyyy hh:mm tt";
            const string HistoryEventDateFormat = "M/d/yyyh:mm tt";
            const string SearchType = "Search & Summarize";

            //string checkNoErrorAfterEmailMe = "Verify: Clicking 'Email Me' doesn't cause any errors";
            string checkContentNameLabel = "Verify: Content name is displayed as OConnor's";
            string checkContentNameOnHistoryPane = "Verify: Content pane name is displayed as 'O'Connor's' in the left history pane";
            string checkHistoryPage = "Verify: Category page name is displayed as 'Search & Summarize . O'Connor's' instead of 'AI-Assisted Research . All State & Federal' ";
            string checkHistoryPageLeadsToAiResearch = "Verify: Category page event from the History page leads to AI-Assisted Research page";
            string checkGoToFullistoryPageLeadsToAiResearch = "Verify: Category page event from the History page (via 'Go to full history' link) leads to Full history search page";
            string checkDelivery = "Verify: Category page name is displayed instead of jurisdiction on the Delivered history";
            string checkHistoryPageViaGoToFullHistory = "Verify: Category page name is displayed instead of jurisdiction on the History page (via 'Go to full history' link)";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            homePage = homePage.Header.CompartmentDropdown.ArrowButton.Click<PrecisionHomePage>();

            var productName = homePage.Header.CompartmentDropdown.SelectedOptionLink.Text;

            var browsePage = homePage.BrowseTabPanel.SetActiveTab<AllContentTabPanel>(PrecisionBrowseTab.ContentTypes)
              .ClickBrowseCategory<EdgeContentTypeSearchResultPage>(ContentType.SecondarySources)
              .ClickLinkByText<EdgeContentTypeSearchResultPage>(OConnorsContentName)
              .ClickLinkByText<EdgeContentTypeSearchResultPage>(OConnorsSearchQuery);

            var aiAssistantPage = browsePage.Toolbar.ClickToolbarElement<AiAssistedResearchPage>(EdgeToolbarElements.SearchAndSummarizeOConnors);

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            this.TestCaseVerify.IsTrue(
               checkContentNameLabel,
               aiAssistantPage.Toolbar.CategoryPageLabel.Text.Equals($"Content: {OConnorsContentName}"),
               "Category page name is NOT 'OConnor's'");

            //aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(FirstQuestion);
            //aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();

            //SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().EmailMeButton.Displayed);

            //aiAssistantPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().EmailMeButton.Click<AiAssistedResearchPage>();

            //SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            //Bug 2177003
            //this.TestCaseVerify.IsTrue(
            //    checkNoErrorAfterEmailMe,
            //    aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed,
            //    "Clicking 'Email Me' causes errors");

            //aiAssistantPage = aiAssistantPage.Toolbar.NewResearchButton.Click<AiAssistedResearchPage>();

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(SecondQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage = aiAssistantPage.ConversationHistory.ExpandButton.Click<AiAssistedResearchPage>();

            this.TestCaseVerify.IsTrue(
                checkContentNameOnHistoryPane,
                aiAssistantPage.ConversationHistory.Conversations.All(historyEvent => historyEvent.ContentLabel.Text.Contains($"Content: {OConnorsContentName}")),
                "Content pane name on the left history pane is NOT 'OConnor's'");

            // History tab
            var recentHistoryDialog = aiAssistantPage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);
            SafeMethodExecutor.WaitUntil(() => recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemLink.Displayed);

            var conversationDate = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ConversationDateLabel.Text;
            var formattedConversationDate = DateTime.ParseExact(conversationDate, ConversationDateFormat, CultureInfo.InvariantCulture);

            if (!recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemDateLabel.Text.Equals(formattedConversationDate))
            {
                SafeMethodExecutor.WaitUntil(() =>
                {
                    aiAssistantPage = BrowserPool.CurrentBrowser.Refresh<AiAssistedResearchPage>();

                    conversationDate = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ConversationDateLabel.Text;
                    formattedConversationDate = DateTime.ParseExact(conversationDate, ConversationDateFormat, CultureInfo.InvariantCulture);

                    recentHistoryDialog = aiAssistantPage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);

                    var historyEventDate = recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemDateLabel.Text;
                    var upperBoundHistoryEventDate = formattedConversationDate.AddMinutes(1).ToString(HistoryEventDateFormat);

                    return historyEventDate.Equals(formattedConversationDate.ToString(HistoryEventDateFormat)) || historyEventDate.Equals(upperBoundHistoryEventDate);
                }, timeoutFromSec: 60, pollingIntervalInMilliseconds: 2000);
            }

            this.TestCaseVerify.IsTrue(
                checkHistoryPage,
                recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemLink.Text.Equals(SecondQuestion)
                && recentHistoryDialog.GetRecentSearchesItems().First().MetaData.Contains($"{SearchType}{OConnorsContentName}"),
                "Category page name is NOT displayed as 'Search & Summarize . O'Connor's' on the History page");

            aiAssistantPage = recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemLink.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkHistoryPageLeadsToAiResearch,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.Remove(0, aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.IndexOf("says:") + 7).Equals(SecondQuestion)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Count.Equals(1)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed
                && aiAssistantPage.Toolbar.CategoryPageLabel.Text.Equals($"Content: {OConnorsContentName}"),
                "Category page event from the History tab doesn't lead to AI-Assisted Research page");

            // History page
            recentHistoryDialog = aiAssistantPage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);
            var historyPage = recentHistoryDialog.ViewAllLink.Click<EdgeCommonHistoryPage>();

            this.TestCaseVerify.IsTrue(
                checkHistoryPage,
                historyPage.HistoryTable.GetGridItems().First().Title.Equals(SecondQuestion)
                && historyPage.HistoryTable.GetGridItems().First().Summary.Equals($"{SearchType}{OConnorsContentName}"),
                //&& historyPage.HistoryTable.GetGridItems().ElementAt(1).Title.Equals(FirstQuestion)
                //&& historyPage.HistoryTable.GetGridItems().ElementAt(1).Summary.Equals($"{SearchType}{OConnorsContentName}"),
                "Category page name is NOT displayed as 'Search & Summarize . O'Connor's on the History page");

            // Delivery
            var historyEventsCount = historyPage.HistoryTable.GetGridItems().Count.ToString();

            var downloadDialog = historyPage.EdgeToolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);

            if (this.Settings.GetValue(EnvironmentConstants.IsFedRamp).ToLower().Equals("no"))
            {
                downloadDialog.TheBasicsTab.NumberToDeliver.SelectOption<DownloadDialog>(downloadDialog.TheBasicsTab.NumberToDeliver.Options.FirstOrDefault(option => option.Contains(historyEventsCount)));
            }

            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<PrecisionCommonSearchResultPage>();

            var fileName = historyEventsCount.Equals(1) ? $"{productName} - List of {historyEventsCount} item from {GetUserInfo().FirstName} {GetUserInfo().LastName} All History.pdf" : $"{productName} - List of {historyEventsCount} items from {GetUserInfo().FirstName} {GetUserInfo().LastName} All History.pdf";

            FileUtil.WaitForFileDownload(this.FolderToSave, fileName);

            var text = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(this.FolderToSave, fileName)).Replace("\r\n", string.Empty).Replace(" ", string.Empty);

            this.TestCaseVerify.IsTrue(
                checkDelivery,
                text.Contains($"{SecondQuestion}{SearchAndSummarizeOConnors}".Replace(" ", string.Empty)),
                "Category page name is NOT displayed as 'Search & Summarize . O'Connor's' on the Delivered history");

            aiAssistantPage = historyPage.HistoryTable.GetGridItems().First().ClickLinkByText<AiAssistedResearchPage>(historyPage.HistoryTable.GetGridItems().First().Title);
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                 checkHistoryPageLeadsToAiResearch,
                 aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.Remove(0, aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.IndexOf("says:") + 7).Equals(SecondQuestion)
                 && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Count.Equals(1)
                 && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed
                 && aiAssistantPage.Toolbar.CategoryPageLabel.Text.Equals($"Content: {OConnorsContentName}"),
                 "Category page event from the History page doesn't lead to AI-Assisted Research page");

            // History page via "Go to full history" link
            aiAssistantPage = aiAssistantPage.ConversationHistory.ExpandButton.Click<AiAssistedResearchPage>();

            historyPage = aiAssistantPage.ConversationHistory.GoToFullHistoryLink.Click<EdgeCommonHistoryPage>();

            BrowserPool.CurrentBrowser.CreateTab(HistoryPageTab);
            BrowserPool.CurrentBrowser.ActivateTab(HistoryPageTab);

            SafeMethodExecutor.WaitUntil(() => historyPage.EdgeToolbar.DeliveryDropdown.IsDisplayed());

            this.TestCaseVerify.IsTrue(
                checkHistoryPageViaGoToFullHistory,
                historyPage.HistoryTable.GetGridItems().First().Title.Equals(SecondQuestion)
                && historyPage.HistoryTable.GetGridItems().First().Summary.Equals($"{SearchType}{OConnorsContentName}"),
                "Category page name is NOT displayed instead of jurisdiction on the History page (via 'Go to full history' link)");

            aiAssistantPage = historyPage.HistoryTable.GetGridItems().First().ClickLinkByText<AiAssistedResearchPage>(historyPage.HistoryTable.GetGridItems().First().Title);
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkGoToFullistoryPageLeadsToAiResearch,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.Remove(0, aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.IndexOf("says:") + 7).Equals(SecondQuestion)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Count.Equals(1)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed
                && aiAssistantPage.Toolbar.CategoryPageLabel.Text.Equals($"Content: {OConnorsContentName}"),
                "Category page event from the History page (via 'Go to full history' link) doesn't lead to AI-Assisted Research page");
        }

        private string RemoveNewLinesAndExtraSpaces(string text) => text.Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(" ", string.Empty);

        private string CleanTextForCompare(string text)
        {
            text = text.Replace(" ", string.Empty).Replace(")", string.Empty).Replace("\r\n", string.Empty);

            text = Regex.Replace(text, @"GovernmentWorks\.\b.?", string.Empty).Replace($"WestlawSearch&SummarizeRutter•Responsegenerated:{DateTime.Now:MMMMd,yyyy}WestlawPrecision.©{DateTime.Now:yyyy}ThomsonReuters.NoclaimtooriginalU.S.", string.Empty).Replace("•", string.Empty);

            text = Regex.Replace(text, @"\b\d+\.\b", string.Empty);

            text = Regex.Replace(text, @"(?<=.)\d+(?=[A-Za-z])", string.Empty);

            text = Regex.Replace(text, @"(?<=[A-Za-z])\d+(?=[A-Za-z])", string.Empty);

            return text;
        }
    }
}
