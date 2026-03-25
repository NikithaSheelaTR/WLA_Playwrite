namespace Framework.Common.UI.Products.Shared.Components.ResearchRecommendations
{
    using OpenQA.Selenium;

    /// <summary>
    /// Cases Tab Panel Component for RR
    /// </summary>
    public class CasesTabComponent : BaseRrTabComponent
    {
        private static readonly By CasesResultListLocator = By.Id("coid_rr_tabPanel0");

        private static readonly By ContainerLocator = By.Id("co_id_ContentType_case");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Cases";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// container element
        /// </summary>
        protected override By SearchResultsListLocator => CasesResultListLocator;
    }
}
