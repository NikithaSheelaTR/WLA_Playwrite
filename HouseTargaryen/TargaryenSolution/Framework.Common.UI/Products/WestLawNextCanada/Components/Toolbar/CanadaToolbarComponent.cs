namespace Framework.Common.UI.Products.WestLawNextCanada.Components.Toolbar
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;
    using Framework.Common.UI.Products.WestLawNextCanada.DropDowns;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Toolbars;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Canada Co-cites toolbar component
    /// </summary>
    public class CanadaToolbarComponent : EdgeToolbarComponent
    {
        private static readonly By DetailDropdownLocator = By.XPath(".//div[@class= 'ToolbarSection-item'][.//button[@aria-label='Detail Level menu']]");
        private static readonly By ProvisionsContextButtonLocator = By.XPath("//li[contains(@id,'co_docToolbarMenuRightChildWidget')]/a");
        private static readonly By CreateWestclipAlertLocator = By.Id("co_search_alertMenuOptionLink_WestClipCanada");

        /// <summary>
        /// Initializes a new instance of the <see cref="CanadaToolbarComponent"/> class. 
        /// </summary>
        /// <param name="containerLocator">
        /// container locator
        /// </param>
        public CanadaToolbarComponent(By containerLocator) => this.ComponentLocator = containerLocator;

        /// <summary>
        /// Detail Slider Component
        /// </summary>
        public new CanadaDetailDropdown DetailDropdown => new CanadaDetailDropdown(this.ComponentLocator, DetailDropdownLocator);

        /// <summary>
        /// Sort by Dropdown 
        /// </summary>
        public new CanadaCoCitesSortDropdown SortBy => new CanadaCoCitesSortDropdown(this.ComponentLocator, DetailDropdownLocator);

        /// <summary>
        /// Creates instance for GoTo dropdown
        /// </summary>
        public new CanadaGoToDropdown GoTo => new CanadaGoToDropdown();

        /// <summary>
        /// Provisions Context Button
        /// </summary>
        public IButton ProvisionsContextButton => new Button(this.ComponentLocator, ProvisionsContextButtonLocator);

        /// <summary>
        /// Click on the Alerts bell on the search result page
        /// </summary>
        /// <typeparam name="T">The Alert page that opens</typeparam>
        /// <returns>the Alert page that opens</returns>
        public T CreateWestClipAlert<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(
                By.XPath(this.ToolbarMap[EdgeToolbarElements.CreateAlertDropdown].LocatorString)).Click();

            DriverExtensions.WaitForElement(CreateWestclipAlertLocator).Click();
            DriverExtensions.WaitForJavaScript();

            return DriverExtensions.CreatePageInstance<T>();
        }
        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator { get; }
    }
}