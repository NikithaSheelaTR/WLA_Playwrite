namespace WestlawPrecision.Tests.LitigationAnalytics.DeliveryTests
{
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsHomePageComponents.Entities;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.LitigationAnalytics;
    using Framework.Common.UI.Raw.EnhancementTests.Utils;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;
    using System.Linq;

    [TestClass]
    public class DeliveryTests : BaseDeliveryTest
    {
        protected const string FeatureTestCategory = "LitigationAnalyticsDeliveryTests";

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        public void AttorneysExperienceDeliveryTest()
        {
            const string Query = "Garrett D. Blanchfield ";
            const string DeliveriedDocumentTitleVerify = "Verify that delivered document contains Title";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var attorneysTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsAttorneysEntityTabComponent>(LitigationAnalyticsProfiles.Attorneys);
            attorneysTabComponent.ByNameRadiobutton.Select();
            var typeahead = attorneysTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            string reportName = $"Westlaw Precision - Litigation Analytics Report for {analyticsProfilerPage.AnalyticsTitle.Replace(".", "")}.pdf";
            var experienceTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            experienceTab.ContentViewTabPanel.ChartViewContainer.ShowMoreButton.Click();
            var downloadDialog = experienceTab.ContentViewTabPanel.TitleToolBarComponent.DeliveryDropdown.SelectOption<LitigationAnalyticsDownloadDialog>(DeliveryMethod.Download);
            this.DownloadCurrentViewReport(downloadDialog, reportName);

            string textDelivery = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, reportName));

            this.TestCaseVerify.IsTrue(
                DeliveriedDocumentTitleVerify,
                    textDelivery.Contains(analyticsProfilerPage.AnalyticsTitle),
                    "Downloaded document does not contain title");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        public void CaseTypeExperienceDeliveryTest()
        {
            const string Query = "Civil Rights";
            const string DeliveriedDocumentTitleVerify = "Verify that delivered document contains Title";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();
            string reportName = $"Westlaw Precision - Litigation Analytics Report for {Query}.pdf";
            var experienceTab = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCaseTypeEntityTabComponent>(LitigationAnalyticsProfiles.CaseTypes)
                .EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query).TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>()
                .ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            experienceTab.ContentViewTabPanel.ChartViewContainer.ShowMoreButton.Click();
            var downloadDialog = experienceTab.ContentViewTabPanel.TitleToolBarComponent.DeliveryDropdown.SelectOption<LitigationAnalyticsDownloadDialog>(DeliveryMethod.Download);

            this.DownloadCurrentViewReport(downloadDialog, reportName);

            string textDelivery = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, reportName));

            this.TestCaseVerify.IsTrue(
                DeliveriedDocumentTitleVerify,
                    textDelivery.Contains(Query),
                    "Downloaded document does not contain filtered item with note");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        public void CompaniesDeliveryTest()
        {
            const string Query = "amazon";
            const string DeliveriedDocumentTitleVerify = "Verify that delivered document contains Title";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();
            var resultListPage = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCompaniesEntityTabComponent>(LitigationAnalyticsProfiles.Companies)
                .EnterSearchQueryAndClickSearch<CompareResultListPage>(Query);
            resultListPage.CompanySelectionComponent.CompaniesResultListItems.First().InputCheckBox.Set(true);
            DriverExtensions.ScrollPageToBottom();
            var experienceTab = resultListPage.MySelectionsComponent.CreateReportButton.Click<LitigationAnalyticsProfilerPage>().ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            string reportName = $"Westlaw Precision - Litigation Analytics Report for {Query}.pdf";

            experienceTab.ContentViewTabPanel.ChartViewContainer.ShowMoreButton.Click();
            var downloadDialog = experienceTab.ContentViewTabPanel.TitleToolBarComponent.DeliveryDropdown.SelectOption<LitigationAnalyticsDownloadDialog>(DeliveryMethod.Download);

            this.DownloadCurrentViewReport(downloadDialog, reportName);

            string textDelivery = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, reportName));

            this.TestCaseVerify.IsTrue(
                DeliveriedDocumentTitleVerify,
                    textDelivery.Contains(Query),
                    "Downloaded document does not contain filtered item with note");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        public void CompaniesSelectAllDeliveryTest()
         {
            const string Query = "amazon";
            const string DeliveriedDocumentTitleVerify = "Verify that Select All delivered document contains Title";

            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();
            var resultListPage = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCompaniesEntityTabComponent>(LitigationAnalyticsProfiles.Companies)
                .EnterSearchQueryAndClickSearch<CompareResultListPage>(Query);
            resultListPage.CompanySelectionComponent.CompaniesResultListItems.First().SelectAllInputCheckBox.Set(true);
            DriverExtensions.ScrollPageToBottom();
            var experienceTab = resultListPage.MySelectionsComponent.CreateReportButton.Click<LitigationAnalyticsProfilerPage>().ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            string reportName = $"Westlaw Precision - Litigation Analytics Report for {Query}.pdf";

            experienceTab.ContentViewTabPanel.ChartViewContainer.ShowMoreButton.Click();
            var downloadDialog = experienceTab.ContentViewTabPanel.TitleToolBarComponent.DeliveryDropdown.SelectOption<LitigationAnalyticsDownloadDialog>(DeliveryMethod.Download);

            this.DownloadCurrentViewReport(downloadDialog, reportName);

            string textDelivery = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, reportName));

            this.TestCaseVerify.IsTrue(
                DeliveriedDocumentTitleVerify,
                    textDelivery.Contains(Query),
                    "Downloaded document does not contain filtered item with note");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        public void CourtsExperienceDeliveryTest()
        {
            const string Query = "New York Supreme Court";
            const string DeliveriedDocumentTitleVerify = "Verify that delivered document contains Title";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var courtsTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsCourtsEntityTabComponent>(LitigationAnalyticsProfiles.Courts);
            var typeahead = courtsTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            string reportName = $"Westlaw Precision - Litigation Analytics Report for {Query}.pdf";
            var experienceTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            experienceTab.ContentViewTabPanel.ChartViewContainer.ShowMoreButton.Click();
            var downloadDialog = experienceTab.ContentViewTabPanel.TitleToolBarComponent.DeliveryDropdown.SelectOption<LitigationAnalyticsDownloadDialog>(DeliveryMethod.Download);
            this.DownloadCurrentViewReport(downloadDialog, reportName);

            string textDelivery = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, reportName));

            this.TestCaseVerify.IsTrue(
                DeliveriedDocumentTitleVerify,
                    textDelivery.Contains(Query),
                    "Downloaded document does not contain filtered item with note");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        public void DamagesExperienceDeliveryTest()
        {
            const string DeliveriedDocumentTitleVerify = "Verify that delivered document contains Title";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var damagesTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsDamagesEntityTabComponent>(LitigationAnalyticsProfiles.Damages);
            var caseTypeDialog = damagesTabComponent.CaseTypeButton.Click<LitigationAnalyticsCaseTypeSelectingDialog>();
            caseTypeDialog.CaseTypeItems.First().NameLink.Click<LitigationAnalyticsCaseTypeSelectingDialog>().SaveButton.Click<LitigationAnalyticsDamagesEntityTabComponent>();
            var profilePage = damagesTabComponent.SearchButton.Click<DamagesProfileTabComponent>();
            string reportName = $"Westlaw Precision - Litigation Analytics Report.pdf";
            profilePage.ContentViewTabPanel.ChartViewContainer.ShowMoreButton.Click();
            var downloadDialog = profilePage.ContentViewTabPanel.TitleToolBarComponent.DeliveryDropdown.SelectOption<LitigationAnalyticsDownloadDialog>(DeliveryMethod.Download);
            this.DownloadCurrentViewReport(downloadDialog, reportName);

            string textDelivery = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, reportName));

            this.TestCaseVerify.IsTrue(
                DeliveriedDocumentTitleVerify,
                    textDelivery.Contains("Litigation Analytics Report"),
                    "Downloaded document does not contain filtered item with note");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        public void JudgesExperienceDeliveryTest()
        {
            const string Query = "Tom S. Lee";
            const string DeliveriedDocumentTitleVerify = "Verify that delivered document contains Title";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var judgeTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsJudgesEntityTabComponent>(LitigationAnalyticsProfiles.Judges);
            var typeahead = judgeTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            string reportName = $"Westlaw Precision - Litigation Analytics Report for {analyticsProfilerPage.AnalyticsTitle.Replace(".", "")}.pdf";
            var experienceTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            experienceTab.ContentViewTabPanel.ChartViewContainer.ShowMoreButton.Click();

            var downloadDialog = experienceTab.ContentViewTabPanel.TitleToolBarComponent.DeliveryDropdown.SelectOption<LitigationAnalyticsDownloadDialog>(DeliveryMethod.Download);
            this.DownloadCurrentViewReport(downloadDialog, reportName);

            string textDelivery = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, reportName));

            this.TestCaseVerify.IsTrue(
                DeliveriedDocumentTitleVerify,
                    textDelivery.Contains(analyticsProfilerPage.AnalyticsTitle),
                    "Downloaded document does not contain filtered item with note");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        public void LawFirmDeliveryTest()
        {
            const string Query = "New York City Law Department";
            const string DeliveriedDocumentTitleVerify = "Verify that delivered document contains Title";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var lawFirmsTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsLawFirmsEntityTabComponent>(LitigationAnalyticsProfiles.LawFirms);
            lawFirmsTabComponent.ByNameRadiobutton.Select();
            var typeahead = lawFirmsTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            string reportName = $"Westlaw Precision - Litigation Analytics Report for {analyticsProfilerPage.AnalyticsTitle.Replace(".", "")}.pdf";
            var experienceTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            experienceTab.ContentViewTabPanel.ChartViewContainer.ShowMoreButton.Click();
            var downloadDialog = experienceTab.ContentViewTabPanel.TitleToolBarComponent.DeliveryDropdown.SelectOption<LitigationAnalyticsDownloadDialog>(DeliveryMethod.Download);

            this.DownloadCurrentViewReport(downloadDialog, reportName);

            string textDelivery = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, reportName));

            this.TestCaseVerify.IsTrue(
                DeliveriedDocumentTitleVerify,
                    textDelivery.Contains(analyticsProfilerPage.AnalyticsTitle),
                    "Downloaded document does not contain filtered item with note");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        public void LawFirmCompareDeliveryTest()
        {
            const string Query = "New York City Law";
            const string DeliveriedDocumentTitleVerify = "Verify that delivered document contains Title";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var lawFirmsTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsLawFirmsEntityTabComponent>(LitigationAnalyticsProfiles.LawFirms);
            lawFirmsTabComponent.CompareRadiobutton.Select();
            var resultListPage = lawFirmsTabComponent.EnterSearchQueryAndClickSearch<CompareResultListPage>(Query);
            resultListPage.CompareResultComponent.SetCheckboxByIndex(0, true);
            resultListPage.CompareResultComponent.SetCheckboxByIndex(1, true);
            DriverExtensions.ScrollPageToBottom();
            var lawFirmTitles = resultListPage.MySelectionsComponent.SelectedCompanyNames;

            var analyticsProfilerPage = resultListPage.MySelectionsComponent.CreateReportButton.Click<LitigationAnalyticsProfilerPage>();
            SafeMethodExecutor.WaitUntil(() => !analyticsProfilerPage.AnalyticsProfileTabPage.LoadingSpinner.Displayed, 40);
            var experienceTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            SafeMethodExecutor.WaitUntil(() => !experienceTab.LoadingSpinner.Displayed, 40);
            string reportName = "Westlaw Precision - Litigation Analytics Report for New York City Law Department.pdf";
            var downloadDialog = experienceTab.ContentViewTabPanel.TitleToolBarComponent.DeliveryDropdown.SelectOption<LitigationAnalyticsDownloadDialog>(DeliveryMethod.Download);

            this.DownloadCurrentViewReport(downloadDialog, reportName);

            string textDelivery = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, reportName));

            this.TestCaseVerify.IsTrue(
                DeliveriedDocumentTitleVerify,
                    textDelivery.ContainsAnyItem(lawFirmTitles),
                    "Downloaded document does not contain filtered item with note");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        public void LawFirmDealsDeliveryTest()
        {
            const string Query = "Sullivan & Cromwell LLP";
            const string DeliveriedDocumentTitleVerify = "Verify that delivered document contains Title";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var lawFirmsTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsLawFirmsEntityTabComponent>(LitigationAnalyticsProfiles.LawFirms);
            lawFirmsTabComponent.ByNameRadiobutton.Select();
            var typeahead = lawFirmsTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            string reportName = $"Westlaw Precision - Litigation Analytics Report for {analyticsProfilerPage.AnalyticsTitle.Replace(".", "").Replace("&", "And")}.pdf";
            var dealsTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Deals);
            var targetSic = dealsTab.ResultListComponent.GetAllSearchResultItems<LitigationAnalyticsPageResultListItem>().Select(item => item.GetTargetSicName.Replace(" ", ""));
            var downloadDialog = dealsTab.ContentViewTabPanel.TitleToolBarComponent.DeliveryDropdown.SelectOption<LitigationAnalyticsDownloadDialog>(DeliveryMethod.Download);

            this.DownloadCurrentViewReportWithSettings(downloadDialog, reportName, ItemsToInclude.ListOfDockets);

            string textDelivery = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, reportName)).Replace(" ", "").Replace("\n", "").Replace("\r", "");

            this.TestCaseVerify.IsTrue(
                DeliveriedDocumentTitleVerify,
                    targetSic.All(sic => textDelivery.Contains(sic)),
                    "Downloaded document does not contain filtered item with note");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        public void AttorneysExperienceIncludeMultidistrictLitigationDeliveryTest()
        {
            const string Query = "Lawrence Bender";
            const string DeliveriedDocumentTitleVerify = "Verify that Delivery works with Include multidistrict Litigation toggle";
            var litigationAnalyticsPage = this.OpenLitigationAnalyticsPage();

            var attorneysTabComponent = litigationAnalyticsPage.HeaderPanel.SetActiveTab<LitigationAnalyticsAttorneysEntityTabComponent>(LitigationAnalyticsProfiles.Attorneys);
            attorneysTabComponent.ByNameRadiobutton.Select();
            var typeahead = attorneysTabComponent.EnterSearchQuery<LitigationAnalyticsTypeAheadDialog>(Query);
            var analyticsProfilerPage = typeahead.TypeaheadItems.First().NameLink.Click<LitigationAnalyticsProfilerPage>();
            string reportName = $"Westlaw Precision - Litigation Analytics Report for {analyticsProfilerPage.AnalyticsTitle.Replace(".", "")}.pdf";
            var experienceTab = analyticsProfilerPage.ProfileTabPanel.SetActiveTab<ExperienceProfileTabComponent>(ProfileComponentTab.Experience);
            experienceTab.NarrowPane.IncludeMultidistrictLitigationToggle.ToggleState(true);
            var downloadDialog = experienceTab.ContentViewTabPanel.TitleToolBarComponent.DeliveryDropdown.SelectOption<LitigationAnalyticsDownloadDialog>(DeliveryMethod.Download);
            this.DownloadCurrentViewReport(downloadDialog, reportName);

            string textDelivery = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, reportName));

            this.TestCaseVerify.IsTrue(
                DeliveriedDocumentTitleVerify,
                    textDelivery.Contains(analyticsProfilerPage.AnalyticsTitle),
                    "Downloaded document does not contain title");
        }
    }
}