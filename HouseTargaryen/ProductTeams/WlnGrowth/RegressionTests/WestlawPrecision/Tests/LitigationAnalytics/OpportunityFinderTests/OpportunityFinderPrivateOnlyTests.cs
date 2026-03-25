namespace WestlawPrecision.Tests.LitigationAnalytics.OpportunityFinderTests
{
    using Framework.Common.UI.Utils;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsHomePageComponents.Entities;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.OpportunityFinderComponents;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics.OpportunityFinder;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.LitigationAnalytics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    using Framework.Core.Utils.Execution;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    [TestClass]
    public class OpportunityFinderPrivateOnlyTests : BaseAnalyticsTest
    {
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void OpportunityFinderPrivateOnlyBackButtonTest()
        {
            const string backButtonVerify = "Verify that filters in My Selections list equals after click on back button";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var opportunityFinderTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsOpportunityFinderEntityTabComponent>(LitigationAnalyticsProfiles.OpportunityFinder);
            opportunityFinderTabComponent.CompanyMenuButton.Click<LitigationAnalyticsOpportunityFinderEntityTabComponent>().CompanyMenuItem(OpportunityFinderCompanydropdownItems.PrivateOnly).Click();
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

            opportunityFinderPage.BackButton().Click();
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void OpportunityFinderPrivateOnlyDisplayPerPageTest()
        {
            const string backButtonVerify = "Verify that Rows displayed per page";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var opportunityFinderTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsOpportunityFinderEntityTabComponent>(LitigationAnalyticsProfiles.OpportunityFinder);
            opportunityFinderTabComponent.CompanyMenuButton.Click<LitigationAnalyticsOpportunityFinderEntityTabComponent>().CompanyMenuItem(OpportunityFinderCompanydropdownItems.PrivateOnly).Click();
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
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void OpportunityFinderPrivateOnlyWebsiteURLTest()
        {
            const string backButtonVerify = "Verify that Website URL works on customized report page for Private only";
            const string ExpectedPartOfUrl = "Analytics/OpportunityFinder";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var opportunityFinderTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsOpportunityFinderEntityTabComponent>(LitigationAnalyticsProfiles.OpportunityFinder);
            opportunityFinderTabComponent.CompanyMenuButton.Click<LitigationAnalyticsOpportunityFinderEntityTabComponent>().CompanyMenuItem(OpportunityFinderCompanydropdownItems.PrivateOnly).Click();
            var opportunityFinderPage = opportunityFinderTabComponent.ContinueButton.Click<LitigationAnalyticsOpportunityFinderProfilerPage>();
            opportunityFinderPage.StepFirstLegalActivityComponent.IncludeActivityWithinTheLastRadiobutton(OpportunityFinderIncludeActivityWithinTheLast.FiveYears).Select();
            var companyAttributesComponent = opportunityFinderPage.ContinueButton().Click<CompanyAttributesComponent>();
            var filterDialog = companyAttributesComponent.Filters.FilterItemButton("Legal activity thresholds").Click<OpportunityFinderFilterDialog>();
            filterDialog.SelectDropdownItem(AllAmountsDropdownItems.AllAmountsLessThan);
            filterDialog.SelectAmount("1000000");
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
                "Website URL doesn't work on customized report page for Private only");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void OpportunityFinderPrivateOnlyDisplayRowCountPerPageTest()
        {
            const string backButtonVerify = "Verify that Rows displayed per page for Private only";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var opportunityFinderTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsOpportunityFinderEntityTabComponent>(LitigationAnalyticsProfiles.OpportunityFinder);
            opportunityFinderTabComponent.CompanyMenuButton.Click<LitigationAnalyticsOpportunityFinderEntityTabComponent>().CompanyMenuItem(OpportunityFinderCompanydropdownItems.PrivateOnly).Click();
            var opportunityFinderPage = opportunityFinderTabComponent.ContinueButton.Click<LitigationAnalyticsOpportunityFinderProfilerPage>();
            opportunityFinderPage.StepFirstLegalActivityComponent.IncludeActivityWithinTheLastRadiobutton(OpportunityFinderIncludeActivityWithinTheLast.ThreeYears).Select();
            var companyAttributesComponent = opportunityFinderPage.ContinueButton().Click<CompanyAttributesComponent>();
            var filterDialog = companyAttributesComponent.Filters.FilterItemButton("Legal activity thresholds").Click<OpportunityFinderFilterDialog>();
            filterDialog.SelectDropdownItem(AllAmountsDropdownItems.AllAmountsGreaterThan);
            filterDialog.SelectAmount("10000");
            var stepThree = opportunityFinderPage.ContinueButton().Click<OpportunityFinderCustomizeAndViewInTheReportComponent>();
            stepThree.ColumnItem(OpportunityFinderColumnsToDisplay.Country).Set(true);
            var customizedReportPage = opportunityFinderPage.ContinueButton().Click<LitigationAnalyticsOpportunityFinderCustomizedReportPage>();

            customizedReportPage.SelectTableRowCountPerPage(OpportunityFinderRowCountPerPage.Ten);
            this.TestCaseVerify.IsTrue(
                backButtonVerify,
                customizedReportPage.TableRowItems.Count.Equals(Convert.ToInt32(OpportunityFinderRowCountPerPage.Ten)),
                "Ten Rows doesn't displayed per page for Private only");

            customizedReportPage.SelectTableRowCountPerPage(OpportunityFinderRowCountPerPage.Twenty);
            this.TestCaseVerify.IsTrue(
                backButtonVerify,
                customizedReportPage.TableRowItems.Count.Equals(Convert.ToInt32(OpportunityFinderRowCountPerPage.Twenty)),
                "Twenty Rows doesn't displayed per page for Private only");

            customizedReportPage.SelectTableRowCountPerPage(OpportunityFinderRowCountPerPage.Fifty);
            this.TestCaseVerify.IsTrue(
                backButtonVerify,
                customizedReportPage.TableRowItems.Count.Equals(Convert.ToInt32(OpportunityFinderRowCountPerPage.Fifty)),
                "Fifty Rows doesn't displayed per page for Private only");

            customizedReportPage.SelectTableRowCountPerPage(OpportunityFinderRowCountPerPage.OneHundred);
            this.TestCaseVerify.IsTrue(
                backButtonVerify,
                customizedReportPage.TableRowItems.Count.Equals(Convert.ToInt32(OpportunityFinderRowCountPerPage.OneHundred)),
                "OneHundred Rows doesn't displayed per page for Private only");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void CustomizedReportDownloadReportCancelButtonTest()
        {
            const string backButtonVerify = "Verify that 'Cancel' button closed delivery window";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var opportunityFinderTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsOpportunityFinderEntityTabComponent>(LitigationAnalyticsProfiles.OpportunityFinder);
            opportunityFinderTabComponent.CompanyMenuButton.Click<LitigationAnalyticsOpportunityFinderEntityTabComponent>().CompanyMenuItem(OpportunityFinderCompanydropdownItems.PrivateOnly).Click();
            var opportunityFinderPage = opportunityFinderTabComponent.ContinueButton.Click<LitigationAnalyticsOpportunityFinderProfilerPage>();
            opportunityFinderPage.StepFirstLegalActivityComponent.IncludeActivityWithinTheLastRadiobutton(OpportunityFinderIncludeActivityWithinTheLast.OneYear).Select();
            var companyAttributesComponent = opportunityFinderPage.ContinueButton().Click<CompanyAttributesComponent>();
            var filterDialog = companyAttributesComponent.Filters.FilterItemButton("Court").Click<OpportunityFinderFilterDialog>();
            filterDialog.EnterSearchQuery("Federal");
            filterDialog.OpportunityFinderFacetResultItems.First(item => item.SearchFacetLabelText.Text.Equals("Federal")).SearchFacetCheckbox.Set(true);
            var stepThree = opportunityFinderPage.ContinueButton().Click<OpportunityFinderCustomizeAndViewInTheReportComponent>();
            stepThree.ColumnItem(OpportunityFinderColumnsToDisplay.Country).Set(true);
            var filtersInMySelectionsList = opportunityFinderPage.MySelectionsFiltersText;
            var customizedReportPage = opportunityFinderPage.ContinueButton().Click<LitigationAnalyticsOpportunityFinderCustomizedReportPage>();
            SafeMethodExecutor.Execute(() => DriverExtensions.WaitForPageLoad());
            var downloadDialog = customizedReportPage.DownloadReportButton.Click<LitigationAnalyticsDownloadDialog>();
            downloadDialog.ClickCancel<LitigationAnalyticsOpportunityFinderCustomizedReportPage>();

            this.TestCaseVerify.IsFalse(
                backButtonVerify,
                downloadDialog.IsDownloadDialogDisplayed(),
                "Verify that download dialog is not displayed");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        public void CustomizedReportDownloadReportPrivateOnlyTest()
        {
            const string backButtonVerify = "Verify that Download button deliveried report for Private Only";
            const string reportName = "Westlaw Precision - Litigation Analytics Report for Opportunity Finder.csv";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var opportunityFinderTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsOpportunityFinderEntityTabComponent>(LitigationAnalyticsProfiles.OpportunityFinder);
            opportunityFinderTabComponent.CompanyMenuButton.Click<LitigationAnalyticsOpportunityFinderEntityTabComponent>().CompanyMenuItem(OpportunityFinderCompanydropdownItems.PrivateOnly).Click();
            var opportunityFinderPage = opportunityFinderTabComponent.ContinueButton.Click<LitigationAnalyticsOpportunityFinderProfilerPage>();
            opportunityFinderPage.StepFirstLegalActivityComponent.IncludeActivityWithinTheLastRadiobutton(OpportunityFinderIncludeActivityWithinTheLast.ThreeYears).Select();
            var companyAttributesComponent = opportunityFinderPage.ContinueButton().Click<CompanyAttributesComponent>();
            var filterDialog = companyAttributesComponent.Filters.FilterItemButton("Legal role").Click<OpportunityFinderFilterDialog>();
            filterDialog.OpportunityFinderFacetResultItems.First(item => item.SearchFacetLabelText.Text.Equals("Attorney")).SearchFacetCheckbox.Set(true);
            var stepThree = opportunityFinderPage.ContinueButton().Click<OpportunityFinderCustomizeAndViewInTheReportComponent>();
            stepThree.ColumnItem(OpportunityFinderColumnsToDisplay.AuthorityID).Set(true);
            var customizedReportPage = opportunityFinderPage.ContinueButton().Click<LitigationAnalyticsOpportunityFinderCustomizedReportPage>();
            customizedReportPage.DownloadReportButton.ScrollToElement();
            var downloadDialog = customizedReportPage.DownloadReportButton.Click<LitigationAnalyticsDownloadDialog>();
            downloadDialog.ClickDownloadButton<LitigationAnalyticsReadyForDeliveryDialog>().DownloadButton.Click<ExperienceProfileTabComponent>();
            FileUtils.WaitForFileDownload(FolderToSave, reportName);

            this.TestCaseVerify.IsFalse(
                backButtonVerify,
                downloadDialog.IsDownloadDialogDisplayed(),
                "Download dialog is not displayed for Private Only");
        }
    }
}