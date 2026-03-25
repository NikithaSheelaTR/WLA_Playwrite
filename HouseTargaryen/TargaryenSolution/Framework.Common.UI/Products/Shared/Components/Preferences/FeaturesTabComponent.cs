namespace Framework.Common.UI.Products.Shared.Components.Preferences
{
    using Framework.Common.UI.Products.Shared.Enums.Preferences;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Features Tab Component
    /// </summary>
    public class FeaturesTabComponent : BaseTabComponent
    {
        private const string DescriptionLctMask = "//h3[text()={0}]/following-sibling::div[1]";

        private static readonly By RrHeaderLocator = By.XPath("//div[@id='coid_userSettingsTabPanel5']//div[@id='coid_userSettingsSearchContent']/h3");

        private static readonly By ContainerLocator = By.Id("coid_userSettingsTabPanel5");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Features";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Sets desired feature to given value
        /// </summary>
        /// <param name="feature">The name of the feature</param>
        /// <param name="value">true or false state of the checkbox</param>
        /// <returns>Features tab component</returns>
        public FeaturesTabComponent SetFeature(FeaturesTab feature, bool value)
        {
            IWebElement checkBox = DriverExtensions.WaitForElementDisplayed(By.Id(EnumPropertyModelCache.GetEnumInfo<FeaturesTab, WebElementInfo>(feature).Id));

            checkBox.SetCheckbox(value);
            return this;
        }

        /// <summary>
        /// The get feature description.
        /// </summary>
        /// <param name="featureTitle">
        /// The feature title.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetFeatureDescription(string featureTitle) =>
             DriverExtensions.GetText(SafeXpath.BySafeXpath(DescriptionLctMask, featureTitle));

        /// <summary>
        /// Verify if feature description is displayed.
        /// </summary>
        /// <param name="featureTitle">
        /// The feature title.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public bool IstFeatureDescriptionDisplayed(string featureTitle) =>
            DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(DescriptionLctMask, featureTitle));

        /// <summary>
        /// Gets the text of the feature tab
        /// </summary>
        /// <returns>The text in descriptoin of the feature tab</returns>
        public string GetRrName() => DriverExtensions.GetText(RrHeaderLocator);

        /// <summary>
        /// Checks is checkbox displayed
        /// </summary>
        /// <param name="feature">The name of the feature</param>
        /// <returns>True if checkbox displayed</returns>
        public bool IsCheckboxDisplayed(FeaturesTab feature) =>
            DriverExtensions.IsDisplayed(By.Id(EnumPropertyModelCache.GetEnumInfo<FeaturesTab, WebElementInfo>(feature).Id), 5);

        /// <summary>
        /// Checks is checkbox displayed
        /// </summary>
        /// <param name="feature">The name of the feature</param>
        /// <returns>True if checkbox displayed</returns>
        public bool IsCheckboxSelected(FeaturesTab feature) =>
              DriverExtensions.WaitForElementDisplayed(By.Id(EnumPropertyModelCache.GetEnumInfo<FeaturesTab, WebElementInfo>(feature).Id)).Selected;

        /// <summary>
        /// The get checkbox description.
        /// </summary>
        /// <param name="feature">
        /// The feature.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetCheckboxDescription(FeaturesTab feature) =>
            DriverExtensions.GetElement(By.Id(EnumPropertyModelCache.GetEnumInfo<FeaturesTab, WebElementInfo>(feature).Id)).GetParentElement().Text;
    }
}