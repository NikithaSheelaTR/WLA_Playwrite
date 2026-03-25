namespace WestlawPrecision.Tests.LitigationAnalytics
{
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
    using System.IO;
    using System.Linq;
    using WestlawPrecision.Tests.LitigationAnalytics.DeliveryTests;

    [TestClass]
    public class IndustryTests : BaseDeliveryTest
    {
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void IndustryTabTest()
        {
            const string IndustryName = "Fishing";
            const string FullTableViewVerify = "Verify that Experience tab Show full table view link is opened new tab with full table";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var industryTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCompaniesEntityTabComponent>(LitigationAnalyticsProfiles.Companies);
            industryTabComponent.ByIndustryRadioButton.Select();
            var industrySelectingDialog = industryTabComponent.SelectIndustryButton.Click<LitigationAnalyticsIndustrySelectingDialog>();
            industrySelectingDialog.NaiscRadiobutton.Select();
            industrySelectingDialog.IndustrySearchBox.SetText(IndustryName);
            industrySelectingDialog.IndustryResultItems.First(element => element.GetIndustryName.Contains(IndustryName)).IndustryName.Click();
            industryTabComponent = industrySelectingDialog.SaveButton.Click<LitigationAnalyticsCompaniesEntityTabComponent>();
            var analyticsProfilerPage = industryTabComponent.SearchButton.Click<LitigationAnalyticsProfilerPage>();

            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 40);
            var experienceTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            int tableRowCount = experienceTab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.TableContentItemsList.Count;

            var caseTypeTab = experienceTab.ContentViewTabPanel.HeaderTabPanel.SetActiveTab<CaseTypeTabComponent>(LitigationAnalyticsChartHeaderTab.CaseType);
            var fullTableViewExperiencetab = experienceTab.ContentViewTabPanel.ChartViewContainer.TableContentComponent.ClickOnShowFullTable<LitigationAnalyticsProfilerPage>();

            SafeMethodExecutor.WaitUntil(() => !fullTableViewExperiencetab.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 40);
            string currentTabName = fullTableViewExperiencetab.ProfileTabPanel.CurrentTabName;
            var tableRowCountOnFullTableViewTab = fullTableViewExperiencetab.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience).ContentViewTabPanel.ChartViewContainer.TableContentComponent.TableContentItemsList.Count;

            this.TestCaseVerify.IsTrue(
            FullTableViewVerify,
            tableRowCount != tableRowCountOnFullTableViewTab && currentTabName.Equals("Experience"),
            $"Verify that Experience tab Show full table view link isn't opened new tab with full table. Table row count on LA page {tableRowCount}, count on full table page {tableRowCountOnFullTableViewTab}");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void IndustryDeliveryTest()
        {
            const string IndustryName = "Information";
            const string DeliveriedDocumentChartNamesVerify = "Verify that downloaded document contains the Filter Industry item";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var industryTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCompaniesEntityTabComponent>(LitigationAnalyticsProfiles.Companies);
            industryTabComponent.ByIndustryRadioButton.Select();
            var industrySelectingDialog = industryTabComponent.SelectIndustryButton.Click<LitigationAnalyticsIndustrySelectingDialog>();
            industrySelectingDialog.NaiscRadiobutton.Select();
            industrySelectingDialog.IndustrySearchBox.SetText(IndustryName);
            SafeMethodExecutor.WaitUntil(() => industrySelectingDialog.IndustryResultItems.Any(), 30);
            industrySelectingDialog.IndustryResultItems.First(element => element.GetIndustryName.Contains(IndustryName)).IndustryName.Click();
            industryTabComponent = industrySelectingDialog.SaveButton.Click<LitigationAnalyticsCompaniesEntityTabComponent>();
            var analyticsProfilerPage = industryTabComponent.SearchButton.Click<LitigationAnalyticsProfilerPage>();

            string reportName = $"Westlaw Precision - Litigation Analytics Report for {IndustryName}.pdf";
            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 50);
            var profilePage = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            SafeMethodExecutor.WaitUntil(() => !profilePage.LoadingSpinner.Displayed, 30);

            var downloadDialog = profilePage.ContentViewTabPanel.TitleToolBarComponent.DeliveryDropdown.SelectOption<LitigationAnalyticsDownloadDialog>(DeliveryMethod.Download);
            this.DownloadCurrentViewReport(downloadDialog, reportName);
            string textDelivery = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, reportName));

            this.TestCaseVerify.IsTrue(
                DeliveriedDocumentChartNamesVerify,
                    textDelivery.Contains(IndustryName),
                    "Downloaded document does not contain the Filter item");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void IndustryChartFilterMultiToggleTest()
        {
            const string IndustryName = "Information";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var industryTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCompaniesEntityTabComponent>(LitigationAnalyticsProfiles.Companies);
            industryTabComponent.ByIndustryRadioButton.Select();
            var industrySelectingDialog = industryTabComponent.SelectIndustryButton.Click<LitigationAnalyticsIndustrySelectingDialog>();
            industrySelectingDialog.NaiscRadiobutton.Select();
            industrySelectingDialog.IndustrySearchBox.SetText(IndustryName);
            SafeMethodExecutor.WaitUntil(() => industrySelectingDialog.IndustryResultItems.Any(), 40);
            industrySelectingDialog.IndustryResultItems.First(element => element.GetIndustryName.Contains(IndustryName)).IndustryName.Click();
            industryTabComponent = industrySelectingDialog.SaveButton.Click<LitigationAnalyticsCompaniesEntityTabComponent>();
            var analyticsProfilerPage = industryTabComponent.SearchButton.Click<LitigationAnalyticsProfilerPage>();

            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 30);
            var experience = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            SafeMethodExecutor.WaitUntil(() => !experience.LoadingSpinner.Displayed, 30);
            experience.NarrowPane.SelectMultipleFiltersToggle.ScrollToElement();

            if (experience.NarrowPane.ApplyButton.Displayed)
            {
                experience.NarrowPane.SelectMultipleFiltersToggle.ToggleState(true);
            }

            int experienceDocketsBeforeCount = experience.ResultListComponent.GetDocketsCount();
            var courtFacet = experience.NarrowPane.SearchFacets.CascadingFacetButton(LitigationAnalyticsCascadingFacets.Court).Click<LitigationAnalyticsFacetDialog>();
            var facetItem = courtFacet.FacetResultItems.First(item => item.SearchFacetLabelText.Text.Equals("Federal"));
            facetItem.SearchFacetCheckbox.ScrollToElement();
            facetItem.SearchFacetCheckbox.Set(true);

            int experienceDocketsAfterCount = experience.ResultListComponent.GetDocketsCount();

            this.TestCaseVerify.AreNotSame(
                "Chart filtering by Industry Entity works correctly in Experience tab when Multiple Filters are Disabled.",
                experienceDocketsBeforeCount, experienceDocketsAfterCount,
                "Chart filtering by Industry Entity does not work correctly in Experience tab when Multiple Filters are Disabled.");

            BrowserPool.CurrentBrowser.Refresh();

            if (!experience.NarrowPane.ApplyButton.Displayed)
            {
                experience.NarrowPane.SelectMultipleFiltersToggle.ToggleState(true);
            }
            experienceDocketsBeforeCount = experience.ResultListComponent.GetDocketsCount();

            courtFacet = experience.NarrowPane.SearchFacets.CascadingFacetButton(LitigationAnalyticsCascadingFacets.Court).Click<LitigationAnalyticsFacetDialog>();
            facetItem = courtFacet.FacetResultItems.First(item => item.SearchFacetLabelText.Text.Equals("State"));
            facetItem.SearchFacetCheckbox.ScrollToElement();
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
                "Chart filtering by Industry Entity works correctly in Experience tab when Multiple Filters are Enabled.",
                experienceDocketsBeforeCount, experienceDocketsAfterCount,
                "Chart filtering by Industry Entity does not work correctly in Experience tab when Multiple Filters are Enabled.");
        }
    }
}