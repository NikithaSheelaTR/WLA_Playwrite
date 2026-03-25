namespace WestlawPrecision.Tests.SeparateAthensFeature.ParallelSearch
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Core.Utils.Execution;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;
    using Framework.Common.UI.Products.Shared.Dialogs.Delivery;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.WestLawNext.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Foldering;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.AALP;
    using Framework.Common.UI.Products.WestlawEdge.Components.History;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo;
    using Framework.Common.UI.Products.Shared.Enums.RI;
    using Framework.Core.Utils.IO;
    using Framework.Common.UI.Raw.EnhancementTests.Utils;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.History;
    using Framework.Common.UI.Products.WestLawNext.Enums.Toolbars;
    using System.IO;
    using System;   
    using System.Linq;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    /// <summary>
    /// Parallel Search Tests
    /// </summary>
    [TestClass]
    public class ParallelSearchTests : ParallelSearchBaseTest
    {
        private const string FeatureTestCategoryParallelSearch = "ParallelSearch";

        /// <summary>
        /// Test Parallel Search page opens, search works, and document navigation works 
        /// (Stories:2091164 2091166 2095020 2101076 2103881 Test Cases:2100562 2100979 2099581 2104392 2104984).
        /// 1. Sign in WL Precision with Parallel Search access
        /// 2. Click Parallel Search link next to global search
        /// 3. Check: Verify Parallel Search page opens on new tab
        /// 4. Submit search: Possession of drugs is inadmissible in a DWI prosecution under rule 403
        /// 5. Check: Verify search results are displayed
        /// 6. Click first result title link
        /// 7. Check: Verify clicking result title link opens to document page
        /// 8. Click Return button on document page
        /// 9. Check: Verify clicking return button on document returns to Parallel Search results
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryParallelSearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory("ParallelSearchSmokeTest")]
        public void ParallelSearchSmokeTest()
        {
            const string SearchQuery = "Possession of drugs is inadmissible in a DWI prosecution under rule 403";

            var parallelSearchPage = this.GetHomePage<PrecisionHomePage>().Header.ParallelSearchLink.Click<ParallelSearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(ParallelSearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(ParallelSearchTab);

            this.TestCaseVerify.IsTrue(
                "Verify access link next to global search opens Parallel Search page on new tab",
                parallelSearchPage.PageHeader.Displayed,
                "Access link next to global search does not open Parallel Search page on new tab");

            parallelSearchPage.QueryBox.EnterSearchQuery(SearchQuery);
            parallelSearchPage.QueryBox.SubmitSearchQuery();
            SafeMethodExecutor.WaitUntil(() => !parallelSearchPage.ProgressRingLabel.Displayed);

            var resultCount = parallelSearchPage.Results.CasesItems.Count;

            this.TestCaseVerify.IsTrue(
               "Verify search results are displayed",
               parallelSearchPage.Results.ResultCountLabel.Text.Contains("Cases (" + resultCount + ")"),
               "Search results are not displayed. Result count: " + resultCount);

            var documentPage = parallelSearchPage.Results.CasesItems.First().DocumentTitleLink.Click<EdgeCommonDocumentPage>();

            this.TestCaseVerify.IsTrue(
               "Verify clicking Parallel Search result title link opens to document page",
               documentPage.IsDocumentLoaded(),
               "Clicking Parallel Search result title link does not open to document page");

            parallelSearchPage = documentPage.FixedHeader.ClickReturnToListButton<ParallelSearchPage>();
            SafeMethodExecutor.WaitUntil(() => !parallelSearchPage.ProgressRingLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                "Verify clicking return button on document returns to Parallel Search results",
                parallelSearchPage.Results.ResultCountLabel.Text.Contains("Cases ("),
                "Clicking return button on document does not return to Parallel Search results");
        }

        /// <summary>
        /// Test Parallel Search page opens, Search Tip Modal validations 
        /// (Stories:2091155 Test Cases:2099996).
        /// 1. Sign in WL Precision with Parallel Search access
        /// 2. Click Parallel Search link next to global search
        /// 3. click on Parallel Search Tips button
        /// 4. Check: Verify Search Tips Title displayed
        /// 5. Check: Verify Search Tips Header displayed
        /// 6. Check: Verify Search Tips Paragraph displayed
        /// 7. click on Parallel Search Tips close button
        /// 8. Check: Verify Search Tips Title not displayed upon clicking on close button
        /// 9. click on Parallel Search Tips button
        /// 10. Check: Verify Search Tips Title displayed
        /// 11. click on Parallel Search Tips cross button
        /// 12. Check: Verify Search Tips Title not displayed upon clicking on cross button
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryParallelSearch)]
        public void ParallelSearchTipsTest()
        {
            const string ExpectedSearchTipsTitle = "Tips for using Parallel Search";
            const string ExpectedSearchTipsHeader = "Enter a sentence or legal concept to find cases with conceptually similar sentences.";
            const string ExpectedSearchTipsParagraph = "Write your query as a full sentence of between 5 and 30 words. Query length has an impact on the length of results returned, and a short phrase may not produce useful results. A query that’s a lengthy paragraph can also be problematic.";

            var parallelSearchPage = this.GetHomePage<PrecisionHomePage>().Header.ParallelSearchLink.Click<ParallelSearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(ParallelSearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(ParallelSearchTab);

            parallelSearchPage.QueryBox.SearchTipsButton.Click();

            ParallelSearchTipsComponent SearchTipsModal = parallelSearchPage.QueryBox.SearchTips;

            string SearchTipsTitle = SearchTipsModal.GetTitleElement().Text;
            this.TestCaseVerify.IsTrue(
                "Verify search tips title is displayed after clicking on search tips button",
                SearchTipsTitle.Equals(ExpectedSearchTipsTitle),
                "Search tips title not displayed after clicking on search tips button");

            string SearchTipsHeader = SearchTipsModal.HeaderLabel.Text;
            this.TestCaseVerify.IsTrue(
                "Verify search tips header is displayed",
                SearchTipsHeader.Equals(ExpectedSearchTipsHeader),
                "Search tips header not displayed");

            string SearchTipsParagraph = SearchTipsModal.ParagraphLabel.Text;
            this.TestCaseVerify.IsTrue(
                "Verify search tips paragraph is displayed",
                SearchTipsParagraph.Equals(ExpectedSearchTipsParagraph),
                "Search tips paragraph not displayed");

            SearchTipsModal.TipsCloseButton.Click();
            this.TestCaseVerify.IsFalse(
                "Verify search tips title not displayed upon clicking close button",
                SearchTipsModal.GetTitleElement().Displayed,
                "Search tips title displayed upon clicking close button");

            parallelSearchPage.QueryBox.SearchTipsButton.Click();
            this.TestCaseVerify.IsTrue(
                "Verify search tips title displayed after clicking on search tip button",
                SearchTipsModal.GetTitleElement().Displayed,
                "Search tips title not displayed after clicking on search tip button");

            SearchTipsModal.SelectXButton();
            this.TestCaseVerify.IsFalse(
                "Verify search tips title not displayed upon clicking cross button",
                SearchTipsModal.GetTitleElement().Displayed,
                "Search tips title displayed upon clicking cross button");
        }

        /// <summary>
        /// Test Parallel Search page opens, checkbox, highlight, pinpointing validation
        /// (Stories: 2091170, 2099310 Test Cases:2102755, 2108443).
        /// 1. Sign in WL Precision with Parallel Search access
        /// 2. Click Parallel Search link next to global search
        /// 3. Submit search: Possession of drugs is inadmissible in a DWI prosecution under rule 403
        /// 5. Check: Verify search results are displayed
        /// 6. Click on select all checkbox
        /// 7. Check: selected All count displayed correctly
        /// 8. uncheck first checkbox
        /// 9. Check: selected All count displayed correctly, should be decreased by 1
        /// 10. check: Verify highlighted text displayed
        /// 11. click on highlighted text link
        /// 12. Check: Verify clicking highlighted text link opens to document page
        /// 13. Check: Verify pinpointing(Green arrow) is displayed
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryParallelSearch)]
        public void ParallelSearchCheckBoxHighlightPinpointTest()
        {
            const string SearchQuery = "Possession of drugs is inadmissible in a DWI prosecution under rule 403";

            var parallelSearchPage = this.GetHomePage<PrecisionHomePage>().Header.ParallelSearchLink.Click<ParallelSearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(ParallelSearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(ParallelSearchTab);

            parallelSearchPage.QueryBox.EnterSearchQuery(SearchQuery);
            parallelSearchPage.QueryBox.SubmitSearchQuery();
            SafeMethodExecutor.WaitUntil(() => !parallelSearchPage.ProgressRingLabel.Displayed);

            parallelSearchPage.Results.SelectAllCheckbox();
            var resultCount = parallelSearchPage.Results.CasesItems.Count;

            this.TestCaseVerify.IsTrue(
            "Verify selected items count is displayed. Selected count: " + resultCount,
               parallelSearchPage.Results.SelectedLabel.Text.Equals(resultCount + " selected"),
               "Selected items count is not displayed. Selected count: " + resultCount);

            parallelSearchPage.Results.ClickFirstCheckbox();
            this.TestCaseVerify.IsTrue(
            "Verify selected items count is displayed. Selected count: " + (resultCount - 1),
               parallelSearchPage.Results.SelectedLabel.Text.Equals((resultCount - 1) + " selected"),
               "Selected items count is not displayed. Selected count: " + (resultCount - 1));

            this.TestCaseVerify.IsTrue(
                "Verify text is highlighted in result items",
                parallelSearchPage.Results.HighlightedTextLink.Displayed,
                "text is not highlighted in result items");

            var documentPage = parallelSearchPage.Results.HighlightedTextLink.Click<EdgeCommonDocumentPage>();

            this.TestCaseVerify.IsTrue(
               "Verify pinpoint green arrow is displayed after clicking on passage link",
               documentPage.IsBestPortionArrowDisplayed(),
               "pinpoint green arrow is not displayed after clicking on passage link");
        }

        /// <summary>
        /// Test Parallel Search page opens, warning Msg, loading page validation
        /// (Stories: 2099306, 2093334 Test Cases:2102285, 2112464).
        /// 1. Sign in WL Precision with Parallel Search access
        /// 2. Click Parallel Search link next to global search
        /// 3. Submit Search with empty query
        /// 4. Check: Verify warning message displayed
        /// 5. Submit Search with Invalid query
        /// 6. Check: Verify warning message displayed
        /// 7. clear the searched query and Submit search: Possession of drugs is inadmissible in a DWI prosecution under rule 403
        /// 8. Check: Verify loading page displayed 
        /// 9. Click on juridiction button
        /// 10.Click on clear all button on juridiction dialog and save it
        /// 11.Check: Verify error message displayed
        /// 12.Select juridictions and save it
        /// 13.clear the searched query and Submit search, wait untill results are displayed
        /// 14. Check: Verify result items have selected juridictions
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryParallelSearch)]
        public void ParallelSearchLoadingPageAndJurisdictionTest()
        {
            const string SearchQuery = "Possession of drugs is inadmissible in a DWI prosecution under rule 403";
            const string ExpectedWarningMsg = "Enter a sentence or legal concept to find cases with conceptually similar sentences.";
            const string ExpectedMsg = "Finding cases with conceptually similar sentences...";
            const string ExpectedJuridictionErrorMsg = "Please select at least one jurisdiction.";

            var parallelSearchPage = this.GetHomePage<PrecisionHomePage>().Header.ParallelSearchLink.Click<ParallelSearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(ParallelSearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(ParallelSearchTab);

            parallelSearchPage.QueryBox.SubmitSearchQuery();
            string WarningMsg = parallelSearchPage.WarningMsgLabel.Text;
            this.TestCaseVerify.IsTrue(
                "Verify warning message is displayed after selecting the search button without entering a query",
                WarningMsg.Equals(ExpectedWarningMsg),
                "Warning message not displayed after selecting the search button without entering a query");
            BrowserPool.CurrentBrowser.Refresh();

            parallelSearchPage.QueryBox.EnterSearchQuery("#$%");
            parallelSearchPage.QueryBox.SubmitSearchQuery();
            WarningMsg = parallelSearchPage.WarningMsgLabel.Text;
            this.TestCaseVerify.IsTrue(
                "Verify warning message is displayed after selecting search button with an invalid query",
                WarningMsg.Equals(ExpectedWarningMsg),
                "Warning message not displayed after selecting search button with an invalid query");

            parallelSearchPage.QueryBox.ClearButton.Click();
            parallelSearchPage.QueryBox.EnterSearchQuery(SearchQuery);
            parallelSearchPage.QueryBox.SubmitSearchQuery();
            string LoadingInfoMsg = parallelSearchPage.ProgressRingLabel.Text;
            this.TestCaseVerify.IsTrue(
                "Verify info message is displayed upon clicking on search button",
                LoadingInfoMsg.Equals(ExpectedMsg),
                "Info message not displayed upon clicking on search button");

            var aiJurisdictionDialog = parallelSearchPage.QueryBox.JurisdictionButton.Click<AiJurisdictionDialog>();
            aiJurisdictionDialog.ClearAllButton.Click();
            aiJurisdictionDialog.SaveButton.Click();
            string JuridictionErrorMsg = aiJurisdictionDialog.ErrorMessageLabel.Text;
            this.TestCaseVerify.IsTrue(
                "Verify error message displayed when juridiction not selected",
                aiJurisdictionDialog.ErrorMessageLabel.Text.Equals(ExpectedJuridictionErrorMsg),
                "Error message not displayed when juridiction not selected");
            aiJurisdictionDialog.SelectJurisdictions(true, Jurisdiction.California, Jurisdiction.NewJersey).SaveButton.Click<ParallelSearchPage>();
            parallelSearchPage.QueryBox.ClearButton.Click();
            parallelSearchPage.QueryBox.EnterSearchQuery(SearchQuery);
            parallelSearchPage.QueryBox.SubmitSearchQuery();
            SafeMethodExecutor.WaitUntil(() => !parallelSearchPage.ProgressRingLabel.Displayed);

            var resultCount = parallelSearchPage.Results.CasesItems.Count;
            string Juridiction = parallelSearchPage.Results.CasesItems[new Random().Next(0, resultCount - 1)].MetadataJurisLabel.Text;
            this.TestCaseVerify.IsTrue(
                "Verify selected juridiction displayed in result items",
                Juridiction.Contains("California") | Juridiction.Contains("New Jersey"),
                "Selected juridiction not displayed in result items");
        }

        /// <summary>
        /// Test Parallel Search page opens, foldering validation
        /// (Stories: 2091174 Test Cases:2110759).
        /// 1. Sign in WL Precision with Parallel Search access
        /// 2. Go to Folders page and clear all foldered items in root folder
        /// 3. Click Parallel Search link next to global search
        /// 4. Submit search: Possession of drugs is inadmissible in a DWI prosecution under rule 403
        /// 5. Wait untill results are displayed
        /// 6. Click on checkbox for first result item
        /// 7. Click Folder button and select root folder to save the result
        /// 8. Check: Verify foldering Saved to message displayed successfully
        /// 9. Click Folder button and select root folder to save the result again
        /// 10.Check: Verify foldering cannot add duplicate message       
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryParallelSearch)]
        public void ParallelSearchFolderSelectedDocumentTest()
        {
            const string SearchQuery = "Possession of drugs is inadmissible in a DWI prosecution under rule 403";

            var folderPage = EdgeNavigationManager.Instance.GoToFoldersPage<EdgeResearchOrganizerPage>();
            folderPage.ClearFolderGrid();

            var parallelSearchPage = this.GetHomePage<PrecisionHomePage>().Header.ParallelSearchLink.Click<ParallelSearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(ParallelSearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(ParallelSearchTab);

            parallelSearchPage.QueryBox.EnterSearchQuery(SearchQuery);
            parallelSearchPage.QueryBox.SubmitSearchQuery();
            SafeMethodExecutor.WaitUntil(() => !parallelSearchPage.ProgressRingLabel.Displayed);

            parallelSearchPage.Results.ClickFirstCheckbox();
            var FirstDocTitle = parallelSearchPage.Results.CasesItems.First().DocumentTitleLink.Text;
            var ToolbarObject = parallelSearchPage.Results.Toolbar;
            var saveToFolderDialog = ToolbarObject.SaveToFolderButton.Click<EdgeSaveToFolderDialog>();
            string rootFolder = saveToFolderDialog.FolderTreeComponent.GetRootFolderName();
            saveToFolderDialog.FolderTreeComponent.SelectFolderByName(rootFolder);

            saveToFolderDialog.ClickSaveButton<ParallelSearchPage>();
            string folderMessage = ToolbarObject.FolderMessageLabel.Text;
            this.TestCaseVerify.IsTrue(
                "Verify foldering saved to message displayed successfully",
                folderMessage.Contains(FirstDocTitle) && folderMessage.Contains("saved to"),
                "Saved to message not displayed");

            saveToFolderDialog = ToolbarObject.SaveToFolderButton.Click<EdgeSaveToFolderDialog>();
            rootFolder = saveToFolderDialog.FolderTreeComponent.GetRootFolderName();
            saveToFolderDialog.FolderTreeComponent.SelectFolderByName(rootFolder);

            saveToFolderDialog.ClickSaveButton<AiAssistedResearchPage>();
            folderMessage = ToolbarObject.FolderMessageLabel.Text;
            this.TestCaseVerify.IsTrue(
                "Verify foldering cannot add duplicate message",
                folderMessage.Contains(FirstDocTitle) && folderMessage.Contains("Cannot add duplicates"),
                "foldering added duplicate message");
        }

        /// <summary>
        /// Test Parallel Search page opens, history validation
        /// (Stories: 2091152 Test Cases:2108014, 2105969).
        /// 1. Sign in WL Precision with Parallel Search access
        /// 2. Click Parallel Search from key feature section
        /// 3. Check: Verify Parallel Search page opens on new tab
        /// 4. Click on juridiction button, select juridiction and save it
        /// 5. Submit search: Possession of drugs is inadmissible in a DWI prosecution under rule 403
        /// 6. Wait untill results are displayed
        /// 7. Click on history tab and view all 
        /// 8. Click on list view in history page and get the first item details
        /// 9. Check: Verify Content, Search Type and Juridiction displayed correctly
        /// 10. Click on the link Title
        /// 11. Check: Verify it navigates to Parallel Search result page and Query is populated
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryParallelSearch)]
        public void ParallelSearchRenderParallelSearchHistoryTest()
        {
            const string ParallelSearchLabel = "Parallel Search";
            const string SearchQuery = "Possession of drugs is inadmissible in a DWI prosecution under rule 403";
            const string ExpectedContent = "Cases";
            const string ExpectedSearchType = "Parallel Search";

            var parallelSearchPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(ParallelSearchLabel).Click<ParallelSearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(ParallelSearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(ParallelSearchTab);

            this.TestCaseVerify.IsTrue(
                "Verify parallel search from feature section opens Parallel Search page on new tab",
                parallelSearchPage.PageHeader.Displayed,
                "parallel search from feature section does not open Parallel Search page on new tab");

            var aiJurisdictionDialog = parallelSearchPage.QueryBox.JurisdictionButton.Click<AiJurisdictionDialog>();
            aiJurisdictionDialog.SelectJurisdictions(true, Jurisdiction.California).SaveButton.Click<ParallelSearchPage>();

            parallelSearchPage.QueryBox.EnterSearchQuery(SearchQuery);
            parallelSearchPage.QueryBox.SubmitSearchQuery();
            SafeMethodExecutor.WaitUntil(() => !parallelSearchPage.ProgressRingLabel.Displayed);

            var historyPage = EdgeNavigationManager.Instance.GoToHistoryPage<EdgeCommonHistoryPage>();
            var ListItems = historyPage.HistoryTabPanel.SetActiveTab<CurrentHistoryTabComponent>(HistoryTabs.ListView);
            var Firstitem = historyPage.HistoryTable.GetGridItems().First();

            this.TestCaseVerify.IsTrue(
                "Verify content displayed as cases",
                Firstitem.EntryContent.Equals(ExpectedContent),
                "content not displayed as cases");

            this.TestCaseVerify.IsTrue(
                "Verify search type displayed as parallel search",
                Firstitem.SearchType.Equals(ExpectedSearchType),
                "search type not displayed as parallel search");

            this.TestCaseVerify.IsTrue(
                "Verify jurdiction displayed matches the juridiction selected for parallel search",
                Firstitem.Jurisdiction.Equals(Jurisdiction.California.ToString()),
                "jurdiction displayed does not match the juridiction selected for parallel search");

            Firstitem.ClickLinkByText<ParallelSearchPage>(SearchQuery);
            SafeMethodExecutor.WaitUntil(() => !parallelSearchPage.ProgressRingLabel.Displayed);
            var query = parallelSearchPage.QueryBox.SearchQueryLabel.GetAttribute("current-value");

            this.TestCaseVerify.IsTrue(
                "Verify query displayed matches the searched query for parallel search",
                query.Equals(SearchQuery),
                "query displayed does not match the searched query for parallel search");
        }

        /// <summary>
        /// Test delivery of Parallel Search results (Story: 2091172 Test Case: 2115309).
        /// 1. Sign in WL Precision with Parallel Search access
        /// 2. Click Parallel Search link next to global search
        /// 3. Submit search: Possession of drugs is inadmissible in a DWI prosecution under rule 403
        /// 4. Download results in PDF format with cover page
        /// 5. Read downloaded content
        /// 6. Check: Verify delivery contains search query
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryParallelSearch)]
        public void ParallelSearchDeliveryTest()
        {
            const string SearchQuery = "Possession of drugs is inadmissible in a DWI prosecution under rule 403";

            var parallelSearchPage = this.GetHomePage<PrecisionHomePage>().Header.ParallelSearchLink.Click<ParallelSearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(ParallelSearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(ParallelSearchTab);

            parallelSearchPage = parallelSearchPage.Header.CompartmentDropdown.ArrowButton.Click<ParallelSearchPage>();
            var productName = parallelSearchPage.Header.CompartmentDropdown.SelectedOptionLink.Text;

            parallelSearchPage.QueryBox.EnterSearchQuery(SearchQuery);
            parallelSearchPage.QueryBox.SubmitSearchQuery();
            SafeMethodExecutor.WaitUntil(() => !parallelSearchPage.ProgressRingLabel.Displayed);

            //Download search results with cover page
            var downloadDialog = parallelSearchPage.Results.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog.TheBasicsTab.WhatToDeliver.SelectOption(WhatToDeliver.ListOfItems);
            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);
            downloadDialog.LayoutAndLimitsTab.SetIncludeSectionOption(LayoutAndLimitsInclude.CoverPage);
            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<AiAssistedResearchPage>();
            var fileName = $"{productName} - Parallel Search results - {DateTime.Now.ToString(DeliveryDateFormat)}.pdf";
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            var text = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, fileName));

            this.TestCaseVerify.IsTrue(
                "Verify delivery contains search query",
                text.Contains($"Query:  {SearchQuery}"),
                "Delivery does not contain search query");
        }

        /// <summary>
        /// Test viewed PUI and KeyCite flag on Parallel Search results
        /// (Stories:2091168 2091149 Test Cases:2112421 2105929).
        /// 1. Sign in WL Precision with Parallel Search access
        /// 2. Click Parallel Search link next to global search
        /// 3. Submit search: Possession of drugs is inadmissible in a DWI prosecution under rule 403
        /// 4. Click to view first document and vavigate back to results
        /// 5. Check: Verify viewed PUI displayed on viewed document
        /// 6. Click the first KeyCite flag link
        /// 7. Check: Verify clicking KeyCite flag takes to document's Negative Treatment tab
        /// 8. Click Return button on document page
        /// 9. Check: Verify clicking return button on Negative Treatment returns to Parallel Search results
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryParallelSearch)]
        public void ParallelSearchKeyCiteFlagAndViewedPUITest()
        {
            const string SearchQuery = "Possession of drugs is inadmissible in a DWI prosecution under rule 403";

            var parallelSearchPage = this.GetHomePage<PrecisionHomePage>().Header.ParallelSearchLink.Click<ParallelSearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(ParallelSearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(ParallelSearchTab);

            parallelSearchPage.QueryBox.EnterSearchQuery(SearchQuery);
            parallelSearchPage.QueryBox.SubmitSearchQuery();
            SafeMethodExecutor.WaitUntil(() => !parallelSearchPage.ProgressRingLabel.Displayed);

            var documentPage = parallelSearchPage.Results.CasesItems.First().DocumentTitleLink.Click<EdgeCommonDocumentPage>();
            parallelSearchPage = documentPage.FixedHeader.ClickReturnToListButton<ParallelSearchPage>();
            SafeMethodExecutor.WaitUntil(() => !parallelSearchPage.ProgressRingLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                "Verify viewed PUI displayed on viewed document",
                parallelSearchPage.Results.CasesItems.First().ViewedPUIButton.Displayed,
                "Viewed PUI not displayed");

            parallelSearchPage.Results.CasesItems.First(doc => doc.KeyCiteFlagLink.Displayed).KeyCiteFlagLink.ScrollToElement();
            var negativeTreatmentPage = parallelSearchPage.Results.CasesItems.First(doc => doc.KeyCiteFlagLink.Displayed).KeyCiteFlagLink.Click<EdgeNegativeTreatmentPage>();

            this.TestCaseVerify.IsTrue(
                "Verify clicking KeyCite flag takes to document's Negative Treatment tab",
                negativeTreatmentPage.RiTabs.GetSelectedTab().Equals(RiTab.NegativeTreatment)
                || negativeTreatmentPage.RiTabs.GetSelectedTab().Equals(RiTab.History),
                "Clicking KeyCite flag does not take to document's Negative Treatment tab");

            parallelSearchPage = negativeTreatmentPage.FixedHeader.ClickReturnToListButton<ParallelSearchPage>();
            SafeMethodExecutor.WaitUntil(() => !parallelSearchPage.ProgressRingLabel.Displayed);
            parallelSearchPage.ScrollPageToTop();

            this.TestCaseVerify.IsTrue(
                "Verify clicking return button on Negative Treatment returns to Parallel Search results",
                parallelSearchPage.Results.ResultCountLabel.Text.Contains("Cases ("),
                "Clicking return button on Negative Treatmen does not return to Parallel Search results");
        }

        /// <summary>
        /// Test Parallel Search doc navigation and stemming highlighting 
        /// (Stories:2121247 2131953 2132541 Tasks: 2135799 2135152 Bugs:2134801 2134710 2134705).
        /// 1. Sign in WL Precision with IAC-PARALLEL-SEARCH-PHASE2
        /// 2. Access Parallel Search page and run search:
        /// 3. Query = The label shall not be false or misleading.
        /// 4. Click first result title link
        /// 5. Check: Verify Doc navigation widget is present from clicking result title link
        /// 6. Check: Verify stemming works: labeling is highlighted because query contains label
        /// 7. Click Next Document button to go to next document
        /// 8. Check: Verify Doc navigation widget is present after clicking Next Document
        /// 9. Check: Verify Return to listt button is present after clicking Next Document 
        /// 10.Click Return to list button
        /// 11.Check: Verify User is taken to result page from next document
        /// 12.Click first result passage link
        /// 13.Check: Verify Doc navigation widget is present from clicking result passage link
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryParallelSearch)]
        [TestCategory("ParallelSearchPhase2")]
        public void ParallelSearchDocNavigationAndHighlightTest()
        {
            const string SearchQuery = "The label shall not be false or misleading.";

            var parallelSearchPage = this.GetHomePage<PrecisionHomePage>().Header.ParallelSearchLink.Click<ParallelSearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(ParallelSearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(ParallelSearchTab);

            parallelSearchPage.QueryBox.EnterSearchQuery(SearchQuery);
            parallelSearchPage.QueryBox.SubmitSearchQuery();
            SafeMethodExecutor.WaitUntil(() => !parallelSearchPage.ProgressRingLabel.Displayed);

            var documentPage = parallelSearchPage.Results.CasesItems.First().DocumentTitleLink.Click<EdgeCommonDocumentPage>();
            this.TestCaseVerify.IsTrue(
               "Verify Doc navigation widget is present from clicking result title link",
               documentPage.Toolbar.NavigationComponent.IsDisplayed(),
               "Doc navigation widget is not present from clicking result title link");

            this.TestCaseVerify.IsTrue(
               "Verify stemming works: labeling is highlighted because query contains label",
               documentPage.HighlightedTerms.Select(item => item.Text).ToList().Any(item => item.Equals("labeling")),
               "Stemming does not work: labeling should be highlighted because query contains label");

            documentPage = documentPage.Toolbar.NavigationComponent.ClickNextDocumentButton<EdgeCommonDocumentPage>();
            this.TestCaseVerify.IsTrue(
               "Verify Doc navigation widget is present after clicking Next Document",
               documentPage.Toolbar.NavigationComponent.IsDisplayed(),
               "Doc navigation widget is not present after clicking Next Document");

            this.TestCaseVerify.IsTrue(
               "Verify Return to list button is present after clicking Next Document",
               documentPage.FixedHeader.IsReturnToListButtonDisplayed(),
               "Return to list button is not present after clicking Next Document");

            parallelSearchPage = documentPage.FixedHeader.ClickReturnToListButton<ParallelSearchPage>();
            SafeMethodExecutor.WaitUntil(() => !parallelSearchPage.ProgressRingLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                "Verify User is taken to result page from next document",
                parallelSearchPage.Results.ResultCountLabel.Text.Contains("Cases ("),
                "User is not taken to result page from next document");

            documentPage = parallelSearchPage.Results.CasesItems.First().DocumentPassageLink.Click<EdgeCommonDocumentPage>();
            this.TestCaseVerify.IsTrue(
               "Verify Doc navigation widget is present from clicking result passage link",
               documentPage.Toolbar.NavigationComponent.IsDisplayed(),
               "Doc navigation widget is not present from clicking result passage link");
        }

        /// <summary>
        /// Test Parallel Search date range filter (Story: 2144853 Task: 2144899, 2160580).
        /// 1. Sign in WL Precision with Parallel Search access
        /// 2. Click Parallel Search link next to global search
        /// 3. Submit search: What is the difference between signing in individual capacity versus signing on behalf of someone else?
        /// 4. Wait until results are displayed
        /// 5. Select “Date range” option in the date facet under filter 
        /// 6. Enter start date, end date and click on done.
        /// 7. Check: Verify that “No results found” page is displayed.
        /// 8. Click ’Clear all filters’ button.
        /// 9. Check: Verify that result results are displayed.
        /// 10. Check: Verify that filter is set to its default state.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryParallelSearch)]
        public void ParallelSearchDateRangeZeroResultsTest()
        {
            const string SearchQuery = "What is the difference between signing in individual capacity versus signing on behalf of someone else?";
            const string StartDate = "1937-04-20";
            const string EndDate = "1937-08-23";
            const string NoResultFound = "No results found";
            const string ClearAllFilters = "Clear all filters";

            var parallelSearchPage = this.GetHomePage<PrecisionHomePage>().Header.ParallelSearchLink.Click<ParallelSearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(ParallelSearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(ParallelSearchTab);

            parallelSearchPage.QueryBox.EnterSearchQuery(SearchQuery);
            parallelSearchPage.QueryBox.SubmitSearchQuery();
            SafeMethodExecutor.WaitUntil(() => !parallelSearchPage.ProgressRingLabel.Displayed);

            var resultCount = parallelSearchPage.Results.CasesItems.Count;

            var dateFacet = parallelSearchPage.Results.FilterFacet.DateFacet;
            dateFacet.ClickDateRangeRadioButton();
            dateFacet.EnterStartDate(StartDate);
            dateFacet.EnterEndDate(EndDate);
            dateFacet.DoneButton.Click<ParallelSearchPage>();

            this.TestCaseVerify.IsTrue(
               "Verify 'No results found' message is displayed",
               parallelSearchPage.Results.NoResultFoundLabel.Text.Equals(NoResultFound),
               "'No results found' message is not displayed");

            this.TestCaseVerify.IsTrue(
               "Verify 'Clear all filters' message is displayed",
               parallelSearchPage.Results.ClearAllFiltersButton.Text.Equals(ClearAllFilters),
               "'Clear all filters' message is not displayed");

            parallelSearchPage.Results.ClearAllFiltersButton.Click();

            this.TestCaseVerify.IsTrue(
               "Verify date filter is set to its default state.",
               dateFacet.DateAllRadio.GetAttribute("current-checked").Equals("true"),
               "date filter is not set to its default state.");

            this.TestCaseVerify.IsTrue(
               "Verify all search results are displayed",
               parallelSearchPage.Results.ResultCountLabel.Text.Contains("Cases (" + resultCount + ")"),
               "All search results are not displayed. Result count: " + resultCount);
        }

        /// <summary>
        /// Test Parallel Search date range filter (Story: 2131595 Task: 2146706).
        /// 1. Sign in WL Precision with Parallel Search access
        /// 2. Click Parallel Search link next to global search
        /// 3. Submit search: What is the difference between signing in individual capacity versus signing on behalf of someone else?
        /// 4. Wait until results are displayed
        /// 5. Check: Verify Search within results filter displayed
        /// 6. Enter invalid search term and click on search button
        /// 7.Check: Verify error message is displayed
        /// 8. Enter valid search term and click on search button
        /// 9. Check: Verify purple highlighting is present in results
        /// 10. Check: Verify Go to snippet<N> links dispalyed
        /// 11. Click on Go to snippet link
        /// 12. Check: Verify document is displayed and 'All Terms' option selected in navigation widget
        /// 13. Select "Search Within" option from dropdown in the navigation widget and click on next arrow.
        /// 14. Check: Verify that next passage has terms with purple highlight
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryParallelSearch)]
        public void ParallelSearchSearchWithinResultsTest()
        {
            const string SearchQuery = "What is the difference between signing in individual capacity versus signing on behalf of someone else?";
            const string SearchWithinResults = "Search within results";
            const string GoToSnippet1 = "Go to snippet 1";
            const string SearchError = "Your query could not be processed.";

            var parallelSearchPage = this.GetHomePage<PrecisionHomePage>().Header.ParallelSearchLink.Click<ParallelSearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(ParallelSearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(ParallelSearchTab);

            parallelSearchPage.QueryBox.EnterSearchQuery(SearchQuery);
            parallelSearchPage.QueryBox.SubmitSearchQuery();
            SafeMethodExecutor.WaitUntil(() => !parallelSearchPage.ProgressRingLabel.Displayed);

            var resultItemDocTitle = parallelSearchPage.Results.CasesItems.First().DocumentTitleLink.Text;
           
            var searchWithinResultsFacet = parallelSearchPage.Results.FilterFacet.SearchWithinResultsFacet;
            var searchWithinResultsLabel = searchWithinResultsFacet.GetSearchWithinResultsLabel();
            this.TestCaseVerify.IsTrue(
               "Verify 'Search within results' filter is displayed",
               searchWithinResultsLabel.Equals(SearchWithinResults),
               "'Search within results' filter is not displayed");

            searchWithinResultsFacet.EnterSearchWithinResultTerm("# "+ resultItemDocTitle);
            searchWithinResultsFacet.ClickSearchButton();
            this.TestCaseVerify.IsTrue(
               "Verify error message is displayed for invalid search term",
               searchWithinResultsFacet.SearchWithinError.Text.Equals(SearchError),
               "Error message not displayed for invalid search term");

            searchWithinResultsFacet.EnterSearchWithinResultTerm(resultItemDocTitle, true);
            searchWithinResultsFacet.ClickSearchButton();
            this.TestCaseVerify.IsTrue(
               "Verify purple highlighting is present in results",
               parallelSearchPage.Results.PurpleHighlightedLabels.First().Displayed,
               "Purple highlighting is not present in results");

            var goToSnippet1= parallelSearchPage.Results.GoToSnippet1LinkElement();            
            this.TestCaseVerify.IsTrue(
               "Verify go to snippet link is present in results",
               goToSnippet1.Text.Equals(GoToSnippet1),
               "Go to snippet link is not present in results");
            goToSnippet1.JavascriptClick();

            var documentPage = new PrecisionCommonDocumentPage();
            documentPage.Toolbar.TermNavigation.ClickToExpandTermNavigationDropdown();
            documentPage.Toolbar.TermNavigation.ClickTermNavigationOption(SearchTermNavigationOption.SearchWithin);
            documentPage.Toolbar.TermNavigation.ClickNextTermArrowButton<EdgeCommonDocumentPage>();
            string purpleText = documentPage.PurpleHighlightedTerms.First().Text;
            bool isPurpleHighlighted = resultItemDocTitle.ToLower().Contains(purpleText.ToLower());
            this.TestCaseVerify.IsTrue(
                "Verify that doc contains purple highlighting for search within terms",
                isPurpleHighlighted,
                "Doc doesn't contain purple highlighting for search within terms. Highlighted Term: " + purpleText + " Searched Term: " + resultItemDocTitle);
        }

        /// <summary>
        /// Test Parallel Search jurisdictional filter (Story: 2131596 Task: 2135313).
        /// 1. Sign in WL Precision with Parallel Search access
        /// 2. Click Parallel Search link next to global search
        /// 3. Submit search: "What constitutes a personnel file?"
        /// 4. Wait until results are displayed
        /// 5. Select “Federal” option in the jurisdictional filter 
        /// 6. Check: federal filter is applied and the number of results matches the count displayed next to the Federal(x) filter
        /// 7. Click first result tilte link and navigate to document page
        /// 8. Click Return button on document page
        /// 9. Check: Verify clicking return button on document returns to Parallel search results with filter continues to be applied
        /// 10. Click Federal option again and deselect the checkbox
        /// 11. Check: Verify if search result count is returned back to 25
        /// 12. Click on first checkbox under "State" filter
        /// 13. Check: Verify that applied jurisdiction is present in the metadata of the results
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryParallelSearch)]
        public void ParallelSearchJurisdictionFilterTest()
        {
            const string SearchQuery = "What constitutes a personnel file?";

            var parallelSearchPage = this.GetHomePage<PrecisionHomePage>().Header.ParallelSearchLink.Click<ParallelSearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(ParallelSearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(ParallelSearchTab);

            var aiJurisdictionDialog = parallelSearchPage.QueryBox.JurisdictionButton.Click<AiJurisdictionDialog>();
            aiJurisdictionDialog.ClearAllButton.Click();
            parallelSearchPage = aiJurisdictionDialog.SelectDefaultJurisdiction().SaveButton.Click<ParallelSearchPage>();
            parallelSearchPage.QueryBox.EnterSearchQuery(SearchQuery);
            parallelSearchPage.QueryBox.SubmitSearchQuery();
            SafeMethodExecutor.WaitUntil(() => !parallelSearchPage.ProgressRingLabel.Displayed);

            var defaultResultCount = parallelSearchPage.Results.CasesItems.Count;
            var jurisdictionFacet = parallelSearchPage.Results.FilterFacet.JurisdictionFacet;
            jurisdictionFacet.ClickFederalCheckbox();

            var resultCountWithFilterApplied = jurisdictionFacet.FederalCount();

            this.TestCaseVerify.IsTrue(
               "Verify result count with Federal filter applied is equal to the count displaying next to Federal under Jurisdiction facet",
               parallelSearchPage.Results.ResultCountLabel.Text.Contains("Cases " + resultCountWithFilterApplied),
               "Result count with Federal filter applied does not match the count displaying next to Federal under Jurisdiction facet . Result count: " + resultCountWithFilterApplied);

            var documentPage = parallelSearchPage.Results.CasesItems.First().DocumentTitleLink.Click<EdgeCommonDocumentPage>();
            parallelSearchPage = documentPage.FixedHeader.ClickReturnToListButton<ParallelSearchPage>();

            this.TestCaseVerify.IsTrue(
                "Verify User is taken to result page with federal filter applied",
                parallelSearchPage.Results.ResultCountLabel.Text.Contains("Cases " + resultCountWithFilterApplied),
               "Search results does not match federal result count. Result count: " + resultCountWithFilterApplied);

            jurisdictionFacet.ClickFederalCheckbox();

            this.TestCaseVerify.IsTrue(
                "Verify Federal checbox is unselected and results return back to default count",
                parallelSearchPage.Results.ResultCountLabel.Text.Contains("Cases (" + defaultResultCount + ")"),
                "Federal checkbox is not unselected. Result count: " + defaultResultCount);

            string stateFilter = jurisdictionFacet.ClickFirstStateCheckbox();
            string juridiction = parallelSearchPage.Results.CasesItems.First().MetadataJurisLabel.Text;

            this.TestCaseVerify.IsTrue(
                "Verify selected juridiction is displayed in result items",
                juridiction.Contains(stateFilter),
                "Selected juridiction is not displayed in result items");
        }
    }
}
