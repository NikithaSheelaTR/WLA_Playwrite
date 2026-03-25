namespace Framework.Common.UI.Products.WestLawNextCanada.Dialogs.NarrowPane.Facets
{
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.NarrowPane.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Edge Key Number Facet Dialog
    /// </summary>
    public class CanadaEdgeJurisdictionTitleFacetDialog : BaseAvailableAndSelectedHierarchicalOptionsListsDialog
    {
        private static readonly By ContainerLocator = By.Id("co_facet_co_keynumber_popup");

        /// <summary>
        /// Container
        /// </summary>
        protected override IWebElement Container => DriverExtensions.WaitForElementDisplayed(ContainerLocator);
    }
}
