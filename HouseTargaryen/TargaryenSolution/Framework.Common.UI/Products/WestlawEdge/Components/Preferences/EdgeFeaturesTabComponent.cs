namespace Framework.Common.UI.Products.WestlawEdge.Components.Preferences
{
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Preferences;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Features Tab Component
    /// </summary>
    public class EdgeFeaturesTabComponent : BaseTabComponent
    {
        private static readonly By CheckboxLocator = By.XPath(".//input[@type='checkbox']");

        private static readonly By ContainerLocator = By.Id("panel_Feature");

        private static readonly By FeatureDescriptionLocator = By.XPath(".//span[2]");

        private static readonly By FeatureTitleLocator = By.XPath(".//span[1]");

        private EnumPropertyMapper<EdgeFeaturesTab, WebElementInfo> featureMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Features";

        /// <summary>
        /// Tool Mapper
        /// </summary>
        protected EnumPropertyMapper<EdgeFeaturesTab, WebElementInfo> FeatureMap =>
            this.featureMap = this.featureMap ?? EnumPropertyModelCache.GetMap<EdgeFeaturesTab, WebElementInfo>(
                                  string.Empty,
                                  @"Resources/EnumPropertyMaps/WestlawEdge/Preferences");

        /// <summary>
        /// Sets desired feature to given value
        /// </summary>
        /// <param name="feature">The name of the feature</param>
        /// <param name="value">true or false state of the checkbox</param>
        /// <returns>Features tab component</returns>
        public EdgeFeaturesTabComponent SetFeature(EdgeFeaturesTab feature, bool value)
        {
            DriverExtensions.GetElement(By.XPath(this.FeatureMap[feature].LocatorString), CheckboxLocator).SetCheckbox(value);
            return this;
        }

        /// <summary>
        /// The get feature description.
        /// </summary>
        /// <param name="feature">
        /// The feature.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetFeatureDescription(EdgeFeaturesTab feature) =>
             DriverExtensions.GetElement(By.XPath(this.FeatureMap[feature].LocatorString), FeatureDescriptionLocator).GetText();

        /// <summary>
        /// Verify if feature description is displayed.
        /// </summary>
        /// <param name="feature">
        /// The feature title.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public bool IsFeatureDescriptionDisplayed(EdgeFeaturesTab feature) =>
            DriverExtensions.IsDisplayed(By.XPath(this.FeatureMap[feature].LocatorString), FeatureDescriptionLocator);

        /// <summary>
        /// Checks is checkbox displayed
        /// </summary>
        /// <param name="feature">The name of the feature</param>
        /// <returns>True if checkbox displayed</returns>
        public bool IsFeatureTitleDisplayed(EdgeFeaturesTab feature) =>
            DriverExtensions.IsDisplayed(By.XPath(this.FeatureMap[feature].LocatorString), FeatureTitleLocator);

        /// <summary>
        /// Checks is checkbox displayed
        /// </summary>
        /// <param name="feature">The name of the feature</param>
        /// <returns>True if checkbox displayed</returns>
        public bool IsCheckboxDisplayed(EdgeFeaturesTab feature) =>
            DriverExtensions.IsDisplayed(By.XPath(this.FeatureMap[feature].LocatorString), CheckboxLocator);

        /// <summary>
        /// Checks is checkbox displayed
        /// </summary>
        /// <param name="feature">The name of the feature</param>
        /// <returns>True if checkbox displayed</returns>
        public bool IsCheckboxSelected(EdgeFeaturesTab feature) =>
            DriverExtensions.GetElement(By.XPath(this.FeatureMap[feature].LocatorString), CheckboxLocator).Selected;
    }
}
