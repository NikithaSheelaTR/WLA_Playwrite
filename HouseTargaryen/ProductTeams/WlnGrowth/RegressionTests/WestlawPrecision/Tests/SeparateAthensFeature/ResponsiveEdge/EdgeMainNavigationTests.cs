namespace WestlawPrecision.Tests.SeparateAthensFeature.ResponsiveEdge
{
    using Framework.Common.UI.Raw.WestlawEdge.Enums.BrowseComponent;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Footer;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Header;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.NotificationCenter;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass] 
    public class ResponsiveMainNavigationTests : EdgeResponsiveBaseTest
    {
        protected const string FeatureTestCategory = "EdgeMainNavigation";

        /// <summary>
        /// Test the links on hamburger menu (Story #1324063).
        /// 1. Log on WL Edge and reduce browser width to 1024
        /// 2. Open hamburger menu and click Folders link and Check: if Folders page is displayed
        /// 3. Open hamburger menu and click History link and Check: if History page is displayed
        /// 4. Open hamburger menu and click Favorites link and Check: if Favorites page is displayed
        /// 5. Open hamburger menu and click Notifications link and Check: if Notification Center page is displayed
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void EdgeMainNavigationHamburgerMenuLinksTest()
        { 
            const string ExpectedPartOfHistoryPageTitle = "History | Westlaw";
            const string ExpectedPartOfFoldersPageTitle = "Research | Westlaw";
            const string ExpectedPartOfFavoritesPageTitle = "My Favorites | Westlaw";
            const string ExpectedNotificationLabelTitle = "Notifications";
            const int BrowserWidth = 1024;
            const int BrowserHeight = 900;

            this.SetBrowserSize(BrowserWidth, BrowserHeight);

            var researchOrganizerPage = this.GetHomePage<EdgeHomePage>().Header.ClickHamburgerMenuButton()
                    .ClickLink<EdgeResearchOrganizerPage>(GlobalNavigationRightPaneLinks.Folders);
            this.TestCaseVerify.IsTrue(
                "Verify 'Folders' tab navigates to 'Folders' page.",
                researchOrganizerPage.Title.Contains(ExpectedPartOfFoldersPageTitle),
                "'Folders' tab NOT navigates to 'Folders' page.");

            var historyPage = researchOrganizerPage.Header.ClickHamburgerMenuButton()
                         .ClickLink<EdgeCommonHistoryPage>(GlobalNavigationRightPaneLinks.History);
            this.TestCaseVerify.IsTrue(
                "Verify 'History' tab navigates to 'History' page.",
                historyPage.Title.Contains(ExpectedPartOfHistoryPageTitle),
                "'History' tab NOT navigates to 'History' page.");

            var favoritesPage = historyPage.Header.ClickHamburgerMenuButton()
                                           .ClickLink<EdgeCommonFavoritesPage>(GlobalNavigationRightPaneLinks.Favorites);
            this.TestCaseVerify.IsTrue(
                "Verify 'Favorites' tab navigates to 'Favorites' page.",
                favoritesPage.Title.Contains(ExpectedPartOfFavoritesPageTitle),
                "'Favorites' tab NOT navigates to 'Favorites' page.");

            var notificationCenterPage = researchOrganizerPage.Header.ClickHamburgerMenuButton()
           .ClickLink<NotificationCenterPage>(GlobalNavigationRightPaneLinks.Notifications);
            this.TestCaseVerify.IsTrue(
                "Verify 'Notifications' tab navigates to 'Notification Center' page.",
                notificationCenterPage.NotificationHeaderLabel.Text.Equals(ExpectedNotificationLabelTitle),
                "'Notifications' tab NOT navigates to 'Notification Center' page.");
        }

        /// <summary>
        /// Test the Full Desktop link in responsive mode (Story #1324066).
        /// 1. Log on WL Edge and reduce browser width to 1024
        /// 2. Click Full Desktop link at footer
        /// 3. Check: Full Desktop link is not displayed
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void EdgeFullDesktopLinkTest()
        {
            const int BrowserWidth = 1024;
            const int BrowserHeight = 900;

            this.SetBrowserSize(BrowserWidth, BrowserHeight);

            var homePage = this.GetHomePage<EdgeHomePage>().Footer.ClickLink<EdgeHomePage>(EdgeFooterLinks.FullDesktop);
            this.TestCaseVerify.IsFalse(
                "Verify 'Full Desktop' link is not displayed after it's clicked.",
                homePage.Footer.IsFooterLinkDisplayed(EdgeFooterLinks.FullDesktop),
                "'Full Desktop' link is displayed after it's clicked.");
        }

        /// <summary>
        /// Test the browse content links (Story ##1324076).
        /// 1. Log on WL Edge and reduce browser width to 768
        /// 2. Click Content types and then Cases link and Check: Cases browse page is displayed
        /// 3. Click home logo, Federal materials, and then Federal Cases link
        /// 4. Check: Federal Cases browse page is displayed
        /// 5. Click home logo, State materials, and then Alabama link
        /// 6. Check: Alabama browse page is displayed
        /// 7. Click home logo, Practice areas, and then Antitrust link
        /// 8. Check: Antitrust browse page is displayed
        /// 9. Click home logo, Tools
        /// 10.Check: Case Evaluator link is displayed (used to be hidden)
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void EdgeMainNavigationBrowseContentLinksTest()
        {
            const string ExpectedCasesPageTitle = "Cases | Westlaw";
            const string ExpectedFederalCasesPageTitle = "Federal Cases | Westlaw";
            const string ExpectedAlabamaPageTitle = "Alabama | Westlaw";
            const string ExpectedAntitrustPageTitle = "Antitrust | Westlaw";
            const string CaseEvaluatorName = "Case Evaluator";
            const int BrowserWidth = 582;
            const int BrowserHeight = 900;

            this.SetBrowserSize(BrowserWidth, BrowserHeight);

            var edgeHomePage = this.GetHomePage<EdgeHomePage>();
            var casesBrowsePage = edgeHomePage.BrowseTabPanel
                                              .ClickBrowseTab<EdgeHomePage>(BrowseTab.ContentTypes).BrowseContentRightPaneComponent
                                              .ClickBrowseContentLink<EdgeCommonBrowsePage>(BrowseContentRightPaneLinks.Cases);
            this.TestCaseVerify.IsTrue(
                "Verify 'Content types.Cases' link navigates to 'Cases' page.",
                casesBrowsePage.Title.Contains(ExpectedCasesPageTitle),
                "'Content types.Cases' link NOT navigates to 'Cases' page.");

            edgeHomePage = casesBrowsePage.Header.ClickLogo<EdgeHomePage>();

            var federalCasesBrowsePage = edgeHomePage.BrowseTabPanel
                                                     .ClickBrowseTab<EdgeHomePage>(BrowseTab.FederalMaterials).BrowseContentRightPaneComponent
                                                     .ClickBrowseContentLink<EdgeCommonBrowsePage>(BrowseContentRightPaneLinks.FederalCases);
            this.TestCaseVerify.IsTrue(
                "Verify 'Federal materials.Federal Cases' link navigates to 'Federal Cases' page.",
                federalCasesBrowsePage.Title.Contains(ExpectedFederalCasesPageTitle),
                "'Federal materials.Federal Cases' link NOT navigates to 'Federal Cases' page.");

            edgeHomePage = federalCasesBrowsePage.Header.ClickLogo<EdgeHomePage>();

            var alabamaBrowsePage = edgeHomePage.BrowseTabPanel
                                                .ClickBrowseTab<EdgeHomePage>(BrowseTab.StateMaterials).BrowseContentRightPaneComponent
                                                .ClickBrowseContentLink<EdgeCommonBrowsePage>(BrowseContentRightPaneLinks.Alabama);
            this.TestCaseVerify.IsTrue(
                "Verify 'State materials.Alabama' link navigates to 'Alabama' page.",
                alabamaBrowsePage.Title.Contains(ExpectedAlabamaPageTitle),
                "'State materials.Alabama' link NOT navigates to 'Alabama' page.");

            edgeHomePage = alabamaBrowsePage.Header.ClickLogo<EdgeHomePage>();

            var antitrustBrowsePage = edgeHomePage.BrowseTabPanel
                                                  .ClickBrowseTab<EdgeHomePage>(BrowseTab.PracticeAreas).BrowseContentRightPaneComponent
                                                  .ClickBrowseContentLink<EdgeCommonBrowsePage>(BrowseContentRightPaneLinks.Antitrust);
            this.TestCaseVerify.IsTrue(
                "Verify 'Practice areas.Antitrust' link navigates to 'Antitrust' page.",
                antitrustBrowsePage.Title.Contains(ExpectedAntitrustPageTitle),
                "'Practice areas.Antitrust' link NOT navigates to 'Antitrust' page.");

            edgeHomePage = antitrustBrowsePage.Header.ClickLogo<EdgeHomePage>();
            bool isCaseEvaluatorToolDisplayed = edgeHomePage.BrowseTabPanel.ClickBrowseTab<EdgeHomePage>(BrowseTab.Tools)
                                                            .BrowseContentRightPaneComponent.IsTextLinkDisplayed(CaseEvaluatorName);
            this.TestCaseVerify.IsTrue(
                "Verify 'Tools.Case Evaluator' link is displayed in responsive mode.",
                isCaseEvaluatorToolDisplayed,
                "'Tools.Case Evaluator' link NOT displayed in responsive mode.");
        }
    }
}