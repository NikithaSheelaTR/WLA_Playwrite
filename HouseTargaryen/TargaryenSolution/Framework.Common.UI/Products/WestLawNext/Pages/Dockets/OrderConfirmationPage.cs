namespace Framework.Common.UI.Products.WestLawNext.Pages.Dockets
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Docket Order Confirmation Page
    /// </summary>
    public class OrderConfirmationPage : BaseModuleRegressionPage
    {
        private static readonly By OrderAnotherDocumentButtonLocator = By.Id("OrderAnotherDocument");

        /// <summary>
        /// Click Order Another Document
        /// </summary>
        /// <returns> Order Document Form Page </returns>
        public OrderDocumentFormPage ClickOrderAnotherDocument()
        {
            DriverExtensions.GetElement(OrderAnotherDocumentButtonLocator).Click();
            return new OrderDocumentFormPage();
        }
    }
}
