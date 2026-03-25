namespace WestlawPrecision.Tests.LitigationAnalytics
{
    using System.IO;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsHomePageComponents.Entities;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.Tabs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.LitigationAnalytics;
    using Framework.Common.UI.Raw.EnhancementTests.Utils;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.Utils.Execution;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WestlawPrecision.Tests.LitigationAnalytics.DeliveryTests;

    [TestClass]
    public class AttorneyTests : BaseDeliveryTest
    {
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(SmokeTestCategory)]
        public void AttorneysFilteringTest()
        {
            const string Query = "Lawrence Bender";
            const string FilteringVerify = "Verify that filtering on Attorneys Entity works correctely";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var attorneysabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsAttorneysEntityTabComponent>(LitigationAnalyticsProfiles.Attorneys);
            attorneysabComponent.ByNameRadiobutton.Select();
            var typeahead = attorneysabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var motionsTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<MotionsProfileTabComponent>(ProfileComponentTab.Motions);
            SafeMethodExecutor.WaitUntil(() => !motionsTab.LoadingSpinner.Displayed, 20);

            if (motionsTab.NarrowPane.ApplyButton.Displayed)
            {
                motionsTab.NarrowPane.SelectMultipleFiltersToggle.ToggleState(true);
            }

            var motionTypeFacet = motionsTab.NarrowPane.SearchFacets.SelectFacet(LitigationAnalyticsFacets.MotionType);
            motionTypeFacet.NameSubTabButton.Click();
            var facetItem = motionTypeFacet.FacetResultItems.First();
            facetItem.SearchFacetCheckbox.Set(true);
            motionTypeFacet.FacetTitleButton.Click();

            this.TestCaseVerify.AreEqual(
            FilteringVerify,
            motionsTab.ResultListComponent.GetDocketsCount(),
            motionsTab.NarrowPane.SearchFacets.FacetResultItems(LitigationAnalyticsFacets.MotionType).First().SearchFacetOutputStateFederalTextValue.Select(m => int.Parse(m.Text)).Sum(),
            "Verify that filtering on Attorneys Entity works correctely");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void AttorneysCascadeCourtFilteringTest()
        {
            const string Query = "Lawrence Bender";
            const string FilteringVerify = "Verify that cascading Court filter on Attorneys Entity works correctely";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var attorneysabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsAttorneysEntityTabComponent>(LitigationAnalyticsProfiles.Attorneys);
            attorneysabComponent.ByNameRadiobutton.Select();
            var typeahead = attorneysabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var experience = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            SafeMethodExecutor.WaitUntil(() => !experience.LoadingSpinner.Displayed, 20);

            if (experience.NarrowPane.ApplyButton.Displayed)
            {
                experience.NarrowPane.SelectMultipleFiltersToggle.ToggleState(true);
            }

            var courtFacet = experience.NarrowPane.SearchFacets.CascadingFacetButton(LitigationAnalyticsCascadingFacets.Court).Click<LitigationAnalyticsFacetDialog>();
            var facetItem = courtFacet.FacetResultItems.First(item => item.SearchFacetLabelText.Text.Equals("Federal"));
            facetItem.SearchFacetCheckbox.Set(true);

            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var motionsTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<MotionsProfileTabComponent>(ProfileComponentTab.Motions);

            this.TestCaseVerify.IsTrue(
              FilteringVerify,
              motionsTab.NarrowPane.SearchFacets.CascadingFacetResultItems(LitigationAnalyticsCascadingFacets.Court).First().SearchFacetCheckbox.Selected,
             "Verify that Court cascading filter on Attorneys -> Motions works correctely");

            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var outcomesTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<OutcomesProfileTabComponent>(ProfileComponentTab.Outcomes);

            this.TestCaseVerify.IsTrue(
              FilteringVerify,
              outcomesTab.NarrowPane.SearchFacets.CascadingFacetResultItems(LitigationAnalyticsCascadingFacets.Court).First().SearchFacetCheckbox.Selected,
             " Verify that Court cascading filter on Attorneys -> Outcomes works correctely");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void AttorneyChartFilteringTest()
        {
            const string Query = "Lawrence Bender";
            string ChartsFilteringVerify = "Charts filtering on Attorney Entity is working correctely";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var casestypeTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCaseTypeEntityTabComponent>(LitigationAnalyticsProfiles.Attorneys);
            var typeahead = casestypeTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var experience = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            var courtFacet = experience.NarrowPane.SearchFacets.CascadingFacetButton(LitigationAnalyticsCascadingFacets.Court).Click<LitigationAnalyticsFacetDialog>();
            var facetItem = courtFacet.FacetResultItems.First(item => item.SearchFacetLabelText.Text.Equals("Federal"));
            facetItem.SearchFacetCheckbox.Set(true);

            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var motionsTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<MotionsProfileTabComponent>(ProfileComponentTab.Motions);
            var filingTypeTab = motionsTab.ContentViewTabPanel.HeaderTabPanel.SetActiveTab<FilingRoleTabComponent>(LitigationAnalyticsChartHeaderTab.FilingRole);            
            int motionsordersBeforeCount = motionsTab.ResultListComponent.GetDocketsCount();
            motionsTab.ContentViewTabPanel.ChartViewContainer.ChartsContent.ChartContentItemsList.First().StackedButtonChartClick();
            int motionsordersAfterCount = motionsTab.ResultListComponent.GetDocketsCount();

            this.TestCaseVerify.AreNotSame(
            ChartsFilteringVerify,
            motionsordersBeforeCount, motionsordersAfterCount,
            "Verify that chart filtering on Attorneys Entity is not working correctly");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void AttorneyExperienceTabShowFullTableTest()
        {
            const string Query = "Lawrence Bender";
            const string FullTableViewVerify = "Verify that Experience tab Show full table view link is opened new tab with full table";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var courtsTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsAttorneysEntityTabComponent>(LitigationAnalyticsProfiles.Attorneys);
            var typeahead = courtsTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var experienceTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);

            var caseTypeTab = experienceTab.ContentViewTabPanel.HeaderTabPanel.SetActiveTab<CaseTypeTabComponent>(LitigationAnalyticsChartHeaderTab.CaseType);
            int tableRowCount = experienceTab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.TableContentItemsList.Count;
            var fullTableViewExperiencetab = experienceTab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.ClickOnShowFullTable<LitigationAnalyticsProfilerPage>();

            SafeMethodExecutor.WaitUntil(() => fullTableViewExperiencetab.ProfileTabPanel.IsDisplayed(ProfileComponentTab.Experience), 50);
            string currentTabName = fullTableViewExperiencetab.ProfileTabPanel.CurrentTabName;
            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var tableRowCountOnFullTableViewTab = fullTableViewExperiencetab.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience).ContentViewTabPanel.ChartViewContainer.TableContentComponent.TableContentItemsList.Count;

            this.TestCaseVerify.IsTrue(
            FullTableViewVerify,
            tableRowCount != tableRowCountOnFullTableViewTab && currentTabName.Equals("Experience"),
            "Verify that Experience tab Show full table view link isn't opened new tab with full table");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void AttorneyMultipleChartModeDeliveryTest()
        {
            const string Query = "Lawrence Bender";
            const string DeliveriedDocumentChartNamesVerify = "Verify that downloaded document contains the Filter item";
            const string DeliveriedDocumentVerify = "Docket analytics";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var courtsTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsAttorneysEntityTabComponent>(LitigationAnalyticsProfiles.Attorneys);
            var typeahead = courtsTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var profilePage = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            string reportName = $"Westlaw Precision - Litigation Analytics Report for {analyticsProfilerPage.AnalyticsTitle.Replace(".", "")}.pdf";

            var listOfDisplayedTabs = profilePage.ContentViewTabPanel.HeaderTabPanel.GetListOfDisplayedTabs;
            profilePage.ContentViewTabPanel.DisplayChartsIndividuallyToggle.ToggleState(true);
            var listOfDisplayedCharts = profilePage.ContentViewTabPanel.ChartViewContainerList.Select(e => e.ChartsHeaderIndividualyHeader.Text).ToList();

            var downloadDialog = profilePage.ContentViewTabPanel.TitleToolBarComponent.DeliveryDropdown.SelectOption<LitigationAnalyticsDownloadDialog>(DeliveryMethod.Download);
            this.DownloadCurrentViewReport(downloadDialog, reportName);
            string textDelivery = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, reportName));

            this.TestCaseVerify.IsTrue(
                DeliveriedDocumentChartNamesVerify,
                    textDelivery.Contains(Query) || textDelivery.Contains(DeliveriedDocumentVerify),
                    "Downloaded document does not contain the Filter item");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void AttorneyMultiplefiltersToggleChartFilteringTest()
        {
            const string Query = "Lawrence Bender";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var casestypeTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCaseTypeEntityTabComponent>(LitigationAnalyticsProfiles.Attorneys);
            var typeahead = casestypeTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var experience = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);            

            experience.NarrowPane.SelectMultipleFiltersToggle.ScrollToElement();

            if (experience.NarrowPane.ApplyButton.Displayed)
            {
                experience.NarrowPane.SelectMultipleFiltersToggle.ToggleState(false);
            }

            int experienceDocketsBeforeCount = experience.ResultListComponent.GetDocketsCount();
            var courtFacet = experience.NarrowPane.SearchFacets.CascadingFacetButton(LitigationAnalyticsCascadingFacets.Court).Click<LitigationAnalyticsFacetDialog>();
            var facetItem = courtFacet.FacetResultItems.First(item => item.SearchFacetLabelText.Text.Equals("Federal"));
            facetItem.SearchFacetCheckbox.Set(true);

            int experienceDocketsAfterCount = experience.ResultListComponent.GetDocketsCount();

            this.TestCaseVerify.AreNotSame(
                "Chart filtering by Attorney works correctly in Experience tab when Multiple Filters are Disabled.",
                experienceDocketsBeforeCount, experienceDocketsAfterCount,
                "Chart filtering by Attorney does not work correctly in Experience tab when Multiple Filters are Disabled.");

            SafeMethodExecutor.WaitUntil(() => { experience.NarrowPane.ClearButton.ScrollToElement(); experience.NarrowPane.ClearButton.Click(); return true; }, timeoutFromSec: 15);
            BrowserPool.CurrentBrowser.Refresh();

            experience.NarrowPane.SelectMultipleFiltersToggle.ToggleState(true);
            experienceDocketsBeforeCount = experience.ResultListComponent.GetDocketsCount();

            courtFacet = experience.NarrowPane.SearchFacets.CascadingFacetButton(LitigationAnalyticsCascadingFacets.Court).Click<LitigationAnalyticsFacetDialog>();
            facetItem = courtFacet.FacetResultItems.First(item => item.SearchFacetLabelText.Text.Equals("State"));
            facetItem.SearchFacetCheckbox.Set(true);

            int experienceDocketsAfterFacetChecked = experience.ResultListComponent.GetDocketsCount();

            this.TestCaseVerify.AreEqual(
                "Dockets count should not change before clicking Apply when Multiple Filters are Enabled.",
                experienceDocketsBeforeCount, experienceDocketsAfterFacetChecked,
                "Dockets count changed before Apply was clicked when Multiple Filters are Enabled.");

            experience.NarrowPane.ApplyButton.ScrollToElement();
            experience.NarrowPane.ApplyButton.Click();

            experienceDocketsAfterCount = experience.ResultListComponent.GetDocketsCount();

            this.TestCaseVerify.AreNotSame(
                "Chart filtering by Attorney works correctly in Experience tab when Multiple Filters are Enabled.",
                experienceDocketsBeforeCount, experienceDocketsAfterCount,
                "Chart filtering by Attorney does not work correctly in Experience tab when Multiple Filters are Enabled.");

            SafeMethodExecutor.WaitUntil(() => { experience.NarrowPane.SelectMultipleFiltersToggle.ToggleState(false); return true; }, timeoutFromSec: 15);
        }
    }
}