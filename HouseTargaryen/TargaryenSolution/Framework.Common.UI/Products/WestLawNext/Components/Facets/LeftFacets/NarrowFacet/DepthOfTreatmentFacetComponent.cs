namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Describe Treatment Facet Component
    /// </summary>
    public class DepthOfTreatmentFacetComponent : BaseFacetCheckboxComponent
    {
        private static readonly By DotBarLocator = By.XPath("./*[contains(@id, 'coid_relatedInfo_dotBar_')]");

        private static readonly By ContainerLocator = By.Id("facet_div_DepthOfTreatment");

        private EnumPropertyMapper<DepthOfTreatmentOptions, WebElementInfo> depthOfTreatmentOptions;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the AwardRange enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<DepthOfTreatmentOptions, WebElementInfo> DepthOfTreatmentOptions
            => this.depthOfTreatmentOptions = this.depthOfTreatmentOptions ?? EnumPropertyModelCache.GetMap<DepthOfTreatmentOptions, WebElementInfo>();

        /// <summary>
        /// Get the count of dots
        /// </summary>
        /// <param name="option">DepthOfTreatmentOption</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetBarDotsCount(DepthOfTreatmentOptions option)
        {
            string attribute = DriverExtensions.GetAttribute("id", By.XPath(this.DepthOfTreatmentOptions[option].LocatorString), DotBarLocator);
            return attribute.Substring(attribute.Length - 1).ConvertCountToInt();
        }

        /// <summary>
        /// Get the count of facet results
        /// </summary>
        /// <param name="option">DepthOfTreatmentOption</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetCheckboxCount(DepthOfTreatmentOptions option)
            => this.GetCheckboxCount(this.DepthOfTreatmentOptions[option].LocatorString);

        /// <summary>
        /// Apply the specified checkbox
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="option">The option.</param>
        /// <param name="setTo">setTo</param>
        /// <returns>The new instance of T page.</returns>
        public T SetCheckbox<T>(DepthOfTreatmentOptions option, bool setTo = true) where T : ICreatablePageObject
            => this.SetCheckbox<T>(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.DepthOfTreatmentOptions[option].LocatorString)), setTo);

        /// <summary>
        /// Gets whether the given facet is checked under the Treatment Status section
        /// </summary>
        /// <param name="option">The label text of the checkbox</param>
        /// <returns>True if the facet is a checkbox</returns>
        public bool IsCheckboxSelected(DepthOfTreatmentOptions option)
            => this.IsCheckboxSelected(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.DepthOfTreatmentOptions[option].LocatorString)));
    }
}