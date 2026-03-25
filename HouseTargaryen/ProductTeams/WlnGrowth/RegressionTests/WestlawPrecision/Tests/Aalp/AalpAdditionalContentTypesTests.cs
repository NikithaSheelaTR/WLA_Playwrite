namespace WestlawPrecision.Tests.Aalp
{
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Products.Shared.Dialogs.Delivery;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
    using Framework.Common.UI.Products.WestLawNext.Enums.Delivery;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Toolbars;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Raw.EnhancementTests.Utils;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.Extensions;
    using Framework.Core.Utils.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading;
    using System.Linq;
    using System.IO;
    using System;

    /// <summary>
    /// Aalp Additional ContentT types Tests
    /// </summary>
    [TestClass]
    public class AalpAdditionalContentTypesTests : AalpBaseTest
    {
        private const string AalpAdditionalContentTestCategory = "AalpAdditionalContentTest";
        private const string AssistedResearchLabel = "AI-Assisted Research";

        /// <summary>
        /// Verify additional content features on Precision landing page
        /// Test case: 1887912,1901351  User Story: 1885139,1884960,1884976,1899948
        /// 1. Sign in WL Precision with PLAskPlus FAC set to Grant
        /// 2. Click AI-Assisted Research card from Key Features in home page and Navigate to AAR landing page on new tab
        /// 3. Ask and submit question: What is the definition of substantial evidence?
        /// 4. Check: Verify new disclaimer system message displayed 
        /// 5. Check: Verify content labels displayed in expected order
        /// 6. Check: Verify Case meta data label displayed
        /// 7. Click 1st Case doc link. Do checking below and go back to results
        /// 8. Check: Verify Case document page is opened in the same tab
        /// 9. Check: Verify Case document page does not display document navigation widget
        /// 10. Check: Verify Case document page does not display term navigatioin widget
        /// 11.Check: Verify Case document page does not display term highlighting widget
        /// 12. Check: Verify Secondary Source meta data label displayed
        /// 13. Click 1st Secondary Source document link. Do checking below and go back to results
        /// 14.Check: Verify Secondary Source document page is opened in the same tab
        /// 15.Check: Verify Admin Decision meta data label displayed
        /// 16.Click 1st Admin Decision doc link. Do checking below and go back to results
        /// 17.Check: Verify Admin Decision document page is opened in the same tab
        /// 18.Check: Verify Practical Law meta data label displayed
        /// 19.Click 1st Practical Law doc link. Do checking below and go back to results
        /// 20.Check: Verify Practical Law document page is opened in the same tab
        /// 21.Check: Verify Practical Law document page has Return to report button
        /// 22.Check: Verify Practical Law document Return to report button works
        /// 23.Ask follow-up question: What are the cases involving substantial evidence?
        /// 24.Check: Verify only Case content label displayed on follow-up question results
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamDahlNonAjsCategory)]
        [TestCategory(TeamDahlCategory)]
        [TestCategory(AalpAdditionalContentTestCategory)]
        [TestCategory("TransitionToSharat")]
        public void AdditionalContentLandingPageTest()
        {
            const string Question = "What is the definition of substantial evidence?";
            const string Question2 = "What are the cases involving substantial evidence?";
            const string ExpectedContentLabels = "Cases, statutes, and regulations;Administrative decisions and guidance;Practical Law;Additional secondary sources;Current awareness";
            const string ExpectedContentLabels2 = "Cases, statutes, and regulations";
            const string ExpectedSystemMessage = "The above response is AI-generated and may contain errors. It should be verified for accuracy.";

            var aiAssistantPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(AssistedResearchLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);
            Thread.Sleep(2000);
            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchPage>();

            aiAssistantPage.QueryBox.QuestionTextbox.SetText(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                "Verify new disclaimer system message displayed",
                aiAssistantPage.Chat.ChatSummaryLabel.Text.Contains(ExpectedSystemMessage),
                "New disclaimer system message not displayed");

            var contentLabels = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.ContentLabels.Select(label => label.Text).ToList();
            string displayedContentLabels = string.Join(";", contentLabels);
            this.TestCaseVerify.IsTrue(
                "Verify content labels displayed in expected order",
                ExpectedContentLabels.Equals(displayedContentLabels),
                "Content labels not displayed in expected order. Expected:" + ExpectedContentLabels + " Displayed:" + displayedContentLabels);

            string caseMetaLabel = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.CasesItems.First().MetadataLabel.Text;
            this.TestCaseVerify.IsTrue(
                "Verify Case meta data label displayed",
                caseMetaLabel != null && caseMetaLabel.Trim().Length > 0,
                "Case meta data label not displayed");

            var documentPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.CasesItems.First().DocumentTitleLink.Click<EdgeCommonDocumentPage>();

            this.TestCaseVerify.IsTrue(
               "Verify Case document page is opened in the same tab",
               documentPage.IsDocumentLoaded(),
               "Case document page is not opened in the same tab");

            this.TestCaseVerify.IsFalse(
               "Verify Case document page does not display document navigation widget",
               documentPage.Toolbar.NavigationComponent.IsDisplayed(),
               "Case document page is not opened in the same tab");

            this.TestCaseVerify.IsFalse(
               "Verify Case document page does not display term navigatioin widget",
               documentPage.Toolbar.IsToolbarElementDisplayed(EdgeToolbarElements.TermNavigation),
               "Case document page is not opened in the same tab");

            this.TestCaseVerify.IsFalse(
               "Verify Case document page does not display term highlighting widget",
               documentPage.Toolbar.IsToolbarElementDisplayed(EdgeToolbarElements.FocusHighlighting),
               "Case document page is not opened in the same tab");

            aiAssistantPage = documentPage.FixedHeader.ClickReturnToListButton<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            string secondarySourceMetaLabel = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SecondarySourcesItems.First().MetadataLabel.Text;
            this.TestCaseVerify.IsTrue(
                "Verify Secondary Source meta data label displayed",
                secondarySourceMetaLabel != null && secondarySourceMetaLabel.Trim().Length > 0,
                "Secondary Source meta data label not displayed");

            documentPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SecondarySourcesItems.First().DocumentTitleLink.Click<EdgeCommonDocumentPage>();

            this.TestCaseVerify.IsTrue(
               "Verify Secondary Source document page is opened in the same tab",
               documentPage.IsDocumentLoaded(),
               "Secondary Source document page is not opened in the same tab");

            aiAssistantPage = documentPage.FixedHeader.ClickReturnToListButton<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            string adminDecisionMetaLabel = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.AdminDecisionsItems.First().MetadataLabel.Text;
            this.TestCaseVerify.IsTrue(
                "Verify Admin Decision meta data label displayed",
                adminDecisionMetaLabel != null && adminDecisionMetaLabel.Trim().Length > 0,
                "Admin Decision meta data label not displayed");

            documentPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.AdminDecisionsItems.First().DocumentTitleLink.Click<EdgeCommonDocumentPage>();

            this.TestCaseVerify.IsTrue(
               "Verify Admin Decision document page is opened in the same tab",
               documentPage.IsDocumentLoaded(),
               "Admin Decision document page is not opened in the same tab");

            aiAssistantPage = documentPage.FixedHeader.ClickReturnToListButton<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            string practicalLaweMetaLabel = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.PracticalLawItems.First().MetadataLabel.Text;
            this.TestCaseVerify.IsTrue(
                "Verify Practical Law meta data label displayed",
                practicalLaweMetaLabel != null && practicalLaweMetaLabel.Trim().Length > 0,
                "Practical Law meta data label not displayed");

            documentPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.PracticalLawItems.First().DocumentTitleLink.Click<EdgeCommonDocumentPage>();

            this.TestCaseVerify.IsTrue(
               "Verify Practical Law document page is opened in the same tab",
               documentPage.IsDocumentLoaded(),
               "Practical Law document page is not opened in the same tab");

            this.TestCaseVerify.IsTrue(
               "Verify Practical Law document page has Return to report button",
               documentPage.FixedHeader.PLDocReturnToReportButton.Displayed,
               "Practical Law document page does not have Return to report button");

            aiAssistantPage = documentPage.FixedHeader.PLDocReturnToReportButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                "Verify Practical Law document Return to report button works",
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.PracticalLawItems.First().MetadataLabel.Displayed,
                "Practical Law document Return to report button does not work");

            // Additional content types should not be included on follow-up question results
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question2);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).ProgressDotsLabel.Displayed);

            contentLabels = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.ContentLabels.Select(label => label.Text).ToList();
            displayedContentLabels = string.Join("", contentLabels);
            this.TestCaseVerify.IsTrue(
                "Verify only Case content label displayed on follow-up question results",
                ExpectedContentLabels2.Equals(displayedContentLabels),
                "Case content label not displayed as expected. Expected:" + ExpectedContentLabels2 + " Displayed:" + displayedContentLabels);
        }

        /// <summary>
        /// Verify Secondary Sources summary on AAR result page
        /// Test task: 2134362  User Story: 2134170
        /// 1. Sign in WLP with access: IAC-AI-ADD-SECONDARY-SOURCES-TO-AAR
        /// 2. Run AAR search: Which party must demonstrate why discovery would be burdensome?
        /// 3. Check: Verify disclaimer system message displayed
        /// 4. Check: Verify secondary sources summaries heading displayed
        /// 5. Check: Verify One and up to three secondary sources summaries displayed
        /// 6. Capture the three secondary sources summary title text
        /// 7. Capture the three secondary sources supporting material title text
        /// 8. Check: Verify secondary sources summaries not repeated in supporting materials
        /// 9. Click first summary title link
        /// 10.Check: Verify clicking first summary title opens document page
        /// 11.Return to result and capture first summary title text
        /// 12.Ask follow-up question: Discuss rulings from the 5th circuit on this matter.
        /// 13.Check: Verify secondary sources summaries displayed on follow-up results
        /// 14.Download result in pdf then open downloaded file
        /// 15.Check: Verify delivery contains SS section heading and first title text
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamDahlCategory)]
        [TestCategory(AalpAdditionalContentTestCategory)]
        [TestCategory("TransitionToSharat")]
        public void SecondarySourcesSummariesTest()
        {
            const string Question = "Which party must demonstrate why discovery would be burdensome?";
            const string Question2 = "Discuss rulings from the 5th circuit on this matter.";
            const string SummariesHeading = "Commentary about this question";
            const string ExpectedSystemMessage = "The above response is AI-generated and may contain errors. It should be verified for accuracy.";

            var aiAssistantPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(AssistedResearchLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);
            Thread.Sleep(2000);
            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchPage>();

            aiAssistantPage.QueryBox.QuestionTextbox.SetText(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                "Verify disclaimer system message displayed",
                aiAssistantPage.Chat.AdditionalSummariesDisclaimerLabel.Text.Contains(ExpectedSystemMessage),
                "Disclaimer system message not displayed");

            this.TestCaseVerify.IsTrue(
                "Verify secondary sources summaries heading displayed",
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SecondarySourcesSummariesHeadingLabel.Text.Contains(SummariesHeading),
                "Secondary sources summaries heading not displayed");

            var numberOfSummaries = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SecondarySourcesSummariesItems.Count;
            this.TestCaseVerify.IsTrue(
                "Verify One and up to three secondary sources summaries displayed",
                numberOfSummaries >= 1 && numberOfSummaries <= 3,
                "Wrong number of secondary sources summaries displayed: " + numberOfSummaries);

            // Summary documents should not be repeated in supporting materials section
            var firstSSTitle = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SecondarySourcesSummariesItems.First().DocumentTitleLink.Text;
            var summaryTitles = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SecondarySourcesSummariesItems.Select(item => item.DocumentTitleLink.Text).ToList();
            var supportTitles = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SecondarySourcesItems.Select(item => item.DocumentTitleLink.Text).ToList();
            var duplicateTitles = summaryTitles.Intersect(supportTitles).ToList();

            this.TestCaseVerify.IsTrue(
                "Verify secondary sources summaries not repeated in supporting materials",
                duplicateTitles.Count == 0,
                "Secondary sources summaries repeated in supporting materials: " + duplicateTitles.Count);

            var documentPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SecondarySourcesSummariesItems.First().DocumentTitleLink.Click<EdgeCommonDocumentPage>();
            this.TestCaseVerify.IsTrue(
               "Verify clicking first summary title opens document page",
               documentPage.IsDocumentLoaded(),
               "Clicking first summary title does not open document page");

            aiAssistantPage = documentPage.FixedHeader.ClickReturnToListButton<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            // Secondary sources summaries should be included on follow-up question results
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question2);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).ProgressDotsLabel.Displayed);

            numberOfSummaries = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.ElementAt(1).SupportingMaterials.SecondarySourcesSummariesItems.Count;
            this.TestCaseVerify.IsTrue(
                "Verify secondary sources summaries displayed on follow-up results",
                numberOfSummaries >= 1 && numberOfSummaries <= 3,
                "Secondary sources summaries not displayed on follow-up results");

            // Download result and check summaries are included
            var downloadDialog = aiAssistantPage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);
            downloadDialog.LayoutAndLimitsTab.SetIncludeSectionOption(LayoutAndLimitsInclude.CoverPage);
            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<AiAssistedResearchPage>();
            var fileName = $"Westlaw Precision - Westlaw AI-Assisted Research - {DateTime.Now.ToString(DeliveryDateFormat)}.pdf";
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            var text = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, fileName));

            this.TestCaseVerify.IsTrue(
                "Verify delivery contains SS section heading and first title text",
                text.Contains(SummariesHeading) && text.Contains(firstSSTitle),
                "Delivery does not contain section heading or first title text");
        }

        /// <summary>
        /// Verify passage links on AAR result page
        /// 1. Sign in WL Precision with AAR access
        /// 2. Click AI-Assisted Research card from Key Features in home page and Navigate to AAR landing page on new tab
        /// 3. Ask question: How close must a toilet paper dispenser be to a toilet under the ADA?
        /// 4. Click on the last passage link under the first Admin decision result
        /// 5. Check: Verify document page opened clicking last passage link under first Admin decisions result
        /// 6. Check: Verify Pinpoint green arrow is displayed after clicking Admin decisions passage link
        /// 7. Click Return to Report button and click last passage link under the first PL result
        /// 8. Check: Verify document page opened clicking last passage link under first Practical Law result
        /// 9. Check: Verify Pinpoint green arrow is displayed after clicking Practical Law passage link
        /// 10.Click Return to Report button and click last passage link under first Secondary sources result
        /// 11.Check: Verify document page opened clicking last passage link under first Secondary sources result
        /// 12.Check: Verify Pinpoint green arrow is displayed after clicking Secondary sources passage link
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamDahlNonAjsCategory)]
        [TestCategory(TeamDahlCategory)]
        [TestCategory(AalpAdditionalContentTestCategory)]
        [TestCategory("TransitionToSharat")]
        public void ActPassageLinksTest()
        {
            const string Question = "How close must a toilet paper dispenser be to a toilet under the ADA?";

            var aiAssistantPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(AssistedResearchLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);
            Thread.Sleep(2000);
            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchPage>();

            aiAssistantPage.QueryBox.QuestionTextbox.SetText(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            var documentPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.AdminDecisionsItems.First().DocumentPassageLinks.Last().Click<EdgeCommonDocumentPage>();

            this.TestCaseVerify.IsTrue(
               "Verify document page opened clicking last passage link under first Admin decisions result",
               documentPage.IsDocumentLoaded(),
               "Document page is not opened correctly");

            this.TestCaseVerify.IsTrue(
               "Verify Pinpoint green arrow is displayed after clicking Admin decisions passage link",
               documentPage.IsBestPortionArrowDisplayed(),
               "Pinpoint green arrow is not displayed after clicking Admin decisions passage link");

            aiAssistantPage = documentPage.FixedHeader.ClickReturnToListButton<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            documentPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.PracticalLawItems.First().DocumentPassageLinks.Last().Click<EdgeCommonDocumentPage>();

            this.TestCaseVerify.IsTrue(
               "Verify document page opened clicking last passage link under first Practical Law result",
               documentPage.IsDocumentLoaded(),
               "Document page is not opened correctly");

            this.TestCaseVerify.IsTrue(
               "Verify Pinpoint green arrow is displayed after clicking Practical Law passage link",
               documentPage.IsBestPortionArrowDisplayed(),
               "Pinpoint green arrow is not displayed after clicking Practical Law passage link");

            aiAssistantPage = documentPage.FixedHeader.PLDocReturnToReportButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            documentPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SecondarySourcesItems.First().DocumentPassageLinks.Last().Click<EdgeCommonDocumentPage>();

            this.TestCaseVerify.IsTrue(
               "Verify document page opened clicking last passage link under first Secondary sources result",
               documentPage.IsDocumentLoaded(),
               "Document page is not opened correctly");

            this.TestCaseVerify.IsTrue(
               "Verify Pinpoint green arrow is displayed after clicking Secondary sources passage link",
               documentPage.IsBestPortionArrowDisplayed(),
               "Pinpoint green arrow is not displayed after clicking Secondary sources passage link");
        }

        /// <summary>
        /// THIS TEST IS EXCLUDED FOR NOW
        /// Verify additional content features on Precision landing page
        /// Test case: 1901674  User Story: 1901329
        /// 1. Sign in WL Precision with PLAskPlus FAC set to Deny
        /// 2. Click AI-Assisted Research card from Key Features in home page and Navigate to AAR landing page on new tab
        /// 3. Ask and submit question: What is the definition of substantial evidence?
        /// 4. Check: Verify content labels displayed in expected order without PL content
        /// </summary>
        [TestMethod] // Turns out this may not be a valid test case. Stop running this test for now
        //[TestCategory(CurrentTestCategory)]
        //[TestCategory(TeamDahlNonAjsCategory)]
        //[TestCategory(TeamDahlCategory)]
        //[TestCategory(AalpAdditionalContentTestCategory)]
        [TestProperty("PlAskPlus", "No")]
        [TestCategory("TransitionToSharat")]
        public void ActNoPlAskPlusAccessTest()
        {
            const string Question = "What is the definition of substantial evidence?";
            const string ExpectedContentLabels = "Cases, statutes, and regulations;Administrative decisions and guidance;Additional secondary sources;Current awareness";
            
            var aiAssistantPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(AssistedResearchLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);
            Thread.Sleep(2000);
            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchPage>();

            aiAssistantPage.QueryBox.QuestionTextbox.SetText(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();

            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            var contentLabels = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.ContentLabels.Select(label => label.Text).ToList();
            string displayedContentLabels = string.Join(";", contentLabels);
            this.TestCaseVerify.IsTrue(
                "Verify content labels displayed in expected order without PL content",
                ExpectedContentLabels.Equals(displayedContentLabels),
                "Content labels not displayed in expected order. Expected:" + ExpectedContentLabels + " Displayed:" + displayedContentLabels);
        }

        protected override void InitializeRoutingPageSettings()
        {
            base.InitializeRoutingPageSettings();

            if ((this.TestContext.Properties["PlAskPlus"] != null) &&
                (this.TestContext.Properties["PlAskPlus"].Equals("No")))
                this.Settings.AppendValues(
                    EnvironmentConstants.FeatureAccessControlsOff,
                    SettingUpdateOption.Append,
                    FeatureAccessControl.PLAskPlus);
            else
                this.Settings.AppendValues(
                    EnvironmentConstants.FeatureAccessControlsOn,
                    SettingUpdateOption.Append,
                    FeatureAccessControl.PLAskPlus);
        }
    }
}



