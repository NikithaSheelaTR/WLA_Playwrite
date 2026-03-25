namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.NarrowPane.Facets
{
    using Framework.Common.UI.Products.Shared.Dialogs.Facets.NarrowComponent;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Indigo Key Nature Of Suit Dialog
    /// </summary>
    public class EdgeKeyNatureOfSuitFacetDialog : BaseAvailableAndSelectedOptionsListsDialog
    {
        private static readonly By ContainerLocator = By.Id("co_facet_KeyNatureOfSuit_popup");

        /// <summary>
        /// Container
        /// </summary>
        protected override IWebElement Container => DriverExtensions.WaitForElementDisplayed(ContainerLocator);
    }
}