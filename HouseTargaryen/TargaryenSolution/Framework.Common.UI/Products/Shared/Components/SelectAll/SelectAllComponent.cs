namespace Framework.Common.UI.Products.Shared.Components.SelectAll
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;

    using OpenQA.Selenium;

    /// <summary>
    /// The select all component.
    /// </summary>
    public class SelectAllComponent : BaseModuleRegressionComponent
    {
        private static readonly By SelectAllCheckboxLocator = By.XPath(".//input");

        private static readonly By ClearSelectedLocator = By.XPath(".//button[@class='co_linkBlue'] | .//*[contains(@class, 'Clear')] | //li[@id= 'co_searchHeader_dockItemsClear']");

        private static readonly By ItemsSelectedLocator = By.XPath(".//span[contains(text(),'item')] | .//*[contains(text(),'items selected')] | .//*[contains(text(),'item selected')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectAllComponent"/> class.
        /// </summary>
        /// <param name="containerLocator">
        /// The container.
        /// </param>
        public SelectAllComponent(By containerLocator)
        {
            this.ComponentLocator = containerLocator;
        }

        /// <summary>
        /// Gets the checkbox.
        /// </summary>
        public ICheckBox Checkbox => new CheckBox(this.ComponentLocator, SelectAllCheckboxLocator);

        /// <summary>
        /// Gets the clear link.
        /// </summary>
        public ILink ClearLink => new Link(this.ComponentLocator, ClearSelectedLocator);

        /// <summary>
        /// The selected number label.
        /// </summary>
        public ILabel SelectedNumberLabel => new Label(this.ComponentLocator, ItemsSelectedLocator);

        /// <summary>
        /// Component locator.
        /// </summary>
        protected override By ComponentLocator { get; }
    }
}