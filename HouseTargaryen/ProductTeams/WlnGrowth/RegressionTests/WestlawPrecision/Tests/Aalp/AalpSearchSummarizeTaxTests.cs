namespace WestlawPrecision.Tests.Aalp
{
    using Framework.Common.UI.Products.Shared.Dialogs.Delivery;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Products.WestlawEdge.Components.BrowseWidget;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Foldering;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.AALP;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
    using Framework.Common.UI.Products.WestLawNext.Enums.Delivery;
    using Framework.Common.UI.Products.WestLawNext.Pages.SearchResult.ContentTypeSearchResultPages;
    using Framework.Common.UI.Raw.EnhancementTests.Utils;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Header;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.Extensions;
    using Framework.Core.Utils.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Keys = OpenQA.Selenium.Keys;

    [TestClass]
    public class AalpSearchSummarizeTaxTests : AalpBaseTest
    {
        private const string FeatureTestCategory = "SearhAndSummariseTax";
        string checkEmailMeButtonIsNotDisplayed = "Verify: 'Email me' button is not displayed when opening conversation from history";

        /// <summary>
        /// Test Case 2171720: [Search & Summarize Tax] Tips for best results popup
        /// Test Case 2171674: [Search & Summarize Tax] Ability content info on search page
        /// Test Case 2171706: [Search & Summarize Tax] How the AI works popup
        /// Description: Verify common functionality of AI Assistant including info dialogs, headings, browser tab name, and links
        /// 1. Navigate to theSearch & Summarize tax page and Verify: Browser tab title and page heading are correct.
        /// 2. Verify: Welcome text is displayed on the landing page.
        /// 3. Verify: "Learn more" dialog text on the AI Assistant page is as expected.
        /// 4. Verify: "How AI-Assisted Research works" dialog title and content.
        /// 5. Verify: AI Court Rules page opens correctly.
        /// 6. Verify: "Tips for best results" dialog title and content.
        /// 7. Verify: 'Send' button is disabled for an empty question on the AI Assistant page.
        /// 8. Verify: You cannot submit an empty question using the 'Enter' key on the AI Assistant page.
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        public void SearchAndSummarizeTaxCommonTest()
        {
            string AdministrativeDecisionsAndGuidance = "Administrative Decisions & Guidance";
            const string AiCourtRulesTab = "Ai Court Rules tab";
            const string EmptyQuestion = "         ";
            const string HowAiWorksDialogTitle = "How Search & Summarize Tax works";
            const string TipsForBestResultsDialogTitle = "Tips for best results";

            string checkBrowserTabTitle = "Verify: Browse tab title is correct";
            string checkPageHeading = "Verify: Page heading is correct";
            string checkWelcomeLandingPageTextIsDisplayed = "Verify: Welcome text is displayed";
            string checkLearnMoreLinkDialogLandingPageText = "Verify: Learn more dialog text is as expected (AI Assistant page)";
            string checkHowAiAssistedResearchWorksDialogText = "Verify: 'How AI-Assisted Research works' dialog title and content is as expected";
            string checkAiCourtRulesPage = "Verify: AI Court Rules page is opened";
            string checkTipsForBestResultsDialogText = "Verify: 'Tips for best results' dialog title and content is as expected";
            string checkSendButtonDisabled = "Verify: 'Send' button is disabled for empty (whitespace) question";
            string checkUnableToAskViaEnter = "Verify: Unable to ask empty (whitespace) question via 'Enter' button tap";
            string checkNewResearchButton = "Verify: new research button is displayed";

            var contentTypeSearchResultsPage = this.GetHomePage<PrecisionHomePage>().BrowseTabPanel.SetActiveTab<ContentTypesTabPanel>(PrecisionBrowseTab.ContentTypes)
                .ClickLinkByText<CommonBrowsePage>(AdministrativeDecisionsAndGuidance)
                .GetAllLinks().First(link => link.Text.Contains("All Federal Administrative Decisions & Guidance")).Click<EdgeCommonBrowsePage>()
                .GetAllLinks().First(link => link.Text.Contains("Department of the Treasury (USTREAS)")).Click<EdgeCommonBrowsePage>()
                .GetAllLinks().First(link => link.Text.Contains("Internal Revenue Service (IRS)")).Click<EdgeCommonBrowsePage>();

            var aiAssistantPage = contentTypeSearchResultsPage.SearchAndSummarizeTaxButton.Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Toolbar.IsDisplayed());

            this.TestCaseVerify.IsTrue(
               checkBrowserTabTitle,
               BrowserPool.CurrentBrowser.Title.Equals("Westlaw AI-Assisted Research | Westlaw Precision"),
               "Browser tab title is not correct");

            this.TestCaseVerify.IsTrue(
               checkPageHeading,
               aiAssistantPage.Toolbar.HeadingLabel.Text.Equals(SearchAndSummarizeTax),
               "Heading is NOT correct");

            this.TestCaseVerify.IsTrue(
                checkWelcomeLandingPageTextIsDisplayed,
                aiAssistantPage.Chat.LandingPageLabel.Displayed,
                "Welcome text is NOT displayed");

            var howAiWorksDialog = aiAssistantPage.Chat.HowAiWorksLink.Click<HowAiAssistedResearchWorksDialog>();

            this.TestCaseVerify.IsTrue(
                checkLearnMoreLinkDialogLandingPageText,
                howAiWorksDialog.TitleLabel.Text.Equals(HowAiWorksDialogTitle)
                && howAiWorksDialog.DescriptionLabel.Text.Any(),
                "Learn more dialog text is NOT as expected (AI Assistant page)");

            aiAssistantPage = howAiWorksDialog.CloseButton.Click<AiAssistedResearchPage>();

            howAiWorksDialog = aiAssistantPage.Toolbar.HowAiWorksButton.Click<HowAiAssistedResearchWorksDialog>();

            this.TestCaseVerify.IsTrue(
                checkHowAiAssistedResearchWorksDialogText,
                howAiWorksDialog.TitleLabel.Text.Equals(HowAiWorksDialogTitle)
                && howAiWorksDialog.DescriptionLabel.Text.Any(),
                "'How AI-Assisted Research works' dialog title and content is NOT as expected");

            var browsePage = howAiWorksDialog.ReviewAiCourtRulesLink.Click<EdgeCommonBrowsePage>();
            BrowserPool.CurrentBrowser.CreateTab(AiCourtRulesTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiCourtRulesTab);

            this.TestCaseVerify.IsTrue(
                checkAiCourtRulesPage,
                browsePage.GetBrowsePageTitle().Contains("AI Court Rules"),
                "AI Court Rules page is NOT opened");

            BrowserPool.CurrentBrowser.CloseTab(AiCourtRulesTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = howAiWorksDialog.CloseButton.Click<AiAssistedResearchPage>();

            var tipsForBestResultsDialog = aiAssistantPage.Toolbar.TipsBestResultsButton.Click<TipsForBestResultsDialog>();

            this.TestCaseVerify.IsTrue(
                checkTipsForBestResultsDialogText,
                tipsForBestResultsDialog.TitleLabel.Text.Equals(TipsForBestResultsDialogTitle)
                && tipsForBestResultsDialog.DescriptionLabel.Text.Length > 0,
                "'Tips for best results' dialog title and content is NOT as expected");

            aiAssistantPage = tipsForBestResultsDialog.CloseButton.Click<AiAssistedResearchPage>();

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(EmptyQuestion);

            this.TestCaseVerify.IsFalse(
                checkSendButtonDisabled,
                aiAssistantPage.QueryBox.SendQuestionButton.Enabled,
                "'Send' button is NOT disabled for empty (whitespace) question");

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Keys.Enter);

            this.TestCaseVerify.IsTrue(
                checkUnableToAskViaEnter,
                aiAssistantPage.Chat.LandingPageLabel.Displayed,
                "Able to ask empty (whitespace) question via 'Enter' button tap");

            this.TestCaseVerify.IsTrue(
                checkNewResearchButton,
                aiAssistantPage.Toolbar.NewResearchButton.Displayed,
                "New reesearch button is displayed");
        }

        /// <summary>
        /// Test Case 2171677: [Search & Summarize Tax]  Ability content info in History
        /// Description: Verify presence of Search and Summarize Tax button/link on specific content links.
        /// 1. Navigate to Home page -> Content types -> Administrative Decisions & Guidance -> All Federal Administrative
        /// Decisions & Guidance -> Department of the Treasury (USTREAS) -> Internal Revenue Service (IRS).
        /// 2. Select any link on the page e.g. "Actions on Decision"
        /// 3. Verify: Search and Summarize text is displayed on the landing page.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        public void AbilitySearchAndSummarizeTaxLinkTest()
        {
            string AdministrativeDecisionsAndGuidance = "Administrative Decisions & Guidance";

            string checkPageHeading = "Verify: Page heading is correct";
            string checkSelectedLink = "Verify: Selected link is correct";

            var contentTypeSearchResultsPage = this.GetHomePage<PrecisionHomePage>().BrowseTabPanel.SetActiveTab<ContentTypesTabPanel>(PrecisionBrowseTab.ContentTypes)
                .ClickLinkByText<CommonBrowsePage>(AdministrativeDecisionsAndGuidance)
                .GetAllLinks().First(link => link.Text.Contains("All Federal Administrative Decisions & Guidance")).Click<EdgeCommonBrowsePage>()
                .GetAllLinks().First(link => link.Text.Contains("Department of the Treasury (USTREAS)")).Click<EdgeCommonBrowsePage>()
                .GetAllLinks().First(link => link.Text.Contains("Internal Revenue Service (IRS)")).Click<EdgeCommonBrowsePage>();

            var selectedLink = this.SelectRandomLink(contentTypeSearchResultsPage);
            string selectedLinkText = selectedLink.Text;

            var nextTab = selectedLink.Click<EdgeCommonBrowsePage>();
            var actualLinkOpened = nextTab.GetBrowsePageTitle();

            this.TestCaseVerify.IsTrue(
                checkSelectedLink,
                actualLinkOpened.IndexOf(selectedLinkText, StringComparison.OrdinalIgnoreCase) >= 0,
                "Opened link does not match selected link. Selected: '{selectedLinkText}', Actual: '{actualLinkOpened}'");

            var aiAssistantPage = nextTab.SearchAndSummarizeTaxButton.Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Toolbar.IsDisplayed());

            this.TestCaseVerify.IsTrue(
                checkPageHeading,
                aiAssistantPage.Toolbar.HeadingLabel.Text.Equals(SearchAndSummarizeTax),
                "Heading is NOT correct");                       
        }

        /// <summary>
        /// Test Case 2171677: [Search & Summarize Tax]  Ability content info in History
        /// Description: Verify history events functionality in Search and Summarize Tax
        /// 1. Navigate to Search & Summarize tax page and start session with a specific question
        /// 2. Submit the question and wait for the response to be generated.
        /// 3. Expand the conversation history.
        /// 4. Verify: Content label is displayed as 'Content: Federal Administrative Tax Materials'
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniSmokeTestCategory)]
        public void SearchAndSummarizeTaxContentInfoHistoryTest()
        {
            string AdministrativeDecisionsAndGuidance = "Administrative Decisions & Guidance";
            const string Question = "What are the laws on genetic information of employees";
            string checkContentText = "Verify: Content label text in history";
            string expectedContentLabel = "Content: Federal Administrative Tax Materials";

            var contentTypeSearchResultsPage = this.GetHomePage<PrecisionHomePage>().BrowseTabPanel.SetActiveTab<ContentTypesTabPanel>(PrecisionBrowseTab.ContentTypes)
                .ClickLinkByText<CommonBrowsePage>(AdministrativeDecisionsAndGuidance)
                .GetAllLinks().First(link => link.Text.Contains("All Federal Administrative Decisions & Guidance")).Click<EdgeCommonBrowsePage>()
                .GetAllLinks().First(link => link.Text.Contains("Department of the Treasury (USTREAS)")).Click<EdgeCommonBrowsePage>()
                .GetAllLinks().First(link => link.Text.Contains("Internal Revenue Service (IRS)")).Click<EdgeCommonBrowsePage>();

            var aiAssistantPage = contentTypeSearchResultsPage.SearchAndSummarizeTaxButton.Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();

            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage = aiAssistantPage.ConversationHistory.ExpandButton.Click<AiAssistedResearchPage>();

            var contentLabelText = aiAssistantPage.ConversationHistory.Conversations.Select(conversation => conversation.ContentLabel.Text).ToList();

            this.TestCaseVerify.IsTrue(
            checkContentText,
            contentLabelText.ToList().First().Equals(expectedContentLabel),
            $" ContentLabel does not match. Expected: '{expectedContentLabel}', Actual: '{contentLabelText}'");
        }

        /// <summary>
        /// Description: Verify the functionality of the "Email me" button in Tax Skill
        /// 1. Navigate to the Search and Summarize Tax page.
        /// 2. Set the default jurisdiction and enter a question regarding ADA compliance for toilet paper dispensers.
        /// 3. Verify: Loading message and "Email me" button text are as expected while the response is being generated.
        /// 4. Click the "Email me" button and Verify: Message updates to confirm email notification and button is hidden.
        /// 5. Verify: "Email me" message and button are not displayed once the answer is ready.
        /// 6. Enter a follow-up question and Verify: "Email me" button is displayed for the follow-up question.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        public void SearchAndSummarizeEmailMeTest()
        {
            string administrativeDecisionsAndGuidance = "Administrative Decisions & Guidance";
            const string Question = "For paid family leave, are siblings of employees covered as family members ?";
            const string FollowUpQuestion = "Would a brother of an employee be considered family member for paid family leave?";
            const string LoadingMessage = "Loading your response, this may take a few moments...";

            string checkLoadingMessage = "Verify: Loading message is as expected";
            string checkStateAfterClickEmailMeButton = "Verify: 'Email me' message is changed, 'Email me' button isn't displayed";
            string checkStateWhenAnswerIsDisplayed = "Verify: 'Email me' and 'Email me' button aren't displayed when answer is displayed";
            string checkFollowUpEmailMeButton = "Verify: 'Email me' button is displayed for a follow-up";

            var contentTypeSearchResultsPage = this.GetHomePage<PrecisionHomePage>().BrowseTabPanel.SetActiveTab<ContentTypesTabPanel>(PrecisionBrowseTab.ContentTypes)
               .ClickLinkByText<CommonBrowsePage>(administrativeDecisionsAndGuidance)
               .GetAllLinks().First(link => link.Text.Contains("All Federal Administrative Decisions & Guidance")).Click<EdgeCommonBrowsePage>()
               .GetAllLinks().First(link => link.Text.Contains("Department of the Treasury (USTREAS)")).Click<EdgeCommonBrowsePage>()
               .GetAllLinks().First(link => link.Text.Contains("Internal Revenue Service (IRS)")).Click<EdgeCommonBrowsePage>();

            var aiAssistantPage = contentTypeSearchResultsPage.SearchAndSummarizeTaxButton.Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Toolbar.IsDisplayed());

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();

            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().EmailMeButton.Displayed);

            this.TestCaseVerify.IsTrue(
                checkLoadingMessage,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().EmailMeMessageLabel.Text.Substring(0, aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().EmailMeMessageLabel.Text.IndexOf("\r\n")).Equals(LoadingMessage)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().EmailMeButton.Text.Equals("Email me when response is ready"),
                "Loading message is NOT as expected");

            aiAssistantPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().EmailMeButton.Click<AiAssistedResearchPage>();

            this.TestCaseVerify.IsTrue(
                checkStateAfterClickEmailMeButton,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().EmailMeMessageLabel.Text.Equals($"{LoadingMessage}\r\nYou'll receive an email at {this.GetUserInfo().Email.ToLower()} when your response is ready.")
                && !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().EmailMeButton.Displayed,
                "'Email me' message is NOT changed, 'Email me' button is displayed");

            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsFalse(
                checkStateWhenAnswerIsDisplayed,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().EmailMeMessageLabel.Displayed
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().EmailMeButton.Displayed,
                "'Email me' and 'Email me' button are displayed when answer is displayed");

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(FollowUpQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();

            this.TestCaseVerify.IsTrue(
                checkFollowUpEmailMeButton,
                SafeMethodExecutor.ExecuteUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).EmailMeButton.Displayed),
                "'Email me' button is NOT displayed for a follow-up");
        }

        /// <summary>
        /// Test Case 2171677: [Search & Summarize Tax]  Ability content info in History
        /// Description: Verify history events functionality in Search and Summarize Tax
        /// 1. Navigate to Search & Summarize tax page and start session with a specific question
        /// 2. Submit the question and wait for the response to be generated.
        /// 3. Expand the conversation history.
        /// 4. Verify: Content label is displayed as 'Content: Federal Administrative Tax Materials'
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        public void SearchAndSummarizeTaxHistoryPageTest()
        {
            string AdministrativeDecisionsAndGuidance = "Administrative Decisions & Guidance";
            const string Question = "What are the laws on genetic information of employees";
            string checkSignOffPage = "Verify: AI Research event is present on History page";
            string checkRecentSearchEvent = "Verify: Recent search event contains question, event type and jurisdiction";
            string checkRecentSearchEventOpensConversation = "Verify: Recent search event click opens the recent conversation";
            string checkAiResearchFacet = "Verify: History event facet is applied";
            string checkHistoryPageEvent = "Verify: History page event contains question, event type and jurisdiction";
            string checkHistoryPageOpensConversation = "Verify: History page event click opens the recent conversation";
            var aiAssistantPage = this.GetHomePage<PrecisionHomePage>().BrowseTabPanel.SetActiveTab<ContentTypesTabPanel>(PrecisionBrowseTab.ContentTypes)
                .ClickLinkByText<CommonBrowsePage>(AdministrativeDecisionsAndGuidance).ToolsAndResourcesComponent.ClickLinkByText<AiAssistedResearchPage>("Search & Summarize Tax");

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();

            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            var signOffPage = (CommonSignOffPage)this.DefaultSignOnManager.SignOff();

            var evenOnSignOffPage = signOffPage.SignOffSessionDetailsComponent.GetSessionItems().First().GetDescriptionText().Replace(" ", string.Empty);
            this.TestCaseVerify.IsTrue(
                checkSignOffPage,
                evenOnSignOffPage.Equals($"{Question.Replace(" ", string.Empty)}\r\nSearch&SummarizeFederalAdministrativeTaxMaterials"),
                "AI Research event is NOT present on History page");

            var clientIdPage = this.SignOnBack();

            var eventDescription = clientIdPage.RecentResearchPane.RecentResearchList.First().Text.Replace(" ", string.Empty);

            this.TestCaseVerify.IsTrue(
                checkRecentSearchEvent,
                eventDescription.Equals($"{Question.Replace(" ", string.Empty)}\r\nSearch&SummarizeFederalAdministrativeTaxMaterials"),
                "Recent search event DOESN'T contain question, event type and jurisdiction");

            aiAssistantPage = clientIdPage.RecentResearchPane.RecentResearchList.First().ClickTitleLink<AiAssistedResearchPage>();

            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkRecentSearchEventOpensConversation,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.Remove(0, aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.IndexOf("says:") + 7).Equals(Question)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Count.Equals(1)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed,
                "Recent search event click DOESN'T open the recent conversation");

            aiAssistantPage = aiAssistantPage.ConversationHistory.ExpandButton.Click<AiAssistedResearchPage>();

            var historyPage = aiAssistantPage.ConversationHistory.GoToFullHistoryLink.Click<EdgeCommonHistoryPage>();

            BrowserPool.CurrentBrowser.CreateTab(HistoryPageTab);
            BrowserPool.CurrentBrowser.ActivateTab(HistoryPageTab);

            this.TestCaseVerify.IsTrue(
                checkAiResearchFacet,
                historyPage.NarrowPane.Filter.HistoryEventFacet.GetSelectedOptions().First().Equals("AI Research"),
                "History event facet is NOT applied");

            this.TestCaseVerify.AreEqual(
                checkHistoryPageEvent,
                $"{Question}Search&SummarizeFederalAdministrativeTaxMaterials".Replace(" ", string.Empty),
                $"{historyPage.HistoryTable.GetGridItems().First().Title}{historyPage.HistoryTable.GetGridItems().First().Summary}".Replace(" ", string.Empty),
                "History page event DOESN'T contains question, event type and jurisdiction");

            aiAssistantPage = historyPage.HistoryTable.GetGridItems().First().ClickLinkByText<AiAssistedResearchPage>(Question);

            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsFalse(
                checkEmailMeButtonIsNotDisplayed,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().EmailMeButton.Displayed,
                "'Email me' button is displayed when result is opened from history");

            this.TestCaseVerify.IsTrue(
                checkHistoryPageOpensConversation,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.Remove(0, aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.IndexOf("says:") + 7).Equals(Question)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Count.Equals(1)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed,
                "History page event click DOESN'T open the recent conversation");
        }

        /// <summary>
        /// Verify foldering Search and Summarize Tax results feature on Precision landing page
        /// 1. Sign in WL Precision
        /// 2. Go to Folders page and clear all foldered items in root folder
        /// 3. Go to Tax Skills page and ask question: What are the laws on genetic information of employees
        /// 4. Click Folder button and select root folder to save the AAR result
        /// 5. Check: Verify foldering successful message
        /// 6. Click Folder button and select root folder to save the search result again
        /// 7. Check: Verify foldering cannot add duplicate message
        /// 8. Go to Folders page and view root folder
        /// 9. Check: Verify Tax Skills search result is foldered with research question as foldered title
        /// 10.Check the checkbox next to the foldered Tax Skills search result and click Download button
        /// 11.Check: Verify InfoBox block message displayed when trying to download foldered Tax Skills search
        /// 12.Click foldered Tax Skills search result title (same as the research question). 
        /// 13.Check: Verify clicking foldered title takes to Search and Summarize Tax result page
        /// 14.Go to Folders page and clear all foldered items in root folder
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        public void SearchAndSummarizeTaxAddResultToFolderTest()
        {
            string administrativeDecisionsAndGuidance = "Administrative Decisions & Guidance";
            const string ExpectedDeliveryInfoMessage = "Your request contains AI-Assisted Research items, which are not deliverable.";
            const string Question = "What are the laws on genetic information of employees";
            const string checkFolderingSuccess = "Verify foldering is successful";
            const string checkFolderingDuplicacy = "Verify duplicate messages cannot be added in foldering";
            const string checkFolderedTitleForTaxSkillResult = "Verify Tax skills result is foldered with research question as foldered title";
            const string checkInfoBoxBlockMessage = "Verify InfoBox block message displayed when trying to download foldered research";
            const string checkPageHeading = "Foldered research leads to Search and Summarize Tax page.";
            string checkContentTextInFolder = "Verify: Content label text in Folder";
            string expectedContentLabelInFolder = "Federal Administrative Tax Materials";

            var folderPage = EdgeNavigationManager.Instance.GoToFoldersPage<EdgeResearchOrganizerPage>();
            folderPage.ClearFolderGrid();

            var homePage = this.GetHomePage<PrecisionHomePage>();
            homePage = homePage.Header.ClickLogo<PrecisionHomePage>();

            var contentTypeSearchResultsPage = this.GetHomePage<PrecisionHomePage>().BrowseTabPanel.SetActiveTab<ContentTypesTabPanel>(PrecisionBrowseTab.ContentTypes)
            .ClickLinkByText<CommonBrowsePage>(administrativeDecisionsAndGuidance)
            .GetAllLinks().First(link => link.Text.Contains("All Federal Administrative Decisions & Guidance")).Click<EdgeCommonBrowsePage>()
            .GetAllLinks().First(link => link.Text.Contains("Department of the Treasury (USTREAS)")).Click<EdgeCommonBrowsePage>()
            .GetAllLinks().First(link => link.Text.Contains("Internal Revenue Service (IRS)")).Click<EdgeCommonBrowsePage>();

            var aiAssistantPage = contentTypeSearchResultsPage.SearchAndSummarizeTaxButton.Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();

            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            var saveToFolderDialog = aiAssistantPage.Toolbar.SaveToFolderButton.Click<EdgeSaveToFolderDialog>();
            string rootFolder = saveToFolderDialog.FolderTreeComponent.GetRootFolderName();
            saveToFolderDialog.FolderTreeComponent.SelectFolderByName(rootFolder);

            saveToFolderDialog.ClickSaveButton<AiAssistedResearchPage>();
            string folderMessage = aiAssistantPage.FolderMessageLabel.Text;
            // This check indirectly tests: Bug 2032840: AALP: Foldering - Folder dialog remains after clicking Save button
            this.TestCaseVerify.IsTrue(
                checkFolderingSuccess,
                folderMessage.Contains(Question) && folderMessage.Contains("saved to"),
                "Saved to message not displayed");

            saveToFolderDialog = aiAssistantPage.Toolbar.SaveToFolderButton.Click<EdgeSaveToFolderDialog>();
            rootFolder = saveToFolderDialog.FolderTreeComponent.GetRootFolderName();
            saveToFolderDialog.FolderTreeComponent.SelectFolderByName(rootFolder);

            saveToFolderDialog.ClickSaveButton<AiAssistedResearchPage>();
            folderMessage = aiAssistantPage.FolderMessageLabel.Text;
            this.TestCaseVerify.IsTrue(
                checkFolderingDuplicacy,
                folderMessage.Contains(Question) && folderMessage.Contains("Cannot add duplicates"),
                "Research is not added to the folder");

            var recentFolderDialog = aiAssistantPage.Header.ClickHeaderTab<EdgeRecentFoldersDialog>(EdgeHeaderTabs.Folders);
            folderPage = recentFolderDialog.ClickFolderByName(rootFolder).ClickViewThisFolderButton();

            this.TestCaseVerify.IsTrue(
                checkFolderedTitleForTaxSkillResult,
                folderPage.FolderGrid.IsItemDisplayed(Question),
                "Tax skills result is not added to the folder");

            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.FolderContentLabel.Displayed);
            var contentLabelText = aiAssistantPage.FolderContentLabel.Text.Trim();

            this.TestCaseVerify.IsTrue(
            checkContentTextInFolder,
            contentLabelText.Equals(expectedContentLabelInFolder.Trim()),
            " ContentLabel does not match. Expected: '{expectedContentLabelInFolder}', Actual: '{contentLabelText}'");

            folderPage.FolderGrid.SelectItemByName(Question);
            folderPage.EdgeToolbar.DeliveryDropdown.SelectOption(DeliveryMethod.Download);
            folderPage.HoverOverQuickAccessPanel();//Download tooltip blocks the message. Move cursor elsewhere to see the message.
            string infoBoxMessage = folderPage.EdgeToolbar.AarInfoBoxMessageLabel.Text;

            this.TestCaseVerify.IsTrue(
                checkInfoBoxBlockMessage,
                infoBoxMessage.Contains(ExpectedDeliveryInfoMessage),
                "InfoBox Message not displayed when trying to deliver foldered research");

            SafeMethodExecutor.WaitUntil(() => folderPage.FolderGrid.IsItemDisplayed(Question));

            aiAssistantPage = folderPage.FolderGrid.ClickGridItemByName<AiAssistedResearchPage>(Question);
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);
            
            this.TestCaseVerify.IsFalse(
               checkEmailMeButtonIsNotDisplayed,
               aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().EmailMeButton.Displayed,
               "'Email me' button is displayed when result is opened from history");

            this.TestCaseVerify.IsTrue(
               checkPageHeading,
               aiAssistantPage.Toolbar.HeadingLabel.Text.Equals(SearchAndSummarizeTax),
               "Foldered research does not lead to Search and Summarize Tax page.");
          
            folderPage = EdgeNavigationManager.Instance.GoToFoldersPage<EdgeResearchOrganizerPage>();
            folderPage.ClearFolderGrid();
        }

        /// <summary>
        /// Verify each source number has a heading label against it in Search and Summarize Tax results 
        /// 1. Sign in WL Precision
        /// 2. Go to Search and Summarize Tax page and ask question: What is the definition of substantial evidence?
        /// 3. Submit the question and wait for the response to be generated.
        /// 4. Check: Verify each number item in response has a heading label against it.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        public void SearchAndSummarizeTaxSourceHeadersTest()
        {
            string AdministrativeDecisionsAndGuidance = "Administrative Decisions & Guidance";
            const string Question = "What is the definition of substantial evidence?";
            const string checkSourceHeadingLabel = "Numbered item is followed by Heading label.";
            const string checkCitationLabel = "Every numbered item has a citation label below it.";

            var contentTypeSearchResultsPage = this.GetHomePage<PrecisionHomePage>().BrowseTabPanel.SetActiveTab<ContentTypesTabPanel>(PrecisionBrowseTab.ContentTypes)
            .ClickLinkByText<CommonBrowsePage>(AdministrativeDecisionsAndGuidance)
            .GetAllLinks().First(link => link.Text.Contains("All Federal Administrative Decisions & Guidance")).Click<EdgeCommonBrowsePage>()
            .GetAllLinks().First(link => link.Text.Contains("Department of the Treasury (USTREAS)")).Click<EdgeCommonBrowsePage>()
            .GetAllLinks().First(link => link.Text.Contains("Internal Revenue Service (IRS)")).Click<EdgeCommonBrowsePage>();

            var aiAssistantPage = contentTypeSearchResultsPage.SearchAndSummarizeTaxButton.Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();

            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            var supportingMaterials = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials;
            var numberedItems = supportingMaterials.NumberedItemLabel.Select(label => label.Text).ToList();
            var headingLabels = supportingMaterials.NumberedHeadingLabels.Select(label => label.Text).ToList();
            var citationLabels = supportingMaterials.CitationLabel.Select(label => label.Text).ToList();

            var numberHeadingMap = contentTypeSearchResultsPage.CreateMap(numberedItems, headingLabels);

            foreach (var searchResultPoint in numberedItems)
            {
                string headingLabel = numberHeadingMap.ContainsKey(searchResultPoint) ? numberHeadingMap[searchResultPoint] : null;

                this.TestCaseVerify.IsFalse(
                checkSourceHeadingLabel,
                string.IsNullOrWhiteSpace(headingLabel),
                $"Heading label following numbered item '{searchResultPoint}' is empty or missing."
                );
            }

                this.TestCaseVerify.IsFalse(
                checkCitationLabel,
                citationLabels.Any(string.IsNullOrWhiteSpace),
                "One or more citation labels are empty or missing."
                );
        }

        /// <summary>
        /// Bug 2178820: [Search & Summarize Tax] Delivery Issues (email/download)
        /// Description: Verify print and email delivery options in Tax Skills
        /// 1. Navigate to the Search and Summarize Tax page and Verify: Delivery dropdown is not displayed on the landing page.
        /// 2. Submit a question about chapter 13 hardship discharge.
        /// 3. Verify: Delivery dropdown is disabled while the response is being generated.
        /// 4. If not in FedRamp environment, open the email dialog:
        ///    - Verify: Email options are correct.
        ///    - Verify: Email delivery works.
        /// 5. Open the print dialog:
        ///    - Verify: Print options are correct.
        ///    - Verify: Print delivery works.
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        public void SearchAndSummarizeTaxPrintAndEmailDeliveryTest()
        {
            string administrativeDecisionsAndGuidance = "Administrative Decisions & Guidance";
            const string Question = "What are the laws on genetic information of employees";
            string checkDeliveryDropdownIsNotDisplayed = "Verify: Delivery dropdown is not displayed on landing page";
           // string checkDeliveryDropdownIsDisabled = "Verify: Delivery dropdown is disabled";
            string checkEmailDialog = "Verify: Email options are correct";
            string checkEmailDeliveryWorks = "Verify: Email delivery works";
            string checkPrintDialog = "Verify: Print options are correct";
            string checkPrintDeliveryWorks = "Verify: Print delivery works";

            var contentTypeSearchResultsPage = this.GetHomePage<PrecisionHomePage>().BrowseTabPanel.SetActiveTab<ContentTypesTabPanel>(PrecisionBrowseTab.ContentTypes)
                .ClickLinkByText<CommonBrowsePage>(administrativeDecisionsAndGuidance)
                .GetAllLinks().First(link => link.Text.Contains("All Federal Administrative Decisions & Guidance")).Click<EdgeCommonBrowsePage>()
                .GetAllLinks().First(link => link.Text.Contains("Department of the Treasury (USTREAS)")).Click<EdgeCommonBrowsePage>()
                .GetAllLinks().First(link => link.Text.Contains("Internal Revenue Service (IRS)")).Click<EdgeCommonBrowsePage>();
            var aiAssistantPage = contentTypeSearchResultsPage.SearchAndSummarizeTaxButton.Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            this.TestCaseVerify.IsFalse(
               checkDeliveryDropdownIsNotDisplayed,
               aiAssistantPage.Toolbar.DeliveryDropdown.IsDeliveryDropdownDisplayed(),
               "Delivery dropdown is displayed on landing page");

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();

            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            var answer = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Select(item => this.CleanTextForCompare(item.AnswerLabel.Text)).ToList();

            var downloadDialog = aiAssistantPage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);
            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<AiAssistedResearchPage>();
            var fileName = $"Westlaw Precision - Westlaw Search And Summarize Tax - {DateTime.Now.ToString(DeliveryDateFormat)}.pdf";
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            var text = this.CleanTextForCompare(PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, fileName)));
            
            this.TestCaseVerify.IsTrue(
                "Verify deliveried doc has responce",
                answer.All(summary => text.Contains(summary)) ,
                $"Responce and document are not equal");
                      
            if (this.Settings.GetValue(EnvironmentConstants.IsFedRamp).ToLower().Equals("no"))
            {
                // Email dialog
                var emailDialog = aiAssistantPage.Toolbar.DeliveryDropdown.SelectOption<EmailDialog>(DeliveryMethod.Email);
                emailDialog.RecipientsTab.WhatToDeliver.SelectOption(WhatToDeliver.AiAssistedResearch);
                emailDialog.EnterEmailText("email@noemail.com");
                emailDialog.LayoutAndLimitsTab.SetIncludeSectionOption(LayoutAndLimitsInclude.CoverPage);

                this.TestCaseVerify.IsTrue(
                    checkEmailDialog,
                    emailDialog.GetDialogTitle().Equals("Email Research")
                    && emailDialog.RecipientsTab.IsEmailToTextboxDisplayed()
                    && emailDialog.RecipientsTab.IsEmailSubjectTextboxDisplayed()
                    && emailDialog.RecipientsTab.IsEmailNoteTextboxDisplayed()
                    && emailDialog.LayoutAndLimitsTab.CoverPageComment.Displayed,
                    "Email options are not correct");

                emailDialog.RecipientsTab.FormatDropdown.SelectOption(DeliveryFormat.Pdf);

                this.TestCaseVerify.IsTrue(
                    checkEmailDeliveryWorks,
                    emailDialog.ClickEmailButton<EmailDialog>().IsTextPresented("Ready For Email"),
                    "Email delivery doesn't work");
            }

            // Print dialog
            var printDialog = aiAssistantPage.Toolbar.DeliveryDropdown.SelectOption<PrintDialog>(DeliveryMethod.Print);
            printDialog.TheBasicsTab.WhatToDeliver.SelectOption(WhatToDeliver.AiAssistedResearch);

            this.TestCaseVerify.IsTrue(
                checkPrintDialog,
                printDialog.GetDialogTitle().Equals("Print Research")
                && printDialog.TheBasicsTab.WhatToDeliver.IsOptionSelected(WhatToDeliver.AiAssistedResearch),
                "Print options are NOT correct");

            printDialog.ClickPrintButton();

            bool printDialogIsClosed = printDialog.CloseBrowserPrintDialog(this.TestExecutionContext.TestClient.Id,
               () => this.TestCaseVerify.IsTrue(
                        checkPrintDeliveryWorks,
                        printDialog.IsLoadingPrintLightBoxDisplayed(50000),
                        "Print delivery doesn't work"));
        }

        ///<summary>
        /// Description: Verify recent questions dropdown functionality in AI Assistant
        /// 1. Verify: Recent questions button is displayed on the Home page.
        /// 2. Submit a question and Verify: Recent questions button is disabled while the answer is generating.
        /// 3. Verify: Recent questions button is not displayed after the answer is ready.
        /// 4. Start a new research and Verify: Asked question appears in the recent questions dropdown.
        /// 5. Navigate back to the Home page and Verify: Asked question appears in the dropdown on the Home page.
        /// 6. Select the question from the dropdown and Verify: Ability to run the question selected from the dropdown.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void SearchAndSummarizeRecentQuestionsDropdownTest()
        {
            string AdministrativeDecisionsAndGuidance = "Administrative Decisions & Guidance";

            const string Question = "What are the laws on genetic information of employees";
       //     const string SecondQuestion = "What are the laws on genetic information of employees&";

            string checkRecentQuestionsIsDisabled = "Verify: Recent questions button is disabled when answer is generating";
            string checkRecentQuestionsIsNotDisplayed = "Verify: Recent questions button is not displayed after answer is ready";
            string checkQuestionAppearsInRecentQuestions = "Verify: Asked question appears in dropdown";

            var contentTypeSearchResultsPage = this.GetHomePage<PrecisionHomePage>().BrowseTabPanel.SetActiveTab<ContentTypesTabPanel>(PrecisionBrowseTab.ContentTypes)
                .ClickLinkByText<CommonBrowsePage>(AdministrativeDecisionsAndGuidance)
                .GetAllLinks().First(link => link.Text.Contains("All Federal Administrative Decisions & Guidance")).Click<EdgeCommonBrowsePage>()
                .GetAllLinks().First(link => link.Text.Contains("Department of the Treasury (USTREAS)")).Click<EdgeCommonBrowsePage>()
                .GetAllLinks().First(link => link.Text.Contains("Internal Revenue Service (IRS)")).Click<EdgeCommonBrowsePage>();

            var aiAssistantPage = contentTypeSearchResultsPage.SearchAndSummarizeTaxButton.Click<AiAssistedResearchPage>();

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Toolbar.IsDisplayed());

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();

            this.TestCaseVerify.IsFalse(
                checkRecentQuestionsIsDisabled,
                aiAssistantPage.QueryBox.RecentQuestionsDropdown.IsDropdownEnabled(),
                "Recent questions button is active when answer is generating");

            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsFalse(
               checkRecentQuestionsIsNotDisplayed,
               aiAssistantPage.QueryBox.RecentQuestionsDropdown.IsDisplayed(),
               "Recent questions button is displayed after answer is ready");

            aiAssistantPage = aiAssistantPage.Toolbar.NewResearchButton.Click<AiAssistedResearchPage>();

            if (!(aiAssistantPage.QueryBox.RecentQuestionsDropdown.Options.First().Equals(Question)))
            {
                SafeMethodExecutor.WaitUntil(() =>
                {
                    aiAssistantPage = BrowserPool.CurrentBrowser.Refresh<AiAssistedResearchPage>();
                    bool questionPresent = aiAssistantPage.QueryBox.RecentQuestionsDropdown.Options.First().Equals(Question);
                    return questionPresent;
                }, timeoutFromSec: 300, pollingIntervalInMilliseconds: 2000);
            }

            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.QueryBox.RecentQuestionsDropdown.IsDisplayed());

            this.TestCaseVerify.IsTrue(
                checkQuestionAppearsInRecentQuestions,
                aiAssistantPage.QueryBox.RecentQuestionsDropdown.Options.First().Equals(Question),
                "Asked question doesn't appear in dropdown");
        }

        /// <summary>
        /// Verify the search results header labels are displaying proper title in Search and Summarize Tax results 
        /// 1. Sign in WL Precision
        /// 2. Go to Search and Summarize Tax page and ask question: Find all IRS administrative materials on the Disaster Relief act starting with 2020.
        /// 3. Submit the question and wait for the response to be generated.
        /// 4. Check: Verify each number item in response has a heading label against it.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        public void SearchAndSummarizeTaxSearchResultHeadingLabelTest()
        {
            string AdministrativeDecisionsAndGuidance = "Administrative Decisions & Guidance";
            const string Question = "What is the definition of substantial evidence?";
            const string checkSearchResultHeadingLabel = "No document title contains label as 'Title Available on Document'.";
            string InvalidHeadingLabel = "Title Available on Document";
            var ShowMoreResultsText = "Show more results";

            var contentTypeSearchResultsPage = this.GetHomePage<PrecisionHomePage>().BrowseTabPanel.SetActiveTab<ContentTypesTabPanel>(PrecisionBrowseTab.ContentTypes)
            .ClickLinkByText<CommonBrowsePage>(AdministrativeDecisionsAndGuidance)
            .GetAllLinks().First(link => link.Text.Contains("All Federal Administrative Decisions & Guidance")).Click<EdgeCommonBrowsePage>()
            .GetAllLinks().First(link => link.Text.Contains("Department of the Treasury (USTREAS)")).Click<EdgeCommonBrowsePage>()
            .GetAllLinks().First(link => link.Text.Contains("Internal Revenue Service (IRS)")).Click<EdgeCommonBrowsePage>();

            var aiAssistantPage = contentTypeSearchResultsPage.SearchAndSummarizeTaxButton.Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();

            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage.ScrollPageToBottom();
            var ShowResultsButton = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ShowResultsButton;
            ClickButtonIfVisibleAndTextMatches(ShowResultsButton, ShowMoreResultsText);

            var supportingMaterials = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials;
            var headingLabels = supportingMaterials.NumberedHeadingLabels.Select(label => label.Text).ToList();

            this.TestCaseVerify.IsFalse(
            checkSearchResultHeadingLabel,
            headingLabels.Any(label => label == InvalidHeadingLabel),
            "One or more heading labels is invalid."
            );
        }

        /// <summary>
        /// Selects random link in the given page.
        /// </summary>
        private dynamic SelectRandomLink(CommonBrowsePage browserPage)
        {
            var allLinks = browserPage.GetAllLinks().ToList();
            if (allLinks == null || allLinks.Count == 0)
            {
                Assert.Fail("No links found on the page to select.");
            }

            var random = new Random();
            int randomIndex = random.Next(allLinks.Count);
            return allLinks[randomIndex];
        }

        private void ClickButtonIfVisibleAndTextMatches(dynamic button, string expectedText)
        {
            DriverExtensions.WaitForJavaScript();

            if (button != null && button.Displayed && button.Enabled && button.Text.Equals(expectedText))
            {
                button.Click();
            }
        }

        protected override void InitializeRoutingPageSettings()
        {
            this.Settings.AppendValues(
                EnvironmentConstants.InfrastructureAccessControlsOn,
                SettingUpdateOption.Append,
                "IAC-AI-TAX-SKILL");

            if (this.TestContext.Properties["AiResearchTax"] != null && this.TestContext.Properties["AiResearchTax"].Equals("On"))
            {
                this.Settings.AppendValues(
                    EnvironmentConstants.FeatureAccessControlsOn,
                    SettingUpdateOption.Append,
                    FeatureAccessControl.AIResearchTax);
            }
            else
            {
                base.InitializeRoutingPageSettings();
            }

        }

        protected override void OnManageCredential()
        {
            base.OnManageCredential();
            this.DefaultSignOnContext.RoutingSettingsInfo.RoutingTextboxSettings[RoutingSettingTextbox.CategoryPageCollectionSet] = "w_cb_catpagesqa_cs";
        }

        private string CleanTextForCompare(string text)
        {
            text = text.Replace(" ", string.Empty).Replace(")", string.Empty).Replace("\r\n", string.Empty);
            text = Regex.Replace(text, @"\b\d+\.\b", string.Empty);
            text = Regex.Replace(text,$"WestlawSearch&SummarizeTax•Responsegenerated:{DateTime.Now:MMMMd,yyyy}WestlawPrecision.©{DateTime.Now:yyyy}ThomsonReuters.NoclaimtooriginalU.S.", string.Empty).Replace("•", string.Empty);

            return text;
        }
    }
}
