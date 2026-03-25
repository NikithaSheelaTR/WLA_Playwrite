namespace Framework.Common.UI.Products.Shared.Items.ResultList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Components;
    using Framework.Common.UI.Products.Shared.Enums.Document;
    using Framework.Common.UI.Products.Shared.Enums.Search;
    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// The Search Result Item
    /// todo: make this class internal when Search Manager is implemented
    /// </summary>
    public class ResultListItem : BaseItem, IDraggableWebElement
    {
        /// <summary>
        /// The guid locator.
        /// </summary>
        protected static readonly By GuidLocator = By.XPath(".//h3//a | .//h2//a");

        // Yellow color
        private const string ColorForSearchTermString = "rgba(255, 255, 102, 1)";

        private const string AdditionalForEdgeColorForSearchTermString = "rgba(255, 248, 96, 1)";

        // Purple color
        private const string ColorForSearchWithinTermString = "rgba(190, 190, 252, 1)";

        private const string DetailLevelLctMask = "co_search_detailLevel_{0}";

        private static readonly By LinkLocator =
            By.XPath(".//a[starts-with(@id,'cobalt_') and contains(@id,'result_') and contains(@id,'title')]");

        private static readonly By SummaryLocator =
            By.XPath(".//div[contains(@id, 'co_searchResults_summary') or contains(@class,'co_searchResults_summary')]");

        private static readonly By CitationsLocator = By.XPath(".//span[starts-with(@class, 'co_') and contains(@class, 'search_detailLevel_')]");

        private static readonly By DetailLevelTwoLocator = By.ClassName("co_search_detailLevel_2");

        private static readonly By CheckboxLocator = By.CssSelector("input[type='checkbox'][id*='checkbox_']");

        private static readonly By KeyCiteFlagLocator = By.XPath(".//div[contains(@class, 'search_keyciteFlag')]//a");

        private static readonly By SnippetLocator = By.XPath(".//div[contains(@id, 'co_snippet')]");

        private static readonly By SnippetKeywordLocator = By.XPath("./span");

        private static readonly By SearchTermLocator = By.ClassName("co_searchTerm");

        private static readonly By SearchWithinTermLocator = By.XPath(".//span[@class='co_searchTerm co_keyword']");

        private static readonly By DocumentTrackButtonLocator = By.XPath(".//a[@title='Track' and @class='co_tbButton co_search_trackable_document']");

        private static readonly By BackgroundImageDocumentIconLocator = By.XPath(".//li[contains(@class,'co_document_icon_')]");

        private static readonly By SourceLocator = By.XPath(".//div[contains(@class, 'co_searchContent')]/span/h3");

        private static readonly By SearchResultCountLocator = By.ClassName("co_searchCount");

        private static readonly By SearchResultCitationLocator = By.ClassName("co_searchResults_citation");

        private static readonly By OfficialCitesLinkLocator = By.XPath(".//a[contains(@id, 'cobalt_result_official_cite')]");

        private static readonly By HoverKeyCiteTextLocator = By.XPath("//a[@class = 'co_rStripedFlagSm']");
        
        /// <summary>
        /// Initializes a new instance of the ResultListItem class.
        /// Search result item constructor
        /// </summary>
        /// <param name="containerElement"> The container Element. </param>
        public ResultListItem(IWebElement containerElement)
            : base(containerElement)
        {
        }

        /// <summary>
        /// Gets Data
        /// </summary>
        public string Date
        {
            get
            {
                DateTime dateTime;
                IWebElement date =
                    DriverExtensions.GetElements(this.Container, CitationsLocator)
                                    .FirstOrDefault(i => DateTime.TryParse(i.Text, out dateTime));
                return date != null ? date.Text : string.Empty;
            }
        }

        /// <summary>
        /// If item is out Of Plan
        /// </summary>
        public bool IsOutOfPlan => this.Container.GetAttribute("class").Contains("co_outOfPlan");

        /// <summary>
        /// The value of docguid attribute
        /// </summary>
        public string Guid
            => DriverExtensions.SafeGetElement(this.Container, GuidLocator)?.GetAttribute("docguid");

        /// <summary>
        /// todo: rename to Title
        /// Gets Title Link Text
        /// </summary>
        public string LinkText => DriverExtensions.GetElement(this.Container, LinkLocator).Text;

        /// <summary>
        /// Gets Summary
        /// </summary>
        public string Summary => this.TryGetText(SummaryLocator);

        /// <summary>
        /// Get full link of the document (href value)
        /// </summary>
        public string Link => DriverExtensions.SafeGetElement(this.Container, GuidLocator)?.GetAttribute("href");

        /// <summary>
        /// The detail text.
        /// </summary>
        public string DetailText => this.TryGetText(DetailLevelTwoLocator);

        /// <summary>
        /// The jurisdiction (jur attribute value)
        /// </summary>
        public string Jurisdiction => DriverExtensions.GetElement(this.Container, LinkLocator).GetAttribute("jur");

        /// <summary>
        /// Gets the Publication name
        /// </summary>
        public string PublicationName
        {
            get
            {
                IWebElement citationElement = DriverExtensions.GetElement(
                    this.Container,
                    SearchResultCitationLocator);

                return
                    DriverExtensions.GetElements(citationElement, By.XPath("./span[1]"))
                                            .Select(elem => elem.Text)
                                            .ToList()
                                            .Aggregate((i, j) => i + " " + j)
                                            .Trim();
            }
        }

        /// <summary>
        /// Gets the Citation 
        /// </summary>
        public string CitationName
        {
            get
            {
                IWebElement citationElement = DriverExtensions.GetElement(
                    this.Container,
                    SearchResultCitationLocator);

                return
                    DriverExtensions.GetElements(citationElement, By.XPath("./span[2]"))
                                            .Select(elem => elem.Text)
                                            .ToList()
                                            .Aggregate((i, j) => i + " " + j)
                                            .Trim();
            }
        }

        /// <summary>
        /// Gets the Jurisdiction 
        /// </summary>
        public string AggregatedJurisdiction
        {
            get
            {
                IWebElement citationElement = DriverExtensions.GetElement(
                    this.Container,
                    SearchResultCitationLocator);

                return
                    DriverExtensions.GetElements(citationElement, By.XPath("./span[4]"))
                                            .Select(elem => elem.Text)
                                            .ToList()
                                            .Aggregate((i, j) => i + " " + j)
                                            .Trim();
            }
        }

        /// <summary>
        /// Citations list
        /// </summary>
        public IList<string> Citations =>
            DriverExtensions.GetElements(this.Container, CitationsLocator).Any()
                ? DriverExtensions.GetElements(this.Container, CitationsLocator).Select(c => c.Text).ToList()
                : new List<string>();

        /// <summary>
        /// Gets the aggregated citations.
        /// </summary>
        public string AggregatedCitation
        {
            get
            {
                IWebElement citationElement = DriverExtensions.GetElement(
                    this.Container,
                    SearchResultCitationLocator);

                return
                    DriverExtensions.GetElements(citationElement, By.XPath("./span"))
                                            .Select(elem => elem.Text)
                                            .ToList()
                                            .Aggregate((i, j) => i + " " + j)
                                            .Trim();
            }
        }

        /// <summary>
        /// The search terms.
        /// </summary>
        public IEnumerable<string> SearchTerms =>
            DriverExtensions.GetElements(this.Container, SearchTermLocator).Any()
                ? DriverExtensions.GetElements(this.Container, SearchTermLocator)
                                  .Where(elem => elem.GetCssValue("background-color").Equals(ColorForSearchTermString)
                                                 || elem.GetCssValue("background-color").Equals(AdditionalForEdgeColorForSearchTermString))
                                  .Select(el => el.Text) : new List<string>();

        /// <summary>
        /// The search terms.
        /// </summary>
        public IEnumerable<string> SearchWithinTerms =>
            DriverExtensions.GetElements(this.Container, SearchWithinTermLocator).Any()
                ? DriverExtensions.GetElements(this.Container, SearchWithinTermLocator).Where(
                    elem => elem.GetCssValue("background-color")
                                .Equals(ColorForSearchWithinTermString)).Select(el => el.Text)
                : new List<string>();

        /// <summary>
        /// Gets the key cite flag.
        /// </summary>
        public virtual KeyCiteFlag KeyCiteFlag
        {
            get
            {
                if (DriverExtensions.IsDisplayed(this.Container, KeyCiteFlagLocator))
                {
                    string flagClass = this.KeyCiteFlagElement.GetAttribute("class");
                    return flagClass.GetEnumValueByPropertyModel<KeyCiteFlag, WebElementInfo>(mod => mod.ClassName);
                }

                return KeyCiteFlag.NoFlag;
            }
        }

        /// <summary>
        /// The snippets.
        /// </summary>
        public IEnumerable<string> Snippets =>
            DriverExtensions.GetElements(this.Container, SnippetLocator).Any(snippet => snippet.Displayed)
                ? DriverExtensions.GetElements(this.Container, SnippetLocator).Where(snippet => snippet.Displayed).Select(el => el.Text)
                : new List<string>();

        /// <summary>
        /// Gets all of the document icons on the search result
        /// </summary>
        /// <returns> The IList of the document icons </returns>
        public IList<DocumentIcon> DocIcons
        {
            get
            {
                IEnumerable<string> pathsToSrc = DriverExtensions
                                                 .GetElements(this.Container, By.XPath(".//img"))
                                                 .Where(elem => elem.IsDisplayed())
                                                 .Select(elem => elem.GetAttribute("src"));

                return pathsToSrc.Any()
                           ? this.DocIconsMap.Where(
                                     pair => pathsToSrc.Any(path => path.Contains(pair.Value.SourceFile)))
                                 .Select(pair => pair.Key).ToList()
                           : new List<DocumentIcon>();
            }
        }

        /// <summary>
        /// Gets the source of the search result, through the string that appears in the result item
        /// header when LoggingQuantity is set to Verbose on the routing page
        /// </summary>
        public IList<SearchResultSource> SearchResultSources =>
            DriverExtensions.GetElement(this.Container, SourceLocator).Text.Split(',')
                            .Select(elem => elem.GetEnumValueByText<SearchResultSource>()).ToList();

        /// <summary>
        /// The doc icons map.
        /// </summary>
        private EnumPropertyMapper<DocumentIcon, WebElementInfo> DocIconsMap =>
             EnumPropertyModelCache.GetMap<DocumentIcon, WebElementInfo>();

        /// <summary>
        /// Gets the key cite flag element.
        /// </summary>
        private IWebElement KeyCiteFlagElement => DriverExtensions.SafeGetElement(this.Container, KeyCiteFlagLocator);

        /// <summary>
        /// Gets all of the document icons on the search result item that have background image
        /// </summary>
        /// <returns> The IList of the document icons that have background image </returns>
        public IList<DocumentIcon> GetDocIconsWithBackgroundImage()
        {
            List<string> iconsPaths = DriverExtensions
                                      .GetElements(this.Container, BackgroundImageDocumentIconLocator).Where(element => element.IsDisplayed())
                                      .Select(element => element.GetAttribute("class")).ToList();

            return iconsPaths.Any()
                       ? this.DocIconsMap.Where(pair => iconsPaths.Any(path => pair.Value.LocatorString.Contains(path.Trim())))
                             .Select(pair => pair.Key).ToList()
                       : new List<DocumentIcon>();
        }

        /// <summary>
        /// Is item in view
        /// </summary>
        public bool IsItemInView()
        {
            bool isInView;
            try
            {
                isInView = DriverExtensions.SafeGetElement(this.Container, LinkLocator).IsElementInView();
            }
            catch (NullReferenceException)
            {
                isInView = false;
            }
            return isInView;
        }

        /// <summary>
        /// The click snippet.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <typeparam name="TPage">
        /// The type of page
        /// </typeparam>
        /// <returns>
        /// The document page
        /// </returns>
        public virtual TPage ClickSnippet<TPage>(int index = 0) where TPage : ICommonDocumentPage
        {
            DriverExtensions.GetElements(this.Container, new ByChained(SnippetLocator, By.XPath("./a"))).ElementAt(index).Click();
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// The click key cite flag.
        /// </summary>
        /// <typeparam name="TPage">
        /// the type of page
        /// </typeparam>
        /// <returns>
        /// The Document page
        /// </returns>
        public TPage ClickKeyCiteFlag<TPage>()
            where TPage : ICreatablePageObject
        {
            this.KeyCiteFlagElement.Click();
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// GetHoverKeyCiteText
        /// </summary>
        /// <returns></returns>
        public string GetHoverKeyCiteText() =>
            DriverExtensions.GetAttribute("oldtitle", this.Container, HoverKeyCiteTextLocator);

        /// <summary>
        /// Sets result list checkBox
        /// </summary>
        /// <param name="state"> True to check, false - uncheck </param>
        public void SetCheckBox(bool state = true)
            => DriverExtensions.GetElement(this.Container, CheckboxLocator).SetCheckboxUsingClick(state);

        /// <summary>
        /// The is checkbox selected.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsCheckboxSelected() => DriverExtensions.GetElement(this.Container, CheckboxLocator).Selected;

        /// <summary>
        /// The click title.
        /// </summary>
        /// <typeparam name="TPage">
        /// Page type
        /// </typeparam>
        /// <returns>
        /// The desired page
        /// </returns>
        public TPage ClickTitle<TPage>()
            where TPage : ICreatablePageObject
        {
            // Used JavascriptClick for IE browser.
            DriverExtensions.WaitForElement(this.Container, LinkLocator).JavascriptClick();
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// The click random track button.
        /// </summary>
        /// <typeparam name="TPage"> Type of object to return.  </typeparam>
        /// <returns> New page instance./>. </returns>
        public TPage ClickTrackButton<TPage>() where TPage : ICreatablePageObject
        {
            DriverExtensions.GetElement(this.Container, DocumentTrackButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// The click official cites link.
        /// </summary>
        /// <typeparam name="TPage">
        /// Page type
        /// </typeparam>
        /// <returns>
        /// The desired page
        /// </returns>
        public TPage ClickOfficialCitesLink<TPage>()
            where TPage : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(this.Container, OfficialCitesLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// The has index.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool HasIndex() => DriverExtensions.IsDisplayed(this.Container, SearchResultCountLocator);

        /// <summary>
        /// Get index number of the result item
        /// </summary>
        /// <returns><see cref="Int32"/></returns>
        public int GetIndex() =>
            DriverExtensions.GetTextSafe(this.Container, SearchResultCountLocator).ConvertCountToInt();

        /// <summary>
        /// Checks whether or not expected detail elements are visible for the specified detail level
        /// </summary>
        /// <param name="searchDetailLevel"> The detail level to check </param>
        /// <returns> True if all elements of the specified detail level are displayed, false otherwise </returns>
        public bool IsDetailLevelDisplayed(DetailLevel searchDetailLevel)
        {
            int numberOfDetailLevel = this.GetNumberOfDetailLevel(searchDetailLevel);
            IList<IWebElement> elements =
                DriverExtensions.GetElements(this.Container, By.ClassName(string.Format(DetailLevelLctMask, numberOfDetailLevel)));
            return elements.All(elem => elem.IsDisplayed());
        }

        /// <summary>
        /// Parameter 'origination context' from the URL
        /// </summary>
        /// <returns> Origination Context </returns>
        public string GetOriginationContextParameter()
        {
            int startIndex = this.Link.IndexOf("originationContext", StringComparison.Ordinal);
            int endIndex = this.Link.IndexOf("transitionType=SearchItem", StringComparison.Ordinal);
            string originalContext = this.Link.Substring(startIndex, endIndex - startIndex - 1);
            originalContext = originalContext.Replace("%20", string.Empty).Replace("originationContext=", string.Empty);
            return originalContext;
        }

        /// <summary>
        /// Gets a set amount of highlighted key words from a given snippet
        /// </summary>
        /// <param name="snippetNumber"> Number of snippet </param>
        /// <param name="termsCount"> Count of search terms </param>
        /// <returns> String of KeyWords </returns>
        public string GetSnippetKeyWords(int snippetNumber, int termsCount) =>
            DriverExtensions
                .GetElements(
                    DriverExtensions.GetElements(this.Container, new ByChained(SnippetLocator, By.XPath("./a"))).ElementAt(snippetNumber),
                    SnippetKeywordLocator).Take(termsCount).Select(item => item.Text).Aggregate((s1, s2) => s1 + s2);

        /// <inheritdoc />
        IWebElement IDraggableWebElement.GetDraggableElement() => DriverExtensions.GetElement(this.Container, LinkLocator);

        /// <summary>
        /// Gets element text
        /// </summary>
        /// <param name="element"> The element locator </param>
        /// <returns> Element Text </returns>
        protected string TryGetText(By element) => DriverExtensions.GetTextSafe(this.Container, element);

        /// <summary>
        /// The get number of detail level.
        /// </summary>
        /// <param name="searchDetailLevel">
        /// The search detail level.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private int GetNumberOfDetailLevel(DetailLevel searchDetailLevel)
        {
            int detailLevelNum = 0;
            switch (searchDetailLevel)
            {
                case DetailLevel.LessDetail:
                    detailLevelNum = 1;
                    break;
                case DetailLevel.MoreDetail:
                    detailLevelNum = 2;
                    break;
                case DetailLevel.MostDetail:
                    detailLevelNum = 3;
                    break;
            }

            return detailLevelNum;
        }
    }
}