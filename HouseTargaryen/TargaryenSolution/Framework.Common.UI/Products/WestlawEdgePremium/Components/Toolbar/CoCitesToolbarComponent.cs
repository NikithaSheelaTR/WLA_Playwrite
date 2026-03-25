namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.Toolbar
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;
    using Framework.Common.UI.Products.WestlawEdgePremium.DropDowns.RiTab;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Toolbars;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Co-cites toolbar component
    /// </summary>
    public class CoCitesToolbarComponent : EdgeToolbarComponent
    {
        private static readonly By DetailDropdownLocator = By.XPath(".//div[@class= 'ToolbarSection-item'][.//button[contains(@aria-label, 'Detail Level:')]]");

        private static readonly By TitleLocator = By.XPath(" //div[@class='ToolbarSection-item']/h3");

        /// <summary>
        /// Initializes a new instance of the <see cref="CoCitesToolbarComponent"/> class. 
        /// </summary>
        /// <param name="containerLocator">
        /// container locator
        /// </param>
        public CoCitesToolbarComponent(By containerLocator) => this.ComponentLocator = containerLocator;

        /// <summary>
        /// Detail Slider Component
        /// </summary>
        public new DetailDropdown DetailDropdown => new DetailDropdown(this.ComponentLocator, DetailDropdownLocator);

        /// <summary>
        /// Delivery dropdown
        /// </summary>
        public new DeliveryDropdown DeliveryDropdown => new DeliveryDropdown(this.ComponentLocator);

        /// <summary>
        /// Sort dropdown
        /// </summary>
        public new CoCitesSortDropdown SortBy => new CoCitesSortDropdown();

        /// <summary>
        /// Title
        /// </summary>
        public ILabel Title => new Label(TitleLocator);

        /// <summary>
        /// Get count from title
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// Count of co-cites cases</returns>
        public int GetCountFromTitle() => this.Title.Text.RetrieveCountFromBrackets();

        /// <summary>
        /// Click on the toolbar button.
        /// </summary>
        /// <param name="toolbarElement"> Toolbar option to click. </param>
        public override void ClickToolbarElement(EdgeToolbarElements toolbarElement)
        {
            DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.ToolbarMap[toolbarElement].LocatorString.Insert(0, "."))).CustomClick();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator { get; }
    }
}