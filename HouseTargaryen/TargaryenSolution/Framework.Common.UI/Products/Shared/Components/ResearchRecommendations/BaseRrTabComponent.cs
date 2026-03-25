namespace Framework.Common.UI.Products.Shared.Components.ResearchRecommendations
{
    using Framework.Common.UI.Products.Shared.Components.SelectAll;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Base Tab Panel component for Research Recommendations
    /// </summary>
    public abstract class BaseRrTabComponent : BaseTabComponent
    {
        private static readonly By ResearchRecommendationsToolbarLocator = By.XPath(".//div[@class='co_navTools']");

        private static readonly By SelectAllComponentLocator = By.XPath(".//ul[@class='co_navOptions']");

        /// <summary>
        /// Gets the select all component.
        /// </summary>
        public SelectAllComponent SelectAllComponent => new SelectAllComponent(new ByChained(this.SearchResultsListLocator, SelectAllComponentLocator));

        /// <summary>
        /// Gets the result list.
        /// </summary>
        public ResearchRecommendationsResultList<ResearchRecommendationsResultListItem> ResultList => 
            new ResearchRecommendationsResultList<ResearchRecommendationsResultListItem>(DriverExtensions.WaitForElement(this.SearchResultsListLocator));

        /// <summary>
        /// Toolbar element
        /// </summary>
        public RrToolbarComponent Toolbar => new RrToolbarComponent(new ByChained(this.SearchResultsListLocator, ResearchRecommendationsToolbarLocator));

        /// <summary>
        /// Gets the search results list locator.
        /// </summary>
        protected abstract By SearchResultsListLocator { get; }
    }
}
