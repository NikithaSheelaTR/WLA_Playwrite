namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;
    using Framework.Common.UI.Products.WestLawNext.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Indigo Content Types Facet Component
    /// </summary>
    public class EdgeContentTypesFacetComponent : BaseModuleRegressionComponent
    {
        private const string ContentTypeLctMask = "//div[@id='co_contentTypeLinksBox']/ul/li[.//*[text()='{0}']]/span";

        private const string SubContentTypeLctMask = "//div[@id='co_contentTypeLinksBox']//*[@class='co_indent']/a[text()='{0}']";

        private const string ContentTypeCountLctMask = "//div[@id='co_contentTypeLinksBox']//li[.//*[contains(text(),'{0}')]]/span";

        private const string DefaultContentTypeLctMask = "//div[@id='co_contentTypeDefaultTypes']//li/label[text()='{0}']";

        private const string FacetItemLctLinkMask = "//div[@id='co_contentTypeLinksBox']//li[.//*[contains(text(),'{0}')]]//a | //div[@id='co_contentTypeLinksBox']//li[./*[contains(text(),'{0}')]]/button";

        private const string FacetItemLctMask = "//div[@id='co_contentTypeLinksBox']//li[.//*[contains(text(),'{0}')]]";

        private static readonly By ActiveFacetItemLocator = By.ClassName("co_leftColumn_activePage");

        private static readonly By CategoryPagesLocator = By.XPath("//div[@id='co_contentTypeLinksBox'] /ul/li");

        private static readonly By DocumentTypeFacetLinksLocator = By.XPath("//div[@id='co_contentTypeLinksBox']/ul//li//a | //div[@id='co_contentTypeLinksBox']/ul//li//div");

        private static readonly By HeaderLocator = By.Id("tab-content-types");

        private static readonly By SaveButtonLocator = By.ClassName("co_multifacet_apply");

        private static readonly By ShowMoreLinkLocator = By.XPath("//div[@id='co_contentTypeLinksBox'] /*[contains(text(),'Show more')]");

        private static readonly By SetDefaultLinkLocator = By.Id("coid_defaultTypesLink");

        private static readonly By ListItemLocator = By.CssSelector("#co_contentTypeLinksBox>ul>li");

        private static readonly By TagSelector = By.CssSelector("a, strong, button");

        private static readonly By ContainerLocator = By.Id("coid_contentTypesContainer");

        private static readonly By AllContentTypeLocator = By.XPath(".//span[@class='co_accessibilityLabel']/parent::*/preceding-sibling::*");

        private static readonly By ActiveContentTypeLocator = new ByChained(ListItemLocator, TagSelector);

        private EnumPropertyMapper<ContentType, ContentTypeInfo> contentTypeMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map
        /// </summary>
        protected EnumPropertyMapper<ContentType, ContentTypeInfo> ContentTypeMap
            => this.contentTypeMap = this.contentTypeMap ?? EnumPropertyModelCache.GetMap<ContentType, ContentTypeInfo>();

        /// <summary>
        /// Search Within Facet
        /// </summary>
        public EdgeSearchWithinFacetComponent SearchWithinFacet => new EdgeSearchWithinFacetComponent();

        /// <summary>
        /// Check is set as default link displayed
        /// </summary>
        /// <returns> True if set as default link displayed</returns> 
        public ILink SetDefaultLink => new Link(SetDefaultLinkLocator);

        /// <summary>
        /// Clicks one of the browse category on the left panel
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="category">The category.</param>
        /// <returns>New instance of T page.</returns>
        public T ClickBrowseCategory<T>(ContentType category)
            where T : ICreatablePageObject =>
            this.ClickLinkByText<T>(this.ContentTypeMap[category].Text);

        /// <summary>
        /// Click Content Type Link by Content Type - View Pane
        /// </summary>
        /// <typeparam name="T"> Page Type </typeparam>
        /// <param name="contentType"> Content type </param>
        /// <returns> New instance of the page </returns>
        public T ClickContentTypeLink<T>(ContentType contentType)
            where T : ICreatablePageObject => this.ClickContentTypeLink<T>(this.ContentTypeMap[contentType].Text);

        /// <summary>
        /// Click Content Type Link by link text - View Pane
        /// </summary>
        /// <param name="contentType"> The content Type. </param>
        /// <typeparam name="T"> Page Type  </typeparam>
        /// <returns> New instance of the page  </returns>
        public T ClickContentTypeLink<T>(string contentType)
            where T : ICreatablePageObject
        {
            this.ExpandContentTypesPane();
            By facetLinkLocator = By.XPath(string.Format(FacetItemLctLinkMask, contentType));
            By facetItemLocator = By.XPath(string.Format(FacetItemLctMask, contentType));
            DriverExtensions.JavascriptClick(
                DriverExtensions.IsDisplayed(facetLinkLocator) ? facetLinkLocator : facetItemLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks More content type button
        /// </summary>
        /// <typeparam name="T">T </typeparam>
        /// <returns> New instance of T page </returns>
        public T ClickMoreContentTypeButton<T>()
            where T : ICreatablePageObject
        {
            this.ExpandContentTypesPane();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Gets a dictionary of counts for all content types
        /// </summary>
        /// <returns>Dictionary of content types -> counts</returns>
        public Dictionary<ContentType, int> GetContentTypeFacetCountFromViewResultFacet()
        {
            this.ExpandContentTypesPane();
            var counts = new Dictionary<ContentType, int>();
            List<string> contentTypes = DriverExtensions.GetElements(ActiveContentTypeLocator).Select(e => e.Text).ToList();

            contentTypes.RemoveAll(c => c.Equals("All results"));
            contentTypes.RemoveAll(c => c.Equals("All Results"));
            contentTypes.Remove("Overview");

            foreach (string contentType in contentTypes)
            {
                ContentType type =
                    contentType.GetEnumValueByPropertyModel<ContentType, ContentTypeInfo>(info => info.Text);
                int count = this.GetNumberOfResultsForContentType(type);
                counts.Add(type, count);
            }

            return counts;
        }

        /// <summary>
        /// Get Category Pages Count
        /// </summary>
        /// <returns> The count of category pages </returns>
        public int GetCategoryPagesCount()
        {
            this.ExpandContentTypesPane();
            return DriverExtensions.GetElements(CategoryPagesLocator).Count;
        }

        /// <summary>
        /// Get the number of results for a given content type
        /// </summary>
        /// <param name="contentType"> Content type </param>
        /// <returns> Number of results </returns>
        public int GetContentTypeCount(ContentType contentType)
            => this.GetContentTypeCount(this.ContentTypeMap[contentType].Text);

        /// <summary>
        /// Get the number of results for a given content type
        /// </summary>
        /// <param name="contentType"> Content type </param>
        /// <returns> Number of results </returns>
        public int GetContentTypeCount(string contentType)
            => DriverExtensions.WaitForElement(By.XPath(string.Format(ContentTypeCountLctMask, contentType))).Text.ConvertCountToInt();

        /// <summary>
        /// Gets a list of elements for each facet link in the document type facet list. The selected facet will not be a link
        /// but will have the same id when another facet is selected and becomes a link again.
        /// </summary>
        /// <returns>List of facet elements</returns>
        public List<string> GetDocumentTypeFacetLinkList()
        {
            this.ExpandContentTypesPane();
            return DriverExtensions.GetElements(DocumentTypeFacetLinksLocator).Select(element => element.Text).ToList();
        }

        /// <summary>
        /// Get the list of Content Types
        /// </summary>
        /// <returns>List of content types</returns>
        public List<string> GetContentTypesList()
            => DriverExtensions.GetElements(ActiveContentTypeLocator).Select(e => e.Text).ToList();

        /// <summary>
        /// Get the list of the all Content Types (active and disabled)
        /// </summary>
        /// <returns>List of content types</returns>
        public List<string> GetAllContentTypesList() => DriverExtensions.GetElements(ListItemLocator, AllContentTypeLocator).Select(e => e.Text).ToList();

        /// <summary>
        /// Get Header Text
        /// </summary>
        /// <returns>header text</returns>
        public string GetHeaderText() => DriverExtensions.GetText(HeaderLocator);

        /// <summary>
        /// Get the number of results for a given content type
        /// </summary>
        /// <param name="contentType"> Content type </param>
        /// <returns> Number of results </returns>
        public int GetNumberOfResultsForContentType(ContentType contentType)
        {
            this.ExpandContentTypesPane();
            string displayNameString = this.ContentTypeMap[contentType].Text;
            By facetCountLocator = By.XPath(string.Format(ContentTypeLctMask, displayNameString));
            return DriverExtensions.WaitForElement(facetCountLocator).Text.ConvertCountToInt();
        }

        /// <summary>
        /// Get the number of results for a given content type
        /// </summary>
        /// <param name="contentType"> Content type </param>
        /// <returns> Number of results </returns>
        public int GetNumberOfResultsForContentType(string contentType)
        {
            this.ExpandContentTypesPane();
            By facetCountLocator = By.XPath(string.Format(ContentTypeLctMask, contentType));
            return DriverExtensions.WaitForElement(facetCountLocator).Text.ConvertCountToInt();
        }

        /// <summary>
        /// Expand Content Types Pane
        /// </summary>
        private void ExpandContentTypesPane()
        {
            if (DriverExtensions.IsDisplayed(ShowMoreLinkLocator, 5))
            {
                DriverExtensions.WaitForElement(ShowMoreLinkLocator).CustomClick();
            }
        }

        /// <summary>
        /// Determine if a facet element is selected by the given facet name.
        /// </summary>
        /// <param name="facetName"> Name displayed for the facet </param>
        /// <returns> True if the facet is selected, false otherwise </returns> 
        public bool IsContentTypeSelected(string facetName)
            => DriverExtensions.WaitForElement(ActiveFacetItemLocator).GetText().Contains(facetName);

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
            => DriverExtensions.IsDisplayed(By.XPath(string.Format(ContentTypeLctMask, contentType)), 5);

        /// <summary>
        /// Determines if a specific content type link is present on the left navigation
        /// </summary>
        /// <param name="contentType">Content to look for</param>
        /// <returns>True if the link is displayed, false otherwise</returns>
        public bool IsSubContentTypeDisplayed(ContentType contentType)
            => DriverExtensions.IsDisplayed(By.XPath(string.Format(SubContentTypeLctMask, this.ContentTypeMap[contentType].Text)));

        /// <summary>
        /// Determines if a specific content type link is enabled
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns>true if content type option is enabled</returns>
        public bool IsContentTypeEnabled(ContentType contentType) =>
            !DriverExtensions.GetElement(By.XPath(string.Format(FacetItemLctMask, ContentTypeMap[contentType].Text))).GetAttribute("Class").Contains("co_disabled");

        /// <summary>
        /// Verify View label is  present
        /// </summary>
        /// <returns>true if Delivery widget present, false otherwise</returns>
        public bool IsHeaderDisplayed() => DriverExtensions.IsDisplayed(HeaderLocator, 5);

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
            DriverExtensions.Click(SaveButtonLocator);
            DriverExtensions.WaitForJavaScript();
        }
    }
}