namespace Framework.Common.UI.Products.Shared.Components.Toolbar
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Enums.Alerts;
    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Components.EdgeResponsive;
    using Framework.Common.UI.Products.WestLawNext.Components.SecondarySources;
    using Framework.Common.UI.Products.WestLawNext.Components.Toolbar;
    using Framework.Common.UI.Products.WestLawNext.Enums.Toolbars;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;
    using System.Threading;

    /// <summary>
    /// Toolbar that contains a number of different widgets.
    /// </summary>
    public class Toolbar : BaseModuleRegressionComponent
    {
        private static readonly By ToolbarContainerLocator = By.XPath("//div[@id='co_docToolbar']");
        private static readonly By ViewTypeDropdownLocator = By.XPath(".//select[@class='co_RI_ViewBy']");
        private static readonly By KebabMenuLocator = By.XPath(".//div[@id='co_docToolbarMenuResponsive']//span[contains(text(),'Open Tools') or contains(text(),'Open tools') or contains(text(),'Actions')]");
        private static readonly By FilterButtonLocator = By.XPath(".//div[@id='co_docToolbarMenuResponsive']//span[contains(text(),'Filter')]");
        private static readonly By ToolTipLabelLocator = By.XPath("//div[contains(@class,'a11yTooltip-content') and @aria-hidden='false']/div[2]");
        private const string ToolTipMessageLocator = "//div[@role='tooltip' and @aria-hidden='false' and @id='{0}']";

        /// <summary>
        /// Alphabetical navigation bar component 
        /// </summary>
        public AlphabeticalNavigationBarComponent AlphabeticalNavigationBarComponent { get; } = new AlphabeticalNavigationBarComponent();

        /// <summary>
        /// This is the instance of the Annotations widget we will use in the toolbar
        /// </summary>
        public AnnotationsDropdown AnnotationsDropdown { get; protected set; } = new AnnotationsDropdown();

        /// <summary>
        /// Custom Sort By Dropdown
        /// </summary>
        /// This is a temporary fix as sort by options didn't get changed in all the places, in future it might get changed, then we can use this new sortby class 
        public virtual CustomSortByDropdown CustomSortBy { get; } = new CustomSortByDropdown();

        /// <summary>
        /// Delivery Widget
        /// </summary>
        public DeliveryDropdown DeliveryDropdown { get; } = new DeliveryDropdown();

        /// <summary>
        /// Detail Slider Component
        /// </summary>
        public DetailDropdown DetailDropdown { get; set; } = new DetailDropdown();

        /// <summary>
        /// Alert result view dropdown
        /// </summary>
        public AlertResultViewDropdown AlertResultViewDropdown { get; set; } = new AlertResultViewDropdown();

        /// <summary>
        /// This is the instance of the GoTo we will use in the toolbar
        /// </summary>
        public GoToDropdown GoTo { get; protected set; } = new GoToDropdown();

        /// <summary>
        /// GraphicComponent Component
        /// </summary>
        public GraphicComponent GraphicComponent { get; } = new GraphicComponent();

        /// <summary>
        /// History Facet
        /// </summary>
        public HistoryDropdown HistoryDropdown { get; } = new HistoryDropdown();

        /// <summary>
        /// This is the instance of the NavigationComponent we will use in the toolbar
        /// </summary>
        public NavigationComponent NavigationComponent { get; protected set; } = new NavigationComponent();

        /// <summary>
        /// Pagination Navigator Component
        /// </summary>
        public PageNavigatorComponent PageNavigator { get; } = new PageNavigatorComponent();

        /// <summary>
        /// Sort By Dropdown
        /// </summary>
        public virtual SortByDropdown SortBy { get; } = new SortByDropdown();

        /// <summary>
        /// Accessibility Sort By Dropdown
        /// </summary>
        public AccessibilitySortByDropdown AccessibilitySortBy { get; } = new AccessibilitySortByDropdown();

        /// <summary>
        /// This is the instance of the CopyMenuOption 
        /// </summary>
        public CopyMenuDropdown CopyMenu { get; } = new CopyMenuDropdown();

        /// <summary>
        /// Citing Reference Sort By Dropdown
        /// </summary>
        public CitingReferenceSortByDropdown CitingReferenceSortBy { get; set; } = new CitingReferenceSortByDropdown();

        /// <summary>
        /// Term Navigation Component
        /// </summary>
        public TermNavigationComponent TermNavigation { get; } = new TermNavigationComponent();

        /// <summary>
        /// This is the instance of the Table of Content widget we will use in the toolbar
        /// </summary>
        public TableOfContentsComponent TocComponent { get; protected set; } = new TableOfContentsComponent();

        /// <summary>
        /// View Type DropDown
        /// </summary>
        public IDropdown<ViewTypeDropdownOptions> ViewType { get; } = new Dropdown<ViewTypeDropdownOptions>(ViewTypeDropdownLocator);

        /// <summary>
        /// Kebab menu button for displaying right panel
        /// </summary>
        public IButton KebabMenuButton => new Button(KebabMenuLocator);

        /// <summary>
        /// Filter button for displaying left panel
        /// </summary>
        public IButton FilterButton => new Button(FilterButtonLocator);

        /// <summary>
        /// Get toolbar buttons by element name
        /// </summary>
        /// <param name="toolbarElement">the Alert Type</param>
        public IButton ToolBarByNameButton(ToolbarElements toolbarElement) => new Button(By.XPath(this.ToolbarMap[toolbarElement].LocatorString));

        /// <summary>
        /// Get toolbar element tooltip text
        /// </summary>
        public ILabel ToolTipLabel => new Label(ToolTipLabelLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ToolbarContainerLocator;

        /// <summary>
        /// Toolbar Options Map
        /// </summary>
        protected EnumPropertyMapper<ToolbarElements, WebElementInfo> ToolbarMap
        => EnumPropertyModelCache.GetMap<ToolbarElements, WebElementInfo>();

        /// <summary>
        /// Gets the AlertType enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<AlertType, WebElementInfo> AlertTypeMap
            => EnumPropertyModelCache.GetMap<AlertType, WebElementInfo>("Toolbar");

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
        /// Click on the Alerts bell on the search result page
        /// </summary>
        /// <typeparam name="T">The Alert page that opens</typeparam>
        /// <param name="alertType">the Alert Type</param>
        /// <returns>the Alert page that opens</returns>
        public T CreateAlert<T>(AlertType alertType) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(
                By.XPath(this.ToolbarMap[ToolbarElements.CreateAlertDropdown].LocatorString)).Click();

            DriverExtensions.Click(By.Id(this.AlertTypeMap[alertType].Id));
            DriverExtensions.WaitForJavaScript();

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on the Alerts bell on the document result page
        /// </summary>
        /// <param name="alertType">the alert type</param>
        /// <typeparam name="T">The Alert page that opens</typeparam>
        /// <returns>Page for alert creation</returns>
        public T CreateDealTrackAlert<T>(AlertType alertType) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(
                By.XPath(this.ToolbarMap[ToolbarElements.CreateDealTrackAlert].LocatorString)).Click();

            DriverExtensions.Click(By.Id(this.AlertTypeMap[alertType].Id));
            DriverExtensions.WaitForJavaScript();

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Return the count found in the category label on the toolbar
        /// </summary>
        /// <returns> Count found in the toolbar header</returns>
        public int GetCategoryHeaderCount()
            => this.GetToolbarElementText(ToolbarElements.CategoryLabel).RetrieveCountFromBrackets();

        /// <summary>
        /// History count from the toolbar header
        /// </summary>
        /// <returns> History count </returns>
        public int GetHistoryHeaderCount()
            => this.GetToolbarElementText(ToolbarElements.History).RetrieveCountFromBrackets();

        /// <summary>
        /// Get Toolbar option's text
        /// </summary>
        /// <param name="toolbarElement"> Toolbar option </param>
        /// <returns> Toolbar option text </returns>
        public string GetToolbarElementText(ToolbarElements toolbarElement)
            => DriverExtensions.GetText(By.XPath(this.ToolbarMap[toolbarElement].LocatorString));

        /// <summary>
        /// Gets Toolbar immediate text
        /// </summary>
        /// <param name="toolbarElement"> Toolbar option </param>
        /// <returns> Toolbar immediate option text </returns>
        public string GetToolbarImmediateElementText(ToolbarElements toolbarElement)
            => DriverExtensions.GetImmediateText(By.XPath(this.ToolbarMap[toolbarElement].LocatorString));

        /// <summary>
        /// Get Toolbar Option Title
        /// </summary>
        /// <param name="toolbarElement"> toolbar option </param>
        /// <returns> option's title </returns>
        public string GetToolbarElementTitle(ToolbarElements toolbarElement)
            =>
                DriverExtensions.WaitForElement(By.XPath(this.ToolbarMap[toolbarElement].LocatorString))
                                .GetAttribute("title");

        /// <summary>
        /// Verify that toolbar option is displayed
        /// </summary>
        /// <param name="toolbarElement"> Option to verify </param>
        /// <returns> True if the toolbar option is visible, false otherwise </returns>
        public bool IsToolbarElementDisplayed(ToolbarElements toolbarElement)
            => DriverExtensions.IsDisplayed(By.XPath(this.ToolbarMap[toolbarElement].LocatorString), 5);

        /// <summary>
        /// Click Kebab Menu button to display right panel
        /// </summary>
        /// <returns>New instance of ToolsRightPanelComponent.</returns>
        public ToolsRightPanelComponent ClickKebabMenuButton()
        {
            this.KebabMenuButton.Click();
            return new ToolsRightPanelComponent();
        }

        /// <summary>
        /// Click Filter button to display left panel
        /// </summary>
        /// <returns>New instance of ContentTypesLeftPanelComponent.</returns>
        public ContentTypesLeftPanelComponent ClickFilterButton()
        {
            this.FilterButton.Click();
            return new ContentTypesLeftPanelComponent();
        }

        /// <summary>
        /// Get Toolbar Option tooltip
        /// </summary>
        /// <param name="toolbarElement"></param>
        /// <returns></returns>
        public string GetToolTipElementText(ToolbarElements toolbarElement)
        {
            string toolTipElementId = DriverExtensions.WaitForElement(By.XPath(this.ToolbarMap[toolbarElement].LocatorString))
                                .GetAttribute("aria-describedby");
            DriverExtensions.WaitForElement(By.XPath(this.ToolbarMap[toolbarElement].LocatorString)).Hover();
            return DriverExtensions.WaitForElement(By.XPath(string.Format(ToolTipMessageLocator, toolTipElementId))).GetText();
        }
    }
}