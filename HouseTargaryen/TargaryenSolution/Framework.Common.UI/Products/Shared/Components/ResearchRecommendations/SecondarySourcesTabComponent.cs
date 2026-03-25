namespace Framework.Common.UI.Products.Shared.Components.ResearchRecommendations
{
    using OpenQA.Selenium;

    /// <summary>
    /// Secondary Sources Tab Panel Component for RR
    /// </summary>
    public class SecondarySourcesTabComponent : BaseRrTabComponent
    {
        private static readonly By SecondarySourcesResultListLocator = By.Id("coid_rr_tabPanel2");

        private static readonly By ContainerLocator = By.Id("co_id_ContentType_analytical");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Secondary Sources";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// container element
        /// </summary>
        protected override By SearchResultsListLocator => SecondarySourcesResultListLocator;
    }
}
