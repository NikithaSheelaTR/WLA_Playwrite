namespace Framework.Common.UI.Products.WestlawAdvantage.Dialogs.DeepResearch
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Delivery progress dialog
    /// </summary>
    public class DeliveryProgressDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//saf-dialog-v3[@data-testid='delivery-dialog']");

        /// <summary>
        /// Is dialog displayed
        /// </summary>
        public bool IsDisplayed() => DriverExtensions.GetElement(ContainerLocator).Displayed;
    }
}
