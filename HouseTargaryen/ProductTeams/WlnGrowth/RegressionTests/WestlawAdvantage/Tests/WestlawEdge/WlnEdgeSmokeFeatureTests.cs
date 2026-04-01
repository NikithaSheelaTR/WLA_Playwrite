namespace WestlawAdvantage.Tests
{
    using Framework.Common.Api.Endpoints;
    using Framework.Common.Api.Enums;
    using Framework.Common.UI.DataModel;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Enums.Facets;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.NewTypeAhead;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.StatutesCompare;
    using Framework.Common.UI.Products.WestlawEdge.Enums.NarrowPanel;
    using Framework.Common.UI.Products.WestlawEdge.Enums.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Pages.Judicial;
    using Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Common.UI.Products.WestLawNext.Utils;
    using Framework.Common.UI.Raw.WestlawEdge.Enums;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Header;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.NewTypeAhead;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Toolbars;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.Document;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.LegalAnalaytics;
    using Framework.Common.UI.Tests;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Security;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.DataModel.Security.Specialized;
    using Framework.Core.QualityChecks.MsUnit;
    using Framework.Core.QualityChecks.Result;
    using Framework.Core.Utils.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// Contains smoke feature tets for WLN Edge
    /// </summary>
    [TestClass]
    public sealed class WlnEdgeSmokeFeatureTests : BaseWebUiRegressionTest
    {
        private const string ExpectedQuery = "What is the fair use doctrine?";

        private readonly Dictionary<string, string> searchOptions = new Dictionary<string, string>
        {
            { "Gorsuch, Neil M", "Cases heard by Gorsuch, Neil M" },
            { "Cutaiar, Trevor M", "Cases argued by Cutaiar, Trevor M" },
            { "Shearman & Sterling", "Cases represented by Shearman & Sterling" },
            { "2015", "Cases before 2015" },
            {
                "equitable conversion/ reconversion/ election of beneficiary",
                "Cases with the Key Number for equitable conversion/ reconversion/ election of beneficiary"
            }
        };

        public WlnEdgeSmokeFeatureTests()
        {
            this.UiExecutionSettings = this.UiExecutionSettings.SetFlag(UiExecutionFlags.AllowUiPreconditionRoutines);
        }

        /// <summary>
        /// Verifies answers of QnAComponent are displayed on teaser (QnA from Type Ahead)
        /// </summary>
        [TestMethod]
        [TestCategory("Indigo Regression")]
        [TestCategory("EdgeSmoke2.0")]
        [TestCategory("EdgeSmokeQnATeaser")]
        public void TeaserViewQnAPrecisionTest()
        {
            const string Query = "What are the elements of trademark infringement?";
            const string CurrentJurisdictions = "All States | 3rd Circuit";

            var answersCountOnTeaser = new QualityCheck("Verify of answers count are displayed on Teaser");
            var displayOfEnteredQuery = new QualityCheck("Verify display of entered query on Teaser");
            var displayOfJurisdiction = new QualityCheck("Verify display of selected jurisdiction on Teaser");

            this.QualityTestCase.AddQualityChecks(answersCountOnTeaser, displayOfEnteredQuery, displayOfJurisdiction);

            var homePage = this.GetHomePage<PrecisionHomePage>();
            homePage = this.CloseHomeTourIfDisplayed(homePage);
            homePage.Header.OpenJurisdictionDialog()
                    .SelectJurisdictions(true, Jurisdiction.AllStates, Jurisdiction.ThirdCircuit)
                    .SaveButton.Click<PrecisionHomePage>();
            var searchResultPage = homePage.Header.EnterSearchQueryAndClickSearch<PrecisionCommonSearchResultPage>(Query)
                                           .NarrowPane.ContentType
                                           .ClickContentTypeLink<PrecisionCommonSearchResultPage>(ContentType.Cases);

            int answerCount = searchResultPage.QnAComponent.QnAResultList.GetAnswersCount();

            QualityVerify.IsTrue(answersCountOnTeaser, answerCount <= 2);
            QualityVerify.IsTrue(
                displayOfEnteredQuery,
                searchResultPage.QnAComponent.GetQuestionText().Equals(Query));

            string jurisdictionOnTeaser = searchResultPage.QnAComponent.GetAnswersJurisdictionText();

            QualityVerify.IsTrue(
                displayOfJurisdiction,
                jurisdictionOnTeaser.ToLower().Equals(CurrentJurisdictions.ToLower()));
        }

        /// <summary>
        /// Verifies answer selection from typeahead navigates to the teaser page (QnA from Type Ahead)
        /// </summary>
        [TestMethod]
        [TestCategory("Indigo Regression")]
        [TestCategory("EdgeSmoke2.0")]
        [TestCategory("EdgeSmokeQnAAnswer")]
        public void AnswerDisplayQnAPrecisionTest()
        {
            const string Query = "What is fair use for copyright purposes?";

            var bringToTeaserBySelectingTypeaheadQuestion =
                new QualityCheck("Verify selecting typeahead question bring to the teaser page");
            var displayTenAnswers = new QualityCheck("Verify display up to ten answers");
            var displayOfEnteredQuestion = new QualityCheck("Verify display of entered question");
            var displayOfSelectedJurisdiction = new QualityCheck("Verify display of selected jurisdiction");

            this.QualityTestCase.AddQualityChecks(
                bringToTeaserBySelectingTypeaheadQuestion,
                displayTenAnswers,
                displayOfEnteredQuestion,
                displayOfSelectedJurisdiction);

            var homePage = this.GetHomePage<PrecisionHomePage>();
            homePage = this.CloseHomeTourIfDisplayed(homePage);
            homePage.Header.OpenJurisdictionDialog().SelectDefaultJurisdiction().SaveButton.Click<PrecisionHomePage>();
            var typeAhead = homePage.Header.EnterSearchQuery<TrdTypeAheadDialog>(Query, true);
            var searchResultPage =
                typeAhead.SuggestionsComponent.ClickOnSuggestionByIndex<PrecisionCommonSearchResultPage>(
                    NewTypeAheadCategories.WestlawAnswers,
                    0);

            QualityVerify.IsTrue(
                bringToTeaserBySelectingTypeaheadQuestion,
                searchResultPage.QnAComponent.IsDisplayed());

            var answerPage = searchResultPage.QnAComponent.ViewMoreButton.Click<EdgeQnAFullTextDocumentPage>();

            int count = answerPage.QnAResultList.GetAnswersCount();
            QualityVerify.IsTrue(displayTenAnswers, count <= 10, "More than TEN answers are displayed");

            string questionOnAnswerPage = answerPage.GetQuestionText();
            QualityVerify.IsTrue(displayOfEnteredQuestion, questionOnAnswerPage.Equals(ExpectedQuery));

            string juris = answerPage.GetJurisdictionForStaticQaText();
            string jurisSelected = answerPage.GetSelectedJurisdiction();

            QualityVerify.IsTrue(displayOfSelectedJurisdiction, juris.Equals(jurisSelected));
        }

        /// <summary>
        /// Verify Analytics answer page is displayed.
        /// </summary>
        [TestMethod]
        [TestCategory("Indigo Regression")]
        [TestCategory("EdgeSmoke2.0")]
        [TestCategory("EdgeSmokeLA")]
        [TestCategory("FedRampIgnore")]
        public void LegalAnalyticsPrecisionTest()
        {
            const string Query = "Schiltz, Patrick J";

            var typeahedContainsSuggestionCheck = new QualityCheck(
                "Verify Typeahead contains suggestions under Litigation Analytics section");
            var analyticsPageOpensWithourErrorsCheck =
                new QualityCheck("Verify Litigation Analytics page opens without errors");
            var chartIsPresentCheck = new QualityCheck("Verify State chart is presented on the page ");

            this.QualityTestCase.AddQualityChecks(
                typeahedContainsSuggestionCheck,
                analyticsPageOpensWithourErrorsCheck,
                chartIsPresentCheck);

            var homePage = this.GetHomePage<PrecisionHomePage>();
            homePage = this.CloseHomeTourIfDisplayed(homePage);
            homePage.Header.OpenJurisdictionDialog().SelectDefaultJurisdiction().SaveButton.Click<PrecisionHomePage>();
            var typeAhead = homePage.Header.EnterSearchQuery<TrdTypeAheadDialog>(Query);

            bool isSuggestionPresent = typeAhead.SuggestionsComponent
                                                .GetSuggestionsByCategoryName(
                                                    NewTypeAheadCategories.LitigationAnalytics)
                                                .Any(x => x.Text.Contains(Query));

            QualityVerify.IsTrue(typeahedContainsSuggestionCheck, isSuggestionPresent);

            var analyticsProfiler = typeAhead.SuggestionsComponent.ClickOnSuggestionByText<EdgeAnalyticsProfilerPage>(
                NewTypeAheadCategories.LitigationAnalytics,
                Query);

            QualityVerify.IsFalse(analyticsPageOpensWithourErrorsCheck, analyticsProfiler.IsErrorPage);
            Thread.Sleep(2000);
            QualityVerify.IsTrue(chartIsPresentCheck, analyticsProfiler.IsChartDisplayed());
        }

        /// <summary>
        /// Verifies Compare versions dialog is opened on document page (Statutes Compare)
        /// </summary>
        [TestMethod]
        [TestCategory("Indigo Regression")]
        [TestCategory("EdgeSmoke2.0")]
        [TestCategory("EdgeSmokeSC")]
        public void ComparsionLightboxDisplayPrecisionTest()
        {
            const string Query = "West's Ann.Cal.Com.Code § 1202";

            var displayCompareVersionsButton =
                new QualityCheck("Verify Compare Versions button is present on document page");
            var documentTitleCheck = new QualityCheck(
                "Verify document title on document page contains the title from the Compare Versions dialog");
            var noErrorsCheck = new QualityCheck("Verify there are no errors on the page");

            this.QualityTestCase.AddQualityChecks(displayCompareVersionsButton, noErrorsCheck, documentTitleCheck);

            var homePage = this.GetHomePage<PrecisionHomePage>();
            homePage = this.CloseHomeTourIfDisplayed(homePage);
            homePage.Header.OpenJurisdictionDialog().SelectDefaultJurisdiction().SaveButton.Click<PrecisionHomePage>();
            var documentPage = homePage.Header.EnterSearchQueryAndClickSearch<PrecisionCommonDocumentPage>(Query);

            string docTitle = documentPage.GetDocumentTitle().Remove(0, 8);

            QualityVerify.IsTrue(
                displayCompareVersionsButton,
                documentPage.Toolbar.IsToolbarElementDisplayed(EdgeToolbarElements.CompareVersions));

            var statuteCompareDialog =
                documentPage.Toolbar.ClickToolbarElement<CompareVersionsDialog>(EdgeToolbarElements.CompareVersions);

            QualityVerify.IsTrue(documentTitleCheck, statuteCompareDialog.GetDocumentTitleText().Contains(docTitle));
            QualityVerify.IsFalse(noErrorsCheck, documentPage.IsErrorPage);
        }

        /// <summary>
        /// Verifies QnA component is displayed after clicking an answer from typeahead (TRD QnA)
        /// </summary>
        [TestMethod]
        [TestCategory("Indigo Regression")]
        [TestCategory("EdgeSmoke2.0")]
        [TestCategory("EdgeSmokeTRD_QnA")]
        public void TrdTypeaheadQnAPrecisionTest()
        {
            const string Query = "power";

            var displayQnA = new QualityCheck("Verify QnA component is displayed");
            var noErrorsCheck = new QualityCheck("Verify there are no errors on the page");

            this.QualityTestCase.AddQualityChecks(displayQnA, noErrorsCheck);

            var homePage = this.GetHomePage<PrecisionHomePage>();
            homePage = this.CloseHomeTourIfDisplayed(homePage);
            homePage.Header.OpenJurisdictionDialog().SelectDefaultJurisdiction().SaveButton.Click<PrecisionHomePage>();
            var typeAhead = homePage.Header.EnterSearchQuery<TrdTypeAheadDialog>(Query);
            var searchResultPage =
                typeAhead.SuggestionsComponent.ClickOnSuggestionByIndex<PrecisionCommonSearchResultPage>(
                    NewTypeAheadCategories.WestlawAnswers,
                    0);

            QualityVerify.IsTrue(displayQnA, searchResultPage.QnAComponent.IsDisplayed());
            QualityVerify.IsFalse(noErrorsCheck, searchResultPage.IsErrorPage);
        }

        /// <summary>
        /// Verifies search content pages category on typeahead (TRD Content Pages)
        /// </summary>
        [TestMethod]
        [TestCategory("Indigo Regression")]
        [TestCategory("EdgeSmoke2.0")]
        [TestCategory("EdgeSmokeTRD_CP")]
        [TestCategory("FedRampWLE1")]
        public void TrdTypeaheadContentPagesCategoryPrecisionTest()
        {
            const string Query = "power";

            var titleCheck = new QualityCheck("Verify title is the same as on the result page");
            var noErrorsCheck = new QualityCheck("Verify there are no errors on the page");

            this.QualityTestCase.AddQualityChecks(titleCheck, noErrorsCheck);

            var homePage = this.GetHomePage<PrecisionHomePage>();
            homePage = this.CloseHomeTourIfDisplayed(homePage);
            homePage.Header.OpenJurisdictionDialog().SelectDefaultJurisdiction().SaveButton.Click<PrecisionHomePage>();
            var typeAhead = homePage.Header.EnterSearchQuery<TrdTypeAheadDialog>(Query);

            string firstSuggestion = typeAhead.SuggestionsComponent
                                              .GetSuggestionsByCategoryName(NewTypeAheadCategories.ContentPages)
                                              .ElementAt(1).Text;

            var searchResultPage = typeAhead.SuggestionsComponent.ClickOnSuggestionByText<PrecisionCommonSearchResultPage>(
                NewTypeAheadCategories.ContentPages,
                firstSuggestion);

            QualityVerify.AreEqual(titleCheck, searchResultPage.GetBrowsePageTitle(), firstSuggestion);
            QualityVerify.IsFalse(noErrorsCheck, searchResultPage.IsErrorPage);
        }

        /// <summary>
        /// Verifies snapshot category on typeahead (TRD Snapshots)
        /// </summary>
        [TestMethod]
        [TestCategory("Indigo Regression")]
        [TestCategory("EdgeSmoke2.0")]
        [TestCategory("EdgeSmokeTRD_S")]
        public void TrdTypeaheadSnapshotCategoryPrecisionTest()
        {
            const string Query = "power";

            var noErrorsCheck = new QualityCheck("Verify there are no errors on the page");

            this.QualityTestCase.AddQualityChecks(noErrorsCheck);

            var homePage = this.GetHomePage<PrecisionHomePage>();
            homePage = this.CloseHomeTourIfDisplayed(homePage);
            homePage.Header.OpenJurisdictionDialog().SelectDefaultJurisdiction().SaveButton.Click<PrecisionHomePage>();
            var typeAhead = homePage.Header.EnterSearchQuery<TrdTypeAheadDialog>(Query);
            var searchResultPage =
                typeAhead.SuggestionsComponent.ClickOnSuggestionByIndex<PrecisionCommonSearchResultPage>(
                    NewTypeAheadCategories.Snapshots,
                    0);

            QualityVerify.IsFalse(noErrorsCheck, searchResultPage.IsErrorPage);
        }

        /// <summary>
        /// Tests TRD Search Results page
        /// </summary>
        [TestMethod]
        [TestCategory("Indigo Regression")]
        [TestCategory("EdgeSmoke2.0")]
        [TestCategory("EdgeSmokeTRD_RP")]
        public void TrdResultsPagePrecisionTest()
        {
            var isSearchResultPageHasErrorCheck = new List<QualityCheck>();
            var areSearchResultPresentCheck = new List<QualityCheck>();

            foreach (KeyValuePair<string, string> option in this.searchOptions)
            {
                var isSearchResultPageHasError =
                    new QualityCheck("Verify there are no errors for the query: " + option.Value);
                var areSearchResultsPresent =
                    new QualityCheck("The search results are present for the query: " + option.Value);

                isSearchResultPageHasErrorCheck.Add(isSearchResultPageHasError);
                areSearchResultPresentCheck.Add(areSearchResultsPresent);

                this.QualityTestCase.AddQualityChecks(isSearchResultPageHasError, areSearchResultsPresent);
            }

            var homePage = this.GetHomePage<PrecisionHomePage>();
            homePage = this.CloseHomeTourIfDisplayed(homePage);
            homePage.Header.OpenJurisdictionDialog().SelectDefaultJurisdiction().SaveButton.Click<PrecisionHomePage>();

            int i = 0;

            foreach (KeyValuePair<string, string> option in this.searchOptions)
            {
                var resultPage =
                    homePage.Header.EnterSearchQueryAndClickSearch<PrecisionCommonSearchResultPage>(option.Value);

                QualityVerify.IsFalse(isSearchResultPageHasErrorCheck[i], resultPage.IsErrorPage);
                QualityVerify.IsTrue(
                    areSearchResultPresentCheck[i],
                    resultPage.ResultList.GetResultDocumentsNames().Count > 0);
                i++;
            }
        }

        /// <summary>
        /// Verifies search facets
        /// </summary>
        [TestMethod]
        [TestCategory("Indigo Regression")]
        [TestCategory("EdgeSmoke2.0")]
        [TestCategory("EdgeSmokeSF")]
        public void SearchFacetsMultiplyFiltersApplyPrecisionTest()
        {
            const string Query = "power";
            const string Jurisdiction = "State";
            const string Date = "Last 3 years";
            const string ReportedStatus = "Reported";

            var resultsCheck = new QualityCheck("Verify there are results found after multiply filters are applied");
            var applyJurisdictionCheck = new QualityCheck("Verify jurisdiction facet is applied");
            var applyDateCheck = new QualityCheck("Verify date facet is applied");
            var applyReportedStatusCheck = new QualityCheck("Verify reported status facet is applied");
            var noErrorsCheck = new QualityCheck("Verify there are no errors on the page");

            this.QualityTestCase.AddQualityChecks(
                resultsCheck,
                applyJurisdictionCheck,
                applyDateCheck,
                applyReportedStatusCheck,
                noErrorsCheck);

            var searchHomePage = this.GetHomePage<PrecisionHomePage>();
            searchHomePage = this.CloseHomeTourIfDisplayed(searchHomePage);
            searchHomePage.Header.OpenJurisdictionDialog().SelectDefaultJurisdiction()
                          .SaveButton.Click<PrecisionHomePage>();

            var searchResultPage = searchHomePage
                                   .Header.EnterSearchQueryAndClickSearch<PrecisionCommonSearchResultPage>(Query).NarrowPane
                                   .ContentType.ClickContentTypeLink<PrecisionCommonSearchResultPage>(ContentType.Cases);

            searchResultPage.NarrowTabPanel.SetActiveTab<FiltersTabComponent>(NarrowTab.Filters).Filter.SelectMultipleFilters();

            searchResultPage.NarrowPane.Filter.JurisdictionFacet.SetCheckboxForFacetByName<PrecisionCommonSearchResultPage>(
                true,
                Jurisdiction);
            searchResultPage.NarrowPane.Filter.DateFacet.ApplyFacet<PrecisionCommonSearchResultPage>(
                EdgeDateRangeOptions.Last3Years);
            searchResultPage.NarrowPane.Filter.ReportedStatusFacet.ApplyFacet<PrecisionCommonSearchResultPage>(
                true,
                Reported.Reported);
            searchResultPage.NarrowPane.Filter.ClickApplyFiltersButton<PrecisionCommonSearchResultPage>();

            QualityVerify.IsTrue(resultsCheck, searchResultPage.ResultList.SearchResultItemsCount > 0);

            QualityVerify.IsTrue(
                applyJurisdictionCheck,
                searchResultPage.NarrowPane.Filter.JurisdictionFacet.IsCheckboxSelected(Jurisdiction));

            searchResultPage.NarrowPane.Filter.DateFacet.ClickDateFacet();

            QualityVerify.IsTrue(
                applyDateCheck,
                searchResultPage.NarrowPane.Filter.DateFacet.GetDateFacetActiveOption().Equals(Date));

            QualityVerify.IsTrue(
                applyReportedStatusCheck,
                searchResultPage.NarrowPane.Filter.ReportedStatusFacet.IsCheckboxSelected(ReportedStatus));

            QualityVerify.IsFalse(noErrorsCheck, searchResultPage.IsErrorPage);
        }

        /// <summary>
        /// Smoke test for regular Quick check (check work option)
        /// TestCase #1415755
        /// </summary>
        [TestMethod]
        [TestCategory("Indigo Regression")]
        [TestCategory("EdgeSmoke2.0")]
        [TestCategory("QuickCheckContentDisplayed")]
        [TestProperty("AllowQuickCheckPreconditions", "true")]
        [DeploymentItem(@"Resources\TestData\QuickCheckTestDocuments")]
        public void QuickCheckCheckYourWorkSmokePrecisionTest()
        {
            const string TestFileName = @"QuickCheckTestFile.docx";
            string testFilePath = Path.Combine(Environment.CurrentDirectory, TestFileName);

            var checkContentDisplayedForRecommendations =
                new QualityCheck("Verify content is displayed for recommendations tab");
            var checkContentDisplayedForWarnings =
                new QualityCheck("Verify content is displayed for warnings for cited authority tab");
            var checkContentDisplayedForTableOfAuthorities =
                new QualityCheck("Verify content is displayed for ToA tab");
            var checkFilterPanelForQuotations =
                new QualityCheck("Verify that the filter panel is displayed for quotations tab.");
            var checkQuotationDisplayedCorrectly = new QualityCheck(
                "Verify that pre-quote, post-quote and quote are displayed for the user and westlaw documents.");
            var checkReportDisplayedOnTheHistoryDialog =
                new QualityCheck("Verify that appropriate document is displayed in history.");

            this.QualityTestCase.AddQualityChecks(
                checkContentDisplayedForRecommendations,
                checkContentDisplayedForWarnings,
                checkContentDisplayedForTableOfAuthorities,
                checkFilterPanelForQuotations,
                checkQuotationDisplayedCorrectly,
                checkReportDisplayedOnTheHistoryDialog);

            this.CloseHomeTourIfDisplayed(this.GetHomePage<PrecisionHomePage>());
            var reportPage = QuickCheckUiManager.UploadFile<QuickCheckRecommendationsPage>(testFilePath);

            QualityVerify.IsTrue(
                checkContentDisplayedForRecommendations,
                !reportPage.ReportTabsPanel.GetRecommendationsTab().ResultList.AllItemsCountLabel.Text.RetrieveCountFromBrackets().Equals(0));
            QualityVerify.IsTrue(
                checkContentDisplayedForWarnings,
                !reportPage.ReportTabsPanel.GetWarningsForCitedAuthorityTab().ResultList.Count.Equals(0));
            QualityVerify.IsTrue(
                checkContentDisplayedForTableOfAuthorities,
                !reportPage.ReportTabsPanel.GetTableOfAuthoritiesTab().ResultList.Count.Equals(0));
            QualityVerify.IsTrue(
                checkFilterPanelForQuotations,
                reportPage.ReportTabsPanel.GetQuotationAnalysisTab().NarrowPane.IsDisplayed());

            QuotationAnalysisItem quotationItem =
                reportPage.ReportTabsPanel.GetQuotationAnalysisTab().ResultList.First();

            QualityVerify.IsTrue(
                checkQuotationDisplayedCorrectly,
                quotationItem.DocumentPreQuoteLink.Displayed && quotationItem.DocumentPostQuoteLink.Displayed
                                                             && quotationItem.DocumentQuoteLink.Displayed
                                                             && quotationItem.QuoteHeaderLink.Displayed);

            var historyWidget = reportPage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);

            QualityVerify.AreEqual(
                checkReportDisplayedOnTheHistoryDialog,
                TestFileName,
                historyWidget.GetItemTitleByGuid("QuickCheck"),
                "file name is not displayed on the history widget");
        }

        /// <summary>
        /// Smoke test for regular Quick check (check work option)
        /// TestCase #1415830
        /// </summary>
        [TestMethod]
        [TestCategory("Indigo Regression")]
        [TestCategory("EdgeSmoke2.0")]
        [TestCategory("QuickCheckOpponentModeContentDisplayed")]
        [TestProperty("AllowQuickCheckPreconditions", "true")]
        [DeploymentItem(@"Resources\TestData\QuickCheckTestDocuments")]
        public void QuickCheckCheckOpponentWorkSmokePrecisionTest()
        {
            const string TestFileName = @"OpponentTestFile.docx";
            string testFilePath = Path.Combine(Environment.CurrentDirectory, TestFileName);

            var checkContentDisplayedForRecommendations =
                new QualityCheck("Verify content is displayed for recommendations tab");
            var checkContentDisplayedForWarnings =
                new QualityCheck("Verify content is displayed for warnings for cited authority tab");
            var checkContentDisplayedForTableOfAuthorities =
                new QualityCheck("Verify content is displayed for ToA tab");
            var checkLexisCitesBadgesDisplayed = new QualityCheck("Verify Lexis cites displayed for ToA tab");
            var checkFilterPanelForQuotations =
                new QualityCheck("Verify that the filter panel is displayed for quotations tab.");
            var checkQuotationDisplayedCorrectly = new QualityCheck(
                "Verify that pre-quote, post-quote and quote are displayed for the user and westlaw documents.");
            var checkReportDisplayedOnTheHistoryDialog =
                new QualityCheck("Verify that appropriate document is displayed in history.");

            this.QualityTestCase.AddQualityChecks(
                checkContentDisplayedForRecommendations,
                checkContentDisplayedForWarnings,
                checkContentDisplayedForTableOfAuthorities,
                checkLexisCitesBadgesDisplayed,
                checkFilterPanelForQuotations,
                checkQuotationDisplayedCorrectly,
                checkReportDisplayedOnTheHistoryDialog);

            this.CloseHomeTourIfDisplayed(this.GetHomePage<PrecisionHomePage>());
            var reportPage = QuickCheckUiManager.UploadFile<QuickCheckRecommendationsPage>(
                testFilePath,
                WhatYouWouldLikeToDoOptions.AnalyzeOpponents);

            QualityVerify.IsTrue(
                checkContentDisplayedForRecommendations,
                !reportPage.ReportTabsPanel.GetRecommendationsTab().ResultList.AllItemsCountLabel.Text.RetrieveCountFromBrackets().Equals(0));
            QualityVerify.IsTrue(
                checkContentDisplayedForWarnings,
                !reportPage.ReportTabsPanel.GetWarningsForCitedAuthorityTab().ResultList.Count.Equals(0));
            QualityVerify.IsTrue(
                checkContentDisplayedForTableOfAuthorities,
                !reportPage.ReportTabsPanel.GetTableOfAuthoritiesTab().ResultList.Count.Equals(0));
            QualityVerify.IsTrue(
                checkLexisCitesBadgesDisplayed,
                reportPage.ReportTabsPanel.GetTableOfAuthoritiesTab().ResultList.Any(el => el.LexisLabel.Displayed));
            QualityVerify.IsTrue(
                checkFilterPanelForQuotations,
                reportPage.ReportTabsPanel.GetQuotationAnalysisTab().NarrowPane.IsDisplayed());

            QuotationAnalysisItem quotationItem =
                reportPage.ReportTabsPanel.GetQuotationAnalysisTab().ResultList.First();

            QualityVerify.IsTrue(
                checkQuotationDisplayedCorrectly,
                quotationItem.DocumentPreQuoteLink.Displayed && quotationItem.DocumentPostQuoteLink.Displayed
                                                             && quotationItem.DocumentQuoteLink.Displayed
                                                             && quotationItem.QuoteHeaderLink.Displayed);

            var historyWidget = reportPage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);

            QualityVerify.AreEqual(
                checkReportDisplayedOnTheHistoryDialog,
                TestFileName,
                historyWidget.GetItemTitleByGuid("QuickCheck"),
                "file name is not displayed on the history widget");
        }

        /// <summary>
        /// Quick Check: Judicial: Smoke test
        /// TestCase #1415834
        /// </summary>
        [TestMethod]
        [TestCategory("Indigo Regression")]
        [TestCategory("EdgeSmoke2.0")]
        [TestCategory("FedRampWLE1")]
        [TestCategory("JudicialContentDisplayed")]
        [TestProperty("AllowQuickCheckPreconditions", "true")]
        [DeploymentItem(@"Resources\TestData\QuickCheckTestDocuments")]
        public void JudicialSmokePrecisionTest()
        {
            const string ExpectedHistoryItemTitle = "Party 1 v Party 2";

            const string Document1 = "JudicialTestFile1.pdf";
            const string Document2 = "JudicialTestFile2.pdf";
            const string Document3 = "JudicialTestFile3.pdf";
            const string Document4 = "JudicialTestFile4.pdf";

            var checkOmittedByBoth = new QualityCheck("Verify: Omitted by both tab have items ");
            var checkOmittedByPartyOne = new QualityCheck("Verify: result list is displayed for party 1");
            var checkOmittedByPartyTwo = new QualityCheck("Verify: result list is displayed for party 2");
            var checkCitedAuthority = new QualityCheck("Verify: result list is displayed for cited authority");
            var checkQuotationsPartyOne = new QualityCheck("Verify: quotations displayed for party one");
            var checkQuotationsPartyTwo = new QualityCheck("Verify: quotations displayed for party two");
            var checkHistoryPage = new QualityCheck("Verify: judicial report is displayed on history page");

            this.QualityTestCase.AddQualityChecks(
                checkOmittedByBoth,
                checkOmittedByPartyOne,
                checkOmittedByPartyTwo,
                checkCitedAuthority,
                checkQuotationsPartyOne,
                checkQuotationsPartyTwo,
                checkHistoryPage);

            var assignment = new Dictionary<string, JudicialParties>
            {
                { Document1, JudicialParties.FirstParty },
                { Document2, JudicialParties.FirstParty },
                { Document3, JudicialParties.SecondParty },
                { Document4, JudicialParties.SecondParty }
            };

            this.CloseHomeTourIfDisplayed(this.GetHomePage<PrecisionHomePage>());
            JudicialRecommendationsPage reportPage =
                QuickCheckUiManager.AssignDocumentsToPartiesAndGetReport(Environment.CurrentDirectory, assignment);

            QualityVerify.IsTrue(
                checkOmittedByBoth,
                reportPage.ReportTabsPanel.GetJudicialRecommendationsTab().PartySwitcher.GetOmittedByBoth().AllCases
                          .Count != 0);

            QualityVerify.IsTrue(
                checkOmittedByPartyOne,
                reportPage.ReportTabsPanel.GetJudicialRecommendationsTab().PartySwitcher.GetFirstParty().AllCases.Count
                != 0);

            QualityVerify.IsTrue(
                checkOmittedByPartyTwo,
                reportPage.ReportTabsPanel.GetJudicialRecommendationsTab().PartySwitcher.GetSecondParty().AllCases.Count
                != 0);

            QualityVerify.IsTrue(
                checkCitedAuthority,
                reportPage.ReportTabsPanel.GetJudicialCitedAuthorityTab().ResultList.Count != 0);

            QualityVerify.IsTrue(
                checkQuotationsPartyOne,
                reportPage.ReportTabsPanel.GetJudicialQuoteAlertsTab().PartySwitcher.GetFirstParty().ResultList.Count
                != 0);

            QualityVerify.IsTrue(
                checkQuotationsPartyTwo,
                reportPage.ReportTabsPanel.GetJudicialQuoteAlertsTab().PartySwitcher.GetSecondParty().ResultList.Count
                != 0);

            var historyDialog = this.GetHomePage<PrecisionHomePage>().Header
                                    .ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);

            QualityVerify.IsTrue(
                checkHistoryPage,
                historyDialog.GetRecentSearchesItems().First(item => item.HistoryItemLink.Text.Contains(ExpectedHistoryItemTitle))
                             .IsClickable);
        }

        protected override void PerformUiPreconditionRoutines()
        {
            var userInfo = this.DefaultSignOnContext.UserInfo as WlnUserInfo;
            if (null != this.TestContext.Properties["AllowQuickCheckPreconditions"]
                && this.TestContext.Properties["AllowQuickCheckPreconditions"].Equals("true"))
            {
                WebsiteManager.SetUserSettings(
                    this.GetUserInfo(),
                    TestConfigurationRepository.DefaultInstance.FindProduct(CobaltProductId.WestlawEdge),
                    this.TestExecutionContext.TestEnvironment,
                    BrowserPool.CurrentBrowser.Driver.GetCookies().GetCookieContainerFromCookies(),
                    PreferenceName.ShowQuickCheckJudicialOverview,
                    "false");

                WebsiteManager.SetPreferences(
                    this.GetUserInfo(),
                    TestConfigurationRepository.DefaultInstance.FindProduct(CobaltProductId.WestlawEdge),
                    this.TestExecutionContext.TestEnvironment,
                    BrowserPool.CurrentBrowser.Driver.GetCookies().GetCookieContainerFromCookies(),
                    VerticalName.Website,
                    PreferenceName.SuppressQuotationFacetsChevronInfo,
                    "true");

                WebsiteManager.SetUserSettings(
                    this.GetUserInfo(),
                    TestConfigurationRepository.DefaultInstance.FindProduct(CobaltProductId.WestlawEdge),
                    this.TestExecutionContext.TestEnvironment,
                    BrowserPool.CurrentBrowser.Driver.GetCookies().GetCookieContainerFromCookies(),
                    PreferenceName.ShowDocAnalyzerOppWorkOverview,
                    "false");

                WebsiteManager.SetUserSettings(
                    this.GetUserInfo(),
                    TestConfigurationRepository.DefaultInstance.FindProduct(CobaltProductId.WestlawEdge),
                    this.TestExecutionContext.TestEnvironment,
                    BrowserPool.CurrentBrowser.Driver.GetCookies().GetCookieContainerFromCookies(),
                    PreferenceName.ShowDocAnalyzerOverview,
                    "false");
            }

            WebsiteManager.SetPreferences(
                userInfo,
                TestConfigurationRepository.DefaultInstance.FindProduct(CobaltProductId.WestlawEdge),
                this.TestExecutionContext.TestEnvironment,
                BrowserPool.CurrentBrowser.Driver.GetCookies().GetCookieContainerFromCookies(),
                VerticalName.Website,
                PreferenceName.ShowFilterPanelRedesignOverview,
                "false");
        }

        /// Retrieve the user(users) for test
        /// </summary>
        protected override void OnManageCredential()
        {
            string passwordPool = "";
            string isFedRamp = this.Settings.GetValue(EnvironmentConstants.IsFedRamp);
            var userCredential = isFedRamp != null && isFedRamp.ToLower().Equals("yes") && !this.TestExecutionContext.TestEnvironment.IsLower
                                            ? passwordPool = "Edge2.0Prod"
                                            : passwordPool = "Edge2.0";

            var credentialSettings = new UserDbCredential(
                this.TestContext,
                PasswordVertical.IndigoRegression,
                passwordPool);
            CredentialPool.RegisterUser(credentialSettings);

            this.DefaultSignOnContext = new WlnSignOnContext<IUserInfo>(this.TestExecutionContext, CredentialPool.GetFirstOrDefaultUser<WlnUserInfo>());
        }

        private PrecisionHomePage CloseHomeTourIfDisplayed(PrecisionHomePage homePage)
        {
            if (homePage.IsHomeTourDisplayed())
            {
                homePage = homePage.TourTheHomepageComponent.RemindMeLaterButton.Click<PrecisionHomePage>();
            }

            return homePage;
        }

        /// <summary>
        /// The get user info.
        /// </summary>
        /// <returns>
        /// The <see cref="WlnUserInfo"/>.
        /// </returns>
        private WlnUserInfo GetUserInfo() => this.DefaultSignOnContext.UserInfo as WlnUserInfo;
    }
}
