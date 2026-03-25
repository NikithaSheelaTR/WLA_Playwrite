namespace WestlawPrecision.Tests.Aalp
{
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Foldering;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.NewTypeAhead;
    using Framework.Common.UI.Products.WestlawEdge.Pages.SearchResult.ContentTypeSearchResultPages;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Header;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.NewTypeAhead;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Toolbars;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading;

    /// <summary>
    /// Aalp Foldering Feature Tests
    /// </summary>
    [TestClass]
    public class AalpFolderingTests : AalpBaseTest
    {
        private const string AalpFolderingTestCategory = "AalpFolderingTest";
        private const string ClaimsExplorerLabel = "Claims Explorer";

        /// <summary>
        /// Verify foldering AAR results feature on Precision landing page
        /// Test case: 1998030,1998031,2001301 User Story: 1891512
        /// 1. Sign in WL Precision with IAC on: IAC-AI-AAR-FOLDERING
        /// 2. Go to Folders page and clear all foldered items in root folder
        /// 3. Go to AAR landing page and ask question: What is the definition of substantial evidence?
        /// 4. Click Folder button and select root folder to save the AAR result
        /// 5. Check: Verify fodering successfully message
        /// 6. Click Folder button and select root folder to save the AAR result again
        /// 7. Check: Verify foldering cannot add duplicate message
        /// 8. Go to Folders page and view root folder
        /// 9. Check: Verify AAR result is foldered with research question as foldered title
        /// 10.Check the checkbox next to the foldered AAR result and click Download button
        /// 11.Check: Verify InfoBox block message displayed when trying to download foldered AAR
        /// 12.Click foldered AAR result title (same as the research question). 
        /// 13.Check: Verify clicking foldered title takes to AAR result page
        /// 14.Go to Folders page and clear all foldered items in root folder
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamDahlNonAjsCategory)]
        [TestCategory(TeamDahlCategory)]
        [TestCategory(AalpFolderingTestCategory)]
        [TestCategory("TransitionToSharat")]
        public void AddAarResultToFolderTest()
        {
            const string Question = "What is the definition of substantial evidence?";
            const string ExpectedDeliveryInfoMessage = "Your request contains AI-Assisted Research items, which are not deliverable.";
            const string AssistantResearchLabel = "AI-Assisted Research";

            var folderPage = EdgeNavigationManager.Instance.GoToFoldersPage<EdgeResearchOrganizerPage>();
            folderPage.ClearFolderGrid();

            var homePage = this.GetHomePage<PrecisionHomePage>();
            homePage = homePage.Header.ClickLogo<PrecisionHomePage>();
            var aiAssistantPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AssistantResearchLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);
            Thread.Sleep(2000);
            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchPage>();

            aiAssistantPage.QueryBox.QuestionTextbox.SetText(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            var saveToFolderDialog = aiAssistantPage.Toolbar.SaveToFolderButton.Click<EdgeSaveToFolderDialog>();
            string rootFolder = saveToFolderDialog.FolderTreeComponent.GetRootFolderName();
            saveToFolderDialog.FolderTreeComponent.SelectFolderByName(rootFolder);

            saveToFolderDialog.ClickSaveButton<AiAssistedResearchPage>();
            string folderMessage = aiAssistantPage.FolderMessageLabel.Text;
            // This check indirectly tests: Bug 2032840: AALP: Foldering - Folder dialog remains after clicking Save button
            this.TestCaseVerify.IsTrue(
                "Verify fodering successfully message",
                folderMessage.Contains(Question) && folderMessage.Contains("saved to"),
                "Saved to message not displayed");

            saveToFolderDialog = aiAssistantPage.Toolbar.SaveToFolderButton.Click<EdgeSaveToFolderDialog>();
            rootFolder = saveToFolderDialog.FolderTreeComponent.GetRootFolderName();
            saveToFolderDialog.FolderTreeComponent.SelectFolderByName(rootFolder);

            saveToFolderDialog.ClickSaveButton<AiAssistedResearchPage>();
            folderMessage = aiAssistantPage.FolderMessageLabel.Text;
            this.TestCaseVerify.IsTrue(
                "Verify foldering cannot add duplicate message",
                folderMessage.Contains(Question) && folderMessage.Contains("Cannot add duplicates"),
                "Research is not added to the folder");

            var recentFolderDialog = aiAssistantPage.Header.ClickHeaderTab<EdgeRecentFoldersDialog>(EdgeHeaderTabs.Folders);
            folderPage = recentFolderDialog.ClickFolderByName(rootFolder).ClickViewThisFolderButton();
            this.TestCaseVerify.IsTrue(
                "Verify AAR result is foldered with research question as foldered title",
                folderPage.FolderGrid.IsItemDisplayed(Question),
                "AAR result is not added to the folder");

            folderPage.FolderGrid.SelectItemByName(Question);
            folderPage.EdgeToolbar.DeliveryDropdown.SelectOption(DeliveryMethod.Download);
            folderPage.HoverOverQuickAccessPanel();//Download tooltip blocks the message. Move cursor elsewhere to see the message.
            string infoBoxMessage = folderPage.EdgeToolbar.AarInfoBoxMessageLabel.Text;

            this.TestCaseVerify.IsTrue(
                "Verify InfoBox block message displayed when trying to download foldered AAR",
                infoBoxMessage.Contains(ExpectedDeliveryInfoMessage),
                "InfoBox Message not displayed when trying to deliver foldered AAR");

            aiAssistantPage = folderPage.FolderGrid.ClickGridItemByName<AiAssistedResearchPage>(Question);
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                "Verify clicking foldered title takes to AAR result page",
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AnswerLabel.Displayed
                && aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SupportingMaterialsItems.Any(),
                "Clicking foldered title does not take to AAR result page");

            folderPage = EdgeNavigationManager.Instance.GoToFoldersPage<EdgeResearchOrganizerPage>();
            folderPage.ClearFolderGrid();
        }

        /// <summary>
        /// Verify Folder availibility on AAR Treatise results page
        /// Test case: 2048372 User Story: 1891502
        /// 1. Sign in WL Precision with IAC on: IAC-AI-AAR-FOLDERING
        /// 2. Type in search box: Civil Procedure Before Trial (The Rutter Group, California Practice Guide)
        /// 3. Click to select the first suggested link
        /// 4. Submit the question: Can parties orally stipulate to change the date of a deposition?
        /// 5. Check: Verify Foder button displayed on Treatise research results page
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamDahlNonAjsCategory)]
        [TestCategory(TeamDahlCategory)]
        [TestCategory(AalpFolderingTestCategory)]
        [TestCategory("TransitionToSharat")]
        public void AarTreatiseResultFolderTest()
        {
            const string TreatisePage = "Civil Procedure Before Trial (The Rutter Group, California Practice Guide)";
            const string Question = "Can parties orally stipulate to change the date of a deposition?";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            Thread.Sleep(2000);
            var typeahead = homePage.Header.EnterSearchQuery<TrdTypeAheadDialog>(TreatisePage);
            var categoryPage = typeahead.OtherComponent.ClickOnSuggestionByIndex<EdgeContentTypeSearchResultPage>(NewTypeAheadCategories.ContentPages, 0);
            var aiAssistantPage = categoryPage.Toolbar.ClickToolbarElement<AiAssistedResearchPage>(EdgeToolbarElements.SearchAndSummarizeRutter);

            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                "Verify Foder button displayed on Treatise research results page",
                aiAssistantPage.Toolbar.SaveToFolderButton.Displayed,
                "Foder button not displayed on Treatise research results page");
        }

        /// <summary>
        /// Verify Folder is not available on AAR Claims results page
        /// Test case: 2048372 User Story: 1891502
        /// 1. Sign in WL Precision with IAC on: IAC-AI-AAR-FOLDERING
        /// 2. Access Claims Explorer via the Key Features section
        /// 3. Submit question: An immigrant worked with a non-profit...information in a public court filing.
        /// 4. Check: Verify Foder button not displayed on Claims research results page
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamDahlNonAjsCategory)]
        [TestCategory(TeamDahlCategory)]
        [TestCategory(AalpFolderingTestCategory)]
        [TestCategory("TransitionToSharat")]
        public void AarClaimsResultFolderTest()
        {
            const string Question = "An immigrant worked with a non-profit that provided immigration bonds. The insurer of the bonds had access to the non-profit's computer system. The insurer wrongfully accessed his personal information including emails between him and the non-profit, then subsequently exposed the login information in a public court filing.";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            Thread.Sleep(2000);
            homePage.Header.OpenJurisdictionDialog<EdgeJurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<PrecisionHomePage>();

            var claimsExplorerPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(ClaimsExplorerLabel).Click<AiClaimsExplorerPage>();
            BrowserPool.CurrentBrowser.CreateTab(ClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(ClaimsExplorerTab);

            claimsExplorerPage = claimsExplorerPage.QueryBox.QuestionTextbox.SetText<AiClaimsExplorerPage>(Question);
            claimsExplorerPage = claimsExplorerPage.QueryBox.SendQuestionButton.Click<AiClaimsExplorerPage>();
            SafeMethodExecutor.WaitUntil(() => !claimsExplorerPage.Chat.ClaimsExplorerQuestionAndAnswerItem.ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsFalse(
                "Verify Foder button not displayed on Claims research results page",
                claimsExplorerPage.Toolbar.SaveToFolderButton.Displayed,
                "Foder button displayed on Claims research results page");
        }

        protected override void InitializeRoutingPageSettings()
        {
            this.Settings.AppendValues(
                EnvironmentConstants.InfrastructureAccessControlsOn,
                SettingUpdateOption.Append,
                "IAC-AI-AAR-FOLDERING");
        }
    }
}