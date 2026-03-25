namespace WestlawAdvantage.Tests.LAKnowYourJudge
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Framework.Common.UI.Products.WestlawAdvantage.Pages;
    using Framework.Common.UI.Products.WestlawEdge.Components.LegalAnalytics;
    using Framework.Common.UI.Products.WestlawEdge.Enums.LegalAnalytics;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.LegalAnalaytics;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;

    /// <summary>
    /// Know your judge (Litigation Analytics) Base Test class
    /// </summary>
    [TestClass]
    [DeploymentItem("TestData/KnowYourJudge")]
    public class KnowYourJudgeBaseTest : WlaBaseTest
    {
        protected const string FeatureTestCategory = "KnowYourJudge";
        protected const string KnowYourJudgeTab = "Know your judge";
        protected const string KnowYourJudgeCardTitle = "Know Your Judge - Federal District Courts";

        /// <summary>
        /// Open profile page for a judge
        /// </summary>
        /// <param name="judgeName">Judge name</param>
        protected AnalyticsProfilerPage OpenProfilePageForJudge(string judgeName)
        {
            const string LitigationAnalytics = "Litigation Analytics";

            var homePage = this.GetHomePage<AdvantageHomePage>();
            if (!homePage.FeaturesIncludedPanel.IsDisplayed())
                homePage = homePage.WestlawAdvantageLogoLink.Click<AdvantageHomePage>();

            return homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(LitigationAnalytics)
                           .Click<AnalyticsSearchPage>().HeaderPanel
                           .SetActiveTab<JudgeTabComponent>(LitigationAnalyticsTabs.Judges)
                           .EnterSearchQueryAndClickSearch<EdgeCommonSearchResultPage>(judgeName)
                           .ClickLinkByText<AnalyticsProfilerPage>(judgeName);
        }

        // NOTE: Set IAC (IAC-AI-KNOW-YOUR-JUDGE) and FAC (AiKnowYourJudge) values until they are turned on by default
        // In Jenkins script (for scheduled runs)
        // In Resources.LocalTestConfig.xml (to run locally):
        //<add key = "FACS_ON" value="AiKnowYourJudge" />
        //<add key = "IACS_ON" value= "IAC-AI-KNOW-YOUR-JUDGE" />
    }
}

