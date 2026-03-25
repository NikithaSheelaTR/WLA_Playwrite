namespace Framework.Common.UI.Products.Shared.Pages.Browse
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.BreadCrumb;
    using Framework.Common.UI.Products.Shared.Components.CategoryPage;
    using Framework.Common.UI.Products.Shared.Components.Facets.RightFacets;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Products.WestlawEdge.Components.HomePage;
    using Framework.Common.UI.Products.WestLawNext.Components.IpTools;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.WestLawNextCanada.Components;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    /// <summary>
    /// This class is the Search Module Regression's implementation of a Browse Page (Category Page) Object
    /// </summary>
    public class CommonBrowsePage : CommonAuthenticatedWestlawNextPage, IBrowseCategoryPage
    {
        private const string CategoryLinkLocator =
            "//a[@href and normalize-space(text())={0} and not(./span[text()='Collapse' or text()='Expand']) and not(@href='#')] | //a[starts-with(@id,'tocItem_') and @href and not(@href='#') and normalize-space(string(.)) = {0} and not(.//span[normalize-space(.)='Collapse' or normalize-space(.)='Expand'])]";

        private static readonly By CustomSearchTabLocator = By.XPath("//*[@id='co_resultsPageLabel'] | //*[@id='co_browsePageLabel']");

        private static readonly By IndexSearchResultCountTextLocator = By.ClassName("co_indexSearchResultCount");

        private static readonly By ScopeInfoIconLocator = By.Id("coid_website_browsePageScopeMoreInfo");

        private static readonly By TocBrowseCategoryItemsLocator = By.XPath("//li[.//input and .//a]");

        private static readonly By TocItemLinkLocator = By.XPath(".//a");

        private static readonly By TocItemCheckboxLocator = By.XPath(".//input");

        private static readonly By AllLinksLocator = By.XPath("//div[@class='co_browsePageSectionWidget']//li//a");

        private static readonly By LoadingSpinnerLabelLocator = By.XPath("//div[contains(@class, 'co_search_ajaxLoading')]");

        private EnumPropertyMapper<Jurisdiction, JurisdictionInfo> jurisdictionsMap;

        /// <summary>
        /// Loading spinner label
        /// </summary>
        public ILabel LoadingSpinnerLabel => new Label(LoadingSpinnerLabelLocator);

        /// <summary>
        /// Specify Content To Search
        /// </summary>
        public SpecifyContentToSearchComponent SpecifyContentComponent => new SpecifyContentToSearchComponent();

        /// <summary>
        /// Favorites Component (Add To and Edit Favorites Links)
        /// </summary>
        public FavoritesComponent Favorites { get; private set; } = new FavoritesComponent();

        /// <summary>
        /// Legislative Watch Page Component (select progress of bills and documents)
        /// </summary>
        public LegislativeWatchComponent LegislativeWatch { get; private set; } = new LegislativeWatchComponent();

        /// <summary>
        /// Start Page Component (Add and Remove Page as start after user login)
        /// </summary>
        public StartPageComponent StartPage { get; private set; } = new StartPageComponent();

        /// <summary>
        /// Tools And Resources Widget
        /// Might be present on the right hand side for some category pages
        /// </summary>
        public ToolsAndResourcesFacetComponent ToolsAndResourcesComponent { get; private set; } = new ToolsAndResourcesFacetComponent();

        /// <summary>
        /// Suggested Titles widget
        /// Might be present on the right hand side for 'secondary source' page
        /// </summary>
        public SuggestedTitlesFacetComponent SuggestedTitlesFacetComponent { get; private set; } = new SuggestedTitlesFacetComponent();

        /// <summary>
        /// Tray component
        /// </summary>
        public TrayComponent TrayComponent { get; } = new TrayComponent();

        /// <summary>
        /// The Breadcrumb Component.
        /// </summary>
        public BreadCrumbComponent Breadcrumb { get; private set; } = new BreadCrumbComponent();

        /// <summary>
        /// Toc Items(Link and checkbox) List
        /// </summary>
        public IDictionary<Link, CheckBox> TocItemsList =>
            DriverExtensions.GetElements(TocBrowseCategoryItemsLocator)
                            .Select(
                                item => new
                                {
                                    link = new Link(item, TocItemLinkLocator),
                                    checkbox = new CheckBox(item, TocItemCheckboxLocator)
                                }).ToDictionary(pair => pair.link, pair => pair.checkbox);

        /// <summary>
        /// Gets the jurisdiction enumeration to JurisdictionInfo map.
        /// </summary>
        protected EnumPropertyMapper<Jurisdiction, JurisdictionInfo> JurisdictionsMap
            => this.jurisdictionsMap = this.jurisdictionsMap ?? EnumPropertyModelCache.GetMap<Jurisdiction, JurisdictionInfo>();

        /// <summary>
        /// Checks if a category page link is displayed
        /// </summary>
        /// <param name="category"> The link to check for </param>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsCategoryLinkDisplayed(string category)
        {
            IWebElement categoryLinkElement = this.GetCategoryLinkByText(category);
            return categoryLinkElement != null && categoryLinkElement.Displayed;
        }

        /// <summary>
        /// Click link by partial text
        /// That method is needed for categories which have 'nbsp;' symbols in html markup
        /// </summary>
        /// <typeparam name="T"> Page object </typeparam>
        /// <param name="linkText"> The link text. </param>
        /// <returns> New page object </returns>
        public override T ClickLinkByText<T>(string linkText)
        {
            DriverExtensions.WaitForElement(By.PartialLinkText(linkText)).Click();
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary> 
        /// Clicks a link on a category page 
        /// </summary>
        /// <typeparam name="T"> Page Object </typeparam>
        /// <param name="category"> the text of the link to click </param>
        /// <returns> an page object of the specified type </returns>
        public T ClickCategory<T>(string category) where T : ICreatablePageObject
        {
            DriverExtensions.Click(this.GetCategoryLinkByText(category));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks a Jurisdiction link on a category page 
        /// </summary>
        /// <typeparam name="T"> Page Object </typeparam>
        /// <param name="jurisdiction"> the jurisdiction to click on </param>
        /// <returns> an page object of the specified type </returns>
        public T ClickJurisdictionCategory<T>(Jurisdiction jurisdiction) where T : ICreatablePageObject
            => this.ClickCategory<T>(this.JurisdictionsMap[jurisdiction].JurisdictionName);

        /// <summary>
        /// Gets the text of the custom search tab
        /// </summary>
        /// <returns>Text of the custom tab</returns>
        public string GetCustomTabText()
            => DriverExtensions.GetElement(CustomSearchTabLocator).GetHiddenText();

        /// <summary>
        /// Returns the breadcrumb text
        /// </summary>
        /// <returns>The text</returns>
        public int GetIndexSearchResultCount()
            => DriverExtensions.WaitForElement(IndexSearchResultCountTextLocator).Text.ConvertCountToInt();

        /// <summary>
        /// Clicks scope info
        /// </summary>
        /// <returns> The <see cref="ScopeDialog"/>. </returns>
        public ScopeDialog ClickScopeInfoIcon()
        {
            DriverExtensions.GetElement(ScopeInfoIconLocator).Click();
            return new ScopeDialog();
        }

        /// <summary>
        /// Checks whether the page Scope Icon is displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsScopeInfoIconDisplayed() => DriverExtensions.IsDisplayed(ScopeInfoIconLocator);

        /// <summary>
        /// Returns true if link is displayed on page
        /// </summary>
        /// <param name="linkText"> The Link </param>
        /// <returns> True if displayed, false otherwise  </returns>
        public bool IsLinkDisplayed(string linkText) => DriverExtensions.IsDisplayed(By.LinkText(linkText), 5);

        /// <summary> 
        /// Get Category Page Link by visible Link Text 
        /// </summary>
        /// <param name="categoryLink"> Category Link Name </param>
        /// <returns> Link as IWebElement </returns>
        private IWebElement GetCategoryLinkByText(string categoryLink)
            => DriverExtensions.SafeGetElement(SafeXpath.BySafeXpath(CategoryLinkLocator, categoryLink));

        /// <summary>
        /// Retrieve all the Links in the given page
        /// </summary>
        /// <returns></returns>
        public IReadOnlyCollection<ILink> GetAllLinks() => new ElementsCollection<Link>(AllLinksLocator);

        /// <summary>
        /// Creates a map of each key to its correspondi.
        /// </summary>
        public Dictionary<TKey, TValue> CreateMap<TKey, TValue>(IList<TKey> keys, IList<TValue> values)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }
   
            if (keys.Count != values.Count)
            {
                throw new ArgumentException("Keys and values lists must have the same count.");
            }

            var map = new Dictionary<TKey, TValue>();
            for (int i = 0; i < keys.Count; i++)
            {
                map[keys[i]] = values[i];
            }

            return map;
        }
    }
}