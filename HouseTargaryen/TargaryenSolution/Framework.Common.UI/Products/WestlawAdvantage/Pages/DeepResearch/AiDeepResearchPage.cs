namespace Framework.Common.UI.Products.WestlawAdvantage.Pages.DeepResearch
{
    using Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;

    /// <summary>
    /// AI Deep Research Landing Page
    /// </summary>
    public class AiDeepResearchPage : EdgeCommonAuthenticatedWestlawNextPage
    {
        /// <summary>
        /// Deep Research Header
        /// </summary>
        public DeepResearchHeader DeepResearchHeader { get; } = new DeepResearchHeader();

        /// <summary>
        /// Welcome Component
        /// </summary>
        public AiDeepResearchWelcomeComponent WelcomeComponent { get; } = new AiDeepResearchWelcomeComponent();

        /// <summary>
        /// Deep Research Result Component
        /// </summary>
        public AiDeepResearchResultComponent ResultComponent { get; } = new AiDeepResearchResultComponent();

        /// <summary>
        /// Usage debug
        /// </summary>
        public UsageDebugComponent UsageDebug { get; } = new UsageDebugComponent();
    }
}

