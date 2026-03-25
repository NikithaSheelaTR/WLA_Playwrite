namespace WestlawPrecision.Tests.LitigationAnalytics.LawFirmTests
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsHomePageComponents.Entities;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.SubCharts.TransactionStatus;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.Tabs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.LitigationAnalytics;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Execution;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    [TestClass]
    public class LawFirmTests : BaseAnalyticsTest
    {
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void LawFirmsCompareTest()
        {
            const string Query = "New York";
            const string DeliveriedDocumentTitleVerify = "Verify that Compare works correctly";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var lawFirmsTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsLawFirmsEntityTabComponent>(LitigationAnalyticsProfiles.LawFirms);
            lawFirmsTabComponent.CompareRadiobutton.Select();
            var resultListPage = lawFirmsTabComponent.EnterSearchQueryAndClickSearch<CompareResultListPage>(Query);
            resultListPage.CompareResultComponent.SetCheckboxByIndex(0, true);
            resultListPage.CompareResultComponent.SetCheckboxByIndex(1, true);

            this.TestCaseVerify.AreEqual(
                DeliveriedDocumentTitleVerify, "",
                resultListPage.CompareResultComponent.ResultListItems.ElementAtOrDefault(3).InputCheckBox.GetAttribute("disabled"),
                    "Compare doesn't work correctly");

            DriverExtensions.ScrollPageToBottom();
            resultListPage.MySelectionsComponent.ClearAllSelectionsButton.Click();

            this.TestCaseVerify.AreEqual(
                DeliveriedDocumentTitleVerify, "Select two firms to create your report",
                resultListPage.MySelectionsComponent.SelectTwoFirmsMessage.Text,
                    "Analytics Page doesn't open");

            resultListPage.CompareResultComponent.SetCheckboxByIndex(0, true);
            resultListPage.CompareResultComponent.SetCheckboxByIndex(1, true);
            DriverExtensions.ScrollPageToBottom();

            var analyticsProfilerPage = resultListPage.MySelectionsComponent.CreateReportButton.Click<LitigationAnalyticsProfilerPage>();

            this.TestCaseVerify.IsTrue(
                DeliveriedDocumentTitleVerify,
                analyticsProfilerPage.IsProfilerPage(),
                    "Analytics Page doesn't open");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void LawFirmsCompareTrendChartByPracticeAreaTest()
        {
            const string DeliveriedDocumentTitleVerify = "Verify that Compare works correctly";
            const string Query = "New York";
            const string TrendChartByPracticeAreaVerify = "Verify that Compare Trend chart by practice area works correct";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var lawFirmsTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsLawFirmsEntityTabComponent>(LitigationAnalyticsProfiles.LawFirms);
            lawFirmsTabComponent.CompareRadiobutton.Select();
            var resultListPage = lawFirmsTabComponent.EnterSearchQueryAndClickSearch<CompareResultListPage>(Query);
            resultListPage.CompareResultComponent.SetCheckboxByIndex(0, true);
            resultListPage.CompareResultComponent.SetCheckboxByIndex(1, true);

            this.TestCaseVerify.AreEqual(
                DeliveriedDocumentTitleVerify, "",
                resultListPage.CompareResultComponent.ResultListItems.ElementAtOrDefault(3).InputCheckBox.GetAttribute("disabled"),
                    "Compare doesn't work correctly");

            DriverExtensions.ScrollPageToBottom();
            resultListPage.MySelectionsComponent.ClearAllSelectionsButton.Click();

            this.TestCaseVerify.AreEqual(
                DeliveriedDocumentTitleVerify, "Select two firms to create your report",
                resultListPage.MySelectionsComponent.SelectTwoFirmsMessage.Text,
                    "Analytics Page doesn't open");

            resultListPage.CompareResultComponent.SetCheckboxByIndex(0, true);
            resultListPage.CompareResultComponent.SetCheckboxByIndex(1, true);
            DriverExtensions.ScrollPageToBottom();

            var analyticsProfilerPage = resultListPage.MySelectionsComponent.CreateReportButton.Click<LitigationAnalyticsProfilerPage>();

            this.TestCaseVerify.IsTrue(
                DeliveriedDocumentTitleVerify,
                analyticsProfilerPage.IsProfilerPage(),
                    "Analytics Page doesn't open");

            var experienceTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            var trendChartComponent = experienceTab.ContentViewTabPanel.HeaderTabPanel.SetActiveTab<LitigationAnalyticsBaseContentChartComponent>(LitigationAnalyticsChartHeaderTab.CaseType).ChartContainer.TrendChartByPracticeArea;
            var chartName = trendChartComponent.LineChartHeader.Text;
            trendChartComponent.ForwardButton.Click();

            this.TestCaseVerify.AreNotSame(
                               TrendChartByPracticeAreaVerify, chartName,
                               trendChartComponent.LineChartHeader.Text,
                               "Trend chart by practice area doesn't work correct");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void LawFirmsFilteringTest()
        {
            const string Query = "New York";
            const string FacetQuery = "tech";
            const string FilteringVerify = "Verify that filtering on Court Entity works correctely";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var lawFirmsTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsLawFirmsEntityTabComponent>(LitigationAnalyticsProfiles.LawFirms);
            lawFirmsTabComponent.ByNameRadiobutton.Select();
            var typeahead = lawFirmsTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            var outcomestab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<OutcomesProfileTabComponent>(ProfileComponentTab.Outcomes);
            SafeMethodExecutor.WaitUntil(() => !outcomestab.LoadingSpinner.Displayed, 20);
            
            if (outcomestab.NarrowPane.ApplyButton.Displayed)
            {
                outcomestab.NarrowPane.SelectMultipleFiltersToggle.ToggleState(true);
            }

            var partyFacet = outcomestab.NarrowPane.SearchFacets.SelectFacet(LitigationAnalyticsFacets.Party);
            partyFacet.EnterSearchQuery(FacetQuery);
            SafeMethodExecutor.WaitUntil(() => partyFacet.FacetResultItems.Any(), 20);
            partyFacet.FacetResultItems.First().SearchFacetCheckbox.ScrollToElement();
            partyFacet.FacetResultItems.First().SearchFacetCheckbox.Set(true);
            partyFacet.FacetTitleButton.Click();

            this.TestCaseVerify.AreEqual(
            FilteringVerify,
            outcomestab.ResultListComponent.GetDocketsCount(),
            int.Parse(outcomestab.NarrowPane.SearchFacets.FacetResultItems(LitigationAnalyticsFacets.Party).First().SearchFacetOutputTextValue.Text),
            "Verify that filtering on Court Entity works correctely");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void LawFirmsCompareChartFilteringTest()
        {
            const string FacetQuery = "com";
            const string Query = "Shearman";
            const string FilteringVerify = "Verify that filter on Law Firms Entity applyes on Deals tab";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var lawFirmsTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsLawFirmsEntityTabComponent>(LitigationAnalyticsProfiles.LawFirms);
            lawFirmsTabComponent.CompareRadiobutton.Select();
            var resultListPage = lawFirmsTabComponent.EnterSearchQueryAndClickSearch<CompareResultListPage>(Query);
            resultListPage.CompareResultComponent.SetCheckboxByIndex(0, true);
            resultListPage.CompareResultComponent.SetCheckboxByIndex(1, true);
            DriverExtensions.ScrollPageToBottom();

            var analyticsProfilerPage = resultListPage.MySelectionsComponent.CreateReportButton.Click<LitigationAnalyticsProfilerPage>();
            var dealsTabTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<DealsProfileTabComponent>(ProfileComponentTab.Deals);
            SafeMethodExecutor.WaitUntil(() => !dealsTabTab.LoadingSpinner.Displayed, 20);
            
            if (dealsTabTab.NarrowPane.ApplyButton.Displayed)
            {
                dealsTabTab.NarrowPane.SelectMultipleFiltersToggle.ToggleState(true);
            }

            var transactionStatusFacet = dealsTabTab.NarrowPane.SearchFacets.SelectFacet(LitigationAnalyticsFacets.TransactionStatus);          
            transactionStatusFacet.EnterSearchQuery(FacetQuery);
            var facetItem = transactionStatusFacet.FacetResultItems.First();
            facetItem.SearchFacetCheckbox.ScrollToElement();
            facetItem.SearchFacetCheckbox.Set(true);
            transactionStatusFacet.FacetTitleButton.Click();

            this.TestCaseVerify.AreEqual(
            FilteringVerify,
            dealsTabTab.ResultListComponent.GetDocketsCount(),
            int.Parse(dealsTabTab.NarrowPane.SearchFacets.FacetResultItems(LitigationAnalyticsFacets.TransactionStatus).First().SearchFacetOutputTextValue.Text),
            "Verify that filter on Law Firms Entity doesn't  apply on Deals tab");

        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(SmokeTestCategory)]
        public void LawFirmsDealsFilteringTest()
        {
            const string Query = "Shearman & Sterling LLP";
            const string FacetQuery = "com";
            const string FilteringVerify = "Verify that filtering on Law Firms Entity works correctely on Deals tab";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var lawFirmsTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsLawFirmsEntityTabComponent>(LitigationAnalyticsProfiles.LawFirms);
            lawFirmsTabComponent.ByNameRadiobutton.Select();
            var typeahead = lawFirmsTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            var dealsTabTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<DealsProfileTabComponent>(ProfileComponentTab.Deals);
            SafeMethodExecutor.WaitUntil(() => !dealsTabTab.LoadingSpinner.Displayed, 20);
            
            if (dealsTabTab.NarrowPane.ApplyButton.Displayed)
            {
                dealsTabTab.NarrowPane.SelectMultipleFiltersToggle.ToggleState(true);
            }

            var transactionStatusFacet = dealsTabTab.NarrowPane.SearchFacets.SelectFacet(LitigationAnalyticsFacets.TransactionStatus);
            transactionStatusFacet.EnterSearchQuery(FacetQuery);
            var facetItem = transactionStatusFacet.FacetResultItems.First();
            facetItem.SearchFacetCheckbox.ScrollToElement();
            facetItem.SearchFacetCheckbox.Set(true);
            transactionStatusFacet.FacetTitleButton.Click();

            this.TestCaseVerify.AreEqual(
            FilteringVerify,
            dealsTabTab.ResultListComponent.GetDocketsCount(),
            int.Parse(dealsTabTab.NarrowPane.SearchFacets.FacetResultItems(LitigationAnalyticsFacets.TransactionStatus).First().SearchFacetOutputTextValue.Text),
            "Verify that filtering on Law Firms Entity doesn't work correctely on Deals tab");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(SmokeTestCategory)]
        public void LawFirmsOutcomesChartFilteringTest()
        {
            const string Query = "Shearman & Sterling LLP";
            const string ChartsFilteringVerify = "Verify that filtering on Law Firms Entity works correctely on Outcomes tab";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var lawFirmsTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsLawFirmsEntityTabComponent>(LitigationAnalyticsProfiles.LawFirms);
            lawFirmsTabComponent.ByNameRadiobutton.Select();
            var typeahead = lawFirmsTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            var dealsTabTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<OutcomesProfileTabComponent>(ProfileComponentTab.Outcomes);
            var participantsTab = dealsTabTab.ContentViewTabPanel.HeaderTabPanel.SetActiveTab<CaseTypeTabComponent>(LitigationAnalyticsChartHeaderTab.CaseType);

            int docketCountBeforeFiltering = dealsTabTab.ResultListComponent.GetDocketsCount();
            dealsTabTab.ContentViewTabPanel.ChartViewContainer.ChartsContent.ChartContentItemsList.First().StackedButtonChartClick();

            int docketCountAfterFiltering = dealsTabTab.ResultListComponent.GetDocketsCount();

            this.TestCaseVerify.AreNotSame(
            ChartsFilteringVerify,
            docketCountBeforeFiltering, docketCountAfterFiltering,
            "Charts filtering on Company Entity is not working correctely");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(SmokeTestCategory)]
        public void LawFirmsDealsChartFilteringTest()
        {
            const string Query = "Shearman & Sterling LLP";
            const string ChartsFilteringVerify = "Verify that filtering on Law Firms Entity works correctely on Deals tab";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var lawFirmsTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsLawFirmsEntityTabComponent>(LitigationAnalyticsProfiles.LawFirms);
            lawFirmsTabComponent.ByNameRadiobutton.Select();
            var typeahead = lawFirmsTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            var dealsTabTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<DealsProfileTabComponent>(ProfileComponentTab.Deals);
            var participantsTab = dealsTabTab.ContentViewTabPanel.HeaderTabPanel.SetActiveTab<TransactionStatusTabComponent>(LitigationAnalyticsChartHeaderTab.TransactionStatus);
            participantsTab.TransactionStatusSubChartPanel.SetActiveTab<CountTrendSubChartTab>(LitigationAnalyticsContainerSubcategories.CountTrend);

            int docketCountBeforeFiltering = dealsTabTab.ResultListComponent.GetDocketsCount();
            dealsTabTab.ContentViewTabPanel.ChartViewContainer.ChartsContent.ChartContentItemsList.First().StackedButtonChartClick();

            int docketCountAfterFiltering = dealsTabTab.ResultListComponent.GetDocketsCount();

            this.TestCaseVerify.AreNotSame(
            ChartsFilteringVerify,
            docketCountBeforeFiltering, docketCountAfterFiltering,
            "Charts filtering on Company Entity is not working correctely");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void LawFirmsExperienceTabShowFullTableTest()
        {
            const string Query = "Shearman & Sterling LLP";
            const string FullTableViewVerify = "Verify that Law Firms - Experience tab Show full table view link is opened new tab with full table";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var lawFirmsTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsLawFirmsEntityTabComponent>(LitigationAnalyticsProfiles.LawFirms);
            lawFirmsTabComponent.ByNameRadiobutton.Select();
            var typeahead = lawFirmsTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            var experienceTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);

            var caseTypeTab = experienceTab.ContentViewTabPanel.HeaderTabPanel.SetActiveTab<CaseTypeTabComponent>(LitigationAnalyticsChartHeaderTab.CaseType);
            int tableRowCount = experienceTab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.TableContentItemsList.Count;
            var fullTableViewExperiencetab = experienceTab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.ClickOnShowFullTable<LitigationAnalyticsProfilerPage>();

            SafeMethodExecutor.WaitUntil(() => fullTableViewExperiencetab.ProfileTabPanel.IsDisplayed(ProfileComponentTab.Experience), 50);
            string currentTabName = fullTableViewExperiencetab.ProfileTabPanel.CurrentTabName;
            var tableRowCountOnFullTableViewTab = fullTableViewExperiencetab.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience)
                .ContentViewTabPanel.ChartViewContainer.TableContentComponent.TableContentItemsList.Count;

            this.TestCaseVerify.IsTrue(
            FullTableViewVerify,
            tableRowCount != tableRowCountOnFullTableViewTab && currentTabName.Equals("Experience"),
            $"Verify that Law Firms - Experience tab Show full table view link isn't opened new tab with full table. Table row count on LA page {tableRowCount}, count on full table page {tableRowCountOnFullTableViewTab}");

            string tabName = BrowserPool.CurrentBrowser.TabHandles.Last();
            BrowserPool.CurrentBrowser.CreateTab(tabName);
            BrowserPool.CurrentBrowser.CloseTab(tabName);
            BrowserPool.CurrentBrowser.ActivateTab(tabName);

            var dealsTabTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<DealsProfileTabComponent>(ProfileComponentTab.Deals);
            var considerationOfferedTab = dealsTabTab.ContentViewTabPanel.HeaderTabPanel.SetActiveTab<ConsiderationOfferedTabComponent>(LitigationAnalyticsChartHeaderTab.ConsiderationOffered);
            considerationOfferedTab.TransactionStatusSubChartPanel.SetActiveTab<CountTrendSubChartTab>(LitigationAnalyticsContainerSubcategories.CountTrend);
            int dealsTableRowCount = dealsTabTab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.TableContentItemsList.Count;

            var fullTableViewOutcomesTab = dealsTabTab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.ClickOnShowFullTable<LitigationAnalyticsProfilerPage>();

            SafeMethodExecutor.WaitUntil(() => fullTableViewOutcomesTab.ProfileTabPanel.IsDisplayed(ProfileComponentTab.Deals), 50);
            string dealsCurrentTabName = fullTableViewOutcomesTab.ProfileTabPanel.CurrentTabName;
            var dealsTableRowCountOnFullTableViewTab = fullTableViewOutcomesTab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.TableContentItemsList.Count;

            this.TestCaseVerify.IsTrue(
            FullTableViewVerify,
            dealsTableRowCount != dealsTableRowCountOnFullTableViewTab && dealsCurrentTabName.Equals("Deals"),
            $"Verify that Law Firms - Deals tab Show full table view link isn't opened new tab with full table.  Table row count on LA page {tableRowCount}, count on full table page {tableRowCountOnFullTableViewTab}");
            
        }              

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void LawFirmsCascadeAttorneyFilteringTest()
        {
            const string Query = "New York";
            const string FilteringVerify = "Verify that cascading Attorney filter on LawFirms Entity works correctely";
            const string FacetQuery = "Darian Alexander";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var lawFirmsTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsLawFirmsEntityTabComponent>(LitigationAnalyticsProfiles.LawFirms);
            lawFirmsTabComponent.ByNameRadiobutton.Select();
            var typeahead = lawFirmsTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);

            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            var outcomesTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<OutcomesProfileTabComponent>(ProfileComponentTab.Outcomes);
            SafeMethodExecutor.WaitUntil(() => !outcomesTab.LoadingSpinner.Displayed, 20);

            if (outcomesTab.NarrowPane.ApplyButton.Displayed)
            {
                outcomesTab.NarrowPane.SelectMultipleFiltersToggle.ToggleState(true);
            }
            var attorneyStatusFacet = outcomesTab.NarrowPane.SearchFacets.CascadingFacetButton(LitigationAnalyticsCascadingFacets.Attorney).Click<LitigationAnalyticsFacetDialog>();
            attorneyStatusFacet.NameSubTabButton.Click();
            var facetItem = attorneyStatusFacet.FacetResultItems.First(item => item.SearchFacetLabelText.Text.Equals(FacetQuery));
            facetItem.SearchFacetCheckbox.ScrollToElement();
            facetItem.SearchFacetCheckbox.Set(true);

            var motionsTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<MotionsProfileTabComponent>(ProfileComponentTab.Motions);

            this.TestCaseVerify.IsTrue(
              FilteringVerify,
              motionsTab.NarrowPane.SearchFacets.CascadingFacetResultItems(LitigationAnalyticsCascadingFacets.Attorney).First().SearchFacetCheckbox.Selected,
             "Verify that LawFirms cascading filter on Attorneys -> Motions works correctely");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void StackByYearLawFirmTabTest()
        {
            const string Query = "Amazon";
            string checkStackByYearFunctionWorkWithCharts = "Verify: Stack By Year is displayed and enabled with Charts";
            string checkStackByYearFunctionWorkWithTable = "Verify: Stack By Year is displayed and enabled with Charts";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var lawFirmsTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsLawFirmsEntityTabComponent>(LitigationAnalyticsProfiles.LawFirms);
            lawFirmsTabComponent.ByNameRadiobutton.Select();
            var typeahead = lawFirmsTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            var experienceTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            SafeMethodExecutor.WaitUntil(() => !experienceTab.LoadingSpinner.Displayed, 20);
            var legend = experienceTab.ContentViewTabPanel.ChartViewContainer.Legend.LegendColorsList[0];
            var chartItem = experienceTab.ContentViewTabPanel.ChartViewContainer.ChartsContent.ChartContentItemsList.First();

            this.TestCaseVerify.IsTrue(
                    checkStackByYearFunctionWorkWithCharts,
                    chartItem.ColorsList().Contains(legend),
                    "StackByYear is not enabled with Charts ");

            this.TestCaseVerify.IsTrue(
                checkStackByYearFunctionWorkWithTable,
                experienceTab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.TableHeaderTitles.Count > 2,
                "StackByYear is not enabled with Tables ");
        }
    }
}