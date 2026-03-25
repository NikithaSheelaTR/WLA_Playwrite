namespace Framework.Common.UI.Products.TaxnetPro.Components.Toolbar.Footer
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Toolbar.FooterToolbar;
    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Pagination Footer component
    /// </summary>
    public class TaxnetProPaginationFooterComponent : PaginationFooterComponent
    {
        private const string FooterItemLctMask = "//img[@alt = '{0}']";
        private static readonly By FooterComponentLocator = By.XPath("//ul[contains(@class,'co_navFooter')]");

        /// <summary>
        /// Click the page button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="option"> The option. </param>
        /// <returns> A new instance of the search result page </returns>
        public override T ClickPageButton<T>(FooterPaginationOption option)
        {
            DriverExtensions.GetElement(
                DriverExtensions.WaitForElement(FooterComponentLocator),
                By.XPath(string.Format(FooterItemLctMask, this.PaginationMap[option].Text))).JavascriptClick();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click the page button
        /// </summary>
        /// <param name="option"> The option. </param>
        /// <returns> A new instance of the search result page </returns>
        public override bool IsFooterOptionDisplayed(FooterPaginationOption option)
            =>
                DriverExtensions.IsDisplayed(
                    By.XPath(string.Format(FooterItemLctMask, this.PaginationMap[option].Text)));

        /// <summary>
        /// Click the page button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="option"> The option. </param>
        /// <returns> A new instance of the search result page </returns>
        public T ClickPageButtonIfDisplayed<T>(FooterPaginationOption option) where T : ICreatablePageObject
        {
            if (this.IsFooterOptionDisplayed(option))
            {
                return this.ClickPageButton<T>(option);
            }

            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}