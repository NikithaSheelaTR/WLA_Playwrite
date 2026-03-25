namespace Framework.Common.UI.Products.WestlawEdge.Components.Toolbar
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.RI;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Toolbars;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// The notes of decisions toolbar.
    /// </summary>
    public class NotesOfDecisionsToolbarComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("co_docToolbar");

        private static readonly By NotesOfDecisionsSortByDropDownLocator = By.XPath("//select[@name='coid_relatedInfo_NOD_SortElement']");

        private EnumPropertyMapper<NotesOfDecisionsToolbarElements, WebElementInfo> toolbarMap;

        /// <summary>
        /// Notes Of Decisions Custom SortBy Dropdown.
        /// </summary>
        public CustomSortByDropdown SortBy { get; } = new CustomSortByDropdown(
            "Edge",
            @"Resources/EnumPropertyMaps/WestlawEdge/Toolbars");

        /// <summary>
        /// Delivery Widget
        /// </summary>
        public DeliveryDropdown DeliveryDropdown { get; } = new DeliveryDropdown();

        /// <summary>
        /// Notes Of Decisions SortBy Dropdown.
        /// </summary>
        public IDropdown<NodSortByOptions> SortDropdown { get; } = new Dropdown<NodSortByOptions>(NotesOfDecisionsSortByDropDownLocator);

        /// <summary>
        /// Toolbar Options Map
        /// </summary>
        protected EnumPropertyMapper<NotesOfDecisionsToolbarElements, WebElementInfo> ToolbarMap =>
            this.toolbarMap = this.toolbarMap
                              ?? EnumPropertyModelCache.GetMap<NotesOfDecisionsToolbarElements, WebElementInfo>(
                                  string.Empty,
                                  @"Resources/EnumPropertyMaps/WestlawEdge/Toolbars");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
        
        /// <summary>
        /// Click on the toolbar button.
        /// </summary>
        /// <typeparam name="T">Page returned from clicking the button.</typeparam>
        /// <param name="toolbarElement">Element to click on the toolbar.</param>
        /// <returns>The expected page.</returns>
        public T ClickToolbarElement<T>(NotesOfDecisionsToolbarElements toolbarElement) where T : ICreatablePageObject
        {
            this.ClickToolbarElement(toolbarElement);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on the toolbar button.
        /// </summary>
        /// <param name="toolbarElement"> Toolbar option to click. </param>
        public void ClickToolbarElement(NotesOfDecisionsToolbarElements toolbarElement)
        {
            By elementToClick = By.XPath(this.ToolbarMap[toolbarElement].LocatorString);
            if (!DriverExtensions.WaitForElement(elementToClick).IsElementInView())
            {
                DriverExtensions.ScrollTo(elementToClick);
            }

            DriverExtensions.GetElement(elementToClick).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Verify that toolbar option is displayed
        /// </summary>
        /// <param name="toolbarElement"> Option to verify </param>
        /// <returns> True if the toolbar option is visible, false otherwise </returns>
        public bool IsToolbarElementDisplayed(NotesOfDecisionsToolbarElements toolbarElement)
            => DriverExtensions.IsDisplayed(By.XPath(this.ToolbarMap[toolbarElement].LocatorString), 5);
    }
}
