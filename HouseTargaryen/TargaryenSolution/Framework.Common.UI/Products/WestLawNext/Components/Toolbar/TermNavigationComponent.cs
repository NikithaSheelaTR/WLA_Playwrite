namespace Framework.Common.UI.Products.WestLawNext.Components.Toolbar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.MultipleSearchWithin;
    using Framework.Common.UI.Products.WestLawNext.Enums.Toolbars;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.FocusHighlighting;
    using Framework.Common.UI.Raw.WestlawEdge.Models.FocusHighlighting;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// TermNavigationComponent inside Toolbar
    /// TODO: Create new dropdown for search term navigation options
    /// </summary>
    public class TermNavigationComponent : BaseModuleRegressionComponent
    {
        private static readonly By NextTermArrowLocator = By.Id("co_documentFooterSearchTermNavigationNext");

        private static readonly By TooltipValueLocator = By.XPath("//div[contains(@class,'a11yTooltip-content') and @aria-hidden='false']");

        private static readonly By PreviousTermArrowLocator = By.Id("co_documentFooterSearchTermNavigationPrevious");

        private static readonly By ContainerLocator = By.Id("co_docToolbarTermNavigation");

        private static readonly By TermNavigationDropDownLocator = By.XPath("//span[contains(@id,'TermNavigationDropdownMenu-buttonText')] | //span[@id='co_documentSearchTermNavigationText'] | //button[@class='a11yDropdown-button co_termNav_bestPortion']//span[contains(@class, 'a11yDropdown-buttonText')]");

        private static readonly By TermNavigationSearchTermMenuLocator = By.XPath(".//div[@id='co_searchTermMenu' and not(contains(@class,'co_hideState'))]");

        private static readonly By TermNavigationWidgetDropDownTabArrowLocator = By.XPath(".//*[@id='co_termNavigationWidgetDropDownTabArrow'] | //button[@aria-controls='co_searchTermMenuDropDown']");

        private static readonly By SelectedOptionLocator = By.XPath("//span[@id = 'co_documentSearchTermNavigationText'] | //li[contains(@class,'a11yDropdown-item_selected')]//span[starts-with(@class,'a11yDropdown-itemText co_termNav_')]");

        private static readonly By FocusTermsLocator = By.XPath("//a[@class = 'co_termNav_FocusHighlighting']//div[contains(@class, 'Multi')]");

        private static readonly By OptionLocator = By.XPath("//li[not(@class = 'co_hideState')]/a[contains(@id, 'coid_termNav')] | //div[@class='co_dropdownBoxContentRight']/ul/li/a[contains(@class,'CoCitesTerm')] | //*[@class='a11yDropdown-menu a11yDropdownContent-container']//ul/li[not(contains(@class,'co_hideState'))]//span[contains(@class,'co_termNav_')] | //*[@id='co_searchTermMenuDropDown']//ul/li[not(contains(@class,'co_hideState'))]//span[contains(@class,'co_termNav_')]");
        
        private EnumPropertyMapper<SearchTermNavigationOption, WebElementInfo> termOptions;

        /// <summary>
        /// The term color map.
        /// </summary>
        private EnumPropertyMapper<TermColors, FocusHighlightingTermInfo> termColorMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// TermOptions dropdown
        /// </summary>
        private EnumPropertyMapper<SearchTermNavigationOption, WebElementInfo> TermOptions
            => this.termOptions = this.termOptions ?? EnumPropertyModelCache.GetMap<SearchTermNavigationOption, WebElementInfo>();

        /// <summary>
        /// Gets the TermColors enumeration to FocusHighlightingTermInfo map.
        /// </summary>
        protected EnumPropertyMapper<TermColors, FocusHighlightingTermInfo> TermColorMap =>
            this.termColorMap = this.termColorMap
                                ?? EnumPropertyModelCache.GetMap<TermColors, FocusHighlightingTermInfo>(
                                    string.Empty,
                                    @"Resources/EnumPropertyMaps/WestlawEdge/FocusHighlighting");

        /// <summary>
        /// Term navigation options
        /// </summary>
        public List<SearchTermNavigationOption> Options
        {
            get
            {
                List<string> elementsClasses = DriverExtensions.GetElements(OptionLocator)
                             .Select(e => e.GetAttribute("class").Replace("co_selected", "").Trim()).ToList();

                return elementsClasses.Select(x => x.GetEnumValueByPropertyModel<SearchTermNavigationOption, WebElementInfo>(el => el.ClassName)).ToList();
            }
        }

        /// <summary>
        /// Term navigation options
        /// </summary>
        public List<SearchTermNavigationOption> TermNavigationOptions
        {
            get
            {
                List<string> elementsClasses = DriverExtensions.GetElements(OptionLocator)
    .            Select(e => e.GetAttribute("class")
                .Split(' ')
                .FirstOrDefault(c => c.StartsWith("co_termNav_")))
                .Where(c => !string.IsNullOrEmpty(c))
                .ToList();

                return elementsClasses
                    .Select(x => x.GetEnumValueByPropertyModel<SearchTermNavigationOption, WebElementInfo>(el => el.ClassName))
                    .ToList();
            }
        }

        /// <summary>
        /// Clicks NextTermArrow
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>T page</returns>
        public T ClickNextTermArrowButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(NextTermArrowLocator).CustomClick();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks PreviousTermArrow
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>T page</returns>
        public T ClickPreviousTermArrowButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(PreviousTermArrowLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click Desired Term Navigation
        /// </summary>
        /// <param name="option">SearchTermNavigationOption</param>
        public void ClickTermNavigationOption(SearchTermNavigationOption option)
            => DriverExtensions.WaitForElement(By.XPath(this.TermOptions[option].LocatorString)).Click();

        /// <summary>
        /// Click Search Within option in Term Navigation drop-down
        /// </summary>
        /// <returns>Search Within Flyout dialog</returns>
        public SearchWithinFlyoutDialog ClickTermNavigationOptionSearchWithin()
        {
            DriverExtensions.WaitForElement(By.XPath(this.TermOptions[SearchTermNavigationOption.SearchWithin].LocatorString)).Click();
            return new SearchWithinFlyoutDialog();
        }  

        /// <summary>
        /// Expands dropdown widget
        /// </summary>
        public void ClickToExpandTermNavigationDropdown()
        {
            if (DriverExtensions.IsDisplayed(TermNavigationWidgetDropDownTabArrowLocator))
            {
                DriverExtensions.Click(TermNavigationWidgetDropDownTabArrowLocator);
            }
        }

        /// <summary>
        /// Get selected navigation option
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetSelectedNavigationOption() => DriverExtensions.GetText(TermNavigationDropDownLocator);

        /// <summary>
        /// Get Term Option Text
        /// Note: drop-down should be expanded first.
        /// </summary>
        /// <param name="optionToGetTextFrom">Enum to map with webelementInfo</param>
        /// <returns>Term option text</returns>
        public string GetTermOptionText(SearchTermNavigationOption optionToGetTextFrom) =>
            DriverExtensions.GetText(By.XPath(this.TermOptions[optionToGetTextFrom].LocatorString));

        /// <summary>
        /// Gets title of the next term arrow
        /// To get title of the next term arrow, it is needed to hover over the arrow
        /// </summary>
        /// <returns>title</returns>
        public string GetTitleOfTheNextTermArrow()
        {
            DriverExtensions.Hover(NextTermArrowLocator);
            return DriverExtensions.WaitForElement(TooltipValueLocator).Text;
        }

        /// <summary>
        /// Gets Title of PreviousTermArrow
        /// To get title of the previous term arrow, it is needed to hover over the arrow
        /// </summary>
        /// <returns>title</returns>
        public string GetTitleOfThePreviousTermArrow()
        {
            DriverExtensions.Hover(PreviousTermArrowLocator);
            return DriverExtensions.WaitForElement(TooltipValueLocator).Text;
        }

        /// <summary>
        /// Checks if the NextTermArrow is displayed
        /// </summary>
        /// <returns>Whether or not the NextTermArrow is displayed</returns>
        public bool IsNextTermArrowDisplayed() =>
            DriverExtensions.IsDisplayed(NextTermArrowLocator)
            && !DriverExtensions.GetAttribute("class", NextTermArrowLocator).Contains("co_hideState");

        /// <summary>
        /// Checks if the PreviousTermArrow is displayed
        /// </summary>
        /// <returns>Whether or not the PreviousTermArrow is displayed</returns>
        public bool IsPreviousTermArrowDisplayed() => DriverExtensions.IsDisplayed(PreviousTermArrowLocator);

        /// <summary>
        /// Checks if the NextTermArrow is Enabled
        /// </summary>
        /// <returns>Whether or not the NextTermArrow is enabled</returns>
        public bool IsNextTermArrowEnabled()
            => !DriverExtensions.GetAttribute("class", NextTermArrowLocator).Contains("disabled");

        /// <summary>
        /// Checks if the PreviousTermArrow is Enabled
        /// </summary>
        /// <returns>Whether or not the PreviousTermArrow is enabled</returns>
        public bool IsPreviousTermArrowEnabled()
            => !DriverExtensions.GetAttribute("class", PreviousTermArrowLocator).Contains("disabled");

        /// <summary>
        /// Checks if the TermNavigationComponentContainer is Displayed
        /// </summary>
        /// <returns>Whether or not the TermNavigationComponentContainer is displayed</returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, TermNavigationSearchTermMenuLocator);

        /// <summary>
        /// Checks if Desired Term Navigation option is Displayed
        /// </summary>
        /// <param name="option">The option.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsTermNavigationOptionDisplayed(SearchTermNavigationOption option)
            => DriverExtensions.IsDisplayed(By.XPath(this.TermOptions[option].LocatorString));

        /// <summary>
        /// Get selected term navigation option
        /// </summary>
        /// <returns>Instance of SeachTermNavigation</returns>
        public SearchTermNavigationOption GetSelectedTermNavigationOption()
            => DriverExtensions.GetAttribute("class", SelectedOptionLocator).GetEnumValueByPropertyModel<SearchTermNavigationOption, WebElementInfo>(el => el.ClassName);

        /// <summary>
        /// Get selected term navigation option
        /// </summary>
        /// <returns>Instance of SeachTermNavigation</returns>
        public SearchTermNavigationOption GetSelectedTermNavigationOptionValue()
            => DriverExtensions.GetAttribute("class", SelectedOptionLocator).Split(' ').FirstOrDefault(c => c.StartsWith("co_termNav_")).GetEnumValueByPropertyModel<SearchTermNavigationOption, WebElementInfo>(el => el.ClassName);
        /// <summary>
        /// Get focus terms and corresponding colors 
        /// </summary>
        /// <returns>Dictionary of terms and colors</returns>
        public Dictionary<string, TermColors> GetTermsColors()
            => DriverExtensions.GetElements(FocusTermsLocator).ToDictionary(x => x.Text, y => this.GetColorTypeByCode(y.GetCssValue("background-color")));

        /// <summary>
        /// Get color type by code
        /// </summary>
        /// <param name="termCode">Term code</param>
        /// <returns>Term color</returns>
        private TermColors GetColorTypeByCode(string termCode)
        => Enum.GetValues(typeof(TermColors))
        .Cast<TermColors>()
        .First(color => this.TermColorMap[color].BackgroundColorCode.Equals(termCode));
    }
}