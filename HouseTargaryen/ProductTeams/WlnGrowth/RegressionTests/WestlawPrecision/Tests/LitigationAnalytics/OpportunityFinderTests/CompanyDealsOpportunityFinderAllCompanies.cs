using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.OpportunityFinderComponents;
using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs;
using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics;
using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics.OpportunityFinder;
using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
using Framework.Common.UI.Products.WestlawEdgePremium.Pages.LitigationAnalytics;
using Framework.Common.UI.Utils;
using Framework.Core.Utils.Execution;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace WestlawPrecision.Tests.LitigationAnalytics.OpportunityFinderTests
{
    [TestClass]
    public class CompanyDealsOpportunityFinderAllCompanies : BaseOpportunityFinderTest
    {
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void OpportunityFinderDealsAllCompaniesDownloadReportCancelButtonTest()
        {
            const string DownloadDialogCancelButtonVerify = "Verify: Cancel button closed download dialog";

            var opportunityFinderPage = this.SelectMergersAndAcquisitions(OpportunityFinderCompanydropdownItems.AllCompanies);

            opportunityFinderPage.StepFirstLegalActivityComponent.IncludeActivityWithinTheLastRadiobutton(OpportunityFinderIncludeActivityWithinTheLast.ThreeYears).Select();
            var companyAttributesComponent = opportunityFinderPage.ContinueButton().Click<CompanyAttributesComponent>();
            var filterDialog = companyAttributesComponent.Filters.FilterItemButton("Industry code").Click<OpportunityFinderFilterDialog>();
            filterDialog.EnterSearchQuery("73 Business Services");
            filterDialog.OpportunityFinderFacetResultItems.First().SearchFacetCheckbox.Set(true);

            var stepThree = opportunityFinderPage.ContinueButton().Click<OpportunityFinderCustomizeAndViewInTheReportComponent>();
            stepThree.ColumnItem(OpportunityFinderColumnsToDisplay.City).Set(true);

            var customizedReportPage = opportunityFinderPage.ContinueButton().Click<LitigationAnalyticsOpportunityFinderCustomizedReportPage>();
            customizedReportPage.DownloadReportButton.ScrollToElement();
            var downloadDialog = customizedReportPage.DownloadReportButton.Click<LitigationAnalyticsDownloadDialog>();
            downloadDialog.ClickCancel<LitigationAnalyticsOpportunityFinderCustomizedReportPage>();

            this.TestCaseVerify.IsFalse(
                DownloadDialogCancelButtonVerify,
                downloadDialog.IsDownloadDialogDisplayed(),
                "Download dialog is displayed");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void OpportunityFinderDealsAllCompaniesDownloadReportTest()
        {
            const string DownloadReportVerify = "Verify: report is downloaded to folder";
            const string reportName = "Westlaw Precision - Litigation Analytics Report for Opportunity Finder.csv";

            var opportunityFinderPage = this.SelectMergersAndAcquisitions(OpportunityFinderCompanydropdownItems.AllCompanies);

            opportunityFinderPage.StepFirstLegalActivityComponent.IncludeActivityWithinTheLastRadiobutton(OpportunityFinderIncludeActivityWithinTheLast.FiveYears).Select();
            var companyAttributesComponent = opportunityFinderPage.ContinueButton().Click<CompanyAttributesComponent>();
            var filterDialog = companyAttributesComponent.Filters.FilterItemButton("Industry code").Click<OpportunityFinderFilterDialog>();
            filterDialog.EnterSearchQuery("73 Business Services");
            filterDialog.OpportunityFinderFacetResultItems.First().SearchFacetCheckbox.Set(true);

            var stepThree = opportunityFinderPage.ContinueButton().Click<OpportunityFinderCustomizeAndViewInTheReportComponent>();
            stepThree.ColumnItem(OpportunityFinderColumnsToDisplay.WebsiteUrl).Set(true);

            var customizedReportPage = opportunityFinderPage.ContinueButton().Click<LitigationAnalyticsOpportunityFinderCustomizedReportPage>();
            customizedReportPage.DownloadReportButton.ScrollToElement();
            var downloadDialog = customizedReportPage.DownloadReportButton.Click<LitigationAnalyticsDownloadDialog>();
            downloadDialog.ClickDownloadButton<LitigationAnalyticsReadyForDeliveryDialog>().DownloadButton.Click<ExperienceProfileTabComponent>();
            FileUtils.WaitForFileDownload(FolderToSave, reportName);

            this.TestCaseVerify.IsFalse(
                DownloadReportVerify,
                downloadDialog.IsDownloadDialogDisplayed(),
                "Download dialog is not displayed for Private Only");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void OpportunityFinderDealsAllCompaniesDisplayPerPageTest()
        {
            const string DisplayPerPageVerify = "Verify: Rows displayed per page";

            var opportunityFinderPage = this.SelectMergersAndAcquisitions(OpportunityFinderCompanydropdownItems.AllCompanies);

            opportunityFinderPage.StepFirstLegalActivityComponent.IncludeActivityWithinTheLastRadiobutton(OpportunityFinderIncludeActivityWithinTheLast.FiveYears).Select();
            var companyAttributesComponent = opportunityFinderPage.ContinueButton().Click<CompanyAttributesComponent>();
            var filterDialog = companyAttributesComponent.Filters.FilterItemButton("Transaction status").Click<OpportunityFinderFilterDialog>();
            SafeMethodExecutor.WaitUntil(() => filterDialog.OpportunityFinderFacetResultItems.Any(), 40);
            filterDialog.OpportunityFinderFacetResultItems.First(item => item.SearchFacetLabelText.Text.Equals("Completed")).SearchFacetCheckbox.Set(true);

            var stepThree = opportunityFinderPage.ContinueButton().Click<OpportunityFinderCustomizeAndViewInTheReportComponent>();
            stepThree.ColumnItem(OpportunityFinderColumnsToDisplay.City).Set(true);
            var customizedReportPage = opportunityFinderPage.ContinueButton().Click<LitigationAnalyticsOpportunityFinderCustomizedReportPage>();
            customizedReportPage.SelectTableRowCountPerPage(OpportunityFinderRowCountPerPage.OneHundred);

            this.TestCaseVerify.IsTrue(
                DisplayPerPageVerify,
                customizedReportPage.TableRowItems.Count.Equals(Convert.ToInt32(OpportunityFinderRowCountPerPage.OneHundred)),
                "Rows doesn't displayed per page");

            customizedReportPage.SelectTableRowCountPerPage(OpportunityFinderRowCountPerPage.Twenty);

            this.TestCaseVerify.IsTrue(
                DisplayPerPageVerify,
                customizedReportPage.TableRowItems.Count.Equals(Convert.ToInt32(OpportunityFinderRowCountPerPage.Twenty)),
                "Rows doesn't displayed per page");

            customizedReportPage.SelectTableRowCountPerPage(OpportunityFinderRowCountPerPage.Fifty);

            this.TestCaseVerify.IsTrue(
                DisplayPerPageVerify,
                customizedReportPage.TableRowItems.Count.Equals(Convert.ToInt32(OpportunityFinderRowCountPerPage.Fifty)),
                "Rows doesn't displayed per page");

            customizedReportPage.SelectTableRowCountPerPage(OpportunityFinderRowCountPerPage.Ten);

            this.TestCaseVerify.IsTrue(
                DisplayPerPageVerify,
                customizedReportPage.TableRowItems.Count.Equals(Convert.ToInt32(OpportunityFinderRowCountPerPage.Ten)),
                "Rows doesn't displayed per page");
        }
    }
}
