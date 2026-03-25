namespace WestlawPrecision.Tests.SeparateAthensFeature.ResponsiveEdge
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Enums.RI;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Products.WestlawEdge.Components.EdgeResponsive;
    using Framework.Common.UI.Products.WestLawNext.Enums.RI;
    using Framework.Common.UI.Products.WestLawNext.Pages.IpTools;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo.StatuteHistoryPages;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.Utils.Enums;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EdgeDocumentRiTabTests : EdgeResponsiveBaseTest
    {
        protected const string FeatureTestCategory = "EdgeDocument";

        /// <summary>
        /// Test the Related Documents button text on search result page and document page (Story ##1324089 and #1324091).
        /// 1. Log on WL Edge and set jurisdiction to all states and federal
        /// 2. Run search (query = tax) and reduce browser width to 600
        /// 3. Check: Related button text remains: Related documents)
        /// 4. Maximize browser and click document by guid (I94a47930441d11ecae80b6011f92c3df)
        /// 5. Reduce browser width to 600 and Check: Related button text is Related documents
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void EdgeRelatedButtonTextTest()
        {
            const string DocGuid = "Iba81801040e311e49488c8f438320c70";
            const string Query = "Tax";
            const string ExpectedButtonText = "Related documents";
            const int BrowserWidth = 600;
            const int BrowserHeight = 900;

            this.GetHomePage<EdgeHomePage>().Header.OpenJurisdictionDialog()
                .SelectJurisdictions(true, Jurisdiction.AllStates, Jurisdiction.AllFederal).ClickSelectButton<CommonSearchHomePage>();
            var commonSearchResultPage = this.GetHomePage<EdgeHomePage>().Header.EnterSearchQueryAndClickSearch<EdgeCommonSearchResultPage>(Query);
            this.SetBrowserSize(BrowserWidth, BrowserHeight); 

            // Sleep was added to wait for changing the button text
            Thread.Sleep(2000);

            this.TestCaseVerify.AreEqual(
                "Verify 'Related documents' button text remains 'Related documents' on 'Search result' page.",
                ExpectedButtonText,
                commonSearchResultPage.ResultList.GetRelatedDocumentsButtonText(),
                "'Related documents' button text NOT displayed on 'Search result' page.");

            BrowserPool.CurrentBrowser.Maximize();
            var documentPage = commonSearchResultPage.ResultList.ClickOnSearchResultDocumentByGuid<EdgeCommonDocumentPage>(DocGuid);
            this.SetBrowserSize(BrowserWidth, BrowserHeight);

            // Sleep was added to wait for changing the button text
            Thread.Sleep(2000);

            this.TestCaseVerify.AreEqual(
                "Verify 'Related documents' button text remains 'Related documents' on 'Document' page.",
                ExpectedButtonText,
                documentPage.FixedHeader.GetRelatedDocumentsButtonName(),
                "'Related documents' button text NOT displayed on 'Document' page.");
        }

        /// <summary>
        /// Test the Full Desktop link in responsive mode (Story #1485531).
        /// 1. Log on WL Edge and do a find on: 13 F3d 888
        /// 2. Reduce the browser width to 500px
        /// 3. Check: previous carousel button is displayed
        /// 4. Check: next carousel button is displayed
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void EdgeRiTabCarouselButtonsTest()
        {
            const string Query = "13 F3d 888";
            const int BrowserWidth = 500;
            const int BrowserHeight = 900;

            var documentPage = this.GetHomePage<EdgeHomePage>().Header.EnterSearchQueryAndClickSearch<EdgeCommonDocumentPage>(Query, true);
            this.SetBrowserSize(BrowserWidth, BrowserHeight);

            this.TestCaseVerify.IsTrue(
                "Verify Previous carousel button is displayed.",
                documentPage.PreviousCarouselButton.Displayed,
                "Previous carousel button is not displayed.");

            this.TestCaseVerify.IsTrue(
                "Verify Next carousel button is displayed.",
                documentPage.NextCarouselButton.Displayed,
                "Next carousel button is not displayed.");
        }

        /// <summary>
        /// Test bugs 1697624 and 1697625
        /// 1. Sign in WL Edge and find in All States & Federal on: WA ST 18.06.140
        /// 2. Go to Versions page under History tab
        /// 3. Reduce browser width to 1024px (responsive mode)
        /// 4. Click Kebab menu and then select Search within tool
        /// 5. Check: Search within widget is displayed after selecting from Kebab menu
        /// 6. Check: Filter button is displayed and left panel is not (they should not co-exist)
        /// 7. Click Filter button and click Legislative History Materials
        /// 8. Click Filter button to display left panel
        /// 9. Check: Legislative History Materials has Search within filter widget as expected
        /// * Test fails due to tab not being whitelisted
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void EdgeSearchWithinWidgetTest()
        {
            const string Query = "WA ST 18.06.140";
            const int BrowserWidth = 1024;
            const int BrowserHeight = 1000;

            var documentPage = this.GetHomePage<EdgeHomePage>().Header.EnterSearchQueryAndClickSearch<EdgeCommonDocumentPage>(Query);
            var statuteHistoryPage = documentPage.RiTabs.ClickTab<StatuteHistoryWestlawEdgePage>(RiTab.History);
            var versionsPage = statuteHistoryPage.ClickOnkHistorySection<VersionsPage>(HistorySections.Versions);
            this.SetBrowserSize(BrowserWidth, BrowserHeight);

            var toolsRightPanelComponent = versionsPage.Toolbar.ClickKebabMenuButton();
            Thread.Sleep(2000);
            versionsPage = toolsRightPanelComponent.SearchWithinButton.Click<VersionsPage>();

            this.TestCaseVerify.IsTrue(
                "Verify Search within widget is displayed after selecting from Kebab menu",
                versionsPage.IsDisplayed(Dialogs.NotesOfDecisionsSearchWithin),
                "Search within is not displayed after selecting from Kebab menu");

            this.TestCaseVerify.IsTrue(
                "Verify Filter button is displayed and left panel is not displayed",
                versionsPage.Toolbar.FilterButton.Displayed && !versionsPage.NarrowPane.IsDisplayed(),
                "Filter button and left panel should not be displayed at the same time");

            var legHistoryPage = versionsPage.Toolbar.ClickFilterButton().SwitchToHistorySection<LegislativeHistoryMaterialsPage>(HistorySections.LegislativeHistoryMaterials);
            bool searchWithinDisplayed = legHistoryPage.Toolbar.ClickFilterButton().SearchWithinButton.Displayed;
            this.TestCaseVerify.IsTrue(
                "Verify Legislative History Materials has Search within as expected",
                searchWithinDisplayed,
                "Legislative History Materials should have Search within");
        }

        /// <summary>
        /// For Story 1678239: [AUTOMATION] WL Edge RI Tabs: General Pattern: Left Panel
        /// 1. Sign in WL Edge and find in All States & Federal on: 440 P.3d 1150
        /// 2. Reduce browser width to 1024px (responsive mode)
        /// 3. Check: Filter button not displayed on Document tab
        /// 4. Click Kebab menu and then Table of contents
        /// 5. Check: Left panel displayed on Document tab
        /// 6. Close left panel then go to Filings tab
        /// 7. Check: Filter button displayed on Filings tab
        /// 8. Click Filter button
        /// 9. Check: Left panel displayed on Filings tab
        /// 10.Close left panel then go to Citing references tab
        /// 11.Check: Filter button displayed on Citing references tab
        /// 12.Click Filter button
        /// 13.Check: Left panel displayed on Citing references tab
        /// 14.Close left panel then go to Table of Authorities tab
        /// 11.Check: Filter button displayed on Table of Authorities tab
        /// 12.Click Filter button
        /// 13.Check: Left panel displayed on Table of Authorities tab
        /// 14.Close left panel on Table of Authorities tab
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void EdgeLeftPanelTest()
        {
            const string Query = "440 P.3d 1150";
            const int BrowserWidth = 1024;
            const int BrowserHeight = 1000;
            this.SetBrowserSize(BrowserWidth, BrowserHeight);

            // Check Document tab
            var documentPage = this.GetHomePage<EdgeHomePage>().Header.EnterSearchQueryAndClickSearch<EdgeCommonDocumentPage>(Query);
            this.TestCaseVerify.IsFalse(
                "Verify left panel not displayed on Document tab",
                documentPage.Toc.IsDisplayed(),
                "Left panel should not display on Document tab");

            this.TestCaseVerify.IsFalse(
                "Verify Filter button not displayed on Document tab",
                documentPage.Toolbar.FilterButton.Displayed,
                "Filter button should not display on Document tab");

            documentPage = documentPage.Toolbar.ClickKebabMenuButton().TableOfContentsButton.Click<EdgeCommonDocumentPage>();
            this.TestCaseVerify.IsTrue(
                "Verify clicking TOC from Kebab menu on Document tab",
                documentPage.ResponsiveLeftPanel.CloseButton.Displayed,
                "Left panel not displayed with Close button");
            documentPage = documentPage.ResponsiveLeftPanel.CloseButton.Click<EdgeCommonDocumentPage>();

            // Check Filings tab
            var filingsPage = documentPage.RiTabs.ClickTab<FilingsPage>(RiTab.Filings);
            this.TestCaseVerify.IsTrue(
                "Verify Filter button displayed on Filings RI tab",
                filingsPage.Toolbar.FilterButton.Displayed,
                "Filter button not displayed on Filings RI tab");

            filingsPage = filingsPage.Toolbar.FilterButton.Click<FilingsPage>();
            this.TestCaseVerify.IsTrue(
                "Verify clicking Filter button on Filings tab",
                filingsPage.ResponsiveLeftPanel.CollapseButton.Displayed,
                "Left panel not displayed with collapse button");
            filingsPage.ResponsiveLeftPanel.CollapseButton.Click();

            // Check Citing References tab
            var citingReferencesPage = filingsPage.RiTabs.ClickTab<CitingReferencesPage>(RiTab.CitingReferences);
            this.TestCaseVerify.IsTrue(
                "Verify Filter button displayed on Citing References RI tab",
                citingReferencesPage.Toolbar.FilterButton.Displayed,
                "Filter button not displayed on Citing References RI tab");

            citingReferencesPage = citingReferencesPage.Toolbar.FilterButton.Click<CitingReferencesPage>();
            this.TestCaseVerify.IsTrue(
                "Verify clicking Filter button on Citing References tab",
                citingReferencesPage.ResponsiveLeftPanel.CollapseButton.Displayed,
                "Left panel not displayed with collapse button");
            citingReferencesPage.ResponsiveLeftPanel.CollapseButton.Click();

            // Check Table of Authorities tab
            var tableOfAuthoritiesPage = citingReferencesPage.RiTabs.ClickTab<TableOfAuthoritiesPage>(RiTab.TableOfAuthorities);
            this.TestCaseVerify.IsTrue(
                "Verify Filter button displayed on Table Of Authorities RI tab",
                tableOfAuthoritiesPage.Toolbar.FilterButton.Displayed,
                "Filter button not displayed on Table Of Authorities RI tab");

            tableOfAuthoritiesPage = tableOfAuthoritiesPage.Toolbar.FilterButton.Click<TableOfAuthoritiesPage>();
            this.TestCaseVerify.IsTrue(
                "Verify clicking Filter button on Table Of Authorities tab",
                tableOfAuthoritiesPage.ResponsiveLeftPanel.CollapseButton.Displayed,
                "Left panel not displayed with collapse button");
            tableOfAuthoritiesPage.ResponsiveLeftPanel.CollapseButton.Click();
        }

        /// <summary>
        /// For Story 1678245: [AUTOMATION] WL Edge: Responsive Design - RI Tabs: Claims History 3-dot menu
        /// 1. Sign in WL Edge and find in All States & Federal on: US PAT 6481516
        /// 2. Click IP Tools and click Claim History
        /// 3. Reduce browser width to 1024px (responsive mode)
        /// 4. Check: Filter button is displayed
        /// 5. Click Filter button
        /// 6. Check: Left panel is displayed
        /// * Test fails due to tab not being whitelisted
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void EdgeIpToolsClaimsHistoryLeftPanelTest()
        {
            const string Query = "US PAT 6481516";
            const int BrowserWidth = 1024;
            const int BrowserHeight = 1000;

            var ipToolsDocPage = this.GetHomePage<EdgeHomePage>().Header.EnterSearchQueryAndClickSearch<CommonDocumentPage>(Query);
            var ipToolsOverviewPage = ipToolsDocPage.RiTabs.ClickTab<IpToolsPage>(RiTab.IpTools);
            var claimHistoryPage = ipToolsOverviewPage.ClaimsHistory.ClickSection<TabPage>();
            this.SetBrowserSize(BrowserWidth, BrowserHeight);

            this.TestCaseVerify.IsTrue(
                "Verify Filter button displayed on IP Tools: Claim History page",
                claimHistoryPage.Toolbar.FilterButton.Displayed,
                "Filter button should display on Claim History page");

            claimHistoryPage = claimHistoryPage.Toolbar.FilterButton.Click<FilingsPage>();
            this.TestCaseVerify.IsTrue(
                "Verify clicking Filter button on IP Tools: Claim History page",
                claimHistoryPage.ResponsiveLeftPanel.CollapseButton.Displayed,
                "Left panel not displayed with collapse button");
            claimHistoryPage.ResponsiveLeftPanel.CollapseButton.Click();
        }

        /// <summary>
        /// For User Story 1678258: [AUTOMATION] Edge: Responsive Design - RI Tabs(1): History responsive toolbar
        /// 1. Sign in WL Edge and find in All States & Federal on: 194 L.Ed.2d 655
        /// 2. Click History to go to History RI tab page
        /// 3. Check: Graphical keycite should be displayed
        /// 4. Reduce browser width to 1024px (responsive mode)
        /// 5. Check: Graphical keycite continues to be displayed
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void EdgeHideGraphicsHistoryTabTest()
        {
            const string Query = "194 L.Ed.2d 655";
            const int BrowserWidth = 1024;
            const int BrowserHeight = 1000;

            var docPage = this.GetHomePage<EdgeHomePage>().Header.EnterSearchQueryAndClickSearch<CommonDocumentPage>(Query);
            var historyPage = docPage.RiTabs.ClickTab<OtherHistoryPage>(RiTab.History);

            this.TestCaseVerify.IsTrue(
                "Verify graphical keycite displayed in non-responsive mode",
                historyPage.IsGraphicalKeyCiteDisplayed(),
                "Graphical keycite should display on RI History page");

            this.SetBrowserSize(BrowserWidth, BrowserHeight);
            this.TestCaseVerify.IsTrue(
                "Verify graphical keycite displayed in responsive mode",
                historyPage.IsGraphicalKeyCiteDisplayed(),
                "Graphical keycite should display on RI History page");
        }

        /// <summary>
        /// For User Story 1680964 (1799012) and test story 1678845 on Notes of Decisions tab
        /// 1. Sign in WL Edge and find in All States & Federal on: 17 USCA 101
        /// 2. Click Notes of Decisions tab 
        /// 3. Reduce browser width to 1024px (responsive mode)
        /// 4. Click Filter button to display left panel
        /// 5. Check: Left panel has scrollbar for displaying all content
        /// 6. Close left panel and click 3-dot menu to display right panel
        /// 7. Check: KeyCite status is displayed in 3-dot menu
        /// * Test fails due to tab not being whitelisted
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void EdgeNotesOfDecisionsTabTest()
        {
            const string Query = "17 USCA 101";
            const int BrowserWidth = 1024;
            const int BrowserHeight = 1000;

            var docPage = this.GetHomePage<EdgeHomePage>().Header.EnterSearchQueryAndClickSearch<CommonDocumentPage>(Query);
            var nodPage = docPage.RiTabs.ClickTab<NotesOfDecisionsPage>(RiTab.NotesOfDecisions);
            this.SetBrowserSize(BrowserWidth, BrowserHeight);

            this.TestCaseVerify.IsTrue(
                "Verify left panel has scrollbar for displaying all content",
                nodPage.Toolbar.ClickFilterButton().IsScrollbarDisplayed(),
                "Left panel has no scrollbar. Some content may be missing.");
            BrowserPool.CurrentBrowser.Refresh();

            this.TestCaseVerify.IsTrue(
                "Verify KeyCite status is displayed in 3-dot menu",
                nodPage.Toolbar.ClickKebabMenuButton().KeyCiteStatusButton.Displayed,
                "KeyCite status is not displayed in 3-dot menu");
            BrowserPool.CurrentBrowser.Refresh();
        }

        /// <summary>
        /// For User Story 1679129 (1795790): [AUTOMATION] WL Edge: Responsive Design - RI Tabs: General Pattern: Pagination controls
        /// 1. Sign in WL Edge and find in All States & Federal on: MEDADV § 54:29
        /// 2. Click Medical References tab
        /// 3. Reduce browser width to 1024px (responsive mode)
        /// 4. Check: Results per page is displayed in 3-dot menu
        /// * Test fails due to tab not being whitelisted
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void EdgeMedicalReferencesTabTest()
        {
            const string Query = "MEDADV § 54:29";
            const int BrowserWidth = 1024;
            const int BrowserHeight = 1000;

            var docPage = this.GetHomePage<EdgeHomePage>().Header.EnterSearchQueryAndClickSearch<CommonDocumentPage>(Query);
            var medRefPage = docPage.RiTabs.ClickTab<TabPage>(RiTab.MedicalReferences);
            this.SetBrowserSize(BrowserWidth, BrowserHeight);

            this.TestCaseVerify.IsTrue(
                "Verify Results per page is displayed in 3-dot menu",
                medRefPage.Toolbar.ClickKebabMenuButton().ResultsPerPageButton.Displayed,
                "Results per page is not displayed in 3-dot menu");
            BrowserPool.CurrentBrowser.Refresh();
        }

        /// <summary>
        /// Test User Story 1679587: WL Edge: RI Tabs: Sorting: 2) Add Sorting option to 3 dot menu for RI Tabs
        /// 1. Sign in WL Edge and find in All States & Federal on: 101 sct 1
        /// 2. Go to Table of Authorities tab and get selected sort option
        /// 3. Reduce browser width to 1024px (responsive mode)
        /// 4. Click Kebab menu and Sort submenu and select Quote: Quoted First option
        /// 5. Check: Setting Sort option on Table of Authorities page works (compare default and selected options)
        /// 6. Find in All States & Federal on: 17 USCA 101
        /// 7. Go to Citing References tab and get selected sort option
        /// 8. Click Kebab menu and Sort submenu and select Date: Oldest First option
        /// 9. Check: Setting Sort option on Citing References page works
        /// 10.Find in All States & Federal on: 17 USCA 101
        /// 11.Go to History: Legislative Materials and get selected sort option
        /// 12.Click Kebab menu and Sort submenu and select Date: Oldest First option
        /// 13.Check: Setting Sort option on History: Legislative Materials page works
        /// 10.Find in All States & Federal on: MEDADV § 54:29
        /// 11.Go to Medical References and get selected sort option
        /// 12.Click Kebab menu and Sort submenu and select Date: Oldest First option
        /// 13.Check: Setting Sort option on Medical References page works
        /// * Test fails due to tab not being whitelisted
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void EdgeToolbarSortWidgetTest()
        {
            const string ToaQuery = "101 sct 1";
            const string NodQuery = "17 USCA 101";//NOD sort is implemented differently so dropping testing on NOD
            const string MedRefQuery = "MEDADV § 54:29";
            const string ToaDefaultSort = "Alphabetically by Title";
            const string CiteRefDefaultSort = "Date: Newest First";
            const string MedRefDefaultSort = "Relevance";
            const int BrowserWidth = 1024;
            const int BrowserHeight = 1000;

            var documentPage = this.GetHomePage<EdgeHomePage>().Header.EnterSearchQueryAndClickSearch<CommonDocumentPage>(ToaQuery);
            var toaPage = documentPage.RiTabs.ClickTab<TableOfAuthoritiesPage>(RiTab.TableOfAuthorities);
            string defaultSort = toaPage.Toolbar.SortBy.SelectedOptionText;
            if(!defaultSort.Equals(ToaDefaultSort))
                toaPage = toaPage.Toolbar.SortBy.SelectOption<TableOfAuthoritiesPage>(ToaDefaultSort);
            this.SetBrowserSize(BrowserWidth, BrowserHeight);
            RightPanelSortComponent sortPanel = toaPage.Toolbar.ClickKebabMenuButton().ClickSortButton();
            Thread.Sleep(2000);
            toaPage = sortPanel.QuoteQuotedFirstButton.Click<TableOfAuthoritiesPage>();
            BrowserPool.CurrentBrowser.Maximize();
            string newSort = toaPage.Toolbar.SortBy.SelectedOptionText;

            this.TestCaseVerify.IsFalse(
                "Verify setting Sort option on Table of Authorities page works",
                defaultSort.Equals(newSort),
                "Setting Sort value on Kebab submenu does not work");
            toaPage.Toolbar.SortBy.SelectOption<TableOfAuthoritiesPage>(ToaDefaultSort);

            documentPage = this.GetHomePage<EdgeHomePage>().Header.EnterSearchQueryAndClickSearch<CommonDocumentPage>(NodQuery);
            var citeRefPage = documentPage.RiTabs.ClickTab<CitingReferencesPage>(RiTab.CitingReferences);
            defaultSort = citeRefPage.Toolbar.SortBy.SelectedOptionText;
            if (!defaultSort.Equals(CiteRefDefaultSort))
                citeRefPage = citeRefPage.Toolbar.SortBy.SelectOption<CitingReferencesPage>(CiteRefDefaultSort);
            this.SetBrowserSize(BrowserWidth, BrowserHeight);
            sortPanel = citeRefPage.Toolbar.ClickKebabMenuButton().ClickSortButton();
            Thread.Sleep(2000);
            citeRefPage = sortPanel.DateOldestFirstButton.Click<CitingReferencesPage>();
            BrowserPool.CurrentBrowser.Maximize();
            newSort = citeRefPage.Toolbar.SortBy.SelectedOptionText;

            this.TestCaseVerify.IsFalse(
                "Verify setting Sort option on Citing References page works",
                defaultSort.Equals(newSort),
                "Setting Sort value on Kebab submenu does not work");
            citeRefPage.Toolbar.SortBy.SelectOption<CitingReferencesPage>(CiteRefDefaultSort);

            documentPage = this.GetHomePage<EdgeHomePage>().Header.EnterSearchQueryAndClickSearch<CommonDocumentPage>(NodQuery);
            var historyPage = documentPage.RiTabs.ClickTab<StatuteHistoryWestlawEdgePage>(RiTab.History);
            var legHistoryPage = historyPage.ClickOnkHistorySection<LegislativeHistoryMaterialsPage>(HistorySections.LegislativeHistoryMaterials);
            defaultSort = legHistoryPage.Toolbar.SortBy.SelectedOptionText;
            if (!defaultSort.Equals(CiteRefDefaultSort))
                legHistoryPage = legHistoryPage.Toolbar.SortBy.SelectOption<LegislativeHistoryMaterialsPage>(CiteRefDefaultSort);
            this.SetBrowserSize(BrowserWidth, BrowserHeight);
            sortPanel = legHistoryPage.Toolbar.ClickKebabMenuButton().ClickSortButton();
            Thread.Sleep(2000);
            legHistoryPage = sortPanel.DateOldestFirstButton.Click<LegislativeHistoryMaterialsPage>();
            BrowserPool.CurrentBrowser.Maximize();
            newSort = legHistoryPage.Toolbar.SortBy.SelectedOptionText;

            this.TestCaseVerify.IsFalse(
                "Verify setting Sort option on History: Legislative History Materials page works",
                defaultSort.Equals(newSort),
                "Setting Sort value on Kebab submenu does not work");
            legHistoryPage.Toolbar.SortBy.SelectOption<LegislativeHistoryMaterialsPage>(CiteRefDefaultSort);

            documentPage = this.GetHomePage<EdgeHomePage>().Header.EnterSearchQueryAndClickSearch<CommonDocumentPage>(MedRefQuery);
            var medRefPage = documentPage.RiTabs.ClickTab<TabPage>(RiTab.MedicalReferences);
            defaultSort = medRefPage.Toolbar.SortBy.SelectedOptionText;
            if (!defaultSort.Equals(MedRefDefaultSort))
                medRefPage = medRefPage.Toolbar.SortBy.SelectOption<TabPage>(MedRefDefaultSort);
            this.SetBrowserSize(BrowserWidth, BrowserHeight);
            sortPanel = medRefPage.Toolbar.ClickKebabMenuButton().ClickSortButton();
            Thread.Sleep(2000);
            medRefPage = sortPanel.DateOldestFirstButton.Click<TabPage>();
            BrowserPool.CurrentBrowser.Maximize();
            newSort = medRefPage.Toolbar.SortBy.SelectedOptionText;

            this.TestCaseVerify.IsFalse(
                "Verify setting Sort option on Medical References page works",
                defaultSort.Equals(newSort),
                "Setting Sort value on Kebab submenu does not work");
            medRefPage.Toolbar.SortBy.SelectOption<TabPage>(MedRefDefaultSort);
        }

        /// <summary>
        /// Test story 1680999 and bug 1787752 (1796026)
        /// 1. Sign in WL Edge and find in All States & Federal on: WA ST 18.06.140
        /// 2. Go to Versions page under History tab
        /// 3. Reduce browser width to 1024px (responsive mode)
        /// 4. Click Filter button and then click Legislative History Materials
        /// 5. Check: Switching content using left panel works on History tab
        /// 6. Check: Left panel collapsed after switching content on History tab
        /// 7. Find: 17 USCA 101 and go to Context & Analysis tab
        /// 8. Click Filter button and then click Forms
        /// 9. Check: Switching content using left panel works on Context & Analysis tab
        /// 10.Check: Left panel collapsed after switching content on Context & Analysis tab
        /// * Test fails due to tab not being whitelisted
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void EdgeLeftPanelSwitchingContentTest()
        {
            const string HistoryQuery = "WA ST 18.06.140";
            const string ContextQuery = "17 USCA 101";
            const string ContextSwitchTo = "Forms";
            const int BrowserWidth = 1024;
            const int BrowserHeight = 1000;

            var documentPage = this.GetHomePage<EdgeHomePage>().Header.EnterSearchQueryAndClickSearch<EdgeCommonDocumentPage>(HistoryQuery);
            var statuteHistoryPage = documentPage.RiTabs.ClickTab<StatuteHistoryWestlawEdgePage>(RiTab.History);
            var versionsPage = statuteHistoryPage.ClickOnkHistorySection<VersionsPage>(HistorySections.Versions);
            this.SetBrowserSize(BrowserWidth, BrowserHeight);

            var legHistoryPage = versionsPage.Toolbar.ClickFilterButton().SwitchToHistorySection<LegislativeHistoryMaterialsPage>(HistorySections.LegislativeHistoryMaterials);
            Thread.Sleep(2000);
            bool pageSwitched = legHistoryPage.CategoryLabelResponsive.HiddenText.Contains(HistorySections.LegislativeHistoryMaterials.GetStringValue());

            this.TestCaseVerify.IsTrue(
                "Verify switching content using left panel works on History tab",
                pageSwitched,
                "Switching content using left panel failed on History tab");

            this.TestCaseVerify.IsFalse(
                "Verify left panel collapsed after switching content on History tab",
                legHistoryPage.IsLeftPanelDisplayed(),
                "Left panel should be collapsed after switching content on History tab");

            documentPage = this.GetHomePage<EdgeHomePage>().Header.EnterSearchQueryAndClickSearch<EdgeCommonDocumentPage>(ContextQuery);
            var contextPage = documentPage.RiTabs.ClickTab<ContextAndAnalysisPage>(RiTab.ContextAnalysis);
            contextPage = contextPage.Toolbar.ClickFilterButton().ClickLinkByText<ContextAndAnalysisPage>(ContextSwitchTo);
            Thread.Sleep(2000);
            List<string> headers = contextPage.GetAllContentHeaders();

            this.TestCaseVerify.IsTrue(
                "Verify switching content using left panel works on Context & Analysis tab",
                headers.Any(h => h.Contains(ContextSwitchTo)),
                "Switching content using left panel failed on Context & Analysis tab");

            this.TestCaseVerify.IsFalse(
                "Verify left panel collapsed after switching content on Context & Analysis tab",
                contextPage.IsLeftPanelDisplayed(),
                "Left panel should be collapsed after switching content on Context & Analysis tab");
        }
    }
}
