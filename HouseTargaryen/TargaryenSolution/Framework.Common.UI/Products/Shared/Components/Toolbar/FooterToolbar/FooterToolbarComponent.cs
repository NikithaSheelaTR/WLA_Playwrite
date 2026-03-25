namespace Framework.Common.UI.Products.Shared.Components.Toolbar.FooterToolbar
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Toolbars;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// Toolbar that contains a number of different widgets.
    /// </summary>
    public class FooterToolbarComponent : BaseModuleRegressionComponent
    {
        private const string ResultItemsPerPageDropDownValuesLocator = "//ul[@aria-label='Items per page' or 'Le nombre d’éléments par page']/li//span[text()='{0}']";

        private static readonly By ContainerLocator = By.CssSelector("#co_search_footerToolbar, .co_navFooter");
        private static readonly By ResultItemsPerPageDropDownLocator =
            By.XPath("//select[@id='coid_search_pagination_size_footer' or @ng-model='paginationSelect' or @id='co_researchOrg_detailsTable_resultsPerPage' or @id='coid_graphical_history_pagination_size_footer'] | //div[@class='co_navFooter_itemCount']/select");
        private static readonly By ResultItemsPerPageDropDownButtonLocator = By.XPath("//div[@class='co_navFooter_itemCount']//button[@class='a11yDropdown-button'] | //div[@Id='co_mrFooterOptions']//button[@class='a11yDropdown-button']");
        
        /// <summary>
        /// Pagination component
        /// </summary>
        public PaginationFooterComponent PaginationComponent { get; } = new PaginationFooterComponent();

        /// <summary>
        /// Per Page DropDown
        /// </summary>
        public IDropdown<ResultItemsPerPage> PerPageDropDown { get; } = new Dropdown<ResultItemsPerPage>(ResultItemsPerPageDropDownLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Toolbar Options Map
        /// </summary>
        protected virtual EnumPropertyMapper<EdgeToolbarElements, WebElementInfo> ToolbarMap =>
             EnumPropertyModelCache.GetMap<EdgeToolbarElements, WebElementInfo>(
                                  string.Empty,
                                  @"Resources/EnumPropertyMaps/WestlawEdge/Toolbars");

        /// <summary>
        /// Verify that toolbar option is displayed
        /// </summary>
        /// <param name="toolbarElement"> Option to verify </param>
        /// <returns> True if the toolbar option is visible, false otherwise </returns>
        public bool IsToolbarElementDisplayed(EdgeToolbarElements toolbarElement)
            => DriverExtensions.IsDisplayed(By.XPath(this.ToolbarMap[toolbarElement].LocatorString), 2);

        /// <summary>
        /// Per Page DropDown Button Locator
        /// </summary>
        public IButton PerPageDropDownButton => new Button(this.ComponentLocator, ResultItemsPerPageDropDownButtonLocator);

        /// <summary>
        /// Select value in Per Page DropDown button type
        /// </summary>
        public void SelectPerPageDropDownValue(ResultItemsPerPage value) {
            DriverExtensions.ScrollPageToBottom();
            if (DriverExtensions.IsDisplayed(ResultItemsPerPageDropDownButtonLocator))
            {
                DriverExtensions.ScrollTo(ResultItemsPerPageDropDownButtonLocator);
                if (!PerPageDropDownButton.Text.Equals(value.GetStringValue()))
                {
                    PerPageDropDownButton.Click();
                    DriverExtensions.GetElement(By.XPath(string.Format(ResultItemsPerPageDropDownValuesLocator, value.GetStringValue()))).Click();
                    DriverExtensions.WaitForPageLoad();
                    DriverExtensions.WaitForJavaScript();
                }
            }
        }
    }
}