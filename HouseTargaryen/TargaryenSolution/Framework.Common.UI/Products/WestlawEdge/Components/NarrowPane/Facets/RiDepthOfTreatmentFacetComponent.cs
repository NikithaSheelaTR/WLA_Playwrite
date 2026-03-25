namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Ri DepthOfTreatmentFacetComponent
    /// </summary>
    public class RiDepthOfTreatmentFacetComponent : EdgeBaseFacetComponent
    {
        private static readonly By ContainerLocator
            = By.XPath("//div[@class = 'co_divider co_entry_facet' and .//*[@id='SearchFacetMultipleXBoxes-DepthOfTreatmentHeader']]");

        private static readonly By RiDepthOfTreatmentLableLocator = By.XPath("//span[text() = 'Depth of Treatment']");

        private static readonly By TreatmentCountLocator = By.XPath("//span[@class='SearchFacet-outputTextValue']");

        private static readonly By TreatmentCheckboxLocator = By.XPath("./input[@class='SearchFacet-inputCheckbox']");

        private EnumPropertyMapper<EdgeDepthOfTreatmentOptions, WebElementInfo> edgeDepthOfTreatmentMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the Compartment Type Map
        /// </summary>
        private EnumPropertyMapper<EdgeDepthOfTreatmentOptions, WebElementInfo> EdgeDepthOfTreatmentMap =>
            this.edgeDepthOfTreatmentMap = this.edgeDepthOfTreatmentMap
                                           ?? EnumPropertyModelCache
                                               .GetMap<EdgeDepthOfTreatmentOptions, WebElementInfo>(
                                                   string.Empty,
                                                   @"Resources/EnumPropertyMaps/WestlawEdge");

        /// <summary>
        /// Verify that Depth Of Treatment facet is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, 5);

        /// <summary>
        ///  Verify that Depth Of Treatment label is enabled
        /// </summary>
        /// <returns> True if label is enabled, false otherwise </returns>
        public bool IsLabelEnabled() => DriverExtensions.IsEnabled(RiDepthOfTreatmentLableLocator);

        /// <summary>
        /// Verify that checkbox is enabled
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        /// <returns>
        /// True if checkbox is enabled, false otherwise 
        /// </returns>
        public bool IsCheckboxEnabled(EdgeDepthOfTreatmentOptions option) => DriverExtensions.IsEnabled(By.XPath(this.EdgeDepthOfTreatmentMap[option].LocatorString));

        /// <summary>
        /// Get count
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        /// <returns>
        /// Examined Treatment Count 
        /// </returns>
        public int GetCheckboxCount(EdgeDepthOfTreatmentOptions option) => DriverExtensions.GetElement(By.XPath(this.EdgeDepthOfTreatmentMap[option].LocatorString), TreatmentCountLocator).GetText().ConvertCountToInt();

        /// <summary>
        /// Set Treatment checkbox
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        public void SetCheckbox(EdgeDepthOfTreatmentOptions option)
        {
            DriverExtensions.WaitForElement(By.XPath(this.EdgeDepthOfTreatmentMap[option].LocatorString)).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Determine the number of bars displayed in the given facet checkbox link
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        /// <returns>
        /// Number of  bars displayed
        /// </returns>
        public int GetLinkBars(EdgeDepthOfTreatmentOptions option) =>
         DriverExtensions.GetElement(By.XPath(this.EdgeDepthOfTreatmentMap[option].LocatorString), By.TagName("div")).GetAttribute("class").ConvertCountToInt();

        /// <summary>
        /// Verify that checkbox checked
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        /// <returns>
        /// True if Examined Treatment checkbox is checked, false otherwise 
        /// </returns>
        public bool IsCheckboxChecked(EdgeDepthOfTreatmentOptions option) => DriverExtensions.IsCheckboxSelected(DriverExtensions.GetElement(By.XPath(this.EdgeDepthOfTreatmentMap[option].LocatorString), TreatmentCheckboxLocator));
    }
}