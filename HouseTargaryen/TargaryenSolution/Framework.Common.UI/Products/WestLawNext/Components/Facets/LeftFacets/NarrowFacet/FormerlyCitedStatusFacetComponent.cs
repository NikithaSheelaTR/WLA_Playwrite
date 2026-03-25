namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Describe Formerly Cited Status Facet Component
    /// </summary>
    public class FormerlyCitedStatusFacetComponent : BaseFacetCheckboxComponent
    {
        private static readonly By ContainerLocator = By.Id("facet_div_FormerlyCitedStatus");

        private EnumPropertyMapper<CitedStatus, WebElementInfo> citedStatusMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        private EnumPropertyMapper<CitedStatus, WebElementInfo> CitedStatusMap =>
            this.citedStatusMap = this.citedStatusMap ?? EnumPropertyModelCache.GetMap<CitedStatus, WebElementInfo>();

        /// <summary>
        /// Verify that the given facet is of the checkbox type under the Reported Status section
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCheckbox(CitedStatus status)
            => this.IsCheckbox(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.CitedStatusMap[status].LocatorString)));

        /// <summary>
        /// Verify that the given facet is of the checkbox type under the Reported Status section
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCheckboxSelected(CitedStatus status)
            => this.IsCheckboxSelected(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.CitedStatusMap[status].LocatorString)));

        /// <summary>
        /// Verify that the given facet is of the checkbox type under the Reported Status section
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public int GetCheckboxCount(CitedStatus status) => this.GetCheckboxCount(this.CitedStatusMap[status].LocatorString);

        /// <summary>
        /// GetCheckboxText
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetCheckboxText(CitedStatus status)
            => this.GetCheckboxText(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.CitedStatusMap[status].LocatorString)));

        /// <summary>
        /// Apply the specified checkbox
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="status">The status.</param>
        /// <param name="setTo">setTo</param>
        /// <returns>The new instance of T page.</returns>
        public T SetCheckbox<T>(CitedStatus status, bool setTo = true) where T : ICreatablePageObject
            => this.SetCheckbox<T>(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.CitedStatusMap[status].LocatorString)), setTo);
    }
}