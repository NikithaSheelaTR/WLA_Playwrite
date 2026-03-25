namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.NarrowPane.Facets
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;

    using OpenQA.Selenium;

    /// <summary>
    ///  Search within this folder
    /// </summary>
    public class EdgeSearchWithinThisFolder : BaseModuleRegressionDialog
    {
        private static readonly By Container = By.XPath("//div[@id = 'coid_SearchWithinLightbox']");
        private static readonly By SearchButtonLocator = By.XPath(".//button[@class = 'SearchFacet-button SearchFacet-buttonSubmit SearchFacet-buttonSearch--secondary']");
        private static readonly By SearchInputLocator = By.XPath(".//input[@name = 'SearchFacetSearchWithin-inputKeyword']");
        
        /// <summary>
        /// SearchInput textbox
        /// </summary>
        public ITextbox SearchTextbox => new Textbox(Container, SearchInputLocator);

        /// <summary>
        /// Search button 
        /// </summary>
        public IButton SearchButton => new Button(Container, SearchButtonLocator);

        /// <summary>
        /// Perform search
        /// </summary>
        /// <param name="query">query</param>
        /// <returns>new instance of <see cref= "EdgeResearchOrganizerPage"/></returns>
        public EdgeResearchOrganizerPage PerformSearch(string query)
        {
            this.SearchTextbox.SetText(query);
            return this.SearchButton.Click<EdgeResearchOrganizerPage>();
        }
    }
}