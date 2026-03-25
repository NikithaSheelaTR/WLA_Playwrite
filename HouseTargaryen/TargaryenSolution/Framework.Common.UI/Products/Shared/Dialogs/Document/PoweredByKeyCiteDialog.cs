namespace Framework.Common.UI.Products.Shared.Dialogs.Document
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Powered By KeyCite dialog
    /// </summary>
    public sealed class PoweredByKeyCiteDialog : BaseModuleRegressionDialog
    {
        private static readonly By CloseButtonLocator = By.Id("co_poweredByKeyCiteClose");

        private static readonly By XCloseButtonLocator = By.XPath("//button[@class='co_overlayBox_closeButton co_iconBtn']");

        private static readonly By DialogTitleLocator = By.XPath("//div[@class='co_overlayBox_headline']//h3 | //div[@class='co_overlayBox_headline']//h2");

        private static readonly By OverrulingRiskWarningMessageLocator = By.CssSelector("ul.co_keyciteLightboxList>li:nth-of-type(5)>p");


        /// <summary>
        /// Label for Overruling risk warning message displayed in KeyCite Canada lightbox
        /// </summary>
        public ILabel OverrulingRiskWarningInLightbox => new Label(OverrulingRiskWarningMessageLocator);

        /// <summary>
        /// Clicks the Close button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickClose<T>() where T : ICreatablePageObject => this.ClickElement<T>(CloseButtonLocator);

        /// <summary>
        /// Gets the dialog title text
        /// </summary>
        /// <returns> The title text from the dialog headline element </returns>
        public string GetDialogTitle() => DriverExtensions.WaitForElement(DialogTitleLocator).Text;

        /// <summary>
        /// Verifies that close button is displayed.
        /// </summary>
        /// <returns> True if cancel button is displayed </returns>
        public bool IsCloseButtonDisplayed() => DriverExtensions.IsDisplayed(CloseButtonLocator, 5);

        /// <summary>
        /// Is X Close Button Displayed
        /// </summary>
        /// <returns> True if X close button is displayed </returns>
        public bool IsXCloseButtonDisplayed() => DriverExtensions.IsDisplayed(XCloseButtonLocator);
    }
}
