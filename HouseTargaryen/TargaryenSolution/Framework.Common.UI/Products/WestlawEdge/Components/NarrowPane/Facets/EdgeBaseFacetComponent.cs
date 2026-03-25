namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.WestlawEdge.Elements.Judicial;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Base class for all Facets
    /// </summary>
    public abstract class EdgeBaseFacetComponent : BaseModuleRegressionComponent
    {
        private static readonly By FacetTitleLocator = By.XPath(".//*[@class='SearchFacet-buttonText']");

        //TODO: Remove 'or' from locator
        private static readonly By FacetExpandIconLocator = By.XPath(".//span[@class='icon25 icon_add-blue' or @class='th_indigo_icon icon_plus' or contains(@class, 'icon_rightCaret-blue')] | //a[@id='co_dateWidget_Date_dropdown'] | //button[@class='a11yDateWidget-button co_defaultBtn']");

        //TODO: Remove 'or' from locator
        private static readonly By FacetCollapseIconLocator = By.XPath(".//span[@class = 'icon25 icon_remove-blue' or 'th_indigo_icon icon_minus']");

        private static readonly By FacetStateLocator = By.XPath(".//button[@class='SearchFacet-buttonToggle']");

        private const string SelectFacetItemByNameLctMask  = "//*[contains(text(),'{0}')]";

        /// <summary>
        /// The facet title
        /// </summary>
        public IButton FacetTitle => new CustomClickButton(this.ComponentLocator, FacetTitleLocator);

        /// <summary>
        /// Expand button
        /// </summary>
        public IButton ExpandButton => new Button(this.ComponentLocator, FacetExpandIconLocator);

        /// <summary>
        /// Collapse button
        /// </summary>
        public IButton CollapseButton => new Button(this.ComponentLocator, FacetCollapseIconLocator);

        /// <summary>
        /// Click on facet to expand
        /// </summary>
        public void ExpandFacet()
        {
            if (DriverExtensions.IsDisplayed(this.ComponentLocator, FacetExpandIconLocator))
            {
                DriverExtensions.Click(ComponentLocator, FacetExpandIconLocator);
            }
        }

     /// <summary>
    /// Click on facet to collapse
    /// </summary>
    public void CollapseFacet()
        {
            if (this.ExpandButton.Displayed)
            {
                this.FacetTitle.Click();
            }
        }

        /// <summary>
        /// Verifies that a facet is collapsed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>. True if a facet is collapsed </returns>
        public bool IsFacetCollapsed() => DriverExtensions
            .GetElement(this.ComponentLocator, FacetStateLocator).GetAttribute("aria-expanded").Equals("false");

        /// <summary>
        /// Select Facet Item By Name.
        /// </summary>
        /// <param name="facetItemName"> facet item that can be expanded </param>
        public void SelectFacetItemByName(string facetItemName) => DriverExtensions
                                                                   .GetElement(By.XPath(string.Format(SelectFacetItemByNameLctMask, facetItemName))).Click();

    }

}
