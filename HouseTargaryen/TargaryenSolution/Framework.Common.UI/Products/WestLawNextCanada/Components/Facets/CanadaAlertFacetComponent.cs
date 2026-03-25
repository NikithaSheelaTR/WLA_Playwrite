namespace Framework.Common.UI.Products.WestLawNextCanada.Components.Facets
{
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNextCanada.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Canada Alert Facet Component.
    /// </summary>
    public class CanadaAlertFacetComponent : AlertFacetComponent
    {
        private const string AlertTypeOptionsLctMask = "//div[@id = 'coid_alertType']//a[text() = '{0}']";
        private const string AlertFilterOptionsLctMask = "//h4[@class = 'co_facet_header' and text() = '{0}']";

        private static readonly By WestlawAlertCenterLocator = By.Id("coid_wlnLinkOut");

        private EnumPropertyMapper<CanadaAlertTypeToolbar, WebElementInfo> alertTypeToolbarMap;

        /// <summary>
        /// Gets the CanadaAlertTypeToolbar enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<CanadaAlertTypeToolbar, WebElementInfo> CanadaAlertTypeToolbarMap
            => this.alertTypeToolbarMap = this.alertTypeToolbarMap ?? EnumPropertyModelCache.GetMap<CanadaAlertTypeToolbar, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawNextCanada");

        /// <summary>
        /// Click on specified Alert type
        /// </summary>
        /// <param name="alertTypeToolbar">Alert Type</param>
        public void ClickOnAlertType(CanadaAlertTypeToolbar alertTypeToolbar)
        {
            DriverExtensions.GetElement(By.Id(this.CanadaAlertTypeToolbarMap[alertTypeToolbar].Id), By.TagName("button")).Click();
            DriverExtensions.WaitForAnimation();
            DriverExtensions.WaitForPageLoad();
        }

        /// <summary>
        /// Get number of alerts present in specified alert type
        /// </summary>
        /// <param name="alertTypeToolbar">Alert Type</param>
        /// <returns> Number of alerts </returns>
        public int GetNumberOfAlertsByType(CanadaAlertTypeToolbar alertTypeToolbar)
        {
            return DriverExtensions.GetElement(By.Id(this.CanadaAlertTypeToolbarMap[alertTypeToolbar].Id), By.XPath("./span")).Text.ConvertCountToInt();
        }

        /// <summary>
        /// Get Westlaw Alert Center text.
        /// </summary>
        /// <returns>Returns Westlaw Alert Center text</returns>
        public string GetWestlawAlertCenterText() => DriverExtensions.GetText(WestlawAlertCenterLocator);

        /// <summary>
        /// Checks if specified Alert Filter option is displayed.
        /// </summary>
        /// <param name="filerOption"> Filter option </param>>
        /// <returns>Returns true if Alert Filter option displayed</returns>
        public bool IsAlertFilterOptionDisplayed(string filerOption) => 
            DriverExtensions.IsDisplayed(By.XPath(string.Format(AlertFilterOptionsLctMask, filerOption)));

        /// <summary>
        /// Checks if specified Alert Type option is displayed.
        /// </summary>
        /// <param name="alertTypeToolbar"> Alert Type option </param>>
        /// <returns>Returns true if Alert Type option displayed</returns>
        public bool IsAlertTypeOptionDisplayed(CanadaAlertTypeToolbar alertTypeToolbar) =>
            DriverExtensions.IsDisplayed(By.XPath(string.Format(AlertTypeOptionsLctMask, this.CanadaAlertTypeToolbarMap[alertTypeToolbar].Text)));
    }
}
