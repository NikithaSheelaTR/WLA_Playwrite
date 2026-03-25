namespace WestlawPrecision.Tests.LitigationAnalytics
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsHomePageComponents.Entities;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.Tabs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.Utils.Execution;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    [TestClass]
    public class DamageTests : BaseAnalyticsTest
    {
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void DamageChartFilteringTest()
        {
            const string Query = "Civil Rights";
            string ChartsFilteringVerify = "Charts filtering on Damage Entity is working correctely";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var damagesTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsDamagesEntityTabComponent>(LitigationAnalyticsProfiles.Damages);
            var caseTypeDialog = damagesTabComponent.CaseTypeButton.Click<LitigationAnalyticsCaseTypeSelectingDialog>();
            caseTypeDialog.EnterSearchQueryCaseTypeDialog<LitigationAnalyticsCaseTypeSelectingDialog>(Query);
            caseTypeDialog.CaseTypeItems.First().NameLink.Click<LitigationAnalyticsCaseTypeSelectingDialog>().SaveButton.Click<LitigationAnalyticsDamagesEntityTabComponent>();
            var profilePage = damagesTabComponent.SearchButton.Click<DamagesProfileTabComponent>();

            var distributiontab = profilePage.ContentViewTabPanel.HeaderTabPanel.SetActiveTab<DistributionTabComponent>(LitigationAnalyticsChartHeaderTab.Distribution);
            SafeMethodExecutor.WaitUntil(() => !distributiontab.LoadingSpinner.Displayed, 20);

            if (distributiontab.NarrowPane.ApplyButton.Displayed)
            {
                distributiontab.NarrowPane.SelectMultipleFiltersToggle.ToggleState(true);
            }

            var damagesTypeFacet = distributiontab.NarrowPane.SearchFacets.SelectFacet(LitigationAnalyticsFacets.DamagesType);
            SafeMethodExecutor.WaitUntil(() => damagesTypeFacet.FacetResultItems.First().SearchFacetCheckbox.Displayed, 20);

            var facetItem = damagesTypeFacet.FacetResultItems.First();
            facetItem.SearchFacetCheckbox.ScrollToElement();
            facetItem.SearchFacetCheckbox.Set(true);
            damagesTypeFacet.FacetTitleButton.Click();

            this.TestCaseVerify.AreEqual(
            ChartsFilteringVerify,
            distributiontab.ResultListComponent.GetDocketsCount(),
            int.Parse(distributiontab.NarrowPane.SearchFacets.FacetResultItems(LitigationAnalyticsFacets.DamagesType).First().SearchFacetOutputTextValue.Text),
            "Chart filtering on Damage Entity is not working correctely");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void DamageMultipleChartTest()
        {
            const string Query = "Civil Rights";
            string MultipalChartsViewVerify = "Multiple chart view toggle works correctly";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var damagesTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsDamagesEntityTabComponent>(LitigationAnalyticsProfiles.Damages);
            var caseTypeDialog = damagesTabComponent.CaseTypeButton.Click<LitigationAnalyticsCaseTypeSelectingDialog>();
            caseTypeDialog.EnterSearchQueryCaseTypeDialog<LitigationAnalyticsCaseTypeSelectingDialog>(Query);
            caseTypeDialog.CaseTypeItems.First().NameLink.Click<LitigationAnalyticsCaseTypeSelectingDialog>().SaveButton.Click<LitigationAnalyticsDamagesEntityTabComponent>();
            var profilePage = damagesTabComponent.SearchButton.Click<DamagesProfileTabComponent>();
            var listOfDisplayedTabs = profilePage.ContentViewTabPanel.HeaderTabPanel.GetListOfDisplayedTabs;
            profilePage.ContentViewTabPanel.DisplayChartsIndividuallyToggle.ToggleState(true);
            var listOfDisplayedCharts = profilePage.ContentViewTabPanel.ChartViewContainerList.Select(e => e.ChartsHeaderIndividualyHeader.Text).ToList();

            this.TestCaseVerify.IsTrue(
               MultipalChartsViewVerify,
               listOfDisplayedTabs.SequenceEqual(listOfDisplayedCharts), "Multiple chart view toggle is not working correctly");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void DamageDistributionTabShowFullTableTest()
        {
            const string Query = "Civil Rights";
            const string FullTableViewVerify = "Verify that Damage tab - Distribution tab Show full table view link is opened new tab with full table should have six rows";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var damagesTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsDamagesEntityTabComponent>(LitigationAnalyticsProfiles.Damages);
            var caseTypeDialog = damagesTabComponent.CaseTypeButton.Click<LitigationAnalyticsCaseTypeSelectingDialog>();
            caseTypeDialog.EnterSearchQueryCaseTypeDialog<LitigationAnalyticsCaseTypeSelectingDialog>(Query);
            caseTypeDialog.CaseTypeItems.First().NameLink.Click<LitigationAnalyticsCaseTypeSelectingDialog>().SaveButton.Click<LitigationAnalyticsDamagesEntityTabComponent>();
            var profilePage = damagesTabComponent.SearchButton.Click<DamagesProfileTabComponent>();

            int tableRowCount = profilePage.ContentViewTabPanel.ChartViewContainer.TableContentComponent.TableContentItemsList.Count;
            var distributiontab = profilePage.ContentViewTabPanel.HeaderTabPanel.SetActiveTab<DistributionTabComponent>(LitigationAnalyticsChartHeaderTab.Distribution);
            var fullTableViewDistributionTab = distributiontab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.ClickOnShowFullTable<DamagesProfileTabComponent>();

            var currentTab = fullTableViewDistributionTab.ContentViewTabPanel.HeaderTabPanel.SetActiveTab<DistributionTabComponent>(LitigationAnalyticsChartHeaderTab.Distribution);
            var tableRowCountOnFullTableViewTab = currentTab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.TableContentItemsList.Count;

            this.TestCaseVerify.IsTrue(
            FullTableViewVerify,
            tableRowCount != tableRowCountOnFullTableViewTab,
            $"Verify that Damage Tab - Distribution tab Show full table view link isn't opened new tab with full table. Table row count on LA page {tableRowCount}, count on full table page {tableRowCountOnFullTableViewTab}");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(SmokeTestCategory)]
        public void DamagesMultipleToggleFilteringTest()
        {
            const string Query = "Civil Rights";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var damagesTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsDamagesEntityTabComponent>(LitigationAnalyticsProfiles.Damages);
            var caseTypeDialog = damagesTabComponent.CaseTypeButton.Click<LitigationAnalyticsCaseTypeSelectingDialog>();
            caseTypeDialog.EnterSearchQueryCaseTypeDialog<LitigationAnalyticsCaseTypeSelectingDialog>(Query);
            caseTypeDialog.CaseTypeItems.First().NameLink.Click<LitigationAnalyticsCaseTypeSelectingDialog>().SaveButton.Click<LitigationAnalyticsDamagesEntityTabComponent>();
            var profilePage = damagesTabComponent.SearchButton.Click<DamagesProfileTabComponent>();

            var distributiontab = profilePage.ContentViewTabPanel.HeaderTabPanel.SetActiveTab<DistributionTabComponent>(LitigationAnalyticsChartHeaderTab.Distribution);
            SafeMethodExecutor.WaitUntil(() => !distributiontab.LoadingSpinner.Displayed, 20);

            distributiontab.NarrowPane.SelectMultipleFiltersToggle.ScrollToElement();

            var experienceDocketsBeforeCount = distributiontab.ResultListComponent.GetDocketsCount();

            if (distributiontab.NarrowPane.ApplyButton.Displayed)
            {
                distributiontab.NarrowPane.SelectMultipleFiltersToggle.ToggleState(true);
            }

            SafeMethodExecutor.WaitUntil(() => !distributiontab.NarrowPane.ApplyButton.Displayed, 20);

            var damagesTypeFacet = distributiontab.NarrowPane.SearchFacets.SelectFacet(LitigationAnalyticsFacets.DamagesType);
            SafeMethodExecutor.WaitUntil(() => damagesTypeFacet.FacetResultItems.First().SearchFacetCheckbox.Displayed, 20);

            var facetItem = damagesTypeFacet.FacetResultItems.First();
            facetItem.SearchFacetCheckbox.ScrollToElement();
            facetItem.SearchFacetCheckbox.Set(true);
            damagesTypeFacet.FacetTitleButton.Click();

            int experienceDocketsAfterCount = distributiontab.ResultListComponent.GetDocketsCount();

            this.TestCaseVerify.AreNotSame(
                "Chart filtering by Case Type Entity works correctly in Experience tab when Multiple Filters are Disabled.",
                experienceDocketsBeforeCount, experienceDocketsAfterCount,
                "Chart filtering by Case Type Entity does not work correctly in Experience tab when Multiple Filters are Disabled.");

            BrowserPool.CurrentBrowser.Refresh();

            if (!distributiontab.NarrowPane.ApplyButton.Displayed)
            {
                distributiontab.NarrowPane.SelectMultipleFiltersToggle.ToggleState(true);
            }

            var newpdamagesTypeFacet = distributiontab.NarrowPane.SearchFacets.SelectFacet(LitigationAnalyticsFacets.DamagesType);
            facetItem = newpdamagesTypeFacet.FacetResultItems.First();
            facetItem.SearchFacetCheckbox.ScrollToElement();
            facetItem.SearchFacetCheckbox.Set(true);
            newpdamagesTypeFacet.FacetTitleButton.Click();

            var experienceDocketsAfterFacetChecked = distributiontab.ResultListComponent.GetDocketsCount();

            this.TestCaseVerify.AreEqual(
                "Dockets count should not change before clicking Apply when Multiple Filters are Enabled.",
                experienceDocketsBeforeCount, experienceDocketsAfterFacetChecked,
                "Dockets count changed before Apply was clicked when Multiple Filters are Enabled.");

            distributiontab.NarrowPane.ApplyButton.ScrollToElement();
            distributiontab.NarrowPane.ApplyButton.Click();

            experienceDocketsAfterCount = distributiontab.ResultListComponent.GetDocketsCount();

            this.TestCaseVerify.AreNotSame(
                "Chart filtering by Case Type Entity works correctly in Experience tab when Multiple Filters are Enabled.",
                experienceDocketsBeforeCount, experienceDocketsAfterCount,
                    "Chart filtering by Case Type Entity does not work correctly in Experience tab when Multiple Filters are Enabled.");
        }
    }
}