namespace Framework.Common.UI.Products.GovernmentWeblinks.Pages.Search
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Weblinks search page class
    /// </summary>
    public class BaseWeblinksSearchPage : BaseGovernmentWeblinksPage
    {
        private static readonly By SearchButtonLocator = By.XPath("//div[@class='co_column oneColumn']/input[@class='co_formBtnGreen']");

        private static readonly By SearchOptionsLocator = By.XPath("//ul[@class='inlineList noBorder']/li");

        /// <summary>
        /// Search button
        /// </summary>
        public IButton SearchButton => new Button(SearchButtonLocator);
        
        /// <summary>
        /// Gets search options
        /// </summary>
        /// <returns>search options</returns>
        public List<string> GetSearchOptions() =>
            DriverExtensions.GetElements(SearchOptionsLocator).Select(x => x.Text).ToList();

        /// <summary>
        /// Clicks on the search button
        /// </summary>
        /// <typeparam name="T">The type of the page</typeparam>
        /// <returns>The instance of the page</returns>
        protected T ClickSearchButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.Click(SearchButtonLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Gets text from the search textbox
        /// </summary>
        /// <param name="locator">The locator</param>
        /// <returns>The <see cref="string"/></returns>
        protected string GetTextFromTextarea(By locator) 
            => DriverExtensions.WaitForElement(locator).GetAttribute("value");
    }
}
