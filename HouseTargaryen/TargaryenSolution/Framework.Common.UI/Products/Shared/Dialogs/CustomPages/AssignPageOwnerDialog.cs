namespace Framework.Common.UI.Products.Shared.Dialogs.CustomPages
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Assign Page Owner dialog
    /// </summary>
    public class AssignPageOwnerDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.ClassName("co_overlayBox_container");
        private static readonly By CloseButtonLocator = By.XPath(".//button[@class = 'co_overlayBox_closeButton co_iconBtn']");
        private static readonly By MassageTestLocator = By.XPath(".//div[@class = 'co_overlayBox_content']/p");

        private IWebElement Container => DriverExtensions.WaitForElement(ContainerLocator);

        /// <summary>
        /// Get Message Text
        /// </summary>
        /// <returns></returns>
        public string GetMessage() => DriverExtensions.GetElement(this.Container, MassageTestLocator).Text;

        /// <summary>
        /// Close Dialog
        /// </summary>
        public void CloseDialog() => this.ClickElement(this.Container, CloseButtonLocator);
    }
}
