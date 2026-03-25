namespace Framework.Common.UI.Products.WestlawEdgePremium.Pages.RelatedInfo
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.Toolbar;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.Toolbar.FooterToolbar;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.CitedWith;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Base page for Cited With and Cociting pages
    /// </summary>
    public abstract class BaseCoCitationsPage : EdgeTabPage
    {
        private static readonly By CoCitedItemsLocator = By.XPath(".//li[@class = 'ResultItem']");
        private static readonly By ContainerLocator = By.XPath("//div[@id = 'co_pageContainer']");
        private static readonly By ItemsSelectedLabelLocator = By.XPath(".//div[contains(@class, 'SelectItem-nbrSelected')]");
        private static readonly By SelectAllCheckBoxLocator = By.XPath(".//label[text() = 'Select all items' ]/input");
        private static readonly By ClearSelectedLinkLocator = By.XPath(".//button[text() = 'Clear selected']");
        private static readonly By SelectAllAdditionalItemsInfoBoxLocator = By.XPath("//div[contains(@class, 'co_infoBox')][.//button[@id='GetAllAdditionalItemsButton']]");

        /// <summary>
        /// Toolbar components
        /// </summary>
        public new CoCitesToolbarComponent Toolbar { get; set; } = new CoCitesToolbarComponent(ContainerLocator);

        /// <summary>
        /// List of co-cite items
        /// </summary>
        public ItemsCollection<CoCitationsItem> CoCiteList => new ItemsCollection<CoCitationsItem>(this.Container, CoCitedItemsLocator);
        
        /// <summary>
        /// Footer toolbar
        /// </summary>
        public CoCitesFooterToolbarComponent FooterToolbar => new CoCitesFooterToolbarComponent(ContainerLocator);

        #region Select all items and select additinal items functionality 

        /// <summary>
        /// Items Selected label
        /// </summary>
        public new ILabel ItemsSelectedLabel => new Label(this.Container, ItemsSelectedLabelLocator);

        /// <summary>
        /// Select all checkbox
        /// </summary>
        public new ICheckBox SelectAllResultsCheckBox => new CheckBox(this.Container, SelectAllCheckBoxLocator);

        /// <summary>
        /// Clear selected link
        /// </summary>
        public new ILink ClearAllCheckboxesLink => new Link(this.Container, ClearSelectedLinkLocator);

        /// <summary>
        /// Select all additional items info-box
        /// </summary>
        public IInfoBoxWithLink SelectAllAdditionalItemsInfoBox => new InfoBoxWithLink(SelectAllAdditionalItemsInfoBoxLocator);

        #endregion
        
        /// <summary>
        /// Container locator
        /// </summary>
        protected IWebElement Container => DriverExtensions.WaitForElement(ContainerLocator);
    }
}