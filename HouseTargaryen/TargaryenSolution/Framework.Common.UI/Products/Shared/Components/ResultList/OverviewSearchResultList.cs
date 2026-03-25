namespace Framework.Common.UI.Products.Shared.Components.ResultList
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Components.ResultLists;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Products.WestLawNext.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <inheritdoc />
    /// <summary>
    /// The overview search result grid. 
    /// </summary>
    public class OverviewSearchResultList : IOverviewSearchResultList
    {
        private const string VieAllRegex = @"(?<resultsHeader>\w+\D+)View all\s[\d,]+$";

        private const string SearchResultsByContentTypeLctMask = "//li[starts-with(@id, 'cobalt_search_results_{0}')]";

        private const string ViewAllLinkLctMask = "//div[@id='cobalt_search_{0}_results']/h2/a";

        private static readonly By SearchResultsElementLocator = By.XPath("//li[starts-with(@id, 'cobalt_search_')]");

        private static readonly By SectionHeaderLocator = By.XPath("//div[@id='co_search_results_inner']//h2");

        private static readonly By SearchResultHeaderLocator = By.XPath("//h2[@class='co_search_header']");

        private static readonly By MoreInfoLinkLocator = By.Id("co_moreInfoLink");

        private static readonly By InfoMessageLocator = By.XPath("//div[contains(@class,'co_searchMoreInfoTooltip')]");

        private EnumPropertyMapper<ContentType, ContentTypeInfo> contentTypeMap;

        /// <inheritdoc />
        public int TotalCount => DriverExtensions.GetElements(SearchResultsElementLocator).Count;

        /// <inheritdoc />
        public int SectionsCount => this.GetResultsHeaders().Count;

        /// <inheritdoc />
        public IList<ContentType> Sections =>
            this.GetResultsHeaders().Select(item => item.GetEnumValueByText<ContentType>(item)).ToList();

        /// <inheritdoc />
        public string MoreInfoText => this.MoreInfoMessage.Text.Trim();

        /// <summary>
        /// The more info icon.
        /// </summary>
        protected IWebElement MoreInfoIcon => DriverExtensions.WaitForElement(MoreInfoLinkLocator);

        /// <summary>
        /// Gets the more info message.
        /// </summary>
        protected IWebElement MoreInfoMessage
        {
            get
            {
                this.MoreInfoIcon.SeleniumHover();
                return DriverExtensions.WaitForElementDisplayed(InfoMessageLocator);
            }
        }

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<ContentType, ContentTypeInfo> ContentTypeMap
            => this.contentTypeMap = this.contentTypeMap ?? EnumPropertyModelCache.GetMap<ContentType, ContentTypeInfo>();

        /// <inheritdoc />
        public IList<ResultListItem> GetItems(ContentType contentType)
        {
            DriverExtensions.WaitForElement(SearchResultsElementLocator);
            return DriverExtensions
                   .GetElements(
                       By.XPath(
                           string.Format(
                               SearchResultsByContentTypeLctMask,
                               this.ContentTypeMap[contentType].SearchResultsLocatorString)))
                   .Select(el => new ResultListItem(el)).ToList();
        }

        /// <inheritdoc />
        public ResultListItem GetItem(ContentType contentType, int index) =>
            this.GetItems(contentType)[index];

        /// <inheritdoc />
        public ResultListItem GetItem(ContentType contentType, string nameOrGuid) =>
            nameOrGuid.IsWestlawGuid()
                ? this.GetItems(contentType).First(item => item.Guid.Equals(nameOrGuid))
                : this.GetItems(contentType).First(item => item.LinkText.Equals(nameOrGuid));

        /// <inheritdoc />
        public TPage ClickViewAll<TPage>(ContentType type)
            where TPage : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(
                By.XPath(
                    string.Format(
                        ViewAllLinkLctMask,
                        this.ContentTypeMap[type].SearchResultsLocatorString))).Click();
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <inheritdoc />
        public IList<string> GetResultsHeaders()
        {
            var excludeViewAllRegex = new Regex(VieAllRegex);
            return
                DriverExtensions.GetElements(SearchResultHeaderLocator)
                                .Select(e => excludeViewAllRegex.Match(e.Text).Groups["resultsHeader"].Value.Trim())
                                .ToList();
        }

        /// <inheritdoc />
        public bool HasSection(ContentType type) =>
            DriverExtensions.GetElements(SectionHeaderLocator).Any(e => e.Text.StartsWith(this.ContentTypeMap[type].Text));

        /// <inheritdoc />
        public bool HasViewAllLink(ContentType type) =>
            DriverExtensions.GetElements(SearchResultHeaderLocator)
                            .Where(e => e.Text.Contains(this.ContentTypeMap[type].Text))
                            .Select(e => e.Text.Contains("View all")).First();

        /// <inheritdoc />
        public bool IsMoreInfoIconDisplayed() => this.MoreInfoIcon.IsDisplayed();
    }
}