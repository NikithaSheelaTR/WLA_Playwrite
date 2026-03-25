namespace Framework.Common.UI.Products.Shared.Dialogs.HomePage
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Improve Westlaw Dialog
    /// </summary>
    public class ImproveWestlawDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator =
            By.XPath("//div[contains(@class,'co_overlayBox_container') and .//*[text()='Improve Westlaw']]");

        private static readonly By MessageLocator = By.ClassName("co_overlayBox_content");

        /// <summary>
        /// Get Text from Improve Westlaw dialog
        /// </summary>
        /// <returns> Improve Westlaw dialog message </returns>
        public string GetMessageText() => DriverExtensions.GetText(ContainerLocator, MessageLocator);
    }
}
