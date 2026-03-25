namespace WestlawPrecision.Tests.LitigationAnalytics.OpportunityFinderTests
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsHomePageComponents.Entities;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.OpportunityFinderComponents;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics.OpportunityFinder;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.LitigationAnalytics;
    using Framework.Common.UI.Utils;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Execution;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public class OpportunityFinderAllCompaniesTests : BaseAnalyticsTest
    {
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void OpportunityFinderAllCompaniesBackButtonTest()
        {
            const string backButtonVerify = "Verify that filters in My Selections list equals after click on back button";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var opportunityFinderTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsOpportunityFinderEntityTabComponent>(LitigationAnalyticsProfiles.OpportunityFinder);
            opportunityFinderTabComponent.CompanyMenuButton.Click<LitigationAnalyticsOpportunityFinderEntityTabComponent>().CompanyMenuItem(OpportunityFinderCompanydropdownItems.AllCompanies).Click();
            var opportunityFinderPage = opportunityFinderTabComponent.ContinueButton.Click<LitigationAnalyticsOpportunityFinderProfilerPage>();
            opportunityFinderPage.StepFirstLegalActivityComponent.IncludeActivityWithinTheLastRadiobutton(OpportunityFinderIncludeActivityWithinTheLast.OneYear).Select();
            var filtersInMySelectionsListFirstStep = opportunityFinderPage.MySelectionsFiltersText;
            var companyAttributesComponent = opportunityFinderPage.ContinueButton().Click<CompanyAttributesComponent>();
            var filterDialog = companyAttributesComponent.Filters.FilterItemButton("Location").Click<OpportunityFinderFilterDialog>();
            filterDialog.EnterSearchQuery("New York");
            filterDialog.OpportunityFinderFacetResultItems.First().SearchFacetCheckbox.Set(true);
            var filtersInMySelectionsListSecondStep = opportunityFinderPage.MySelectionsFiltersText;
            var stepThree = opportunityFinderPage.ContinueButton().Click<OpportunityFinderCustomizeAndViewInTheReportComponent>();
            stepThree.ColumnItem(OpportunityFinderColumnsToDisplay.Country).Set(true);
            var filtersInMySelectionsList = opportunityFinderPage.MySelectionsFiltersText;
            opportunityFinderPage.ContinueButton().Click();
            opportunityFinderPage.BackButton().Click();

            this.TestCaseVerify.IsTrue(
                backButtonVerify,
                opportunityFinderPage.MySelectionsFiltersText.SequenceEqual(filtersInMySelectionsList),
                "Verify that filters in My Selections list are not equal after click on back button to third");

            opportunityFinderPage.BackButton().Click();

            this.TestCaseVerify.IsTrue(
                backButtonVerify,
                opportunityFinderPage.MySelectionsFiltersText.SequenceEqual(filtersInMySelectionsListSecondStep),
                "Verify that filters in My Selections list are not equal after click on back button to second step");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void OpportunityFinderAllCompaniesDisplayPerPageTest()
        {

            const string backButtonVerify = "Verify that Rows displayed per page";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var opportunityFinderTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsOpportunityFinderEntityTabComponent>(LitigationAnalyticsProfiles.OpportunityFinder);
            opportunityFinderTabComponent.CompanyMenuButton.Click<LitigationAnalyticsOpportunityFinderEntityTabComponent>().CompanyMenuItem(OpportunityFinderCompanydropdownItems.AllCompanies).Click();
            var opportunityFinderPage = opportunityFinderTabComponent.ContinueButton.Click<LitigationAnalyticsOpportunityFinderProfilerPage>();
            opportunityFinderPage.StepFirstLegalActivityComponent.IncludeActivityWithinTheLastRadiobutton(OpportunityFinderIncludeActivityWithinTheLast.ThreeYears).Select();
            var companyAttributesComponent = opportunityFinderPage.ContinueButton().Click<CompanyAttributesComponent>();
            var filterDialog = companyAttributesComponent.Filters.FilterItemButton("Legal activity thresholds").Click<OpportunityFinderFilterDialog>();
            filterDialog.SelectDropdownItem(AllAmountsDropdownItems.AllAmountsGreaterThan);
            filterDialog.SelectAmount("10");
            var stepThree = opportunityFinderPage.ContinueButton().Click<OpportunityFinderCustomizeAndViewInTheReportComponent>();
            stepThree.ColumnItem(OpportunityFinderColumnsToDisplay.Country).Set(true);
            var customizedReportPage = opportunityFinderPage.ContinueButton().Click<LitigationAnalyticsOpportunityFinderCustomizedReportPage>();
            customizedReportPage.SelectTableRowCountPerPage(OpportunityFinderRowCountPerPage.Ten);

            this.TestCaseVerify.IsTrue(
                backButtonVerify,
                customizedReportPage.TableRowItems.Count.Equals(Convert.ToInt32(OpportunityFinderRowCountPerPage.Ten)),
                "Verify that that Rows doesn't displayed per page");

            customizedReportPage.SelectTableRowCountPerPage(OpportunityFinderRowCountPerPage.Twenty);

            this.TestCaseVerify.IsTrue(
                backButtonVerify,
                customizedReportPage.TableRowItems.Count.Equals(Convert.ToInt32(OpportunityFinderRowCountPerPage.Twenty)),
                "Verify that that Rows doesn't displayed per page");

            customizedReportPage.SelectTableRowCountPerPage(OpportunityFinderRowCountPerPage.Fifty);

            this.TestCaseVerify.IsTrue(
                backButtonVerify,
                customizedReportPage.TableRowItems.Count.Equals(Convert.ToInt32(OpportunityFinderRowCountPerPage.Fifty)),
                "Verify that that Rows doesn't displayed per page");

            customizedReportPage.SelectTableRowCountPerPage(OpportunityFinderRowCountPerPage.OneHundred);

            this.TestCaseVerify.IsTrue(
                backButtonVerify,
                customizedReportPage.TableRowItems.Count.Equals(Convert.ToInt32(OpportunityFinderRowCountPerPage.OneHundred)),
                "Verify that that Rows doesn't displayed per page");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void OpportunityFinderAllCompaniesWebsiteURLTest()
        {
            const string backButtonVerify = "Verify that Website URL works on customized report page";
            const string ExpectedPartOfUrl = "Analytics/OpportunityFinder";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var opportunityFinderTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsOpportunityFinderEntityTabComponent>(LitigationAnalyticsProfiles.OpportunityFinder);
            opportunityFinderTabComponent.CompanyMenuButton.Click<LitigationAnalyticsOpportunityFinderEntityTabComponent>().CompanyMenuItem(OpportunityFinderCompanydropdownItems.AllCompanies).Click();
            var opportunityFinderPage = opportunityFinderTabComponent.ContinueButton.Click<LitigationAnalyticsOpportunityFinderProfilerPage>();
            opportunityFinderPage.StepFirstLegalActivityComponent.IncludeActivityWithinTheLastRadiobutton(OpportunityFinderIncludeActivityWithinTheLast.OneYear).Select();
            var companyAttributesComponent = opportunityFinderPage.ContinueButton().Click<CompanyAttributesComponent>();
            var filterDialog = companyAttributesComponent.Filters.FilterItemButton("Legal activity thresholds").Click<OpportunityFinderFilterDialog>();
            filterDialog.SelectDropdownItem(AllAmountsDropdownItems.AllAmountsLessThan);
            filterDialog.SelectAmount("10000000");
            var stepThree = opportunityFinderPage.ContinueButton().Click<OpportunityFinderCustomizeAndViewInTheReportComponent>();
            stepThree.ColumnItem(OpportunityFinderColumnsToDisplay.WebsiteUrl).Set(true);
            var customizedReportPage = opportunityFinderPage.ContinueButton().Click<LitigationAnalyticsOpportunityFinderCustomizedReportPage>();
            customizedReportPage.SelectTableRowItem(OpportunityFinderColumnsToDisplay.WebsiteUrl).Click();

            string tabName = BrowserPool.CurrentBrowser.TabHandles.Last();
            BrowserPool.CurrentBrowser.CreateTab(tabName);
            BrowserPool.CurrentBrowser.ActivateTab(tabName);

            this.TestCaseVerify.IsFalse(
                backButtonVerify,
                BrowserPool.CurrentBrowser.Url.Contains(ExpectedPartOfUrl),
                "Verify that that Website URL doesn't work on customized report page");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void OpportunityFinderAllCompaniesCustomizedReportDownloadReportTest()
        {
            const string backButtonVerify = "Verify that Download button deliveried report";
            const string reportName = "Westlaw Precision - Litigation Analytics Report for Opportunity Finder.csv";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var opportunityFinderTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsOpportunityFinderEntityTabComponent>(LitigationAnalyticsProfiles.OpportunityFinder);
            opportunityFinderTabComponent.CompanyMenuButton.Click<LitigationAnalyticsOpportunityFinderEntityTabComponent>().CompanyMenuItem(OpportunityFinderCompanydropdownItems.AllCompanies).Click();
            var opportunityFinderPage = opportunityFinderTabComponent.ContinueButton.Click<LitigationAnalyticsOpportunityFinderProfilerPage>();
            opportunityFinderPage.StepFirstLegalActivityComponent.IncludeActivityWithinTheLastRadiobutton(OpportunityFinderIncludeActivityWithinTheLast.OneYear).Select();
            var companyAttributesComponent = opportunityFinderPage.ContinueButton().Click<CompanyAttributesComponent>();
            var filterDialog = companyAttributesComponent.Filters.FilterItemButton("Industry code").Click<OpportunityFinderFilterDialog>();
            filterDialog.EnterSearchQuery("Administration Of Economic Programs");
            filterDialog.OpportunityFinderFacetResultItems.First().SearchFacetCheckbox.Set(true);
            filterDialog = companyAttributesComponent.Filters.FilterItemButton("Court").Click<OpportunityFinderFilterDialog>();
            filterDialog.OpportunityFinderFacetResultItems.First(item => item.SearchFacetLabelText.Text.Equals("State")).SearchFacetCheckbox.Set(true);
            var stepThree = opportunityFinderPage.ContinueButton().Click<OpportunityFinderCustomizeAndViewInTheReportComponent>();
            stepThree.ColumnItem(OpportunityFinderColumnsToDisplay.AuthorityID).Set(true);
            var filtersInMySelectionsList = opportunityFinderPage.MySelectionsFiltersText;
            var customizedReportPage = opportunityFinderPage.ContinueButton().Click<LitigationAnalyticsOpportunityFinderCustomizedReportPage>();
            customizedReportPage.DownloadReportButton.ScrollToElement();
            var downloadDialog = customizedReportPage.DownloadReportButton.Click<LitigationAnalyticsDownloadDialog>();
            downloadDialog.ClickDownloadButton<LitigationAnalyticsReadyForDeliveryDialog>().DownloadButton.Click<ExperienceProfileTabComponent>();
            FileUtils.WaitForFileDownload(FolderToSave, reportName);

            this.TestCaseVerify.IsFalse(
                backButtonVerify,
                downloadDialog.IsDownloadDialogDisplayed(),
                "Verify that download dialog is not displayed");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void OpportunityFinderAllCompaniesLimitTest()
        {
            const string backButtonVerify = "Verify that Download button deliveried report";
            const string LimitMessage = "The current selection would return too many results. Please apply more filters.";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var opportunityFinderTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsOpportunityFinderEntityTabComponent>(LitigationAnalyticsProfiles.OpportunityFinder);
            opportunityFinderTabComponent.CompanyMenuButton.Click<LitigationAnalyticsOpportunityFinderEntityTabComponent>().CompanyMenuItem(OpportunityFinderCompanydropdownItems.AllCompanies).Click();
            var opportunityFinderPage = opportunityFinderTabComponent.ContinueButton.Click<LitigationAnalyticsOpportunityFinderProfilerPage>();
            opportunityFinderPage.StepFirstLegalActivityComponent.IncludeActivityWithinTheLastRadiobutton(OpportunityFinderIncludeActivityWithinTheLast.FiveYears).Select();
            var companyAttributesComponent = opportunityFinderPage.ContinueButton().Click<CompanyAttributesComponent>();
            var filterDialog = companyAttributesComponent.Filters.FilterItemButton("Case type").Click<OpportunityFinderFilterDialog>();
            filterDialog.OpportunityFinderFacetResultItems.First().SearchFacetCheckbox.Set(true);
            var stepThree = opportunityFinderPage.ContinueButton().Click<OpportunityFinderCustomizeAndViewInTheReportComponent>();
            stepThree.ColumnItem(OpportunityFinderColumnsToDisplay.AuthorityID).Set(true);
            var customizedReportPage = opportunityFinderPage.ContinueButton().Click<LitigationAnalyticsOpportunityFinderCustomizedReportPage>();

            this.TestCaseVerify.IsTrue(
                backButtonVerify,
                customizedReportPage.LimitMessageLabel.Text.Equals(LimitMessage),
                "Verify that limit message is not displayed");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void CustomizedReportDownloadReportCancelButtonAllCompaniesTest()
        {
            const string backButtonVerify = "Verify that 'Cancel' button closed delivery window";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var opportunityFinderTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsOpportunityFinderEntityTabComponent>(LitigationAnalyticsProfiles.OpportunityFinder);
            opportunityFinderTabComponent.CompanyMenuButton.Click<LitigationAnalyticsOpportunityFinderEntityTabComponent>().CompanyMenuItem(OpportunityFinderCompanydropdownItems.AllCompanies).Click();
            var opportunityFinderPage = opportunityFinderTabComponent.ContinueButton.Click<LitigationAnalyticsOpportunityFinderProfilerPage>();
            opportunityFinderPage.StepFirstLegalActivityComponent.IncludeActivityWithinTheLastRadiobutton(OpportunityFinderIncludeActivityWithinTheLast.OneYear).Select();
            var companyAttributesComponent = opportunityFinderPage.ContinueButton().Click<CompanyAttributesComponent>();
            var filterDialog = companyAttributesComponent.Filters.FilterItemButton("Court").Click<OpportunityFinderFilterDialog>();            
            filterDialog.EnterSearchQuery("Federal");
            SafeMethodExecutor.Execute(() => filterDialog.OpportunityFinderFacetResultItems.First().IsCurrentItemDisplayed());
            filterDialog.OpportunityFinderFacetResultItems.First(item => item.SearchFacetLabelText.Text.Equals("Federal")).SearchFacetCheckbox.Set(true);
            var stepThree = opportunityFinderPage.ContinueButton().Click<OpportunityFinderCustomizeAndViewInTheReportComponent>();
            stepThree.ColumnItem(OpportunityFinderColumnsToDisplay.Country).Set(true);
            var filtersInMySelectionsList = opportunityFinderPage.MySelectionsFiltersText;
            var customizedReportPage = opportunityFinderPage.ContinueButton().Click<LitigationAnalyticsOpportunityFinderCustomizedReportPage>();
            SafeMethodExecutor.Execute(() => DriverExtensions.WaitForPageLoad());
            customizedReportPage.DownloadReportButton.ScrollToElement();
            var downloadDialog = customizedReportPage.DownloadReportButton.Click<LitigationAnalyticsDownloadDialog>();
            downloadDialog.ClickCancel<LitigationAnalyticsOpportunityFinderCustomizedReportPage>();

            this.TestCaseVerify.IsFalse(
                backButtonVerify,
                downloadDialog.IsDownloadDialogDisplayed(),
                "Cancel button closed delivery window dialog is not displayed.");
        }
    }
}
