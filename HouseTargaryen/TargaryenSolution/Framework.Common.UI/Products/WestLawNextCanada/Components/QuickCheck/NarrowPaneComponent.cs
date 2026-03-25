namespace Framework.Common.UI.Products.WestLawNextCanada.Components.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck;
    using OpenQA.Selenium;

    /// <summary>
    /// Recommendations narrow pane component for Westlaw Next Canada.
    /// </summary>
    public class NarrowPaneComponent : RecommendationsNarrowPaneComponent
    {
        private static readonly By DocumentHeadingsFacetButtonLocator = By.Id("SearchFacet-header-button-id-DocumentHeadings");
        private static readonly By DocumentItemsSelectedLabelLocator = By.XPath("//div[@id='SearchFacet-body-id-DocumentHeadings']//span");

        /// <summary>
        /// Document Headings facet button
        /// </summary>
        public IButton DocumentHeadingsFacetButton => new Button(DocumentHeadingsFacetButtonLocator);

        /// <summary>
        /// Label indicating the number of selected document items in the Document Headings facet.
        /// </summary>
        public ILabel DocumentItemsSelectedLabel => new Label(DocumentItemsSelectedLabelLocator);
    }
}