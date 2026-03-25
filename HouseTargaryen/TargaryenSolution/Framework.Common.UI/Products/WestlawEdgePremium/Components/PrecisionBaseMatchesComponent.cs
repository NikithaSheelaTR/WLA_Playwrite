namespace Framework.Common.UI.Products.WestlawEdgePremium.Components
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Precision base matches component
    /// </summary>
    public abstract class PrecisionBaseMatchesComponent
    {       
        private static readonly By CollapsedButtonTreeLocator = By.XPath(".//button[@class='PrecisionSearch-button Icon-collapsed']");    
        private static readonly By FacetItemLocator = By.XPath(".//label | .//li[@role='treeitem']");        
        private static readonly By HighlightedTermLocator = By.XPath(".//span[@class='co_searchTerm']");
        private static readonly By TopItemLabelLocator = By.XPath("./div[not(contains(@class, 'secondary'))]/*/label | .//div[not(contains(@class, 'secondary'))]/*[@role = 'listitem']//label");
        private static readonly By ItemLabelLocator = By.XPath(".//*[contains(@class, 'PrecisionSearch-labelText') or contains(@class, 'PrecisionFilters-labelText')]");
        
        /// <summary>
        /// Top items label
        /// </summary>
        public IReadOnlyCollection<ILabel> TopItemLabels => new ElementsCollection<Label>(this.ComponentLocator, TopItemLabelLocator);

        /// <summary>
        /// Get facet item by name
        /// </summary>
        /// <param name="itemName"> Item Name </param>
        /// /// <param name="isExpand"> Is expand</param>
        /// <returns>Facet item</returns>
        public PrecisionFacetOptionItem GetFacetItemByName(string itemName, bool isExpand = true) =>
            this.GetAllFacetItems(isExpand).First(item => item.ItemTitleLink.Text.Contains(itemName));

        /// <summary>
        /// Get highlighted terms text
        /// </summary>
        /// <returns>List of highlighted terms</returns>
        public List<string> GetHighlightedTermsText() => new ElementsCollection<Label>(this.ComponentLocator, HighlightedTermLocator).Select(t => t.Text).ToList();

        /// <summary>
        /// Get all facet items and all their child items through the tree view. 
        /// </summary>
        /// <returns>List of all facet items including child</returns>
        public List<PrecisionFacetOptionItem> GetAllFacetItems(bool isExpand = true)
        {
            var results = new List<PrecisionFacetOptionItem>();

            DriverExtensions.WaitForElementDisplayed(this.ComponentLocator, ItemLabelLocator);

            if (isExpand)
            {
                this.ExpandAll();
            }

            DriverExtensions.WaitForJavaScript();
            DriverExtensions.GetElements(this.ComponentLocator, FacetItemLocator).ToList().ForEach(el => results.Add(new PrecisionFacetOptionItem(el)));

            return results;
        }

        /// <summary>
        /// Gets count of specific item name
        /// </summary>
        /// <param name="itemName">Item name</param>
        /// /// <param name="isExpand">Is expand</param>
        /// <returns>Returns -1 if item doesn't contain count label</returns>
        public int? GetItemCountByItemName(string itemName, bool isExpand = true) 
            => this.GetFacetItemByName(itemName, isExpand).ItemTitleLink.Text.RetrieveCountFromBrackets();

        /// <summary>
        /// Is facet item displayed
        /// </summary>
        /// <param name="itemName"> Item Name </param>
        /// /// <param name="isExpand"> Is expand </param>
        /// <returns>Is facet item displayed returns true, false otherwise</returns>
        public bool IsFacetItemDisplayed(string itemName, bool isExpand = true)
        {
            bool isFacetItemDisplayed = false;
            if (this.GetAllFacetItems(isExpand).First().ItemTitleLink.Text.Contains('('))
            {
                isFacetItemDisplayed = this.GetAllFacetItems(isExpand).Select(item => item.ItemTitleLink.Text.Substring(0, item.ItemTitleLink.Text.LastIndexOf('(')).Trim()).Any(title => title.Equals(itemName));
            }
            else
            {
                isFacetItemDisplayed = this.GetAllFacetItems(isExpand).Select(item => item.ItemTitleLink.Text.Trim()).Any(title => title.Equals(itemName));

            }

            return isFacetItemDisplayed;
        }

        /// <summary>
        /// Verify that component is displayed
        /// </summary>
        /// <returns> True if component is displayed, false otherwise </returns>
        public bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator);

        /// <summary>
        /// Expand all elements
        /// </summary>
        private void ExpandAll()
        {          
            DriverExtensions.WaitForElementDisplayed(this.ComponentLocator, ItemLabelLocator);
            if (DriverExtensions.IsDisplayed(this.ComponentLocator, CollapsedButtonTreeLocator))
            {
                DriverExtensions.GetElements(this.ComponentLocator, CollapsedButtonTreeLocator).ToList().ForEach(el => DriverExtensions.Click(el));                
            }            

        }        

        /// <summary>
        /// Component locator
        /// </summary>
        protected abstract By ComponentLocator { get; }
    }
}
