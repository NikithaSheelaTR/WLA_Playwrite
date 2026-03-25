namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.NarrowPane.Facets
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    ///  Describe  dialog which appears after clicking on the 'Select' button  on the International class Facet
    /// </summary>
    public class EdgeInternationalClassDialog : BaseAvailableAndSelectedHierarchicalOptionsListsDialog
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id='co_facet_ipClassInternational_popup']");

        /// <summary>
        /// Container
        /// </summary>
        protected override IWebElement Container => DriverExtensions.WaitForElementDisplayed(ContainerLocator);
    }
}