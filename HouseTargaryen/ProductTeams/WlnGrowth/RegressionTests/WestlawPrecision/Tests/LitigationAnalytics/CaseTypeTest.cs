namespace WestlawPrecision.Tests.LitigationAnalytics
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsHomePageComponents.Entities;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.Tabs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.LitigationAnalytics;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.Utils.Execution;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    [TestClass]
    public class CaseTypeTest : BaseAnalyticsTest
    {
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void CaseTypeChartFilteringTest()
        {
            const string Query = "Civil Rights";
            string ChartsFilteringVerify = "Charts filtering on Case Type Entity is working correctely";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var casestypeTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCaseTypeEntityTabComponent>(LitigationAnalyticsProfiles.CaseTypes);
            var typeahead = casestypeTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var experience = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            var courtFacet = experience.NarrowPane.SearchFacets.CascadingFacetButton(LitigationAnalyticsCascadingFacets.Court).Click<LitigationAnalyticsFacetDialog>();
            var facetItem = courtFacet.FacetResultItems.First(item => item.SearchFacetLabelText.Text.Equals("Federal"));
            facetItem.SearchFacetCheckbox.ScrollToElement();
            facetItem.SearchFacetCheckbox.Set(true);

            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var motionsTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<MotionsProfileTabComponent>(ProfileComponentTab.Motions);
            var caseTypeTab = motionsTab.ContentViewTabPanel.HeaderTabPanel.SetActiveTab<CaseTypeTabComponent>(LitigationAnalyticsChartHeaderTab.CaseType);

            int motionsordersBeforeCount = motionsTab.ResultListComponent.GetDocketsCount();
            motionsTab.ContentViewTabPanel.ChartViewContainer.ChartsContent.ChartContentItemsList.First().StackedButtonChartClick();
            int motionsordersAfterCount = motionsTab.ResultListComponent.GetDocketsCount();

            this.TestCaseVerify.AreNotSame(
            ChartsFilteringVerify,
            motionsordersBeforeCount, motionsordersAfterCount,
            "Charts filtering on Company Entity is not working correctely");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void CaseTypeCascadeLawfirmFilteringTest()
        {
            const string Query = "Civil Rights";
            const string FilteringVerify = "Verify that cascading Attorney filter on LawFirms Entity works correctely";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var casestypeTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCaseTypeEntityTabComponent>(LitigationAnalyticsProfiles.CaseTypes);
            var typeahead = casestypeTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();

            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var experience = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);

            SafeMethodExecutor.WaitUntil(() => !experience.LoadingSpinner.Displayed, 30);
          
            if (experience.NarrowPane.ApplyButton.Displayed)
            {
                experience.NarrowPane.SelectMultipleFiltersToggle.ToggleState(true);
            }

            var lawfirmStatusFacet = experience.NarrowPane.SearchFacets.CascadingFacetButton(LitigationAnalyticsCascadingFacets.Lawfirm).Click<LitigationAnalyticsFacetDialog>();
            lawfirmStatusFacet.NameSubTabButton.ScrollToElement();
            lawfirmStatusFacet.NameSubTabButton.Click();
            var facetItem = lawfirmStatusFacet.FacetResultItems.First();
            facetItem.SearchFacetCheckbox.ScrollToElement();
            facetItem.SearchFacetCheckbox.Set(true);

            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var outcomesTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<OutcomesProfileTabComponent>(ProfileComponentTab.Outcomes);

            this.TestCaseVerify.IsTrue(
              FilteringVerify,
              outcomesTab.NarrowPane.SearchFacets.CascadingFacetResultItems(LitigationAnalyticsCascadingFacets.Lawfirm).First().SearchFacetCheckbox.Selected,
             "Verify that CaseType cascading filter on LawFirms -> OutComes not working correctely");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void CaseTypeExperienceTabShowFullTableTest()
        {

            const string Query = "Civil Rights";
            const string FullTableViewVerify = "Verify that Case Type - Experience tab Show full table view link is opened new tab with full table";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var casestypeTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCaseTypeEntityTabComponent>(LitigationAnalyticsProfiles.CaseTypes);
            var typeahead = casestypeTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var experienceTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);

            var caseTypeTab = experienceTab.ContentViewTabPanel.HeaderTabPanel.SetActiveTab<CaseTypeTabComponent>(LitigationAnalyticsChartHeaderTab.CaseType);
            int tableRowCount = experienceTab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.TableContentItemsList.Count;
            var fullTableViewExperiencetab = experienceTab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.ClickOnShowFullTable<LitigationAnalyticsProfilerPage>();

            SafeMethodExecutor.WaitUntil(() => !fullTableViewExperiencetab.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            string currentTabName = fullTableViewExperiencetab.ProfileTabPanel.CurrentTabName;
            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var tableRowCountOnFullTableViewTab = fullTableViewExperiencetab.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience)
                .ContentViewTabPanel.ChartViewContainer.TableContentComponent.TableContentItemsList.Count;

            this.TestCaseVerify.IsTrue(
            FullTableViewVerify,
            tableRowCount != tableRowCountOnFullTableViewTab && currentTabName.Equals("Experience"),
            $"Verify that Case Type - Experience tab Show full table view link isn't opened new tab with full table. Table row count on LA page {tableRowCount}, count on full table page {tableRowCountOnFullTableViewTab}");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void CaseTypeChartFilterMultiToggleTest()
        {
            const string Query = "Civil Rights";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var casestypeTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCaseTypeEntityTabComponent>(LitigationAnalyticsProfiles.CaseTypes);
            var typeahead = casestypeTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var experience = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            SafeMethodExecutor.WaitUntil(() => !experience.LoadingSpinner.Displayed, 30);

            experience.NarrowPane.SelectMultipleFiltersToggle.ScrollToElement();
       
            if (experience.NarrowPane.ApplyButton.Displayed)
            {
                experience.NarrowPane.SelectMultipleFiltersToggle.ToggleState(true);
            }

            var experienceDocketsBeforeCount = experience.ResultListComponent.GetDocketsCount();
            var courtFacet = experience.NarrowPane.SearchFacets.CascadingFacetButton(LitigationAnalyticsCascadingFacets.Court).Click<LitigationAnalyticsFacetDialog>();
            var facetItem = courtFacet.FacetResultItems.First(item => item.SearchFacetLabelText.Text.Equals("Federal"));
            facetItem.SearchFacetCheckbox.ScrollToElement();
            facetItem.SearchFacetCheckbox.Set(true);

            int experienceDocketsAfterCount = experience.ResultListComponent.GetDocketsCount();

            this.TestCaseVerify.AreNotSame(
                "Chart filtering by Case Type Entity works correctly in Experience tab when Multiple Filters are Disabled.",
                experienceDocketsBeforeCount, experienceDocketsAfterCount,
                "Chart filtering by Case Type Entity does not work correctly in Experience tab when Multiple Filters are Disabled.");
            BrowserPool.CurrentBrowser.Refresh();            if (!experience.NarrowPane.ApplyButton.Displayed)
            {
                experience.NarrowPane.SelectMultipleFiltersToggle.ToggleState(true);
            }
            
            experienceDocketsBeforeCount = experience.ResultListComponent.GetDocketsCount();

            courtFacet = experience.NarrowPane.SearchFacets.CascadingFacetButton(LitigationAnalyticsCascadingFacets.Court).Click<LitigationAnalyticsFacetDialog>();
            facetItem = courtFacet.FacetResultItems.First(item => item.SearchFacetLabelText.Text.Equals("State"));
            facetItem.SearchFacetCheckbox.ScrollToElement();
            facetItem.SearchFacetCheckbox.Set(true);

            var experienceDocketsAfterFacetChecked = experience.ResultListComponent.GetDocketsCount();

            this.TestCaseVerify.AreEqual(
                "Dockets count should not change before clicking Apply when Multiple Filters are Enabled.",
                experienceDocketsBeforeCount, experienceDocketsAfterFacetChecked,
                "Dockets count changed before Apply was clicked when Multiple Filters are Enabled.");

            experience.NarrowPane.ApplyButton.ScrollToElement();
            experience.NarrowPane.ApplyButton.Click();

            experienceDocketsAfterCount = experience.ResultListComponent.GetDocketsCount();

            this.TestCaseVerify.AreNotSame(
                "Chart filtering by Case Type Entity works correctly in Experience tab when Multiple Filters are Enabled.",
                experienceDocketsBeforeCount, experienceDocketsAfterCount,
                    "Chart filtering by Case Type Entity does not work correctly in Experience tab when Multiple Filters are Enabled.");
        }
    }
}