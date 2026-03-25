namespace Framework.Common.UI.Products.WestlawAdvCanada.Components.DeepResearch
{
    using Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch;

    /// <summary>
    /// Westlaw Advantange Canada Deep Research Result Component
    /// </summary>
    public class DeepResearchResultComponent : AiDeepResearchResultComponent
    {
        /// <summary>
        /// Deep Research Left Result Component
        /// </summary>
        public DeepResearchLeftResultComponent LeftResultComponent { get; } = new DeepResearchLeftResultComponent();
    }
}