namespace WestlawPrecision.Tests.Aalp
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.Utils.Extensions;
    using Framework.Common.UI.Products.WestlawAdvantage.Pages.DeepResearch;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;

    /// <summary>
    /// AI Deep Research tests (aka Guided Research)
    /// </summary>
    [TestClass]
    public class AalpDeepResearchTests : AalpBaseTest
    {
        private const string FeatureTestCategoryDeepResearch = "DeepResearch";

        /// <summary>
        /// Test Deep Research aka Guided Research landing page displays correctly.
        /// User story: 2156413 Task: 2168148
        /// 1. Sign in WL Precision with routing (QED has to use site: use2):
        ///    IAC: IAC-AI-WESTLAW-LABS-RAPID-PROTOTYPE,IAC-FAC-REGION-ROUTING,IAC-AI-WESTLAW-GUIDED-RESEARCH
        ///    FAC: AIWestlawGuidedResearch, AIWestlawLabsInterface
        /// 2. Click Westlaw Deep Research card on the home page
        /// 3. Check: Verify welcome message is displayed on landing page
        /// 4. Check: Verify Question button displayed on landing page
        /// 5. Check: Verify selected jurisdictions displayed on landing page
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamDahlCategory)]
        public void DeepResearchSmokeTest()
        {
            const string DeepResearchWidgetLabel = "Westlaw Deep AI Research";
            const string WelcomeMessage = "Deep AI Research";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            AiDeepResearchPage deepResearchPage = homePage.FeaturesIncludedPanel.GetLabsWidgetLinkByTitle(DeepResearchWidgetLabel).Click<AiDeepResearchPage>();

            this.TestCaseVerify.IsTrue(
                "Verify welcome message is displayed on landing page",
                deepResearchPage.WelcomeComponent.WelcomeHeaderLabel.Text.Contains(WelcomeMessage),
                "Welcome message is not displayed on landing page");

            this.TestCaseVerify.IsTrue(
                "Verify Question button displayed on landing page",
                deepResearchPage.WelcomeComponent.InputComponent.SendButton.Displayed,
                "Question button not displayed on page");

            this.TestCaseVerify.IsFalse(
                "Verify selected jurisdictions displayed on landing page",
                string.IsNullOrEmpty(deepResearchPage.WelcomeComponent.InputComponent.JurisdictionButton.Text),
                "Selected jurisdictions not displayed on landing page");
        }

        protected override void InitializeRoutingPageSettings()
        {
            this.Settings.AppendValues(
                EnvironmentConstants.InfrastructureAccessControlsOn,
                SettingUpdateOption.Append,
                "IAC-AI-WESTLAW-LABS-RAPID-PROTOTYPE",
                "IAC-FAC-REGION-ROUTING",
                "IAC-AI-WESTLAW-GUIDED-RESEARCH");

            this.Settings.AppendValues(
                EnvironmentConstants.FeatureAccessControlsOn,
                SettingUpdateOption.Append,
                FeatureAccessControl.AIWestlawGuidedResearch);

            this.Settings.AppendValues(
                EnvironmentConstants.FeatureAccessControlsOn,
                SettingUpdateOption.Append,
                FeatureAccessControl.AIWestlawLabsInterface);
        }
    }
}

