namespace Framework.Common.UI.Products.WestLawNext.Dialogs.Header
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;
    using UI.Utils.QualityLibraryFacade.Library.Utils;

    /// <summary>
    /// ContentTypeDialog
    /// </summary>
    public class ContentTypeDialog : BaseModuleRegressionDialog
    {
        private const string CustomTypeLctMask = "//li[@id='co_searchWidgetCustomTab']//a[contains(text(),{0})]";

        private const string OptionLctMask = "//div[@id='co_searchCategoryDropdown']//a[contains(text(),{0})]";

        private static readonly By AllContentTypeLocator = By.CssSelector("#co_searchWidgetAllWestlawTab a");

        private static readonly By OpenCloseDialogLocator = By.Id("co_categorySearchButton");

        private static readonly By ActiveContentTypeLocator = By.CssSelector(".co_tabActive a");

        private static readonly By MoreInfoIconLocator = By.Id("co_selectedItemsLink");

        /// <summary>
        /// ClickAllContentType
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The new instance of T page</returns>
        public T ClickAllContentType<T>() where T : ICreatablePageObject => this.ClickElement<T>(AllContentTypeLocator);

        /// <summary>
        /// The set secondary sources category.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="contentType">The category Name.</param>
        /// <returns>The new instance of T page</returns>
        public T ClickCustomContentType<T>(string contentType) where T : ICreatablePageObject
            => this.ClickElement<T>(SafeXpath.BySafeXpath(CustomTypeLctMask, contentType));

        /// <summary>
        /// Check if option is displayed
        /// </summary>
        /// <param name="option">Option</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsOptionDisplayed(string option) => DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(OptionLctMask, option));

        /// <summary>
        /// CloseContentTypeDialog
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The new instance of T page</returns>
        public T CloseContentTypeDialog<T>() where T : ICreatablePageObject => this.ClickElement<T>(OpenCloseDialogLocator);

        /// <summary>
        /// GetActiveContentType
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetActiveContentType() => DriverExtensions.GetText(ActiveContentTypeLocator);

        /// <summary>
        /// Verify that tab contains tooltip (more info icon)
        /// </summary>
        /// <returns> True if contains, false otherwise </returns>
        public bool IsMoreInfoIconDisplayed() => DriverExtensions.IsDisplayed(MoreInfoIconLocator);
    }
}