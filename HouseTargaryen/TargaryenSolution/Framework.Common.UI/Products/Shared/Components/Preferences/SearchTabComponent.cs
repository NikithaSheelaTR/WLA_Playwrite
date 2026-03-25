namespace Framework.Common.UI.Products.Shared.Components.Preferences
{
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Enums.Preferences;
    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Products.WestLawNext.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Search Tab Component
    /// </summary>
    public class SearchTabComponent : BaseTabComponent
    {
        private const string SearchPreferenceLocatorMask = "//input[@name='DefaultSortType_{0}' and @value='{1}']";
        private static readonly By ContainerLocator = By.Id("coid_userSettingsTabPanel3");
        
        /// <summary>
        /// Default Search Sort Preferences Options
        /// </summary>
        private readonly Dictionary<ContentType, SearchSortOption> defaultSearchSortOptions =
            new Dictionary<ContentType, SearchSortOption>
                {
                    { ContentType.Cases, SearchSortOption.Relevance },
                    {
                        ContentType.TrialCourtOrders, SearchSortOption.Relevance
                    },
                    { ContentType.StatutesAndCourtRules, SearchSortOption.Relevance },
                    { ContentType.Regulations, SearchSortOption.Relevance },
                    {
                        ContentType.AdministrativeDecisionsAndGuidance,
                        SearchSortOption.Relevance
                    },
                    { ContentType.PracticalLaw, SearchSortOption.Relevance },
                    {
                        ContentType.SecondarySources, SearchSortOption.Relevance
                    },
                    { ContentType.Forms, SearchSortOption.Relevance },
                    { ContentType.Briefs, SearchSortOption.Relevance },
                    {
                        ContentType.TrialCourtDocuments,
                        SearchSortOption.Relevance
                    },
                    {
                        ContentType.ExpertMaterials, SearchSortOption.Relevance
                    },
                    {
                        ContentType.JuryVerdictsAndSettlements,
                        SearchSortOption.Relevance
                    },
                    {
                        ContentType.ProposedAndEnactedLegislation,
                        SearchSortOption.Relevance
                    },
                    {
                        ContentType.ProposedAndAdoptedRegulations,
                        SearchSortOption.Relevance
                    },
                    { ContentType.News, SearchSortOption.Date },
                    { ContentType.Dockets, SearchSortOption.CaseTitle }
                };

        /// <summary>
        /// Content Type Map
        /// </summary>
        private EnumPropertyMapper<ContentType, ContentTypeInfo> contentTypeMap;

        /// <summary>
        /// Search Type Map
        /// </summary>
        private EnumPropertyMapper<SearchTab, WebElementInfo> searchTabMap;

        /// <summary>
        /// Search Sort Option Map
        /// </summary>
        private EnumPropertyMapper<SearchSortOption, WebElementInfo> searchSortOptionMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<ContentType, ContentTypeInfo> ContentTypeMap
            => this.contentTypeMap = this.contentTypeMap ?? EnumPropertyModelCache.GetMap<ContentType, ContentTypeInfo>();

        /// <summary>
        /// Gets the SearchTab enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<SearchTab, WebElementInfo> SearchTabMap
            => this.searchTabMap = this.searchTabMap ?? EnumPropertyModelCache.GetMap<SearchTab, WebElementInfo>();

        /// <summary>
        /// Gets the SearchSortOption enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<SearchSortOption, WebElementInfo> SearchSortOptionMap
            => this.searchSortOptionMap = this.searchSortOptionMap ?? EnumPropertyModelCache.GetMap<SearchSortOption, WebElementInfo>();

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Search";

        /// <summary>
        /// Returns true if the specified element on the search tab is displayed
        /// </summary>
        /// <param name="searchTabOption">the option to look for</param>
        /// <returns>If the option is visible</returns>
        public bool IsDisplayed(SearchTab searchTabOption)
            => DriverExtensions.IsDisplayed(By.CssSelector(this.SearchTabMap[searchTabOption].LocatorString));

        /// <summary>
        /// Returns true if the specified element on the search tab is selected (checked for checkboxes)
        /// </summary>
        /// <param name="searchTabOption">the option to look for</param>
        /// <returns>true if selected, false otherwise</returns>
        public bool IsSearchTabOptionSelected(SearchTab searchTabOption)
        {
            By locator = By.CssSelector(this.SearchTabMap[searchTabOption].LocatorString);
            return DriverExtensions.GetElement(locator).Selected;
        }

        /// <summary>
        /// Set Search Default Sort Order
        /// ToDo Should Be investigated - return value is not necessary
        /// </summary>
        /// <param name="contentType"> Content Type </param>
        /// <param name="sortType"> Sort Type  </param>
        /// <returns>  The <see cref="SearchTabComponent"/>SearchTabComponent</returns>
        public SearchTabComponent SetSearchDefaultSortOrder(ContentType contentType, SearchSortOption sortType)
        {
            string contentTypeString = this.ContentTypeMap[contentType].NarrowPaneLinkLocatorString;
            string sortTypeValue = this.SearchSortOptionMap[sortType].Id;
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.Click(By.XPath(string.Format(SearchPreferenceLocatorMask, contentTypeString , sortTypeValue)));
            DriverExtensions.WaitForJavaScript();
            return this;
        }

        /// <summary>
        /// ToDo Search Tab Method (Preference Dialog)
        /// Default Search Sorting preferences the following
        /// Cases - Relevance
        /// TrialCourtOrders - Relevance
        /// Statutes - Relevance
        /// Regulations - Relevance
        /// AdministrativeDecisionsAndGuidance - Relevance
        /// PracticalLaw - Relevance
        /// SecondarySources - Relevance
        /// Forms - Relevance
        /// Briefs - Relevance
        /// TrialCourtDocuments - Relevance
        /// ExpertMaterials - Relevance
        /// JuryVerdictsAndSettlements - Relevance
        /// ProposedAndEnactedLegislation - Relevance
        /// ProposedAndAdoptedRegulations - Relevance
        /// News - Date
        /// </summary>
        /// <returns>
        /// The <see cref="SearchTabComponent"/>SearchTabComponent </returns>
        public SearchTabComponent SetSearchSortPreferencesToDefault()
        {
            foreach (KeyValuePair<ContentType, SearchSortOption> option in this.defaultSearchSortOptions)
            {
                this.SetSearchDefaultSortOrder(option.Key, option.Value);
            }

            // return new PreferencesDialog();
            return this;
        }

        /// <summary>
        /// Sets the specified checkbox option on the search tab to the specified value.
        /// </summary>
        /// <param name="tabOption">
        /// the option to look for
        /// </param>
        /// <param name="setTo"> What to set the checkbox to. True for checked, false for unchecked. </param>
        /// <returns>
        /// The <see cref="SearchTabComponent"/>SearchTabComponent</returns>
        public SearchTabComponent SetSearchTabOptionCheckbox(SearchTab tabOption, bool setTo)
        {
            By locator = By.CssSelector(this.SearchTabMap[tabOption].LocatorString);
            DriverExtensions.SetCheckbox(setTo, locator);
            return this;
        }
    }
}