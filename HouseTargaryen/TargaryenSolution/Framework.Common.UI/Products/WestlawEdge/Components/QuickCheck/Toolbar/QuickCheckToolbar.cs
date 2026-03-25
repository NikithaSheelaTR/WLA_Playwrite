namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.Toolbar
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Enums.QuickCheck;
    using Framework.Common.UI.Products.WestLawNext.Components.Toolbar;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// The document analyzer toolbar.
    /// </summary>
    public class QuickCheckToolbar
    {
        private static readonly By DetailLevelContainer = By.XPath(".//div[@id = 'DA-detailLevelSelectorContainer']");
        private static readonly By UnverifiedCitationsLocator = By.XPath("//button[contains(.,'Unverified citations')]");
        private static readonly By ExpandAllHeadersLinkLocator = By.XPath("//li[@class='DA-Recommendation-ExpandCollapsedHeadings']/*");
        private static readonly By SortQuotationsDropdownLocator = By.XPath("//div[@class='DA-ViewText']//following-sibling::div[@class='a11yDropdown'] | //div[@id='DA-SortSelectorContainer']");

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickCheckToolbar"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public QuickCheckToolbar(IWebElement container)
        {
            this.Container = container;
        }

        /// <summary>
        /// Gets the expand collapsed headings link.
        /// </summary>
        public ILink ExpandCollapsedHeadingsLink => new Link(ExpandAllHeadersLinkLocator);

        /// <summary>
        /// Gets the unverified citations button.
        /// </summary>
        public IButton UnverifiedCitationsButton => new Button(UnverifiedCitationsLocator);

        /// <summary>
        /// Gets the delivery dropdown.
        /// </summary>
        public virtual DeliveryDropdown DeliveryDropdown => new DeliveryDropdown();

        /// <summary>
        /// The detail level dropdown.
        /// </summary>
        public QuickCheckDetailLevelDropdown DetailDropdown =>
            new QuickCheckDetailLevelDropdown(DriverExtensions.WaitForElement(this.Container, DetailLevelContainer));
        
        /// <summary>
        /// The Quick Check sort dropdown.
        /// </summary>
        public QuickCheckSortDropdown QuickCheckSortDropdown =>
            new QuickCheckSortDropdown(DriverExtensions.WaitForElement(this.Container, SortQuotationsDropdownLocator));

        /// <summary>
        /// The pagination navigator component
        /// </summary>
        public PageNavigatorComponent PageNavigator => new PageNavigatorComponent();

        /// <summary>
        /// Toolbar Options Map
        /// </summary>
        private EnumPropertyMapper<QuickCheckToolbarElements, WebElementInfo> ToolbarMap =>
            EnumPropertyModelCache.GetMap<QuickCheckToolbarElements, WebElementInfo>("", @"Resources\EnumPropertyMaps\WestlawEdge\QuickCheck");

        /// <summary>
        /// Gets the container.
        /// </summary>
        protected IWebElement Container { get; }

        /// <summary>
        /// Click on the toolbar button.
        /// </summary>
        /// <typeparam name="T">Page returned from clicking the button.</typeparam>
        /// <param name="toolbarElement">Element to click on the toolbar.</param>
        /// <returns>The expected page.</returns>
        public T ClickToolbarElement<T>(QuickCheckToolbarElements toolbarElement) where T : ICreatablePageObject
        {
            By elementToClick = By.XPath(this.ToolbarMap[toolbarElement].LocatorString);
            if (!DriverExtensions.WaitForElement(this.Container, elementToClick).IsElementInView())
            {
                DriverExtensions.ScrollTo(this.Container, elementToClick);
            }

            // JS is used since temporary CSS issue exists
            DriverExtensions.Click(this.Container, elementToClick);
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Verify that toolbar option is displayed
        /// </summary>
        /// <param name="toolbarElement"> Option to verify </param>
        /// <returns> True if the toolbar option is visible, false otherwise </returns>
        public bool IsToolbarElementDisplayed(QuickCheckToolbarElements toolbarElement) =>
            DriverExtensions.IsDisplayed(By.XPath(this.ToolbarMap[toolbarElement].LocatorString));
    }
}
