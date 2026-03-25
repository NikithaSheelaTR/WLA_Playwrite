namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components;

    using OpenQA.Selenium;

    /// <summary>
    /// This class models the keep list dialog.
    /// </summary>
    public class PrecisionViewKeepListDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//*[@class='Panel-right Panel-keepList']");
        private static readonly By RemoveButtonLocator = By.XPath(".//*[@class='icon25 icon_trash-blue']");
        private static readonly By HeadingLabelLocator = By.ClassName("Panel-heading");
        private static readonly By CloseDialogLocator = By.ClassName("Panel-close");
        private static readonly By SelectedItemsLabelLocator = By.ClassName("co_navItemsSelected");
        private static readonly By ClearSelectedItemsButtonLocator = By.ClassName("co_linkBlue");
        private static readonly By SelectAllItemsCheckboxLocator = By.Id("KeepList-selectAllItems");
        private static readonly By InfoBoxMessageLocator = By.XPath("./ancestor::div/following-sibling::*[contains(@class, 'error') or contains(@class, 'success')]//*[@class='co_infoBox_message']");
        private static readonly By FolderSelectedButtonLocator = By.XPath(".//button[contains(text(), 'Folder selected')]");
        private static readonly By FolderMessageLabelLocator = By.XPath("//*[@class='co_foldering_popupMessageContainer']//*[@class='co_infoBox_message']");
        private static readonly By ZeroStateMessageLabelLocator = By.ClassName("KeepList-zeroState-message");
        private static readonly By ZeroStateCloseButtonLocator = By.XPath(".//button[@class='co_primaryBtn' and text()='Close']");
        private static readonly By DropdownLocator = By.XPath(".//div[contains(@id, 'DetailSliderTab')]");

        /// <summary>
        /// Dialog heading label
        /// </summary>
        public ILabel HeadingLabel => new Label(this.ComponentLocator, HeadingLabelLocator);

        /// <summary>
        /// Dialog close button
        /// </summary>
        public IButton CloseButton => new Button(this.ComponentLocator, CloseDialogLocator);

        /// <summary>
        /// Select All items checkbox
        /// </summary>
        public ICheckBox SelectAllItemsCheckBox => new CheckBox(this.ComponentLocator, SelectAllItemsCheckboxLocator);

        /// <summary>
        /// Selected items label
        /// </summary>
        public ILabel SelectedItemsLabel => new Label(this.ComponentLocator, SelectedItemsLabelLocator);

        /// <summary>
        /// Clear selected items button
        /// </summary>
        public IButton ClearSelectedItemsButton => new Button(this.ComponentLocator, ClearSelectedItemsButtonLocator);

        /// <summary>
        /// Remove button
        /// </summary>
        public IButton RemoveButton => new Button(this.ComponentLocator, RemoveButtonLocator);

        /// <summary>
        /// The result list section of the page
        /// </summary>
        public PrecisionKeepListResultListComponent ResultList => new PrecisionKeepListResultListComponent();

        /// <summary>
        /// Athens Keep List Message alert
        /// </summary>
        public ILabel MessageLabel => new Label(this.ComponentLocator, InfoBoxMessageLocator);

        /// <summary>
        /// Folder selected button
        /// </summary>
        public IButton FolderSelectedButton => new Button(this.ComponentLocator, FolderSelectedButtonLocator);

        /// <summary>
        /// Athens Folder message
        /// </summary>
        public ILabel FolderMessageLabel => new Label(FolderMessageLabelLocator);

        /// <summary>
        /// Detail Slider Component
        /// </summary>
        public DetailDropdown DetailDropdown => new DetailDropdown(this.ComponentLocator, DropdownLocator);

        /// <summary>
        /// Athens Zero State label 
        /// </summary>
        public ILabel ZeroStateMessageLabel => new Label(this.ComponentLocator, ZeroStateMessageLabelLocator);

        /// <summary>
        /// Zero state close button
        /// </summary>
        public IButton ZeroStateCloseButton => new Button(this.ComponentLocator, ZeroStateCloseButtonLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected By ComponentLocator => ContainerLocator;
    }
}
