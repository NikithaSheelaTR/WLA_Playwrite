namespace Framework.Common.UI.Products.GovernmentWeblinks.Pages.Search
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.GovernmentWeblinks.Models;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The search results page class
    /// </summary>
    public class SearchResultsPage : BaseGovernmentWeblinksPage
    {
        private static readonly By EditSearchLinkLocator = By.XPath("//p[@class='co_editSearch']/a");

        private static readonly By IndexLocator = By.XPath(".//*[@class='co_resultsListCount' or @class='resultLink']");

        private static readonly By NextGroupButtonLocator = By.XPath("//a[contains(text(), \"Next set of documents\")]");

        private static readonly By TitleLocator = By.XPath(".//span/following-sibling::a | .//span[not(@class='co_resultsListCount') and not(@class='co_searchTerm')]");

        private static readonly By LinkLocator = By.XPath(".//a[@class='resultLink']");

        private static readonly By DescriptionLocator = By.XPath(".//*[@class='co_resultsListDescription']");

        private static readonly By ResultsContainerLocator = By.Id("results");

        private static readonly By TotalResultsLocator = By.XPath("//h1[contains(text(), \"Results\")]");

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResultsPage"/> class. 
        /// The initialization of resultModel list is needed here, SocketException otherwise
        /// </summary>
        public SearchResultsPage()

        {
            this.ResultList = DriverExtensions
                .GetElements(ResultsContainerLocator, By.TagName("li")).Select(
                    e => new WeblinksSearchResultModel(
                        int.Parse(DriverExtensions.WaitForElement(e, IndexLocator).Text.Replace(".", string.Empty)),
                        DriverExtensions.IsDisplayed(e, TitleLocator) ? DriverExtensions.WaitForElement(e, TitleLocator).Text : string.Empty,
                        DriverExtensions.IsDisplayed(e, DescriptionLocator) ? DriverExtensions.WaitForElement(e, DescriptionLocator).Text : string.Empty,
                        DriverExtensions.WaitForElement(e, LinkLocator).GetAttribute("href"))).ToList();
        }

        /// <summary>
        /// Gets the resultModel list.
        /// </summary>
        public List<WeblinksSearchResultModel> ResultList { get; }

        /// <summary>
        /// Click on the Edit search link
        /// </summary>
        /// <typeparam name="T">The instance of page</typeparam>
        /// <returns>
        /// The ICreatablePageObject
        /// </returns>
        public T ClickEditSearch<T>()
            where T : ICreatablePageObject => this.ClickElement<T>(DriverExtensions.WaitForElement(EditSearchLinkLocator));

        /// <summary>
        /// Edit Search link
        /// </summary>
        public ILink EditSearchLink => new Link(EditSearchLinkLocator);

        /// <summary>
        /// Click on the Next Result Group
        /// </summary>
        /// <typeparam name="T">The instance of page</typeparam>
        /// <returns>
        /// The ICreatablePageObject
        /// </returns>
        public T ClickNextResultGroup<T>() 
            where T : ICreatablePageObject => this.ClickElement<T>(DriverExtensions.WaitForElement(NextGroupButtonLocator));

        /// <summary>
        /// Click on the item of resultModel list
        /// </summary>
        /// <param name="resultModel">WeblinksSearchResultModel</param>
        /// <returns>
        /// The <see cref="WeblinksDocumentPage"/>.
        /// </returns>
        public WeblinksDocumentPage ClickResult(WeblinksSearchResultModel resultModel) 
            => this.ClickElement<WeblinksDocumentPage>(DriverExtensions.GetElements(ResultsContainerLocator, LinkLocator).ElementAt(resultModel.Index - 1));

        /// <summary>
        /// The get total results.
        /// </summary>
        /// <returns>
        /// The number of results <see cref="int"/>.
        /// </returns>
        public int GetTotalResults() 
            => DriverExtensions.GetText(TotalResultsLocator).ConvertCountToInt();

        /// <summary>
        /// Clicks element
        /// </summary>
        /// <param name="element">Element name</param>
        /// <typeparam name="T">The instance of page</typeparam>
        /// <returns>
        /// The ICreatablePageObject
        /// </returns>
        private T ClickElement<T>(IWebElement element) where T : ICreatablePageObject
        {
            element.Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
