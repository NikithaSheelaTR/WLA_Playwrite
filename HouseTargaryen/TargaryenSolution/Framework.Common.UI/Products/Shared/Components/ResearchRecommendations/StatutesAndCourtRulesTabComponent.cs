namespace Framework.Common.UI.Products.Shared.Components.ResearchRecommendations
{
    using OpenQA.Selenium;

    /// <summary>
    /// Statutes and Court Rules Tab Panel Component for RR
    /// </summary>
    public class StatutesAndCourtRulesTabComponent : BaseRrTabComponent
    {
        private static readonly By StatutesAndCourtRulesResultListLocator = By.Id("coid_rr_tabPanel1");

        private static readonly By ContainerLocator = By.Id("co_id_ContentType_statute");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Statutes & Court Rules";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// container element
        /// </summary>
        protected override By SearchResultsListLocator => StatutesAndCourtRulesResultListLocator;
    }
}
