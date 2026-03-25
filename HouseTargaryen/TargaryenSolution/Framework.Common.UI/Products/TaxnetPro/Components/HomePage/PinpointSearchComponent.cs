namespace Framework.Common.UI.Products.TaxnetPro.Components.HomePage
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.TaxnetPro.Enums.HomePage;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Pinpoint search component from home page
    /// </summary>
    public class PinpointSearchComponent : BaseModuleRegressionComponent
    {
        private const string PinpointSearchOptionLctMask =
            "//select[@id='coid_pinpoint_typeSelect_0']/option[text()='{0}']";
        private static readonly By ContainerLocator = By.XPath("//h3[text()='Pinpoint Search']/parent::div");
        private static readonly By CitationDropdownLocator = By.XPath("//div[@id='coid_pinpoint_controls_0']//select");
        private static readonly By CitationTextLocator = By.Id("coid_pinpoint_search_0");
        private static readonly By SearchButtonLocator = By.Id("coid_pinpoint_searchButton_0");

        private EnumPropertyMapper<PinpointSearchValues, WebElementInfo> pinpointSearchMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Citation Dropdown
        /// </summary>
        public IDropdown<PinpointSearchValues> Citation => new Dropdown<PinpointSearchValues>(CitationDropdownLocator);

        private EnumPropertyMapper<PinpointSearchValues, WebElementInfo> PinpointSearchMap =>
            this.pinpointSearchMap = this.pinpointSearchMap
                                     ?? EnumPropertyModelCache.GetMap<PinpointSearchValues, WebElementInfo>(
                                         string.Empty,
                                         @"Resources/EnumPropertyMaps/TaxnetPro");

        /// <summary>
        /// Selects Pinpoint search value
        /// </summary>
        /// <param name="pinpoint">Pinpoint search to select</param>
        /// <returns>The <see cref="PinpointSearchComponent"/>.</returns>
        public PinpointSearchComponent SelectOption(PinpointSearchValues pinpoint)
        {
            DriverExtensions.WaitForElementDisplayed(
                By.XPath(string.Format(PinpointSearchOptionLctMask, PinpointSearchMap[pinpoint].Text))).Click();
            return this;
        }

        /// <summary>
        /// Fills the search text in search input box
        /// </summary>
        /// <param name="citationText">Text to be searched</param>
        /// <returns>Instance of Pinpoint search component</returns>
        public PinpointSearchComponent SetPinpointSearchText(string citationText)
        {
            DriverExtensions.SetTextField(citationText,CitationTextLocator);
            return this;
        }

        /// <summary>
        /// Clicks on the search button
        /// </summary>
        /// <typeparam name="T">The type of the page</typeparam>
        /// <returns>The instance of the page</returns>
        public T ClickSearchButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.Click(SearchButtonLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
