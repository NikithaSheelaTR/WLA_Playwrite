namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.NarrowPane.Facets
{
    using System.Collections.Generic;
    using System.Linq;
    
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;      

    using OpenQA.Selenium;

    /// <summary>
    /// SearchWithinDialog
    /// </summary>
    public class EdgeSearchWithinDialog : BaseModuleRegressionDialog
    {
        private EnumPropertyMapper<SearchWithin, WebElementInfo> searchWithinLinksMap;

        private static readonly By ErrorMessageLabelLocator = By.XPath(".//div[@class='co_infoBox_message']");       

        private static readonly By ContainerLocator = By.XPath("//*[@id='coid_lightboxOverlay']");

        /// <summary>
        /// Error Message lable
        /// </summary>
        public ILabel ErrorMessageLabel => new Label(ContainerLocator, ErrorMessageLabelLocator);

        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeSearchWithinDialog"/> class. 
        ///  </summary>
        /// <param name="additionalInfo"> additional Info </param>
        public EdgeSearchWithinDialog(string additionalInfo)
        {
            this.AdditionalInfo = additionalInfo;
        }

        /// <summary>
        /// Indigo Footer Links Map
        /// </summary>
        protected EnumPropertyMapper<SearchWithin, WebElementInfo> SearchWithinLinksMap =>
            this.searchWithinLinksMap = this.searchWithinLinksMap
                                        ?? EnumPropertyModelCache.GetMap<SearchWithin, WebElementInfo>(
                                            this.AdditionalInfo,
                                            @"Resources/EnumPropertyMaps/WestlawEdge/Dialogs");

        private string AdditionalInfo { get; }        

        /// <summary>
        /// Enter Query
        /// </summary>
        /// <param name="query"> Query </param>
        /// <returns> The <see cref="EdgeSearchWithinDialog"/>. </returns>
        public EdgeSearchWithinDialog EnterQuery(string query)
        {
            DriverExtensions.SetTextField(query, By.XPath(this.SearchWithinLinksMap[SearchWithin.SearchInput].LocatorString));
            return this;
        }

        /// <summary>
        /// Enter query and search
        /// </summary>
        /// <param name="query"> The query. </param>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T EnterQueryAndPerformSearchWithin<T>(string query) where T : ICreatablePageObject =>
            this.EnterQuery(query).ClickSearchButton<T>();

        /// <summary>
        /// Click Search button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New page object </returns>
        public T ClickSearchButton<T>()
            where T : ICreatablePageObject =>
            this.ClickElement<T>(By.XPath(this.SearchWithinLinksMap[SearchWithin.SearchButton].LocatorString));

        /// <summary>
        /// Set Look For Terms In The Same Paragraph checkbox
        /// </summary>
        public void SetLookForTermsInTheSameParagraph(bool setTo = true) =>
            DriverExtensions.SetCheckbox(By.XPath(this.SearchWithinLinksMap[SearchWithin.LookForTermsInTheSameParagraphCheckbox].LocatorString), setTo);

        /// <summary>
        /// Checks undo search link
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsUndoSearchWithinLinkDisplayed() => DriverExtensions.IsDisplayed(By.XPath(this.SearchWithinLinksMap[SearchWithin.UndoButton].LocatorString));

        /// <summary>
        /// Clicks undo search link
        /// </summary>
        /// <typeparam name="T">Page type</typeparam>
        /// <returns>The <see cref="bool"/>.</returns>
        public T ClickUndoSearchWithinLink<T>() where T : ICreatablePageObject => this.ClickElement<T>(By.XPath(this.SearchWithinLinksMap[SearchWithin.UndoButton].LocatorString));

        /// <summary>
        /// Returns search input text
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetSearchWithinTextboxText() => DriverExtensions.GetText(By.XPath(this.SearchWithinLinksMap[SearchWithin.SearchInput].LocatorString));

        /// <summary>
        /// Get Search Input Value
        /// </summary>
        /// <returns> Text from Search button </returns>
        public string GetSearchButtonText() => DriverExtensions.GetText(By.XPath(this.SearchWithinLinksMap[SearchWithin.SearchButton].LocatorString));

        /// <summary>
        /// Click on Recent Searches button
        /// </summary>
        /// <returns> The <see cref="EdgeSearchWithinDialog"/>. </returns>
        public EdgeSearchWithinDialog ClickRecentSearchesButton()
        {
            this.ClickElement(By.XPath(this.SearchWithinLinksMap[SearchWithin.RecentSearchesButton].LocatorString));
            return this;
        }

        /// <summary>
        /// Click Recent Searches Query
        /// </summary>
        /// <param name="index"> link's index </param>
        /// <returns> The <see cref="EdgeSearchWithinDialog"/>. </returns>
        public EdgeSearchWithinDialog ClickRecentSearchesSuggestion(int index)
        {
            this.ClickElement(
                DriverExtensions
                    .GetElements(By.XPath(this.SearchWithinLinksMap[SearchWithin.RecentSearchesQuery].LocatorString))
                    .ElementAt(index));
            return this;
        }

        /// <summary>
        /// Click Recent Searches suggestion.
        /// </summary>
        /// <param name="query"> Search query </param>
        /// <returns>The <see cref="EdgeSearchWithinDialog"/>.</returns>
        public EdgeSearchWithinDialog ClickRecentSearchesSuggestion(string query)
        {
            this.ClickElement(
                By.XPath(
                    string.Format(this.SearchWithinLinksMap[SearchWithin.RecentSearchesQuery].LocatorMask, query)));
            return this;
        }

        /// <summary>
        /// Get Recent Searches queries
        /// </summary>
        /// <returns> Recent searches </returns>
        public List<string> GetRecentSearchesList() =>
            DriverExtensions.GetElements(By.XPath(this.SearchWithinLinksMap[SearchWithin.RecentSearchesQuery].LocatorString)).Select(query => query.Text).ToList();

        /// <summary>
        /// Get disabled Recent Searches queries
        /// </summary>
        /// <returns> Disaled recent searches </returns>
        public List<string> GetDisabledRecentSearchesList() =>
            DriverExtensions.GetElements(By.XPath(this.SearchWithinLinksMap[SearchWithin.DisabledRecentSearchesQuery].LocatorString)).Select(query => query.Text).ToList();       
    }
}