namespace Framework.Common.UI.Products.WestLawNext.Dialogs
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// CopyLink Dialog
    /// </summary>
    public class CopyLinkDialog : BaseModuleRegressionDialog
    {
        private static readonly By CopyLinkLocator = By.Id("co_linkBuilderLightbox_CopyButton");

        private static readonly By CopyLinkAreaLocator = By.Id("co_LinkBuilderUrl");

        private static readonly By BottomContainerLocator = By.CssSelector("div.co_overlayBox_optionsBottom");

        private static readonly By CancelButtonLocator = By.Id("co_linkBuilderLightbox_CancelButton");

        private static readonly By CloseButtonLocator = By.Id("co_linkBuilderLightbox_CloseLink");

        private static readonly By TitleLocator = By.XPath("//div[@class='co_overlayBox_headline']//h3 | //div[@class='co_overlayBox_headline']//h2");
        
        /// <summary>
        /// Gets Title of LightBox
        /// </summary>
        /// <returns>Title</returns>
        public string Title => DriverExtensions.GetElement(TitleLocator).Text;

        /// <summary>
        /// Gets Bottom Container
        /// </summary>
        private IWebElement BottomContainer => DriverExtensions.WaitForElement(BottomContainerLocator);

        /// <summary>
        /// Is Close Button Displayed
        /// </summary>
        /// <returns>True if Close Button is displayed, otherwise - false</returns>
        public bool IsCloseButtonDisplayed() => DriverExtensions.IsDisplayed(CloseButtonLocator);

        /// <summary>
        /// Click on the 'Cancel' button
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The new instance of T page</returns>
        public T ClickCancelButton<T>() where T : ICreatablePageObject =>
            this.ClickElement<T>(this.BottomContainer, CancelButtonLocator);

        /// <summary>
        /// Click on the Close button
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The new instance of T page</returns>
        public T ClickCloseButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(CloseButtonLocator);

        /// <summary>
        /// IsCancel Button Displayed
        /// </summary>
        /// <returns>True if Cancel Button is displayed, otherwise - false</returns>
        public bool IsCancelButtonDisplayed() => DriverExtensions.IsDisplayed(CancelButtonLocator);

        /// <summary>
        /// Click Copy Link Button
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The new instance of T page</returns>
        public T ClickCopyLinkButton<T>() where T : ICreatablePageObject
        {
            this.ClickElement(CopyLinkLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Is Copy Link Button Displayed
        /// </summary>
        /// <returns>True if Copy Link Button is displayed, otherwise - false</returns>
        public bool IsCopyLinkButtonDisplayed() => DriverExtensions.IsDisplayed(CopyLinkLocator);

        /// <summary>
        /// Is Link Builder Area Displayed
        /// </summary>
        /// <returns>True if Copy Link Area is displayed, otherwise - false</returns>
        public bool IsCopyLinkAreaDisplayed() => DriverExtensions.IsDisplayed(CopyLinkAreaLocator);

        /// <summary>
        /// Get Link Builder URL
        /// </summary>
        /// <returns>Get Link Url</returns>
        public string GetCopyLinkUrl() => DriverExtensions.WaitForElement(CopyLinkAreaLocator).GetAttribute("value");

        /// <summary>
        /// Get Copy Button Text
        /// </summary>
        /// <returns>Copy button text</returns>
        public string GetCopyButtonText() => DriverExtensions.WaitForElement(CopyLinkLocator).Text;
    }
}