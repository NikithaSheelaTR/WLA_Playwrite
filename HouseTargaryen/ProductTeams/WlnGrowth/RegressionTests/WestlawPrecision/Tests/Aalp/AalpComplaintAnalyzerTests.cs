namespace WestlawPrecision.Tests.Aalp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.ComplaintAnalyzer;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.AALP.ComplaintAnalyzer;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP.ComplaintAnalyzer;
    using Framework.Common.UI.Raw.EnhancementTests.Utils;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.Extensions;
    using Framework.Core.Utils.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WestlawPrecision.Utilities;
    using Keys = OpenQA.Selenium.Keys;

    /// <summary>
    /// Complaint Analyzer tests
    /// </summary>
    [TestClass]
    public class AalpComplaintAnalyzerTests : AalpBaseTest
    {
        protected const string FeatureTestCategory = "ComplaintAnalyzer";
        protected const string AiAnalyzeAComplaintTab = "Analyze A Complaint page";
        protected const string ComplaintAnalyzerLabel = "Complaint Analyzer";

        /// <summary>
        /// Test cases: 2146582, 2150101, 2149184
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        public void AiComplaintAnalyzerUploadFileCommonTest()
        {
            const string EmptyFileErrorMessage = "File cannot be empty.";
            const string FileSizeUploadErrorMessage = "File uploads cannot exceed 20MB.";
            const string SupportedFilesErrorMessage = "File type must be .DOCX or .PDF.";

            string checkComplaintAnalyzerTileDescription = "Verify: 'Complaint Analyzer' tile description is as expected";
            string checkSafeAnalysisButtonDisabled = "Verify: 'Safe Analysis' button is disabled after clicking on it";
            string checkLandingPage = "Verify: Complaint Analyzer page is opened";
            string checkClaimsTabPanelFilterAfterDocumentUpload = "Verify: Filter in claims tab is displayed after Document upload";
            string checkSummaryDetails = "Verify: Summary tab displays information as per uploaded document/text";
            string checkClaimsTabFilterOptionsAfterDocumentUpload = "Verify: Filter in claims tab are as per parties after Document upload";
            string checkClaimsTabCardHeadingsAfterDocumentUpload = "Verify: All claims tab cards contains expected section headings";
            string checkEventsTabSectionsForDocumentUpload = "Verify: Document Upload - Events tab cards contains all sections";
            string checkClaimsTabFilterSelectionForDocumentUpload = "Verify: Filter when applied in claims tab gives proper result for Document upload";
            string checkEventsTabFilterSelectionForDocumentUpload = "Verify: Filter when applied in events tab gives proper result for Document upload";
            string checkDefensesTabSectionsForDocumentUpload = "Verify: Document Upload - Defenses tab cards contains all sections";
            string checkDefensesTabFilterSelectionForDocumentUpload = "Verify: Filter when applied in Defenses tab gives proper result for Document upload";
            string checkAnalyzeNewComplaintButtonWorkingAsNew = "Verify: Analyze new complaint is working as new analysis, not showing the previous uploaded file";
            string checkImageFileUpload = "Verify: Image upload is not supported";
            string checkZeroByteDocFileUpload = "Verify: ZeroByte document upload is not supported";
            string checkZeroBytePDFFileUpload = "Verify: ZeroByte pdf upload is not supported";
            string checkMoreThan20MbFileUpload = "Verify: Morethan 20MB size pdf upload is not supported";
            string checkOcrFileUpload = "Verify: OCR file upload is successful";

            var documentPath = $@"{TestDocsFolderPath}\2023 WL 9108365.docx";

            var expectedHeadingTitlesList = new List<string> { "Citation:", "Description:", "Parties involved:", "Related facts:", "Relief sought:" };

            var homePage = this.GetHomePage<PrecisionHomePage>();

            SafeMethodExecutor.WaitUntil(() => homePage.FeaturesIncludedPanel.GetLabsWidgetTextByTitle(ComplaintAnalyzerLabel).Displayed);

            this.TestCaseVerify.AreEqual(
                 checkComplaintAnalyzerTileDescription,
                 "Extract key insights for legal analysis and effective strategy development.",
                 homePage.FeaturesIncludedPanel.GetLabsWidgetTextByTitle(ComplaintAnalyzerLabel).Text,
                 "'Complaint Analyzer' tile description is NOT as expected");

            var analyzeAComplaintPage = homePage.FeaturesIncludedPanel.GetLabsWidgetLinkByTitle(ComplaintAnalyzerLabel).Click<AiAnalyzeAComplaintPage>();

            this.TestCaseVerify.IsTrue(
                checkLandingPage,
                BrowserPool.CurrentBrowser.Url.Contains("usageFeature=AIComplaintAnalyzerCariApi"),
                "Complaint Analyzer page is NOT opened");

            var uploadADocumentTab = analyzeAComplaintPage.ComplaintAnalyzerTabPanel.SetActiveTab<UploadADocumentTab>(ComplaintAnalyzerTabs.UploadADocument);

            // Upload A Document
            uploadADocumentTab = uploadADocumentTab.UploadFile(documentPath);
            var complaintAnalyzerSkillLandingPage = uploadADocumentTab.SafeAnalysisButton.Click<ComplaintAnalyzerSkillLandingPage>();

            SafeMethodExecutor.WaitUntil(() => !uploadADocumentTab.SafeAnalysisButton.Enabled);

            this.TestCaseVerify.IsFalse(
                checkSafeAnalysisButtonDisabled,
                uploadADocumentTab.SafeAnalysisButton.Enabled,
                "'Safe Analysis' button is NOT disabled after clicking on it");

            SafeMethodExecutor.WaitUntil(() => !complaintAnalyzerSkillLandingPage.ProgressBarLabel.Displayed);

            //Get text from the uploaded file for comparison
            Clipboard.SetText(DocxTextExtractor.ExtractTextFromWord(documentPath));
            var fileTextForCompare = this.CleanText(Clipboard.GetText());

            //Verify summary tab
            SafeMethodExecutor.WaitUntil(() => complaintAnalyzerSkillLandingPage.AnalyzeNewComplaintButton.Displayed);

            var summaryTabPanel = complaintAnalyzerSkillLandingPage.ComplaintAnalyzerResultTabPanel.SetActiveTab<SummaryTab>(ComplaintAnalyzerResultTabs.Summary);

            SafeMethodExecutor.WaitUntil(() => summaryTabPanel.PartiesCardPlaintifLabel.Displayed);

            //Filing information
            var caseNumber = summaryTabPanel.FillingInfoCardCaseNumberLabel.Text.Replace("Case Number: ", string.Empty).ToLower();
            var caption = this.CleanText(summaryTabPanel.FillingInfoCardCaptionLabel.Text);

            //Parties
            var plantiffFullString = summaryTabPanel.PartiesCardPlaintifLabel.Text.Split('(');
            var plaintiff = this.CleanText(plantiffFullString[0]);
            var defendantsList = summaryTabPanel.PartiesCardDefendantsLabels.Select(label => this.CleanText(label.Text)).ToList();

            //Pleading overview
            var keyClaimsList = summaryTabPanel.KeyClaimsLabels.Select(label => this.CleanText(label.Text)).ToList();
            var reliefSoughtList = summaryTabPanel.ReliefSoughtLabels.Select(label => this.CleanText(label.Text)).ToList();

            this.TestCaseVerify.IsTrue(
                checkSummaryDetails,
                fileTextForCompare.Contains(caseNumber)
                && fileTextForCompare.Contains(caption)
                && fileTextForCompare.Contains(plaintiff)
                && defendantsList.All(item => fileTextForCompare.Contains(item))
                && keyClaimsList.Count > 0,
                // Bug 2149409 : && reliefSoughtList.All(item => fileTextForCompare.Contains(item)),
                "Summary tab does NOT contain summary of the uploaded file");

            defendantsList.Add(this.CleanText(summaryTabPanel.PartiesCardPlaintifLabel.Text));

            //Verify claims tab
            var claimsTab = complaintAnalyzerSkillLandingPage.ComplaintAnalyzerResultTabPanel.SetActiveTab<ClaimsTab>(ComplaintAnalyzerResultTabs.Claims);

            var claimCardSectionTitles = claimsTab.ResultList.ClaimsCardSectionLabels.Select(item => item.Text).ToList();

            var filter = claimsTab.ComplaintAnalyserFilter.PartyFacet.FiltersCheckBoxes.Select(item => this.CleanText(item.GetAttribute("current-value"))).ToList();
            
            this.TestCaseVerify.IsTrue(
                checkClaimsTabCardHeadingsAfterDocumentUpload,
                claimCardSectionTitles.Distinct().All(items => expectedHeadingTitlesList.Contains(items))
                && claimsTab.ResultList.ClaimsCardPlantiffLabels.Count > 0
                && claimsTab.ResultList.ClaimsCardDefendantLabels.Count > 0
                && claimsTab.ResultList.RelatedFactsShortDescriptionLabels.Count > 0
                && claimsTab.ResultList.RelatedFactsLongDescriptionLabels.Count > 0
                && !claimsTab.ResultList.ReliefSoughtLabels.Any(item => item.Text.Contains("§")),
                "Claims tab cards does NOT contain all sections");

            this.TestCaseVerify.IsTrue(
                checkClaimsTabFilterOptionsAfterDocumentUpload,
                claimsTab.ComplaintAnalyserFilter.PartyFacet.FilterButton.Displayed
                && filter.Distinct().All(items => defendantsList.Contains(items)),
                "Filter in claims tab is NOT displayed with all plaintiff and defendants");

            //Applying filters
            var filterOption = claimsTab.ComplaintAnalyserFilter.PartyFacet.FiltersCheckBoxes.ElementAt(1).GetAttribute("current-value");
            claimsTab.ComplaintAnalyserFilter.PartyFacet.SelectParty(false, filterOption);

            var defendentasPlaintiffList = claimsTab.ResultList.ClaimsCardDefendantLabels.Select(item => item.Text).ToList();
            var countOfCards = claimsTab.ResultList.ClaimsCardLabels.Count;

            this.TestCaseVerify.IsTrue(
                checkClaimsTabFilterSelectionForDocumentUpload,
                defendentasPlaintiffList.Count(item => item.Contains(filterOption)).Equals(countOfCards),
                "Filter in claims tab is NOT filtering as per selection");

            //Verify events timeline tab
            var eventTabPanel = complaintAnalyzerSkillLandingPage.ComplaintAnalyzerResultTabPanel.SetActiveTab<EventTimelineTab>(ComplaintAnalyzerResultTabs.EventTimeline);

            var eventsFilterPartyOptions = eventTabPanel.ComplaintAnalyserFilter.PartyFacet.FiltersCheckBoxes.Select(item => this.CleanText(item.Text)).ToList();

            this.TestCaseVerify.IsTrue(
                checkEventsTabSectionsForDocumentUpload,
                eventTabPanel.ResultList.EventsCardPlantiffLabels.Count > 0
                && eventTabPanel.ResultList.EventsCardDefendantLabels.Count > 0
                && eventTabPanel.ResultList.EventsTypeLabels.Count > 0,
                "Document Upload - Events tab cards does NOT contain all sections");

            this.TestCaseVerify.IsTrue(
               checkClaimsTabPanelFilterAfterDocumentUpload,
               eventTabPanel.ComplaintAnalyserFilter.PartyFacet.FilterButton.Displayed
               && eventsFilterPartyOptions.Distinct().All(items => defendantsList.Contains(items)),
               "Filter in events timeline tab is NOT displayed with all plaintiff and defendants");

            //Applying Filters
            var filterOptionEvents = eventTabPanel.ComplaintAnalyserFilter.PartyFacet.FiltersCheckBoxes.ElementAt(1).GetAttribute("current-value");
            eventTabPanel.ComplaintAnalyserFilter.PartyFacet.SelectParty(false, filterOptionEvents);

            defendentasPlaintiffList = eventTabPanel.ResultList.EventsCardDefendantLabels.Select(item => item.Text).ToList();
            countOfCards = eventTabPanel.ResultList.EventsCardLabels.Count;

            this.TestCaseVerify.IsTrue(
                checkEventsTabFilterSelectionForDocumentUpload,
                defendentasPlaintiffList.Contains(filterOptionEvents)
                && defendentasPlaintiffList.Count(item => item.Contains(filterOption)).Equals(countOfCards),
                "Filter in claims tab is NOT filtering as per selection");

            //Verify Defenses tab
            var defensesTabPanel = complaintAnalyzerSkillLandingPage.ComplaintAnalyzerResultTabPanel.SetActiveTab<DefensesTab>(ComplaintAnalyzerResultTabs.Defenses);

            this.TestCaseVerify.IsTrue(
                checkDefensesTabSectionsForDocumentUpload,
                defensesTabPanel.ResultList.DefensesCardLabels.Count > 0
                && defensesTabPanel.ResultList.DefensesCardTableLabels.Count > 0
                && defensesTabPanel.ResultList.DefensesCardOverviewSectionLabels.Count > 0,
                "Document Upload - Defenses tab cards does NOT contain all sections");

            //Applying Filters
            var filterOptionDefenses = defensesTabPanel.ComplaintAnalyserFilter.PartyFacet.FiltersCheckBoxes.ElementAt(1).GetAttribute("current-value");
            defensesTabPanel.ComplaintAnalyserFilter.PartyFacet.SelectParty(false, filterOptionEvents);

            this.TestCaseVerify.IsTrue(
                checkDefensesTabFilterSelectionForDocumentUpload,
                defensesTabPanel.ResultList.DefensesCardLabels.Count.Equals(4),
                "Document Upload - Defenses tab Filter is NOT filtering as per selection");

            //Check Analyze new complaint working as new complaint analyzer
            complaintAnalyzerSkillLandingPage = complaintAnalyzerSkillLandingPage.AnalyzeNewComplaintButton.Click<ComplaintAnalyzerSkillLandingPage>();
            uploadADocumentTab = analyzeAComplaintPage.ComplaintAnalyzerTabPanel.SetActiveTab<UploadADocumentTab>(ComplaintAnalyzerTabs.UploadADocument);

            this.TestCaseVerify.IsTrue(
                checkAnalyzeNewComplaintButtonWorkingAsNew,
                !uploadADocumentTab.UploadedFileName.Displayed,
                "Analyze new complaint is NOT working as new analysis, showing the previous uploaded file");

            //Verify OCR pdf
            homePage = BrowserPool.CurrentBrowser.GoBack<PrecisionHomePage>();

            analyzeAComplaintPage = homePage.FeaturesIncludedPanel.GetLabsWidgetLinkByTitle(ComplaintAnalyzerLabel).Click<AiAnalyzeAComplaintPage>();

            uploadADocumentTab = analyzeAComplaintPage.ComplaintAnalyzerTabPanel.SetActiveTab<UploadADocumentTab>(ComplaintAnalyzerTabs.UploadADocument);

            //OCR Upload
            uploadADocumentTab = uploadADocumentTab.UploadFile($@"{TestDocsFolderPath}\Sanchez v. Friant Water Authority.pdf");
            complaintAnalyzerSkillLandingPage = uploadADocumentTab.SafeAnalysisButton.Click<ComplaintAnalyzerSkillLandingPage>();

            SafeMethodExecutor.WaitUntil(() => !complaintAnalyzerSkillLandingPage.ProgressBarLabel.Displayed);

            //Verify summary tab
            summaryTabPanel = complaintAnalyzerSkillLandingPage.ComplaintAnalyzerResultTabPanel.SetActiveTab<SummaryTab>(ComplaintAnalyzerResultTabs.Summary);

            SafeMethodExecutor.WaitUntil(() => summaryTabPanel.PartiesCardPlaintifLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkOcrFileUpload,
                summaryTabPanel.PartiesCardPlaintifLabel.Displayed,
                "OCR file upload was not successful");

            //File Upload Error Message Validations 
            homePage = BrowserPool.CurrentBrowser.GoBack<PrecisionHomePage>();

            analyzeAComplaintPage = homePage.FeaturesIncludedPanel.GetLabsWidgetLinkByTitle(ComplaintAnalyzerLabel).Click<AiAnalyzeAComplaintPage>();

            uploadADocumentTab = analyzeAComplaintPage.ComplaintAnalyzerTabPanel.SetActiveTab<UploadADocumentTab>(ComplaintAnalyzerTabs.UploadADocument);

            //Image Upload
            uploadADocumentTab = uploadADocumentTab.UploadFile($@"{TestDocsFolderPath}\ComplaintAnalyzerImage.jpg");

            this.TestCaseVerify.IsTrue(
                checkImageFileUpload,
                uploadADocumentTab.UploadFileErrorMessage.GetAttribute("message").Equals(SupportedFilesErrorMessage)
                && uploadADocumentTab.SafeAnalysisButton.GetAttribute("class").Equals("disabled"),
                "File type must be .DOCX or .PDF error message was not displayed");

            //0 Byte File upload
            uploadADocumentTab = uploadADocumentTab.UploadFile($@"{TestDocsFolderPath}\ComplaintAnalyzer0KbFile.docx");

            this.TestCaseVerify.IsTrue(
                checkZeroByteDocFileUpload,
                uploadADocumentTab.UploadFileErrorMessage.GetAttribute("message").Equals(EmptyFileErrorMessage)
                && uploadADocumentTab.SafeAnalysisButton.GetAttribute("class").Equals("disabled"),
                "File cannot be empty error message was not displayed for Doc file type");

            uploadADocumentTab = uploadADocumentTab.UploadFile($@"{TestDocsFolderPath}\0KbFile.pdf");

            this.TestCaseVerify.IsTrue(
                checkZeroBytePDFFileUpload,
                uploadADocumentTab.UploadFileErrorMessage.GetAttribute("message").Equals(EmptyFileErrorMessage)
                && uploadADocumentTab.SafeAnalysisButton.GetAttribute("class").Equals("disabled"),
                "File cannot be empty error message was not displayed for PDF file type");

            //Docx/pdf with size>20MB
            uploadADocumentTab = uploadADocumentTab.UploadFile($@"{TestDocsFolderPath}\30MbFile.pdf");

            this.TestCaseVerify.IsTrue(
                checkMoreThan20MbFileUpload,
                uploadADocumentTab.UploadFileErrorMessage.GetAttribute("message").Equals(FileSizeUploadErrorMessage)
                && uploadADocumentTab.SafeAnalysisButton.GetAttribute("class").Equals("disabled"),
                "File uploads cannot exceed 20MB error message was not displayed for PDF file type");
        }

        /// <summary>
        /// Test cases: 2146582
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        public void AiComplaintAnalyzerUploadTextCommonTest()
        {
            const string CharactersLimitErrorMessage = "Character limit exceeded. Try uploading as a document instead.";
            const string MoreInformationErrorMessage = "More information needed";
            
            string checkClaimsTabPanelFilterAfterDocumentUpload = "Verify: Filter in claims tab is displayed after Document upload";
            string checkClaimsTabPanelFilterAfterComplaintTextUpload = "Verify: Filter in claims tab is displayed after Complaint Text upload";
            string checkSummaryDetails = "Verify: Summary tab displays information as per uploaded document/text";
            string checkClaimsTabCardHeadingsAfterTextUpload = "Verify: All claims tab cards contains expected section headings";
            string checkEventsTabSectionsForTextUpload = "Verify: Text Upload - Events tab cards contains all sections";
            string checkClaimsTabFilterSelectionForDocumentUpload = "Verify: Filter when applied in claims tab gives proper result for Document upload";
            string checkEventsTabFilterSelectionForDocumentUpload = "Verify: Filter when applied in events tab gives proper result for Document upload";
            string checkDefensesTabSectionsForDocumentUpload = "Verify: Text Upload - Defenses tab cards contains all sections";
            string checkDefensesTabFilterSelectionForDocumentUpload = "Verify: Filter when applied in Defenses tab gives proper result for uploaded text";
            string checkTextCharacterLimit = "Verify: Text character limit in complaint analyzer";

            var documentPath = $@"{TestDocsFolderPath}\2023 WL 9108365.docx";

            var expectedHeadingTitlesList = new List<string> { "Citation:", "Description:", "Parties involved:", "Related facts:", "Relief sought:" };

            var homePage = this.GetHomePage<PrecisionHomePage>();

            var analyzeAComplaintPage = homePage.FeaturesIncludedPanel.GetLabsWidgetLinkByTitle(ComplaintAnalyzerLabel).Click<AiAnalyzeAComplaintPage>();

            Clipboard.SetText(DocxTextExtractor.ExtractTextFromWord(documentPath));

            DriverExtensions.WaitForJavaScript();

            var fileTextForCompare = this.CleanText(Clipboard.GetText());

            // Enter Complaint Text
            BrowserPool.CurrentBrowser.CreateTab(AiAnalyzeAComplaintTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAnalyzeAComplaintTab);                    

            var enterComplaintTextTab = analyzeAComplaintPage.ComplaintAnalyzerTabPanel.SetActiveTab<EnterComplaintTextTab>(ComplaintAnalyzerTabs.EnterComplaintText);

            enterComplaintTextTab.EnterComplaintTextbox.SendKeys(Keys.Control + "v");

            var complaintAnalyzerSkillLandingPage = enterComplaintTextTab.SafeAnalysisButton.Click<ComplaintAnalyzerSkillLandingPage>();

            SafeMethodExecutor.WaitUntil(() => !complaintAnalyzerSkillLandingPage.ProgressBarLabel.Displayed);

            //Verify summary tab
            var summaryTabPanel = complaintAnalyzerSkillLandingPage.ComplaintAnalyzerResultTabPanel.SetActiveTab<SummaryTab>(ComplaintAnalyzerResultTabs.Summary);

            SafeMethodExecutor.WaitUntil(() => summaryTabPanel.FillingInfoCardCaseNumberLabel.Displayed);

            //Filing information
            var caseNumber = summaryTabPanel.FillingInfoCardCaseNumberLabel.Text.Replace("Case Number: ", string.Empty).ToLower();
            var caption = this.CleanText(summaryTabPanel.FillingInfoCardCaptionLabel.Text);

            //Parties
            var plantiffFullString = summaryTabPanel.PartiesCardPlaintifLabel.Text.Split('(');
            var plaintiff = this.CleanText(plantiffFullString[0]);
            var defendantsList = summaryTabPanel.PartiesCardDefendantsLabels.Select(label => this.CleanText(label.Text)).ToList();

            //Pleading overview
            var keyClaimsList = summaryTabPanel.KeyClaimsLabels.Select(label => this.CleanText(label.Text)).ToList();
            var reliefSoughtList = summaryTabPanel.ReliefSoughtLabels.Select(label => this.CleanText(label.Text)).ToList();

            this.TestCaseVerify.IsTrue(
                checkSummaryDetails,
                fileTextForCompare.Contains(caseNumber)
                && fileTextForCompare.Contains(caption)
                && fileTextForCompare.Contains(plaintiff)
                && defendantsList.All(item => fileTextForCompare.Contains(item))
                && keyClaimsList.Count>0,
                // Bug 2149409 : && reliefSoughtList.All(item => fileTextForCompare.Contains(item)),
                "Summary tab does NOT contain summary of the uploaded text");

            defendantsList.Add(this.CleanText(summaryTabPanel.PartiesCardPlaintifLabel.Text));

            //Verify claims tab
            var claimsTab = complaintAnalyzerSkillLandingPage.ComplaintAnalyzerResultTabPanel.SetActiveTab<ClaimsTab>(ComplaintAnalyzerResultTabs.Claims);

            var claimCardSectionTitles = claimsTab.ResultList.ClaimsCardSectionLabels.Select(item => item.Text).ToList();

            var filter = claimsTab.ComplaintAnalyserFilter.PartyFacet.FiltersCheckBoxes.Select(item => this.CleanText(item.GetAttribute("current-value"))).ToList();

            this.TestCaseVerify.IsTrue(
                checkClaimsTabPanelFilterAfterComplaintTextUpload,
                claimCardSectionTitles.Distinct().All(items => expectedHeadingTitlesList.Contains(items))
                && claimsTab.ResultList.ClaimsCardPlantiffLabels.Count > 0
                && claimsTab.ResultList.ClaimsCardDefendantLabels.Count > 0
                && claimsTab.ResultList.RelatedFactsShortDescriptionLabels.Count > 0
                && claimsTab.ResultList.RelatedFactsLongDescriptionLabels.Count > 0,
                // Bug 2149409 : && !claimsTab.ResultList.ReliefSoughtLabels.Any(item => item.Text.Contains("§")),
                "Claims tab cards does NOT contain all sections for upload text");

            this.TestCaseVerify.IsTrue(
                checkClaimsTabCardHeadingsAfterTextUpload,
                claimsTab.ComplaintAnalyserFilter.PartyFacet.FilterButton.Displayed
                && filter.Distinct().All(items => defendantsList.Contains(items)),
                "Filter in claims tab is NOT displayed with all plaintiff and defendants for upload text");

            //Applying Filters
            var filterOption = claimsTab.ComplaintAnalyserFilter.PartyFacet.FiltersCheckBoxes.ElementAt(1).GetAttribute("current-value");
            claimsTab.ComplaintAnalyserFilter.PartyFacet.SelectParty(false, filterOption);

            var defendentasPlaintiffList = claimsTab.ResultList.ClaimsCardDefendantLabels.Select(item => item.Text).ToList();
            var countOfCards = claimsTab.ResultList.ClaimsCardLabels.Count;

            this.TestCaseVerify.IsTrue(
                checkClaimsTabFilterSelectionForDocumentUpload,
                defendentasPlaintiffList.Count(item => item.Contains(filterOption)).Equals(countOfCards),
                "Filter in claims tab is NOT filtering as per selection for upload text");

            //Verify events timeline tab
            var eventTabPanel = complaintAnalyzerSkillLandingPage.ComplaintAnalyzerResultTabPanel.SetActiveTab<EventTimelineTab>(ComplaintAnalyzerResultTabs.EventTimeline);

            var eventsFilterPartyOptions = eventTabPanel.ComplaintAnalyserFilter.PartyFacet.FiltersCheckBoxes.Select(item => this.CleanText(item.Text)).ToList();

            this.TestCaseVerify.IsTrue(
                checkEventsTabSectionsForTextUpload,
                eventTabPanel.ResultList.EventsCardPlantiffLabels.Count > 0
                && eventTabPanel.ResultList.EventsCardDefendantLabels.Count > 0
                && eventTabPanel.ResultList.EventsTypeLabels.Count > 0,
                "Text Upload - Events tab cards does NOT contain all sections");

            this.TestCaseVerify.IsTrue(
                checkClaimsTabPanelFilterAfterDocumentUpload,
                eventTabPanel.ComplaintAnalyserFilter.PartyFacet.FilterButton.Displayed
                && eventsFilterPartyOptions.Distinct().All(items => defendantsList.Contains(items)),
                "Filter in events timeline tab is NOT displayed with all plaintiff and defendants for upload text");

            //Applying Filters
            var filterOptionEvents = eventTabPanel.ComplaintAnalyserFilter.PartyFacet.FiltersCheckBoxes.ElementAt(1).GetAttribute("current-value");
            eventTabPanel.ComplaintAnalyserFilter.PartyFacet.SelectParty(false, filterOptionEvents);

            defendentasPlaintiffList = eventTabPanel.ResultList.EventsCardDefendantLabels.Select(item => item.Text).ToList();
            countOfCards = eventTabPanel.ResultList.EventsCardLabels.Count;

            this.TestCaseVerify.IsTrue(
                checkEventsTabFilterSelectionForDocumentUpload,
                defendentasPlaintiffList.Count(item => item.Contains(filterOption)).Equals(countOfCards),
                "Filter in events tab is NOT filtering as per selection when analyzing text");

            //Verify Defenses tab
            var defensesTabPanel = complaintAnalyzerSkillLandingPage.ComplaintAnalyzerResultTabPanel.SetActiveTab<DefensesTab>(ComplaintAnalyzerResultTabs.Defenses);

            this.TestCaseVerify.IsTrue(
                checkDefensesTabSectionsForDocumentUpload,
                defensesTabPanel.ResultList.DefensesCardLabels.Count > 0
                && defensesTabPanel.ResultList.DefensesCardTableLabels.Count > 0
                && defensesTabPanel.ResultList.DefensesCardOverviewSectionLabels.Count > 0,
                "Text Upload - Defenses tab cards does NOT contain all sections");

            //Applying Filters
            var filterOptionDefenses = defensesTabPanel.ComplaintAnalyserFilter.PartyFacet.FiltersCheckBoxes.ElementAt(1).GetAttribute("current-value");
            defensesTabPanel.ComplaintAnalyserFilter.PartyFacet.SelectParty(false, filterOptionEvents);

            this.TestCaseVerify.IsTrue(
                checkDefensesTabFilterSelectionForDocumentUpload,
                defensesTabPanel.ResultList.DefensesCardLabels.Count.Equals(4),
                "Text Upload - Defenses tab Filter is NOT filtering as per selection");

            //Invalid conditions: Error message validation

            // Enter Complaint Text
            homePage = BrowserPool.CurrentBrowser.GoBack<PrecisionHomePage>();

            analyzeAComplaintPage = homePage.FeaturesIncludedPanel.GetLabsWidgetLinkByTitle(ComplaintAnalyzerLabel).Click<AiAnalyzeAComplaintPage>();

            enterComplaintTextTab = analyzeAComplaintPage.ComplaintAnalyzerTabPanel.SetActiveTab<EnterComplaintTextTab>(ComplaintAnalyzerTabs.EnterComplaintText);

            this.TestCaseVerify.IsTrue(
                checkTextCharacterLimit,
                enterComplaintTextTab.EnterComplaintTextbox.GetAttribute("validation-message").Equals(MoreInformationErrorMessage)
                && enterComplaintTextTab.SafeAnalysisButton.GetAttribute("class").Equals("disabled"),
                "Text character limit error messages are not displayed in complaint analyzer");

            Clipboard.SetText(StringExtensions.RandomString(230001, false));
            enterComplaintTextTab.EnterComplaintTextbox.SendKeys(Keys.Control + "v");

            this.TestCaseVerify.IsTrue(
                checkTextCharacterLimit,
                enterComplaintTextTab.RemainingCharacterLabel.Text.Equals("0 characters remaining")
                && enterComplaintTextTab.EnterComplaintTextbox.GetAttribute("validation-message").Equals(CharactersLimitErrorMessage)
                && enterComplaintTextTab.SafeAnalysisButton.GetAttribute("class").Equals("disabled")
                && enterComplaintTextTab.EnterComplaintTextbox.GetAttribute("maxlength").Equals("230000"),
                "Text character limit error messages are not dispalyed in complaint analyzer");
        }

        /// <summary>
        /// Test cases: 2226328
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestCategory(TeamMatzekFedRampCategory)]
        public void AiComplaintAnalyzerDeliveryTest()
        {
            string checkDeliveredData = "Verify: Delivered document contains all the required information";

            var documentPath = $@"{TestDocsFolderPath}\2023 WL 9108365.docx";
            var homePage = this.GetHomePage<PrecisionHomePage>();

            SafeMethodExecutor.WaitUntil(() => homePage.FeaturesIncludedPanel.GetLabsWidgetTextByTitle(ComplaintAnalyzerLabel).Displayed);

            var analyzeAComplaintPage = homePage.FeaturesIncludedPanel.GetLabsWidgetLinkByTitle(ComplaintAnalyzerLabel).Click<AiAnalyzeAComplaintPage>();
            var uploadADocumentTab = analyzeAComplaintPage.ComplaintAnalyzerTabPanel.SetActiveTab<UploadADocumentTab>(ComplaintAnalyzerTabs.UploadADocument);

            // Upload A Document
            uploadADocumentTab = uploadADocumentTab.UploadFile(documentPath);

            SafeMethodExecutor.WaitUntil(() => uploadADocumentTab.SafeAnalysisButton.Enabled);

            var complaintAnalyzerSkillLandingPage = uploadADocumentTab.SafeAnalysisButton.Click<ComplaintAnalyzerSkillLandingPage>();

            SafeMethodExecutor.WaitUntil(() => complaintAnalyzerSkillLandingPage.ProgressBarLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => !complaintAnalyzerSkillLandingPage.ProgressBarLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => complaintAnalyzerSkillLandingPage.AnalyzeNewComplaintButton.Enabled);

            var summaryTabPanel = complaintAnalyzerSkillLandingPage.ComplaintAnalyzerResultTabPanel.SetActiveTab<SummaryTab>(ComplaintAnalyzerResultTabs.Summary);
            complaintAnalyzerSkillLandingPage.Toolbar.ComplaintAnalyzerDeliveryDropdown.SelectOption<ComplaintAnalyzerSkillLandingPage>(DeliveryOptions.PDF);

            var fileName = $"Westlaw Precision - Complaint Analysis - {DateTime.Now.ToString("MM-dd-yyyy")}.pdf";
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            var text = this.CleanTextForCompare(PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, fileName)));

            this.TestCaseVerify.IsTrue(
                checkDeliveredData,
                text.Contains("ComplaintAnalysis")
                && text.Contains("Summary")
                && text.Contains("Claims")
                && text.Contains("Eventtimeline")
                && text.Contains("Defenses"),
                "Delivered Complaint Analyzer document doesn't contain all the required information");
        }
        private string CleanText(string text)
        {
            text = text.Replace("dba:", string.Empty).Replace("violations", "violation").Replace("commonlaw", string.Empty).Replace("violationof", string.Empty).Replace("-lethalnegligence", string.Empty).Replace("-non", string.Empty).Replace(" ", string.Empty).Replace(")", string.Empty).Replace("\r\n", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Replace(".", string.Empty).Replace(",", string.Empty).ToLower();

            return text;
        }

        private string CleanTextForCompare(string text) => text.Replace(" ", string.Empty).Replace(" ", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Replace(" ", string.Empty).Replace("\r\n", string.Empty).Replace("’", string.Empty).Replace("'", string.Empty).Replace("§ ", string.Empty);
    }
}
