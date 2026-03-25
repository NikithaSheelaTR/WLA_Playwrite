namespace WestlawPrecision.Tests.LitigationAnalytics.OpportunityFinderTests
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.OpportunityFinderComponents;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics.OpportunityFinder;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.LitigationAnalytics;
    using Framework.Common.UI.Utils;
    using Framework.Common.UI.Utils.Browser;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public class CompanyDealsOpportunityFinderPublicOnlyTests : BaseOpportunityFinderTest
    {
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void CompanyDealsOpportunityFinderMAPublicOnlyBackButtonTest()
        {
            const string BackButtonVerify = "Verify that filters in My Selections list equals after click on back button";

            var opportunityFinderPage = this.SelectMergersAndAcquisitions(OpportunityFinderCompanydropdownItems.PublicOnly);
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
                BackButtonVerify,
                opportunityFinderPage.MySelectionsFiltersText.SequenceEqual(filtersInMySelectionsList),
                "Verify that filters in My Selections list are not equal after click on back button to third");

            opportunityFinderPage.BackButton().Click();

            this.TestCaseVerify.IsTrue(
                BackButtonVerify,
                opportunityFinderPage.MySelectionsFiltersText.SequenceEqual(filtersInMySelectionsListSecondStep),
                "Verify that filters in My Selections list are not equal after click on back button to second step");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void CompanyDealsOpportunityFinderMAPublicOnlyWebsiteURLTest()
        {
            const string WebsiteURLVerify = "Verify that Website URL works on customized report page";
            const string ExpectedPartOfUrl = "Analytics/OpportunityFinder";

            var opportunityFinderPage = this.SelectMergersAndAcquisitions(OpportunityFinderCompanydropdownItems.PublicOnly);
            opportunityFinderPage.StepFirstLegalActivityComponent.IncludeActivityWithinTheLastRadiobutton(OpportunityFinderIncludeActivityWithinTheLast.OneYear).Select();
            var companyAttributesComponent = opportunityFinderPage.ContinueButton().Click<CompanyAttributesComponent>();
            var filterDialog = companyAttributesComponent.Filters.FilterItemButton("Revenue").Click<OpportunityFinderFilterDialog>();
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
                WebsiteURLVerify,
                BrowserPool.CurrentBrowser.Url.Contains(ExpectedPartOfUrl),
                "Verify that that Website URL doesn't work on customized report page");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void CompanyDealsOpportunityFinderMAPublicOnlyDisplayPerPageTest()
        {
            const string RowCountVerify = "Verify that Rows displayed per page";

            var opportunityFinderPage = this.SelectMergersAndAcquisitions(OpportunityFinderCompanydropdownItems.PublicOnly);
            opportunityFinderPage.StepFirstLegalActivityComponent.IncludeActivityWithinTheLastRadiobutton(OpportunityFinderIncludeActivityWithinTheLast.FiveYears).Select();
            var companyAttributesComponent = opportunityFinderPage.ContinueButton().Click<CompanyAttributesComponent>();
            var filterDialog = companyAttributesComponent.Filters.FilterItemButton("Total employees").Click<OpportunityFinderFilterDialog>();
            filterDialog.SelectDropdownItem(AllAmountsDropdownItems.AllAmountsGreaterThan);
            filterDialog.SelectAmount("10");
            var stepThree = opportunityFinderPage.ContinueButton().Click<OpportunityFinderCustomizeAndViewInTheReportComponent>();
            stepThree.ColumnItem(OpportunityFinderColumnsToDisplay.WebsiteUrl).Set(true);
            var customizedReportPage = opportunityFinderPage.ContinueButton().Click<LitigationAnalyticsOpportunityFinderCustomizedReportPage>();
            customizedReportPage.SelectTableRowCountPerPage(OpportunityFinderRowCountPerPage.Ten);

            this.TestCaseVerify.IsTrue(
                RowCountVerify,
                customizedReportPage.TableRowItems.Count.Equals(Convert.ToInt32(OpportunityFinderRowCountPerPage.Ten)),
                "Rows doesn't displayed per page");

            customizedReportPage.SelectTableRowCountPerPage(OpportunityFinderRowCountPerPage.Twenty);

            this.TestCaseVerify.IsTrue(
                RowCountVerify,
                customizedReportPage.TableRowItems.Count.Equals(Convert.ToInt32(OpportunityFinderRowCountPerPage.Twenty)),
                "Rows doesn't displayed per page");

            customizedReportPage.SelectTableRowCountPerPage(OpportunityFinderRowCountPerPage.Fifty);

            this.TestCaseVerify.IsTrue(
                RowCountVerify,
                customizedReportPage.TableRowItems.Count.Equals(Convert.ToInt32(OpportunityFinderRowCountPerPage.Fifty)),
                "Rows doesn't displayed per page");

            customizedReportPage.SelectTableRowCountPerPage(OpportunityFinderRowCountPerPage.OneHundred);

            this.TestCaseVerify.IsTrue(
                RowCountVerify,
                customizedReportPage.TableRowItems.Count.Equals(Convert.ToInt32(OpportunityFinderRowCountPerPage.OneHundred)),
                "Rows doesn't displayed per page");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void CompanyDealsMACustomizedReportDownloadReportCancelButtonTest()
        {
            const string CancelButtonVerify = "Verify that 'Cancel' button closed delivery window";

            var opportunityFinderPage = this.SelectMergersAndAcquisitions(OpportunityFinderCompanydropdownItems.PublicOnly);
            opportunityFinderPage.StepFirstLegalActivityComponent.IncludeActivityWithinTheLastRadiobutton(OpportunityFinderIncludeActivityWithinTheLast.OneYear).Select();
            var companyAttributesComponent = opportunityFinderPage.ContinueButton().Click<CompanyAttributesComponent>();
            var filterDialog = companyAttributesComponent.Filters.FilterItemButton("Transaction value").Click<OpportunityFinderFilterDialog>();
            filterDialog.EnterSearchQuery("$1m – $10m");
            filterDialog.OpportunityFinderFacetResultItems.First().SearchFacetCheckbox.Set(true);
            var stepThree = opportunityFinderPage.ContinueButton().Click<OpportunityFinderCustomizeAndViewInTheReportComponent>();
            stepThree.ColumnItem(OpportunityFinderColumnsToDisplay.Country).Set(true);
            var filtersInMySelectionsList = opportunityFinderPage.MySelectionsFiltersText;
            var customizedReportPage = opportunityFinderPage.ContinueButton().Click<LitigationAnalyticsOpportunityFinderCustomizedReportPage>();
            customizedReportPage.DownloadReportButton.ScrollToElement();
            var downloadDialog = customizedReportPage.DownloadReportButton.Click<LitigationAnalyticsDownloadDialog>();
            downloadDialog.ClickCancel<LitigationAnalyticsOpportunityFinderCustomizedReportPage>();

            this.TestCaseVerify.IsFalse(
                CancelButtonVerify,
                downloadDialog.IsDownloadDialogDisplayed(),
                "Verify that download dialog is not displayed");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void CompanyDealsOpportunityFinderMACustomizedReportDownloadReportTest()
        {
            const string DownloadButtonVerify = "Verify that Download button deliveried report";
            const string reportName = "Westlaw Precision - Litigation Analytics Report for Opportunity Finder.csv";

            var opportunityFinderPage = this.SelectMergersAndAcquisitions(OpportunityFinderCompanydropdownItems.PublicOnly);
            opportunityFinderPage.StepFirstLegalActivityComponent.IncludeActivityWithinTheLastRadiobutton(OpportunityFinderIncludeActivityWithinTheLast.FiveYears).Select();
            var companyAttributesComponent = opportunityFinderPage.ContinueButton().Click<CompanyAttributesComponent>();
            var filterDialog = companyAttributesComponent.Filters.FilterItemButton("Location").Click<OpportunityFinderFilterDialog>();
            filterDialog.EnterSearchQuery("New York");
            filterDialog.OpportunityFinderFacetResultItems.First().SearchFacetCheckbox.Set(true);
            var stepThree = opportunityFinderPage.ContinueButton().Click<OpportunityFinderCustomizeAndViewInTheReportComponent>();
            stepThree.ColumnItem(OpportunityFinderColumnsToDisplay.CompanyName).Set(true);
            var filtersInMySelectionsList = opportunityFinderPage.MySelectionsFiltersText;
            var customizedReportPage = opportunityFinderPage.ContinueButton().Click<LitigationAnalyticsOpportunityFinderCustomizedReportPage>();
            customizedReportPage.DownloadReportButton.ScrollToElement();
            var downloadDialog = customizedReportPage.DownloadReportButton.Click<LitigationAnalyticsDownloadDialog>();
            downloadDialog.ClickDownloadButton<LitigationAnalyticsReadyForDeliveryDialog>().DownloadButton.Click<ExperienceProfileTabComponent>();
            FileUtils.WaitForFileDownload(FolderToSave, reportName);

            this.TestCaseVerify.IsFalse(
                DownloadButtonVerify,
                downloadDialog.IsDownloadDialogDisplayed(),
                "download dialog is not displayed");
        }
    }
}
