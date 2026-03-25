namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Elements.Judicial;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.LitigationAnalytics;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Execution;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Facet dialog
    /// </summary>
    public class LitigationAnalyticsFacetDialog : BaseModuleRegressionComponent, ICreatablePageObject
    {
        private static readonly By FacetTitleLocator = By.XPath(".//*[@class='SearchFacet-buttonToggle']");
        private static readonly By SearchFieldLocator = By.XPath(".//input[contains(@class,'SearchFacet-inputText')]");
        private static readonly By ContainerLocator = By.XPath("//section[contains(@class, 'SearchFacetHierarchy is-active')]");
        private static readonly By OpportunityContainerLocator = By.XPath("//section[contains(@class, 'SearchFacetHierarchy')]");
        private static readonly By NameSubTabLocator = By.XPath(".//analytics-filter//div[@class = 'la-ToggleButton']/button[@aria-label = 'Name']");
        private static readonly By DocketsSubTabLocator = By.XPath(".//analytics-filter//div[@class = 'la-ToggleButton']/button[contains(@aria-label ,'# of ')]");
        private static readonly By SelectAllcheckboxLocator = By.Id("selectAll");
        private static readonly By ItemLocator = By.XPath(".//*[contains(@class,'SearchFacet-listItem')]");
        private static readonly By OpportunityFinderFilterItemLocator = By.XPath(".//*[contains(@class, 'SearchFacet-listItemGroup')]");
        private static readonly By CascadingItemLocator = By.XPath("//div[@class ='SearchFacet-listItemGroup']/*[@class = 'SearchFacet-label']");

        /// <summary>
        /// The facet title
        /// </summary>
        public IButton FacetTitleButton => new CustomClickButton(this.ComponentLocator, FacetTitleLocator);

        /// <summary>
        /// Enter search query
        /// </summary>
        /// <param name="searchQuery">Search query</param>
        /// <returns>New instance of the page</returns>
        public void EnterSearchQuery(string searchQuery)
        {
            SafeMethodExecutor.WaitUntil(() => DriverExtensions.GetElement(this.ComponentLocator, SearchFieldLocator).Displayed, 40);
            DriverExtensions.SetTextField(searchQuery, DriverExtensions.GetElement(this.ComponentLocator, SearchFieldLocator));
        }

        /// <summary>
        /// Name subtab     
        /// </summary>
        public IButton NameSubTabButton => new Button(NameSubTabLocator);

        /// <summary>
        /// # of dockets/motions subtab
        /// </summary>
        public IButton DocketsSubTabButton => new Button(DocketsSubTabLocator);

        /// <summary>
        /// Select all checkbox
        /// </summary>
        public ICheckBox SelectAllCheckbox => new CheckBox(this.ComponentLocator, SelectAllcheckboxLocator);

        /// <summary>
        /// Result list.
        /// </summary>
        public List<LitigationAnalyticsFacetItem> FacetResultItems => new ItemsCollection<LitigationAnalyticsFacetItem>(this.ComponentLocator, ItemLocator).Where(item => item.IsCurrentItemDisplayed()).ToList();

        /// <summary>
        /// Result list.
        /// </summary>
        public List<LitigationAnalyticsFacetItem> CascadingFacetResultItems => new ItemsCollection<LitigationAnalyticsFacetItem>(this.ComponentLocator, CascadingItemLocator).Where(item => item.IsCurrentItemDisplayed()).ToList();

        /// <summary>
        /// Result list.
        /// </summary>
        public ItemsCollection<LitigationAnalyticsFacetItem> OpportunityFinderFacetResultItems => new ItemsCollection<LitigationAnalyticsFacetItem>(OpportunityContainerLocator, OpportunityFinderFilterItemLocator);

        /// <summary>
        /// Get filter by name
        /// </summary>
        protected EnumPropertyMapper<LitigationAnalyticsFacets, WebElementInfo> Facets =>
            EnumPropertyModelCache.GetMap<LitigationAnalyticsFacets, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/LitigationAnalytics");

        /// <summary>
        /// Component Locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}