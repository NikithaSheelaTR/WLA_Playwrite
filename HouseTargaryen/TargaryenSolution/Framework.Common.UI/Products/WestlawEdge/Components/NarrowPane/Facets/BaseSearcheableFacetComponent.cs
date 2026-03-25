namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Base class fot LPA and not LPA facets 
    /// </summary>
    public abstract class BaseSearcheableFacetComponent : EdgeBaseFacetComponent
    {
        private static readonly By SearchFieldLocator 
            = By.XPath(".//input[contains(@id,'SearchFacet')] | .//input[contains(@class,'SearchFacet')]");

        /// <summary>
        /// Enter text in Find text field
        /// </summary>
        /// <param name="text"></param>
        public void EnterTextInFindField(string text)
            => DriverExtensions.SetTextField(text, this.ComponentLocator, SearchFieldLocator);

        /// <summary>
        /// Is text field displayed 
        /// </summary>
        public bool IsTextFieldDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, SearchFieldLocator);     
    }
}
