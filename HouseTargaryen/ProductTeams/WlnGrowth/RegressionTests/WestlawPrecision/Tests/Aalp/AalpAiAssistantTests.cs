namespace WestlawPrecision.Tests.Aalp
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Products.Shared.Dialogs.Delivery;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Enums.RI;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Products.WestlawEdge.Components.BrowseWidget;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel;
    using Framework.Common.UI.Products.WestlawEdge.Components.Preferences;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Foldering;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.HomePage;
    using Framework.Common.UI.Products.WestlawEdge.Enums.NarrowPanel;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage.HomePageTabs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.AALP;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.AALP;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.ResultList;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
    using Framework.Common.UI.Products.WestLawNext.Enums.Delivery;
    using Framework.Common.UI.Products.WestLawNext.Utils;
    using Framework.Common.UI.Raw.EnhancementTests.Utils;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Header;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Preferences;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Security;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.DataModel.Security.Specialized;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.Extensions;
    using Framework.Core.Utils.IO;
    using global::WestlawPrecision.Utilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keys = OpenQA.Selenium.Keys;
    using PrintDialog = Framework.Common.UI.Products.Shared.Dialogs.Delivery.PrintDialog;

    /// <summary>
    /// AALP common tests
    /// </summary>
    [TestClass]
    public class AalpAiAssistantTests : AalpBaseTest
    {
        private const string FeatureTestCategory = "AiLandingPage";

        /// <summary>
        /// Test case: 1866096
        /// Description: Verify AI Assistant is not displayed when AiResearch FAC is off
        /// 1. Navigate to the Westlaw Precision homepage.
        /// 2. Ensure that the AiResearch FAC (Feature Access Control) setting is turned off.
        /// 3. Verify that the AI Assistant tab is not visible in the Browse Tab Panel.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        [TestProperty("AiResearch", "Off")]
        public void NoAiAssistantIfFacOffTest()
        {
            string checkAiAssistantUnavailable = "Verify: Ai Assistant is unavailable in Westlaw Precision if FAC off";

            this.TestCaseVerify.IsFalse(
                checkAiAssistantUnavailable,
                this.GetHomePage<PrecisionHomePage>().BrowseTabPanel.IsDisplayed(PrecisionBrowseTab.AiAssistedResearch),
                "Ai Assistant is available in Westlaw Precision if FAC off");
        }

        /// <summary>
        /// Test case: 1876459
        /// Description: Verify feedback is not displayed when DoNotTrain FAC is on
        /// 1. Navigate to the Westlaw Precision homepage.
        /// 2. Access the AI Assisted Research tab.
        /// 3. Ensure that the AiDoNotTrain FAC (Feature Access Control) setting is turned on.
        /// 4. Enter a question regarding family leave in the question textbox.
        /// 5. Submit the question and wait for the AI response.
        /// 6. Verify that feedback options (Helpful Yes/No buttons) are not displayed in the AI response.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        [TestProperty("AiDoNotTrain", "On")]
        public void AiNoFeedbackIfDoNotTrainFacOnTest()
        {
            const string Question = "For paid family leave, are siblings of employees covered as family members?";

            string checkNoFeedback = "Verify: Feedback isn't presented if DoNotTrain turned on";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            var aiAssistedResearchTab = homePage.BrowseTabPanel.SetActiveTab<AiAssistedResearchTabPanel>(PrecisionBrowseTab.AiAssistedResearch);
            aiAssistedResearchTab = aiAssistedResearchTab.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchTabPanel>();
            aiAssistedResearchTab = aiAssistedResearchTab.QuestionTextbox.SetText<AiAssistedResearchTabPanel>(Question);
            var aiAssistantPage = aiAssistedResearchTab.SubmitButton.Click<AiAssistedResearchPage>();

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsFalse(
                checkNoFeedback,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().HelpfulYesButton.Displayed
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().HelpfulNoButton.Displayed,
                "Feedback is still presented if DoNotTrain turned on");
        }

        /// <summary>
        /// Test case: 1864390
        /// Description: Verify error page is shown for bad links in AI Assistant
        /// 1. Construct a bad data link using the base URL for the application.
        /// 2. Navigate to the bad data link and verify that the error page is displayed with the correct description.
        /// 3. Verify that the "New conversation" button on the error page navigates to the AI Assistant landing page.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        [TestCategory("Bug 1876989")]
        public void AiAssistantErrorPageTest()
        {
            var baseUrl = TestConfigurationRepository.DefaultInstance.FindEndpoint(TestConfigurationRepository.DefaultInstance.FindModule(CobaltModuleId.Website),
                TestConfigurationRepository.DefaultInstance.FindProduct(this.DefaultCobaltProduct.Id), this.TestExecutionContext.TestEnvironment).Uri;

            var badDataLink = $"{baseUrl}/Conversation/LandingPage/conversaTTTion/454b099c-fb08-4043-9fd5-6ef2b637976e";
            //var expiredLink = $"{baseUrl}.westlaw.com/Conversation/LandingPage/conversation/000b000a-aa00-0000-0aa0-0aa0a00000a";

            string checkBadDataLinkErrorPage = "Verify: AiAssistant bad data link error page description is as expected";
            string checkBadDataPageButtonLeadsToLandingPage = "Verify: 'New conversation' button from the bad data error page leads to AiAssistant landing page";
            //string checkExpiredLinkErrorPage = "Verify: AiAssistant expired link error page description is as expected";
            //string checkExpiredPageButtonLeadsToLandingPage = "Verify: 'New conversation' button from the expired error page leads to AiAssistant landing page";

            var aiAssistantErrorPage = BrowserPool.CurrentBrowser.GoToUrl<AiAssistedResearchErrorPage>(badDataLink);

            this.TestCaseVerify.IsTrue(
                checkBadDataLinkErrorPage,
                aiAssistantErrorPage.ErrorPageHeaderLabel.Text.Equals("An unexpected error occurred")
                && aiAssistantErrorPage.ErrorPageMessageLabel.Text.Equals("This page did not load due to an application issue."),
                "AiAssistant bad data link error page description is NOT as expected");

            var aiAssistantPage = aiAssistantErrorPage.NewResearchButton.Click<AiAssistedResearchPage>();

            this.TestCaseVerify.IsTrue(
                checkBadDataPageButtonLeadsToLandingPage,
                aiAssistantPage.Toolbar.IsDisplayed()
                && aiAssistantPage.Chat.IsDisplayed()
                && aiAssistantPage.ConversationHistory.IsDisplayed()
                && aiAssistantPage.QueryBox.IsDisplayed(),
                "'New conversation' button from the bad data error page DOESN'T lead to AiAssistant landing page");
            //Bug 1876989
            //aiAssistantErrorPage = BrowserPool.CurrentBrowser.GoToUrl<AiAssistantErrorPage>(expiredLink);

            //SafeMethodExecutor.WaitUntil(() => aiAssistantErrorPage.NewResearchButton.Displayed);

            //this.TestCaseVerify.IsTrue(
            //    checkExpiredLinkErrorPage,
            //    aiAssistantErrorPage.ErrorPageHeaderLabel.Text.Equals("AI-assisted research unavailable")
            //    && aiAssistantErrorPage.ErrorPageMessageLabel.Text.Equals("This AI-assisted research cannot be loaded."),
            //    "AiAssistant expired link error page description is NOT as expected");

            //aiAssistantPage = aiAssistantErrorPage.NewResearchButton.Click<AiAssistantPage>();

            //SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.LandingPageLabel.Displayed);

            //this.TestCaseVerify.IsTrue(
            //    checkExpiredPageButtonLeadsToLandingPage,
            //    aiAssistantPage.Toolbar.IsDisplayed()
            //    && aiAssistantPage.Chat.IsDisplayed()
            //    && aiAssistantPage.ConversationHistory.IsDisplayed()
            //    && aiAssistantPage.QueryBox.IsDisplayed(),
            //    "'New conversation' button from the expired error page DOESN'T lead to AiAssistant landing page");
        }

        /// <summary>
        /// Test case: 1843222, 1860963, 1842971, 1863688, 1866948, 1896555
        /// Description: Verify common functionality of AI Assistant including info dialogs, headings, browser tab name, and links
        /// 1. Navigate to the AI-Assisted Research tab on the homepage.
        /// 2. Verify: "Learn more" dialog text on the homepage is as expected.
        /// 3. Verify: Information buttons are displayed.
        /// 4. Verify: 'Send' button is disabled for an empty (whitespace) question on the homepage.
        /// 5. Verify: You cannot submit an empty question using the 'Enter' key on the homepage.
        /// 6. Verify: AI-Assisted feature description on the homepage.
        /// 7. Navigate to the AI Assistant page and Verify: Browser tab title and page heading are correct.
        /// 8. Verify: Welcome text is displayed on the landing page.
        /// 9. Verify: "Learn more" dialog text on the AI Assistant page is as expected.
        /// 10. Verify: "How AI-Assisted Research works" dialog title and content.
        /// 11. Verify: AI Court Rules page opens correctly.
        /// 12. Verify: "Tips for best results" dialog title and content.
        /// 13. Verify: 'Send' button is disabled for an empty question on the AI Assistant page.
        /// 14. Verify: You cannot submit an empty question using the 'Enter' key on the AI Assistant page.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantCommonTest()
        {
            const string AiCourtRulesTab = "Ai Court Rules tab";
            const string EmptyQuestion = "         ";
            const string HowAiWorksDialogTitle = "How AI-Assisted Research works";
            const string TipsForBestResultsDialogTitle = "Tips for best results";
            const string AiAssistantWidgetDescription = "Conversational AI can support legal research by providing insights into legal questions along with quick access to supporting materials from primary law.";

            string checkLearnMoreLinkDialogHomePageText = "Verify: Learn more dialog text is as expected (AI-Assisted Research tab on the home page)";
            string checkInfoButtonsAreDisplayed = "Verify: Info buttons are displayed";
            string checkSendButtonDisabledHomePage = "Verify: 'Send' button is disabled for empty (whitespace) question (AI-Assisted Research tab on the home page)";
            string checkUnableToAskViaEnterHomePage = "Verify: Unable to ask empty (whitespace) question via 'Enter' button tap (AI-Assisted Research tab on the home page)";
            string checkAiAssistantFeatureDescription = "Verify: AI-Assisted feature description is as expected";
            string checkBrowserTabTitle = "Verify: Browse tab title is correct";
            string checkPageHeading = "Verify: Page heading is correct";
            string checkWelcomeLandingPageTextIsDisplayed = "Verify: Welcome text is displayed";
            string checkLearnMoreLinkDialogLandingPageText = "Verify: Learn more dialog text is as expected (AI Assistant page)";
            string checkHowAiAssistedResearchWorksDialogText = "Verify: 'How AI-Assisted Research works' dialog title and content is as expected";
            string checkAiCourtRulesPage = "Verify: AI Court Rules page is opened";
            string checkTipsForBestResultsDialogText = "Verify: 'Tips for best results' dialog title and content is as expected";
            string checkSendButtonDisabled = "Verify: 'Send' button is disabled for empty (whitespace) question";
            string checkUnableToAskViaEnter = "Verify: Unable to ask empty (whitespace) question via 'Enter' button tap";

            var homePage = this.GetHomePage<PrecisionHomePage>();

            var aiAssistedResearchTab = homePage.BrowseTabPanel.SetActiveTab<AiAssistedResearchTabPanel>(PrecisionBrowseTab.AiAssistedResearch);

            var howAiWorksDialog = aiAssistedResearchTab.LearnMoreLink.Click<HowAiAssistedResearchWorksDialog>();

            this.TestCaseVerify.IsTrue(
                checkLearnMoreLinkDialogHomePageText,
                howAiWorksDialog.TitleLabel.Text.Equals(HowAiWorksDialogTitle)
                && howAiWorksDialog.DescriptionLabel.Text.Any(),
                "Learn more dialog text is NOT as expected (AI-Assisted Research tab on the home page)");

            aiAssistedResearchTab = howAiWorksDialog.CloseButton.Click<AiAssistedResearchTabPanel>();

            this.TestCaseVerify.IsTrue(
                checkInfoButtonsAreDisplayed,
                aiAssistedResearchTab.HowAiWorksButton.Displayed
                && aiAssistedResearchTab.TipsForBestResultsButton.Displayed,
                "Info button are NOT displayed");

            aiAssistedResearchTab = aiAssistedResearchTab.QuestionTextbox.SetText<AiAssistedResearchTabPanel>(EmptyQuestion);

            this.TestCaseVerify.IsFalse(
                checkSendButtonDisabledHomePage,
                aiAssistedResearchTab.SubmitButton.Enabled,
                "'Send' button is NOT disabled for empty (whitespace) question (AI-Assisted Research tab on the home page)");

            aiAssistedResearchTab = aiAssistedResearchTab.QuestionTextbox.SetText<AiAssistedResearchTabPanel>(Keys.Enter);

            this.TestCaseVerify.IsTrue(
                checkUnableToAskViaEnterHomePage,
                aiAssistedResearchTab.IsDisplayed(),
                "Able to ask empty (whitespace) question via 'Enter' button tap (AI-Assisted Research tab on the home page)");

            this.TestCaseVerify.AreEqual(
                checkAiAssistantFeatureDescription,
                AiAssistantWidgetDescription,
                homePage.FeaturesIncludedPanel.GetWidgetTextByTitle(AIAssistedResearchHeadingLabel),
                "AI-Assisted feature description is NOT as expected");

            var aiAssistantPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Toolbar.IsDisplayed());

            this.TestCaseVerify.IsTrue(
               checkBrowserTabTitle,
               BrowserPool.CurrentBrowser.Title.Equals("Westlaw AI-Assisted Research | Westlaw Precision"),
               "Browser tab title is not correct");

            this.TestCaseVerify.IsTrue(
               checkPageHeading,
               aiAssistantPage.Toolbar.HeadingLabel.Text.Equals(AIAssistedResearchHeadingLabel),
               "Heading is NOT correct");

            this.TestCaseVerify.IsTrue(
                checkWelcomeLandingPageTextIsDisplayed,
                aiAssistantPage.Chat.LandingPageLabel.Displayed,
                "Welcome text is NOT displayed");

            howAiWorksDialog = aiAssistantPage.Chat.HowAiWorksLink.Click<HowAiAssistedResearchWorksDialog>();

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
        }

        /// <summary>
        /// Test case: 1861439, 1861461, 1863540, 1885497
        /// Description: Verify the functionality of the "Email me" button in AI Assistant
        /// 1. Navigate to the AI Assistant page.
        /// 2. Set the default jurisdiction and enter a question regarding ADA compliance for toilet paper dispensers.
        /// 3. Verify: Loading message and "Email me" button text are as expected while the response is being generated.
        /// 4. Click the "Email me" button and Verify: Message updates to confirm email notification and button is hidden.
        /// 5. Verify: "Email me" message and button are not displayed once the answer is ready.
        /// 6. Enter a follow-up question and Verify: "Email me" button is displayed for the follow-up question.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantEmailMeTest()
        {
            const string Question = "How close must a toilet paper dispenser be to a toilet under the ADA?";
            const string FollowUpQuestion = "What distance does a toilet paper dispenser have to be from the toilet to be compliant with ADA accessibility guidelines?";
            const string LoadingMessage = "Loading your response, this may take a few moments...";

            string checkLoadingMessage = "Verify: Loading message is as expected";
            string checkStateAfterClickEmailMeButton = "Verify: 'Email me' message is changed, 'Email me' button isn't displayed";
            string checkStateWhenAnswerIsDisplayed = "Verify: 'Email me' and 'Email me' button aren't displayed when answer is displayed";
            string checkFollowUpEmailMeButton = "Verify: 'Email me' button is displayed for a follow-up";

            var homePage = this.GetHomePage<PrecisionHomePage>();

            var aiAssistantPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchPage>();
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
        /// Test case: 1730755, 1869133, 1896558
        /// Description: Verify the functionality of the jurisdiction and new research buttons in AI Assistant
        /// 1. Navigate to the AI Assistant page and select Minnesota as the jurisdiction.
        /// 2. Enter a question regarding JFPA claims and initiate a new research.
        /// 3. Verify: "New Conversation" button is disabled while the response is generating.
        /// 4. Verify: Jurisdiction button displays as a label when the response is generating.
        /// 5. Verify: Jurisdiction infobox text on the Toolbar is correct.
        /// 6. Verify: Jurisdiction infobox text on the Query box is correct.
        /// 7. Verify: Jurisdiction button remains disabled when the answer is ready.
        /// 8. Verify: Metadata is correct and corresponds to the selected jurisdiction (Minnesota).
        /// 9. Click "New research" and Verify: Conversation is cleared.
        /// 10. Verify: Jurisdiction becomes a button again after starting new research.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantNewResearchTest()
        {
            const string Question = "Can JFPA claims be brought as a class action?";

            string checkNewConversationButtonIsDisabled = "Verify: New Conversation button is disabled";
            string checkJurisdictionBecomesLabel = "Verify: Jurisdiction button displays as label when response is generating";
            string checkJurisdictionInfoIconOnToolbar = "Verify: Jurisdiction infobox text on Toolbar";
            string checkJurisdictionInfoIconOnQueryBox = "Verify: Jurisdiction infobox text on Query box";
            string checkJurisdictionIsStillDisabled = "Verify: Jurisdiction button is disabled when answer is ready";
            string checkMetadata = "Verify: Metadata is correct and corresponding to selected jurisdiction";
            string checkConversationIsCleared = "Verify: Conversation is cleared";
            string checkJurisdictionBecomesButton = "Verify: Jurisdiction becomes button again";

            var homePage = this.GetHomePage<PrecisionHomePage>();

            var aiAssistantPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.QueryBox.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectJurisdictions(true, Jurisdiction.Minnesota).SaveButton.Click<AiAssistedResearchPage>();
            aiAssistantPage.QueryBox.QuestionTextbox.SetText(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            
            this.TestCaseVerify.IsTrue(
               checkNewConversationButtonIsDisabled,
               !aiAssistantPage.Toolbar.NewResearchButton.Enabled
               && !aiAssistantPage.QueryBox.NewResearchButton.Displayed
               && aiAssistantPage.Toolbar.NewResearchButton.Text.Equals("New research"),
               "New Conversation button is NOT disabled");

            this.TestCaseVerify.IsTrue(
               checkJurisdictionBecomesLabel,
               aiAssistantPage.Toolbar.JurisdictionLabel.Text.Contains($"{Jurisdiction.Minnesota.GetEnumTextValue()}"),
               "Jurisdiction button DOESN'T display as label when response is generating");
           
            aiAssistantPage.Toolbar.JurisdictionInfoIconButton.Click<AiAssistedResearchPage>();
            this.TestCaseVerify.AreEqual(
              checkJurisdictionInfoIconOnToolbar,
              "Selected jurisdiction(s): Minnesota\r\nJurisdiction can be set when starting new AI-assisted research.",
              aiAssistantPage.Toolbar.JurisdictionInfoBox.Text,
              "Jurisdiction infobox text is NOT correct on Toolbar");

            aiAssistantPage.QueryBox.JurisdictionInfoIconButton.Click<AiAssistedResearchPage>();
            this.TestCaseVerify.AreEqual(
              checkJurisdictionInfoIconOnQueryBox,
              "Selected jurisdiction(s): Minnesota\r\nJurisdiction can be set when starting new AI-assisted research.",
              aiAssistantPage.QueryBox.JurisdictionInfoBox.Text,
              "Jurisdiction infobox text is NOT correct on Query box");

            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkJurisdictionIsStillDisabled,
                aiAssistantPage.Toolbar.JurisdictionLabel.Text.Contains($"{Jurisdiction.Minnesota.GetEnumTextValue()}")
                && aiAssistantPage.QueryBox.JurisdictionLabel.Text.Contains($"{Jurisdiction.Minnesota.GetEnumTextValue()}"),
                "Jurisdiction button is NOT disabled when answer is ready");

              var metadataList = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SupportingMaterialsItems.Select(item => item.MetadataLabel.Text).ToList();

               this.TestCaseVerify.IsTrue(
               checkMetadata,
               metadataList.Any(metadata => metadata.Contains($"{Jurisdiction.Minnesota.GetEnumTextValue()}")),
               "Metadata is NOT correct");

            aiAssistantPage = aiAssistantPage.Toolbar.NewResearchButton.Click<AiAssistedResearchPage>();

            this.TestCaseVerify.IsFalse(
               checkConversationIsCleared,
               aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Any(),
               "Conversation is NOT cleared");

            this.TestCaseVerify.IsTrue(
               checkJurisdictionBecomesButton,
               aiAssistantPage.Toolbar.JurisdictionButton.Displayed
               && aiAssistantPage.Toolbar.JurisdictionButton.Text.Contains($"{Jurisdiction.Minnesota.GetEnumTextValue()}"),
               "Jurisdiction DOESN'T become button again");
        }

        /// <summary>
        /// Test case: 1731198
        /// Description: Verify the functionality of the Feedback form in AI Assistant
        /// 1. Navigate to the AI-Assisted Research tab and enter a question about paid family leave.
        /// 2. Submit the question and wait for the response to be generated.
        /// 3. Verify: 'Send Request' button is disabled by default after clicking 'Yes'.
        /// 4. Click 'Cancel' in the feedback form and Verify: Feedback dialog is not displayed after canceling (for 'Yes' button).
        /// 5. Enter feedback, send it, and Verify: 'Yes' button is disabled after sending feedback.
        /// 6. Submit the question again, wait for the response, and click 'No'.
        /// 7. Verify: 'Send Request' button is disabled by default after clicking 'No'.
        /// 8. Click 'Cancel' in the feedback form and Verify: Feedback dialog is not displayed after canceling (for 'No' button).
        /// 9. Enter feedback, send it, and Verify: 'No' button is disabled after sending feedback.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        [TestCategory(TeamSahniSmokeTestCategory)]
        public void AiAssistantFeedbackTest()
        {
            const string Question = "For paid family leave, are siblings of employees covered as family members?";
            const string FeedbackMessage = "Test feedback";

            string checkYesSendRequestButtonDefaultDisabled = "Verify: 'Send Request' button is disabled by default after clicking 'Yes'";
            string checkFeedbackFormCancelledForYes = "Verify: Feedback dialog isn't displayed after clicking 'Cancel' button (for 'Yes' button)";
            string checkYesButtonDisabledAfterFeedback = "Verify: 'Yes' button is disabled after send feedback";
            string checkNoSendRequestButtonDefaultDisabled = "Verify: 'Send Request' button is disabled by default after clicking 'No'";
            string checkFeedbackFormCancelledForNo = "Verify: Feedback dialog isn't displayed after clicking 'Cancel' button (for 'No' button)";
            string checkNoButtonDisabledAfterFeedback = "Verify: 'No' button is disabled after send feedback";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            var aiAssistedResearchTab = homePage.BrowseTabPanel.SetActiveTab<AiAssistedResearchTabPanel>(PrecisionBrowseTab.AiAssistedResearch);
            aiAssistedResearchTab = aiAssistedResearchTab.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchTabPanel>();
            aiAssistedResearchTab = aiAssistedResearchTab.QuestionTextbox.SetText<AiAssistedResearchTabPanel>(Question);
            var aiAssistantPage = aiAssistedResearchTab.SubmitButton.Click<AiAssistedResearchPage>();

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            SafeMethodExecutor.WaitUntil(() =>
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Any() &&
                !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed
            );
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().HelpfulYesButton.Enabled);

            aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().HelpfulYesButton.ScrollToElement();
            aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().HelpfulYesButton.Click();
            var feedbackForm = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().FeedbackForm;

            this.TestCaseVerify.IsFalse(
               checkYesSendRequestButtonDefaultDisabled,
               feedbackForm.SendFeedbackButton.Enabled,
               "'Send Request' button is NOT disabled by default after clicking 'Yes'");

            feedbackForm.CancelButton.ScrollToElement();
            aiAssistantPage = feedbackForm.CancelButton.Click<AiAssistedResearchPage>();

            this.TestCaseVerify.IsFalse(
               checkFeedbackFormCancelledForYes,
               feedbackForm.IsDisplayed(),
               "Feedback dialog is displayed after clicking 'Cancel' button (for 'Yes' button)");

            aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().HelpfulYesButton.ScrollToElement();
            aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().HelpfulYesButton.Click();
            feedbackForm = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().FeedbackForm;
            feedbackForm.Textbox.ScrollToElement();
            feedbackForm.Textbox.SetText(FeedbackMessage);
            feedbackForm.SendFeedbackButton.ScrollToElement();
            aiAssistantPage = feedbackForm.SendFeedbackButton.Click<AiAssistedResearchPage>();

            this.TestCaseVerify.IsFalse(
               checkYesButtonDisabledAfterFeedback,
               aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().HelpfulYesButton.Enabled
               && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().HelpfulNoButton.Displayed,
               "'Yes' button is NOT disabled after send feedback");

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).ProgressDotsLabel.Displayed);

            aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).HelpfulNoButton.ScrollToElement();
            aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).HelpfulNoButton.Click();
            feedbackForm = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).FeedbackForm;

            this.TestCaseVerify.IsFalse(
               checkNoSendRequestButtonDefaultDisabled,
               feedbackForm.SendFeedbackButton.Enabled,
               "'Send Request' button is NOT disabled by default");

            feedbackForm.CancelButton.ScrollToElement();
            aiAssistantPage = feedbackForm.CancelButton.Click<AiAssistedResearchPage>();

            this.TestCaseVerify.IsFalse(
               checkFeedbackFormCancelledForNo,
               feedbackForm.IsDisplayed(),
               "Feedback dialog is displayed after clicking 'Cancel' button (for 'No' button)");

            aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).HelpfulYesButton.ScrollToElement();
            aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).HelpfulNoButton.Click();
            feedbackForm = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).FeedbackForm;
            feedbackForm.Textbox.ScrollToElement();
            feedbackForm.Textbox.SetText(FeedbackMessage);
            feedbackForm.SendFeedbackButton.ScrollToElement();
            aiAssistantPage = feedbackForm.SendFeedbackButton.Click<AiAssistedResearchPage>();

            this.TestCaseVerify.IsFalse(
               checkNoButtonDisabledAfterFeedback,
               aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).HelpfulNoButton.Enabled
               && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).HelpfulYesButton.Displayed,
               "'No' button is NOT disabled after send feedback");
        }

        /// <summary>
        /// Test case: 1770731
        /// Description: Verify feedback is saved in AI Assistant
        /// 1. Navigate to the AI-Assisted Research tab and enter a question about out-of-state depositions.
        /// 2. Submit the question and wait for the response to be generated.
        /// 3. Provide feedback and submit it.
        /// 4. Refresh the page and Verify: Feedback is saved after refresh.
        /// 5. Navigate through conversation history and Verify: User feedback is saved after conversations navigation.
        /// </summary> 
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantSaveFeedbackTest()
        {
            const string Question = "What laws govern taking an out-of-state deposition where the deposition will be in Arizona but the case is in Utah?";
            const string FeedbackText = "Test feedback";

            string checkFeedbackAfterRefresh = "Verify: Feedback is saved after refresh";
            string checkUserFeedbackAfterConversationsNavigation = "Verify: User feedback is saved after conversations navigation";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            var aiAssistedResearchTab = homePage.BrowseTabPanel.SetActiveTab<AiAssistedResearchTabPanel>(PrecisionBrowseTab.AiAssistedResearch);
            aiAssistedResearchTab = aiAssistedResearchTab.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchTabPanel>();

            aiAssistedResearchTab = aiAssistedResearchTab.QuestionTextbox.SetText<AiAssistedResearchTabPanel>(Question);
            var aiAssistantPage = aiAssistedResearchTab.SubmitButton.Click<AiAssistedResearchPage>();

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().HelpfulYesButton.Enabled);

            aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().HelpfulYesButton.ScrollToElement();
            aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().HelpfulYesButton.Click();
            var feedbackForm = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().FeedbackForm;
            feedbackForm.Textbox.SetText(FeedbackText);
            feedbackForm.SendFeedbackButton.ScrollToElement();
            aiAssistantPage = feedbackForm.SendFeedbackButton.Click<AiAssistedResearchPage>();

            aiAssistantPage = BrowserPool.CurrentBrowser.Refresh<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsFalse(
                checkFeedbackAfterRefresh,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().HelpfulYesButton.Enabled
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().HelpfulNoButton.Displayed,
                "User feedback is NOT saved after refresh");

            aiAssistantPage = aiAssistantPage.ConversationHistory.ExpandButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.ConversationHistory.Conversations.ElementAt(1).ConversationButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.ConversationHistory.Conversations.First().ConversationButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsFalse(
                checkUserFeedbackAfterConversationsNavigation,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().HelpfulYesButton.Enabled
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().HelpfulNoButton.Displayed,
                "User feedback is NOT saved after conversations navigation");
        }

        /// <summary>
        /// Test case: 1741401, 1769286
        /// Description: Verify Supporting Materials in AI Assistant
        /// 1. Navigate to the AI Assistant page and verify the page heading is correct.
        /// 2. Enter a question about ADA compliance for toilet paper dispensers and submit.
        /// 3. Verify: Metadata for supporting materials is displayed.
        /// 4. Verify: All supporting materials are unique.
        /// 5. Click on a document title link and Verify: Document page is opened in the same tab.
        /// 6. Return to the AI Assistant page and click on a passage link.
        /// 7. Verify: Pinpoint green arrow is displayed after clicking the passage link.
        /// 8. Return to the AI Assistant page and click on a KeyCite flag link.
        /// 9. Verify: Negative treatment page is opened in a new tab.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantSupportingMaterialsTest()
        {
            const string Question = "How close must a toilet paper dispenser be to a toilet under the ADA?";

            string checkPageHeading = "Verify: Page heading is correct";
            string checkMetadata = "Verify: Metadata is displayed";
            string checkNoDuplicates = "Verify: All supporting materials are unique";
            string checkDocumentPageInNewTab = "Verify: Document page is opened in the same tab";
            string checkPinpointArrow = "Verify: Pinpoint green arrow is displayed after clicking the passage link";
            string checkNegativeTreatmentPageInNewTab = "Verify: Negative treatment page is opened in a new tab";

            var aiAssistantPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            this.TestCaseVerify.IsTrue(
               checkPageHeading,
               aiAssistantPage.Toolbar.HeadingLabel.Text.Equals(AIAssistedResearchHeadingLabel),
               "Heading is NOT correct");

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SupportingMaterialsItems.Any() & !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
               checkMetadata,
               aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SupportingMaterialsItems.All(item => item.MetadataLabel.Displayed),
               "Metadata is NOT displayed");

            SafeMethodExecutor.ExecuteUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SupportingMaterialsItems.Any(),
                timeoutFromSec: 10);
            var supportingMaterials = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SupportingMaterialsItems.Select(item => $"{item.DocumentTitleLink.Text}{item.MetadataLabel.Text}").ToList();
            
            this.TestCaseVerify.AreEqual(
               checkNoDuplicates,
               supportingMaterials.Count(),
               supportingMaterials.Distinct().Count(),
               "NOT all supporting materials are unique");

            var documentPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SupportingMaterialsItems.First().DocumentTitleLink.Click<EdgeCommonDocumentPage>();

            this.TestCaseVerify.IsTrue(
               checkDocumentPageInNewTab,
               documentPage.IsDocumentLoaded(),
               "Document page is NOT opened in the same tab");

            aiAssistantPage = documentPage.FixedHeader.ClickReturnToListButton<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed);

            documentPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SupportingMaterialsItems.First().DocumentPassageLinks.First().Click<EdgeCommonDocumentPage>();

            this.TestCaseVerify.IsTrue(
               checkPinpointArrow,
               documentPage.IsBestPortionArrowDisplayed(),
               "Pinpoint green arrow is NOT displayed after clicking the passage link");

            aiAssistantPage = documentPage.FixedHeader.ClickReturnToListButton<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed);

            aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SupportingMaterialsItems.First(doc => doc.KeyCiteFlagLink.Displayed).KeyCiteFlagLink.ScrollToElement();
            var negativeTreatmentPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SupportingMaterialsItems.First(doc => doc.KeyCiteFlagLink.Displayed).KeyCiteFlagLink.Click<EdgeNegativeTreatmentPage>();
            BrowserPool.CurrentBrowser.CreateTab(NegativeTreatmentTab);
            BrowserPool.CurrentBrowser.ActivateTab(NegativeTreatmentTab);

            this.TestCaseVerify.IsTrue(
               checkNegativeTreatmentPageInNewTab,
               negativeTreatmentPage.RiTabs.GetSelectedTab().Equals(RiTab.NegativeTreatment)
               || negativeTreatmentPage.RiTabs.GetSelectedTab().Equals(RiTab.History),
               "Document page is NOT opened in a new tab");
        }

        /// <summary>
        /// Test case: 1756637, 1769227
        /// Description: Verify jump links in AI Assistant
        /// 1. Navigate to the AI Assistant page and select jurisdictions Louisiana and Mississippi.
        /// 2. Enter a question regarding contract indemnification and submit.
        /// 3. Verify: Jump links navigate correctly to their corresponding supporting materials.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantJumpLinksTest()
        {
            const string Question = "There was a contract with a construction company, Constructio, where Constructio was to build a new hotel in Jackson, Mississippi. " +
                "After construction was complete it became clear that Constructio negligently constructed the air conditioning system " +
                "which now requires hundreds of thousands of dollar to replace and fix. However the contract also contained an indemnification provision " +
                "that indemnifies Constructio for any defects, including their own negligence. Are there any theories to hold that the indemnification provision is unenforceable?";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            homePage = homePage.Header.OpenJurisdictionDialog().SelectJurisdictions(true, Jurisdiction.Louisiana, Jurisdiction.Mississippi).SaveButton.Click<PrecisionHomePage>();
            var aiAssistantPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            var jumpLinks = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().JumpLinks.ToList();
            var jumpLinkNumbers = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().JumpLinks.Select(link => link.Text.ConvertCountToInt()).ToList();
            var linksAndNumbersDictionary = jumpLinks.Zip(jumpLinkNumbers, (key, value) => new { key, value }).ToDictionary(item => item.key, item => item.value);
            linksAndNumbersDictionary.Keys.First().ScrollToElement();
            aiAssistantPage = linksAndNumbersDictionary.Keys.First().Click<AiAssistedResearchPage>();
            this.TestCaseVerify.IsTrue(
                $"Jump link works",
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SupportingMaterialsItems[linksAndNumbersDictionary[linksAndNumbersDictionary.Keys.First()] - 1].DocumentTitleLink.IsInView,

            //foreach (var link in linksAndNumbersDictionary.Keys)
            //{
            //    link.ScrollToElement();               
            //    aiAssistantPage = link.Click<AiAssistedResearchPage>();

            //    this.TestCaseVerify.IsTrue(
            //    $"Jump link works for {linksAndNumbersDictionary[link]}",
            //    aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SupportingMaterialsItems[linksAndNumbersDictionary[link] - 1].DocumentTitleLink.IsInView,
            //    $"Jump link doesn't work for {linksAndNumbersDictionary[link]}");
            //}
             $"Jump link doesn't work for {linksAndNumbersDictionary[linksAndNumbersDictionary.Keys.First()]}");
        }

        /// <summary>
        /// Test case: 1869337, 1904176
        /// Description: Verify jump links hover text in AI Assistant
        /// 1. Navigate to the AI Assistant page and select the default jurisdiction.
        /// 2. Enter a question about chapter 13 hardship discharge and submit.
        /// 3. Hover over the first jump link and Verify: Tooltip text matches the document metadata.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantJumpLinksHoverTextTest()
        {
            const string Question = "What is required for a chapter 13 hardship discharge?";

            string checkHoverText = "Verify: Tooltip text equals to document metadata";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            homePage = homePage.Header.OpenJurisdictionDialog().SelectDefaultJurisdiction().SaveButton.Click<PrecisionHomePage>();
            var aiAssistantPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().JumpLinks.First().Hover();

            string tooltipLabelText = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().TooltipLabel.Text.Replace("\r\n", string.Empty).Replace(" ", string.Empty).Replace("…", string.Empty).Replace("\"", string.Empty).Replace(",", string.Empty).Replace(".", string.Empty);
            int number = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().JumpLinks.First().Text.ConvertCountToInt();

            aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().JumpLinks.First().HoverOut();

            aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().JumpLinks.First().Click();

            string documentTitle = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SupportingMaterialsItems.ElementAt(number - 1).DocumentTitleLink.Text;
            string documentCitations = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SupportingMaterialsItems.ElementAt(number - 1).MetadataLabel.Text;
            string passageText = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SupportingMaterialsItems.ElementAt(number - 1).DocumentPassageLinks.First().Text;

            var expectedDocumentMetadata = $"{documentTitle}{documentCitations}{passageText}".Replace("\r\n", string.Empty).Replace(" ", string.Empty).Replace("…", string.Empty).Replace("\"", string.Empty).Replace(",", string.Empty).Replace(".", string.Empty);

            this.TestCaseVerify.IsTrue(
                checkHoverText,
                expectedDocumentMetadata.Contains(tooltipLabelText),
                "Tooltip text doesn't equal to document metadata");
        }

        /// <summary>
        /// Test case: 1903358, 1908214, 1910175
        /// Description: Verify inline titles and links in AI Assistant
        /// 1. Set profile preferences to include citations and navigate to the AI Assistant page.
        /// 2. Enter a question about differing site conditions in construction and submit.
        /// 3. Verify: Inline title links open the document page.
        /// 4. Verify: Inline KeyCite flag links open the negative treatment page.
        /// 5. Deliver the document and Verify: Include citations checkbox is selected in the delivery modal if it is on in Profile preferences.
        /// 6. Verify: Inline citations are present in the delivered document.
        /// 7. Submit a follow-up question and Verify: Show inline titles checkbox is displayed for the follow-up question.
        /// 8. Deselect the include citations checkbox in the delivery modal and deliver the document.
        /// 9. Verify: Inline citations are not present in the delivered document.
        /// 10. Turn off citations in Profile preferences and Verify: Show inline titles are not displayed when the feature is turned off.
        /// 11. Deliver the document again and Verify: Include citations checkbox is deselected in the delivery modal if it is off in Profile preferences.
        /// 12. Verify: Inline citations are not displayed in the delivered document.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantInlineTitlesTest()
        {
            const string Question = "How do courts treat differing behavioral differing site conditions in constructions?";

            string checkInlineTitleLink = "Verify: Inline title opens document page";
            string checkInlineKeyCiteFlagLink = "Verify: Inline KC flag opens negative treatment page";
            string checkIncludeCitationsIsSelected = "Verify: Include citations checkbox is selected in Delivery modal if it is on in Profile preferences";
            string checkInlineCitesInDelivery = "Verify: Inline cites are present in delivery";
            string checkCheckboxForFollowUpQuestion = "Verify: Show inline titles checkbox is displayed for follow-up question";
            string checkInlineCitesAreNotPresentInDelivery = "Verify: Inline cites are not present in delivered document";
            string checkCheckboxOffState = "Verify: Show inline titles aren't displayed when feature is turned off";
            string checkIncludeCitationsIsDeselected = "Verify: Include citations checkbox is deselected in Delivery modal if it is off in Profile preferences";
            string checkInlineCitesAreNotDisplayedInDelivery = "Verify: Inline cites are not displayed in delivered document";

            var homePage = this.GetHomePage<PrecisionHomePage>();

            //Turn on citations in Profile preferences
            var preferencesDialog = homePage.Header.ClickHeaderTab<EdgeProfileSettingsDialog>(EdgeHeaderTabs.PreferencesAndSignOut).ClickWestlawPreferences<EdgePreferencesDialog>();
            var featuresTab = preferencesDialog.TabPanel.SetActiveTab<EdgeFeaturesTabComponent>(EdgePreferencesDialogTabs.Features);

            if (!featuresTab.IsCheckboxSelected(EdgeFeaturesTab.AiAssistedResearchCitations))
            {
                featuresTab.SetFeature(EdgeFeaturesTab.AiAssistedResearchCitations, true);
            }

            homePage = preferencesDialog.SaveButton.Click<PrecisionHomePage>();

            //Ask a question
            homePage = homePage.Header.OpenJurisdictionDialog().SelectJurisdictions(true, Jurisdiction.AllFederal).SaveButton.Click<PrecisionHomePage>();
            var aiAssistantPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            var documentPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().InlineTitlesLinks.First().Click<PrecisionCommonDocumentPage>();

            this.TestCaseVerify.IsTrue(
                checkInlineTitleLink,
                documentPage.IsDocumentLoaded(),
                "Inline title doesn't open document page");

            aiAssistantPage = documentPage.FixedHeader.ClickReturnToListButton<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            var negativeTreatmentPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().InlineTitlesKeyCiteFlagsLinks.First().Click<EdgeNegativeTreatmentPage>();

            this.TestCaseVerify.IsTrue(
                checkInlineKeyCiteFlagLink,
                negativeTreatmentPage.RiTabs.GetSelectedTab().Equals(RiTab.NegativeTreatment)
                || negativeTreatmentPage.RiTabs.GetSelectedTab().Equals(RiTab.History),
                "Inline KC flag doesn't open negative treatment page");

            aiAssistantPage = negativeTreatmentPage.FixedHeader.ClickReturnToListButton<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            //Delivery, checkbox is selected 
            var answerLabel = this.CleanTextForCompare(aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Text);

            var downloadDialog = aiAssistantPage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);

            this.TestCaseVerify.IsTrue(
                checkIncludeCitationsIsSelected,
                downloadDialog.LayoutAndLimitsTab.IsIncludeSectionOptionSelected(LayoutAndLimitsInclude.IncludeCitationsInTheResponse),
                "Include citations checkbox is not selected");

            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<AiAssistedResearchPage>();
            var fileName = $"Westlaw Precision - Westlaw AI-Assisted Research - {DateTime.Now.ToString(DeliveryDateFormat)}.pdf";
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            var text = this.CleanTextForCompare(PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, fileName)));

            this.TestCaseVerify.IsTrue(
                checkInlineCitesInDelivery,
                text.Contains(answerLabel),
                "Inline cites are missing in delivery");

            //Ask follow-up question
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkCheckboxForFollowUpQuestion,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).InlineTitlesLinks.Any(),
                "Show inline titles checkbox is NOT displayed for follow-up question");

            //Delivery, deselect the checkbox          
            downloadDialog = aiAssistantPage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);
            downloadDialog.LayoutAndLimitsTab.SetIncludeSectionOption(LayoutAndLimitsInclude.IncludeCitationsInTheResponse, false);
            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<AiAssistedResearchPage>();
            fileName = $"Westlaw Precision - Westlaw AI-Assisted Research - {DateTime.Now.ToString(DeliveryDateFormat)} (1).pdf";
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            text = this.CleanTextForCompare(PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, fileName)));

            this.TestCaseVerify.IsFalse(
                checkInlineCitesAreNotPresentInDelivery,
                text.Contains(answerLabel),
                "Inline cites are still present in delivery");

            //Turn off citations in Profile preferences
            preferencesDialog = aiAssistantPage.Header.ClickHeaderTab<EdgeProfileSettingsDialog>(EdgeHeaderTabs.PreferencesAndSignOut).ClickWestlawPreferences<EdgePreferencesDialog>();
            featuresTab = preferencesDialog.TabPanel.SetActiveTab<EdgeFeaturesTabComponent>(EdgePreferencesDialogTabs.Features);
            featuresTab.SetFeature(EdgeFeaturesTab.AiAssistedResearchCitations, false);
            aiAssistantPage = preferencesDialog.SaveButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            var isInlineTitlesMainQuestion = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().InlineTitlesLinks.Any();

            aiAssistantPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().CollapseButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).ExpandButton.Click<AiAssistedResearchPage>();

            var isInlineTitlesFollowUpQuestion = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).InlineTitlesLinks.Any();

            this.TestCaseVerify.IsFalse(
                checkCheckboxOffState,
                isInlineTitlesMainQuestion
                && isInlineTitlesFollowUpQuestion,
                "Show inline titles are displayed when feature is turned off");

            //Delivery, checkbox is deselected
            answerLabel = this.CleanTextForCompare(aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Text);

            downloadDialog = aiAssistantPage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);

            this.TestCaseVerify.IsFalse(
                checkIncludeCitationsIsDeselected,
                downloadDialog.LayoutAndLimitsTab.IsIncludeSectionOptionSelected(LayoutAndLimitsInclude.IncludeCitationsInTheResponse),
                "Include citations checkbox is selected");

            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<AiAssistedResearchPage>();
            fileName = $"Westlaw Precision - Westlaw AI-Assisted Research - {DateTime.Now.ToString(DeliveryDateFormat)} (2).pdf";
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            text = this.CleanTextForCompare(PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, fileName)));

            this.TestCaseVerify.IsTrue(
                checkInlineCitesAreNotDisplayedInDelivery,
                text.Contains(answerLabel),
                "Inline cites are still present in delivery");
        }

        /// <summary>
        /// Test case: 1730378, 1836892, 1841103, 2113207
        /// Description: Verify the ability to ask follow-up questions in AI Assistant
        /// 1. Navigate to the AI Assistant page and verify the main question textbox placeholder is as expected.
        /// 2. Set jurisdiction to Northern Mariana Islands and submit a main question.
        /// 3. Verify: Follow-up question textbox placeholder is as expected.
        /// 4. Verify: Main question supporting materials are limited by the selected jurisdiction.
        /// 5. Submit a follow-up question and Verify: Previously asked question is collapsed automatically.
        /// 6. Verify: Follow-up question response is placed under the first response.
        /// 7. Verify: Only one question can be expanded, and page is scrolled to top once a question is expanded.
        /// 8. Verify: Follow-up questions supporting materials are limited by the selected jurisdiction.
        /// 9. Verify: All questions can be collapsed at the same time.
        /// 10. Verify: Question in progress can be collapsed.
        /// 11. Submit additional follow-up questions until the limit is reached.
        /// 12. Verify: Question limit message is correct.
        /// 13. Start a new research and Verify: Conversation is cleared.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantFollowUpQuestionsTest()
        {
            const string Question = "For paid family leave, are siblings of employees covered as family members?";
            const string FollowUpQuestion = "Would a brother of an employee be considered family member for paid family leave?";

            string checkQuestionTextboxMainPlaceholder = "Verify: Main question textbox placeholder is as expected";
            string checkQuestionTextboxFollowUpPlaceholder = "Verify: Follow-up question textbox placeholder is as expected";
            string checkMainQuestionSupportingMaterialsLimitation = "Verify: Main is question always limited by the selected jurisdiction";
            string checkPreviousQuestionCollapsed = "Verify: Previously asked question is collapsed automatically once the next is asked";
            string checkFollowUpQuestionResponsePlacement = "Verify: Follow-up question response is under the first response";
            string checkViewPortBehavior = "Verify: Only one question can be expanded, page is scrolled to top once question is expanded";
            string checkFollowUpQuestionSupportingMaterialsLimitation = "Verify: Follow-up questions are always limited by the selected jurisdiction";
            string checkAllQuestionsCollapsed = "Verify: All the questions can be collapsed at the same time";
            string checkQuestionInProgressCollapsed = "Verify: Question in progress can be collapsed";
            string checkQuestionLimit = "Verify: Question limit message is correct";
            string checkConversationIsCleared = "Verify: Conversation is cleared";

            var aiAssistantPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            this.TestCaseVerify.IsTrue(
                checkQuestionTextboxMainPlaceholder,
                aiAssistantPage.QueryBox.TitleLabel.Text.Equals("Ask a question"),
                "Main question textbox placeholder is NOT as expected");

            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectJurisdictions(true, Jurisdiction.NorthernMarianaIslands).SaveButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkQuestionTextboxFollowUpPlaceholder,
                aiAssistantPage.QueryBox.TitleLabel.Text.Equals("Ask a follow-up question about this response"),
                "Follow-up question textbox placeholder is NOT as expected");

            var supportingMaterialsMetadata = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SupportingMaterialsItems.Select(item => item.MetadataLabel.Text).ToList();

            this.TestCaseVerify.IsTrue(
                checkMainQuestionSupportingMaterialsLimitation,
                supportingMaterialsMetadata.TrueForAll(item => item.Contains("Northern Mariana Islands") || item.Contains("NMI")),
                "Main question are NOT always limited by the selected jurisdiction");

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(FollowUpQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkPreviousQuestionCollapsed,
                !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().IsExpanded
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).IsExpanded,
                "Previously asked question is NOT collapsed automatically once the next is asked");

            var questions = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Select(item => item.QuestionLabel.Text.Remove(0, item.QuestionLabel.Text.IndexOf("says:") + 7)).ToList().Select(input => Regex.Replace(input, @"(?<=\?).*$", ""));

            this.TestCaseVerify.IsTrue(
                checkFollowUpQuestionResponsePlacement,
                questions.SequenceEqual(new List<string> { Question, FollowUpQuestion })
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Count.Equals(2),
                "Follow-up question response is NOT under the first response");

            aiAssistantPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ExpandButton.Click<AiAssistedResearchPage>();

            aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).ExpandButton.ScrollToElement();

            aiAssistantPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).ExpandButton.Click<AiAssistedResearchPage>();

            this.TestCaseVerify.IsTrue(
                checkViewPortBehavior,
                !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().IsExpanded
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).IsExpanded
                && aiAssistantPage.IsPageScrolledToTop(),
                "NOT only one question can be expanded, page is NOT scrolled to top once question is expanded");

            supportingMaterialsMetadata = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).SupportingMaterials.SupportingMaterialsItems.Select(item => item.MetadataLabel.Text).ToList();

            this.TestCaseVerify.IsTrue(
                checkFollowUpQuestionSupportingMaterialsLimitation,
                supportingMaterialsMetadata.TrueForAll(item => item.Contains("Northern Mariana Islands") || item.Contains("NMI")),
                "Follow-up questions are NOT always limited by the selected jurisdiction");

            aiAssistantPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).CollapseButton.Click<AiAssistedResearchPage>();

            this.TestCaseVerify.IsTrue(
                checkAllQuestionsCollapsed,
                !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().IsExpanded
                && !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).IsExpanded,
                "NOT all the questions can be collapsed at the same time");

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(FollowUpQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();

            aiAssistantPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).ExpandButton.Click<AiAssistedResearchPage>();

            this.TestCaseVerify.IsTrue(
                checkQuestionInProgressCollapsed,
                !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().IsExpanded
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).IsExpanded
                && !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(2).IsExpanded,
                "Question in progress can NOT be collapsed");

            aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(2).ExpandButton.ScrollToElement();
            aiAssistantPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(2).ExpandButton.Click<AiAssistedResearchPage>();

            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(2).ProgressDotsLabel.Displayed);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(FollowUpQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(3).ProgressDotsLabel.Displayed);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(FollowUpQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(4).ProgressDotsLabel.Displayed);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(FollowUpQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(5).ProgressDotsLabel.Displayed);

            this.TestCaseVerify.AreEqual(
                checkQuestionLimit,
                "No more follow-up questions available for this AI-assisted research.",
                aiAssistantPage.QueryBox.QuestionLimitLabel.Text,
                "Question limit message is NOT correct");

            aiAssistantPage = aiAssistantPage.QueryBox.NewResearchButton.Click<AiAssistedResearchPage>();

            this.TestCaseVerify.IsFalse(
                checkConversationIsCleared,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Any(),
                "Conversation is NOT cleared");
        }

        /// <summary>
        /// Test case: 1768299, 1769220, 1867461, 1871144
        /// Description: Verify behavior when the follow-up question is invalid in AI Assistant
        /// 1. Navigate to the AI Assistant page and select the default jurisdiction.
        /// 2. Scenario 1: Submit an invalid question with slashes and Verify: It causes an out-of-scope message.
        /// 3. Scenario 2: Submit a valid main question and then an invalid follow-up question with slashes.
        /// 4. Verify: Follow-up invalid question causes an out-of-scope message and maintains the question sequence.
        /// 5. Submit a valid follow-up question after the error and Verify: The valid question is processed correctly.
        /// 6. Start a new research and submit a valid main question followed by an invalid follow-up question with "nothing".
        /// 7. Verify: The follow-up question generates an answer even if initially perceived as invalid.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantFollowUpInvalidQuestionTest()
        {
            const string Question = "My client is a hotel room attendant in IL. He is one of the 28 employees working at the hotel. He gets two 10 minute rest breaks and one 20 minute meal period when he works more than 7 hours. " +
                                        "He finds that there's not enough drinking water in the break room and he has to purchase one from the convenient store inside the hotel. Is his employer in violation of IL wage and hour law and " +
                                        "what will be their penalty?";
            const string SlashInvalidQuestion = "///////////////////////////////";
            const string NothingInvalidQuestion = "nothing";
            const string OutOfScopeMessage = "Your question appears to be outside of the scope of this feature, which uses generative AI to address legal research questions. Please rephrase your question and submit it again, or try another Westlaw research tool.";

            string checkSlashInvalidQuestion = $"{SlashInvalidQuestion} causes general error";
            string checkFollowUpSlashInvalidQuestion = $"Verify: Follow-up {SlashInvalidQuestion} invalid question causes out of scope message";
            string checkValidQuestionAfterError = "Verify: Ability to ask follow-up valid question after error";
            string checkFollowUpNothingInvalidQuestion = $"Verify: Follow-up {NothingInvalidQuestion} invalid question generates the answer";

            var aiAssistantPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchPage>();

            //Scenario 1:
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(SlashInvalidQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.AreEqual(
                checkSlashInvalidQuestion,
                OutOfScopeMessage,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionOutOfScopeMessageLabel.Text,
                $"{SlashInvalidQuestion} doesn't cause general error");

            //Scenario 2:
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(SlashInvalidQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).ProgressDotsLabel.Displayed);

            string actualOutOfScopeMessage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).QuestionOutOfScopeMessageLabel.Text;

            this.TestCaseVerify.IsTrue(
                checkFollowUpSlashInvalidQuestion,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Select(item => item.QuestionLabel.Text.Remove(0, item.QuestionLabel.Text.IndexOf("says:") + 7)).ToList().SequenceEqual(new List<string> { Question, SlashInvalidQuestion })
                && actualOutOfScopeMessage.Equals(OutOfScopeMessage),
                $"Follow-up {SlashInvalidQuestion} invalid question doesn't cause out of scope message");

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkValidQuestionAfterError,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).AnswerLabel.Displayed
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Count().Equals(2),
                "Unable to ask follow-up valid question after error");

            aiAssistantPage = aiAssistantPage.Toolbar.NewResearchButton.Click<AiAssistedResearchPage>();

            //Scenario 3:
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(NothingInvalidQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkFollowUpNothingInvalidQuestion,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).ErrorAnswerLabel.Displayed,
                $"Follow-up {NothingInvalidQuestion} invalid question doesn't generate the answer");
        }

        /// <summary>
        /// Test case: 1871148
        /// Description: Verify that a valid question is still displayed if a follow-up question results in an error in AI Assistant
        /// 1. Navigate to the AI Assistant page and select the default jurisdiction.
        /// 2. Submit a valid question about the elements of a motion to dismiss.
        /// 3. Submit an invalid follow-up question with slashes.
        /// 4. Sign out and sign back in to the application.
        /// 5. Navigate to the AI Assistant page and open the conversation from history.
        /// 6. Verify: Valid question is displayed even if the follow-up question caused an error.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantValidQuestionVisibilityTest()
        {
            const string Question = "describe the necessary elements of a motion to dismiss";
            const string FollowUpSlashInvalidQuestion = "///////////////////////////////";

            string checkValidQuestionFromHistoryVisibility = $"Verify: Valid question is displayed for errored conversation when opens from History";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            var aiAssistantPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchPage>();

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed, timeoutFromSec: 300);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(FollowUpSlashInvalidQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).ProgressDotsLabel.Displayed);

            BrowserPool.CurrentBrowser.CloseTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

            this.DefaultSignOnManager.SignOff();
            homePage = this.SignOnBack().ClickContinueButton<PrecisionHomePage>();

            aiAssistantPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.ConversationHistory.ExpandButton.Click<AiAssistedResearchPage>();

            aiAssistantPage = aiAssistantPage.ConversationHistory.Conversations.First().ConversationButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Any() && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel != null);

            this.TestCaseVerify.IsTrue(
                checkValidQuestionFromHistoryVisibility,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed,
                "Valid question is NOT displayed for errored conversation when opens from History");
        }

        /// <summary>
        /// Test case: 1840448, 1861087
        /// Description: Verify print and email delivery options in AI Assistant
        /// 1. Navigate to the AI Assistant page and Verify: Delivery dropdown is not displayed on the landing page.
        /// 2. Set jurisdiction to Bankruptcy Courts and submit a question about chapter 13 hardship discharge.
        /// 3. Verify: Delivery dropdown is disabled while the response is being generated.
        /// 4. If not in FedRamp environment, open the email dialog:
        ///    - Verify: Email options are correct.
        ///    - Verify: Email delivery works.
        /// 5. Open the print dialog:
        ///    - Verify: Print options are correct.
        ///    - Verify: Print delivery works.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantPrintAndEmailDeliveryTest()
        {
            const string Question = "What is required for a chapter 13 hardship discharge?";

            string checkDeliveryDropdownIsNotDisplayed = "Verify: Delivery dropdown is not displayed on landing page";
            string checkDeliveryDropdownIsDisabled = "Verify: Delivery dropdown is disabled";
            string checkEmailDialog = "Verify: Email options are correct";
            string checkEmailDeliveryWorks = "Verify: Email delivery works";
            string checkPrintDialog = "Verify: Print options are correct";
            string checkPrintDeliveryWorks = "Verify: Print delivery works";

            var aiAssistantPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            this.TestCaseVerify.IsFalse(
                checkDeliveryDropdownIsNotDisplayed,
                aiAssistantPage.Toolbar.DeliveryDropdown.IsDeliveryDropdownDisplayed(),
                "Delivery dropdown is displayed on landing page");

            aiAssistantPage = aiAssistantPage.QueryBox.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectJurisdictions(true, Jurisdiction.BankruptcyCourts).SaveButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();

            this.TestCaseVerify.IsTrue(
                checkDeliveryDropdownIsDisabled,
                !aiAssistantPage.Toolbar.DeliveryDropdown.IsDeliveryDropdownArrowDisplayed()
                && aiAssistantPage.Toolbar.DeliveryDropdown.IsTextPresented("Please Wait..."),
                "Delivery dropdown is enabled");

            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

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

        //// <summary>
        /// Test case: 1840448, 1860750, 1842143, 1837542, 1842643, 1861003, 1865338, 1872884, 1876020, 1876005, 1878977, 1886179
        /// Description: Verify PDF download delivery in AI Assistant
        /// 1. Navigate to the AI Assistant page and set preferences for document format to Word processor (RTF).
        /// 2. Submit an invalid main question and Verify: Delivery is disabled for invalid main question.
        /// 3. Submit a valid question about ADA requirements and a follow-up question, then another invalid question.
        /// 4. Open the download dialog and Verify: Download options are correct.
        /// 5. Download the research as a PDF and Verify:
        ///    - Cover page has correct information.
        ///    - Jurisdiction appears twice.
        ///    - Asked questions are present, excluding invalid questions.
        ///    - Section headers are present.
        ///    - Disclaimer count equals the number of questions.
        ///    - Supporting materials are correct.
        ///    - KC flags are present.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantPdfDeliveryTest()
        {
            const string Question = "How close must a toilet paper dispenser be to a toilet under the ADA?";
            const string FollowUpQuestion = "Where must toilet paper dispensers be located under the regulations implementing the ADA?";
            const string InvalidQuestion = "////////////////////////";

            const string DeliveryDisclaimer = "GeneratedbyAI.Notlegalortaxadvice.Aqualifiedprofessionalmustverifyaccuracyandlegalcompliance.";

            string checkDeliveryDisabled = "Verify: Delivery is disabled for invalid main question";
            string checkDownloadDialog = "Verify: Download options are correct";
            string checkCoverPage = "Verify: Cover page has correct info";
            string checkJurisdictionEntries = "Verify: Jurisdiction appears twice";
            string checkAskedQuestions = "Verify: Asked questions are present (excluding the invalid questions)";
            string checkSectionHeaders = "Verify: Section headers are present";
            string checkDisclaimerEntries = "Verify: Disclaimer count equals to the number of questions";
            string checkSupportingMaterials = "Verify: Supporting materials are correct";
            string checkKcFlags = "Verify: KC flags are present";

            FileUtil.DeleteFilesInFolderByMask(FolderToSave, "*.*");

            var homePage = this.GetHomePage<PrecisionHomePage>();

            var preferencesDialog = homePage.Header.OpenProfileSettingsDialog().ClickWestlawPreferences<EdgePreferencesDialog>();

            var deliveryTab = preferencesDialog.TabPanel.SetActiveTab<EdgeDeliveryTabComponent>(EdgePreferencesDialogTabs.Delivery);
            deliveryTab.SetDeliveryTabOptionDropdown(EdgeDeliveryTab.DeliveryDocumentFormat, "Word processor (RTF)");

            preferencesDialog.SaveButton.Click<PrecisionHomePage>();

            var aiAssistantPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.QueryBox.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchPage>();

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(InvalidQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsFalse(
                checkDeliveryDisabled,
                aiAssistantPage.Toolbar.DeliveryDropdown.IsDeliveryDropdownDisplayed(),
                "Delivery is NOT disabled for invalid main question");

            aiAssistantPage = aiAssistantPage.Toolbar.NewResearchButton.Click<AiAssistedResearchPage>();

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            var supportingMaterials = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SupportingMaterialsItems.
                Select(item => item.DocumentTitleLink.Text).ToList();

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(FollowUpQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).ProgressDotsLabel.Displayed);

            var supportingMaterialsSecondQuestion = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).SupportingMaterials.SupportingMaterialsItems.
                Select(item => item.DocumentTitleLink.Text).ToList();

            supportingMaterials.AddRange(supportingMaterialsSecondQuestion);
            supportingMaterials = supportingMaterials.Select(a => Regex.Replace(a, @"\s+", string.Empty).Replace("\r\n", string.Empty)).ToList();

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(InvalidQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(2).ProgressDotsLabel.Displayed);

            var downloadDialog = aiAssistantPage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog.LayoutAndLimitsTab.SetIncludeSectionOption(LayoutAndLimitsInclude.CoverPage);

            this.TestCaseVerify.IsTrue(
               checkDownloadDialog,
               downloadDialog.GetDialogTitle().Equals("Download Research")
               && downloadDialog.LayoutAndLimitsTab.CoverPageComment.Displayed
               && downloadDialog.TheBasicsTab.WhatToDeliver.IsOptionSelected(WhatToDeliver.AiAssistedResearch)
               && (downloadDialog.TheBasicsTab.FormatDropdown.SelectedOption.Equals(DeliveryFormat.Pdf)
               || downloadDialog.TheBasicsTab.FormatDropdown.SelectedOption.Equals(DeliveryFormat.Docx)),
               "Download options are NOT correct");

            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);

            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<AiAssistedResearchPage>();
            var fileName = $"Westlaw Precision - Westlaw AI-Assisted Research - {DateTime.Now.ToString(DeliveryDateFormat)}.pdf";
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            var text = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, fileName));

            var textWithoutWhitespaces = text.Replace(" ", string.Empty);

            this.TestCaseVerify.IsTrue(
               checkCoverPage,
               text.Contains("Westlaw AI-Assisted Research")
               && text.Contains($"Research delivered:  {DateTime.Now.ToString("MMMM d, yyyy")}")
               && text.Contains($"Response generated:  {DateTime.Now.ToString("MMMM d, yyyy")}")
               && text.Contains($"Delivered by:  {this.GetUserInfo().FirstName} {this.GetUserInfo().LastName}")
               && text.Contains($"Client ID:  {this.GetUserInfo().ClientId.ToUpper()}")
               && text.Contains("Jurisdiction:  All State & Federal")
               && text.Contains("Comment:"),
               "Cover page wrong info");

            this.TestCaseVerify.IsTrue(
                checkJurisdictionEntries,
                text.Select((count, item) => text.Substring(item)).Count(sub => sub.StartsWith("Jurisdiction:  All State & Federal")).Equals(2),
                "Jurisdiction doesn't appear twice");

            this.TestCaseVerify.IsTrue(
              checkAskedQuestions,
              text.Contains(Question)
              && text.Replace("\r\n", " ").Contains(FollowUpQuestion)
              && !text.Contains(InvalidQuestion),
              "Asked questions are present (or invalid question is displayed)");

            this.TestCaseVerify.IsTrue(
              checkSectionHeaders,
              text.Contains("Question")
              && text.Contains("Follow-up question")
              && text.Contains("Cases, statutes, and regulations"),
              "Section headers are NOT present");

            this.TestCaseVerify.IsTrue(
               checkDisclaimerEntries,
               textWithoutWhitespaces.Select((count, item) => textWithoutWhitespaces.Substring(item)).Count(sub => sub.StartsWith(DeliveryDisclaimer)).Equals(2),
               "Disclaimer count doesn't equal to the number of questions");

            var links = PdfTextExtractor.GetLinks(Path.Combine(FolderToSave, fileName));

            //Verify supporting materials
            this.TestCaseVerify.IsTrue(
              checkSupportingMaterials,
              supportingMaterials.Take(20).ToList().TrueForAll(docTitle => text.Replace(" ", string.Empty).Replace("\r\n", string.Empty).Replace(" ", string.Empty).Replace(" ", string.Empty).Contains(docTitle)),
              "Supporting materials are not correct");

            //KC flag verification
            this.TestCaseVerify.IsTrue(
              checkKcFlags,
              links.Any(link => link.Contains("RelatedInformation/Flag")),
              "KC flags are NOT present");
        }

        /// <summary>
        /// Test case: 1886048, 1886179
        /// Description: Verify DOCX download delivery in AI Assistant
        /// 1. Navigate to the AI Assistant page and submit a valid question about ADA requirements and a follow-up question.
        /// 2. Submit an additional invalid question.
        /// 3. Open the download dialog and select DOCX format for delivery.
        /// 4. Download the research and Verify:
        ///    - Cover page has correct information.
        ///    - Jurisdiction appears twice.
        ///    - Asked questions are present, excluding invalid questions.
        ///    - Section headers are present.
        ///    - Disclaimer count equals the number of questions.
        ///    - Supporting materials are correct.
        ///    - KC flags are present.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantDocxDeliveryTest()
        {
            const string Question = "How close must a toilet paper dispenser be to a toilet under the ADA?";
            const string FollowUpQuestion = "Where must toilet paper dispensers be located under the regulations implementing the ADA?";
            const string InvalidQuestion = "////////////////////////";

            const string DeliveryDisclaimer = "TheaboveresponseisAI-generatedandmaycontainerrors.Itshouldbeverifiedforaccuracy.";

            string checkCoverPage = "Verify: Cover page has correct info";
            string checkJurisdictionEntries = "Verify: Jurisdiction appears twice";
            string checkAskedQuestions = "Verify: Asked questions are present (excluding the invalid questions)";
            string checkSectionHeaders = "Verify: Section headers are present";
            string checkDisclaimerEntries = "Verify: Disclaimer count equals to the number of questions";
            string checkSupportingMaterials = "Verify: Supporting materials are correct";
            string checkKcFlags = "Verify: KC flags are present";

            var homePage = this.GetHomePage<PrecisionHomePage>();

            FileUtil.DeleteFilesInFolderByMask(FolderToSave, "*.*");

            var aiAssistantPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.QueryBox.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchPage>();

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            var supportingMaterials = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SupportingMaterialsItems.
                Select(item => item.DocumentTitleLink.Text).ToList();

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(FollowUpQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).ProgressDotsLabel.Displayed);

            var supportingMaterialsSecondQuestion = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).SupportingMaterials.SupportingMaterialsItems.
                Select(item => item.DocumentTitleLink.Text).ToList();

            supportingMaterials.AddRange(supportingMaterialsSecondQuestion);
            supportingMaterials = supportingMaterials.Select(a => Regex.Replace(a, @"\s+", string.Empty).Replace("\r\n", string.Empty)).ToList();

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(InvalidQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(2).ProgressDotsLabel.Displayed);

            var downloadDialog = aiAssistantPage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);

            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Docx);
            downloadDialog.LayoutAndLimitsTab.SetIncludeSectionOption(LayoutAndLimitsInclude.CoverPage);

            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<AiAssistedResearchPage>();
            var fileName = $"Westlaw Precision - Westlaw AI-Assisted Research - {DateTime.Now.ToString(DeliveryDateFormat)}.docx";
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            var text = DocxTextExtractor.ExtractTextFromWord(Path.Combine(FolderToSave, fileName));

            var textWithoutWhitespaces = text.Replace(" ", string.Empty);

            this.TestCaseVerify.IsTrue(
               checkCoverPage,
               textWithoutWhitespaces.Contains("Westlaw AI-Assisted Research".Replace(" ", string.Empty))
               && textWithoutWhitespaces.Contains($"Research delivered: {DateTime.Now.ToString("MMMM d, yyyy")}".Replace(" ", string.Empty))
               && textWithoutWhitespaces.Contains($"Response generated: {DateTime.Now.ToString("MMMM d, yyyy")}".Replace(" ", string.Empty))
               && textWithoutWhitespaces.Contains($"Delivered by: {this.GetUserInfo().FirstName} {this.GetUserInfo().LastName}".Replace(" ", string.Empty))
               && textWithoutWhitespaces.Contains($"Client ID: {this.GetUserInfo().ClientId.ToUpper()}".Replace(" ", string.Empty))
               && textWithoutWhitespaces.Contains("Jurisdiction: All State & Federal".Replace(" ", string.Empty))
               && textWithoutWhitespaces.Contains("Comment:"),
               "Cover page wrong info");

            this.TestCaseVerify.IsTrue(
                checkJurisdictionEntries,
                text.Select((count, item) => text.Substring(item)).Count(sub => sub.StartsWith("Jurisdiction: All State & Federal")).Equals(2),
                "Jurisdiction doesn't appear twice");

            this.TestCaseVerify.IsTrue(
              checkAskedQuestions,
              text.Contains(Question)
              && text.Replace("\r\n", " ").Contains(FollowUpQuestion)
              && !text.Contains(InvalidQuestion),
              "Asked questions are present (or invalid question is displayed)");

            this.TestCaseVerify.IsTrue(
              checkSectionHeaders,
              text.Contains("Question")
              && text.Contains("Follow-up question")
              && text.Contains("Cases, statutes, and regulations"),
              "Section headers are NOT present");

            this.TestCaseVerify.IsTrue(
               checkDisclaimerEntries,
               textWithoutWhitespaces.Select((count, item) => textWithoutWhitespaces.Substring(item)).Count(sub => sub.StartsWith(DeliveryDisclaimer)).Equals(2),
               "Disclaimer count doesn't equal to the number of questions");

            var links = DocxTextExtractor.GetLinks(Path.Combine(FolderToSave, fileName));

            //Verify supporting materials
            this.TestCaseVerify.IsTrue(
              checkSupportingMaterials,
              supportingMaterials.Take(20).ToList().TrueForAll(docTitle => text.Replace(" ", string.Empty).Replace("\r\n", string.Empty).Contains(docTitle)),
              "Supporting materials are not correct");

            //KC flag verification
            this.TestCaseVerify.IsTrue(
              checkKcFlags,
              links.Any(link => link.Contains("RelatedInformation/Flag")),
              "KC flags are NOT present");
        }

        /// <summary>
        /// Test case: 1862340, 1863868, 1885963
        /// Description: Verify that the jurisdiction is retained for each conversation in AI Assistant
        /// 1. Start a new conversation with a default jurisdiction and submit a question.
        /// 2. Begin another conversation with a changed jurisdiction (Alabama) and submit a different question.
        /// 3. Open the previous conversation from history and Verify: Jurisdiction is not changed for the previous conversation.
        /// 4. Download the conversation and Verify: Jurisdiction in the delivery matches the original conversation jurisdiction.
        /// 5. Start a new conversation with an intent resolver question and then another with a changed jurisdiction (All Federal).
        /// 6. Open the previous intent resolver conversation from history and Verify: Jurisdiction is not changed for the previous intent resolver conversation.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantJurisdictionChangingTest()
        {
            const string FirstQuestion = "Can a national bank be required to post a supersedeas bond?";
            const string SecondQuestion = "Can an city in Alabama require my client's company to pay a minimum wage above what is required by state and federal law?";
            const string IntentResolverQuestion = "economic loss";

            string checkJurisdictionForPreviousConversation = "Verify: Jurisdiction is not changed for previous conversation";
            string checkJurisdictionForPreviousConversationInDelivery = "Verify: Jurisdiction is not changed for previous conversation in delivery";
            string checkJurisdictionForPreviousIntentResolverConversation = "Verify: Jurisdiction is not changed for previous intent resolver conversation";

            var aiAssistantPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(FirstQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage = aiAssistantPage.Toolbar.NewResearchButton.Click<AiAssistedResearchPage>();

            aiAssistantPage = aiAssistantPage.QueryBox.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectJurisdictions(true, Jurisdiction.Alabama).SaveButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(SecondQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage = aiAssistantPage.ConversationHistory.ExpandButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.ConversationHistory.Conversations.ElementAt(1).ConversationButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkJurisdictionForPreviousConversation,
                aiAssistantPage.Toolbar.JurisdictionLabel.Text.Equals("All State & Federal"),
                "Jurisdiction is changed for previous conversation");

            var downloadDialog = aiAssistantPage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);
            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<AiAssistedResearchPage>();
            var fileName = $"Westlaw Precision - Westlaw AI-Assisted Research - {DateTime.Now.ToString(DeliveryDateFormat)}.pdf";
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            var text = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, fileName));

            this.TestCaseVerify.IsTrue(
                checkJurisdictionForPreviousConversationInDelivery,
                text.Replace(" ", string.Empty).Contains("Jurisdiction:AllState&Federal"),
                "Jurisdiction is changed for previous conversation in delivery");

            aiAssistantPage = aiAssistantPage.Toolbar.NewResearchButton.Click<AiAssistedResearchPage>();

            // Scenario 2:
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(IntentResolverQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage = aiAssistantPage.Toolbar.NewResearchButton.Click<AiAssistedResearchPage>();

            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectJurisdictions(true, Jurisdiction.AllFederal).SaveButton.Click<AiAssistedResearchPage>();

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(FirstQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage = aiAssistantPage.ConversationHistory.Conversations.ElementAt(1).ConversationButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkJurisdictionForPreviousIntentResolverConversation,
                aiAssistantPage.Toolbar.JurisdictionLabel.Text.Equals("Alabama"),
                "Jurisdiction is changed for previous intent resolver conversation");
        }

        /// <summary>
        /// Test case: 1863984
        /// Description: Verify that Dropbox or Kindle delivery options are not available in AI Assistant
        /// 1. Perform a search and select a result item.
        /// 2. Attempt to use Kindle delivery option for the selected item.
        /// 3. Navigate to the AI Assistant page and submit a question.
        /// 4. Verify: Selected delivery options available are 'Download', 'Email', or 'Print', with no 'Dropbox' or 'Kindle' options.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantNoDropboxKindleDeliveryOptionsTest()
        {
            const string Query = "cat";
            const string Question = "When does the 'safe harbor' provision in Fla. Stat. s 679.5061(3) apply when searching for a UCC financing statement?";

            string checkSelectedDeliveryOption = "Verify: Selected delivery option is 'Download', 'Email' or 'Print'. No 'Dropbox' or 'Kindle'";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            var searchResultsPage = homePage.Header.EnterSearchQueryAndClickSearch<PrecisionCommonSearchResultPage>(Query);
            searchResultsPage.NarrowTabPanel.SetActiveTab<ContentTypesTabComponent>(NarrowTab.ContentTypes).ContentType.SetDefaultContentType(ContentType.Cases);
            searchResultsPage = searchResultsPage.Header.EnterSearchQueryAndClickSearch<PrecisionCommonSearchResultPage>(Query);

            searchResultsPage.ResultList.GetAllSearchResultItems<PrecisionResultListItem>().ToList().First().SetCheckBox(true);
            var kindleDialog = searchResultsPage.Toolbar.DeliveryDropdown.SelectOption<KindleDialog>(DeliveryMethod.Kindle);
            kindleDialog.TheBasicsTab.SetKindleEmailAddress("email@noemail.com");
            var readyToDeliveryDialog = kindleDialog.ClickSendButton<ReadyForDeliveryDialog>();
            readyToDeliveryDialog.WaitForEmailDialogToDisappear();

            var aiAssistantPage = searchResultsPage.Header.ExpandToolsFlyoutButton.Click<PrecisionToolsDialog>().ToolLinks.First(link => link.Text.Contains(AIAssistedResearchHeadingLabel)).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
               checkSelectedDeliveryOption,
               aiAssistantPage.Toolbar.DeliveryDropdown.SelectedOption.Equals(DeliveryMethod.Print)
               || aiAssistantPage.Toolbar.DeliveryDropdown.SelectedOption.Equals(DeliveryMethod.Download)
               || aiAssistantPage.Toolbar.DeliveryDropdown.SelectedOption.Equals(DeliveryMethod.Email),
               "Selected delivery option is 'Kindle'");
        }

        /// <summary>
        /// Test case: 1860687, 1843021, 1884789
        /// Description: Verify conversation history behavior in AI Assistant
        /// 1. Start a new conversation with an intent resolver question and a follow-up question.
        /// 2. Close and reopen the AI Assistant page, then Verify: Conversation history is collapsed by default.
        /// 3. Expand the conversation history and open the conversation from history.
        /// 4. Verify: Main and follow-up questions are in the correct sequence when opened from history.
        /// 5. Collapse the conversation history and Verify: It is collapsed after clicking the 'Collapse' button.
        /// 6. Refresh the page and Verify: Conversation history is collapsed to default state after page reload.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantConversationHistoryTest()
        {
            const string IntentResolverQuestion = "weather";
            const string FollowUpQuestion = "What distance does a toilet paper dispenser have to be from the toilet to be compliant with ADA accessibility guidelines?";

            string checkConversationHistoryCollapsedByDefault = "Verify: Conversation history is collapsed by default";
            string checkConversationHistoryCollapsedAfterCollapsing = "Verify: Conversation history is collapsed after clicking 'Collapse' button";
            string checkConversationHistoryCollapsedAfterReload = "Verify: Conversation history is collapsed (dropped to default state) after page reload reload";

            var homePage = this.GetHomePage<PrecisionHomePage>();

            var aiAssistantPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(IntentResolverQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Any() & !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(FollowUpQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();

            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Any() &&
            !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed
);
            BrowserPool.CurrentBrowser.CloseTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

            aiAssistantPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);
            Thread.Sleep(1000);
            this.TestCaseVerify.IsTrue(
                checkConversationHistoryCollapsedByDefault,
                aiAssistantPage.ConversationHistory.IsCollapsed(),
                "Conversation history is NOT collapsed by default");

            aiAssistantPage = aiAssistantPage.ConversationHistory.ExpandButton.Click<AiAssistedResearchPage>();

            aiAssistantPage = aiAssistantPage.ConversationHistory.Conversations.First().ConversationButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage = aiAssistantPage.ConversationHistory.CollapseButton.Click<AiAssistedResearchPage>();

            this.TestCaseVerify.IsTrue(
                checkConversationHistoryCollapsedAfterCollapsing,
                aiAssistantPage.ConversationHistory.IsCollapsed(),
                "Conversation history is NOT collapsed after clicking 'Collapse' button");

            aiAssistantPage = aiAssistantPage.ConversationHistory.ExpandButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = BrowserPool.CurrentBrowser.Refresh<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkConversationHistoryCollapsedAfterReload,
                aiAssistantPage.ConversationHistory.IsCollapsed(),
                "Conversation history is NOT collapsed (dropped to default state) after page reload reload");
        }

        /// <summary>
        /// Test case: -
        /// Description: Verify active and locked conversations in AI Assistant
        /// 1. Start a new conversation and Verify: Time label is displayed for each conversation.
        /// 2. Verify: 'Active' label is displayed for the conversation asked in the current session.
        /// 3. Re-login to the application and Verify: 'Active' label is still displayed after re-login.
        /// 4. Change the client ID and Verify: The conversation is not active, and a read-only message is displayed.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantActiveAndLockedConversationsTest()
        {
            const string Question = "How close must a toilet paper dispenser be to a toilet under the ADA?";

            string checkTimeLabel = "Verify: Time label is displayed";
            string checkActiveLabel = "Verify: 'Active' label is displayed for conversation asked in current session";
            string checkActiveLabelAfterRelogin = "'Active' label is displayed after relogin";
            string checkPageAfterClientIdChange = "Verify: Conversation is not active after client ID change";

            var homePage = this.GetHomePage<PrecisionHomePage>();

            var aiAssistantPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage = aiAssistantPage.ConversationHistory.ExpandButton.Click<AiAssistedResearchPage>();

            this.TestCaseVerify.IsTrue(
               checkTimeLabel,
               aiAssistantPage.ConversationHistory.Conversations.ToList().TrueForAll(item => item.TimeLabel.Displayed),
               "Time label is NOT displayed");

            this.TestCaseVerify.IsTrue(
                checkActiveLabel,
                aiAssistantPage.ConversationHistory.Conversations.First().ActiveLabel.Text.Contains("Active"),
                "'Active' label is NOT displayed for conversation asked in current session");

            //Re-login scenario           
            BrowserPool.CurrentBrowser.CloseTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

            this.DefaultSignOnManager.SignOff();
            this.SignOnBack().ClickContinueButton<PrecisionHomePage>();

            aiAssistantPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.ConversationHistory.ExpandButton.Click<AiAssistedResearchPage>();

            this.TestCaseVerify.IsTrue(
                checkActiveLabelAfterRelogin,
                aiAssistantPage.ConversationHistory.Conversations.First().ActiveLabel.Text.Contains("Active"),
                "'Active' label is NOT displayed after relogin");

            aiAssistantPage = aiAssistantPage.ConversationHistory.Conversations.First().ConversationButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).ProgressDotsLabel.Displayed);

            //Client ID change scenario
            var changeIdDialog = aiAssistantPage.Header.OpenChangeClientIdDialog();
            aiAssistantPage = changeIdDialog.EnterClientIdAndHitContinue<AiAssistedResearchPage>("NewClientId");
            Thread.Sleep(3000);

            aiAssistantPage = aiAssistantPage.ConversationHistory.ExpandButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.ConversationHistory.Conversations.First().ConversationButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkPageAfterClientIdChange,
                !aiAssistantPage.QueryBox.IsDisplayed()
                && !aiAssistantPage.ConversationHistory.Conversations.First().ActiveLabel.Displayed
                && aiAssistantPage.Chat.ReadOnlyMessageLabel.Text.Equals("Follow-up questions for AI-Assisted Research are only available during the first 24 hours."),
                "Conversation is active after client ID change");
        }

        /// <summary>
        /// Test case: 1863798, 1862336, 1865422, 1868882, 1873428
        /// Description: Verify history events functionality in AI Assistant
        /// 1. Start a new AI-assisted research session with a specific question.
        /// 2. Sign off and Verify: AI Research event is present on the sign-off page history.
        /// 3. Sign back in and Verify: Recent search event contains the question, event type, and jurisdiction.
        /// 4. Click on the recent search event and Verify: It opens the recent conversation correctly.
        /// 5. Expand the conversation history and navigate to the full history page.
        /// 6. Verify: History event facet is applied correctly.
        /// 7. Verify: History page event contains the question, event type, and jurisdiction.
        /// 8. Attempt to deliver the AI event from the history page and Verify: Delivery works correctly.
        /// 9. Click on the history page event and Verify: It opens the recent conversation correctly.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantHistoryEventTest()
        {
            const string Question = "For paid-family leave, are siblings of employees covered as family—members?";

          //  string checkSignOffPage = "Verify: AI Research event is present on History page";
            string checkRecentSearchEvent = "Verify: Recent search event contains question, event type and jurisdiction";
            string checkRecentSearchEventOpensConversation = "Verify: Recent search event click opens the recent conversation";
            string checkAiResearchFacet = "Verify: History event facet is applied";
            string checkHistoryPageEvent = "Verify: History page event contains question, event type and jurisdiction";
            string checkDeliveryOnHistoryPage = "verify: AI events can de delivered from History page";
            string checkHistoryPageOpensConversation = "Verify: History page event click opens the recent conversation";

            var homePage = this.GetHomePage<PrecisionHomePage>();

            var aiAssistedResearchTab = homePage.BrowseTabPanel.SetActiveTab<AiAssistedResearchTabPanel>(PrecisionBrowseTab.AiAssistedResearch);
            aiAssistedResearchTab = aiAssistedResearchTab.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchTabPanel>();
            aiAssistedResearchTab = aiAssistedResearchTab.QuestionTextbox.SetText<AiAssistedResearchTabPanel>(Question);
            var aiAssistantPage = aiAssistedResearchTab.SubmitButton.Click<AiAssistedResearchPage>();

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            var signOffPage = (CommonSignOffPage)this.DefaultSignOnManager.SignOff();

            //Bug 2191504 - Recent history event is not reflecting in session summary when user signs off
            //Commenting this section until bug fix

            //  var evenOnSignOffPage = signOffPage.SignOffSessionDetailsComponent.GetSessionItems().First().GetDescriptionText().Replace(" ", string.Empty);

            //this.TestCaseVerify.IsTrue(
            //    checkSignOffPage,
            //    evenOnSignOffPage.Equals($"{Question.Replace(" ", string.Empty)}\r\nAI-AssistedResearchAllState&Federal"),
            //    "AI Research event is NOT present on History page");

            var clientIdPage = this.SignOnBack();

            SafeMethodExecutor.WaitUntil(() => clientIdPage.RecentResearchPane.RecentResearchList.Any());
            var eventDescription = clientIdPage.RecentResearchPane.RecentResearchList.First().Text.Replace(" ", string.Empty);

            this.TestCaseVerify.IsTrue(
                checkRecentSearchEvent,
                eventDescription.Equals($"{Question.Replace(" ", string.Empty)}\r\nAI-AssistedResearchAllState&Federal"),
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
                $"{Question}AI-AssistedResearchAllState&Federal".Replace(" ", string.Empty),
                $"{historyPage.HistoryTable.GetGridItems().First().Title}{historyPage.HistoryTable.GetGridItems().First().Summary}".Replace(" ", string.Empty),
                "History page event DOESN'T contains question, event type and jurisdiction");

            var downloadDialog = historyPage.EdgeToolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);

            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);

            this.TestCaseVerify.IsTrue(
                checkDeliveryOnHistoryPage,
                downloadDialog.DownloadAndWaitForConfirmation<EdgeCommonHistoryPage>(),
                "AI events cann't de delivered fronm History page");

            aiAssistantPage = historyPage.HistoryTable.GetGridItems().First().ClickLinkByText<AiAssistedResearchPage>(Question);

            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkHistoryPageOpensConversation,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.Remove(0, aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.IndexOf("says:") + 7).Equals(Question)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Count.Equals(1)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed,
                "History page event click DOESN'T open the recent conversation");
        }

        /// <summary>
        /// Test case: 1896563, 1897260
        /// Description: Verify mixture of history events (AI-Assisted Research, Treatise, Claims Explorer) in AI Assistant
        /// 1. Conduct AI-assisted research with a specific question and Verify: The research completes successfully.
        /// 2. Perform a search from the Treatise page and Verify: The search completes successfully.
        /// 3. Conduct a query from the Claims Explorer page and Verify: Only Claims events are displayed on the Claims Explorer page.
        /// 4. Navigate to the full history page and Verify: Claims Explorer event is displayed.
        /// 5. Verify: Treatise event is displayed on the History page.
        /// 6. Verify: AI-Assisted Research event is displayed on the History page.
        /// 7. Verify: All AI events are displayed on the History page.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantMixtureOfHistoryEventsTest()
        {
            string SecondarySources = "Secondary Sources";
            const string AiAssistedResearchQuestion = "For paid-family leave, are siblings of employees covered as family—members?";
            const string TreatiseQuestion = "Can parties orally stipulate to change the date of a deposition?";
            const string ClaimsExplorerQuestion = "Client's neighbor stole their golf cart, rammed it into a couple saplings on my client's property.";
            const string ConversationDateFormat = "MMM d, yyyy hh:mm tt";
            // const string HistoryEventDateFormat = "M/d/yyyh:mm tt";

            string checkOnlyClaimsConversations = "Verify: Only Claims events are displayed on Claims Explorer page";
            string checkClaimsExplorerEventOnHistoryPage = "Verify: Claims event is displayed on History page (via 'Go to full history' link)";
            string checkTreatiseEvensOnHistoryPage = "Verify: Treatise event is displayed on History page (via 'Go to full history' link)";
            string checkAiAssistedResearchEventOnHistoryPage = "Verify: Ai-Assisted Research event is displayed on History page (via 'Go to full history' link)";
            string checkAllAiEventsOnHistoryPage = "Verify: All AI events are displayed on History page (via 'Go to full history' link)";

            //Ask question from AI-Assisted Research page
            var homePage = this.GetHomePage<PrecisionHomePage>();
            var aiAssistedResearchTab = homePage.BrowseTabPanel.SetActiveTab<AiAssistedResearchTabPanel>(PrecisionBrowseTab.AiAssistedResearch);
            aiAssistedResearchTab = aiAssistedResearchTab.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchTabPanel>();
            aiAssistedResearchTab = aiAssistedResearchTab.QuestionTextbox.SetText<AiAssistedResearchTabPanel>(AiAssistedResearchQuestion);
            var aiAssistantPage = aiAssistedResearchTab.SubmitButton.Click<AiAssistedResearchPage>();
            
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            SafeMethodExecutor.ExecuteUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            BrowserPool.CurrentBrowser.CloseTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

            // Ask question from Treatise page

            var contentTypeSearchResultsPage = this.GetHomePage<PrecisionHomePage>().BrowseTabPanel.SetActiveTab<ContentTypesTabPanel>(PrecisionBrowseTab.ContentTypes)
            .ClickLinkByText<CommonBrowsePage>(SecondarySources);
             aiAssistantPage = contentTypeSearchResultsPage.ClickLinkByText<AiAssistedResearchPage>("Search & Summarize Rutter");

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(TreatiseQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);
            

            // Ask question from Claims Explorer page
            var claimsExplorerPage = this.ReturnToHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(ClaimsExplorerHeadingLabel).Click<AiClaimsExplorerPage>();
            BrowserPool.CurrentBrowser.CreateTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);
            var aiJurisdictionDialog = claimsExplorerPage.Toolbar.JurisdictionButton.Click<AiJurisdictionDialog>();
            claimsExplorerPage = aiJurisdictionDialog.SelectJurisdictions(true, Jurisdiction.California, Jurisdiction.Federal).SaveButton.Click<AiClaimsExplorerPage>();
            claimsExplorerPage = claimsExplorerPage.QueryBox.QuestionTextbox.SetText<AiClaimsExplorerPage>(ClaimsExplorerQuestion);
            claimsExplorerPage = claimsExplorerPage.QueryBox.SendQuestionButton.Click<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem != null && !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            //Verify only Claims events are on Claims Explorer page
            claimsExplorerPage = claimsExplorerPage.ConversationHistory.ExpandButton.Click<AiClaimsExplorerPage>();
            var conversations = claimsExplorerPage.ConversationHistory.Conversations.ToList().Select(conversation => conversation.ConversationButton.Text).ToList();

            this.TestCaseVerify.IsTrue(
                checkOnlyClaimsConversations,
                !conversations.Any(text => text.Contains(TreatiseQuestion))
                && !conversations.Any(text => text.Contains(AiAssistedResearchQuestion))
                && conversations.Any(text => text.Contains(ClaimsExplorerQuestion)),
                "Not only Claims events are displayed on Claims Explorer page");

            var conversationDate = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.DateLabel.Text;
            var formattedConversationDate = DateTime.ParseExact(conversationDate, ConversationDateFormat, CultureInfo.InvariantCulture);

            // Verify all AI events on History page
            var historyPage = aiAssistantPage.ConversationHistory.GoToFullHistoryLink.Click<EdgeCommonHistoryPage>();
            BrowserPool.CurrentBrowser.CreateTab(HistoryPageTab);
            BrowserPool.CurrentBrowser.ActivateTab(HistoryPageTab);
            SafeMethodExecutor.WaitUntil(() => historyPage.HistoryTable.IsDisplayed());

            this.TestCaseVerify.AreEqual(
                checkClaimsExplorerEventOnHistoryPage,
                $"{ClaimsExplorerQuestion}ClaimsExplorerCaliforniaAllFederal".Replace(" ", string.Empty),
                $"{historyPage.HistoryTable.GetGridItems().First().Title}{historyPage.HistoryTable.GetGridItems().First().Summary}".Replace(" ", string.Empty),
                "Claims Explorer event is NOT displayed on History page (via 'Go to full history' link)");

            this.TestCaseVerify.AreEqual(
               checkTreatiseEvensOnHistoryPage,
               $"{TreatiseQuestion}Search&SummarizeRutterGroup".Replace(" ", string.Empty),
               $"{historyPage.HistoryTable.GetGridItems().ElementAt(1).Title}{historyPage.HistoryTable.GetGridItems().ElementAt(1).Summary}".Replace(" ", string.Empty),
               "Treatise event is NOT displayed on History page (via 'Go to full history' link)");

            this.TestCaseVerify.AreEqual(
               checkAiAssistedResearchEventOnHistoryPage,
               $"{AiAssistedResearchQuestion}AI-AssistedResearchAllState&Federal".Replace(" ", string.Empty),
               $"{historyPage.HistoryTable.GetGridItems().ElementAt(2).Title}{historyPage.HistoryTable.GetGridItems().ElementAt(2).Summary}".Replace(" ", string.Empty),
               "Ai-Assisted Research event is NOT displayed on History page (via 'Go to full history' link)");

            BrowserPool.CurrentBrowser.CloseTab(HistoryPageTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            // Verify all AI events on History page
            historyPage = aiAssistantPage.ConversationHistory.GoToFullHistoryLink.Click<EdgeCommonHistoryPage>();
            BrowserPool.CurrentBrowser.CreateTab(HistoryPageTab);
            BrowserPool.CurrentBrowser.ActivateTab(HistoryPageTab);

            SafeMethodExecutor.WaitUntil(() => historyPage.HistoryTable.IsDisplayed());

            this.TestCaseVerify.IsTrue(
                checkAllAiEventsOnHistoryPage,
                historyPage.HistoryTable.GetGridItems().First().Title.Equals(ClaimsExplorerQuestion)
                && historyPage.HistoryTable.GetGridItems().ElementAt(1).Title.Equals(TreatiseQuestion)
                && historyPage.HistoryTable.GetGridItems().ElementAt(2).Title.Equals(AiAssistedResearchQuestion),
                "Not all AI events are displayed on History page (via 'Go to full history' link)");
        }

        /// <summary>
        /// Test case: 1875494
        /// Description: Verify recent questions dropdown functionality in AI Assistant
        /// 1. Verify: Recent questions button is displayed on the Home page.
        /// 2. Submit a question and Verify: Recent questions button is disabled while the answer is generating.
        /// 3. Verify: Recent questions button is not displayed after the answer is ready.
        /// 4. Start a new research and Verify: Asked question appears in the recent questions dropdown.
        /// 5. Navigate back to the Home page and Verify: Asked question appears in the dropdown on the Home page.
        /// 6. Select the question from the dropdown and Verify: Ability to run the question selected from the dropdown.
        /// </summary>
        //[Ignore]
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantRecentQuestionsDropdownTest()
        {
            const string Question = "For paid family leave, are siblings of employees covered as family members?";

            string checkRecentQuestionsOnHomePage = "Verify: Recent questions button is displayed on Home page";
            string checkRecentQuestionsIsDisabled = "Verify: Recent questions button is disabled when answer is generating";
            string checkRecentQuestionsIsNotDisplayed = "Verify: Recent questions button is not displayed after answer is ready";
            string checkQuestionAppearsInRecentQuestions = "Verify: Asked question appears in dropdown";
            string checkQuestionAppearsInRecentQuestionsHomePage = "Verify: Asked question appears in dropdown on the Home page";
            string checkAbilityToRunQuestionFromDropdown = "Verify: Ability to run question selected from dropdown";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            var aiAssistedResearchTab = homePage.BrowseTabPanel.SetActiveTab<AiAssistedResearchTabPanel>(PrecisionBrowseTab.AiAssistedResearch);

            this.TestCaseVerify.IsTrue(
                checkRecentQuestionsOnHomePage,
                aiAssistedResearchTab.RecentQuestionsDropdown.IsDisplayed(),
                "Recent questions button is NOT displayed on Home page");

            aiAssistedResearchTab = aiAssistedResearchTab.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchTabPanel>();
            aiAssistedResearchTab = aiAssistedResearchTab.QuestionTextbox.SetText<AiAssistedResearchTabPanel>(Question);
            var aiAssistantPage = aiAssistedResearchTab.SubmitButton.Click<AiAssistedResearchPage>();

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

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

            BrowserPool.CurrentBrowser.CloseTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

            aiAssistedResearchTab = homePage.BrowseTabPanel.SetActiveTab<AiAssistedResearchTabPanel>(PrecisionBrowseTab.AiAssistedResearch);

            SafeMethodExecutor.WaitUntil(() => aiAssistedResearchTab.RecentQuestionsDropdown.IsDisplayed());

            this.TestCaseVerify.IsTrue(
                checkQuestionAppearsInRecentQuestionsHomePage,
                aiAssistedResearchTab.RecentQuestionsDropdown.Options.First().Equals(Question),
                "Asked question doesn't appear in dropdown on the Home page");

            aiAssistedResearchTab = aiAssistedResearchTab.RecentQuestionsDropdown.SelectOption<AiAssistedResearchTabPanel>(Question);

            aiAssistantPage = aiAssistedResearchTab.SubmitButton.Click<AiAssistedResearchPage>();

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkAbilityToRunQuestionFromDropdown,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed,
                "Question is failing");
        }

        /// <summary>
        /// Test case: 1901935
        /// Description: Verify that recent questions dropdown correctly displays questions on corresponding pages (AI-Assisted Research, Treatise, Claims Explorer)
        /// 1. Ask a question on the Treatise page and Verify: Recent questions dropdown contains only Treatise questions.
        /// 2. Ask a question on the Claims Explorer page and Verify: Recent questions dropdown contains only Claims Explorer questions.
        /// 3. Ask a question on the AI-Assisted Research page and Verify: Recent questions dropdown contains only AI-Assisted Research questions.
        /// 4. Navigate through each page and Verify: Each page's recent questions dropdown only contains questions relevant to that page.
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantMixtureOfAiRecentQuestionsDropdownTest()
        {
            string SecondarySources = "Secondary Sources";
            const string ClaimsExplorerQuestion = "Claims Explorer: How close must a toilet paper dispenser be to a toilet under the ADA?";
            const string TreatiseQuestion = "Trestise: Can parties orally stipulate to change the date of a deposition?";
            const string AiAssistedResearchQuestion = "AI-Assisted Research: For paid family leave, are siblings of employees covered as family members?";

            string checkRecentQuestionsOnAiAssistedResearch = "Verify: (AI-Assisted research) Recent questions dropdown contains question asked from AI-Assisted";
            string checkRecentQuestionsOnClaimsExplorer = "Verify: (Claims Explorer) Recent questions dropdown contains question asked from Claims Explorer";
            string checkRecentQuestionsOnTreatise = "(Treatise page) Recent questions dropdown contains question asked from Treatise";

            //Ask on Treatise          
            var homePage = this.GetHomePage<PrecisionHomePage>();
            var contentTypeSearchResultsPage = this.GetHomePage<PrecisionHomePage>().BrowseTabPanel.SetActiveTab<ContentTypesTabPanel>(PrecisionBrowseTab.ContentTypes)
            .ClickLinkByText<CommonBrowsePage>(SecondarySources);
            var aiAssistantPage = contentTypeSearchResultsPage.ClickLinkByText<AiAssistedResearchPage>("Search & Summarize Rutter");

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(TreatiseQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);
          
            //Ask on Claims Explorer
            var claimsExplorerPage = this.ReturnToHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(ClaimsExplorerHeadingLabel).Click<AiClaimsExplorerPage>();
            BrowserPool.CurrentBrowser.CreateTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);
            var aiJurisdictionDialog = claimsExplorerPage.Toolbar.JurisdictionButton.Click<AiJurisdictionDialog>();
            claimsExplorerPage = aiJurisdictionDialog.SelectJurisdictions(true, Jurisdiction.Federal).SaveButton.Click<AiClaimsExplorerPage>();
            claimsExplorerPage = claimsExplorerPage.QueryBox.QuestionTextbox.SetText<AiClaimsExplorerPage>(ClaimsExplorerQuestion);
            claimsExplorerPage = claimsExplorerPage.QueryBox.SendQuestionButton.Click<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            BrowserPool.CurrentBrowser.CloseTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

            //Ask on AI-Assisted Research
            aiAssistantPage = this.ReturnToHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(AiAssistedResearchQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);
            aiAssistantPage = aiAssistantPage.Toolbar.NewResearchButton.Click<AiAssistedResearchPage>();

            if (!(aiAssistantPage.QueryBox.RecentQuestionsDropdown.Options.First().Equals(AiAssistedResearchQuestion)))
            {
                SafeMethodExecutor.WaitUntil(() =>
                {
                    aiAssistantPage = BrowserPool.CurrentBrowser.Refresh<AiAssistedResearchPage>();
                    bool questionPresent = aiAssistantPage.QueryBox.RecentQuestionsDropdown.Options.First().Equals(AiAssistedResearchQuestion);
                    return questionPresent;
                }, timeoutFromSec: 300, pollingIntervalInMilliseconds: 2000);
            }

            var firstThreeRecentQuestions = new List<string>();
            if (aiAssistantPage.QueryBox.RecentQuestionsDropdown.Options.ToList().Count >= 3)
            {
                firstThreeRecentQuestions = aiAssistantPage.QueryBox.RecentQuestionsDropdown.Options.ToList().Take(3).ToList();
            }
            else
            {
                firstThreeRecentQuestions = aiAssistantPage.QueryBox.RecentQuestionsDropdown.Options.ToList().Take(aiAssistantPage.QueryBox.RecentQuestionsDropdown.Options.ToList().Count).ToList();
            }

            this.TestCaseVerify.IsTrue(
                checkRecentQuestionsOnAiAssistedResearch,
                firstThreeRecentQuestions.Any(q => q.Equals(AiAssistedResearchQuestion))
                && !firstThreeRecentQuestions.Any(q => q.Equals(TreatiseQuestion))
                && !firstThreeRecentQuestions.Any(q => q.Equals(ClaimsExplorerQuestion)),
                "(AI-Assisted research) Recent questions dropdown doesn't contain question asked from AI-Assisted Research");

            BrowserPool.CurrentBrowser.CloseTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

            //Go to Claims Explorer page  
            claimsExplorerPage = homePage.Header.ExpandToolsFlyoutButton.Click<PrecisionToolsDialog>().ToolLinks.First(link => link.Text.Contains(ClaimsExplorerHeadingLabel)).Click<AiClaimsExplorerPage>();
            BrowserPool.CurrentBrowser.CreateTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            if (aiAssistantPage.QueryBox.RecentQuestionsDropdown.Options.ToList().Count >= 3)
            {
                firstThreeRecentQuestions = aiAssistantPage.QueryBox.RecentQuestionsDropdown.Options.ToList().Take(3).ToList();
            }
            else
            {
                firstThreeRecentQuestions = aiAssistantPage.QueryBox.RecentQuestionsDropdown.Options.ToList().Take(aiAssistantPage.QueryBox.RecentQuestionsDropdown.Options.ToList().Count).ToList();
            }

            this.TestCaseVerify.IsTrue(
                checkRecentQuestionsOnClaimsExplorer,
                !firstThreeRecentQuestions.Any(q => q.Equals(AiAssistedResearchQuestion))
                && !firstThreeRecentQuestions.Any(q => q.Equals(TreatiseQuestion))
                && firstThreeRecentQuestions.Any(q => q.Equals(ClaimsExplorerQuestion)),
                "(Claims Explorer) Recent questions dropdown doesn't contain question asked from Claims Explorer");

            BrowserPool.CurrentBrowser.CloseTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

            //Instantiate a new PrecisionToolsDialog (which will wrap the existing DOM element) and then click the close button:
            new PrecisionToolsDialog().CloseButton.Click<PrecisionHomePage>();

            //Go to Treatise page    
            contentTypeSearchResultsPage = this.GetHomePage<PrecisionHomePage>().BrowseTabPanel.SetActiveTab<ContentTypesTabPanel>(PrecisionBrowseTab.ContentTypes)
            .ClickLinkByText<CommonBrowsePage>(SecondarySources);

            aiAssistantPage = contentTypeSearchResultsPage.ClickLinkByText<AiAssistedResearchPage>("Search & Summarize Rutter");

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            if (aiAssistantPage.QueryBox.RecentQuestionsDropdown.Options.ToList().Count >= 3)
            {
                firstThreeRecentQuestions = aiAssistantPage.QueryBox.RecentQuestionsDropdown.Options.ToList().Take(3).ToList();
            }
            else
            {
                firstThreeRecentQuestions = aiAssistantPage.QueryBox.RecentQuestionsDropdown.Options.ToList().Take(aiAssistantPage.QueryBox.RecentQuestionsDropdown.Options.ToList().Count).ToList();
            }

            this.TestCaseVerify.IsTrue(
                checkRecentQuestionsOnTreatise,
                !firstThreeRecentQuestions.Any(q => q.Equals(AiAssistedResearchQuestion))
                && firstThreeRecentQuestions.Any(q => q.Equals(TreatiseQuestion))
                && !firstThreeRecentQuestions.Any(q => q.Equals(ClaimsExplorerQuestion)),
                "(Treatise page) Recent questions dropdown doesn't contain question asked from Treatise");
        }
 
        /// <summary>
        /// Test case: 1878336, 1880826, 1885941, 1878349, 1884786, 1884041
        /// Description: Verify history events for conversations with intent resolution message in AI Assistant
        /// 1. Submit a question and an intent resolver question, then Verify: History loads correctly with the intent resolver question.
        /// 2. Refresh the page and Verify: History loads correctly with the intent resolver question after refresh.
        /// 3. Change client ID and Verify: Welcome page is displayed.
        /// 4. Submit an intent resolver question and refresh the page, then Verify: Conversation with bypass message is retained after refresh.
        /// </summary>

        [TestMethod]
        [TestCategory("Bug 1957662")]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantIntentResolverHistoryTest()
        {
            const string Question = "How can a chapter 13 debtor qualify for a hardship discharge?";
            const string IntentResolverQuestion = "weather";
            string checkIntentResolverNotInHistory = "Verify: Intent resolver question does NOT show in history";
            string checkIntentResolverOutOfScopeMessage = "Verify: Intent resolver out of scope message is as expected";
            string IntentResolverMessage = "Your question appears to be outside of the scope of this feature, which uses generative AI to address legal research questions. Please rephrase your question and submit it again, or try another Westlaw research tool.";
            
            var aiAssistantPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(IntentResolverQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
               checkIntentResolverOutOfScopeMessage,
               aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionOutOfScopeMessageLabel.Text.Equals(IntentResolverMessage),
               "Intent resolver out of scope message is NOT as expected");

            aiAssistantPage = aiAssistantPage.ConversationHistory.ExpandButton.Click<AiAssistedResearchPage>();

            var historyPage = aiAssistantPage.ConversationHistory.GoToFullHistoryLink.Click<EdgeCommonHistoryPage>();

            BrowserPool.CurrentBrowser.CreateTab(HistoryPageTab);
            BrowserPool.CurrentBrowser.ActivateTab(HistoryPageTab);

            SafeMethodExecutor.WaitUntil(() => historyPage.HistoryTable.GetGridItems().Count >= 1);

            var historyQuestions = historyPage.HistoryTable.GetGridItems().Select(item => item.Title).ToList();

            this.TestCaseVerify.IsFalse(
                checkIntentResolverNotInHistory,
                historyQuestions.Any().Equals(IntentResolverQuestion),
                "Intent resolver question IS present in history");
        }


        /// <summary>
        /// Test case: 1913903, 1910611
        /// Description: Verify the functionality of the jurisdiction resolver in AI Assistant
        /// 1. Submit a question with a jurisdiction mismatch and Verify: Jurisdiction resolver message is as expected for the main question.
        /// 2. Submit a follow-up question with a different jurisdiction and Verify: Jurisdiction resolver message is as expected for the follow-up question.
        /// 3. Submit a follow-up question without a jurisdiction change and Verify: Jurisdiction resolver message is not displayed.
        /// 4. Deliver the report and Verify: Jurisdiction resolver message is present in the delivered report.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        [TestProperty(EnvironmentConstants.InfrastructureAccessControlsOn, "IAC-AI-JURISDICTION-RESOLVER")]
        public void AiAssistantJurisdictionResolverTest()
        {
            const string Question = "Is harassment under New York Penal Law s 240.26 a lesser included offense of assault under Penal Law s 120.00?";
            const string FollowUpQuestion = "Is harassment under Iowa Penal Law s 240.26 a lesser included offense of assault under Penal Law s 120.00?";
            const string SecondFollowUpQuestion = "What are the key differences between estoppel based on contractual principles and estoppel based on tort principles?";

            const string JurisdictionResolverMessage = "It looks like there's a difference between your selected jurisdiction and your query. The response below is based on the following jurisdiction";

            string checkMainJurisdictionResolver = "Verify: Jurisdiction resolver message for the main question is as expected";
            string checkFollowUpJurisdictionResolver = "Verify: Jurisdiction resolver message for the follow-up question is as expected";
            string checkNoResolverForFollowUpWithoutJurisdiction = "Verify: Jurisdiction resolver message isn't displayed for the follow-up w/o jurisdiction";
            string checkJurisdictionResolverInDelivery = "Verify: Jurisdiction resolver mesage is present in delivered report";

            var aiAssistantPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectJurisdictions(true, Jurisdiction.Guam).SaveButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage.Toolbar.JurisdictionInfoIconButton.Click();

            this.TestCaseVerify.IsTrue(
                checkMainJurisdictionResolver,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().JurisdictionResolverLabel.Text.Equals($"{JurisdictionResolverMessage}: New York")
                && aiAssistantPage.Toolbar.JurisdictionLabel.Text.Equals("New York")
                && aiAssistantPage.Toolbar.JurisdictionInfoBox.Text.Contains("New York")
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SupportingMaterialsItems.Any(item => item.MetadataLabel.Text.Contains("New York")),
                "Jurisdiction resolver message for the main question is NOT as expected");

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(FollowUpQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkFollowUpJurisdictionResolver,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).JurisdictionResolverLabel.Text.Equals($"{JurisdictionResolverMessage}: Iowa")
                && aiAssistantPage.Toolbar.JurisdictionLabel.Text.Equals("Iowa")
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).SupportingMaterials.SupportingMaterialsItems.Any(item => item.MetadataLabel.Text.Contains("Iowa")),
                "Jurisdiction resolver message for the follow-up question is NOT as expected");

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(SecondFollowUpQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(2).ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkNoResolverForFollowUpWithoutJurisdiction,
                !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(2).JurisdictionResolverLabel.Displayed
                && aiAssistantPage.Toolbar.JurisdictionLabel.Text.Equals("Iowa"),
                "Jurisdiction resolver message is displayed for the follow-up w/o jurisdiction");

            //Delivery
            var downloadDialog = aiAssistantPage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);
            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<AiAssistedResearchPage>();
            var fileName = $"Westlaw Precision - Westlaw AI-Assisted Research - {DateTime.Now.ToString(DeliveryDateFormat)}.pdf";
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            var text = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, fileName));

            var textWithoutWhitespaces = text.Replace(" ", string.Empty).Replace("\r\n", string.Empty);

            this.TestCaseVerify.IsTrue(
               checkJurisdictionResolverInDelivery,
               textWithoutWhitespaces.Contains(JurisdictionResolverMessage.Replace(" ", string.Empty).Replace("\r\n", string.Empty)),
               "Jurisdiction resolver mesage is NOT present in delivered report");
        }

        /// <summary>
        /// Test case: 1911253
        /// Description: Verify jurisdiction resolver functionality with query examples in AI Assistant
        /// 1. Submit a question about minimum wage requirements and Verify: Jurisdiction resolver message is correct for California and Nebraska.
        /// 2. Submit a question about recording calls and Verify: Jurisdiction resolver message is correct for California and Texas.
        /// 3. Submit a question about fraudulent concealment and Verify: Jurisdiction resolver message is correct for New York, Texas, and Utah.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        [TestProperty(EnvironmentConstants.InfrastructureAccessControlsOn, "IAC-AI-JURISDICTION-RESOLVER")]
        public void AiAssistantJurisdictionResolverExamplesTest()
        {
            const string Question = "Can an employer located in a particular city in California or Nebraska be required by the city to pay a minimum wage above what is required by state and federal law?";
            const string SecondQuestion = "If a call is recorded in Texas without the consent of a participant from California, can that recording be introduced in court?";
            const string ThirdQuestion = "What standard does the New York, Texas, Utah apply for fraudulent concealment?";

            const string JurisdictionResolverMessage = "It looks like there's a difference between your selected jurisdiction and your query. The response below is based on the following jurisdiction";

            var aiAssistantPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectJurisdictions(true, Jurisdiction.Nebraska).SaveButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                $"Verify: Jurisdiction resolver message is correct for {Question}",
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().JurisdictionResolverLabel.Text.Equals($"{JurisdictionResolverMessage}s: California, Nebraska")
                && aiAssistantPage.Toolbar.JurisdictionLabel.Text.Equals("California, Nebraska"),
                $"Verify: Jurisdiction resolver message is NOT correct for {Question}");

            aiAssistantPage = aiAssistantPage.Toolbar.NewResearchButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectJurisdictions(true, Jurisdiction.California).SaveButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(SecondQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                $"Verify: Jurisdiction resolver message is correct for {SecondQuestion}",
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().JurisdictionResolverLabel.Text.Equals($"{JurisdictionResolverMessage}s: California, Texas")
                && aiAssistantPage.Toolbar.JurisdictionLabel.Text.Equals("California, Texas"),
                $"Verify: Jurisdiction resolver message is NOT correct for {SecondQuestion}");

            aiAssistantPage = aiAssistantPage.Toolbar.NewResearchButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectJurisdictions(true, Jurisdiction.California).SaveButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(ThirdQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                $"Verify: Jurisdiction resolver message is correct for {ThirdQuestion}",
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().JurisdictionResolverLabel.Text.Equals($"{JurisdictionResolverMessage}s: New York, Texas, Utah")
                && aiAssistantPage.Toolbar.JurisdictionLabel.Text.Equals("New York, Texas, Utah"),
                $"Verify: Jurisdiction resolver message is NOT correct for {ThirdQuestion}");
        }

        /// <summary>
        /// Test case: 1877562
        /// Description: Verify the default tab on the Home page for different accounts in AI Assistant
        /// 1. Verify: 'AI-Assisted Research' tab is active by default if the AI feature is enabled for the account.
        /// 2. Sign out and sign in with a different account, Verify: 'Precision Research' tab is active by default if the AI feature is disabled for the account.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        [TestProperty("TwoUsers", "On")]
        public void AiAssistantDefaultTabTest()
        {
            string checkAiAssistedResearchTabActiveByDefault = "Verify: 'AI-Assisted Research' tab is active by default if AI feature enabled";
            string checkPrecisionResearchTabActiveByDefault = "Verify: 'Precision Research' tab is active by default if AI feature disabled";

            var homePage = this.GetHomePage<PrecisionHomePage>();

            this.TestCaseVerify.IsTrue(
                checkAiAssistedResearchTabActiveByDefault,
                homePage.BrowseTabPanel.IsActive(PrecisionBrowseTab.AiAssistedResearch),
                "'AI-Assisted Research' tab is NOT active by default if AI feature enabled");

            this.DefaultSignOnManager.SignOff();
            homePage = this.SignInToWlnAndClearCookies<PrecisionHomePage>(this.SecondUserInfo);

            this.TestCaseVerify.IsTrue(
                checkPrecisionResearchTabActiveByDefault,
                homePage.BrowseTabPanel.IsActive(PrecisionBrowseTab.PrecisionResearch),
                "'Precision Research' tab is NOT active by default if AI feature disabled");
        }

        /// <summary>
        /// Test case: 1911333
        /// Description: Verify the limit on concurrent searches in AI Assistant
        /// 1. Start multiple AI-assisted research searches in parallel and Verify: Limit concurrent searches warning message is displayed when exceeding 3 parallel searches.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantConcurrentSearchesLimitTest()
        {
            const string Question = "Can parties orally stipulate to change the date of a deposition?";

            string checkLimitConcurrentSearchesWarning = "Verify: Limit concurrent searches warning message is displayed for >3 parallel searches";

            var homePage = this.GetHomePage<PrecisionHomePage>();

            var aiAssistantPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            var aiAssistantLandingPageUrl = BrowserPool.CurrentBrowser.Url;

            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();

            BrowserTabManager.Instance.OpenUrlInNewTab($"{AiAssistedResearchTab}Second", aiAssistantLandingPageUrl);
            BrowserTabManager.Instance.SetTabActive($"{AiAssistedResearchTab}Second");

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();

            BrowserTabManager.Instance.OpenUrlInNewTab($"{AiAssistedResearchTab}Third", aiAssistantLandingPageUrl);
            BrowserTabManager.Instance.SetTabActive($"{AiAssistedResearchTab}Third");

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();

            BrowserTabManager.Instance.OpenUrlInNewTab($"{AiAssistedResearchTab}Fourth", aiAssistantLandingPageUrl);
            BrowserTabManager.Instance.SetTabActive($"{AiAssistedResearchTab}Fourth");

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();

            this.TestCaseVerify.AreEqual(
                checkLimitConcurrentSearchesWarning,
                "You can submit this query after one of your last AI searches receives a response.",
                aiAssistantPage.QueryBox.ConcurrentSearchesLimitInfobox.Text,
                "Limit concurrent searches warning message is NOT displayed for >3 parallel searches");
        }

        /// <summary>
        /// Test case: 1999187, 2002129, 1999149
        /// Description: Verify linkouts for AI-Assisted Research (AAR) and Claims Explorer in AI Assistant
        /// 1. Verify: 'Claims Explorer' linkout on the home page works as expected by navigating to the correct page and displaying the correct heading.
        /// 2. Verify: 'Claims Explorer' linkout on the landing page works as expected by navigating to the correct page and displaying the landing label.
        /// 3. Verify: 'AI-Assisted Research' linkout on the landing page works as expected by navigating to the correct page and displaying the correct heading.
        /// 4. Execute a question in Claims Explorer and Verify: 'AI-Assisted Research' linkout in the conversation works as expected.
        /// 5. Execute a question in AI-Assisted Research and Verify: 'Claims Explorer' linkout in the conversation works as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantLinkoutTest()
        {
            const string AssistedResearchQuestion = "Can parties orally stipulate to change the date of a deposition?";
            const string ClaimsQuestion = "You are an associate attorney writing an email summary for your supervising attorney who is determining whether to take on a client and wants you to summarize the relevant claims and issues. The potential client is a 60 year old woman who needs a cane to walk was fired abruptly and without explanation from her job in Minnesota. What possible claims does she have?";
            const string ClaimsHeading = "Claims Explorer";

            string checkHomePageClaimsLinkout = "Verify: 'Claims Explorer' linkout on the home page works as expected";
            string checkLandingPageClaimsLinkout = "Verify: 'Claims Explorer' linkout on the landing works as expected";
            string checkLandingPageAssistedResearchLinkout = "Verify: 'AI-Assisted Research' linkout on the landing works as expected";
            string checkConversationClaimsLinkout = "Verify: 'Claims Explorer' linkout on the conversation works as expected";
            string checkConversationAssistedResearchLinkout = "Verify: 'AI-Assisted Research' linkout on the conversation works as expected";

            var aiAssistedResearchTab = this.GetHomePage<PrecisionHomePage>().BrowseTabPanel.SetActiveTab<AiAssistedResearchTabPanel>(PrecisionBrowseTab.AiAssistedResearch);

            var claimsExplorerPage = aiAssistedResearchTab.TryClaimsExplorerLink.Click<AiClaimsExplorerPage>();

            BrowserPool.CurrentBrowser.CreateTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            this.TestCaseVerify.IsTrue(
               checkHomePageClaimsLinkout,
               BrowserPool.CurrentBrowser.Url.Contains("usageFeature=AIClaimsFinder")
               && claimsExplorerPage.Toolbar.HeadingLabel.Text.Equals(ClaimsHeading),
               "'Claims Explorer' linkout on the home page doesn't work as expected");

            var aiAssistantPage = claimsExplorerPage.Chat.TryLink.Click<AiAssistedResearchPage>();

            BrowserPool.CurrentBrowser.CreateTab($"{AiAssistedResearchTab}FromLanding");
            BrowserPool.CurrentBrowser.ActivateTab($"{AiAssistedResearchTab}FromLanding");

            SafeMethodExecutor.WaitUntil(() => claimsExplorerPage.QueryBox.QuestionTextbox.Displayed);

            this.TestCaseVerify.IsTrue(
                checkLandingPageClaimsLinkout,
                BrowserPool.CurrentBrowser.Title.Equals("Westlaw AI-Assisted Research | Westlaw Precision")
                && aiAssistantPage.Toolbar.HeadingLabel.Text.Equals(AIAssistedResearchHeadingLabel)
                && aiAssistantPage.Chat.LandingPageLabel.Displayed,
                "'Claims Explorer' linkout on the landing doesn't work as expected");

            claimsExplorerPage = aiAssistantPage.Chat.TryLink.Click<AiClaimsExplorerPage>();

            BrowserPool.CurrentBrowser.CreateTab($"{ClaimsExplorerTab}FromLanding");
            BrowserPool.CurrentBrowser.ActivateTab($"{ClaimsExplorerTab}FromLanding");

            this.TestCaseVerify.IsTrue(
               checkLandingPageAssistedResearchLinkout,
               BrowserPool.CurrentBrowser.Url.Contains("usageFeature=AIClaimsFinder")
               && claimsExplorerPage.Toolbar.HeadingLabel.Text.Equals(ClaimsHeading),
               "'AI-Assisted Research' linkout on the landing doesn't work as expected");

            var aiJurisdictionDialog = claimsExplorerPage.Toolbar.JurisdictionButton.Click<AiJurisdictionDialog>();
            claimsExplorerPage = aiJurisdictionDialog.SelectJurisdictions(true, Jurisdiction.California, Jurisdiction.Federal).SaveButton.Click<AiClaimsExplorerPage>();

            claimsExplorerPage = claimsExplorerPage.QueryBox.QuestionTextbox.SetText<AiClaimsExplorerPage>(ClaimsQuestion);
            claimsExplorerPage = claimsExplorerPage.QueryBox.SendQuestionButton.Click<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            var federalTab = claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.SetActiveTab<FederalTabComponent>(ClaimsExplorerAnswerTab.Federal);

            federalTab.TryAiAssistedResearchLink.ScrollToElement();

            aiAssistantPage = federalTab.TryAiAssistedResearchLink.Click<AiAssistedResearchPage>();

            BrowserPool.CurrentBrowser.CreateTab($"{AiAssistedResearchTab}FromConversation");
            BrowserPool.CurrentBrowser.ActivateTab($"{AiAssistedResearchTab}FromConversation");

            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkConversationClaimsLinkout,
                BrowserPool.CurrentBrowser.Title.Equals("Westlaw AI-Assisted Research | Westlaw Precision")
                && aiAssistantPage.Toolbar.HeadingLabel.Text.Equals(AIAssistedResearchHeadingLabel)
                && aiAssistantPage.Chat.LandingPageLabel.Displayed,
                "'Claims Explorer' linkout on the conversation doesn't work as expected");

            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(AssistedResearchQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            claimsExplorerPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().TryClaimsExplorerLink.Click<AiClaimsExplorerPage>();

            BrowserPool.CurrentBrowser.CreateTab($"{ClaimsExplorerTab}FromConversation");
            BrowserPool.CurrentBrowser.ActivateTab($"{ClaimsExplorerTab}FromConversation");

            this.TestCaseVerify.IsTrue(
               checkConversationAssistedResearchLinkout,
               BrowserPool.CurrentBrowser.Url.Contains("usageFeature=AIClaimsFinder")
               && claimsExplorerPage.Toolbar.HeadingLabel.Text.Equals(ClaimsHeading),
               "'AI-Assisted Research' linkout doesn't work as expected");
        }

        /// <summary>
        /// Test case: 2045792
        /// Description: Verify out-of-scope messaging in AI Assistant
        /// 1. Submit various out-of-scope questions and Verify: Out-of-scope message is displayed for each question.
        /// 2. Verify: Tips for best results dialog is displayed when clicking on the tips link.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantOutOfScopeMessagingTest()
        {
            const string OutOfScopeMessage = "Your question appears to be outside of the scope of this feature, which uses generative AI to address legal research questions. Please rephrase your question and submit it again, or try another Westlaw research tool.";
            const string TipsForBestResultsDialogTitle = "Tips for best results";

            List<string> questions = new List<string>
            {
                "How many times did the supreme court reverse the fifth circuit in the 2020 term",
                "Find Kansas appellate cases overturning a fee award by Judge Robert Wonnell",                
            };

            string checkTipsDialog = "Verify: Tips displog is displayed";

            var aiAssistantPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchPage>();

            foreach (var question in questions)
            {
                aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(question);
                aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
                SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

                this.TestCaseVerify.AreEqual(
                    $"Verify: Out of scope message is displayed for {question}",
                    OutOfScopeMessage,
                    aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionOutOfScopeMessageLabel.Text,
                    $"Out of scope message is NOT displayed for {question}");
            }

            var tipsForBestResultsDialog = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().TipsLink.Click<TipsForBestResultsDialog>();

            this.TestCaseVerify.IsTrue(
                checkTipsDialog,
                tipsForBestResultsDialog.TitleLabel.Text.Equals(TipsForBestResultsDialogTitle),
                "Tips dialog is not displayed");
        }

        /// <summary>
        /// Test case: 2087207, 2090687, 2104321
        /// Description: Verify skills detection in AI Assistant for various types of queries
        /// 11. Submit a jurisdictional survey question and Verify: Jurisdictional Surveys skill detection message is displayed, and it does not create a history item.
        /// 2. Refresh the page and Verify: Skills detection message is displayed, and the question is retained.
        /// 3. Verify: Jurisdictional Surveys link navigates to the correct page, and the Continue button works for Jurisdictional Surveys question.
        /// 4. Submit a claims-related question and Verify: Claims Explorer skill detection message is displayed.
        /// 5. Verify: Claims Explorer link navigates to the correct page, and the Continue button works for Claims question.
        /// 6. Submit a date restriction question and Verify: Date restriction skill detection message is displayed.
        /// </summary>
        //[Ignore]
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        [TestProperty(EnvironmentConstants.InfrastructureAccessControlsOn, "IAC-AI-INTENT-RESOLVER-SKILLS, IAC-AI-PROFILE15")]
        public void AiAssistantSkillsDetectionTest()
        {
            const string DateRestrictionQuestion = "does westlaw have an article from ct insider feb 10, 2076 on bridgeport FOI";
            const string JurisdictionalSurveysSkillQuestion = "find the lemon law for all 50 states";
            const string ClaimsSkillQuestion = "What claims are available related to fax fraud?";
            const string FiftyStatesSurveyTab = "50 States Jurisdiction Survey page";
            const string ClaimsExplorerTab = "Claims Explorer page";

            string checkDateRestrictionSkillsDetectionMessage = "Verify: Date restriction skill detection message is displayed";
            string checkJurisdictionalSurveysSkillsDetectionMessage = "Verify: Jurisdictional Surveys skill detection message is displayed";
            string checkSkillsDetectionMessageAfterRefresh = "Verify: Skills detection message is displayed, question is not empty";
            string checkJurisdictionalSurveysLink = "Verify: Jurisdictional Surveys link works";
            string checkContinueButtonForJurisdictionalSurveysQuestion = $"Verify: Continue button works for {JurisdictionalSurveysSkillQuestion}";
            string checkClaimsSkillsDetectionMessage = "Verify: Claims Explorer skill detection message is displayed";
            string checkClaimsExplorerLink = "Verify: Claims Explorer link works";
            string checkContinueButtonForClaimsQuestion = $"Verify: Continue button works for {ClaimsSkillQuestion}";

            var aiAssistantPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(AIAssistedResearchHeadingLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            var recentHistoryDialog = aiAssistantPage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);

            var historyPage = recentHistoryDialog.ViewAllLink.Click<EdgeCommonHistoryPage>();

            aiAssistantPage = BrowserPool.CurrentBrowser.GoBack<AiAssistedResearchPage>();

            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectJurisdictions(true, Jurisdiction.AllStates).SaveButton.Click<AiAssistedResearchPage>();

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(JurisdictionalSurveysSkillQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkJurisdictionalSurveysSkillsDetectionMessage,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SkillsDetectionLabel.Displayed
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SkillsDetectionLabel.Text.Contains("Reviewing statutes and regulations across jurisdictions? Use "),
                "Skills detection message is NOT displayed");

            BrowserPool.CurrentBrowser.Refresh<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SkillsDetectionLabel.Displayed);

            this.TestCaseVerify.IsTrue(
            checkSkillsDetectionMessageAfterRefresh,
            aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SkillsDetectionLabel.Displayed
            && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ContinueUsingAiAssisitedResearchButton.Displayed
            && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.Contains(JurisdictionalSurveysSkillQuestion),
            "Skills detection message is NOT displayed, question is empty");

            var surveysPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AiJurisdictionalSurveysLink.Click<AiJurisdictionalSurveysPage>();
            BrowserPool.CurrentBrowser.CreateTab(FiftyStatesSurveyTab);
            BrowserPool.CurrentBrowser.ActivateTab(FiftyStatesSurveyTab);
            SafeMethodExecutor.ExecuteUntil(() => surveysPage.PageDescription.Displayed, timeoutFromSec: 10);

            this.TestCaseVerify.IsTrue(
                checkJurisdictionalSurveysLink,
                surveysPage.PageDescription.Text.Equals("Survey search criteria"),
                "Clicking Surveys link on AAR result page did not take to survey home page");

            BrowserPool.CurrentBrowser.CloseTab(FiftyStatesSurveyTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ContinueUsingAiAssisitedResearchButton.Click();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkContinueButtonForJurisdictionalSurveysQuestion,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed
                && !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ErrorAnswerLabel.Displayed,
                "Continue button DOESN'T work");

            aiAssistantPage = aiAssistantPage.Toolbar.NewResearchButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectJurisdictions(true, Jurisdiction.NewYork).SaveButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(ClaimsSkillQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkClaimsSkillsDetectionMessage,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SkillsDetectionLabel.Displayed
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SkillsDetectionLabel.Text.Contains("Researching causes of action? Use "),
                "Skills detection message is NOT displayed");

            var claimsExplorerPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ClaimsExplorerLink.Click<AiClaimsExplorerPage>();
            BrowserPool.CurrentBrowser.CreateTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            this.TestCaseVerify.IsTrue(
               checkClaimsExplorerLink,
               claimsExplorerPage.Chat.LandingPageLabel.Text.Contains("Claims Explorer"),
               "Claims Explorer link DOESN'T work");

            BrowserPool.CurrentBrowser.CloseTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ContinueUsingAiAssisitedResearchButton.Click();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage = aiAssistantPage.Toolbar.NewResearchButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(DateRestrictionQuestion);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkDateRestrictionSkillsDetectionMessage,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Text.Replace("\r\n", string.Empty).Equals("No results found matching your date range. Please try a broader date range.")
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ClaimsExplorerLink.Displayed
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AiJurisdictionalSurveysLink.Displayed,
                "Date restriction skill detection message is NOT displayed");
        }

        // <summary>
        /// Description: Verify AAR copy link functionality like label displayed after clicking Copy link Icon button,open the link in new tab and verify the question and answer displayed in the new tab.
        /// 1. Navigate to the Westlaw Precision homepage.
        /// 2. Access the AI Assisted Research tab.
        /// 3. Enter a question regarding family leave in the question textbox.
        /// 5. Submit the question and wait for the AI response.
        /// 6. Save the question and answer to a folder.
        /// 7. Open History page
        /// 8. Select and click last question
        /// 9. Click on the Copy link Icon button in the toolbar and save link.
        /// 10.Open Folders page and go to saved search
        /// 11.Click on the Copy link Icon button in the toolbar and save link.
        /// 12.Log off and log in back.
        /// 13.Open a new tab using the copied link from history page and verify that the question and answer are displayed correctly.
        /// 14.Open a new tab using the copied link from folders page and verify that the question and answer are displayed correctly.
        /// </summary>
        /// Bug 2179860: AAR: Copied Link Includes "##" Special Characters in Query Answer – Should Be Removed
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantResearchCopylinkHistoryAndFolderingTest()
        {
            const string Question = "For paid family leave, are siblings of employees covered as family members?";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            var aiAssistedResearchTab = homePage.BrowseTabPanel.SetActiveTab<AiAssistedResearchTabPanel>(PrecisionBrowseTab.AiAssistedResearch);
            aiAssistedResearchTab = aiAssistedResearchTab.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchTabPanel>();
            aiAssistedResearchTab = aiAssistedResearchTab.QuestionTextbox.SetText<AiAssistedResearchTabPanel>(Question);
            var aiAssistantPage = aiAssistedResearchTab.SubmitButton.Click<AiAssistedResearchPage>();

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            SafeMethodExecutor.ExecuteUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed, timeoutFromSec: 10);
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Toolbar.CopyLinkButton.Displayed);

            var QuestionBeforeCopyLink = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text;
            var AnswerBeforeCopyLink = this.CleanTextForCompare(aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Text);

            var saveToFolderDialog = aiAssistantPage.Toolbar.SaveToFolderButton.Click<EdgeSaveToFolderDialog>();
            string rootFolder = saveToFolderDialog.FolderTreeComponent.GetRootFolderName();
            saveToFolderDialog.FolderTreeComponent.SelectFolderByName(rootFolder);
            saveToFolderDialog.ClickSaveButton<AiAssistedResearchPage>();

            var recentHistoryDialog = homePage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);
            var historyPage = recentHistoryDialog.ClickViewThisHistoryButton<EdgeCommonHistoryPage>();
            aiAssistantPage = historyPage.HistoryTable.GetGridItems().First().ClickLinkByText<AiAssistedResearchPage>(Question);
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage.Toolbar.CopyLinkButton.Click();
            var CopiedLinkFromHistory = Clipboard.GetText();

            var recentFolderDialog = aiAssistantPage.Header.ClickHeaderTab<EdgeRecentFoldersDialog>(EdgeHeaderTabs.Folders);
            var folderPage = recentFolderDialog.ClickFolderByName(rootFolder).ClickViewThisFolderButton();
            aiAssistantPage = folderPage.FolderGrid.ClickGridItemByName<AiAssistedResearchPage>(Question);
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage.Toolbar.CopyLinkButton.Click();
            var CopiedLinkFromFolders = Clipboard.GetText();

            this.DefaultSignOnManager.SignOff();

            homePage = this.SignOnBack().ClickContinueButton<PrecisionHomePage>();
            BrowserPool.CurrentBrowser.CreateTab("Westlaw Precision");
            BrowserPool.CurrentBrowser.ActivateTab("Westlaw Precision");

            var aiAssistedCopylinkpage = this.GetHomePage<AiAssistedResearchPage>()
                   .OpenNewTabUsingJavascript<AiAssistedResearchPage>("Westlaw AI-Assisted Research | Westlaw Precision", CopiedLinkFromHistory);

            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);
            this.TestCaseVerify.AreEqual($"Verify the Question is same the query asked {QuestionBeforeCopyLink}",
                         QuestionBeforeCopyLink,
                   aiAssistedCopylinkpage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text,
                    $"Question is not same the query asked displayed(History)");

            this.TestCaseVerify.AreEqual(
                   $"Verify the Answer is same the query asked {AnswerBeforeCopyLink}",
                   AnswerBeforeCopyLink,
                   this.CleanTextForCompare(aiAssistedCopylinkpage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Text),
                   $"Expect Answer not displayed (History)");

            BrowserPool.CurrentBrowser.CloseTab("Westlaw AI-Assisted Research | Westlaw Precision");
            string tabName = BrowserPool.CurrentBrowser.TabHandles.Last();
            BrowserPool.CurrentBrowser.CreateTab(tabName);
            BrowserPool.CurrentBrowser.ActivateTab(tabName);

            var newAiAssistedCopylinkpage = this.GetHomePage<AiAssistedResearchPage>()
                   .OpenNewTabUsingJavascript<AiAssistedResearchPage>("Westlaw AI-Assisted Research | Westlaw Precision", CopiedLinkFromFolders);

            this.TestCaseVerify.AreEqual($"Verify the Question is same the query asked {QuestionBeforeCopyLink}",
                    QuestionBeforeCopyLink,
                    newAiAssistedCopylinkpage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text,
                    $"Question is not same the query asked displayed(Foldering)");

            var text = this.CleanTextForCompare(newAiAssistedCopylinkpage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Text);

            this.TestCaseVerify.AreEqual(
                   $"Verify the Answer is same the query asked {AnswerBeforeCopyLink}",
                   AnswerBeforeCopyLink,
                   this.CleanTextForCompare(newAiAssistedCopylinkpage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Text),
                       $"Expect Answer not displayed (Foldering)");
        }

        /// <summary>
        /// Description: Verify AAR copy link functionality like label displayed after clicking Copy link Icon button,Check alert loading process label displayed.
        /// 1. Navigate to the Westlaw Precision homepage.
        /// 2. Access the AI Assisted Research tab.
        /// 3. Enter a question regarding family leave in the question textbox.
        /// 5. Submit the question and wait for the AI response.
        /// 6. Click on the Copy link Icon button in the toolbar.
        /// 7. Verify that the copy link label is displayed with the text "The link has been copied successfully."
        /// 8. Open a new tab using the copied link and verify that Alert loading process label displayed 
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantResearchCopylinkAlertLoadingProcessTest()
        {
            const string Question = "For paid family leave, are siblings of employees covered as family members?";
            const string ExpectedAlertLoadingProcessText = "Loading your response, this may take a few moments...";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            var aiAssistedResearchTab = homePage.BrowseTabPanel.SetActiveTab<AiAssistedResearchTabPanel>(PrecisionBrowseTab.AiAssistedResearch);
            aiAssistedResearchTab = aiAssistedResearchTab.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchTabPanel>();
            aiAssistedResearchTab = aiAssistedResearchTab.QuestionTextbox.SetText<AiAssistedResearchTabPanel>(Question);
            var aiAssistantPage = aiAssistedResearchTab.SubmitButton.Click<AiAssistedResearchPage>();

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            SafeMethodExecutor.ExecuteUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Toolbar.CopyLinkButton.Displayed);

            var QuestionBeforeCopyLink = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text;
            var AnswerBeforeCopyLink = this.CleanTextForCompare(aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Text);

            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage.Toolbar.CopyLinkButton.Click();
            var CopiedLinkFromAAR = Clipboard.GetText();

            this.DefaultSignOnManager.SignOff();

            homePage = this.SignOnBack().ClickContinueButton<PrecisionHomePage>();
            BrowserPool.CurrentBrowser.CreateTab("Westlaw Precision");
            BrowserPool.CurrentBrowser.ActivateTab("Westlaw Precision");

            var aiAssistedCopylinkpage = this.GetHomePage<AiAssistedResearchPage>()
                   .OpenNewTabUsingJavascript<AiAssistedResearchPage>("Westlaw AI-Assisted Research | Westlaw Precision", CopiedLinkFromAAR);

            SafeMethodExecutor.WaitUntil(() => aiAssistedCopylinkpage.Chat.AiResearchQuestionAndAnswerItems.First().AlertLoadingProcessLabel.Displayed, timeoutFromSec: 5);

            this.TestCaseVerify.AreEqual($"Verify alert loading process Text displayed after clicking Copy link Icon button {ExpectedAlertLoadingProcessText}",
                       ExpectedAlertLoadingProcessText,
                       aiAssistedCopylinkpage.Chat.AiResearchQuestionAndAnswerItems.First().AlertLoadingProcessLabel.Text,
                    $"Alert loading process label not displayed after clicking Copy link Icon button.");
        }

        /// <summary>
        /// Description: Verify AAR synchronization with AI Jurisdictional Surveys.
        /// 1. Navigate to the Westlaw Precision homepage.
        /// 2. Access the AI Assisted Research tab.
        /// 3. Enter a question regarding family leave in the question textbox.
        /// 4. Submit the question and wait for the AI response.
        /// 5. Verify that the Jurisdictional Surveys link is displayed.
        /// 6. Click on the Jurisdictional Surveys link.
        /// 7. Verify that the Jurisdictional Surveys page is displayed.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantResearchSynchronizationWithJurisdictionalSurveysTest()
        {
            const string Question = "Find the lemon law for all 50 states";
            const string FiftyStatesSurveyTab = "50 States Jurisdiction Survey page";
            string checkJurisdictionalSurveysLink = "Verify: Clicked on Jurisdictional Surveys link and page is displayed";
            string checkJurisdictionalSurveysLinkLabel = "Verify: Ai Jurisdictional Surveys Link label displayed";
            string checkQuestionTextAreaField = "Verify: Same Ai Assistant Research Question is displayed in Jurisdictional Surveys Page";
            string checkContinueUsingARRButtonLabel = "Verify: Continue using AI-Assisted Research Button label displayed";
            const string ExpectedJurisdictionalSurveyLinkText = "AI Jurisdictional Surveys";
            const string ExpectedContinueUsingARRButtonText = "Continue using AI-Assisted Research";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            var aiAssistedResearchTab = homePage.BrowseTabPanel.SetActiveTab<AiAssistedResearchTabPanel>(PrecisionBrowseTab.AiAssistedResearch);
            aiAssistedResearchTab = aiAssistedResearchTab.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectJurisdictions(true, Jurisdiction.Florida).SaveButton.Click<AiAssistedResearchTabPanel>();
            aiAssistedResearchTab = aiAssistedResearchTab.QuestionTextbox.SetText<AiAssistedResearchTabPanel>(Question);
            var aiAssistantPage = aiAssistedResearchTab.SubmitButton.Click<AiAssistedResearchPage>();

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            SafeMethodExecutor.ExecuteUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed, timeoutFromSec: 5);
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.AiJurisdictionalSurveysLink.Displayed, timeoutFromSec: 10);
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Chat.ContinueUsingARRButton.Displayed, timeoutFromSec: 10);

            this.TestCaseVerify.IsTrue(
                checkJurisdictionalSurveysLinkLabel,
                aiAssistantPage.Chat.AiJurisdictionalSurveysLink.Text.Contains(ExpectedJurisdictionalSurveyLinkText),
                "Ai Jurisdictional Surveys Link label not displayed");

            this.TestCaseVerify.IsTrue(
                checkContinueUsingARRButtonLabel,
                aiAssistantPage.Chat.ContinueUsingARRButton.Text.Contains(ExpectedContinueUsingARRButtonText),
                "Ai Jurisdictional Surveys Link label not displayed");

            var surveysPage = aiAssistantPage.Chat.AiJurisdictionalSurveysLink.Click<AiJurisdictionalSurveysPage>();

            BrowserPool.CurrentBrowser.CreateTab(FiftyStatesSurveyTab);
            BrowserPool.CurrentBrowser.ActivateTab(FiftyStatesSurveyTab);
            SafeMethodExecutor.ExecuteUntil(() => surveysPage.JurisdictionalSurveysTitle.Displayed, timeoutFromSec: 10);
            SafeMethodExecutor.WaitUntil(() => surveysPage.QuestionTextarea.Displayed, timeoutFromSec: 10);

            this.TestCaseVerify.IsTrue(
                checkJurisdictionalSurveysLink,
                surveysPage.JurisdictionalSurveysTitle.Text.Equals("AI Jurisdictional Surveys"),
                "Clicking Surveys link on AAR result page did not take to survey home page");

            var actualText = surveysPage.QuestionTextarea.GetAttribute("current-value");
            this.TestCaseVerify.IsTrue(
                checkQuestionTextAreaField, actualText.Equals(Question),
                "Same Question dint displayed in Jurisdictional Surveys Page as Queried in Ai Assistant Research");
        }

        /// <summary>
        /// Description: Verify AAR copy link delivery functionality .
        /// 1. Navigate to the Westlaw Precision homepage.
        /// 2. Open the AAR link.
        /// 3. Enter a question regarding family leave in the question textbox.
        /// 4. Click on the Copy link Icon button in the toolbar and save link.
        /// 5.Log off and log in back.
        /// 6.Open a new tab using the copied link.
        /// 7. Download document and veryfy the content
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AiAssistantResearchCopyLinkDeliveryTest()
        {
            const string Question = "For paid family leave, are siblings of employees covered as family members?";
            const string ExpectedCopyLinkText = "The link has been copied successfully.";
            string checkCopiedLinkBrowserTabTitle = "Verify: Browse tab title is correct in the copied link";
            string checkCopiedLinkPageHeading = "Verify: page heading is correct in the copied link";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            var aiAssistedResearchTab = homePage.BrowseTabPanel.SetActiveTab<AiAssistedResearchTabPanel>(PrecisionBrowseTab.AiAssistedResearch);
            aiAssistedResearchTab = aiAssistedResearchTab.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchTabPanel>();
            aiAssistedResearchTab = aiAssistedResearchTab.QuestionTextbox.SetText<AiAssistedResearchTabPanel>(Question);
            var aiAssistantPage = aiAssistedResearchTab.SubmitButton.Click<AiAssistedResearchPage>();

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            SafeMethodExecutor.ExecuteUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Toolbar.CopyLinkButton.Displayed);

            var QuestionBeforeCopyLink = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text;
            var AnswerBeforeCopyLink = this.CleanTextForCompare(aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Text);
            var originalBrowserTitle = BrowserPool.CurrentBrowser.Title;
            var originalPageHeading = aiAssistantPage.Toolbar.HeadingLabel.Text;

            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            aiAssistantPage.Toolbar.CopyLinkButton.Click();
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Toolbar.CopiedLinkSuccessLabel.Displayed, timeoutFromSec: 10);
            var CopiedLinkFromAAR = Clipboard.GetText();

            this.TestCaseVerify.IsTrue(
                 "Verify copy link label displayed after clicking Copy link Icon button",
                 aiAssistantPage.Toolbar.CopiedLinkSuccessLabel.Text.Contains(ExpectedCopyLinkText),
                 "copy link success label not displayed after clicking Copy link Icon button.");
            
            this.DefaultSignOnManager.SignOff();

            homePage = this.SignOnBack().ClickContinueButton<PrecisionHomePage>();
            BrowserPool.CurrentBrowser.CreateTab("Westlaw Precision");
            BrowserPool.CurrentBrowser.ActivateTab("Westlaw Precision");

            var newAiAssistedCopylinkpage = this.GetHomePage<AiAssistedResearchPage>()
                   .OpenNewTabUsingJavascript<AiAssistedResearchPage>("Westlaw AI-Assisted Research | Westlaw Precision", CopiedLinkFromAAR);
            
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkCopiedLinkBrowserTabTitle,
                BrowserPool.CurrentBrowser.Title.Equals(originalBrowserTitle),
                "Browser tab title is not correct in copied link");

            this.TestCaseVerify.IsTrue(
                checkCopiedLinkPageHeading,
                aiAssistantPage.Toolbar.HeadingLabel.Text.Equals(originalPageHeading),
                "Heading is NOT correct in copied link");

            this.TestCaseVerify.AreEqual(
                $"Verify the Question is same the query asked {QuestionBeforeCopyLink}",
                QuestionBeforeCopyLink,
                newAiAssistedCopylinkpage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text,
                $"Question is not same the query asked displayed");
                
           var aiAssistedSummary = this.CleanTextForCompare(aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Text);

            var downloadDialog = newAiAssistedCopylinkpage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);
            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<AiAssistedResearchPage>();
            var fileName = $"Westlaw Precision - Westlaw AI-Assisted Research - {DateTime.Now.ToString(DeliveryDateFormat)}.pdf";
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            var text = this.CleanTextForCompare(PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, fileName)));

            this.TestCaseVerify.IsTrue(
                "Verify AAR result displayed for selected Query",
                aiAssistedSummary.Any(summary => text.Contains(summary)),
                $"AAR result not displayed for selected Query ");

            this.TestCaseVerify.IsTrue(
                "Verify the Answer is same the query asked",
                aiAssistedSummary.Any(summary => text.Contains(summary)),
                $"Expect Answer not displayed");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamSahniCategory)]
        [TestCategory(TeamSahniFedRampCategory)]
        public void AIDeepResearchToolsTabPrecisionTest()
        {
            const string ExpectedTextPresent = "AI Deep Research";
            string checkToolsDialogAIDeepResearchPresent = "Verify: AI Deep Research link IS not present in Tools Tab panel - Precision";

            var homePage = this.GetHomePage<PrecisionHomePage>();

            var toolsTabPanel = homePage.BrowseTabPanel.SetActiveTab<PrecisionToolsTabPanel>(PrecisionBrowseTab.Tools);

            var toolsTabAiDeepResearchLink = toolsTabPanel.ToolsItems.FirstOrDefault(item => item.HeaderLink.Text == ExpectedTextPresent);

            this.TestCaseVerify.IsFalse(
                 checkToolsDialogAIDeepResearchPresent,
                 toolsTabAiDeepResearchLink.HeaderLink.Displayed,
                 "AI Deep Research link is present in Tools tab panel - Precision");
        }

        protected override void InitializeRoutingPageSettings()
        {
            this.Settings.AppendValues(
                EnvironmentConstants.InfrastructureAccessControlsOn,
                SettingUpdateOption.Append,
                "IAC-AI-TREATISE-RUTTER");

            if (this.TestContext.Properties["AiResearch"] != null && this.TestContext.Properties["AiResearch"].Equals("Off"))
            {
                this.Settings.AppendValues(
                    EnvironmentConstants.FeatureAccessControlsOff,
                    SettingUpdateOption.Append,
                    FeatureAccessControl.AiResearch);
            }
            else if (this.TestContext.Properties["AiDoNotTrain"] != null && this.TestContext.Properties["AiDoNotTrain"].Equals("On"))
            {
                this.Settings.AppendValues(
                    EnvironmentConstants.FeatureAccessControlsOn,
                    SettingUpdateOption.Append,
                    FeatureAccessControl.AiDoNotTrain);
            }
            else
            {
                base.InitializeRoutingPageSettings();
            }
        }

        protected override void OnManageCredential()
        {
            if (this.TestContext.Properties["TwoUsers"] != null && this.TestContext.Properties["TwoUsers"].Equals("On"))
            {
                var isFedRamp = this.Settings.GetValue(EnvironmentConstants.IsFedRamp);

                UserCredential firstUserCredential = (isFedRamp != null && isFedRamp.ToLower().Equals("yes")) && this.TestContext.Properties["TwoUsers"].Equals("On")
                                               ? new UserCredential { UserName = "WestlawPrecision1FR@mailinator.com", Password = "Regre$$ion89", ClientId = "Precision Test" }
                                               : new UserCredential { Email = "precision1@thomsonreuters.com", Password = "Westlaw1!", ClientId = "Precision Test" };

                UserCredential secondUserCredential = (isFedRamp != null && isFedRamp.ToLower().Equals("yes")) && this.TestContext.Properties["TwoUsers"].Equals("On")
                                               ? new UserCredential { UserName = "WestlawPrecision2FR@mailinator.com", Password = "Regre$$ion90", ClientId = "Precision Test" }
                                               : new UserCredential { Email = "precision2@thomsonreuters.com", Password = "Westlaw1!", ClientId = "Precision Test" };

                CredentialPool.RegisterUser(firstUserCredential);
                CredentialPool.RegisterUser(secondUserCredential);

                this.DefaultSignOnContext = new WlnSignOnContext<IUserInfo>(
                                                this.TestExecutionContext,
                                                firstUserCredential.ToWlnUserInfo());

                this.FirstUserInfo = firstUserCredential.ToWlnUserInfo();

                this.SecondUserInfo = secondUserCredential.ToWlnUserInfo();

                this.DefaultSignOnContext = new WlnSignOnContext<IUserInfo>(this.TestExecutionContext, this.FirstUserInfo);
            }
            else
            {
                base.OnManageCredential();
            }
        }

        private string CleanTextForCompare(string text)
        {
            text = text.Replace(" ", string.Empty).Replace(")", string.Empty).Replace("\r\n", string.Empty);
            text = Regex.Replace(text, @"\b\d+\.\b", string.Empty);
            text = Regex.Replace(text, @"GovernmentWorks\.\b.?", string.Empty).Replace($"WestlawAI-AssistedResearch•Responsegenerated:{DateTime.Now:MMMMd,yyyy}WestlawPrecision.©{DateTime.Now:yyyy}ThomsonReuters.NoclaimtooriginalU.S.", string.Empty).Replace("•", string.Empty);

            return text;
        }
    }
}
