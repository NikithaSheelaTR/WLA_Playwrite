namespace WestlawPrecision.Tests.Aalp
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.Utils.Execution;
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Products.Shared.Enums.Content;

    /// <summary>
    /// AalpSupportingDocOopBannerTest
    /// </summary>
    [TestClass]
    public class AalpSupportingDocOopBannerTest : AalpSupportingDocOopBannerBaseTest
    {
        private const string FeatureTestCategoryOOP = "AalpSupportingDocOop";

        /// <summary>
        /// Verify Out of plan banner is displayed as expected for OOP user
        /// Test cases: 1876417  User Story 1870595
        /// 1. Sign in WL Precision as Out of plan user
        /// 2. Click AI-Assisted Research in Key Features section
        /// 3. Ask question: How close must a toilet paper dispenser be to a toilet under the ADA?
        /// 4. Check: Verify Out of plan banner is displayed on first supporting material
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryOOP)]
        [TestCategory(TeamDahlNonAjsCategory)]
        [TestCategory(TeamDahlCategory)]
        [TestCategory("TransitionToSharat")]
        public void VerifyOutOfPlanBannersTest()
        {
            const string ResearchLabel = "AI-Assisted Research";
            const string Question = "How close must a toilet paper dispenser be to a toilet under the ADA?";
            const string ExpectedOopBannerText = "Out of plan";
            string checkOutOfPlanBanner = "Verify: Out of plan banner is displayed";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            var aiAssistantPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(ResearchLabel).Click<AiAssistedResearchPage>();            
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

            aiAssistantPage = aiAssistantPage.QueryBox.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectJurisdictions(true, Jurisdiction.Washington).SaveButton.Click<AiAssistedResearchPage>();
            aiAssistantPage = aiAssistantPage.QueryBox.QuestionTextbox.SetText<AiAssistedResearchPage>(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            var displayedBannerText = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().SupportingMaterials.SupportingMaterialsItems.First().OutOfPlanBannerLabel.Text;

            this.TestCaseVerify.IsTrue(
               checkOutOfPlanBanner,
               ExpectedOopBannerText.Equals(displayedBannerText),
               "OOP banner not displayed correctly. Displayed: " + displayedBannerText + " Expected: " + ExpectedOopBannerText);
        }
    }
}
