namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using Framework.Common.UI.Products.Shared.Components;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    /// <summary>
    /// Toolbar
    /// </summary>
    public class ParallelSearchToolbar : BaseModuleRegressionComponent
    {
        private static readonly By ToolbarContainerLocator = By.XPath("//div[contains(@class,'parallelSearchResultsToolbar')]");
        private static readonly By SelectAllCheckboxLocator = By.XPath(".//saf-checkbox[@id,'parallelSearch-selectAll']");
        private static readonly By SaveToFolderButtonLocator = By.XPath(".//button[contains(@class, 'co_dropdownTitle')]");
        private static readonly By FolderMessageLabelLocator = By.XPath("//*[@class='co_foldering_popupMessageContainer']//*[@class='co_infoBox_message']");

        /// <summary>
        /// Delivery dropdown
        /// </summary>
        public DeliveryDropdown DeliveryDropdown { get; } = new DeliveryDropdown();

        /// <summary>
        /// Select all checkbox
        /// </summary>
        public ICheckBox SelectAllCheckbox => new CheckBox(this.ComponentLocator, SelectAllCheckboxLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ToolbarContainerLocator;

        /// <summary>
        /// Save To Folder Button
        /// </summary>
        public IButton SaveToFolderButton => new Button(this.ComponentLocator, SaveToFolderButtonLocator);

        /// <summary>
        /// Folder message
        /// </summary>
        public ILabel FolderMessageLabel => new Label(FolderMessageLabelLocator);
    }
}

