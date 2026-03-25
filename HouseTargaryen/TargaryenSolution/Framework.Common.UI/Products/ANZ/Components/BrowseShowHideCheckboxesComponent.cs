namespace Framework.Common.UI.Products.ANZ.Components
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using OpenQA.Selenium;

    /// <summary>
    /// Browse Show Hide Checkboxes Component
    /// </summary>
    public class BrowseShowHideCheckboxesComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id='coid_browseShowHideCheckboxesContainer']");

        private static readonly By SpecifyContentToSearchCheckboxLocator = By.XPath(".//input[@id='coid_browseShowCheckboxes']");

        private static readonly By SelectAllContentCheckboxLocator = By.XPath(".//input[@id='coid_browseSelectAllCheckboxInput']");

        private static readonly By ClearSelectionLinkLocator = By.XPath(".//a[@id='co_itemsClearAnchor']");

        /// <summary>
        /// Specify Content To Search Checkbox
        /// </summary>
        public ICheckBox SpecifyContentToSearchCheckbox => new CheckBox(this.ComponentLocator, SpecifyContentToSearchCheckboxLocator);

        /// <summary>
        /// Select All Content Checkbox
        /// </summary>
        public ICheckBox SelectAllContentCheckbox => new CheckBox(this.ComponentLocator, SelectAllContentCheckboxLocator);

        /// <summary>
        /// Clear Selection Link
        /// </summary>
        public ILink ClearSelectionLink => new Link(this.ComponentLocator, ClearSelectionLinkLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;        
    }
}
