namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.WestlawEdge.Elements.Judicial;
    using Framework.Common.UI.Raw.WestlawEdge.Items.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Base class for all Hierarchy Facet (Practice Area and Jurisdiction facets)
    /// </summary>
    public class BaseSearchHierarchyFacetComponent : EdgeBaseFacetComponent
    {
        private const string ExpandFacetItemByNameLctMask = ".//button[./span[contains(text(),'Expand') and contains(text(),'{0}')]] | .//button[./span[text()='{0}']]";

        private static readonly By SearchInputFacetLocator = By.XPath(".//input[@class='SearchFacet-inputText']");

        private static readonly By FacetOptionItemListLocator =
            By.XPath(".//div[contains(@class,'SearchFacet-list') and @role='group' or @role='tree' or @role='listitem'] | .//div[contains(@class,'SearchFacet-list') and @role='group' or @role='tree' or @role='list']");
       
        private static readonly By FacetItemLocator = By.XPath(".//div[contains(@class,'listItem')]");    

        private static readonly By CheckedOptionLocator = By.XPath(".//input[@checked]/following-sibling::label[@class='SearchFacet-labelText']//span");

        private static readonly By StateButtonTreeLocator =
            By.XPath(".//span[@class='Icon-collapsed']/ancestor::button | .//span[contains(text(), 'Collapse')]/ancestor::div[contains(@class, 'SearchFacet-button')]");

        private static readonly By ExpandButtonTreeLocator =
            By.XPath(".//div[contains(@class,'SearchFacet-button SearchFacet-buttonLink SearchFacet-buttonSortTree')]/following-sibling::label/input | .//button[./span[@class='Icon-collapsed']]");

        private static readonly By SearchFacetBreadCrumbsLocator = By.ClassName("SearchFacet-labelText");

        private static readonly By ApplyButtonLocator = By.XPath("//*[.='Apply' or contains(text(),'Activer')]"); 

        private static readonly By ApplyFilterButtonLocator = By.XPath("//*[@id='co_facet_AdminJurisdiction_filterButton']");

        private static readonly By FacetCheckBoxlocater = By.XPath("//*[@class='SearchFacet-inputCheckbox']");

        private static readonly By AdminstrationFacetOptionItemListLocator =
           By.XPath(".//*[@class='co_overlayBox_leftContent']");

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSearchHierarchyFacetComponent"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public BaseSearchHierarchyFacetComponent(By componentLocator)
        {
            this.ComponentLocator = componentLocator;
        }

        /// <summary>
        /// Apply button
        /// </summary>
        public IButton ApplyButton => new CustomClickButton(this.ComponentLocator, ApplyButtonLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator { get; }

        /// <summary>
        /// The is displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator);

        /// <summary>
        /// Is text field displayed 
        /// </summary>
        /// <returns>
        /// True if search input is displayed.
        /// </returns>
        public bool IsSearchInputDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, SearchInputFacetLocator);

        /// <summary>
        /// Is Breadcrumb displayed
        /// </summary>
        /// <returns> true if breadcrumbs are displayed</returns>
        public bool IsBreadcrumbsDisplayed()
            => DriverExtensions.IsDisplayed(this.ComponentLocator, SearchFacetBreadCrumbsLocator);

        /// <summary>
        /// enter text in SearchInput
        /// </summary>
        /// <param name="text">Text to enter</param>
        public void EnterTextInSearchInput(string text)
            => DriverExtensions.SetTextField(text, this.ComponentLocator, SearchInputFacetLocator);
               
        /// <summary>
        /// get items which currently display
        /// </summary>
        /// <returns>
        /// The list of typeahead suggestions.
        /// </returns>
        public List<string> GetNameOfDisplayedSuggesionOptionItems()
            => this.GetTopLevelFacetItems().Select(item => item.Title).ToList();

        /// <summary>
        /// set checkbox for item 
        /// true - set checkbox
        /// false - unset
        /// </summary>
        /// <typeparam name="T"> Name </typeparam>
        /// <param name="index"> Index </param>
        /// <param name="action"> Action  </param>
        /// <returns> T </returns>
        public T SetCheckboxForFacetSuggestionItemByNumber<T>(int index, bool action = true)
            where T : ICreatablePageObject
        {
            this.GetTopLevelFacetItems().ElementAt(index - 1).SetCheckbox(action);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Gets name of selected options
        /// </summary>
        /// <returns>selected options names</returns>
        public List<string> GetSelectedOptions() =>
            DriverExtensions.GetElements(this.ComponentLocator, CheckedOptionLocator).Select(el => el.Text).ToList();

        /// <summary>
        /// The get all options.
        /// </summary>
        /// <returns>
        /// The list of facet options
        /// </returns>
        public List<string> GetAllOptions()
        {
            this.ExpandFacet();
            return this.GetAllFacetItems().Select(item => item.Title)
               .GroupBy(x => x).Select(g => g.First())
               .ToList();
        }

        /// <summary>
        /// Sets checkbox by name for single mode
        /// </summary>
        /// <typeparam name="T"> T page  </typeparam>
        /// <param name="state"> state  </param>
        /// <param name="itemName"> Item Name </param>
        /// <returns> page  </returns>
        public T SetCheckboxForFacetByName<T>(bool state, string itemName) where T : ICreatablePageObject
        {
            this.ExpandFacet();
            this.GetAllFacetItems().First(item => item.Title.Equals(itemName)).SetCheckbox(state);
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.CreatePageInstance<T>();
        }  

        /// <summary>
        /// Apply Facet
        /// </summary>
        /// <typeparam name="T"> T page  </typeparam>
        /// <param name="state"> state  </param>
        /// <param name="itemNames"> item Names</param>
        /// <returns> page  </returns>
        public virtual T ApplyFacet<T>(bool state, params string[] itemNames) where T : ICreatablePageObject
        {
            this.ExpandFacet();
            foreach (string itemName in itemNames)
            {
                this.GetAllFacetItems().First(item => item.Title.Equals(itemName)).SetCheckbox(state);
                DriverExtensions.WaitForJavaScript();
            }

            if (this.ApplyButton.Displayed)
            {
                return this.ApplyButton.Click<T>();
            }

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get count for item in facet by name
        /// </summary>
        /// <param name="itemName"> Item Name </param>
        /// <returns> page  </returns>
        public int GetItemCountByName(string itemName)
        {
            this.ExpandFacet();
            return this.GetAllFacetItems().First(item => item.Title.Equals(itemName)).Count;
        }

        /// <summary>
        /// Is Checkbox Displayed
        /// </summary>
        /// <param name="itemName">Item name</param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsCheckboxDisplayed(string itemName)
        {
            this.ExpandFacet();
            return this.GetAllFacetItems().First(item => item.Title.Equals(itemName)).IsDisplayed;
        }    

        /// <summary>
        /// Is Checkbox Selected
        /// </summary>
        /// <param name="itemName"> Item name</param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsCheckboxSelected(string itemName)
        {
            this.ExpandFacet();
            return this.GetAllFacetItems().First(item => item.Title.Equals(itemName)).IsSelected;
        }

        /// <summary>
        /// Verifies that the list of items is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if list of items is displayed. </returns>
        public bool IsItemsListDisplayed() => DriverExtensions.IsDisplayed(FacetOptionItemListLocator);
               
        /// <summary>
        /// Gets count of specific content type
        /// </summary>
        /// <param name="contentType">Content type e.g Expert Materials, Expert Testimonial History</param>
        /// <returns>Returns -1 if item doesn't contain count label</returns>
        public int GetItemCountByContentType(string contentType)
        {
            this.ExpandFacet();
            return this.GetTopLevelFacetItems().FirstOrDefault(item => item.Title.Contains(contentType))?.Count ?? -1;
        }

        /// <summary>
        /// Are there empty items
        /// </summary>
        /// <returns>The <see cref="bool"/>. True if list of items is displayed. </returns>
        public bool AreThereEmptyItemsAmongFacetOptions()
        {
            this.ExpandFacet();
            return this.GetAllFacetItems().TrueForAll(j => j.Count > 0 && j.IsDisplayed && j.Title.Length > 0);
        }

        /// <summary>
        /// Verifies that a facet item is collapsed.
        /// </summary>
        /// <param name="index"> Number of the facet item that can be expanded </param>
        /// <returns>
        /// The <see cref="bool"/> True if a facet item is collapsed </returns>
        public bool IsFacetItemCollapsed(int index) => DriverExtensions
            .GetElements(this.ComponentLocator, ExpandButtonTreeLocator).ElementAt(index).GetAttribute("aria-expanded").Equals("false");

        /// <summary>
        /// Verify is option clickable
        /// </summary>
        /// <param name="option">option name</param>
        /// <returns>true if clickable, false otherwise</returns>
        public virtual bool IsOptionEnabled(string option)
        {
            this.ExpandFacet();
            return this.GetAllFacetItems().First(item => item.Title.Contains(option)).IsEnabled;
        }
        
        /// <summary>
        /// Get all facet items and all their child items through the tree view. 
        /// </summary>
        /// <param name="parentElement">parent element of items to return</param>
        /// <returns>List of all facet items including child</returns>
        public List<FacetOptionItem> GetAllFacetItems(IWebElement parentElement = null)
        {
            var results = new List<FacetOptionItem>();

            IWebElement parent = parentElement ?? DriverExtensions.GetElement(this.ComponentLocator);
            DriverExtensions.WaitForElementDisplayed(parent, FacetOptionItemListLocator);
            DriverExtensions.GetElements(parent, FacetOptionItemListLocator, FacetItemLocator).ToList().ForEach(
                                el =>
                                {
                                    if (DriverExtensions.IsDisplayed(el, StateButtonTreeLocator))
                                    {
                                        DriverExtensions.Click(el, StateButtonTreeLocator);
                                        List<FacetOptionItem> elements = this.GetAllFacetItems(el);
                                        results.AddRange(elements);
                                    }

                                    results.Add(new FacetOptionItem(el));
                                });
            return results;
        }

        /// <summary>
        /// Get facet items which are currently available (expanded items are not expanded) 
        /// </summary>
        /// <returns>
        /// The list of top level facet items.
        /// </returns>
        public IEnumerable<FacetOptionItem> GetTopLevelFacetItems()
            =>
                DriverExtensions.GetElements(this.ComponentLocator, FacetOptionItemListLocator, FacetItemLocator)
                                .Select(item => new FacetOptionItem(item));

        /// <summary>
        /// Expand a facet item.
        /// </summary>
        /// <param name="facetItemName"> facet item that can be expanded </param>
        public void ExpandFacetItemByName(string facetItemName) => DriverExtensions
                                                                   .GetElement(this.ComponentLocator, By.XPath(string.Format(ExpandFacetItemByNameLctMask, facetItemName))).Click();

        /// <summary>
        /// Filter button
        /// </summary>
        public IButton FilterButton => new CustomClickButton(this.ComponentLocator, ApplyFilterButtonLocator);

        /// <summary>
        /// Facet Check Box
        /// </summary>
        public IButton FacetCheckBox => new CustomClickButton(this.ComponentLocator, FacetCheckBoxlocater);

        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsCheckboxSelected() => DriverExtensions.IsDisplayed(this.ComponentLocator, FacetCheckBoxlocater);

        /// <summary>
        /// Apply Filter
        /// </summary>
        /// <typeparam name="T"> T page  </typeparam>
        /// <param name="state"> state  </param>
        /// <param name="itemNames"> item Names</param>
        /// <returns> page  </returns>
        public virtual T ApplyFilter<T>(bool state, params string[] itemNames) where T : ICreatablePageObject
        {

            if (this.FilterButton.Displayed)
            {
                return this.FilterButton.Click<T>();
            }

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Remove Filter
        /// </summary>
        /// <typeparam name="T"> T page  </typeparam>
        /// <param name="state"> state  </param>
        /// <param name="itemNames"> item Names</param>
        /// <returns> page  </returns>
        public virtual T RemoveFilter<T>(bool state, params string[] itemNames) where T : ICreatablePageObject
        {

            if (this.FacetCheckBox.Displayed)
            {
                return this.FacetCheckBox.Click<T>();
            }
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Verifies that the list of items is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if list of items is displayed. </returns>
        public bool IsFacetItemsListDisplayed() => DriverExtensions.IsDisplayed(AdminstrationFacetOptionItemListLocator);

        /// <summary>
        /// checked option
        /// </summary>
        public IButton CheckedOption => new CustomClickButton(this.ComponentLocator, CheckedOptionLocator);
    }
}
