namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Enums.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// HeadnoteTopicsFacet
    /// </summary>
    public class TreatmentStatusFacetComponent : BaseFacetCheckboxComponent
    {
        private static readonly By ContainerLocator = By.Id("facet_div_TreatmentStatus");

        private EnumPropertyMapper<TreatmentStatus, WebElementInfo> treatmentStatus;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the AwardRange enumeration to WebElementInfo map.
        /// </summary>
        private EnumPropertyMapper<TreatmentStatus, WebElementInfo> TreatmentStatus
            => this.treatmentStatus = this.treatmentStatus ?? EnumPropertyModelCache.GetMap<TreatmentStatus, WebElementInfo>();

        /// <summary>
        /// GetCheckboxCount
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetCheckboxCount(TreatmentStatus status)
            => this.GetCheckboxCount(this.TreatmentStatus[status].LocatorString);

        /// <summary>
        /// Verify that the given facet is of the checkbox type
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCheckbox(TreatmentStatus status)
            => this.IsCheckbox(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.TreatmentStatus[status].LocatorString)));

        /// <summary>
        /// IsCheckboxSelected
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCheckboxSelected(TreatmentStatus status)
            => this.IsCheckboxSelected(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.TreatmentStatus[status].LocatorString)));

        /// <summary>
        /// Apply the specified checkbox
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="status">The status.</param>
        /// <param name="setTo">setTo</param>
        /// <returns>The new instance of T page.</returns>
        public T SetCheckbox<T>(TreatmentStatus status, bool setTo = true) where T : ICreatablePageObject
            => this.SetCheckbox<T>(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.TreatmentStatus[status].LocatorString)), setTo);
    }
}