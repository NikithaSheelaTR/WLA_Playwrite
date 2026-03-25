namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.Typeahead
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.WestlawEdge.Components.NewTypeAhead;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.NewTypeAhead;
    using Framework.Common.UI.Raw.WestlawEdge.Items.TrDiscover;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Core.Utils.Enums;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Precision Suggestions component
    /// </summary>
    public class PrecisionSuggestionsComponent : SuggestionsComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id = 'typeAheadContentTypesDiv']");
        private static readonly By PrecisionResearchTypeLocator = By.XPath(".//span[@id = 'Precision Research_ContentType']/div");
        private static readonly By SuggestionLinkLocator = By.XPath(".//li//button");
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionSuggestionsComponent"/> class.
        /// </summary>
        public PrecisionSuggestionsComponent()
        {
            this.InitializeSupportedCategories();
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// EdgeDocIconsMap
        /// </summary>
        protected EnumPropertyMapper<TypeaheadPrecisionResearchType, WebElementInfo> PrecisionResearchTypeMap =>
            EnumPropertyModelCache.GetMap<TypeaheadPrecisionResearchType, WebElementInfo>(
                                       string.Empty,
                                       @"Resources/EnumPropertyMaps/WestlawEdgePremium");

        /// <summary>
        /// Get all section from the Search Suggestions 
        /// </summary>
        public List<TypeaheadPrecisionResearchType> GetDisplayedTypesInSearchSuggestions()
        {

            var avaliableTypes = DriverExtensions
                                      .GetElements(this.ComponentLocator, PrecisionResearchTypeLocator)
                                      .Select(type => type.GetAttribute("id")).ToList();
            var list = new List<TypeaheadPrecisionResearchType>();
            if (avaliableTypes.Any())
            {
                avaliableTypes.ForEach(t =>

                    list.Add(this.PrecisionResearchTypeMap.First(map => map.Value.LocatorString.Contains(t)).Key));
            }

            return list;
        }

        /// <summary>
        /// Get all suggestions from the Search Suggestions by Type
        /// </summary>
        public List<TrdSearchSuggestionItem> GetSuggestionsByType(TypeaheadPrecisionResearchType type) => 
          this.GetItems(this.ComponentLocator, By.XPath(this.PrecisionResearchTypeMap.First(t => t.Key.Equals(type)).Value.LocatorString), By.XPath(".//li"))
            .Select(element => (TrdSearchSuggestionItem)Activator.CreateInstance(typeof(TrdSearchSuggestionItem), element)).ToList();

        ///<summary>
        /// Get all suggestions from the Search Suggestions by Type
        /// </summary>
        public T ClickOnSuggestionByTypeAndIndex<T>(TypeaheadPrecisionResearchType type, int index)
            where T : ICreatablePageObject
        {
            IWebElement element = this.GetItems(
                this.ComponentLocator,
                By.XPath(this.PrecisionResearchTypeMap.First(t => t.Key.Equals(type)).Value.LocatorString),
                SuggestionLinkLocator)[index];
            element.ScrollToElementCenter();
            element.Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// InitializeSupportedCategories
        /// </summary>
        protected override void InitializeSupportedCategories()
        {
            this.SupportedCategories =
           new List<NewTypeAheadCategories>
               {
                        NewTypeAheadCategories.WestlawAnswers,
                        NewTypeAheadCategories.PrecisionResearch,
                        NewTypeAheadCategories.LitigationAnalytics,
                        NewTypeAheadCategories.FactorsCourtsConsider,
                        NewTypeAheadCategories.ContentPages,
                        NewTypeAheadCategories.Snapshots

               };
        }
    }
}
