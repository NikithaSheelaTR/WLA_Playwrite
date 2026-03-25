namespace Framework.Common.UI.Products.TaxnetPro.Components
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.TaxnetPro.Enums;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Taxnet Pro Search Component
    /// </summary>
    public class TaxnetProYourSearchComponent : YourSearchComponent
    {
        private static readonly By ExpandArrowLocator = By.Id("co_search_advancedSearch_summaryToggle");
        private static readonly By DidYouMeanLocator = By.Id("co_advancedSearch_summaryDidYouMean");

        private static readonly string YourSearchTableValueLocator =
            ".//div[@id='co_search_advancedSearch_summaryInner']//strong[contains(text(),'{0}')]/following-sibling::span";

        private EnumPropertyMapper<YourSearchTableKeys, WebElementInfo> yourSearchTableKey;

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<YourSearchTableKeys, WebElementInfo> YourSearchTableKey =>
            this.yourSearchTableKey = this.yourSearchTableKey
                                      ?? EnumPropertyModelCache.GetMap<YourSearchTableKeys, WebElementInfo>(
                                          string.Empty,
                                          @"Resources/EnumPropertyMaps/TaxnetPro");

        /// <summary>
        /// Expand Your search component
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>The instance of Page</returns>
        public T ExpandYourSearch<T>()
            where T : ICreatablePageObject
        {
            IWebElement link = DriverExtensions.WaitForElement(ExpandArrowLocator);
            if (link.GetAttribute("class").Equals("co_dropdownArrowCollapsed"))
            {
                link.Click();
            }

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get the text from Your Search table
        /// </summary>
        /// <returns>The text from the table</returns>
        public string GetTextFromYourSearchTable(YourSearchTableKeys tableKey)
        {
            return DriverExtensions.WaitForElement(
                By.XPath(string.Format(YourSearchTableValueLocator, this.YourSearchTableKey[tableKey].Text))).GetText();
        }

        /// <summary>
        /// Checks if Did you mean text displayed in your search summary
        /// </summary>
        /// <returns>true if displayed</returns>
        public bool IsDidYouMeanTextDisplayed() => DriverExtensions.IsDisplayed(DidYouMeanLocator);
    }
}