namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel
{
    using Framework.Common.UI.Products.WestLawNext.Components;
    using OpenQA.Selenium;

    /// <summary>
    /// Content Types Tab Component
    /// </summary>
    public class ContentTypesTabComponent : BaseTabComponent
    {
        private By ContainerLocator = By.XPath("//div[@id='coid_contentTypesContainer']");
        
        /// <summary>
        /// Content type facet
        /// </summary>
        public EdgeContentTypesFacetComponent ContentType => new EdgeContentTypesFacetComponent();

        /// <summary>
        /// Content type facet
        /// </summary>
        public SetDefaultComponent SetDefaultComponent => new SetDefaultComponent();

        /// <summary>
        /// Component Locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
        
        /// <summary>
        /// Tab Name
        /// </summary>
        protected override string TabName => "Content Types";       
    }
}
