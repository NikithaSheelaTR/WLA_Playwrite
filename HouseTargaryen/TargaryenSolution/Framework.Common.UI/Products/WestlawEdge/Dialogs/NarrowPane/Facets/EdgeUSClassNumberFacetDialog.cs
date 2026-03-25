namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.NarrowPane.Facets
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// U.S. Class Number dialog
    /// </summary>
    public class EdgeUSClassNumberFacetDialog : BaseAvailableAndSelectedHierarchicalOptionsListsDialog
    {
        private static readonly By ContainerLocator = By.Id("co_facet_usClassNumber_popup");

        /// <summary>
        /// Container
        /// </summary>
        protected override IWebElement Container => DriverExtensions.WaitForElementDisplayed(ContainerLocator);
    }
}