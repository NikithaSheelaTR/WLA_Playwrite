namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.FocusHighlighting
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Reset warning dialog
    /// </summary>
    public class ResetWarningDialog : BaseModuleRegressionDialog
    {
        private static readonly By CancelButtonLocator = By.XPath("//button[@class = 'co_overlayBox_buttonCancel']");
        private static readonly By OkButtonLocator = By.XPath("//ul[@class = 'co_focusHighlightLightboxButtonGroup']//button[@class = 'co_primaryBtn']");
        private static readonly By TitleLocator = By.XPath("//*[contains(@id, 'coid_lightboxAriaLabel_')]");
        private static readonly By MessageLocator = By.XPath("//div[@class = 'co_overlayBox_content']//div");

        /// <summary>
        /// Get dialog title
        /// </summary>
        public string Title => DriverExtensions.GetText(TitleLocator);

        /// <summary>
        /// Get message text
        /// </summary>
        public string Message => DriverExtensions.GetText(MessageLocator);

        /// <summary>
        /// Clicks 'Cancel' button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickCancelButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(CancelButtonLocator);

        /// <summary>
        /// Clicks 'Ok' button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickOkButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(OkButtonLocator);
    }
}