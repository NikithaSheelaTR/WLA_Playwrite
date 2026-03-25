namespace Framework.Common.UI.Products.WestLawNext.Components.SearchResults
{
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Enums.Toolbars;
    using Framework.Common.UI.Products.WestLawNext.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// document toolbar component for smart folders pages
    /// </summary>
    public class CustomToolbarComponent : BaseModuleRegressionComponent
    {
        private static readonly By ClearSelectionLocator = By.XPath("//a[contains(text(), 'Clear Selection')]");

        private static readonly By SelectAllCheckboxLocator = By.XPath("//input[@type='checkbox' and contains(@id, 'electAll')]");

        private static readonly By SelectedCountLocator = By.XPath("//*[@id='coid_selectedCount' or @class='co_navItemsSelected ng-binding']");

        private static readonly By SearchResultHeaderLocator = By.XPath("//h2[@class='co_search_header']");

        private static readonly By ContainerLocator = By.ClassName("co_browseToolbarWrap");

        private EnumPropertyMapper<ContentType, ContentTypeInfo> contentTypeMap;

        private EnumPropertyMapper<ToolbarElements, WebElementInfo> toolbarMap;

        /// <summary>
        /// Delivery Dropdown
        /// </summary>
        public DeliveryDropdown DeliveryDropdown { get; } = new DeliveryDropdown();

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<ContentType, ContentTypeInfo> ContentTypeMap =>
                    this.contentTypeMap =
                        this.contentTypeMap ?? EnumPropertyModelCache.GetMap<ContentType, ContentTypeInfo>();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Toolbar Options Map
        /// </summary>
        private EnumPropertyMapper<ToolbarElements, WebElementInfo> ToolbarMap
            => this.toolbarMap = this.toolbarMap ?? EnumPropertyModelCache.GetMap<ToolbarElements, WebElementInfo>();

        /// <summary>
        /// Click on the clear selection link.
        /// </summary>
        public void ClickClearSelectionLink() =>
            DriverExtensions.WaitForElement(ClearSelectionLocator).Click();

        /// <summary>
        /// Click on the toolbar button.
        /// </summary>
        /// <typeparam name="T">Page returned from clicking the button.</typeparam>
        /// <param name="toolbarElement">Element to click on the toolbar.</param>
        /// <returns>The expected page.</returns>
        public T ClickToolbarElement<T>(ToolbarElements toolbarElement) where T : ICreatablePageObject
        {
            this.ClickToolbarElement(toolbarElement);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on the toolbar button.
        /// </summary>
        /// <param name="toolbarElement"> Toolbar option to click. </param>
        public void ClickToolbarElement(ToolbarElements toolbarElement)
        {
            DriverExtensions.WaitForJavaScript();
            By elementToClick = By.XPath(this.ToolbarMap[toolbarElement].LocatorString);
            if (!DriverExtensions.WaitForElement(elementToClick).IsElementInView())
            {
                DriverExtensions.ScrollTo(elementToClick);
            }

            DriverExtensions.GetElement(elementToClick).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Gets Selected items value
        /// </summary>
        /// <returns> The <see cref="string"/>.</returns>
        public string GetSelectedItemsValue() => DriverExtensions.GetText(SelectedCountLocator);

        /// <summary>
        /// Returns a list of all the different content type results headers
        /// </summary>
        /// <param name="type">type of the Content</param>
        /// <returns> list of all the different content type results headers </returns>
        public bool IsViewAllLinkDisplayed(ContentType type) => DriverExtensions.GetElements(SearchResultHeaderLocator)
                                                                                .Where(e => e.Text.Contains(this.ContentTypeMap[type].Text))
                                                                                .Select(e => e.Text.Contains("View all")).First();

        /// <summary>
        /// Click on the the select all items checkbox
        /// </summary>
        /// <returns>True if displayed</returns>
        public bool IsSelectAllCheckboxSelected() => DriverExtensions.IsCheckboxSelected(SelectAllCheckboxLocator);

        /// <summary>
        /// Verify that toolbar option is displayed
        /// </summary>
        /// <param name="toolbarElement"> Option to verify </param>
        /// <returns> True if the toolbar option is visible, false otherwise </returns>
        public bool IsToolbarElementDisplayed(ToolbarElements toolbarElement)
            => DriverExtensions.IsDisplayed(By.XPath(this.ToolbarMap[toolbarElement].LocatorString), 5);

        /// <summary>
        /// Click on the the select all items checkbox
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T SelectAllItemsCheckbox<T>() where T : ICreatablePageObject
        {
            DriverExtensions.SetCheckbox(true, SelectAllCheckboxLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}