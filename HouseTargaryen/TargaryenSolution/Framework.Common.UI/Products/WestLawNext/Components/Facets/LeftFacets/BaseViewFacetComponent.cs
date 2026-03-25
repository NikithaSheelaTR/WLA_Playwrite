namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.WestLawNext.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// BaseViewFacetComponent
    /// </summary>
    public abstract class BaseViewFacetComponent : BaseModuleRegressionComponent
    {
        private const string ContentTypeCountLctMask = "//div[@id='co_contentTypeLinksBox']//li[.//*[contains(text(),'{0}')]]/span";

        private const string ContentTypeLctMask = "//div[@id='co_contentTypeLinksBox']//li[.//*[contains(text(),'{0}')]]//*[self::a or self::strong]";

        private static readonly By ActiveContentTypeLocator = By.XPath("//*[@class='co_leftColumn_activePage'] | //*[@class='co_leftColumn_activePage']/div");

        private static readonly By ActiveContentTypeTextLocator = By.TagName("strong");

        private static readonly By ActiveFacetTextLocator = By.XPath("//li[@class='co_leftColumn_activePage']//strong");

        private static readonly By ZeroCountNavLocator = By.CssSelector("#co_contentTypeLinksBox > ul > li.co_search_contentNav_zeroCount");

        private static readonly By ListItemLocator = By.CssSelector("#co_contentTypeLinksBox>ul>li");

        private static readonly By TagSelector = By.CssSelector("div, a, button");

        private static readonly By ContentTypeLocator = new ByChained(ListItemLocator, TagSelector);

        private EnumPropertyMapper<ContentType, ContentTypeInfo> contentTypeMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewFacetComponent"/> class. 
        /// </summary>
        public BaseViewFacetComponent()
        {
            // Spinner can spin too long for the View facet, in the case of huge amount of results
            DriverExtensions.WaitForJavaScript(180000);
        }

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<ContentType, ContentTypeInfo> ContentTypeMap
            => this.contentTypeMap = this.contentTypeMap ?? EnumPropertyModelCache.GetMap<ContentType, ContentTypeInfo>();

        /// <summary>
        /// Click Content Type Link by Content Type - View Pane
        /// </summary>
        /// <typeparam name="T"> Page Type </typeparam>
        /// <param name="contentType"> Content type </param>
        /// <returns> New instance of the page </returns>
        public T ClickContentTypeLink<T>(ContentType contentType) where T : ICreatablePageObject
            => this.ClickContentTypeLink<T>(this.ContentTypeMap[contentType].Text);

        /// <summary>
        /// Click Content Type Link by link text - View Pane
        /// </summary>
        /// <param name="contentType"> The content Type. </param>
        /// <typeparam name="T"> Page Type  </typeparam>
        /// <returns> New instance of the page  </returns>
        public T ClickContentTypeLink<T>(string contentType) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(ContentTypeLctMask, contentType))).CustomClick();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get the list of Content Types
        /// </summary>
        /// <returns>List of content types</returns>
        public List<string> GetContentTypesList()
            => DriverExtensions.GetElements(ContentTypeLocator).Select(e => e.Text).ToList();

        /// <summary>
        /// Get the list of Content Types results
        /// </summary>
        /// <returns>List of content types</returns>
        public List<int> GetZeroContentTypesList()
            => DriverExtensions.GetElements(ZeroCountNavLocator).Select(e => e.Text.ConvertCountToInt()).ToList();

        /// <summary>
        /// Determines if a specific content type link is present on the left navigation
        /// </summary>
        /// <param name="contentType">Content to look for</param>
        /// <returns>True if the link is displayed, false otherwise</returns>
        public bool IsContentTypeDisplayed(ContentType contentType)
            => this.IsContentTypeDisplayed(this.ContentTypeMap[contentType].Text);

        /// <summary>
        /// Determines if a specific content type link is present on the left navigation
        /// </summary>
        /// <param name="contentType">Content to look for</param>
        /// <returns>True if the link is displayed, false otherwise</returns>
        public bool IsContentTypeDisplayed(string contentType)
            => DriverExtensions.IsDisplayed(By.XPath(string.Format(ContentTypeLctMask, contentType)), 15);

        /// <summary>
        /// Determine if a facet element is selected by the given facet name.
        /// </summary>
        /// <param name="facetName"> Name displayed for the facet </param>
        /// <returns> True if the facet is selected, false otherwise </returns> 
        public bool IsContentTypeSelected(string facetName)
            => DriverExtensions.WaitForElement(ActiveContentTypeLocator).GetText().Contains(facetName);

        /// <summary>
        /// This method checks whether the Cases facet is disabled and displays a zero count.
        /// </summary>
        /// <param name="contentType">Content to look for</param>
        /// <returns>True if link disabled and count is zero</returns>
        public bool IsContentTypeDisabled(ContentType contentType)
            => DriverExtensions.GetElements(ZeroCountNavLocator).Any(facet => facet.Text.Contains(this.ContentTypeMap[contentType].Text));

        /// <summary>
        /// Get the number of results for a given content type
        /// </summary>
        /// <param name="contentType"> Content type </param>
        /// <returns> Number of results </returns>
        public int GetContentTypeCount(string contentType)
            => DriverExtensions.WaitForElement(By.XPath(string.Format(ContentTypeCountLctMask, contentType))).Text.ConvertCountToInt();

        /// <summary>
        /// Get the number of results for a given content type
        /// </summary>
        /// <param name="contentType"> Content type </param>
        /// <returns> Number of results </returns>
        public int GetContentTypeCount(ContentType contentType)
            => this.GetContentTypeCount(this.ContentTypeMap[contentType].Text);

        /// <summary>
        /// Gets a dictionary of counts for all content types except All Results Content Type
        /// </summary>
        /// <returns>Dictionary of content types -> counts</returns>
        public Dictionary<ContentType, int> GetContentTypesWithCounts()
        {
            var counts = new Dictionary<ContentType, int>();
            List<string> contentTypes = this.GetContentTypesList();

            // get the count for our currently selected content type if present
            if (DriverExtensions.IsDisplayed(By.ClassName("co_leftColumn_activePage")))
            {
                contentTypes.Add(DriverExtensions.GetText(ActiveFacetTextLocator));
            }

            // There are different content types: All Results - for the related info pages, All results - for the search results pages
            contentTypes.RemoveAll(c => c.Equals("All results"));
            contentTypes.RemoveAll(c => c.Equals("All Results"));

            contentTypes.ForEach(
                type =>
                    {
                        ContentType contentType = type.GetEnumValueByPropertyModel<ContentType, ContentTypeInfo>(info => info.Text);
                        counts.Add(contentType, this.GetContentTypeCount(contentType));
                    });

            return counts;
        }
    }
}