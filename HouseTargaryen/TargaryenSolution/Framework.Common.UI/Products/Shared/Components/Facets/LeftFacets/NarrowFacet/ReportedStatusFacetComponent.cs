namespace Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums.Facets;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Reported Status Facet (Filter)
    /// </summary>
    public class ReportedStatusFacetComponent : BaseFacetCheckboxComponent
    {
        private static readonly By ContainerLocator = By.CssSelector("#facet_div_ReportedStatus, #facet_div_reported");

        private EnumPropertyMapper<Reported, WebElementInfo> reportedMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the AwardRange enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<Reported, WebElementInfo> ReportedMap
            => this.reportedMap = this.reportedMap ?? EnumPropertyModelCache.GetMap<Reported, WebElementInfo>();

        /// <summary>
        /// Get count from reported checkbox
        /// </summary>
        /// <param name="option">The option.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetCheckboxCount(Reported option)
            => this.GetCheckboxCount(this.ReportedMap[option].LocatorString);

        /// <summary>
        /// Verify that the given facet is of the checkbox type under the Reported Status section
        /// </summary>
        /// <param name="option">The option.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCheckbox(Reported option)
            => this.IsCheckbox(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.ReportedMap[option].LocatorString)));

        /// <summary>
        /// Gets whether the given facet is checked under the Treatment Status section
        /// </summary>
        /// <param name="option">The option.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCheckboxSelected(Reported option)
            => this.IsCheckboxSelected(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.ReportedMap[option].LocatorString)));

        /// <summary>
        /// Gets whether the given facet is checked under the Treatment Status section
        /// </summary>
        /// <param name="option">The option.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCheckboxDisplayed(Reported option)
            => this.IsCheckboxDisplayed(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.ReportedMap[option].LocatorString)));

        /// <summary>
        /// Apply the specified checkbox
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="option">The option.</param>
        /// <param name="setTo">setTo</param>
        /// <returns>The new instance of T page.</returns>
        public T SetCheckbox<T>(Reported option, bool setTo = true) where T : ICreatablePageObject
            => this.SetCheckbox<T>(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.ReportedMap[option].LocatorString)), setTo);
    }
}