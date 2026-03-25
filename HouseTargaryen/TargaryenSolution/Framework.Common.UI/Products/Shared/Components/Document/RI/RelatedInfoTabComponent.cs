namespace Framework.Common.UI.Products.Shared.Components.Document.RI
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Enums.RI;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// RelatedInfo Tabs
    /// </summary>
    public class RelatedInfoTabComponent : BaseModuleRegressionComponent
    {
        private const string TabItemTmpl =
            "//ul[contains(@class,'Tab-list') or @class='co_tabs']//li[contains(text(), '{0}') or ./h2[./a[contains(@class, 'co_tabLink')] and .//*[contains(text(),'{0}')]]]";

        private static readonly By ActiveTabLocator =
            By.XPath("//ul[@id='co_docPrimaryTabNavigation']//li[contains(@class, 'co_tabLeft') and not(contains(@class,'Inactive'))]");

        private static readonly By TabLocator =
            By.XPath("//ul[@id='co_docPrimaryTabNavigation']//h2[@class='co_tabRight']");

        private static readonly By SelectedTabLocator =
            By.XPath("//ul[@id='co_docPrimaryTabNavigation']//li[contains(@class, 'co_tabActive')]//a");

        private static readonly By TabDropDownAllElements =
            By.XPath("//ul[contains(@class,'a11yDropdown-menu') and contains(@style,'block')]//a");

        private static readonly By TabDropDownArrow = By.ClassName("a11yDropdown-button");

        private static readonly By TabDropDownDisableElements =
            By.XPath("//ul[contains(@class,'a11yDropdown-menu') and contains(@style,'block')]//a[@aria-disabled='true']");

        private static readonly By TabDropDownEnableElements =
            By.XPath("//ul[contains(@class,'a11yDropdown-menu') and contains(@style,'block')]//a[@aria-disabled='false']");

        private static readonly By ContainerLocator = By.Id("co_docPrimaryTabNavigationContainer");

        private static readonly By PreviousNavigationArrowLocator = By.XPath("//button[contains(@class, 'DocumentTabScrollButton--previous')]");

        private static readonly By NextNavigationArrowLocator = By.XPath("//button[contains(@class, 'DocumentTabScrollButton--next')]");
        
        /// <summary>
        /// Next navigation arrow
        /// </summary>
        public IButton NextNavigationArrow => new Button(NextNavigationArrowLocator);

        /// <summary>
        /// Previous navigation arrow
        /// </summary>
        public IButton PreviousNavigationArrow => new Button(PreviousNavigationArrowLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the tabs Map.
        /// </summary>
        private EnumPropertyMapper<RiTab, WebElementInfo> TabsMap
            => EnumPropertyModelCache.GetMap<RiTab, WebElementInfo>();

        /// <summary>
        /// Click on tab
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="tab"> Tab to click on. </param>
        /// <param name="subcategoryOfTab">Additional parameter. The subcategory from dropdown list of specified tab.</param>
        /// <returns> New instance of the page </returns>
        public T ClickTab<T>(RiTab tab, string subcategoryOfTab = "") where T : ICreatablePageObject
        {
            DriverExtensions.WaitForJavaScript();
            IWebElement elemToClickOn = this.GetTabWebElement(tab, subcategoryOfTab);
            elemToClickOn.Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get all active tabs
        /// </summary>
        /// <returns>Active tabs</returns>
        public List<RiTab> GetAllActiveTabs() =>
            DriverExtensions.GetElements(ActiveTabLocator)
                            .Select(elem => elem.Text.RetainText().GetEnumValueByText<RiTab>())
                            .ToList();

        /// <summary>
        /// Get all tabs
        /// </summary>
        /// <returns>All tabs</returns>
        public List<RiTab> GetAllTabs() =>
            DriverExtensions.GetElements(TabLocator)
                            .Select(elem => elem.Text.RetainText().GetEnumValueByText<RiTab>())
                            .ToList();

        /// <summary>
        /// Returns the tab count.
        /// </summary> 
        /// <param name="tab"> Tab to look for </param>
        /// <returns> results count on the tab </returns>
        public int GetCountFromTab(RiTab tab) => this.GetTabWebElement(tab).Text.RetrieveCountFromBrackets();

        /// <summary>
        /// Return the selected tab.
        /// </summary>
        /// <returns> Selected tab</returns>
        public RiTab GetSelectedTab()
            => DriverExtensions.WaitForElement(SelectedTabLocator).Text.RetainText().GetEnumValueByText<RiTab>();

        /// <summary>
        /// Gets all dropdown options of specified tab as <see cref="IDictionary{TKey,TValue}"/> collection. Key is option text, value is count from brackets.
        /// If there is no brackets in option text the value is -1. 
        /// </summary>
        /// <param name="tab"> The tab. </param>
        /// <returns> The <see cref="KeyValuePair{TKey,TValue}"/> collection of dropdown options. </returns>
        public IDictionary<string, int> GetTabAllDropdownOptions(RiTab tab)
            => this.GetTabDropdownOptionsBySpecifiedLocator(tab, TabDropDownAllElements);

        /// <summary>
        /// Gets disabled dropdown options of specified tab as <see cref="IDictionary{TKey,TValue}"/> collection. Key is option text, value is count from brackets.
        /// If there is no brackets in option text the value is -1. 
        /// </summary>
        /// <param name="tab"> The tab. </param>
        /// <returns> The <see cref="KeyValuePair{TKey,TValue}"/> collection of dropdown options. </returns>
        public IDictionary<string, int> GetTabDisabledDropdownOptions(RiTab tab)
            => this.GetTabDropdownOptionsBySpecifiedLocator(tab, TabDropDownDisableElements);

        /// <summary>
        /// Gets enabled dropdown options of specified tab as <see cref="IDictionary{TKey,TValue}"/> collection. Key is option text, value is count from brackets.
        /// If there is no brackets in option text the value is -1. 
        /// </summary>
        /// <param name="tab"> The tab. </param>
        /// <returns> The <see cref="KeyValuePair{TKey,TValue}"/> collection of dropdown options. </returns>
        public IDictionary<string, int> GetTabEnabledDropdownOptions(RiTab tab)
            => this.GetTabDropdownOptionsBySpecifiedLocator(tab, TabDropDownEnableElements);

        /// <summary>
        /// Checks the presence of specified tabs.
        /// </summary>
        /// <param name="tabs">The tabs to check</param>
        /// <returns>True if specified tabs are presented, false if they aren't.</returns>
        public bool IsTabArrowDisplayed(params RiTab[] tabs)
            => tabs.All(tab => DriverExtensions.GetElement(this.GetTabWebElement(tab), TabDropDownArrow).Displayed);

        /// <summary>
        /// Checks the presence of specified tabs.
        /// </summary>
        /// <param name="tabs">The tabs to check</param>
        /// <returns>True if specified tabs are presented, false if they aren't.</returns>
        public bool IsTabDisplayed(params RiTab[] tabs) => tabs.All(tab => this.GetTabWebElement(tab).IsDisplayed());

        /// <summary>
        /// Gets all dropdown options of specified tab as <see cref="IDictionary{TKey,TValue}"/> collection. Key is option text, value is count from brackets.
        /// If there is no brackets in option text the value is -1. 
        /// </summary>
        /// <param name="tab"> The tab. </param>
        /// <param name="locator">The locator to select.</param>
        /// <returns> The <see cref="KeyValuePair{TKey,TValue}"/> collection of dropdown options. </returns>
        private IDictionary<string, int> GetTabDropdownOptionsBySpecifiedLocator(RiTab tab, By locator)
        {
            DriverExtensions.GetElement(this.GetTabWebElement(tab), TabDropDownArrow).Hover();
            DriverExtensions.WaitForElementDisplayed(locator);
            IWebElement tabEl = this.GetTabWebElement(tab);
            return DriverExtensions.GetElements(tabEl, locator)
                                   .ToDictionary(
                                       option => option.Text.RetainText(),
                                       option => option.Text.RetrieveCountFromBrackets());
        }

        /// <summary>
        /// The get tab web element.
        /// </summary>
        /// <param name="tab"> The tab. </param>
        /// <param name="subcategoryOfTab"> The subcategory of tab. </param>
        /// <returns> The <see cref="IWebElement"/>. </returns>
        private IWebElement GetTabWebElement(RiTab tab, string subcategoryOfTab = "")
        {
            string expectedTabText = this.TabsMap[tab].Text;
            string tabXPath = string.Format(TabItemTmpl, expectedTabText);

            IWebElement elementToClick =
                DriverExtensions.GetElements(By.XPath(tabXPath), el => el.Text.RetainText().Equals(expectedTabText))
                                .FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(subcategoryOfTab))
            {
                DriverExtensions.GetElement(elementToClick, TabDropDownArrow).Hover();
                elementToClick =
                    DriverExtensions.GetElements(By.XPath(tabXPath), el => el.Text.RetainText().Equals(expectedTabText))
                                    .First();

                DriverExtensions.WaitForElement(elementToClick, TabDropDownEnableElements);
                elementToClick =
                    DriverExtensions.GetElements(
                        elementToClick,
                        TabDropDownEnableElements,
                        el => el.Text.RetainText().Equals(subcategoryOfTab)).First();
            }

            return elementToClick;
        }
    }
}