namespace WestlawPrecision.Tests.LitigationAnalytics
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsHomePageComponents.Entities;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts;
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
    public class CourtTests : BaseAnalyticsTest
    {
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(SmokeTestCategory)]
        public void CourtFilteringTest()
        {
            const string Query = "United States District Court, N.D. Florida";
            const string FilteringVerify = "Verify that filtering on Court Entity works correctely";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var courtsTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCourtsEntityTabComponent>(LitigationAnalyticsProfiles.Courts);
            var typeahead = courtsTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var expertChallengesTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExpertChallengesTabComponent>(ProfileComponentTab.ExpertChallenges);

            SafeMethodExecutor.WaitUntil(() => !expertChallengesTab.LoadingSpinner.Displayed, 20);

            if (expertChallengesTab.NarrowPane.ApplyButton.Displayed)
            {
                expertChallengesTab.NarrowPane.SelectMultipleFiltersToggle.ToggleState(true);
            }

            var areaOfExperienceFacet = expertChallengesTab.NarrowPane.SearchFacets.SelectFacet(LitigationAnalyticsFacets.AreaOfExpertise);
            areaOfExperienceFacet.NameSubTabButton.Click();
            var facetItem = areaOfExperienceFacet.FacetResultItems.First();
            facetItem.SearchFacetCheckbox.ScrollToElement();
            facetItem.SearchFacetCheckbox.Set(true);
            areaOfExperienceFacet.FacetTitleButton.Click();

            this.TestCaseVerify.AreEqual(
            FilteringVerify,
            expertChallengesTab.ResultListComponent.GetDocketsCount(),
            int.Parse(expertChallengesTab.NarrowPane.SearchFacets.FacetResultItems(LitigationAnalyticsFacets.AreaOfExpertise).First().SearchFacetOutputTextValue.Text),
            "Verify that filtering on Court Entity doesn't work correctely");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void CourtFullTableTest()
        {
            const string Query = "United States District Court, N.D. Florida";
            const string FullTableViewVerify = "Verify that Show full table view link is opened new tab with full table";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var courtsTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCourtsEntityTabComponent>(LitigationAnalyticsProfiles.Courts);
            var typeahead = courtsTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 40);
            var motionsTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<MotionsProfileTabComponent>(ProfileComponentTab.Motions);
            int tableRowCount = motionsTab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.TableContentItemsList.Count;
            var fullTableViewMotiontab = motionsTab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.ClickOnShowFullTable<LitigationAnalyticsProfilerPage>();

            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 40);
            string currentTabName = fullTableViewMotiontab.ProfileTabPanel.CurrentTabName;
            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 40);
            var tableRowCountOnFullTableViewTab = fullTableViewMotiontab.ProfileTabPanel.SetActiveTab<MotionsProfileTabComponent>(ProfileComponentTab.Motions).ContentViewTabPanel.ChartViewContainer.TableContentComponent.TableContentItemsList.Count;

            this.TestCaseVerify.IsTrue(
            FullTableViewVerify,
            tableRowCount != tableRowCountOnFullTableViewTab && currentTabName.Equals("Motions"),
            "Verify that Show full table view link isn't opened new tab with full table");

            string tabName = BrowserPool.CurrentBrowser.TabHandles.Last();
            BrowserPool.CurrentBrowser.CreateTab(tabName);
            BrowserPool.CurrentBrowser.CloseTab(tabName);
            BrowserPool.CurrentBrowser.ActivateTab(tabName);

            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var experienceTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            int experienceTableRowCount = experienceTab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.TableContentItemsList.Count;

            var caseTypeTab = experienceTab.ContentViewTabPanel.HeaderTabPanel.SetActiveTab<CaseTypeTabComponent>(LitigationAnalyticsChartHeaderTab.CaseType);
            var fullTableViewExperiencetab = experienceTab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.ClickOnShowFullTable<LitigationAnalyticsProfilerPage>();

            SafeMethodExecutor.WaitUntil(() => !fullTableViewExperiencetab.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 40);
            string experienceCurrentTabName = fullTableViewExperiencetab.ProfileTabPanel.CurrentTabName;
            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var experienceTableRowCountOnFullTableViewTab = fullTableViewExperiencetab.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience).ContentViewTabPanel.ChartViewContainer.TableContentComponent.TableContentItemsList.Count;

            this.TestCaseVerify.IsTrue(
            FullTableViewVerify,
            experienceTableRowCount != experienceTableRowCountOnFullTableViewTab && experienceCurrentTabName.Equals("Experience"),
            $"Verify that Experience tab Show full table view link isn't opened new tab with full table. Table row count on LA page {tableRowCount}, count on full table page {tableRowCountOnFullTableViewTab}");
          
            tabName = BrowserPool.CurrentBrowser.TabHandles.Last();
            BrowserPool.CurrentBrowser.CreateTab(tabName);
            BrowserPool.CurrentBrowser.CloseTab(tabName);
            BrowserPool.CurrentBrowser.ActivateTab(tabName);

            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var outcomesTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<OutcomesProfileTabComponent>(ProfileComponentTab.Outcomes);
            int outcomesTableRowCount = outcomesTab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.TableContentItemsList.Count;

            var participantsTab = outcomesTab.ContentViewTabPanel.HeaderTabPanel.SetActiveTab<ParticipantsTabComponent>(LitigationAnalyticsChartHeaderTab.Participants);
            var fullTableViewOutcomesTab = outcomesTab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.ClickOnShowFullTable<LitigationAnalyticsProfilerPage>();

            var outcomesCurrentTabName = fullTableViewOutcomesTab.ProfileTabPanel.IsActive(ProfileComponentTab.Outcomes);
            var outcomesTableRowCountOnFullTableViewTab = fullTableViewOutcomesTab.AnalyticsProfileTabPage.ContentViewTabPanel.ChartViewContainer.TableContentComponent.TableContentItemsList.Count;

            this.TestCaseVerify.IsTrue(
            FullTableViewVerify,
            outcomesTableRowCount != outcomesTableRowCountOnFullTableViewTab && outcomesCurrentTabName,
            $"Verify that Outcomes Tab Show full table view link isn't opened new tab with full table. Table row count on LA page {tableRowCount}, count on full table page {tableRowCountOnFullTableViewTab}");        
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void CourtCascadeCourtFilteringTest()
        {
            const string Query = "United States District Court, N.D. Florida";
            const string FilteringVerify = "Verify that cascading Case type filter on Attorneys Entity works correctely";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var courtsabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCourtsEntityTabComponent>(LitigationAnalyticsProfiles.Courts);
            var typeahead = courtsabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var experience = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);

            SafeMethodExecutor.WaitUntil(() => !experience.LoadingSpinner.Displayed, 20);

            if (experience.NarrowPane.ApplyButton.Displayed)
            {
                experience.NarrowPane.SelectMultipleFiltersToggle.ToggleState(true);
            }

            var caseTypeFacet = experience.NarrowPane.SearchFacets.CascadingFacetButton(LitigationAnalyticsCascadingFacets.CaseType).Click<LitigationAnalyticsFacetDialog>();
            var facetCheckbox = caseTypeFacet.CascadingFacetResultItems.First().SearchFacetCheckbox;
            facetCheckbox.ScrollToElement();
            facetCheckbox.Set(true);

            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var motionsTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<MotionsProfileTabComponent>(ProfileComponentTab.Motions);

            this.TestCaseVerify.IsTrue(
              FilteringVerify,
              motionsTab.NarrowPane.SearchFacets.CascadingFacetResultItems(LitigationAnalyticsCascadingFacets.CaseType).First().SearchFacetCheckbox.Selected,
             "Verify that Case type cascading filter on Courts -> Motions works correctely");

            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var outcomesTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<OutcomesProfileTabComponent>(ProfileComponentTab.Outcomes);

            this.TestCaseVerify.IsTrue(
              FilteringVerify,
              outcomesTab.NarrowPane.SearchFacets.CascadingFacetResultItems(LitigationAnalyticsCascadingFacets.CaseType).First().SearchFacetCheckbox.Selected,
             "Verify that Case type cascading filter on Courts -> Outcomes works correctely");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void CourtChartFilteringTest()
        {
            const string Query = "United States District Court, M.D. Georgia";
            const string ChartsFilteringVerify = "Charts filtering on Courts Entity is working correctely";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var courtsTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCourtsEntityTabComponent>(LitigationAnalyticsProfiles.Courts);
            var typeahead = courtsTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var experienceTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            int tableRowCount = experienceTab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.TableContentItemsList.Count;

            var lawFirmFacet = experienceTab.NarrowPane.SearchFacets.SelectFacet(LitigationAnalyticsFacets.LawFirm);
            var facetItem = lawFirmFacet.FacetResultItems.First(item => item.SearchFacetLabelText.Text.Equals("U. S. Attorney's Office"));
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
            "Charts filtering on Courts Entity is not working correctely");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(SmokeTestCategory)]
        public void CourtMultipleToggleFilteringTest()
        {
            const string Query = "United States District Court, N.D. Florida";
            const string FilteringVerify = "Verify that filtering on Court Entity works correctely";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var courtsTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCourtsEntityTabComponent>(LitigationAnalyticsProfiles.Courts);
            var typeahead = courtsTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var expertChallengesTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExpertChallengesTabComponent>(ProfileComponentTab.ExpertChallenges);

            expertChallengesTab.NarrowPane.SelectMultipleFiltersToggle.ScrollToElement();

            var experienceDocketsBeforeCount = expertChallengesTab.ResultListComponent.GetDocketsCount();
            
            if (expertChallengesTab.NarrowPane.ApplyButton.Displayed)
            {
                expertChallengesTab.NarrowPane.SelectMultipleFiltersToggle.ToggleState(true);
            }

            var areaOfExperienceFacet = expertChallengesTab.NarrowPane.SearchFacets.SelectFacet(LitigationAnalyticsFacets.AreaOfExpertise);
            areaOfExperienceFacet.NameSubTabButton.Click();
            var facetItem = areaOfExperienceFacet.FacetResultItems.First();
            facetItem.SearchFacetCheckbox.ScrollToElement();
            facetItem.SearchFacetCheckbox.Set(true);
            areaOfExperienceFacet.FacetTitleButton.Click();

            int experienceDocketsAfterCount = expertChallengesTab.ResultListComponent.GetDocketsCount();

            this.TestCaseVerify.AreNotSame(
                "Chart filtering by Case Type Entity works correctly in Experience tab when Multiple Filters are Disabled.",
                experienceDocketsBeforeCount, experienceDocketsAfterCount,
                "Chart filtering by Case Type Entity does not work correctly in Experience tab when Multiple Filters are Disabled.");

            this.TestCaseVerify.AreEqual(
            FilteringVerify,
            expertChallengesTab.ResultListComponent.GetDocketsCount(),
            int.Parse(expertChallengesTab.NarrowPane.SearchFacets.FacetResultItems(LitigationAnalyticsFacets.AreaOfExpertise).First().SearchFacetOutputTextValue.Text),
            "Verify that filtering on Court Entity doesn't work correctely");

            BrowserPool.CurrentBrowser.Refresh();

            if (!expertChallengesTab.NarrowPane.ApplyButton.Displayed)
            {
                expertChallengesTab.NarrowPane.SelectMultipleFiltersToggle.ToggleState(true);
            }

            areaOfExperienceFacet = expertChallengesTab.NarrowPane.SearchFacets.SelectFacet(LitigationAnalyticsFacets.AreaOfExpertise);
            areaOfExperienceFacet.NameSubTabButton.Click();
            facetItem = areaOfExperienceFacet.FacetResultItems.First();
            facetItem.SearchFacetCheckbox.ScrollToElement();
            facetItem.SearchFacetCheckbox.Set(true);
            areaOfExperienceFacet.FacetTitleButton.Click();

            var experienceDocketsAfterFacetChecked = expertChallengesTab.ResultListComponent.GetDocketsCount();

            this.TestCaseVerify.AreEqual(
                "Dockets count should not change before clicking Apply when Multiple Filters are Enabled.",
                experienceDocketsBeforeCount, experienceDocketsAfterFacetChecked,
                "Dockets count changed before Apply was clicked when Multiple Filters are Enabled.");

            expertChallengesTab.NarrowPane.ApplyButton.ScrollToElement();
            expertChallengesTab.NarrowPane.ApplyButton.Click();

            experienceDocketsAfterCount = expertChallengesTab.ResultListComponent.GetDocketsCount();

            this.TestCaseVerify.AreNotSame(
                "Chart filtering by Case Type Entity works correctly in Experience tab when Multiple Filters are Enabled.",
                experienceDocketsBeforeCount, experienceDocketsAfterCount,
                "Chart filtering by Case Type Entity does not work correctly in Experience tab when Multiple Filters are Enabled.");
        }
    }
}