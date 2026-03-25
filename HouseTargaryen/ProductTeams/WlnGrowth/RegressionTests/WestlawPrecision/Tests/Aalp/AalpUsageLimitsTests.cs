namespace WestlawPrecision.Tests.Aalp
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.Utils.Extensions;
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Header;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.Utils.Execution;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.AALP;
    using System.Threading;
    using System;
    using System.IO;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.NewTypeAhead;
    using Framework.Common.UI.Products.WestlawEdge.Pages.SearchResult.ContentTypeSearchResultPages;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.NewTypeAhead;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Toolbars;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;

    /// <summary>
    /// AALP usage limits tests
    /// </summary>
    [TestClass]
    public class AalpUsageLimitsTests : AalpBaseTest
    {
        private const string FeatureTestCategoryUsageLimit = "UsageLimit";
        private const string FeatureTestCategoryOOP = "AalpOopAccess";

        /// <summary>
        /// Verify usage limit message when daily limit reached
        /// Test cases: 1864863,1864864, 1864865  User Story 1842405
        /// 1. Sign in WL Precision IAC and set daily search limit to 1: 
        ///    IAC-AI-ASSISTANT-USAGE-LIMITS-DEBUG
        /// 2. Click AI-Assisted Research button (opens in new browser tab)
        /// 3. Ask one question: What is the definition of substantial evidence?
        /// 4. Check: Verify expected block message displayed
        /// 5. Check: Verify Question input box is hidden
        /// 6. Check: Verify Jurisdiction button is hidden
        /// 7. Check: Verify New conversation button is disabled
        /// 8. Go to AI-Assisted Research tab on home page tab
        /// 9: Check: Verify question input box disabled on Home page when research limit reached
        /// 10.Check: Verify Submit button disabled on Home page when research limit reached
        /// 11.Expand left History panel and click first entry
        /// 12.Check: Verify Question input box is hidden viewing from History
        /// 13.Check: Verify Jurisdiction button is hidden viewing from History
        /// 14.Check: Verify New conversation button is disabled viewing from History
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryUsageLimit)]
        [TestCategory(TeamDahlNonAjsCategory)]
        [TestCategory(TeamDahlCategory)]
        [TestProperty("ResearchDailyLimit", "1")]
        [TestCategory("TransitionToSharat")]
        public void AiAssistantUsageLimitTest()
        {
            const string HomePageTab = "Home page";
            const string Question = "What is the definition of substantial evidence?";
            const string AssistedResearchLabel = "AI-Assisted Research";

            string checkBlockMessage = "Verify expected block message displayed";
            string checkQuestionBoxHidden = "Verify question input box is hidden with block message displayed";
            string checkJurisButtonHidden = "Verify Jurisdiction button is hidden with block message displayed";
            string checkNewConversationButtonDisabled = "Verify New conversation button is disabled with block message displayed";
            string checkHomePageTextboxDisabled = "Verify question input box disabled on Home page when research limit reached";
            string checkHomePageSubmitButtonDisabled = "Verify Submit button disabled on Home page when research limit reached";
            string checkQuestionBoxHiddenHistory = "Verify question input box is hidden with block message displayed viewing from History";
            string checkJurisButtonHiddenHistory = "Verify Jurisdiction button is hidden with block message displayed viewing from History";
            string checkNewConversationButtonDisabledHistory = "Verify New conversation button is disabled with block message displayed viewing from History";

            var precisionHomePage = this.GetHomePage<PrecisionHomePage>();
            BrowserPool.CurrentBrowser.CreateTab(HomePageTab);

            var aiAssistantPage = precisionHomePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AssistedResearchLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            // In case usage limit already reached, set date back and refresh page
            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchPage>();
            aiAssistantPage.UsageDebug.BackDateExpiryButton.Click();
            BrowserPool.CurrentBrowser.Refresh();

            // Ask a question 
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            var expectedBlockMessage = "You've reached your daily limit of " + aiAssistantPage.UsageDebug.DailyLimitLabel.Text
                + " AI-Assisted Research questions. This limit resets every night at 12:00 a.m. Central time."
                + " You can still access your prior AI-Assisted Research via your History.";
            var displayedBlockMessage = aiAssistantPage.QueryBox.QuestionLimitLabel.Text;

            this.TestCaseVerify.AreEqual(
                checkBlockMessage,
                expectedBlockMessage,
                displayedBlockMessage,
                "Block message is not correct. Expected: " + expectedBlockMessage + " Displayed: " + displayedBlockMessage);

            this.TestCaseVerify.IsFalse(
                checkQuestionBoxHidden,
                aiAssistantPage.QueryBox.QuestionTextbox.Displayed,
                "Question input box should be hidden.");

            this.TestCaseVerify.IsFalse(
                checkJurisButtonHidden,
                aiAssistantPage.Toolbar.JurisdictionButton.Displayed,
                "Jurisdiction button should be hidden.");

            this.TestCaseVerify.IsFalse(
                checkNewConversationButtonDisabled,
                aiAssistantPage.Toolbar.NewResearchButton.Enabled,
                "New conversation button should be disabled.");

            // Closing land page tab and go to AI-Assisted Research tab on home page
            BrowserPool.CurrentBrowser.CloseTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);
            var aiAssistedResearchTab = precisionHomePage.BrowseTabPanel.SetActiveTab<AiAssistedResearchTabPanel>(PrecisionBrowseTab.AiAssistedResearch);

            this.TestCaseVerify.IsFalse(
                checkHomePageTextboxDisabled,
                aiAssistedResearchTab.QuestionTextbox.Enabled,
                "Textbox field on Home page should be disabled.");

            this.TestCaseVerify.IsFalse(
                checkHomePageSubmitButtonDisabled,
                aiAssistedResearchTab.SubmitButton.Enabled,
                "Submit button on Home page should be disabled.");

            aiAssistantPage = precisionHomePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AssistedResearchLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            // On landing page/tab, expand History panel,click first research entry and wait for response to load 
            aiAssistantPage = aiAssistantPage.ConversationHistory.ExpandButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.ConversationHistory.Conversations.First().ConversationButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsFalse(
                checkQuestionBoxHiddenHistory,
                aiAssistantPage.QueryBox.QuestionTextbox.Displayed,
                "Question input box should be hidden viewing from History.");

            this.TestCaseVerify.IsFalse(
                checkJurisButtonHiddenHistory,
                aiAssistantPage.Toolbar.JurisdictionButton.Displayed,
                "Jurisdiction button should be hidden viewing from History.");

            this.TestCaseVerify.IsFalse(
                checkNewConversationButtonDisabledHistory,
                aiAssistantPage.Toolbar.NewResearchButton.Enabled,
                "New conversation button should be disabled viewing from History.");

            // Set date back to get out of limit block
            aiAssistantPage.UsageDebug.BackDateExpiryButton.Click();
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.UsageDebug.DailyUsageLabel.Text.Equals("0"));
        }

        /// <summary>
        /// Verify CoCounsel dialog and AI-Assisted Research link
        /// Test case: 1869261  User Story: 1868347
        /// 1. Sign in WL Precision with the CoCounsel header access 
        /// 2. Click CoCounsel link from page header
        /// 3. Check: Verify CoCounsel dialog opens
        /// 4. Check: Verify Close button displayed on CoCounsel dialog
        /// 5. Click AI-Assisted Research link on the diolag
        /// 6. Check: Verify clicking research link on dialog takes to landing page on new tab
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamDahlNonAjsCategory)]
        [TestCategory(TeamDahlCategory)]
        [TestCategory("TransitionToSharat")]
        public void AiAssistantCoCounselTest()
        {
            const string CoCounselDialogTitle = "CoCounsel";
            const string WelcomeMessage = "Jump-start your work with AI-Assisted Research";

            string checkCoCounselHeaderLink = "Verify CoCounsel dialog opens";
            string checkCoCounselDialogCloseButton = "Verify Close button displayed on CoCounsel dialog";
            string checkClickingAiResearchLink = "Verify clicking research link on dialog takes to landing page on new tab";

            var coCounselDialog = this.GetHomePage<PrecisionHomePage>().Header.ClickHeaderTab<CoCounselDialog>(EdgeHeaderTabs.CoCounsel);
            this.TestCaseVerify.IsTrue(
                checkCoCounselHeaderLink,
                coCounselDialog.TitleLabel.Text.Equals(CoCounselDialogTitle),
                "CoCounsel dialog should open with title: " + CoCounselDialogTitle);

            this.TestCaseVerify.IsTrue(
                checkCoCounselDialogCloseButton,
                coCounselDialog.CloseButton.Displayed,
                "Close button should display on CoCounsel dialog");

            var aiAssistantPage = coCounselDialog.AiAssistedResearchLink.Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);
            Thread.Sleep(3000);

            this.TestCaseVerify.IsTrue(
                checkClickingAiResearchLink,
                aiAssistantPage.Chat.LandingPageLabel.Text.Contains(WelcomeMessage),
                "Clicking research link should take to landing page on new tab");
        }

        /// <summary>
        /// Verify trialist user's daily limit is set to 100
        /// Test cases: 1871777 1905787  Bug: 1893571 User Story: 1869389
        /// 1. Sign in WL Precision with Trialist access 
        /// 2. Click on AI-Assisted Research card from Key features
        /// 3. Check: Verify daily limit for trial user is set to 100
        /// 4. Ask question: What is the definition of substantial evidence?
        /// 5. Check: Verify daily usage increases by 1 after running a search
        /// 6. Click to view first document and click Return to Report button
        /// 7. Reload page (shortcut to reload from history and make sure usage data updated)
        /// 8. Check: Verify daily usage does not increase after viewing a document or reloading page
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryUsageLimit)]
        [TestCategory(TeamDahlNonAjsCategory)]
        [TestCategory(TeamDahlCategory)]
        [TestProperty("Trialist", "DailyLimit")]
        [TestCategory("TransitionToSharat")]
        public void AiAssistantTrialistDailyLimitTest()
        {
            const string expectedDailyLimit = "100";
            const string Question = "What is the definition of substantial evidence?";
            const string AssistedResearchLabel = "AI-Assisted Research";

            string checkDailyLimitDisplayed = "Verify daily limit for trial user is set to 100";
            string checkDailyUsageDisplayedSearch = "Verify daily usage increases by 1 after running a search";
            string checkDailyUsageDisplayedViewDoc = "Verify daily usage does not increase after viewing a document or reloading page";

            var aiAssistantPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(AssistedResearchLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            Thread.Sleep(2000);
            aiAssistantPage.UsageDebug.BackDateExpiryButton.Click();
            BrowserPool.CurrentBrowser.Refresh();
            Thread.Sleep(2000);
            var displayedDailyLimit = aiAssistantPage.UsageDebug.DailyLimitLabel.Text;

            this.TestCaseVerify.AreEqual(
                checkDailyLimitDisplayed,
                expectedDailyLimit,
                displayedDailyLimit,
                "Daily limit displayed is not correct. Expected: " + expectedDailyLimit + " Displayed: " + displayedDailyLimit);

            // Ask a question 
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);
            var displayedDailyUsage = aiAssistantPage.UsageDebug.DailyUsageLabel.Text;

            this.TestCaseVerify.IsTrue(
                checkDailyUsageDisplayedSearch,
                displayedDailyUsage.Equals("1"),
                "Daily usage displayed is not correct. Expected: 1 Displayed: " + displayedDailyUsage);

            // Click to view first document and click Return to Report button
            var documentPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.CasesItems.First().DocumentTitleLink.Click<EdgeCommonDocumentPage>();
            Thread.Sleep(2000);
            aiAssistantPage = documentPage.FixedHeader.ClickReturnToListButton<AiAssistedResearchPage>();
            aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.WaitDisplayed(2000);

            // Reload page to make sure usage data is updated
            BrowserPool.CurrentBrowser.Refresh();
            Thread.Sleep(2000);
            displayedDailyUsage = aiAssistantPage.UsageDebug.DailyUsageLabel.Text;

            this.TestCaseVerify.IsTrue(
                checkDailyUsageDisplayedViewDoc,
                displayedDailyUsage.Equals("1"),
                "Daily usage displayed is not correct. Expected: 1 Displayed: " + displayedDailyUsage);

            aiAssistantPage.UsageDebug.BackDateExpiryButton.Click();
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.UsageDebug.DailyUsageLabel.Text.Equals("0"));
        }

        /// <summary>
        /// Verify usage limit message is displayed as expected for trialist user
        /// Test case: 1871777  User Story: 1870823
        /// 1. Sign in WL Precision with Trialist access and set daily limit to 1
        /// 2. Click on AI-Assisted Research card from Key features
        /// 3. Ask question: What is the definition of substantial evidence?
        /// 4. Check: Verify block message displayed correctly for trial user
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryUsageLimit)]
        [TestCategory(TeamDahlNonAjsCategory)]
        [TestCategory(TeamDahlCategory)]
        [TestProperty("Trialist", "BlockMessage")]
        [TestProperty("ResearchDailyLimit", "1")]
        [TestCategory("TransitionToSharat")]
        public void AiAssistantTrialistBlockMessageTest()
        {
            const string Question = "What is the definition of substantial evidence?";
            const string AssistedResearchLabel = "AI-Assisted Research";

            string checkDisplayedBlockMessage = "Verify block message displayed correctly for trial user";

            var aiAssistantPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(AssistedResearchLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            // In case usage limit already reached, set date back and refresh page
            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchPage>();
            aiAssistantPage.UsageDebug.BackDateExpiryButton.Click();
            BrowserPool.CurrentBrowser.Refresh();

            // Ask a question 
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            var expectedBlockMessage = "You've used all " + aiAssistantPage.UsageDebug.DailyLimitLabel.Text + " AI-Assisted Research questions available during your trial period.";
            var displayedBlockMessage = aiAssistantPage.QueryBox.QuestionLimitLabel.Text;

            this.TestCaseVerify.AreEqual(
                checkDisplayedBlockMessage,
                expectedBlockMessage,
                displayedBlockMessage,
                "Block message is not correct. Expected: " + expectedBlockMessage + " Displayed: " + displayedBlockMessage);

            // Set date back to get out of abusive limit block
            aiAssistantPage.UsageDebug.BackDateExpiryButton.Click();
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.UsageDebug.DailyUsageLabel.Text.Equals("0"));
        }

        /// <summary>
        /// Verify message displayed as expected when there are 5 remaining questions
        /// Test case: 1872982  User Story: 1867347
        /// 1. Sign in WL Precision setting daily limit of questions to 6 and
        ///    IAC to display warning on 5 or less questions remaining
        /// 2. Click on AI-Assisted Research card from Key features
        /// 3. Ask question: What is the definition of substantial evidence?
        /// 4. Check: Verify warning message displayed correctly when 5 questions remaining
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryUsageLimit)]
        [TestCategory(TeamDahlNonAjsCategory)]
        [TestCategory(TeamDahlCategory)]
        [TestProperty("ResearchDailyLimit", "6")]
        [TestCategory("TransitionToSharat")]
        public void AiAssistant5QuestionWarningMessageTest()
        {
            const string Question = "What is the definition of substantial evidence?";
            const string ExpectedWarningMessage = "You have 5 out of 6 questions remaining today. This limit resets nightly at 12:00 a.m. Central time.";
            const string AssistantResearchLabel = "AI-Assisted Research";

            string checkDisplayedWarningMessage = "Verify warning message displayed correctly when 5 questions remaining";

            var aiAssistantPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(AssistantResearchLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            // In case usage limit already reached, set date back and refresh page
            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchPage>();
            aiAssistantPage.UsageDebug.BackDateExpiryButton.Click();
            BrowserPool.CurrentBrowser.Refresh();

            // Ask a question 
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            var displayedWarningkMessage = aiAssistantPage.QueryBox.RemainingQuestionsLabel.Text;

            this.TestCaseVerify.AreEqual(
                checkDisplayedWarningMessage,
                ExpectedWarningMessage,
                displayedWarningkMessage,
                "Warning message is not correct. Expected: " + ExpectedWarningMessage + " Displayed: " + displayedWarningkMessage);

            // Set date back to get out of limit block
            aiAssistantPage.UsageDebug.BackDateExpiryButton.Click();
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.UsageDebug.DailyUsageLabel.Text.Equals("0"));
        }

        /// <summary>
        /// Verify message displayed as expected and search query properly truncated when query is over 2000 characters
        /// Test case: 1874971  Bug (hotfix): 1872940
        /// 1. Sign in WL Precision and on home page enter query with more than 2000 characters
        /// 2. Check: Verify warning message displayed correctly on home page
        /// 3. Check: Verify truncated query has 2000 characters on home page
        /// 4. Go to AI-Assisted Research landing page and enter same query
        /// 5. Check: Verify warning message displayed correctly on landing page
        /// 6. Check: Verify truncated query has 2000 characters on landing page
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryUsageLimit)]
        [TestCategory(TeamDahlNonAjsCategory)]
        [TestCategory(TeamDahlCategory)]
        [TestCategory("TransitionToSharat")]
        [DeploymentItem(@"Resources/TestData/Aalp")]
        public void AiAssistantQuery2000LimitTest()
        {
            string Question = File.ReadAllText(Environment.CurrentDirectory + @"\QueryOver2000Characters.txt");
            const string ExpectedWarningMessage = "Your question exceeds the 2000 character limit, and all text after that limit has been removed.";
            const string AssistedResearchLabel = "AI-Assisted Research";

            string checkWarningMessageHomePage = "Verify warning message displayed correctly on home page";
            string checkTruncatedQueryHomePage = "Verify truncated query has 2000 characters on home page";
            string checkWarningMessageLandingPage = "Verify warning message displayed correctly on landing page";
            string checkTruncatedQueryLandingPage = "Verify truncated query has 2000 characters on landing page";

            var precisionHomePage = this.GetHomePage<PrecisionHomePage>();
            var aiAssistedResearchTab = precisionHomePage.BrowseTabPanel.SetActiveTab<AiAssistedResearchTabPanel>(PrecisionBrowseTab.AiAssistedResearch);
            aiAssistedResearchTab.QuestionTextbox.SetText(Question);

            var displayedWarningMessage = aiAssistedResearchTab.QueryLimitWarningLabel.Text;
            this.TestCaseVerify.AreEqual(
                checkWarningMessageHomePage,
                ExpectedWarningMessage,
                displayedWarningMessage,
                "Expected warning: " + ExpectedWarningMessage + " Displayed warning: " + displayedWarningMessage);

            var truncatedQuery = aiAssistedResearchTab.QuestionTextbox.Text;
            this.TestCaseVerify.IsTrue(
            checkTruncatedQueryHomePage,
            truncatedQuery.Length == 2000,
            "Expected query length is 2000 characters. Displayed: " + truncatedQuery.Length);

            var aiAssistantPage = precisionHomePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AssistedResearchLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            displayedWarningMessage = aiAssistantPage.QueryBox.QueryLimitWarningLabel.Text;
            this.TestCaseVerify.AreEqual(
                checkWarningMessageLandingPage,
                ExpectedWarningMessage,
                displayedWarningMessage,
                "Expected warning: " + ExpectedWarningMessage + " Displayed warning: " + displayedWarningMessage);

            truncatedQuery = aiAssistantPage.QueryBox.QuestionTextbox.Text;
            this.TestCaseVerify.IsTrue(
            checkTruncatedQueryLandingPage,
            truncatedQuery.Length == 2000,
            "Expected query length is 2000 characters. Displayed: " + truncatedQuery.Length);
        }

        /// <summary>
        /// Verify warning popup displayed for OOP user and user can proceed to run search
        /// Test case: 1874970  User Story: 1871780
        /// 1. Sign in WL Precision with OOP reg key with WARN FLAG=X, currently with IACs:
        ///    IAC-AI-ASSISTANT-BLOCK-AND-WARN-OOP,IAC-AI-ASSISTANT-BLOCK-AND-WARN
        /// 2. Click on AI-Assisted Research card from Key features
        /// 3. Ask question: What is the definition of substantial evidence?
        /// 4. Check: Verify Out Of Plan warning dialog displayed correctly
        /// 5. Click Get answer button on the dialog
        /// 6. Check: Verify clicking Get answer button proceeds to run search
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryOOP)]
        [TestCategory(TeamDahlNonAjsCategory)]
        [TestCategory(TeamDahlCategory)]
        [TestProperty("OutOfPlan", "Warning")]
        [TestCategory("TransitionToSharat")]
        public void AiAssistantOutOfPlanWarningTest()
        {
            const string Question = "What is the definition of substantial evidence?";
            const string ExpectedWarningMessage = "Asking questions is out of plan";
            const string AssistedResearchLabel = "AI-Assisted Research";

            string checkOOPDialog = "Verify Out Of Plan warning dialog displayed correctly";
            string checkGetAnswerButton = "Verify clicking Get answer button proceeds to run search";

            var aiAssistantPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(AssistedResearchLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            // Ask a question 
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            var oopDialog = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiResearchOutOfPlanDialog>();

            var displayedDialogTitle = oopDialog.TitleLabel.Text;

            this.TestCaseVerify.AreEqual(
                checkOOPDialog,
                ExpectedWarningMessage,
                displayedDialogTitle,
                "OOP warning dialog is not correct. Expected: " + ExpectedWarningMessage + " Displayed: " + displayedDialogTitle);

            aiAssistantPage = oopDialog.GetAnswerButton.Click<AiAssistedResearchPage>();
            this.TestCaseVerify.IsTrue(
                checkGetAnswerButton,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed,
                "Search does not run when clicking Get answer button from OOP dialog");
        }

        /// <summary>
        /// Verify blocking popup displayed for OOP user and user cannot proceed to run search
        /// Test case: 1874970  User Story: 1871780
        /// 1. Sign in WL Precision with OOP reg key with WARN FLAG=B, currently with IACs:
        ///    IAC-AI-ASSISTANT-BLOCK-AND-WARN-NOTAUTH,IAC-AI-ASSISTANT-BLOCK-AND-WARN
        /// 2. Click on AI-Assisted Research card from Key features
        /// 3. Ask question: What is the definition of substantial evidence?
        /// 4. Check: Verify Out Of Plan blocking dialog displayed correctly
        /// 5. Click Ok button on the dialog
        /// 6. Check: Verify clicking Ok button does not run search
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryOOP)]
        [TestCategory(TeamDahlNonAjsCategory)]
        [TestCategory(TeamDahlCategory)]
        [TestProperty("OutOfPlan", "Blocking")]
        [TestCategory("TransitionToSharat")]
        public void AiAssistantOutOfPlanBlockingTest()
        {
            const string Question = "What is the definition of substantial evidence?";
            const string ExpectedBlockingMessage = "Asking questions is not authorized";
            const string WelcomeMessage = "Jump-start your work with AI-Assisted Research";
            const string AssistantResearchLabel = "AI-Assisted Research";

            string checkOOPDialog = "Verify Out Of Plan blocking dialog displayed correctly";
            string checkGetAnswerButton = "Verify clicking Ok button does not run search";

            var aiAssistantPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(AssistantResearchLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            // Ask a question 
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            var oopDialog = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiResearchOutOfPlanBlockDialog>();

            var displayedDialogTitle = oopDialog.TitleLabel.Text;

            this.TestCaseVerify.AreEqual(
                checkOOPDialog,
                ExpectedBlockingMessage,
                displayedDialogTitle,
                "OOP blocking dialog is not correct. Expected: " + ExpectedBlockingMessage + " Displayed: " + displayedDialogTitle);

            aiAssistantPage = oopDialog.OkButton.Click<AiAssistedResearchPage>();
            this.TestCaseVerify.IsTrue(
                checkGetAnswerButton,
                aiAssistantPage.Chat.LandingPageLabel.Text.Contains(WelcomeMessage),
                "Search should not run when clicking Ok button from OOP dialog");
        }

        /// <summary>
        /// Verify personalization features are disabled when signing in with BlindLog FAC granted
        /// Test case: 1877293  User Story: 1876077
        /// 1. Sign in with BlindLog FAC granted
        /// 2. Check: Verify History not displayed on page header for BlindLog
        /// 3. Check: Verify Notifications not displayed on page header for BlindLog
        /// 4. Check: Verify View all favorites under My links not displayed for BlindLog
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamDahlNonAjsCategory)]
        [TestCategory(TeamDahlCategory)]
        [TestProperty("BlindLog", "GrantBlindLog")]
        [TestCategory("TransitionToSharat")]
        public void AiAssistantBlindLogTest()
        {
            CheckUserLogsAreBlocked("BlindLog");
        }
 
        /// <summary>
        /// Verify personalization features are disabled when signing in with EncryptLog FAC granted
        /// Test case: 1877293  User Story: 1876077
        /// 1. Sign in with EncryptLog FAC granted
        /// 2. Check: Verify History not displayed on page header for EncryptLog
        /// 3. Check: Verify Notifications not displayed on page header for EncryptLog
        /// 4. Check: Verify View all favorites under My links not displayed for EncryptLog
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamDahlNonAjsCategory)]
        [TestCategory(TeamDahlCategory)]
        [TestProperty("EncryptLog", "GrantEncryptLog")]
        [TestCategory("TransitionToSharat")]
        public void AiAssistantEncryptLogTest()
        {
            CheckUserLogsAreBlocked("EncryptLog");
        }

        private void CheckUserLogsAreBlocked(string feature)
        {
            string checkHistoryForFeature = "Verify History not displayed on page header for " + feature;
            string checkNotificationsForFeature = "Verify Notifications not displayed on page header for " + feature;
            //string checkFavoritesForFeature = "Verify View all favorites under My links not displayed for " + feature;

            var homePage = this.GetHomePage<PrecisionHomePage>();

            this.TestCaseVerify.IsFalse(
                checkHistoryForFeature,
                homePage.Header.IsTextLinkDisplayed("History"),
                "History should not display on page header when granting FAC: " + feature);

            this.TestCaseVerify.IsFalse(
                checkNotificationsForFeature,
                homePage.Header.IsTextLinkDisplayed("Notifications"),
                "Notifications should not display on page header when granting FAC: " + feature);

            // This will be addressed in future story
            //var favoritesDialog = homePage.Header.ClickHeaderTab<EdgeFavoritesDialog>(EdgeHeaderTabs.MyLinks);
            //this.TestCaseVerify.IsFalse(
            //    checkFavoritesForFeature,
            //    favoritesDialog.FavoritesViewAllLink.Displayed ,
            //    "View all favorites under My links on page header should not display when granting FAC: " + feature);
        }

        /// <summary>
        /// Test verifies that when the usage limit is set to 1 additional follow-ups are not allowed.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryUsageLimit)]
        [TestCategory(TeamDahlNonAjsCategory)]
        [TestCategory(TeamDahlCategory)]
        [TestProperty("AITreatiseSearchDailyLimit", "1")]
        [TestCategory("TransitionToSharat")]
        public void AiAssistantTreatiseUsageLimitTest()
        {
            const string TypeAheadQueryForCategoryPage = "Civil Procedure Before Trial (The Rutter Group, California Practice Guide)";
            const string Question = "Can parties orally stipulate to change the date of a deposition?";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            var typeahead = homePage.Header.EnterSearchQuery<TrdTypeAheadDialog>(TypeAheadQueryForCategoryPage);
            var categoryPage = typeahead.OtherComponent.ClickOnSuggestionByIndex<EdgeContentTypeSearchResultPage>(NewTypeAheadCategories.ContentPages, 0);
            var aiAssistantPage = categoryPage.Toolbar.ClickToolbarElement<AiAssistedResearchPage>(EdgeToolbarElements.SearchAndSummarizeRutter);

            //Navigate to the AAR tab
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            // In case usage limit already reached, set date back and refresh page
            aiAssistantPage.UsageDebug.BackDateExpiryButton.Click();
            BrowserPool.CurrentBrowser.Refresh();

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            var expectedBlockMessage = "You've reached your daily limit of " + aiAssistantPage.UsageDebug.DailyLimitLabel.Text
                                                                             + " AI-Assisted Research questions. This limit resets every night at 12:00 a.m. Central time."
                                                                             + " You can still access your prior AI-Assisted Research via your History.";
            var displayedBlockMessage = aiAssistantPage.QueryBox.QuestionLimitLabel.Text;

            this.TestCaseVerify.IsTrue(
                "Expected block message displayed for AI Treatise",
                expectedBlockMessage.Equals(displayedBlockMessage),
                $"Expected block message -- {expectedBlockMessage} -- not displayed. Message displayed is: {displayedBlockMessage}");
        }
        
        protected override void InitializeRoutingPageSettings()
        {
            if ((this.TestContext.Properties["AITreatiseSearchDailyLimit"] != null) &&
                (this.TestContext.Properties["AITreatiseSearchDailyLimit"].Equals("1")))
            {
                this.Settings.AppendValues(
                    EnvironmentConstants.AITreatiseSearchDailyLimit,
                    SettingUpdateOption.Append,
                    "1");

                this.Settings.AppendValues(
                    EnvironmentConstants.FeatureAccessControlsOn,
                    SettingUpdateOption.Append,
                    FeatureAccessControl.AiResearchTreatise);

                this.Settings.AppendValues(
                    EnvironmentConstants.InfrastructureAccessControlsOn,
                    SettingUpdateOption.Append,
                    "IAC-AI-ASSISTANT-USAGE-LIMITS-DEBUG",
                    "IAC-AI-ABUSIVE-USE-MESSAGE-V2");
            }

            if (this.TestContext.Properties["Trialist"] != null
                && (this.TestContext.Properties["Trialist"].Equals("BlockMessage") ||
                this.TestContext.Properties["Trialist"].Equals("DailyLimit")))
            {
                this.Settings.AppendValues(
                    EnvironmentConstants.InfrastructureAccessControlsOn,
                    SettingUpdateOption.Append,
                    "IAC-AI-ASSISTANT-USAGE-LIMITS-DEBUG",
                    "IAC-AI-ABUSIVE-USE-SHOW-WARNING");

                this.Settings.AppendValues(
                    EnvironmentConstants.FeatureAccessControlsOn,
                    SettingUpdateOption.Append,
                    FeatureAccessControl.AiResearchTrialist);
            }
            else if (this.TestContext.Properties["OutOfPlan"] != null
                && this.TestContext.Properties["OutOfPlan"].Equals("Warning"))
            {
                this.Settings.AppendValues(
                    EnvironmentConstants.InfrastructureAccessControlsOn,
                    SettingUpdateOption.Append,
                    "IAC-AI-ASSISTANT-BLOCK-AND-WARN-OOP",
                    "IAC-AI-ASSISTANT-BLOCK-AND-WARN");
            }
            else if (this.TestContext.Properties["OutOfPlan"] != null
                && this.TestContext.Properties["OutOfPlan"].Equals("Blocking"))
            {
                this.Settings.AppendValues(
                    EnvironmentConstants.InfrastructureAccessControlsOn,
                    SettingUpdateOption.Append,
                    "IAC-AI-ASSISTANT-BLOCK-AND-WARN-NOTAUTH",
                    "IAC-AI-ASSISTANT-BLOCK-AND-WARN");
            }
            else
            {
                this.Settings.AppendValues(
                    EnvironmentConstants.InfrastructureAccessControlsOn,
                    SettingUpdateOption.Append,
                    "IAC-AI-ASSISTANT-USAGE-LIMITS-DEBUG",
                    "IAC-AI-ABUSIVE-USE-SHOW-WARNING");
            }

            if (this.TestContext.Properties["ResearchDailyLimit"] != null
                && this.TestContext.Properties["ResearchDailyLimit"].Equals("1"))
            {
                this.Settings.AppendValues(
                    EnvironmentConstants.AISearchDailyLimit,
                    SettingUpdateOption.Append, "1");
            }
            else if (this.TestContext.Properties["ResearchDailyLimit"] != null
                && this.TestContext.Properties["ResearchDailyLimit"].Equals("6"))
            {
                this.Settings.AppendValues(
                    EnvironmentConstants.AISearchDailyLimit,
                    SettingUpdateOption.Append, "6");
            }

            if (this.TestContext.Properties["BlindLog"] != null
                && this.TestContext.Properties["BlindLog"].Equals("GrantBlindLog"))
            {
                this.Settings.AppendValues(
                    EnvironmentConstants.FeatureAccessControlsOn,
                    SettingUpdateOption.Append,
                    FeatureAccessControl.BlindLog);
            }
            else if (this.TestContext.Properties["EncryptLog"] != null
                && this.TestContext.Properties["EncryptLog"].Equals("GrantEncryptLog"))
            {
                this.Settings.AppendValues(
                    EnvironmentConstants.FeatureAccessControlsOn,
                    SettingUpdateOption.Append,
                    FeatureAccessControl.EncryptLog);
            }

            this.Settings.AppendValues(
                EnvironmentConstants.FeatureAccessControlsOn,
                SettingUpdateOption.Append,
                FeatureAccessControl.AiResearch);

            this.Settings.AppendValues(
                 EnvironmentConstants.InfrastructureAccessControlsOff,
                 SettingUpdateOption.Append,
                "IAC-AI-COCOUNSEL-ASSISTANT-ACCESS-POINT");

            this.Settings.AppendValues(
                EnvironmentConstants.FeatureAccessControlsOn,
                SettingUpdateOption.Append,
                FeatureAccessControl.CoCounselHeader);
        }
    }
}
