namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.OpportunityFinderComponents
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics.OpportunityFinder;
    using OpenQA.Selenium;

    /// <summary>
    /// Company Attributes Component
    /// </summary>
    public class CompanyAttributesComponent : BaseModuleRegressionComponent, ICreatablePageObject
    {
        /// <summary>
        /// Opportunity Finder Filters Panel
        /// </summary>
        public OpportunityFinderFiltersPanel Filters => new OpportunityFinderFiltersPanel();

        /// <summary>
        /// Opportunity Finder Filter Dialog
        /// </summary>
        public OpportunityFinderFilterDialog FilterDialog => new OpportunityFinderFilterDialog();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => throw new System.NotImplementedException();
    }
}