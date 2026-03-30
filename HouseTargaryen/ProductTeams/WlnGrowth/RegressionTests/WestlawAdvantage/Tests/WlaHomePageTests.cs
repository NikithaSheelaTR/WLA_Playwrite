namespace WestlawAdvantage.Tests
{
    using Framework.Common.UI.Products.Shared.Dialogs.Delivery;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.Shared.Pages.Alerts;
    using Framework.Common.UI.Products.WestlawAdvantage.Components.HomePage;
    using Framework.Common.UI.Products.WestlawAdvantage.Dialogs;
    using Framework.Common.UI.Products.WestlawAdvantage.Dialogs.HomePageLeftToolsBar;
    using Framework.Common.UI.Products.WestlawAdvantage.Enums;
    using Framework.Common.UI.Products.WestlawAdvantage.Pages;
    using Framework.Common.UI.Products.WestlawAdvantage.Pages.DeepResearch;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Foldering;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.HomePage;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.CoCounsel;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage.HomePageTabs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.AALP;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.AALP;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
    using Framework.Common.UI.Products.WestLawNext.Enums.Delivery;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Products.WestLawNext.Pages.SearchResult.ContentTypeSearchResultPages;
    using Framework.Common.UI.Raw.EnhancementTests.Utils;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Folders;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Header;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.Extensions;
    using Framework.Core.Utils.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// WestlawAdvantage Home Page tests
    /// </summary>
    [TestClass]
    public class WlaHomePageTests : WlaBaseTest
    {
        protected const string DeepAIResearchTab = "Westlaw Deep AI Research";
        private const string FeatureTestCategoryDeepResearch = "WestlawF1HomePage";
        protected const string DeliveryDateFormat = "MM-dd-yyyy";

        /// <summary>
        /// Test Case 2196147: [Deep AI Research] Content tab settings
        /// 1. Sign in WL Precision with routing (QED has to use site: use2):
        ///    IAC: IAC-AI-WESTLAW-LABS-RAPID-PROTOTYPE,IAC-AI-WESTLAW-GUIDED-RESEARCH
        ///    FAC: AIWestlawGuidedResearch, IndigoPremiumF1
        /// 2. Click Content tab settings icon under global search
        /// 3. Select Precision Research tab and save
        /// 4. Update the page 
        /// 5. Check: Verify Precision Research tab is shown by default
        /// 6. Click Content tab settings icon under global search
        /// 7. Select Deep AI Research tab and save
        /// 4. Update the page 
        /// 5. Check: Verify Deep AI Research tab is shown by default
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void DeepResearchContentTabSettingsTest()
        {
            string checkDefaultTab = "Verify: Selected tab is default";

            var homePage = this.GetHomePage<AdvantageHomePage>();
            PrintSessionIdFromPageSource();
            var defaultTabDialog = homePage.BrowseTabPanel.DefaultTabGearButtonLocator.Click<AdvantageSelectDefaultTabDialog>();
            defaultTabDialog.SelectDefaultTab(AdvantageSelectDefaultTabOptions.PrecisionResearch);
            homePage = defaultTabDialog.SaveButton.Click<AdvantageHomePage>();

            BrowserPool.CurrentBrowser.Refresh();
                      

            this.TestCaseVerify.IsTrue(
                checkDefaultTab,
                homePage.TabPanel.IsActive(AdvantageBrowseTab.PrecisionResearch),
                "Selected tab is NOT default");

            defaultTabDialog = homePage.BrowseTabPanel.DefaultTabGearButtonLocator.Click<AdvantageSelectDefaultTabDialog>();
            defaultTabDialog.SelectDefaultTab(AdvantageSelectDefaultTabOptions.DeepAIResearch);
            homePage = defaultTabDialog.SaveButton.Click<AdvantageHomePage>();

            BrowserPool.CurrentBrowser.Refresh();

            this.TestCaseVerify.IsTrue(
                checkDefaultTab,
                homePage.TabPanel.IsActive(AdvantageBrowseTab.DeepAIResearch),
                "Selected tab is NOT default");
        }
      
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void LeftToolsBarCheckComponentsTest()
        {
            string checkAIDeepResearchButtonComponent = "Verify: AI Deep Research button is displayed in Left toolbar";
            string checkSearchButtonComponent = "Verify: Search button is displayed in Left toolbar";
            string checkTelescopeButtonComponent = "Verify: Telescope button is displayed in Left toolbar";
            string checkNotificationsButtonComponent = "Verify: Notifications button is displayed in Left toolbar";
            string checkCoCunselButtonComponent = "Verify: CoCunsel button is displayed in Left toolbar";
            string checkMyLinksButtonComponent = "Verify: MyLinks button is displayed in Left toolbar";
            string checkBrowseButtonComponent = "Verify: Browse button is displayed in Left toolbar";
            string checkToolsButtonComponent = "Verify: Tools button is displayed in Left toolbar";
            string checkCliendIDButtonComponent = "Verify: CliendID button is displayed in Left toolbar";
            string checkHistoryButtonComponent = "Verify: History button is displayed in Left toolbar";

            var homePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);
            
            this.TestCaseVerify.IsTrue(
                checkAIDeepResearchButtonComponent,
                homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.AIDeepResearch).Displayed,
                "AI Deep Research button is not displayed in Left toolbar");

            this.TestCaseVerify.IsTrue(
                checkSearchButtonComponent,
                homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.Search).Displayed,
                "Search button is not displayed in Left toolbar");

            this.TestCaseVerify.IsTrue(
               checkTelescopeButtonComponent,
               homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.Telescope).Displayed,
               "Telescope button is not displayed in Left toolbar");

            this.TestCaseVerify.IsTrue(
               checkNotificationsButtonComponent,
               homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.Notifications).Displayed,
               "Notifications button is not displayed in Left toolbar");

            this.TestCaseVerify.IsTrue(
               checkCoCunselButtonComponent,
               homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.CoCunsel).Displayed,
               "CoCunsel button is not displayed in Left toolbar");

            this.TestCaseVerify.IsTrue(
               checkMyLinksButtonComponent,
               homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.MyLinks).Displayed,
               "MyLinks button is not displayed in Left toolbar");

            this.TestCaseVerify.IsTrue(
               checkBrowseButtonComponent,
               homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.Browse).Displayed,
               "Browse button is not displayed in Left toolbar");

            this.TestCaseVerify.IsTrue(
               checkToolsButtonComponent,
               homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.Tools).Displayed,
               "Tools button is not displayed in Left toolbar");

            this.TestCaseVerify.IsTrue(
               checkCliendIDButtonComponent,
               homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.CliendID).Displayed,
               "CliendID button is not displayed in Left toolbar");

            this.TestCaseVerify.IsTrue(
               checkHistoryButtonComponent,
               homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.History).Displayed,
               "History button is not displayed in Left toolbar");
        }

        /// <summary>
        /// Test Deep AI Research displays on home page under Tools tab and Key Features.
        /// Task: 2207651
        /// 1. Sign in WL Advantage    
        /// 2. Check: Verfiy Deep AI Research card under Key Features on the home page
        /// 3. Check: Verify welcome message is displayed on landing page
        /// 4. Check: Verfiy Deep AI Research card under Tool tab on the home page
        /// 5. Check: Verify welcome message is displayed on landing page
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void DeepResearchWidgetVisibilityTest()
        {
            const string AiDeepResearchWidget = "AI Deep Research";
            const string KeyFeaturesHeading = "Key Features";
            string checkKeyFeaturesHeading = "Verify that Key Features heading is present";
            string checkDeepAiResearchLinkIsPresent = "Verify that {widgetTitle} link is present in Key Features section";
            string checkDeepAiResearchLinkToolsTab = $"Verify that Ai Deep Research link is present in Tools tab";

            var homePage = this.GetHomePage<AdvantageHomePage>();

            // Key Features section
            var aiDeepResearchLink = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AiDeepResearchWidget);

            this.TestCaseVerify.AreEqual(
                checkKeyFeaturesHeading,
                KeyFeaturesHeading,
                homePage.FeaturesIncludedPanel.HeaderLabel.Text,
                "Key Features header is not correct or not displayed");

            this.TestCaseVerify.IsTrue(
                checkDeepAiResearchLinkIsPresent,
                aiDeepResearchLink.Displayed,
                "{AiDeepResearchWidget} link is not present in section");

            AiDeepResearchPage keyFeaturesDeepResearchPage = aiDeepResearchLink.Click<AiDeepResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(DeepAIResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(DeepAIResearchTab);
            SafeMethodExecutor.WaitUntil(() => keyFeaturesDeepResearchPage.DeepResearchHeader.HeadingLabel.Displayed, 20);

            this.TestCaseVerify.IsTrue(
                "Verify welcome message is displayed on landing page",
                keyFeaturesDeepResearchPage.WelcomeComponent.WelcomeHeaderLabel.Text.Contains(AiDeepResearchWidget),
                "Welcome message is not displayed on landing page");

            BrowserPool.CurrentBrowser.CloseTab(DeepAIResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

            //Tools tab         
            var toolsTabPanel = homePage.TabPanel.SetActiveTab<AdvantageToolsTabPanel>(AdvantageBrowseTab.Tools);
            var toolsTabAiDeepResearchLink = toolsTabPanel.ToolsItems.FirstOrDefault(item => item.HeaderLink.Text == AiDeepResearchWidget);

            this.TestCaseVerify.IsTrue(
                 checkDeepAiResearchLinkToolsTab,
                 toolsTabAiDeepResearchLink.HeaderLink.Displayed,
                 "{AiDeepResearchWidget} link is not present in Tools tab");

            AiDeepResearchPage toolsTabDeepResearchPage = toolsTabAiDeepResearchLink.HeaderLink.Click<AiDeepResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(DeepAIResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(DeepAIResearchTab);
            SafeMethodExecutor.WaitUntil(() => toolsTabDeepResearchPage.DeepResearchHeader.HeadingLabel.Displayed, 20);

            this.TestCaseVerify.IsTrue(
                "Verify welcome message is displayed on landing page",
                toolsTabDeepResearchPage.WelcomeComponent.WelcomeHeaderLabel.Text.Contains(AiDeepResearchWidget),
                "Welcome message is not displayed on landing page");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void HomePageSignOutTest()
        {
            string checkUserLoggedOut = "Verify: User is signed out and login page is displayed";
            const string signedOutText = "You have signed out.";
            string checkReturnToWlAdvantageButton = "Verify: Return to Westlaw Advantage button is displayed";

            var homePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);

            var signOffPage = homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.SignOut).Click<AdvantageSignOffPage>();

            this.TestCaseVerify.AreEqual(
            checkUserLoggedOut,
            signedOutText,
            signOffPage.GetSignOffMessage(),
            "User is not signed out.");

            this.TestCaseVerify.IsTrue(
            checkReturnToWlAdvantageButton,
            signOffPage.ReturnToWlAdvantageButton.Displayed,
            "Return to Westlaw Advantage button is not displayed.");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void HomePageUserIdTest()
        {
            string checkUserIdIsPresentInResentIDs = "Verify: User Id is present in recent ";
            string checkUserIdHasAlreadyexistMessage = "Verify: User Id has already exist message is displayed";
            string checkRecentUserIdButtonWorks = "Verify: Recent user id button works";

            var homePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);

            var clientIdDialog = homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.CliendID).Click<AdvantageClientIdDialog>();

            clientIdDialog.ClearClientIdTextBox();
            clientIdDialog.ClientIdTextBox.SendKeys("WLA TEST");

            this.TestCaseVerify.IsTrue(
            checkUserIdIsPresentInResentIDs,
            clientIdDialog.RecentClientButton.Select(item => item.Text).Any(item => item.Equals("WLA TEST")),
            "User Id is not present in recent");

            var recentButtonText = clientIdDialog.RecentClientButton.First().Text;
          
            clientIdDialog = homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.CliendID).Click<AdvantageClientIdDialog>();

            this.TestCaseVerify.AreEqual(
                 checkRecentUserIdButtonWorks,
                 clientIdDialog.ClientIdTextBox.Text,
                 recentButtonText,
                "Recent user id button doesn't work");

            clientIdDialog.CancelButton.Click();

            clientIdDialog = homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.CliendID).Click<AdvantageClientIdDialog>();
            clientIdDialog.AddNewClientOrCancelIdButton.Click<AdvantageClientIdDialog>().AddNewClientTextBox.SendKeys("WLA TEST");
            clientIdDialog.SaveNewClientIdButton.Click();

            this.TestCaseVerify.AreEqual(
                checkUserIdHasAlreadyexistMessage,
                "Client ID already exists",
                clientIdDialog.ClientAlreadyExistMessag.Text,
                "User Id has already exist message is not displayed");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void HomePageExpandCollapseTest()
        {
            string checkLeftTollBarIsExpanded = "Verify: Logo text is displayed";
            string checkLeftTollBarIsCollapsed = "Verify: Logo text is not displayed";

            var homePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);

            var a = homePage.SearchTabPanel.SetActiveTab<AIDeepResearchTabPanel>(AdvantageSearchTabs.AIDeepResearch);

            this.TestCaseVerify.IsTrue(
                checkLeftTollBarIsExpanded,
                homePage.AdvantageLeftToolbar.LogoTextLabel.Displayed,
                "Logo text is not displayed");

            homePage.AdvantageLeftToolbar.ExpandButtonCollapcedButton.Click();

            this.TestCaseVerify.IsFalse(
                checkLeftTollBarIsCollapsed,
                homePage.AdvantageLeftToolbar.LogoTextLabel.Displayed,
                "Logo text is displayed");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void DeepResearchWidgetVisibilityAARNotShownTest()
        {
            const string AiDeepResearchWidget = "AI Deep Research";
            const string AiAssistedResearchWidget = "AI-Assisted Research";
            string checkDeepAiResearchLinkToolsTab = $"Verify that Ai Deep Research link is present in Tools tab";
            string checkAiAssistedResearchNotPresent = "Verify that AI-Assisted Research link is NOT present in Tools tab";
            string checkAiAssistedResearchNotPresentInKeyFeatures = "Verify that AI-Assisted Research link is NOT present in Key Features section";

            var homePage = this.GetHomePage<AdvantageHomePage>();

            var aiDeepResearchLink = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AiDeepResearchWidget);
            var aiAssistedResearchLink = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AiAssistedResearchWidget);

            this.TestCaseVerify.IsTrue(
                checkAiAssistedResearchNotPresentInKeyFeatures,
                aiAssistedResearchLink == null || !aiAssistedResearchLink.Displayed,
                "AI-Assisted Research link IS present in Key Features section, but it should NOT be.");

            var toolsTabPanel = homePage.TabPanel.SetActiveTab<AdvantageToolsTabPanel>(AdvantageBrowseTab.Tools);

            var toolsTabAiDeepResearchLink = toolsTabPanel.ToolsItems.FirstOrDefault(item => item.HeaderLink.Text == AiDeepResearchWidget);

            this.TestCaseVerify.IsTrue(
                 checkDeepAiResearchLinkToolsTab,
                 toolsTabAiDeepResearchLink.HeaderLink.Displayed,
                 "{AiDeepResearchWidget} link is not present in Tools tab");

            var aiAssistedResearchPresent = toolsTabPanel.ToolsItems.Any(item => item.HeaderLink.Text == AiAssistedResearchWidget);

            this.TestCaseVerify.IsFalse(
                checkAiAssistedResearchNotPresent,
                aiAssistedResearchPresent,
                "AI-Assisted Research link IS present in Tools tab, but it should NOT be.");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void HomePageToolsAARNotShownTest()
        {
            const string ExpectedTextNotPresent = "AI-Assisted Research";
            string checkToolsDialogAARNorPresent = "Verify: AI-Assisted Research link IS Not present in Tools Dialog";

            var homePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);

            var toolsDialog = homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.Tools).Click<AdvantageToolsDialog>();

            this.TestCaseVerify.IsFalse(
            checkToolsDialogAARNorPresent,
            toolsDialog.ToolsButton.Select(item => item.Text).Any(item => item.Contains(ExpectedTextNotPresent)),
            "AI-Assisted Research is present in Tools Dialog");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void HomePageLeftToolBarNotificationsTest()
        {
            const string AlertsPageLabel = "Alerts";
            string checkAlertsPageText = "Verify: Alerts link is opened and Alerts label is visible";
            const string PreferencesPopupLabel = "Preferences";
            string checkPreferencesPopUpText = "Verify: Preferences link is opened and label is visible";

            var homePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);

            var notifications = homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.Notifications).Click<AdvantageNotificationsDialog>();
            var alertsContentPage = notifications.AlertsLink.Click<AlertCenterPage>();

            this.TestCaseVerify.IsTrue(
                checkAlertsPageText,
                alertsContentPage.Title.Contains(AlertsPageLabel),
                "Alerts label is not visible. Alerts link is not opened");

            BrowserPool.CurrentBrowser.GoBack();

            notifications = homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.Notifications).Click<AdvantageNotificationsDialog>();
            SafeMethodExecutor.WaitUntil(() => notifications.PreferencesLink.Displayed);
            var preferencesDilog = notifications.PreferencesLink.Click<EdgePreferencesDialog>();
                       
            this.TestCaseVerify.AreEqual(
                checkPreferencesPopUpText,
                PreferencesPopupLabel,
                preferencesDilog.HeaderTitleLabel.Text,
                "Preferences label is not visible. Preferences link is not opened");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void HomePageAIDeepResearchHistoryTest()
        {
            const string ExpectedQuestionPresent = "What are the requirements of the functional employee doctrine?";
            string checkHistoryDialogQuestionPresent = "Verify: Expected Question Present in History Dialog";
            string checkAllHistoryPageQuestionPresent = "Verify: Expected Question Present in All History Page";

            var homePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);

            var aideepresearchPanel = homePage.SearchTabPanel.SetActiveTab<AIDeepResearchTabPanel>(AdvantageSearchTabs.AIDeepResearch);
            aideepresearchPanel.QuestionTextArea.SendKeys(ExpectedQuestionPresent);
            var deepResearchPage =  aideepresearchPanel.SendButton.Click<AiDeepResearchPage>();

            BrowserPool.CurrentBrowser.CreateTab(DeepAIResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(DeepAIResearchTab);

            SafeMethodExecutor.WaitUntil(() => !deepResearchPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed);

            BrowserPool.CurrentBrowser.CloseTab(DeepAIResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);
            BrowserPool.CurrentBrowser.Refresh();

            SafeMethodExecutor.ExecuteUntil(() => homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.History).Displayed);
            var historyDialog = homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.History).Click<AdvantageHistoryDialog>();
            SafeMethodExecutor.ExecuteUntil(() => historyDialog.HistoryLabel.Displayed);

            this.TestCaseVerify.IsTrue(
            checkHistoryDialogQuestionPresent,
            historyDialog.HistoryButton.Select(item => item.Text).Any(item => item.Contains(ExpectedQuestionPresent)),
            "Expected Question is not Present in History Dialog");
            var allHistoryPage = historyDialog.OpenFullHistoryButton.Click<EdgeCommonHistoryPage>();

            BrowserPool.CurrentBrowser.CreateTab("All History");
            BrowserPool.CurrentBrowser.ActivateTab("All History");
            SafeMethodExecutor.ExecuteUntil(() => allHistoryPage.HistoryTable.GetGridItems().Equals("All History"));

            this.TestCaseVerify.IsTrue(
                checkAllHistoryPageQuestionPresent,
                allHistoryPage.HistoryTable.GetGridItems().First().Title.Contains(ExpectedQuestionPresent),
                "Expected Keyword & Boolean Search Question is not Present in All History Page");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void HomePageKeywordBooleanSearchHistoryTest()
        {
            var homePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);
          
            const string AllHistoryExpectedKeywordQuestion = "Is alcoholism a disability under NY law, and can you be terminated because of it?";
            string checkKeywordHistoryDialogQuestionPresent = "Verify: Expected Keyword & Boolean Search Question Present in History Dialog";
            string checkKeywordAllHistoryPageQuestionPresent = "Verify: Expected Keyword & Boolean Search Question Present in All History Page";

            var keywordSearchPanel = homePage.SearchTabPanel.SetActiveTab<KeywordAndBooleanSearchTabPanel>(AdvantageSearchTabs.KeywordAndBooleanSearch);
            keywordSearchPanel.QueryTextArea.SendKeys(AllHistoryExpectedKeywordQuestion);
            keywordSearchPanel.SearchButton.Click();

            BrowserPool.CurrentBrowser.CreateTab("Keyword Boolean Search");
            BrowserPool.CurrentBrowser.ActivateTab("Keyword Boolean Search");
            SafeMethodExecutor.ExecuteUntil(() => keywordSearchPanel.KeywordBooleanSearchHeader.Displayed);

            BrowserPool.CurrentBrowser.GoBack();

            SafeMethodExecutor.ExecuteUntil(() => homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.History).Displayed);
            var keywordHistoryDialog = homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.History).Click<AdvantageHistoryDialog>();
            SafeMethodExecutor.ExecuteUntil(() => keywordHistoryDialog.HistoryLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkKeywordHistoryDialogQuestionPresent,
                keywordHistoryDialog.HistoryButton.Select(item => item.Text).Any(item => item.Contains(AllHistoryExpectedKeywordQuestion)),
                "Expected Keyword & Boolean Search Question is not Present in History Dialog");

            var keywordAllHistoryPage = keywordHistoryDialog.OpenFullHistoryButton.Click<EdgeCommonHistoryPage>();
            BrowserPool.CurrentBrowser.CreateTab("All History");
            BrowserPool.CurrentBrowser.ActivateTab("All History");
            SafeMethodExecutor.ExecuteUntil(() => keywordAllHistoryPage.HistoryTable.GetGridItems().Equals("All History"));

            this.TestCaseVerify.IsTrue(
                checkKeywordAllHistoryPageQuestionPresent,
                keywordAllHistoryPage.HistoryTable.GetGridItems().First().Title.Contains(AllHistoryExpectedKeywordQuestion)
,                   "Expected Keyword & Boolean Search Question is not Present in All History Page");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void HomePageLeftToolBarFoldersTest()
        {
            string checkUserResearchTitleLable = "Verify: Folders link is opened and Folders label is visible";

            string loginUserName = "Regression's Research";

            var homePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);

            var folders = homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.Folders).Click<AdvantageFoldersDialog>();
            SafeMethodExecutor.WaitUntil(() => folders.OpenFoldersLink.Displayed);
            folders.ClickOpenFoldersLink();
            SafeMethodExecutor.WaitUntil(() => folders.FoldersPageTitleHeader.Displayed);

            this.TestCaseVerify.IsTrue(
                checkUserResearchTitleLable,
                 folders.FoldersPageTitleHeader.Text.Contains(loginUserName),
                 $"User Research Folder did not open. Actual folder title: '{folders.FoldersPageTitleHeader.Text}' , Expected user name: '{loginUserName}'");

            BrowserPool.CurrentBrowser.GoBack();

            folders = homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.Folders).Click<AdvantageFoldersDialog>();
            folders.RootFolderLink.Click();
            folders.ViewThisFolderButton.Click();
            SafeMethodExecutor.WaitUntil(() => folders.FoldersPageTitleHeader.Displayed);

            this.TestCaseVerify.IsTrue(
                checkUserResearchTitleLable,
                 folders.FoldersPageTitleHeader.Text.Contains(loginUserName),
                 $"User Research Folder did not open. Actual folder title: '{folders.FoldersPageTitleHeader.Text}' , Expected user name: '{loginUserName}'");
      
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void HomePageLeftToolBarBrowseTest()
        {
            const string ContentTypesLabel = "Content types";
            const string FederalMaterialsLabel = "Federal materials";
            const string StateMaterialsLabel = "State materials";
            const string PracticeAreasLabel = "Practice areas";
            string checkContentTypesDialogLabelText = "Verify: Content Types dialog is opened";
            string checkCasesPageLoaded = "Verify: Cases page is loaded after clicking 'Cases' link";
            string checkFederalMaterialsDialogLabelText = "Verify: Federal materials dialog is opened";
            string checkFederalCasesPageLoaded = "Verify: Federal cases page is loaded after clicking 'Federal Cases' link";
            string checkStateMaterialsDialogLabelText = "Verify: State materials dialog is opened";
            string checkStatePageLoaded = "Verify: California state page is loaded after clicking 'California' link";
            string checkPracticeAreasDialogLabelText = "Verify: Practice Areas dialog is opened";
            string checkPracticeAreasPageLoaded = "Verify: Bankruptcy page is loaded after clicking 'Bankruptcy' link";

            var homePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);

            var browse = homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.Browse).Click<AdvantageBrowseDialog>();

            var contentTypesLink = browse.SelectBrowseCategory<AdvantageBrowseDialog>("Content types");

            this.TestCaseVerify.IsTrue(
                checkContentTypesDialogLabelText,
                browse.ContentTypePanelHeaderLabel.Text.Equals(ContentTypesLabel),
                "Content Types label is not visible. Link is not opened");

            var casesPage = browse.ClickBrowseCategoryContentTypeLink<AdvantageBrowseDialog>("Cases");

            this.TestCaseVerify.IsTrue(
                checkCasesPageLoaded,
                browse.BrowseOpenedPageLabel.Text.Equals("Cases"),
                "'Cases' page did not load as expected after clicking the link.");

            BrowserPool.CurrentBrowser.GoBack();

            browse = homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.Browse).Click<AdvantageBrowseDialog>();

            var federalMaterialsLink = browse.SelectBrowseCategory<AdvantageBrowseDialog>("Federal materials");

            this.TestCaseVerify.IsTrue(
                checkFederalMaterialsDialogLabelText,
                browse.FederalMaterialsPanelHeaderLabel.Text.Equals(FederalMaterialsLabel),
                "Federal Materials label is not visible.");

            var federalCasesesPage = browse.ClickBrowseCategoryContentTypeLink<AdvantageBrowseDialog>("Federal Cases");

            this.TestCaseVerify.IsTrue(
                checkFederalCasesPageLoaded,
                browse.BrowseOpenedPageLabel.Text.Equals("Federal Cases"),
                "'Federal Cases' page did not load as expected after clicking the link.");

            BrowserPool.CurrentBrowser.GoBack();

            browse = homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.Browse).Click<AdvantageBrowseDialog>();

            var stateMaterialsLink = browse.SelectBrowseCategory<AdvantageBrowseDialog>("State materials");

            this.TestCaseVerify.IsTrue(
                checkStateMaterialsDialogLabelText,
                browse.StateMaterialsPanelHeaderLabel.Text.Equals(StateMaterialsLabel),
                "Sate Materials label is not visible.");

            var statePage = browse.ClickBrowseCategoryContentTypeLink<AdvantageBrowseDialog>("California");

            this.TestCaseVerify.IsTrue(
                checkStatePageLoaded,
                browse.BrowseOpenedPageLabel.Text.Equals("California"),
                "'California' page did not load as expected after clicking the link.");

            BrowserPool.CurrentBrowser.GoBack();

            browse = homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.Browse).Click<AdvantageBrowseDialog>();

            var practiceAreasLink = browse.SelectBrowseCategory<AdvantageBrowseDialog>("Practice areas");

            this.TestCaseVerify.IsTrue(
                checkPracticeAreasDialogLabelText,
                browse.PracticeAreasPanelHeaderLabel.Text.Equals(PracticeAreasLabel),
                "Practice Areas label is not visible.");

            var practiceAreaPage = browse.ClickBrowseCategoryContentTypeLink<AdvantageBrowseDialog>("Bankruptcy");

            this.TestCaseVerify.IsTrue(
                checkPracticeAreasPageLoaded,
                browse.BrowseOpenedPageLabel.Text.Equals("Bankruptcy"),
                "'Bankruptcy' page did not load as expected after clicking the link.");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void HomePageLeftToolBarProductSelectorTest()
        {
            const string ProductName = "Practical Law";
            string checkThatPracticalLawLinkWorksAsExpected = "Verify: Practical Law page was opened";

            var homePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);

            var productSelectorDialog = homePage.AdvantageLeftToolbar.LogoButton.Click<AdvantageProductSelectorDialog>();

            var practicalLawPage = productSelectorDialog.ClickProductCategory<PracticalLawPage>(ProductName);

            this.TestCaseVerify.AreEqual(
              checkThatPracticalLawLinkWorksAsExpected,
            practicalLawPage.Header.GetLogoText(), ProductName.Replace(" ", ""),
            "Practical Law page was not opened");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void HomePageMyLinksTest()
        {
            const string CustomePageLabel = "Custom pages";
            string checkCustomePageButton = "Verify: Custom pages label is visible";
            const string CreateCustomPagesButton = "Create Custom Page";
            string checkCreateCustomPagesButton = "Verify: Create Custom Page label is visible";
            const string ELibrariesLabel = "eLibraries";
            string checkELibrariesButton = "Verify: ELibraries label is visible";
            const string FavoritesLabel = "Favorites";
            string checkFavoritesButton = "Verify: Favorites label is visible";
            const string CustomePageHeaderLabel = "Custom Pages";
            string checkCustomePageHeaderLabel = "Verify: Custome Page is opened in New Tab and Header Label is visible";
            string checkELibrariesCustomePageHeaderLabel = "Verify: ELibraries Custome Page is opened in New Tab and Header Label is visible";
            string checkFavoritesCustomePageHeaderLabel = "Verify: Open Favorites Page is opened in New Tab and Header Label is visible";

            var homePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);

            var mylinks = homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.MyLinks).Click<AdvantageMyLinksDialog>();

            SafeMethodExecutor.WaitUntil(() => mylinks.CustomPagesButton.Displayed);
            mylinks.CustomPagesButton.Click();
            SafeMethodExecutor.WaitUntil(() => mylinks.CustomPagesHeader.Displayed);
            this.TestCaseVerify.IsTrue(
                checkCustomePageButton,
                mylinks.CustomPagesHeader.Text.Equals(CustomePageLabel),
                "Custom pages label is not visible");

            SafeMethodExecutor.WaitUntil(() => mylinks.CreateCustomPagesButton.Displayed);
            this.TestCaseVerify.IsTrue(
                checkCreateCustomPagesButton,
                mylinks.CreateCustomPagesButton.Text.Equals(CreateCustomPagesButton),
                "Create Custom pages label is not visible");

            SafeMethodExecutor.WaitUntil(() => mylinks.OpenCustomPagesButton.Displayed);

            var OpenCustomPages = mylinks.OpenCustomPagesButton.Click<AdvantageMyLinksDialog>();
            BrowserPool.CurrentBrowser.CreateTab("Custom Pages");
            BrowserPool.CurrentBrowser.ActivateTab("Custom Pages");

            this.TestCaseVerify.IsTrue(
                checkCustomePageHeaderLabel,
                mylinks.OpenCustomPagesHeader.Text.Equals(CustomePageHeaderLabel),
                "Expected Custome Page Header Label is not Custome Page");

            BrowserPool.CurrentBrowser.CloseTab("Custom Pages");

            BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);
            SafeMethodExecutor.WaitUntil(() => mylinks.ELibrariesButton.Displayed);
            mylinks.ELibrariesButton.Click();
            SafeMethodExecutor.WaitUntil(() => mylinks.eLibrariesHeader.Displayed);
            this.TestCaseVerify.IsTrue(
                checkELibrariesButton,
                mylinks.eLibrariesHeader.Text.Equals(ELibrariesLabel),
                "eLibraries label is not visible");

            SafeMethodExecutor.WaitUntil(() => mylinks.CreateCustomPagesButton.Displayed);
            this.TestCaseVerify.IsTrue(
                checkCreateCustomPagesButton,
                mylinks.CreateCustomPagesButton.Text.Equals(CreateCustomPagesButton),
                "ELibraries - Create Custom pages label is not visible");

            SafeMethodExecutor.WaitUntil(() => mylinks.OpenCustomPagesButton.Displayed);

            var ELibrariesOpenCustomPages = mylinks.OpenCustomPagesButton.Click<AdvantageMyLinksDialog>();
            BrowserPool.CurrentBrowser.CreateTab("Custom Pages");
            BrowserPool.CurrentBrowser.ActivateTab("Custom Pages");

            this.TestCaseVerify.IsTrue(
                checkELibrariesCustomePageHeaderLabel,
                mylinks.OpenCustomPagesHeader.Text.Equals(CustomePageHeaderLabel),
                "ELibraries - Expected Custome Page Header Label is not Custome Page");

            BrowserPool.CurrentBrowser.CloseTab("Custom Pages");
            BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

            SafeMethodExecutor.WaitUntil(() => mylinks.FavoritesButton.Displayed);
            mylinks.FavoritesButton.Click();
            SafeMethodExecutor.WaitUntil(() => mylinks.FavoritesHeader.Displayed);
            this.TestCaseVerify.IsTrue(
                checkFavoritesButton,
                mylinks.FavoritesHeader.Text.Equals(FavoritesLabel),
                "Favorites label is not visible");

            SafeMethodExecutor.WaitUntil(() => mylinks.OpenFavoritesButton.Displayed);

            var OpenFavorites = mylinks.OpenFavoritesButton.Click<AdvantageMyLinksDialog>();
            BrowserPool.CurrentBrowser.CreateTab("Favorites");
            BrowserPool.CurrentBrowser.ActivateTab("Favorites");

            this.TestCaseVerify.IsTrue(
                checkFavoritesCustomePageHeaderLabel,
                mylinks.FavoritesPagesHeader.Text.Equals(FavoritesLabel),
                "Favorites - Expected Open Favorites Header Label is not Favorites Page");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void HomePageAJSToolsHiddenSearchTest()
        {
            const string AiJurisdictionalSurveyLink = "AI Jurisdictional Surveys";
            const string facOff = "AIResearchFiftyStates";
            string checkAJSIsNotHiddenInQuickLinks = "Verify:  AI Jurisdictional Surveys isn't hidden in Quick Links";
            string checkAJSNotPresentInOldHomePageTools = "Verify: AI Jurisdictional Surveys link is not present in Tools Tab in old home page";
            string checkAJSNotPresentInTools = "Verify: AI Jurisdictional Surveys link is not present in Tools Dialog";
            string checkAJSNotPresentInQuickLinks = "Verify: AI Jurisdictional Surveys link is not present in Quick Links section";

            var homePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);

            var quickLinksDialog = homePage.ModifyButton.Click<AdvantageQuickLinksDialog>();
            quickLinksDialog.ClearAllButton.Click();
            quickLinksDialog.EnterCategory(AiJurisdictionalSurveyLink);
            quickLinksDialog.SelectCategoryCheckbox(AiJurisdictionalSurveyLink);
            homePage = quickLinksDialog.IsSaveButtonEnabled ? quickLinksDialog.CancelButton.Click<AdvantageNewHomePage>() : quickLinksDialog.SaveButton.Click<AdvantageNewHomePage>();

            this.TestCaseVerify.IsTrue(
                checkAJSIsNotHiddenInQuickLinks,
                homePage.QuickLinks.Any(item => item.Text.Equals(AiJurisdictionalSurveyLink)),
                " AI Jurisdictional Surveys is hidden in Quick Links");

            homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.SignOut).Click<CommonSignOffPage>();
            this.RoutingPageSettings(facOff);

            var homePageFacOff = this.DefaultSignOnManager.SignOn<AdvantageHomePage, ISignOnContext<IUserInfo>>(this.DefaultSignOnContext);

            var toolsTabPanel = homePageFacOff.TabPanel.SetActiveTab<AdvantageToolsTabPanel>(AdvantageBrowseTab.Tools);

            var AJSLinkAvailabilityInToolsTab = toolsTabPanel.ToolsItems.Any(item => item.HeaderLink.Text == AiJurisdictionalSurveyLink);

            this.TestCaseVerify.IsFalse(
                checkAJSNotPresentInOldHomePageTools,
                AJSLinkAvailabilityInToolsTab,
                "AJS Link present in Tools tab in WLA old home page, but it should NOT be.");

            var newHomePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);

            this.TestCaseVerify.AreNotSame(
            checkAJSNotPresentInQuickLinks,
                newHomePage.QuickLinks.Select(item => item.Text.Contains(AiJurisdictionalSurveyLink)),
                "AI Jurisdictional Surveys is present in Quick Links");


            var toolsDialog = newHomePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.Tools).Click<AdvantageToolsDialog>();

            this.TestCaseVerify.IsFalse(
            checkAJSNotPresentInTools,
            toolsDialog.ToolsButton.Select(item => item.Text).Any(item => item.Contains(AiJurisdictionalSurveyLink)),
            "AI Jurisdictional Surveys is present in Tools");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void HomePageAJSHistoryAndFolderingHiddenSearchTest()
        {
            const string AiJurisdictionaSurveysLabel = "AI Jurisdictional Surveys";
            const string SurveyQuestion = "What's the minimum wage?";
            const string JurisCa = "CA-CS";
            string checkAJSQueryResultsPresentInHistoryDialog = "Verify: Jurisdictional Surveys entry displayed in left tool bar History Dialog";
            string checkAJSQueryResultPresentINFullHistoryTable = "Verify: Jurisdictional Surveys entry displayed in Full History Table";
            string checkAJSQueryResultsPresentInRootFolderDialog = "Verify: Jurisdictional Surveys entry displayed in Root Folder Dialog";
            string checkAJSQueryResultsPresentInFolders = "Verify: Jurisdictional Surveys entry displayed in Folders";

            var homePage = this.GetHomePage<AdvantageHomePage>();
            AiJurisdictionalSurveysPage jurisdictionalSurveysPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AiJurisdictionaSurveysLabel).Click<AiJurisdictionalSurveysPage>();

            BrowserPool.CurrentBrowser.CreateTab(JurisdictionalSurveysTab);
            BrowserPool.CurrentBrowser.ActivateTab(JurisdictionalSurveysTab);

            jurisdictionalSurveysPage.ClosePendoMessage();
            SafeMethodExecutor.WaitUntil(() => jurisdictionalSurveysPage.PageDescription.Displayed, timeoutFromSec: 10);

            jurisdictionalSurveysPage.QueryBox.EnterQuestion(SurveyQuestion);
            jurisdictionalSurveysPage.Jurisdictions.SelectJurisdiction(JurisCa);
            SafeMethodExecutor.WaitUntil(() => jurisdictionalSurveysPage.IsCreateSurveyButtonDisabled, timeoutFromSec: 10);

            jurisdictionalSurveysPage.CreateSurveyButtonTop.ScrollToElement();
            jurisdictionalSurveysPage = jurisdictionalSurveysPage.CreateSurveyButtonTop.Click<AiJurisdictionalSurveysPage>();

            SafeMethodExecutor.WaitUntil(() => !jurisdictionalSurveysPage.ProgressLabel.Displayed, timeoutFromSec: 50);
            var timeStampLabelInitial = jurisdictionalSurveysPage.SurveyResult.TimeStampLabel.Text;

            var jurisdictionLabelsInitial = jurisdictionalSurveysPage.SurveyResult.ResultItems.Select(item => item.JurisdictionNameLabel.Text).ToList();
            var questionInitial = jurisdictionalSurveysPage.SurveyResult.QuestionLabel.Text;

            var saveToFolderDialog = jurisdictionalSurveysPage.Toolbar.SaveToFolderButton.Click<EdgeSaveToFolderDialog>();
            string rootFolder = saveToFolderDialog.FolderTreeComponent.GetRootFolderName();
            saveToFolderDialog.FolderTreeComponent.SelectFolderByName(rootFolder);
            saveToFolderDialog.ClickSaveButton<AiJurisdictionalSurveysPage>();

            BrowserPool.CurrentBrowser.CloseTab(JurisdictionalSurveysTab);
            BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

            this.DefaultSignOnManager.SignOff();
            this.RoutingPageSettings("AIResearchFiftyStates");
            this.DefaultSignOnManager.SignOn<AdvantageHomePage, ISignOnContext<IUserInfo>>(this.DefaultSignOnContext);

            var newHomePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);

            var history = newHomePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.History).Click<AdvantageHistoryDialog>();
            SafeMethodExecutor.WaitUntil(() => history.HistoryButton.Any(), timeoutFromSec: 15);

            this.TestCaseVerify.IsTrue(
            checkAJSQueryResultsPresentInHistoryDialog,
            history.HistoryButton.Select(item => item.Text).First().Contains(SurveyQuestion),
            "AI Jurisdictional Surveys entry not displayed in Left tool bar History Dialog");

            var fullHistoryPage = history.OpenFullHistoryButton.Click<EdgeCommonHistoryPage>();
            BrowserPool.CurrentBrowser.CreateTab(HistoryPageTab);
            BrowserPool.CurrentBrowser.ActivateTab(HistoryPageTab);

            SafeMethodExecutor.WaitUntil(() => fullHistoryPage.HistoryTable.GetGridItems().FirstOrDefault()?.IsTextPresented(SurveyQuestion) == true, timeoutFromSec: 15);

            this.TestCaseVerify.AreEqual(
                checkAJSQueryResultPresentINFullHistoryTable,
                SurveyQuestion,
                fullHistoryPage.HistoryTable.GetGridItems().First().Title,
                "Jurisdictional Surveys entry not displayed in History");

            var foldersHomePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);
            var folders = foldersHomePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.Folders).Click<AdvantageFoldersDialog>();
            folders.RootFolderLink.Click();

            SafeMethodExecutor.WaitUntil(() => folders.UserResearchContentSearchResults != null && folders.UserResearchContentSearchResults.Any(),
                timeoutFromSec: 20);

            this.TestCaseVerify.IsTrue(
            checkAJSQueryResultsPresentInRootFolderDialog,
            folders.UserResearchContentSearchResults.Select(item => item.Text).First().Equals(SurveyQuestion),
            "AI Jurisdictional Surveys entry not displayed in Root Folder Dialog");

            var foldersPage = folders.ViewThisFolderButton.Click<EdgeResearchOrganizerPage>();

            BrowserPool.CurrentBrowser.CreateTab(HistoryPageTab);
            BrowserPool.CurrentBrowser.ActivateTab(HistoryPageTab);

            SafeMethodExecutor.WaitUntil(() => foldersPage.FolderGrid != null, timeoutFromSec: 10);

            this.TestCaseVerify.IsTrue(
            checkAJSQueryResultsPresentInFolders,
            foldersPage.FolderGrid.IsItemDisplayed(SurveyQuestion),
            "AI Jurisdictional Surveys entry not displayed in Folders Page");

            foldersPage.FolderGrid.FolderGridItems.First(item => item.Title.Equals(SurveyQuestion)).ActionsMenu
                  .SelectOption<EdgeResearchOrganizerPage>(ActionsMenuOption.Delete);
        }
       
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        //Bug 2253006: [WL][AI Deep Research] feature incorrectly shown on WL Advantage Old homepage "Key Features" section despite FAC set to Deny
        public void HomePageAiDeepResearchToolsHiddenSearchTest()
        {
            const string AiDeepResearchLink = "AI Deep Research";
            const string facOff = "AIWestlawGuidedResearch";
            string checkAIDeepResearchIsNotHiddenInQuickLinks = "Verify: AI Deep Research isn't hidden in Quick Links";
            string checkADRNotPresentInTools = "Verify: AI Deep Research link is not present in Tools Dialog";
            string checkADRNotPresentInQuickLinks = "Verify: AI Deep Research link is not present in Quick Links section";
            string checkADRNotPresentInToolsTab = $"Verify: AI Deep Research link is not present in Tools tab Advantage Home Page";
            string checkADRNotPresentInTabPanel = $"Verify: AI Deep Research is not present in tab Panel Advantage Home Page";
            string checkADRNotPresentInKeyFeatures = $"Verify: AI Deep Research is not present in Key Features Advantage Home Page";

            var homePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);

            var quickLinksDialog = homePage.ModifyButton.Click<AdvantageQuickLinksDialog>();
            quickLinksDialog.ClearAllButton.Click();
            quickLinksDialog.EnterCategory(AiDeepResearchLink);
            quickLinksDialog.SelectCategoryCheckbox(AiDeepResearchLink);
            homePage = quickLinksDialog.IsSaveButtonEnabled ? quickLinksDialog.CancelButton.Click<AdvantageNewHomePage>() : quickLinksDialog.SaveButton.Click<AdvantageNewHomePage>();

            this.TestCaseVerify.IsTrue(
                checkAIDeepResearchIsNotHiddenInQuickLinks,
                homePage.QuickLinks.Any(item => item.Text.Equals(AiDeepResearchLink)),
                "AI Deep Research is hidden in Quick Links");

            homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.SignOut).Click<CommonSignOffPage>();
            this.RoutingPageSettings(facOff);

           var homePageFacOff = this.DefaultSignOnManager.SignOn<AdvantageHomePage, ISignOnContext<IUserInfo>>(this.DefaultSignOnContext);
           var tabPanelAIDeepResearchLink = homePageFacOff.TabPanel.TabLabels.Any(item => item.Text == AiDeepResearchLink);

            this.TestCaseVerify.IsFalse(
                checkADRNotPresentInTabPanel,
                tabPanelAIDeepResearchLink,
                $"{AiDeepResearchLink}  IS present in Home Page tab panel, but it should NOT be.");

            var toolsTabPanel = homePageFacOff.TabPanel.SetActiveTab<AdvantageToolsTabPanel>(AdvantageBrowseTab.Tools);
            var toolsTabAiDeepResearchLink = toolsTabPanel.ToolsItems.Any
                (item => item.HeaderLink.Text == AiDeepResearchLink);

            this.TestCaseVerify.IsFalse(
                checkADRNotPresentInToolsTab,
                toolsTabAiDeepResearchLink,
                $"{AiDeepResearchLink} link IS present in Tools tab, but it should NOT be.");

            var keyfeaturesAiDeepResearchLink = homePageFacOff.FeaturesIncludedPanel.GetWidgetLinkByTitle(AiDeepResearchLink);

            TestCaseVerify.IsFalse(
                checkADRNotPresentInKeyFeatures,
                keyfeaturesAiDeepResearchLink != null && keyfeaturesAiDeepResearchLink.Displayed,
                $"{AiDeepResearchLink} link IS present in Key Features section, but it should NOT be.");

            var newHomePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);

            this.TestCaseVerify.AreNotSame(
                    checkADRNotPresentInQuickLinks,
                    newHomePage.QuickLinks.Select(item => item.Text.Contains(AiDeepResearchLink)),
                    "AI Deep Research is present in Quick Links");

            var toolsDialog = newHomePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.Tools).Click<AdvantageToolsDialog>();

            this.TestCaseVerify.IsFalse(
            checkADRNotPresentInTools,
            toolsDialog.ToolsButton.Select(item => item.Text).Any(item => item.Contains(AiDeepResearchLink)),
            "AI Deep Research is present in Tools");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void WLAClaimsExplorerHiddenTest()
        {
            string facOff = "AIResearchClaims";
            string ClaimsExplorer = "Claims Explorer";
            string ClaimsExplorerPath = "/Conversation/LandingPage?transitionType=Default&contextData=(sc.Default)&usageFeature=AIClaimsFinder";
            string checkClaimsExplorerIsNotHiddenInQuickLinks = "Verify: Claims Explorer isn't hidden in Quick Links";
            string checkClaimsExplorerIsHiddenInQuickLinksWithFacOff = "Verify: Claims Explorer is hidden in Quick Link";
            string checkClaimsExplorerIsHiddenInToolsTabInOldHomePage = "Verify: Claims Explorer is hidden in Tools section";
            string checkClaimsExplorerIsHiddenInLeftToolsBarWithFacOffWithFacOff = "Verify: Claims Explorer is hidden in Left Tools Bar";
            string checkLandingPage = "Verify: Claims Explorer page is opened";

            var homePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);

            var quickLinksDialog = homePage.ModifyButton.Click<AdvantageQuickLinksDialog>();
            quickLinksDialog.ClearAllButton.Click();
            quickLinksDialog.EnterCategory(ClaimsExplorer);
            quickLinksDialog.SelectCategoryCheckbox(ClaimsExplorer);
            homePage = quickLinksDialog.IsSaveButtonEnabled ? quickLinksDialog.CancelButton.Click<AdvantageNewHomePage>() : quickLinksDialog.SaveButton.Click<AdvantageNewHomePage>();

            this.TestCaseVerify.IsTrue(
                checkClaimsExplorerIsNotHiddenInQuickLinks,
                homePage.QuickLinks.Any(item => item.Text.Equals(ClaimsExplorer)),
                "Claims Explorer is hidden in Quick Links");

            homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.SignOut).Click<CommonSignOffPage>();
            this.RoutingPageSettings(facOff);

            var homePageFacOff = this.DefaultSignOnManager.SignOn<AdvantageHomePage, ISignOnContext<IUserInfo>>(this.DefaultSignOnContext);

            var tooltab = homePageFacOff.BrowseTabPanel.SetActiveTab<PrecisionToolsTabPanel>(PrecisionBrowseTab.Tools);

            this.TestCaseVerify.IsFalse(checkClaimsExplorerIsHiddenInToolsTabInOldHomePage,
                tooltab.ToolsItems.Any(item => item.HeaderLink.Text.Contains(ClaimsExplorer)),
                "Claims Explorer is hidden in't Tools section");

            var newhomePageFacOff = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);

            this.TestCaseVerify.IsFalse(
                checkClaimsExplorerIsHiddenInQuickLinksWithFacOff,
                homePage.QuickLinks.Any(item => item.Text.Equals(ClaimsExplorer)),
                "Claims Explorer isn't hidden in Quick Links");

            var toolsTabPanel = newhomePageFacOff.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.Tools).Click<AdvantageToolsDialog>();
            this.TestCaseVerify.IsFalse(checkClaimsExplorerIsHiddenInLeftToolsBarWithFacOffWithFacOff,
                toolsTabPanel.ToolsButton.Any(button => button.Text.Equals(ClaimsExplorer)),
                "Claims Explorer isn't hidden in Left Tools Bar");

            var claimsExplorerPage = BrowserPool.CurrentBrowser.GoToPath<AiClaimsExplorerPage>(ClaimsExplorerPath);

            this.TestCaseVerify.IsTrue(
                checkLandingPage,
                BrowserPool.CurrentBrowser.Title.Contains($"Claims Explorer")
                && claimsExplorerPage.Toolbar.HeadingLabel.Text.Equals("Claims Explorer"),
                "Claims Explorer page is NOT opened");

        }

        //Bug 2241550: [AAR] Full answer is returned instead of 'Claims Explorer' link if search specific questions
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void WLAClaimsExplorerHiddenHistoryTest()
        {
            string facOff = "AIResearchClaims";
            string ClaimsExplorer = "Claims Explorer";
            const string Question = "Where do I file a foreign subpoena in Arizona from a Utah court case?";

            string checkHistoryPageLeadsToClaimsExplorer = "Verify: Event from the History page leads to Claims Explorer page";

            var homePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);
            var toolsTabPanel = homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.Tools).Click<AdvantageToolsDialog>();
            var aiClaimsExplorerPage = toolsTabPanel.ToolsButton.First(item => item.Text.Equals(ClaimsExplorer)).Click<AiClaimsExplorerPage>();

            BrowserPool.CurrentBrowser.CreateTab(ClaimsExplorer);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorer);

            var aiJurisdictionDialog = aiClaimsExplorerPage.Toolbar.JurisdictionButton.Click<AiJurisdictionDialog>();
            aiClaimsExplorerPage = aiJurisdictionDialog.SelectJurisdictionsClaimsExplorer(true, Jurisdiction.California).SaveButton.Click<AiClaimsExplorerPage>();

            aiClaimsExplorerPage = aiClaimsExplorerPage.QueryBox.QuestionTextbox.SetText<AiClaimsExplorerPage>(Question);
            aiClaimsExplorerPage = aiClaimsExplorerPage.QueryBox.SendQuestionButton.Click<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => aiClaimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.DateLabel.Displayed);

            this.DefaultSignOnManager.SignOff();
            this.RoutingPageSettings(facOff);

            var homePageFacOff = this.DefaultSignOnManager.SignOn<AdvantageHomePage, ISignOnContext<IUserInfo>>(this.DefaultSignOnContext);

            var historyPage = EdgeNavigationManager.Instance.GoToHistoryPage<EdgeCommonHistoryPage>();

            //History event click
            var aiClaimsExplorerPageFacOff = historyPage.HistoryTable.GetGridItems().First().ClickLinkByText<AiClaimsExplorerPage>(historyPage.HistoryTable.GetGridItems().First().Title);
            SafeMethodExecutor.WaitUntil(() => !aiClaimsExplorerPageFacOff.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkHistoryPageLeadsToClaimsExplorer,
                aiClaimsExplorerPageFacOff.Chat.ClaimsExplorerQuestionAndAnswerItem.QuestionLabel.Text.Remove(0, aiClaimsExplorerPageFacOff.Chat.ClaimsExplorerQuestionAndAnswerItem.QuestionLabel.Text.IndexOf("says:") + 7).Equals(Question)
                && aiClaimsExplorerPageFacOff.Chat.ClaimsExplorerQuestionAndAnswerItem.AnswerTabPanel.IsDisplayed(ClaimsExplorerAnswerTab.State)
                && aiClaimsExplorerPageFacOff.Toolbar.JurisdictionLabel.Text.Equals("CA, All Fed."),
                "Event from the History page doesn't lead to Claims Explorer page");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void WLAKeywordBooleanRecentSearchesTest()
        {
            var homePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);

            const string RecentSearchExpectedKeywordQuestion = "Is alcoholism a disability under NY law, and can you be terminated because of it?";
            string checkKBRSPresentInRecentSearch = "Verify: Excepted Question is present in Recent Search in Keyword Boolean ";

            var keywordSearchPanel = homePage.SearchTabPanel.SetActiveTab<KeywordAndBooleanSearchTabPanel>(AdvantageSearchTabs.KeywordAndBooleanSearch);
            keywordSearchPanel.QueryTextArea.SendKeys(RecentSearchExpectedKeywordQuestion);
            keywordSearchPanel.SearchButton.Click();

            BrowserPool.CurrentBrowser.CreateTab("Keyword Boolean Search");
            BrowserPool.CurrentBrowser.ActivateTab("Keyword Boolean Search");
            SafeMethodExecutor.ExecuteUntil(() => keywordSearchPanel.KeywordBooleanSearchHeader.Displayed);

            BrowserPool.CurrentBrowser.GoBack();

            SafeMethodExecutor.WaitUntil(() => homePage.SearchTabPanel.SetActiveTab<KeywordAndBooleanSearchTabPanel>(AdvantageSearchTabs.KeywordAndBooleanSearch).IsDisplayed());
            keywordSearchPanel.QueryTextArea.SendKeys(" ");
            SafeMethodExecutor.WaitUntil(() => keywordSearchPanel.RecentSearchPanelList.Any(), timeoutFromSec: 15);

            this.TestCaseVerify.AreEqual(
                 checkKBRSPresentInRecentSearch,
                 RecentSearchExpectedKeywordQuestion,
                 keywordSearchPanel.RecentSearchPanelList.Select(item => item.Text).First(),
                 "Excepted Question is not present in Recent Search in Keyword Boolean");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void WLAKeywordBooleanRecentSavedSearchesDeleteSavedSearchTest()
        {
            var homePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);

            const string RecentSearchExpectedKeywordQuestion = "Is alcoholism a disability under NY law, and can you be terminated because of it?";
            string checkKBRSPresentInSavedSearch = "Verify: Excepted Question is present in Saved Search in Keyword Boolean ";
            const string NoSavedSearchQuestion = "There are no saved searches to display";
            string checkNOSavedSearch = "Verify: Saved Question is deleted and not present in Saved Search in Keyword Boolean";

            var keywordSearchPanel = homePage.SearchTabPanel.SetActiveTab<KeywordAndBooleanSearchTabPanel>(AdvantageSearchTabs.KeywordAndBooleanSearch);
            keywordSearchPanel.QueryTextArea.SendKeys(RecentSearchExpectedKeywordQuestion);
            keywordSearchPanel.SearchButton.Click();

            BrowserPool.CurrentBrowser.CreateTab("Keyword Boolean Search");
            BrowserPool.CurrentBrowser.ActivateTab("Keyword Boolean Search");
            SafeMethodExecutor.ExecuteUntil(() => keywordSearchPanel.KeywordBooleanSearchHeader.Displayed);

            BrowserPool.CurrentBrowser.GoBack();

            SafeMethodExecutor.WaitUntil(() => homePage.SearchTabPanel.SetActiveTab<KeywordAndBooleanSearchTabPanel>(AdvantageSearchTabs.KeywordAndBooleanSearch).IsDisplayed());
            keywordSearchPanel.QueryTextArea.SendKeys(" ");
            SafeMethodExecutor.WaitUntil(() => keywordSearchPanel.RecentSearchPanelList.Any(), timeoutFromSec: 15);

            SafeMethodExecutor.WaitUntil(() => keywordSearchPanel.StarIconButton.Enabled, timeoutFromSec: 15);
            keywordSearchPanel.StarIconButton.Click();
            keywordSearchPanel.SavedSearchesButton.Click();

            this.TestCaseVerify.AreEqual(
                 checkKBRSPresentInSavedSearch,
                 RecentSearchExpectedKeywordQuestion,
                 keywordSearchPanel.SelectedStarIconButton.Text,
                "Excepted Question is not present in Saved Search in Keyword Boolean");

            SafeMethodExecutor.ExecuteUntil(() => keywordSearchPanel.SavedSearchesButton.Displayed, timeoutFromSec: 15);
            keywordSearchPanel.SelectSavedStarIcon(RecentSearchExpectedKeywordQuestion);
            SafeMethodExecutor.ExecuteUntil(() => keywordSearchPanel.NoSavedSearchLabel.Displayed, timeoutFromSec: 15);

            this.TestCaseVerify.AreEqual(
                checkNOSavedSearch,
                NoSavedSearchQuestion,
                keywordSearchPanel.NoSavedSearchLabel.Text,
                "Selected Question is still present in Saved Search in Keyword Boolean");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void WLAAIDeepResearchHiddenSearchHistoryFolderTest()
        {
            const string facOffAIDR = "AIWestlawGuidedResearch";
            const string DeepAIResearchTab = "Westlaw Deep AI Research";
            const string AIDRQuestion = "what's the minimum wage?";
            string checkAIDRQueryResultsPresentInHistoryDialog = "Verify: AI Deep Research Surveys entry displayed in left tool bar History Dialog";
            string checkAIDRQueryResultPresentINFullHistoryTable = "Verify: AI Deep Research Surveys entry displayed in Full History Table";
            string checkAIDRQueryResultsPresentInRootFolderDialog = "Verify: AI Deep Research Surveys entry displayed in Root Folder Dialog";
            string checkAIDRQueryResultsPresentInFolders = "Verify: AI Deep Research Surveys entry displayed in Folders";

            var folderPage = EdgeNavigationManager.Instance.GoToFoldersPage<EdgeResearchOrganizerPage>();
            folderPage.ClearFolderGrid();

            var homePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);

            var aideepresearchPanel = homePage.SearchTabPanel.SetActiveTab<AIDeepResearchTabPanel>(AdvantageSearchTabs.AIDeepResearch);
            aideepresearchPanel.QuestionTextArea.SendKeys(AIDRQuestion);
            aideepresearchPanel.SendButton.Click();

            BrowserPool.CurrentBrowser.CreateTab(DeepAIResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(DeepAIResearchTab);

            SafeMethodExecutor.ExecuteUntil(() => aideepresearchPanel.AIDeepResearchMessageBox.Displayed, timeoutFromSec: 150);

            var saveToFolderDialog = aideepresearchPanel.AIDeepResearchSaveToFolderButton.Click<EdgeSaveToFolderDialog>();
            string rootFolder = saveToFolderDialog.FolderTreeComponent.GetRootFolderName();
            saveToFolderDialog.FolderTreeComponent.SelectFolderByName(rootFolder);
            saveToFolderDialog.ClickSaveButton<AIDeepResearchTabPanel>();

            BrowserPool.CurrentBrowser.CloseTab(DeepAIResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

            homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.SignOut).Click<CommonSignOffPage>();
            this.RoutingPageSettings(facOffAIDR);

            var homePageFacOff = this.DefaultSignOnManager.SignOn<AdvantageHomePage, ISignOnContext<IUserInfo>>(this.DefaultSignOnContext);

            var newHomePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);

            SafeMethodExecutor.ExecuteUntil(() => homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.History).Displayed);
            var historyDialog = homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.History).Click<AdvantageHistoryDialog>();
            SafeMethodExecutor.ExecuteUntil(() => historyDialog.HistoryLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkAIDRQueryResultsPresentInHistoryDialog,
                historyDialog.HistoryButton.Select(item => item.Text).Any(item => item.Contains(AIDRQuestion)),
                "Expected Question is not Present in History Dialog");

            var allHistoryPage = historyDialog.OpenFullHistoryButton.Click<EdgeCommonHistoryPage>();

            BrowserPool.CurrentBrowser.CreateTab("All History");
            BrowserPool.CurrentBrowser.ActivateTab("All History");
            SafeMethodExecutor.ExecuteUntil(() => allHistoryPage.HistoryTable.GetGridItems().Equals("All History"));

            this.TestCaseVerify.IsTrue(
                checkAIDRQueryResultPresentINFullHistoryTable,
                allHistoryPage.HistoryTable.GetGridItems().First().Title.Contains(AIDRQuestion),
                "Expected AI Deep Research Surveys Question is not Present in All History Page");

            BrowserPool.CurrentBrowser.CloseTab("All History");
            BrowserPool.CurrentBrowser.ActivateTab(F1HomePageLink);

            var foldersDialog = newHomePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.Folders).Click<AdvantageFoldersDialog>();
            foldersDialog.RootFolderLink.Click();

            SafeMethodExecutor.WaitUntil(() => foldersDialog.UserResearchContentSearchResults != null && foldersDialog.UserResearchContentSearchResults.Any(),
                timeoutFromSec: 20);

            this.TestCaseVerify.IsTrue(
            checkAIDRQueryResultsPresentInRootFolderDialog,
            foldersDialog.UserResearchContentSearchResults.Select(item => item.Text).First().Equals(AIDRQuestion),
            "AI Deep Research Surveys entry not displayed in Root Folder Dialog");

            var foldersPage = foldersDialog.ViewThisFolderButton.Click<EdgeResearchOrganizerPage>();

            BrowserPool.CurrentBrowser.CreateTab(HistoryPageTab);
            BrowserPool.CurrentBrowser.ActivateTab(HistoryPageTab);

            SafeMethodExecutor.WaitUntil(() => foldersPage.FolderGrid != null, timeoutFromSec: 10);

            this.TestCaseVerify.IsTrue(
            checkAIDRQueryResultsPresentInFolders,
            foldersPage.FolderGrid.IsItemDisplayed(AIDRQuestion),
            "AI Deep Research Surveys entry not displayed in Folders Page");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void WLAAJSIncludeCasesCheckboxNotActiveDeliveryTest()
        {
            const string AiJurisdictionaSurveysLabel = "AI Jurisdictional Surveys";
            const string SurveyQuestion = "find the lemon law for all 50 states";
            const string Jurisdiction = "CA-CS", Jurisdiction2 = "TX-CS", Jurisdiction3 = "FL-CS";

            string checkCasesSectionNotShown = "Verify: Cases section is NOT shown for each Jurisdiction";
            string checkStatutesAndRegulationsSectionShown = "Verify: Statutes and regulations is shown for each Jurisdiction";

            FileUtil.DeleteFilesInFolderByMask(FolderToSave, "*.*");

            var homePage = this.GetHomePage<AdvantageHomePage>();
            AiJurisdictionalSurveysPage jurisdictionalSurveysPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AiJurisdictionaSurveysLabel).Click<AiJurisdictionalSurveysPage>();

            BrowserPool.CurrentBrowser.CreateTab(JurisdictionalSurveysTab);
            BrowserPool.CurrentBrowser.ActivateTab(JurisdictionalSurveysTab);

            SafeMethodExecutor.WaitUntil(() => jurisdictionalSurveysPage.PageDescription.Displayed, timeoutFromSec: 10);
            homePage.WlaAjsSurveyResult.DisableIncludeCasesCheckbox();

            jurisdictionalSurveysPage.QueryBox.EnterQuestion(SurveyQuestion);
            jurisdictionalSurveysPage.Jurisdictions.SelectJurisdiction(Jurisdiction, Jurisdiction2, Jurisdiction3);
            Thread.Sleep(2000); // Sleep added to wait for jurisdictions to be selected and Create Survey button to be enabled, can be removed once the underlying issue is fixed
            jurisdictionalSurveysPage.CreateSurveyButtonTop.ScrollToElement();
            jurisdictionalSurveysPage = jurisdictionalSurveysPage.CreateSurveyButtonTop.Click<AiJurisdictionalSurveysPage>();

            SafeMethodExecutor.WaitUntil(() => !jurisdictionalSurveysPage.ProgressLabel.Displayed, timeoutFromSec: 500 );

            this.TestCaseVerify.IsTrue(
                checkStatutesAndRegulationsSectionShown,
                homePage.WlaAjsSurveyResult.WLAAjsResultItems.All(label => label.ResultsSummaryStatutesAndRegulationsLabel.Text.Contains("statutes and regulations")),
                "Statutes and Regulations section is not shown for 1 or more jurisdictions");

            this.TestCaseVerify.IsFalse(
                checkCasesSectionNotShown,
                homePage.WlaAjsSurveyResult.WLAAjsResultItems.Any(label => label.ResultsJurisdictionCasesLabel.Text.Equals("Cases")),
                "Cases section is shown despite Include Cases checkbox disabled");

            var downloadDialog = jurisdictionalSurveysPage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog.LayoutAndLimitsTab.SetIncludeSectionOption(LayoutAndLimitsInclude.CoverPage);

            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);

            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<AiJurisdictionalSurveysPage>();
            var fileName = $"Westlaw Advantage - AI Jurisdictional Survey - {DateTime.Now.ToString(DeliveryDateFormat)}.pdf";
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            var text = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, fileName));

            var textWithoutWhitespaces = text.Replace(" ", string.Empty);

            this.TestCaseVerify.IsTrue(
              checkStatutesAndRegulationsSectionShown,
              textWithoutWhitespaces.Contains("statutesandregulations"),
                "Statutes and Regulations section is not shown in delivery document");

            this.TestCaseVerify.IsTrue(
             checkCasesSectionNotShown,
             !textWithoutWhitespaces.Contains("Cases"),
               "Cases is shown in delivery document");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void HomePageSmartBrowseToggleTest()
        {
            const string Question = "How do i serve a subpoena for deposition in Florida?";
            const string SmartBrowseEnabledText = "Smart Browse uses AI-generated highlighting in response to your query. Not legal advice. Content accuracy and legal compliance must be verified by a qualified professional.";
            string checkSmartBrowseToggleIsOffByDefault = "Verify: Smart Browse toggle is OFF by default";
            string checkSmartBrowseToggleStateIsOn = "Verify: Smart Browse toggle state is ON after enabling";
            string checkSmartBrowseToggleEnabledTextIsShown = "Verify: Smart Browse toggle enabled text is shown after enabling";

            var homePage = this.GetHomePage<AdvantageHomePage>();
            var searchHomePage = this.GetHomePage<CommonSearchHomePage>();
            searchHomePage.Header.OpenJurisdictionDialog()
                .SelectJurisdictions(true, Jurisdiction.Florida).ClickSelectButton<CommonSearchHomePage>();
            var searchResultsPage = searchHomePage.Header.EnterSearchQueryAndClickSearch<ContentTypeSearchResultsPage>(Question);

            homePage.globalSearchResults.ContentTypesTab.Click<AdvantageSmartBrowseComponent>();
            homePage.globalSearchResults.StatutesAndCourtRulesLink.Click<AdvantageSmartBrowseComponent>();
            homePage.globalSearchResults.WlaSearchQuestionAndAnswerItems.First().StatutesResultsTitleLink.First().Click();

            SafeMethodExecutor.WaitUntil(() => homePage.globalSearchResults.SmartBrowseToggle.Displayed , timeoutFromSec: 15);

            bool initialState = homePage.globalSearchResults.GetSmartBrowseToggleState();

            this.TestCaseVerify.IsTrue(
                checkSmartBrowseToggleIsOffByDefault,
                !initialState,
                "Smart Browse toggle is ON by default, but it should not be");

            homePage.globalSearchResults.ClickSmartBrowseToggle();           

            SafeMethodExecutor.WaitUntil(() => homePage.globalSearchResults.SmartBrowseToggle.Displayed
            && homePage.globalSearchResults.SmartBrowseToggle.Enabled
            && homePage.globalSearchResults.GetSmartBrowseToggleState() == true, timeoutFromSec: 20);
            
            bool isSmartBrowseEnabled = homePage.globalSearchResults.GetSmartBrowseToggleState();

            this.TestCaseVerify.IsTrue(
                checkSmartBrowseToggleStateIsOn,
                isSmartBrowseEnabled,
                "Smart Browse toggle state is OFF after enabling, but it should be ON");
           
            var smartBrowseContent = homePage.globalSearchResults.SmartBrowseEnabledText.Text;

            this.TestCaseVerify.IsTrue(
            checkSmartBrowseToggleEnabledTextIsShown,
            smartBrowseContent.Replace("\r\n", " ").Equals(SmartBrowseEnabledText),
            "Smart Browse toggle enabled text is not shown/not correct");

            homePage.globalSearchResults.ClickSmartBrowseToggle();
            SafeMethodExecutor.WaitUntil(() => homePage.globalSearchResults.GetSmartBrowseToggleState() == false, timeoutFromSec: 10);
        }

        /// <summary>
        /// Test Case 2271947: [CoCounsel in WL] Record in History in header
        /// 1. Open Westlaw Advantage and click CoCounsel button in header
        /// 2. Check: Verify CoCounsel is expanded below
        /// 3. Search question in AI Jurisdictional Surveys via CoCounsel
        /// 4. Check: Verify response is completed
        /// 5. Expand History in Header
        /// 6. Check: Verify History shows question from AJS CoCounsel
        /// 7. Click question link in History
        /// 8. Check: Verify AJS with full response is opened in the same window tab
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniCategory)]
        public void CoCounselAJSRecordInHistoryTest()
        {
            const string SurveyQuestion = "What is the statutory definition of \"credit union\"? I am interested in New Hampshire, Wisconsin, Minnesota, Washington DC, Maine, and Georgia.";

            string checkCoCounselExpanded = "Verify: CoCounsel is expanded in header";
            //string checkResponseCompleted = "Verify: AJS response is completed";
            //string checkQuestionInHistoryDialog = "Verify: AJS CoCounsel question is displayed in History Dialog";
            //string checkAJSPageOpensFromHistory = "Verify: AJS with full response opens from History link";

            var homePage = this.GetHomePage<AdvantageHomePage>();

            homePage = homePage.Header.OpenJurisdictionDialog().SelectDefaultJurisdiction().SaveButton.Click<AdvantageHomePage>();

            var coCounselChatAssistantDialog = new CoCounselChatAssistantDialog();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.MaximizeButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.QuestionTextbox.Displayed);

            coCounselChatAssistantDialog.Chat.QuestionTextbox.SendKeys(SurveyQuestion);
            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.AiAssistedResearchRadiobutton.Displayed);

            coCounselChatAssistantDialog.Chat.AiAssistedResearchRadiobutton.Select();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.StartAiAssistedResearchButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => !coCounselChatAssistantDialog.Chat.StartAiAssistedResearchButton.Displayed);
            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.ViewAllCitedSourcesButton.Present);

            coCounselChatAssistantDialog.Chat.ViewAllCitedSourcesButton.ScrollToElement();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.CollapseButton.Click<CoCounselChatAssistantDialog>();

            var clientIdPage = new CommonClientIdPage();

            var eventDescription = clientIdPage.RecentResearchPane.RecentResearchList.First(item => item.Text.Contains(SurveyQuestion)).Text.Replace(" ", string.Empty);

            this.TestCaseVerify.IsTrue(
                checkCoCounselExpanded,
                eventDescription.Equals($"{SurveyQuestion.Replace(" ", string.Empty)}\r\nAI-AssistedResearchAllState&FederalCoCounsel"),
                "Recent search event DOESN'T contain question, event type and jurisdiction");

            var aiAssistantPage = clientIdPage.RecentResearchPane.RecentResearchList.First(item => item.Text.Contains(SurveyQuestion)).ClickTitleLink<AiAssistedResearchPage>();

            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkCoCounselExpanded,
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.Remove(0, aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().QuestionLabel.Text.IndexOf("says:") + 7).Equals(SurveyQuestion)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.Count.Equals(1)
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed,
                "Recent search event click DOESN'T open the recent conversation");

            // History tab
            var recentHistoryDialog = aiAssistantPage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);

            this.TestCaseVerify.IsTrue(
                checkCoCounselExpanded,
                recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemLink.Text.Equals(SurveyQuestion)
                && recentHistoryDialog.GetRecentSearchesItems().First().MetaData.Replace(" ", string.Empty).Contains($"AI-AssistedResearchAllState&FederalCoCounsel"),
                "History event is NOT displayed on the History tab");

            aiAssistantPage = aiAssistantPage.ConversationHistory.ExpandButton.Click<AiAssistedResearchPage>();

            // Conversations (Left Rail) history
            var lastConversationEvent = aiAssistantPage.ConversationHistory.Conversations.First().ConversationButton.Text;
            // History page
            var historyPage = aiAssistantPage.ConversationHistory.GoToFullHistoryLink.Click<EdgeCommonHistoryPage>();

            BrowserPool.CurrentBrowser.CreateTab(HistoryPageTab);
            BrowserPool.CurrentBrowser.ActivateTab(HistoryPageTab);

            this.TestCaseVerify.IsTrue(
                checkCoCounselExpanded,
                historyPage.NarrowPane.Filter.HistoryEventFacet.GetSelectedOptions().First().Equals("AI Research"),
                "History event facet is NOT applied");

            this.TestCaseVerify.AreEqual(
                checkCoCounselExpanded,
                $"{SurveyQuestion}AI-AssistedResearchAllState&FederalCoCounsel".Replace(" ", string.Empty),
                $"{historyPage.HistoryTable.GetGridItems().First().Title}{historyPage.HistoryTable.GetGridItems().First().Summary}".Replace(" ", string.Empty),
                "History page event DOESN'T contains question, event type and jurisdiction");

            // Step 1: Click CoCounsel button in header and maximize dialog
            //var coCounselButton = homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.CoCunsel);
            //coCounselButton.Click();

            //var coCounselChatAssistantDialog = new CoCounselChatAssistantDialog();
            //coCounselChatAssistantDialog = coCounselChatAssistantDialog.MaximizeButton.Click<CoCounselChatAssistantDialog>();

            //SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.QuestionTextbox.Displayed);

            //this.TestCaseVerify.IsTrue(
            //    checkCoCounselExpanded,
            //    coCounselChatAssistantDialog.Chat.QuestionTextbox.Displayed,
            //    "CoCounsel dialog is not expanded");

            //// Step 2: Enter AJS question and submit
            //coCounselChatAssistantDialog.Chat.QuestionTextbox.SendKeys(SurveyQuestion);
            //coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();

            //// Step 3: Wait for jurisdiction selection and select jurisdictions
            //SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.SelectedJurisdictionLabel.Displayed, timeoutFromSec: 30);

            //coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();

            //// Step 4: Wait for response to complete
            //SafeMethodExecutor.WaitUntil(() => !coCounselChatAssistantDialog.Chat.ProgressLabel.Displayed, timeoutFromSec: 120);
            //SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.CoCounselQuestionAndAnswerItems.Any());

            ////this.TestCaseVerify.IsTrue(
            ////    checkResponseCompleted,
            ////    coCounselChatAssistantDialog.Chat.CoCounselQuestionAndAnswerItems.First().DateLabel.Displayed,
            ////    "AJS response is not completed");

            //// Close CoCounsel dialog
            //coCounselChatAssistantDialog = coCounselChatAssistantDialog.CollapseButton.Click<CoCounselChatAssistantDialog>();

            //// Step 5: Expand History in Header and verify question is shown
            //SafeMethodExecutor.WaitUntil(() => homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.History).Displayed);
            //var historyDialog = homePage.AdvantageLeftToolbar.GetToolsBarButtonByName(AdvantageLeftToolbarItems.History).Click<AdvantageHistoryDialog>();
            //SafeMethodExecutor.WaitUntil(() => historyDialog.HistoryLabel.Displayed);

            //this.TestCaseVerify.IsTrue(
            //    checkQuestionInHistoryDialog,
            //    historyDialog.HistoryButton.Select(item => item.Text).Any(item => item.Contains(SurveyQuestion)),
            //    "AJS CoCounsel question is not displayed in History Dialog");

            //// Step 6: Click question link in History and verify AJS opens with full response
            //var historyItem = historyDialog.HistoryButton.First(item => item.Text.Contains(SurveyQuestion));
            //var ajsPageFromHistory = historyItem.Click<AiJurisdictionalSurveysPage>();

            //SafeMethodExecutor.WaitUntil(() => ajsPageFromHistory.SurveyResult.QuestionLabel.Displayed, timeoutFromSec: 20);

            //this.TestCaseVerify.IsTrue(
            //    checkAJSPageOpensFromHistory,
            //    ajsPageFromHistory.SurveyResult.QuestionLabel.Text.Contains(SurveyQuestion)
            //    && ajsPageFromHistory.SurveyResult.TimeStampLabel.Displayed,
            //    "AJS with full response did not open from History link");
        }

        protected string LoginAndGetUserName()
        {
            var userInfo = this.GetUserInfo();
            string loginUserName = userInfo.UserName;
            return loginUserName;
        }
        protected override void InitializeRoutingPageSettings()
        {
            base.InitializeRoutingPageSettings();
            this.Settings.AppendValues(
                EnvironmentConstants.FeatureAccessControlsOn,
                SettingUpdateOption.Append,
                FeatureAccessControl.AIWestlawGuidedResearch);

            if (this.TestContext.Properties["AIResearchFiftyStates"] != null && this.TestContext.Properties["AIResearchFiftyStates"].Equals("Off"))
            {
                this.Settings.AppendValues(
                    EnvironmentConstants.FeatureAccessControlsOff,
                    SettingUpdateOption.Append,
                    FeatureAccessControl.AIResearchFiftyStates);
            }
        }
     
        protected void RoutingPageSettings(string featureAccessControlsOff)
        {
            this.InitializeRoutingPageSettings();

            this.Settings.AppendValues(
                EnvironmentConstants.FeatureAccessControlsOff,
                SettingUpdateOption.Append,
                featureAccessControlsOff);
            }
    }
}
