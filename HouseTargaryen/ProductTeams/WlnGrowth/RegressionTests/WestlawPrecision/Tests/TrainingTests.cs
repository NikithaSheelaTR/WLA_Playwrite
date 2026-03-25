//namespace WestlawPrecision.Tests
//{
//    using Framework.Common.UI.Products.Shared.Pages;
//    using Framework.Common.UI.Utils.Browser;
//    using Framework.Core.CommonTypes.Configuration;
//    using Framework.Core.CommonTypes.Constants;
//    using Framework.Core.CommonTypes.Enums;
//    using global::WestlawPrecision.Tests.Aalp;
//    using Microsoft.VisualStudio.TestTools.UnitTesting;
//    using Framework.Core.Utils.Extensions;
//    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;
//    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.AALP;
//    using Framework.Common.UI.Products.WestlawEdgePremium.Enums;
//    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
//    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
//    using Framework.Common.UI.Raw.WestlawEdge.Pages;
//    using System.Linq;
//    using Framework.Common.UI.Raw.WestlawEdge.Enums.Header;

//    /// <summary>
//    /// AALP training tests
//    /// </summary>
//    [TestClass]
//    public class TrainingTests : AalpBaseTest
//    {
//        /// <summary>
//        /// Verify AI Assistant is unavailable in WestLaw Edge
//        /// </summary>
//        [TestMethod]
//        [TestProperty("AiResearch", "Off")]
//        public void NoAiAssistantInWestlawEdgeTest()
//        {
//            string checkAiAssistantUnavailable = "Verify: Ai Assistant is unavailable in Westlaw Edge";

//            var errorPage = BrowserPool.CurrentBrowser.GoToUrl<ErrorPageWithoutSpinner>($"https://1.next.{this.TestExecutionContext.TestEnvironment.Name.ToLower()}.westlaw.com/Conversation/LandingPage?transitionType=Default&contextData=(sc.Default)");

//            this.TestCaseVerify.IsTrue(
//                checkAiAssistantUnavailable,
//                errorPage.IsTextPresented("Try refeshing the page. For help, call 1-800-WESTLAW (1-800-937-8529)."),
//                "Ai Assistant is available in Westlaw Edge");
//        }

//        /// <summary>
//        /// Verify common functionality: info dialogs
//        /// </summary>
//        [TestMethod]
//        [TestCategory(CurrentTestCategory)]
//        [TestCategory(TeamMatzekCategory)]
//        public void AIAssistantHomePageTest()
//        {
//            const string HowAiWorksDialogTitle = "How Ai-Assisted Research works";

//            string checkHowAiWorksDialogTitle = "Verify: Learn more dialog description is as expected (AI-Assisted Research tab on the home page)";

//            var homePage = this.GetHomePage<PrecisionHomePage>();

//            var aiAssistedResearchTab = homePage.BrowseTabPanel.SetActiveTab<AiAssistedResearchTabPanel>(PrecisionBrowseTab.AiAssistedResearch);

//            var howAiWorksDialog = aiAssistedResearchTab.LearnMoreLink.Click<HowAiAssistedResearchWorksDialog>();

//            this.TestCaseVerify.IsTrue(
//                checkHowAiWorksDialogTitle,
//                howAiWorksDialog.TitleLabel.Text.Equals(HowAiWorksDialogTitle),
//                "Learn more dialog description is NOT as expected (AI-Assisted Research tab on the home page)");
//        }

//        /// <summary>
//        /// verify common functionality: info dialogs, heading, browser tab name, links
//        /// </summary>
//        [TestMethod]
//        [TestCategory(CurrentTestCategory)]
//        [TestCategory(TeamMatzekCategory)]
//        public void AiAssistantCommonLandingPageTest()
//        {
//            const string TipsForBestResultsDialogTitle = "Tips for best results ";

//            string checkTipsForBestResultsDialogTitle = "Verify: 'Tips for best results' dialog title is as expected";

//            var coCounselHeaderDialog = this.GetHomePage<PrecisionHomePage>().Header.ClickHeaderTab<CoCounselDialog>(EdgeHeaderTabs.CoCounsel);
//            var aiAssistantPage = coCounselHeaderDialog.AiAssistedResearchLink.Click<AiAssistedResearchPage>();
//            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
//            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);

//            var tipsForBestResultsDialog = aiAssistantPage.Toolbar.TipsBestResultsButton.Click<TipsForBestResultsDialog>();

//            this.TestCaseVerify.IsTrue(
//                checkTipsForBestResultsDialogTitle,
//                tipsForBestResultsDialog.TitleLabel.Text.Equals(TipsForBestResultsDialogTitle),
//                "'Tips for best results' dialog title is NOT as expected");
//        }

//        protected override void InitializeRoutingPageSettings()
//        {
//            if (this.TestContext.Properties["AiResearch"] != null && this.TestContext.Properties["AiResearch"].Equals("Off"))
//            {
//                this.Settings.AppendValues(
//                    EnvironmentConstants.FeatureAccessControlsOff,
//                    SettingUpdateOption.Append,
//                    FeatureAccessControl.AiResearch);
//            }
//            else
//            {
//                base.InitializeRoutingPageSettings();
//            }
//        }
//    }
//}
