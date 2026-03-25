namespace Framework.Common.UI.Products.WestLawNext.Pages
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Order Summary page
    /// </summary>
    public class OrderSummaryPage : BaseModuleRegressionPage
    {
        private static readonly By ConfirmationTitleLocator = By.XPath("id('co_subHeader')/h1");

        private static readonly By OrderAnotherDocumentButtonLocator = By.Id("OrderAnotherDocument");

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderSummaryPage"/> class. 
        /// Construct order summary page
        /// </summary>
        public OrderSummaryPage()
        {
            DriverExtensions.WaitForElement(OrderAnotherDocumentButtonLocator);
        }

        /// <summary>
        /// Click on the Order Another document
        /// </summary>
        /// <returns>The <see cref="OrderDocumentFormPage"/>.</returns>
        public OrderDocumentFormPage ClickOrderAnotherDocument()
        {
            DriverExtensions.WaitForElement(OrderAnotherDocumentButtonLocator).Click();
            return new OrderDocumentFormPage();
        }

        /// <summary>
        /// Get Order Id
        /// </summary>
        /// <returns>Order ID string</returns>
        public string GetOrderId()
        {
            string orderConfirmationText = DriverExtensions.WaitForElement(ConfirmationTitleLocator).Text;
            string orderId = orderConfirmationText.Substring(
                orderConfirmationText.IndexOf(':') + 1,
                orderConfirmationText.Length - (orderConfirmationText.IndexOf(':') + 1));
            return orderId;
        }
    }
}