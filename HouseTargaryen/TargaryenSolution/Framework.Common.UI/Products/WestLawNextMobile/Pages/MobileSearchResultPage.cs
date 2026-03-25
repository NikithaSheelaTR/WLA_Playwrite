namespace Framework.Common.UI.Products.WestLawNextMobile.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.WestLawNext.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNextMobile.Components;
    using Framework.Common.UI.Products.WestLawNextMobile.Dropdowns;
    using Framework.Common.UI.Products.WestLawNextMobile.Models;
    using Framework.Common.UI.Products.WestLawNextMobile.Pages.RelatedInformation;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Search Results Page
    /// </summary>
    public class MobileSearchResultPage : MobileBasePageWithHeader
    {
        private const string DocumentFlagLctMask = "//div[@data-guid='{0}']//a[text()='{1}']/preceding-sibling::a";

        private const string DocumentLinkLctMask = "//a[contains(@href,'{0}') and contains(@id, '_title')]";

        private static readonly By BackToCategoryPageLinkLocator = By.XPath("//a[text()='Back to Category Page']");

        private static readonly By YellowKeyCiteFlagLocator = By.XPath("//img[@alt='KeyCite Yellow Flag - Negative Treatment']");

        private static readonly By NextPageLinkLocator = By.LinkText("Next >");

        private static readonly By PageTitleLocator = By.Id("coid_website_totalDocsLabel");

        private static readonly By PreviousPageLinkLocator = By.LinkText("< Previous");

        private static readonly By SmartSearchLinkLocator = By.XPath("//ul[@class='searchSmart']//a[@class='draggable_document_link']");

        private static readonly By SearchWithinButtonLocator = By.Id("coid_website_searchWithinButton");

        private static readonly By TitleLinkLocator = By.XPath(".//a[contains(@id, '_title')]");

        private static readonly By PageNumberLocator = By.XPath("//span[@class='current']");

        private static readonly By SearchResultElementLocator = By.XPath("//div[@class='co_searchContent']");

        /// <summary>
        /// Bottom Tool component
        /// </summary>
        public BottomToolsComponent BottomTool { get; } = new BottomToolsComponent();

        /// <summary>
        /// Skip To dropdown
        /// </summary>
        public SkipToDropdown SkipTo { get; } = new SkipToDropdown();

        /// <summary>
        /// Click the 'Back To Category Page' Link
        /// </summary>
        /// <typeparam name="T"> Return type. </typeparam>
        /// <returns> New instance of the page. </returns>
        public T ClickBackToCategoryPageLink<T>() where T : MobileBasePage
        {
            DriverExtensions.WaitForElementDisplayed(BackToCategoryPageLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click First Result Yellow Flag
        /// </summary>
        /// <returns> The <see cref="NegativeTreatmentPage"/>. </returns>
        public NegativeTreatmentPage ClickFirstResultYellowFlag() => this.ClickElement<NegativeTreatmentPage>(YellowKeyCiteFlagLocator);

        /// <summary>
        /// Clicks the key cite link for a given result
        /// </summary>
        /// <param name="searchResult"> Search result to click on the flag </param>
        /// <returns> The <see cref="NegativeTreatmentPage"/>. </returns>
        public NegativeTreatmentPage ClickKeyCiteForResult(SearchResultModel searchResult)
            => this.ClickElement<NegativeTreatmentPage>(By.XPath(string.Format(DocumentFlagLctMask, searchResult.Guid, searchResult.Title)));

        /// <summary>
        /// Click by Next Page link
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickNextPageLink<T>() where T : ICreatablePageObject => this.ClickElement<T>(NextPageLinkLocator);

        /// <summary>
        /// Clicks a result
        /// </summary>
        /// <param name="result"> The result. </param>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickResult<T>(SearchResultModel result) where T : ICreatablePageObject
            => this.ClickElement<T>(By.XPath(string.Format(DocumentLinkLctMask, result.Guid)));

        /// <summary>
        /// Clicks the search smart link if present
        /// </summary>
        /// <returns>A new instance of the Document Page</returns>
        public MobileDocumentPage ClickSearchSmartLink() => this.ClickElement<MobileDocumentPage>(SmartSearchLinkLocator);

        /// <summary>
        /// Determines if we have search smart present on the results page
        /// </summary>
        /// <returns>True if it is, false otherwise</returns>
        public bool IsSmartSearchResultDisplayed() => DriverExtensions.IsDisplayed(SmartSearchLinkLocator, 5);

        /// <summary>
        /// Gets our current page number
        /// </summary>
        /// <returns>Page number</returns>
        public int GetPageNumber() => DriverExtensions.WaitForElement(PageNumberLocator).Text.ConvertCountToInt();

        /// <summary>
        /// Checks if the search result page is present using the page title.
        /// </summary>
        /// <returns>If the page is present.</returns>
        public bool IsPageDisplayed() => DriverExtensions.IsDisplayed(PageTitleLocator, 5);

        /// <summary>
        /// Get page title
        /// </summary>
        /// <returns> Page title </returns>
        public string GetPageTitle() => DriverExtensions.WaitForElement(PageTitleLocator).GetText();

        /// <summary>
        /// Verify that Previous Page link is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsPreviousPageLinkDisplayed() => DriverExtensions.IsDisplayed(PreviousPageLinkLocator, 5);

        /// <summary>
        /// Verify that Search Within Button is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsSearchWithinButtonDisplayed() => DriverExtensions.IsDisplayed(SearchWithinButtonLocator, 5);

        /// <summary>
        /// Determines if suggested link is displayed on page
        /// </summary>
        /// <param name="suggestedLinkText"> The suggested Link Text. </param>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsSuggestedLinkDisplayed(string suggestedLinkText)
            => DriverExtensions.IsDisplayed(By.LinkText(suggestedLinkText), 5);

        /// <summary>
        /// Gets all the results
        /// </summary>
        /// <returns>List of all the results</returns>
        public List<SearchResultModel> GetResults()
        {
            var searchResultList = new List<SearchResultModel>();
            var contentTypeIdRegex = new Regex(@"cobalt_result_(?<id>[a-zA-Z]+)_title");
            if (DriverExtensions.IsDisplayed(SearchResultElementLocator, 5))
            {
                IReadOnlyCollection<IWebElement> elements = DriverExtensions.GetElements(SearchResultElementLocator);
                searchResultList =
                    elements.Select(
                                e =>
                                    new SearchResultModel
                                    {
                                        Title = DriverExtensions.WaitForElement(e, TitleLinkLocator).Text,
                                        Guid = this.GetGuidByUrl(e),
                                        ContentType =
                                                contentTypeIdRegex.Match(DriverExtensions.WaitForElement(e, TitleLinkLocator).GetAttribute("id"))
                                                .Groups["id"].Value.GetEnumValueByPropertyModel<ContentType, ContentTypeInfo>(info => info.NarrowPaneLinkLocatorString)
                                    })
                            .ToList();
            }

            return searchResultList;
        }

        private string GetGuidByUrl(IWebElement elem)
        {
            string str = DriverExtensions
                .WaitForElement(elem, TitleLinkLocator).GetAttribute("href");
            return str.Substring(str.IndexOf("/d/", StringComparison.Ordinal) + 3, 33);
        }
    }
}