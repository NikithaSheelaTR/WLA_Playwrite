namespace WestlawPrecision.Tests.SeparateAthensFeature.ParallelSearch
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Core.Utils.Execution;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Configuration;
    using System;

    /// <summary>
    /// Out Of Plan Test
    /// </summary>
    [TestClass]
    public class ParallelSearchOutOfPlanTest : ParallelSearchBaseTest
    {
        private const string FeatureTestCategoryParallelSearch= "ParallelSearch";

        public ParallelSearchOutOfPlanTest()
        {
            this.Settings.Append(
               EnvironmentConstants.PasswordPoolName,
               "IndigoPremiumOOP",
               SettingUpdateOption.Overwrite);
            this.Settings.Append(
                           EnvironmentConstants.BlockCiam,
                           "false",
                           SettingUpdateOption.Overwrite);
        }

        /// <summary>
        /// Test Parallel Search page opens, Out of plan indicator validation
        /// (Stories: 2104074 Test Cases:2119237).
        /// 1. Sign in WL Precision with Parallel Search access
        /// 2. Click Parallel Search link next to global search
        /// 3. Submit search: Possession of drugs is inadmissible in a DWI prosecution under rule 403
        /// 4. wait untill results are displayed
        /// 5. Check: Verify out of plan indicator displayed
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryParallelSearch)]
        public void ParallelSearchOOPIndicatorTest()
        {
            const string SearchQuery = "What constitutes a personnel file?";
            const string ExpectedOOP = "Out of plan";

            var parallelSearchPage = this.GetHomePage<PrecisionHomePage>().Header.ParallelSearchLink.Click<ParallelSearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(ParallelSearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(ParallelSearchTab);

            parallelSearchPage.QueryBox.EnterSearchQuery(SearchQuery);
            parallelSearchPage.QueryBox.SubmitSearchQuery();
            SafeMethodExecutor.WaitUntil(() => !parallelSearchPage.ProgressRingLabel.Displayed);

            var resultCount = parallelSearchPage.Results.CasesItems.Count;
            string OOP = parallelSearchPage.Results.CasesItems[new Random().Next(0, resultCount - 1)].OutOfPlanBannerLabel.Text;

            this.TestCaseVerify.IsTrue(
               "Verify out of plan indicator displayed for result items",
               OOP.Equals(ExpectedOOP),
               "Out of plan indicator not displayed for result items");
        }
    }
}
