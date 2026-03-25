namespace Framework.Common.UI.Products.GovernmentWeblinks.Pages.Search
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.GovernmentWeblinks.Interfaces;
    using Framework.Common.UI.Raw.WestlawEdge.Utils.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Custom;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The page for search by docket number or counsel
    /// </summary>
    public class DocketCounselSearchPage : BaseWeblinksSearchPage, IWeblinksSearchPage
    {
        private const string ContentTypesLctMask = "//div[.//label[contains(.,'{0}')]]/input";

        private static readonly By SearchTermsTextboxLocator = By.XPath("//input[@id='querytext']");

        private static readonly By RadioButtonLocator = By.XPath("//div[@class='co_column oneColumn']//input[contains(@type, 'radio')]");

        /// <summary>
        /// Gets list of queries
        /// </summary>
        /// <returns>The list of string. Query</returns>
        public List<string> GetQuery() => new List<string>
                                              {
            DriverExtensions.WaitForElement(
                DriverExtensions.GetElements(RadioButtonLocator).First(e => DriverExtensions.IsRadioButtonSelected(e.GetCssLocator())), By.XPath("./parent::div/label")).Text,
            this.GetTextFromTextarea(SearchTermsTextboxLocator)
        };

        /// <summary>
        /// Search any results
        /// </summary>
        /// <typeparam name="T">The type of a page</typeparam>
        /// <param name="query">The query for search</param>
        /// <returns>The instance of a page</returns>
        public T Search<T>(List<string> query) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(ContentTypesLctMask, query[0]))).Click();
            DriverExtensions.SetTextField(query[1], SearchTermsTextboxLocator);
            return this.ClickSearchButton<T>();
        }
    }
}
