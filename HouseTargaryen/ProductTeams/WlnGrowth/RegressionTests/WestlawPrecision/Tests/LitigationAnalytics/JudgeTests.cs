namespace WestlawPrecision.Tests.LitigationAnalytics
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsHomePageComponents.Entities;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.SubCharts.Participants;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.Tabs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.NarrowPane.Facets;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.LitigationAnalytics;
    using Framework.Core.Utils.Execution;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    [TestClass]
    public class JudgeTests : BaseAnalyticsTest
    {
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(SmokeTestCategory)]
        public void JudgesFilteringTest()
        {
            const string Query = " Lucy Koh";
            const string FilteringVerify = "Verify that filtering on Judges Entity works correctely";

            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var judgesTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsJudgesEntityTabComponent>(LitigationAnalyticsProfiles.Judges);
            var typeahead = judgesTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
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
            expertChallengesTab.NarrowPane.SearchFacets.FacetResultItems(LitigationAnalyticsFacets.AreaOfExpertise).First().SearchFacetOutputStateFederalTextValue.Select(m => int.Parse(m.Text)).Sum(),
            "Fltering on Judges Entity doesn't work correctely");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(SmokeTestCategory)]
        public void JudgesCascadeDateFilteringTest()
        {
            const string Query = "Lee, Tom S";
            string check5YearsFilterIsDisplayed = "Verify: 3 years filter is displayed and enabled";

            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var judgeTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsJudgesEntityTabComponent>(LitigationAnalyticsProfiles.Judges);
            var typeahead = judgeTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            var experienceTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);

            SafeMethodExecutor.WaitUntil(() => !experienceTab.LoadingSpinner.Displayed, 20);

            if (experienceTab.NarrowPane.ApplyButton.Displayed)
            {
                experienceTab.NarrowPane.SelectMultipleFiltersToggle.ToggleState(true);
            }

            var dateFacetDialog = experienceTab.NarrowPane.SearchFacets.CascadingFacetButton(LitigationAnalyticsCascadingFacets.Date).Click<LitigationAnalyticsDateFacetDialog>();
            dateFacetDialog.DateRangeOptionDropdown.SelectOption(LitigationAnalyticsDateRangeOptions.Last3Years);

            this.TestCaseVerify.AreEqual(
                check5YearsFilterIsDisplayed,
                3,
                experienceTab.NarrowPane.SearchFacets.CascadingFacetButton(LitigationAnalyticsCascadingFacets.Date).Click<LitigationAnalyticsDateFacetDialog>().GetDateFacetOutputOption(),
                " 3 years filter is not displayed and enabled on Experience tav");

            var outcomesTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<OutcomesProfileTabComponent>(ProfileComponentTab.Outcomes);

            this.TestCaseVerify.AreEqual(
                check5YearsFilterIsDisplayed,
                3,
                outcomesTab.NarrowPane.SearchFacets.DateFacet.GetDateFacetOutputOption(),
                "3 years filter is not displayed and enabled on Outcomes tab");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void JudgesExperienceTabShowFullTableTest()
        {
            const string Query = "Lee, Tom S";
            const string FullTableViewVerify = "Verify that Judges - Experience tab Show full table view link is opened new tab with full table";

            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var lawFirmsTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsJudgesEntityTabComponent>(LitigationAnalyticsProfiles.Judges);
            var typeahead = lawFirmsTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            var experienceTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);

            var caseTypeTab = experienceTab.ContentViewTabPanel.HeaderTabPanel.SetActiveTab<CaseTypeTabComponent>(LitigationAnalyticsChartHeaderTab.CaseType);
            int tableRowCount = experienceTab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.TableContentItemsList.Count;
            var fullTableViewExperiencetab = experienceTab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.ClickOnShowFullTable<LitigationAnalyticsProfilerPage>();

            SafeMethodExecutor.WaitUntil(() => !fullTableViewExperiencetab.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 50);
            string currentTabName = fullTableViewExperiencetab.ProfileTabPanel.CurrentTabName;
            var tableRowCountOnFullTableViewTab = fullTableViewExperiencetab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.TableContentItemsList.Count;

            this.TestCaseVerify.IsTrue(
            FullTableViewVerify,
            tableRowCount != tableRowCountOnFullTableViewTab && currentTabName.Equals("Experience"),
            $"Verify that Judges - Experience tab Show full table view link isn't opened new tab with full table. Table row count on LA page {tableRowCount}, count on full table page {tableRowCountOnFullTableViewTab}");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void JudgesOutcomesChartFilteringTest()
        {
            const string Query = "Tom S Lee";
            const string ChartsFilteringVerify = "Verify that chart filtering on Judges Entity works correctely on Outcomes tab";

            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var lawFirmsTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsLawFirmsEntityTabComponent>(LitigationAnalyticsProfiles.Judges);
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
        public void StackByYearTest()
        {
            const string Query = "Tom S. Lee";
            string checkStackByYearFunctionWorkWithCharts = "Verify: Stack By Year is displayed and enabled with Charts";
            string checkStackByYearFunctionWorkWithTable = "Verify: Stack By Year is displayed and enabled with Table";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var judgeTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsJudgesEntityTabComponent>(LitigationAnalyticsProfiles.Judges);
            var typeahead = judgeTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            var experienceTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            var participantsTab = experienceTab.ContentViewTabPanel.HeaderTabPanel.SetActiveTab<ParticipantsTabComponent>(LitigationAnalyticsChartHeaderTab.Participants);
            var attorneys = participantsTab.ParticipantsSubChartTabPanel.SetActiveTab<PartiesSubTab>(LitigationAnalyticsContainerSubcategories.Attorneys);
            var legend = attorneys.ChartContainer.Legend.LegendColorsList[2];
            var chartItem = attorneys.ChartContainer.ChartsContent.ChartContentItemsList.First();

            this.TestCaseVerify.IsTrue(
                    checkStackByYearFunctionWorkWithCharts,
                    chartItem.ColorsList().Contains(legend),
                    "StackByYear is not enabled with Charts ");

            this.TestCaseVerify.IsTrue(
                checkStackByYearFunctionWorkWithTable,
                attorneys.TableContainer.TableContent.TableHeaderTitles.Count > 2,
                "StackByYear is not enabled with Tables ");
        }
    }
}