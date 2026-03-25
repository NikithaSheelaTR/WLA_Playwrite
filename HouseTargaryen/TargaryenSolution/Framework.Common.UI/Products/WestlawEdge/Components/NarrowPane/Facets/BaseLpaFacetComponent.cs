namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Dialogs;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Raw.WestlawEdge.Items.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Base class for LPA Indigo facet
    /// It can be used for Law Firm, Party, Attorney, or Judge
    /// </summary>
    public class BaseLpaFacetComponent : BaseSearcheableFacetComponent, IHasLpaSearchLink
    {
        private const string FacetCheckboxLctMask = "//label[@class='SearchFacet-label' and .//mark[text()='{0}']]/input";

        private const string FacetLabelLctMask = "//label[@class='SearchFacet-label']//*[text() ='{0}']";

        private const string FacetSuggestedItemCountLctMask = "(/descendant-or-self::node()/span[@class='SearchFacet-outputTextValue'])[{0}]";

        private static readonly By TextFieldFacetLocator
            = By.XPath(".//input[contains(@id,'SearchFacetLpa')] | .//input[contains(@class,'SearchFacet')]");

        private static readonly By LpaLinkLocator = By.XPath(".//button[contains(@class , 'SearchFacet-buttonLink')]");

        private static readonly By FacetAppliedListLocator = By.XPath(".//div[contains(@class,'SearchFacet-list') and @role='list']");

        private static readonly By FacetSuggestionListLocator =
            By.XPath(
                ".//div[@class='SearchFacet-instructions']//ancestor::div[contains(@class,'SearchFacet-list') and @role='list'] | .//div[contains(@class,'SearchFacet-list') and @role='list']");

        private static readonly By FacetItemLocator = By.XPath(".//div[@role = 'listitem']");

        // The locator currently only tested for Judges - not other LPA types.
        private static readonly By IncludeSubSearchCheckboxLocator = By.XPath(".//input[contains(@id,'co_facet_includeSubSearchCheckbox')]");

        private readonly By componentLocator;

        /// <summary>
        /// Jurisdiction filter checkbox
        /// </summary>
        public ICheckBox IncludeSubSearchCheckbox =>
            new CheckBox(this.ComponentLocator, IncludeSubSearchCheckboxLocator);

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseLpaFacetComponent"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public BaseLpaFacetComponent(By componentLocator)
        {
            this.componentLocator = componentLocator;
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => this.componentLocator;

        /// <summary>
        /// Enter text and click on LPA link
        /// </summary>
        /// <typeparam name="T">
        /// desired type of page object
        /// </typeparam>
        /// <param name="itemName">
        /// the name of item
        /// </param>
        /// <returns>
        /// The desired page object
        /// </returns>
        public T EnterTextAndClickLpaSearchLink<T>(string itemName) where T : ICreatablePageObject
        {
            this.ExpandFacet();
            this.EnterTextInFindField(itemName);
            DriverExtensions.WaitForElement(DriverExtensions.GetElement(this.ComponentLocator), LpaLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Set checkbox for item 
        /// </summary>
        /// <param name="index">
        /// counting number (1,2,3,...) index into the list of LPA suggestions
        /// </param>
        /// <param name="action">
        /// true - set checkbox
        /// false - unset
        /// </param>
        /// <returns>T</returns>
        public T SetCheckboxForFacetSuggestionItemByNumber<T>(int index, bool action = true)
            where T : ICreatablePageObject
        {
            this.GetFacetSuggestionItem().ElementAt(index - 1).SetCheckbox(action);

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get names of all applied items
        /// </summary>
        /// <returns></returns>
        public List<string> GetNameOfAllAppliedItem()
        {
            this.ExpandFacet();
            return this.GetFacetAppliedItems().Select(item => item.Title).ToList();
        }

        /// <summary>
        /// Apply Facet
        /// </summary>
        /// <typeparam name="T">T page</typeparam>
        /// <param name="query">query</param>
        /// <param name="action">action
        /// true - set checkbox
        /// false - unset
        /// </param>
        /// <returns>page</returns>
        public T ApplyFacet<T>(string query, bool action) where T : ICreatablePageObject
        {
            this.ExpandFacet();
            DriverExtensions.GetElement(this.ComponentLocator, TextFieldFacetLocator).SetTextField(query);
            string judgeString = string.Format(FacetCheckboxLctMask, query);
            DriverExtensions.WaitForElement(By.XPath(judgeString));
            DriverExtensions.SetCheckbox(By.XPath(judgeString), action);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Determine if there is a specific judge facet applied
        /// </summary>
        /// <param name="judge"> Judge to look for </param>
        /// <returns> True if the facet is applied, false otherwise </returns>
        public bool IsFacetApplied(string judge)
        {
            By judgeLocator = By.XPath(string.Format(FacetLabelLctMask, judge));
            return DriverExtensions.IsDisplayed(judgeLocator, 5);
        }

        /// <summary>
        /// GetFacetAppliedItem
        /// </summary>
        /// <returns>List of applied options</returns>
        public List<FacetOptionItem> GetFacetAppliedItems() => 
            DriverExtensions.GetElements(this.ComponentLocator, FacetAppliedListLocator, FacetItemLocator).Select(item => new FacetOptionItem(item)).ToList();

        /// <summary>
        /// Get facet suggested item result count 
        /// </summary>
        /// <param name="index">
        /// counting number (1,2,3,...) index into the list of LPA suggestions
        /// </param>
        /// <returns>integer result count</returns>
        public int GetFacetSuggestedItemResultCountByIndexNumber(int index)
        {
            By facetSuggestedOptionCountLocator = By.XPath(string.Format(FacetSuggestedItemCountLctMask, index));
            return DriverExtensions.GetElement(facetSuggestedOptionCountLocator).Text.ConvertCountToInt();
        }

        private IEnumerable<FacetOptionItem> GetFacetSuggestionItem()
        {
            DriverExtensions.WaitForElement(DriverExtensions.GetElement(this.ComponentLocator, FacetSuggestionListLocator), FacetItemLocator);
            return DriverExtensions.GetElements(this.ComponentLocator, FacetSuggestionListLocator, FacetItemLocator)
                                                  .Select(item => new FacetOptionItem(item)).ToList();
        }
    }
}
