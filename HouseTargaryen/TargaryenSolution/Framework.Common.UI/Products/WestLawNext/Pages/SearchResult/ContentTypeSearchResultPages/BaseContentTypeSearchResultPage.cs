namespace Framework.Common.UI.Products.WestLawNext.Pages.SearchResult.ContentTypeSearchResultPages
{
    using Framework.Common.UI.Products.Shared.Components.Facets.RightFacets;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Products.WestLawNext.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// The base content type search result page.
    /// </summary>
    /// <typeparam name="TItem">
    /// the type of result list item
    /// </typeparam>
    public class BaseContentTypeSearchResultPage<TItem> : BaseCategorySearchResultPage<TItem> where TItem : ResultListItem
    {
        // ReSharper disable StaticMemberInGenericType
        private const string DefaultContentTypeLctMask = "//div[@id='co_search_skipToView']//*[text()='{0}']";

        private static readonly By MoreInfohHeaderIconLocator = By.Id("co_searchHeaderInfo");

        private static readonly By ParagraphResultItemLocator = By.XPath("//*[contains(@id,'co_searchResults_leadpara')]");

        private static readonly By NoDocumentsFoundMessageLocator = By.Id("cobalt_search_no_results");

        private static readonly By SmartAnswerLocator = By.Id("co_smartAnswer");

        private static readonly By AdditionalWestSearchButtonLocator = By.XPath("//div[@id='co_flipToFermiLinkContent']//a");

        private static readonly By SetDefaultLinkLocator = By.XPath("//div[@id='co_search_skipToView']//*[contains(text(),'Set Default')]");

        /// <summary>
        /// Content Type Map
        /// </summary>
        private EnumPropertyMapper<ContentType, ContentTypeInfo> contentTypeMap;

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<ContentType, ContentTypeInfo> ContentTypeMap
            => this.contentTypeMap = this.contentTypeMap ?? EnumPropertyModelCache.GetMap<ContentType, ContentTypeInfo>();

        /// <summary>
        /// Gets the snapshot.
        /// </summary>
        public SnapshotsFacetComponent Snapshot { get; } = new SnapshotsFacetComponent();

        /// <summary>
        /// "Learn About" section on the search results page
        /// </summary>
        public LearnAboutFacetComponent LearnAbout { get; } = new LearnAboutFacetComponent();

        /// <summary>
        /// Your Search Result Component
        /// </summary>
        public YourSearchComponent YourSearchComponent { get; } = new YourSearchComponent();

        /// <summary>
        /// Verify if the More Info icon is displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsMoreInfoHeaderIconDisplayed() => DriverExtensions.IsDisplayed(MoreInfohHeaderIconLocator);

        /// <summary>
        /// IsLeadParagraphResultsDisplayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsLeadParagraphResultsDisplayed()
            => DriverExtensions.IsDisplayed(ParagraphResultItemLocator);

        /// <summary>
        /// Is 'No Documents Found' message is displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsNoFoundMessageDisplayed() => DriverExtensions.IsDisplayed(NoDocumentsFoundMessageLocator);

        /// <summary>
        /// Returns true if the blue Smart Answer box is displayed on the page.  This box is used for showing
        /// famous cases/etc. that return for the search but are not in the set jurisdiction
        /// </summary>
        /// <returns> True if the Smart Answer box is displayed </returns>
        public bool IsSmartAnswerDisplayed() => DriverExtensions.IsDisplayed(SmartAnswerLocator, 5);

        /// <summary>
        /// The click additional west search button.
        /// </summary>
        /// <returns>
        /// The <see cref="FermiSearchResultPage"/>.
        /// </returns>
        public FermiSearchResultPage ClickAdditionalWestSearchButton()
        {
            DriverExtensions.WaitForElement(AdditionalWestSearchButtonLocator).Click();
            return new FermiSearchResultPage();
        }

        /// <summary>
        /// Sets chosen content type as a default one
        /// </summary>
        /// <param name="contentType">The content type</param>
        public void SetDefaultContentType(ContentType contentType)
        {
            DriverExtensions.WaitForElement(SetDefaultLinkLocator).Click();
            DriverExtensions.WaitForElement(
                                By.XPath(
                                    string.Format(DefaultContentTypeLctMask, this.ContentTypeMap[contentType].Text)))
                            .Click();
            DriverExtensions.WaitForJavaScript();
        }
    }
}