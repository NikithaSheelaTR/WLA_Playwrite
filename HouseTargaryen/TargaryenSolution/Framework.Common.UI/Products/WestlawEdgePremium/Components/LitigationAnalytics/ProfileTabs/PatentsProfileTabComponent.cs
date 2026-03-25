namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.WestlawEdge.Elements.Judicial;
    using OpenQA.Selenium;

    /// <summary>
    /// PatentsProfileTabComponent
    /// </summary>
    public class PatentsProfileTabComponent : BaseAnalyticsProfileTabPage
    {
        private static readonly By ContainerLocator = By.Id("co_la_patents_tab");
        private static readonly By FacetTitleLocator = By.XPath(".//*[@class='SearchFacet-buttonToggle']");
        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Patents";

        /// <summary>
        /// The facet title
        /// </summary>
        public IButton FacetTitleButton => new CustomClickButton(this.ComponentLocator, FacetTitleLocator);
    }
}