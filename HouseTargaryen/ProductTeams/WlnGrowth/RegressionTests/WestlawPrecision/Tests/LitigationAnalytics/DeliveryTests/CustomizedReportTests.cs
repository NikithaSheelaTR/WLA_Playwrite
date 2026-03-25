namespace WestlawPrecision.Tests.LitigationAnalytics.DeliveryTests
{
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsHomePageComponents.Entities;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.LitigationAnalytics;
    using Framework.Common.UI.Raw.EnhancementTests.Utils;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Execution;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;
    using System.Linq;


    [TestClass]
    public class CustomizedReportTests : BaseDeliveryTest
    {
        private const string CurrentFeatureTestCategory = "CustomizedReportDeliveryTests";
        const string DeliveriedDocumentTitleVerify = "Verify that delivered document contains Title";

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(CurrentFeatureTestCategory)]
        public void CustomizedReportAttorneysDeliveryTest()
        {
            const string Query = "Garrett D. Blanchfield ";

            var attorneysTabComponent = this.OpenLitigationAnalyticsPage().HeaderPanel.SetActiveTab<LitigationAnalyticsAttorneysEntityTabComponent>(LitigationAnalyticsProfiles.Attorneys);
            attorneysTabComponent.ByNameRadiobutton.Select();
            var analyticsProfilerPage = attorneysTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query)
                .TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            string reportName = $"Westlaw Precision - Litigation Analytics Report for {analyticsProfilerPage.AnalyticsTitle.Replace(".", "")}.pdf";
            var experienceTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            experienceTab.ContentViewTabPanel.ChartViewContainer.ShowMoreButton.Click();
            var downloadDialog = experienceTab.ContentViewTabPanel.TitleToolBarComponent.DeliveryDropdown.SelectOption<LitigationAnalyticsDownloadDialog>(DeliveryMethod.Download);

            this.DownloadCustomizedReport(downloadDialog, reportName);

            string textDelivery = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, reportName));

            this.TestCaseVerify.IsTrue(
                DeliveriedDocumentTitleVerify,
                    textDelivery.Contains(analyticsProfilerPage.ProfileTabPanel.CurrentTabName),
                    "Downloaded document does not contain filtered item with note");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(CurrentFeatureTestCategory)]
        public void CustomizedReportLawFirmDeliveryTest()
        {
            const string Query = "New York City";

            var attorneysTabComponent = this.OpenLitigationAnalyticsPage().HeaderPanel.SetActiveTab<LitigationAnalyticsLawFirmsEntityTabComponent>(LitigationAnalyticsProfiles.LawFirms);
            attorneysTabComponent.ByNameRadiobutton.Select();
            var analyticsProfilerPage = attorneysTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query)
                .TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            string reportName = $"Westlaw Precision - Litigation Analytics Report for {analyticsProfilerPage.AnalyticsTitle.Replace(".", "")}.pdf";
            var experienceTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            experienceTab.ContentViewTabPanel.ChartViewContainer.ShowMoreButton.Click();
            var downloadDialog = experienceTab.ContentViewTabPanel.TitleToolBarComponent.DeliveryDropdown.SelectOption<LitigationAnalyticsDownloadDialog>(DeliveryMethod.Download);

            this.DownloadCustomizedReport(downloadDialog, reportName);

            string textDelivery = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, reportName));

            this.TestCaseVerify.IsTrue(
                DeliveriedDocumentTitleVerify,
                    textDelivery.Contains(analyticsProfilerPage.ProfileTabPanel.CurrentTabName),
                    "Downloaded document does not contain filtered item with note");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(CurrentFeatureTestCategory)]
        public void CustomizedReportJudgesDeliveryTest()
        {
            const string Query = "Tom S. Lee";

            var attorneysTabComponent = this.OpenLitigationAnalyticsPage().HeaderPanel.SetActiveTab<LitigationAnalyticsJudgesEntityTabComponent>(LitigationAnalyticsProfiles.Judges);
            var analyticsProfilerPage = attorneysTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query)
                .TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            string reportName = $"Westlaw Precision - Litigation Analytics Report for {analyticsProfilerPage.AnalyticsTitle.Replace(".", "")}.pdf";
            var experienceTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            experienceTab.ContentViewTabPanel.ChartViewContainer.ShowMoreButton.Click();
            var downloadDialog = experienceTab.ContentViewTabPanel.TitleToolBarComponent.DeliveryDropdown.SelectOption<LitigationAnalyticsDownloadDialog>(DeliveryMethod.Download);

            this.DownloadCustomizedReport(downloadDialog, reportName);

            string textDelivery = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, reportName));

            this.TestCaseVerify.IsTrue(
                DeliveriedDocumentTitleVerify,
                    textDelivery.Contains(analyticsProfilerPage.ProfileTabPanel.CurrentTabName),
                    "Downloaded document does not contain filtered item with note");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(CurrentFeatureTestCategory)]
        public void CustomizedReportCaseTypeDeliveryTest()
        {
            const string Query = "Civil Rights";

            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            string reportName = $"Westlaw Precision - Litigation Analytics Report for {Query}.pdf";
            var analyticsProfilerPage = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCaseTypeEntityTabComponent>(LitigationAnalyticsProfiles.CaseTypes)
                .EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query).TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();

            var experienceTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            experienceTab.ContentViewTabPanel.ChartViewContainer.ShowMoreButton.Click();

            var downloadDialog = experienceTab.ContentViewTabPanel.TitleToolBarComponent.DeliveryDropdown.SelectOption<LitigationAnalyticsDownloadDialog>(DeliveryMethod.Download);

            this.DownloadCustomizedReport(downloadDialog, reportName);

            string textDelivery = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, reportName));

            this.TestCaseVerify.IsTrue(
            DeliveriedDocumentTitleVerify,
                    textDelivery.Contains(analyticsProfilerPage.ProfileTabPanel.CurrentTabName),
                    "Downloaded document does not contain filtered item with note");
        }

        //Bug 2110044: [Delivery]  Result list for Outcomes, Damages and Motions tabs are not filtered in downloaded document by Customize Report
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(CurrentFeatureTestCategory)]
        public void CustomizedReportCourtsFilteringDeliveryTest()
        {
            const string Query = "United States District Court District of Columbi";
            const string DocketswasFilteredonOutcomesVerify = "Verify that dockets was filtered on Outcomes tab";

            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var courtsTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCourtsEntityTabComponent>(LitigationAnalyticsProfiles.Courts);
            var typeahead = courtsTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            string reportName = $"Westlaw Precision - Litigation Analytics Report for {Query}.pdf";
            var experienceTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            SafeMethodExecutor.WaitUntil(() => !experienceTab.LoadingSpinner.Displayed, 50);
            var casetypefacet = experienceTab.NarrowPane.SearchFacets.SelectFacet(LitigationAnalyticsFacets.CaseType);
            var facetItem = casetypefacet.FacetResultItems.First();
            facetItem.SearchFacetCheckbox.ScrollToElement();
            facetItem.SearchFacetCheckbox.Set(true);
            int docketsCount = experienceTab.ResultListComponent.GetDocketsCount();
            var downloadDialog = experienceTab.ContentViewTabPanel.TitleToolBarComponent.DeliveryDropdown.SelectOption<LitigationAnalyticsDownloadDialog>(DeliveryMethod.Download);

            this.DownloadCustomizedReport(downloadDialog, reportName);

            string textDelivery = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, reportName)).ToString().Replace(",", "");

            this.TestCaseVerify.IsTrue(
                DocketswasFilteredonOutcomesVerify,
                    textDelivery.Contains($"Dockets (20 of {docketsCount})"),
                    "Downloaded document does not contain filtered item with note");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(CurrentFeatureTestCategory)]
        public void CustomizedReportCompanyDeliveryTest()
        {
            const string Query = "amazon";

            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var resultListPage = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCompaniesEntityTabComponent>(LitigationAnalyticsProfiles.Companies)
                .EnterSearchQueryAndClickSearch<CompareResultListPage>(Query);
            resultListPage.CompanySelectionComponent.CompaniesResultListItems.First().InputCheckBox.Set(true);
            DriverExtensions.ScrollPageToBottom();
            var experienceTab = resultListPage.MySelectionsComponent.CreateReportButton.Click<LitigationAnalyticsProfilerPage>().ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            string reportName = $"Westlaw Precision - Litigation Analytics Report for {Query}.pdf";

            experienceTab.ContentViewTabPanel.ChartViewContainer.ShowMoreButton.Click();
            var downloadDialog = experienceTab.ContentViewTabPanel.TitleToolBarComponent.DeliveryDropdown.SelectOption<LitigationAnalyticsDownloadDialog>(DeliveryMethod.Download);

            this.DownloadCustomizedReport(downloadDialog, reportName);

            string textDelivery = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, reportName));

            this.TestCaseVerify.IsTrue(
                DeliveriedDocumentTitleVerify,
                    textDelivery.Contains(Query),
                    "Downloaded document does not contain filtered item with note");
        }

        //Bug 2113020 : [Law firm compare] - Delivery fails
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(CurrentFeatureTestCategory)]
        public void CustomizedReportLawFirmsCompareTest()
        {
            const string Query = "Barnes And Thornburg LLP";
            const string Query2 = "Taft Stettinius & Hollister LLP";
            const string DeliveriedDocumentTitleVerify = "Verify that Compare works correctly and We should able to Download the Customized Report";

            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var lawFirmsTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsLawFirmsEntityTabComponent>(LitigationAnalyticsProfiles.LawFirms);
            DriverExtensions.ScrollPageToBottom();
            lawFirmsTabComponent.CompareRadiobutton.Select();
            var resultListPage = lawFirmsTabComponent.EnterSearchQueryAndClickSearch<CompareResultListPage>(Query);
            resultListPage.CompareResultComponent.SetCheckboxByIndex(0, true);
            resultListPage = lawFirmsTabComponent.EnterSearchQueryAndClickSearch<CompareResultListPage>(Query2);
            resultListPage.CompareResultComponent.SetCheckboxByIndex(0, true);
            DriverExtensions.ScrollPageToBottom();

            var analyticsProfilerPage = resultListPage.MySelectionsComponent.CreateReportButton.Click<LitigationAnalyticsProfilerPage>();
            string reportName = $"Westlaw Precision - Litigation Analytics Report for {Query}.pdf";
            var experienceTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            experienceTab.ContentViewTabPanel.ChartViewContainer.ShowMoreButton.Click();
            var downloadDialog = experienceTab.ContentViewTabPanel.TitleToolBarComponent.DeliveryDropdown.SelectOption<LitigationAnalyticsDownloadDialog>(DeliveryMethod.Download);

            this.DownloadCustomizedReport(downloadDialog, reportName);

            string textDelivery = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, reportName));

            this.TestCaseVerify.IsTrue(
                DeliveriedDocumentTitleVerify,
                    textDelivery.Contains(analyticsProfilerPage.ProfileTabPanel.CurrentTabName),
                    "Downloaded document does not contain filtered item with note");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(CurrentFeatureTestCategory)]
        public void CustomizedReportCompanyOverviewDeliveryTest()
        {
            const string Query = "apple";
            const string CustomizedReportLabelName = "Overview";

            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var resultListPage = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCompaniesEntityTabComponent>(LitigationAnalyticsProfiles.Companies)
                .EnterSearchQueryAndClickSearch<CompareResultListPage>(Query);
            resultListPage.CompanySelectionComponent.CompaniesResultListItems.First().InputCheckBox.Set(true);
            DriverExtensions.ScrollPageToBottom();
            var motionsTab = resultListPage.MySelectionsComponent.CreateReportButton.Click<LitigationAnalyticsProfilerPage>().ProfileTabPanel.SetActiveTab<MotionsProfileTabComponent>(ProfileComponentTab.Motions);
            string reportName = $"Westlaw Precision - Litigation Analytics Report for {Query}.pdf";

            var downloadDialog = motionsTab.ContentViewTabPanel.TitleToolBarComponent.DeliveryDropdown.SelectOption<LitigationAnalyticsDownloadDialog>(DeliveryMethod.Download);

            this.DownloadCustomizedReportSelectedLabel(downloadDialog, reportName);

            string textDelivery = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, reportName));

            this.TestCaseVerify.IsTrue(
                DeliveriedDocumentTitleVerify,
                    textDelivery.Contains(CustomizedReportLabelName),
                    "Downloaded document does not contain Overview Tab");
        }

    }
}