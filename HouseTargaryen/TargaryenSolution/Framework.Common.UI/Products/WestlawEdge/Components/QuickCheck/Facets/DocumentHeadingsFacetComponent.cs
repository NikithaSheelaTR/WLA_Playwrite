namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.Facets
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;

    using OpenQA.Selenium;

    /// <summary>
    /// Recommendations tab=>The Document Headings Facet Component
    /// </summary>
    public sealed class DocumentHeadingsFacetComponent : EdgeBaseFacetWithAppearingDialogComponent
    {
        private static readonly By RemoveLinkLocator = By.XPath(".//button[.='Remove']");

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentHeadingsFacetComponent"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public DocumentHeadingsFacetComponent(By componentLocator) : base(componentLocator)
        {
        }

        /// <summary>
        /// The remove link.
        /// </summary>
        public ILink RemoveLink => new Link(this.ComponentLocator, RemoveLinkLocator);
    }
}