namespace Framework.Common.UI.Products.WestLawNext.Dialogs.Docket
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Dockets Requests Warning Dialog
    /// </summary>
    public class DocketsRequestsWarningDialog : BaseModuleRegressionDialog
    {
        private static readonly By CloseButtonLocator = By.Id("co_limitExceededOkButton");

        private static readonly By XCloseButtonLocator = By.XPath("//button[@class='co_overlayBox_closeButton co_iconBtn']");

        private static readonly By WarningDialogTitleLocator = By.XPath("//h3[@id='coid_lightboxAriaLabel_7']");

        private static readonly By WarningDialogTextLocator = By.ClassName("co_limitExceededContent");

        /// <summary>
        /// Is Close Button Displayed
        /// </summary>
        /// <returns>Title</returns>
        public bool IsCloseButtonDisplayed() => DriverExtensions.IsDisplayed(CloseButtonLocator);

        /// <summary>
        /// Is X Close Button Displayed
        /// </summary>
        /// <returns>Title</returns>
        public bool IsXCloseButtonDisplayed() => DriverExtensions.IsDisplayed(XCloseButtonLocator);

        /// <summary>
        /// Click Close Button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns>A new instance of this page</returns>
        public T ClickCloseButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(CloseButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get Warning Dialog Title
        /// </summary>
        /// <returns>Title</returns>
        public string GetWarningDialogTitle() => DriverExtensions.GetElement(WarningDialogTitleLocator).Text;

        /// <summary>
        /// Get Warning Dialog Text
        /// </summary>
        /// <returns>Title</returns>
        public string GetWarningDialogText() => DriverExtensions.GetElement(WarningDialogTextLocator).Text;
    }
}