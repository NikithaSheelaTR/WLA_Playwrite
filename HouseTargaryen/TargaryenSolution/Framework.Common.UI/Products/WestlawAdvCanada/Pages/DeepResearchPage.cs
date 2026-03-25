namespace Framework.Common.UI.Products.WestlawAdvCanada.Pages
{
    using Framework.Common.UI.Products.WestlawAdvantage.Pages.DeepResearch;
    using Framework.Common.UI.Products.WestlawAdvCanada.Components.DeepResearch;

    /// <summary>
    /// Deep Research page
    /// </summary>
    public class DeepResearchPage : AiDeepResearchPage
    { 
        /// <summary>
        /// Deep Research Result Component
        /// </summary>
        public new DeepResearchResultComponent ResultComponent { get; } = new DeepResearchResultComponent();

    }
}