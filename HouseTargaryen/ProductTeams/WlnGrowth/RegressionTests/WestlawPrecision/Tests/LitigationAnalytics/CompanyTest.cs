namespace WestlawPrecision.Tests.LitigationAnalytics
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsHomePageComponents.Entities;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.Tabs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.LitigationAnalytics;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Execution;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    [TestClass]
    public class CompanyTests : BaseAnalyticsTest
    {
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void CompanyChartFilteringTest()
        {
            const string Query = "apple";
            string ChartsFilteringVerify = "Charts filtering on Company Entity is working correctely";

            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();
            var resultListPage = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCompaniesEntityTabComponent>(LitigationAnalyticsProfiles.Companies)
                .EnterSearchQueryAndClickSearch<CompareResultListPage>(Query);
            SafeMethodExecutor.WaitUntil(() => !resultListPage.LoadingSpinner.Displayed, 30);
            resultListPage.CompanySelectionComponent.CompaniesResultListItems.First().InputCheckBox.Set(true);
            DriverExtensions.ScrollPageToBottom();
            var analyticsProfilerPage = resultListPage.MySelectionsComponent.CreateReportButton.Click<LitigationAnalyticsProfilerPage>();
            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 50);
            var experience =  analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            var courtFacet = experience.NarrowPane.SearchFacets.CascadingFacetButton(LitigationAnalyticsCascadingFacets.Court).Click<LitigationAnalyticsFacetDialog>();
            var facetItem = courtFacet.FacetResultItems.First(item => item.SearchFacetLabelText.Text.Equals("Federal"));
            facetItem.SearchFacetCheckbox.ScrollToElement();
            facetItem.SearchFacetCheckbox.Set(true);
            analyticsProfilerPage = new LitigationAnalyticsProfilerPage();
            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var patentsTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<PatentsProfileTabComponent>(ProfileComponentTab.Patents);
            patentsTab.NarrowPane.GrantedButton.Click();

            int grantedpatentsBeforeCount = patentsTab.ResultListComponent.GetDocketsCount();
            patentsTab.ContentViewTabPanel.HeaderTabPanel.SetActiveTab<VolumeTabComponent>(LitigationAnalyticsChartHeaderTab.Volume).ChartContainer.ChartsContent.ChartContentItemsList.First().StackedButtonChartClick();
            int grantedpatentsAfterCount = patentsTab.ResultListComponent.GetDocketsCount();

            this.TestCaseVerify.AreNotSame(
            ChartsFilteringVerify,
            grantedpatentsBeforeCount, grantedpatentsAfterCount,
            "Charts filtering on Company Entity is not working correctely");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void CompanyDealsFilteringTest()
        {
            const string Query = "Disney";
            const string FacetQuery = "Pending";
            string ChartsNavigationButtonVerify = "Charts Navigation button on Deals tab Company Entity is working correctely";
            string ChartsFilteringVerify = "Charts Filtering on Deals tab Company Entity is working correctely";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var resultListPage = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCompaniesEntityTabComponent>(LitigationAnalyticsProfiles.Companies)
                .EnterSearchQueryAndClickSearch<CompareResultListPage>(Query);
            SafeMethodExecutor.WaitUntil(() => !resultListPage.LoadingSpinner.Displayed, 40);
            resultListPage.CompanySelectionComponent.CompaniesResultListItems.First().InputCheckBox.Set(true);
            DriverExtensions.ScrollPageToBottom();
            var dealsTab = resultListPage.MySelectionsComponent.CreateReportButton.Click<LitigationAnalyticsProfilerPage>().ProfileTabPanel
                .SetActiveTab<DealsProfileTabComponent>(ProfileComponentTab.Deals);
            var headerTabPanel = dealsTab.ContentViewTabPanel.HeaderTabPanel;
            var getChartHeaderTabsPanel = headerTabPanel.GetListOfDisplayedTabs;
            headerTabPanel.NextNavigationButton.Click();

            this.TestCaseVerify.AreNotSame(
            ChartsNavigationButtonVerify,
            getChartHeaderTabsPanel, headerTabPanel.GetListOfDisplayedTabs,
            "Charts Navigation button on Deals tab Company Entity is not working correctely");

            SafeMethodExecutor.WaitUntil(() => !dealsTab.LoadingSpinner.Displayed, 30);
            dealsTab.NarrowPane.SelectMultipleFiltersToggle.ScrollToElement();

            if (dealsTab.NarrowPane.ApplyButton.Displayed)
            {
                dealsTab.NarrowPane.SelectMultipleFiltersToggle.ToggleState(true);
            }

            var transactionStatusFacet = dealsTab.NarrowPane.SearchFacets.SelectFacet(LitigationAnalyticsFacets.TransactionStatus);
            transactionStatusFacet.EnterSearchQuery(FacetQuery);
            var facetItem = transactionStatusFacet.FacetResultItems.First();
            facetItem.SearchFacetLabelText.ScrollToElement();
            facetItem.SearchFacetCheckbox.Set(true);
            transactionStatusFacet.FacetTitleButton.Click();

            this.TestCaseVerify.AreEqual(
            ChartsFilteringVerify,
            dealsTab.ResultListComponent.GetDocketsCount(),
            int.Parse(dealsTab.NarrowPane.SearchFacets.FacetResultItems(LitigationAnalyticsFacets.TransactionStatus).First().SearchFacetOutputTextValue.Text),
            "Filtering on Companies Deals Entity doesn't work correctely on Deals tab");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void CompanyPatentsTabSwitchingByCatigoriesTest()
        {
            const string Query = "apple";
            string GrantedSubcategoryVerify = "Verify : Granted subcategory was opened by clicking on Granted button";
            string AssignmentsSubcategoryVerify = "Verify : Assignments subcategory was opened by clicking on Assignments button";
            string ApplicationSubcategoryVerify = "Verify : Application subcategory was opened by clicking on Applications button";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var resultListPage = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCompaniesEntityTabComponent>(LitigationAnalyticsProfiles.Companies)
                .EnterSearchQueryAndClickSearch<CompareResultListPage>(Query);
            resultListPage.CompanySelectionComponent.CompaniesResultListItems.First().InputCheckBox.Set(true);
            DriverExtensions.ScrollPageToBottom();
            var patentsTab = resultListPage.MySelectionsComponent.CreateReportButton.Click<LitigationAnalyticsProfilerPage>().ProfileTabPanel.SetActiveTab<DealsProfileTabComponent>(ProfileComponentTab.Patents);
            patentsTab.NarrowPane.GrantedButton.Click();

            this.TestCaseVerify.IsTrue(
            GrantedSubcategoryVerify,
             patentsTab.ContentViewTabPanel.TitleToolBarComponent.ContentTitle.Contains("granted"),
             "Granted subcategory was not opened by clicking on Granted button");

            patentsTab.NarrowPane.ApplicationsdButton.Click();

            this.TestCaseVerify.IsTrue(
            ApplicationSubcategoryVerify,
            patentsTab.ContentViewTabPanel.TitleToolBarComponent.ContentTitle.Contains("application"),
            "Application subcategory was not opened by clicking on Application button");

            patentsTab.NarrowPane.AssignmentsButton.Click();

            this.TestCaseVerify.IsTrue(
            AssignmentsSubcategoryVerify,
             patentsTab.ContentViewTabPanel.TitleToolBarComponent.ContentTitle.Contains("assignment"),
             "Assignments subcategory was not opened by clicking on Assignments button");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void CompanyExperienceTabShowFullTableTest()
        {
            const string Query = "apple";
            const string FullTableViewVerify = "Verify that Company - Experience tab Show full table view link is opened new tab with full table";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var resultListPage = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCompaniesEntityTabComponent>(LitigationAnalyticsProfiles.Companies)
                .EnterSearchQueryAndClickSearch<CompareResultListPage>(Query);
            resultListPage.CompanySelectionComponent.CompaniesResultListItems.First().InputCheckBox.Set(true);
            DriverExtensions.ScrollPageToBottom();
            SafeMethodExecutor.WaitUntil(() => !resultListPage.LoadingSpinner.Displayed, 40);
            var experienceTab = resultListPage.MySelectionsComponent.CreateReportButton.Click<LitigationAnalyticsProfilerPage>().ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);

            SafeMethodExecutor.WaitUntil(() => !experienceTab.LoadingSpinner.Displayed, 40);
            var caseTypeTab = experienceTab.ContentViewTabPanel.HeaderTabPanel.SetActiveTab<CaseTypeTabComponent>(LitigationAnalyticsChartHeaderTab.CaseType);
            int tableRowCount = experienceTab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.TableContentItemsList.Count;
            var fullTableViewExperiencetab = experienceTab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.ClickOnShowFullTable<LitigationAnalyticsProfilerPage>();

            SafeMethodExecutor.WaitUntil(() => fullTableViewExperiencetab.ProfileTabPanel.IsDisplayed(ProfileComponentTab.Experience), 40);
            string currentTabName = fullTableViewExperiencetab.ProfileTabPanel.CurrentTabName;
            var tableRowCountOnFullTableViewTab = fullTableViewExperiencetab.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience)
                .ContentViewTabPanel.ChartViewContainer.TableContentComponent.TableContentItemsList.Count;

            this.TestCaseVerify.IsTrue(
            FullTableViewVerify,
            tableRowCount != tableRowCountOnFullTableViewTab && currentTabName.Equals("Experience"),
            $" Company - Experience tab Show full table view link isn't opened new tab with full table. Table row count on LA page {tableRowCount}, count on full table page {tableRowCountOnFullTableViewTab}");       
        }
    }
}