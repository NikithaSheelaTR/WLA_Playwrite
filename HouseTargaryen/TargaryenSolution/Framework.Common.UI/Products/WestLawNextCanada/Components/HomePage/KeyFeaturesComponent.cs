namespace Framework.Common.UI.Products.WestLawNextCanada.Components.HomePage
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNextCanada.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// Key Features component
    /// </summary>
    public class KeyFeaturesComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class = 'Athens-features-wrapper']");
        private static readonly By HeaderLocator = By.XPath(".//h2");
        private const string LabelLocator = "//div[@class='Athens-features-card-titleContainer']//h3//*[text()=\"{0}\"]";
        private const string InfoLabelLctMask = "//*[text()=\"{0}\"]/ancestor::div[@class='Athens-features-card']//div[@class='Athens-features-card-contentContainer']";
        private const string WidgetLinkLctMask = "//*[@class='Athens-features-card']//*[text()=\"{0}\"]";

        private EnumPropertyMapper<KeyFeatures, WebElementInfo> keyFeatureMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Key Feature Map
        /// </summary>
        protected EnumPropertyMapper<KeyFeatures, WebElementInfo> KeyFeaturesMap =>
            this.keyFeatureMap = this.keyFeatureMap ?? EnumPropertyModelCache.GetMap<KeyFeatures, WebElementInfo>(
                                      string.Empty,
                                      @"Resources/EnumPropertyMaps/WestlawNextCanada");

        /// <summary>
        ///  Precision Features Included header label
        /// </summary>
        public ILabel HeaderLabel => new Label(this.ComponentLocator, HeaderLocator);

        /// <summary>
        /// Get a text of a Features Inluded widget item
        /// </summary>
        public string GetWidgetTextByTitle(string itemTitle)
            => DriverExtensions.GetText(By.XPath(string.Format(InfoLabelLctMask, itemTitle)));

        /// <summary>
        /// Get Link of a Features Inluded widget item
        /// </summary>
        public ILink GetWidgetLinkByTitle(string itemTitle)
            => new Link(By.XPath(string.Format(WidgetLinkLctMask, itemTitle)));

        /// <summary>
        /// Check if a Features Inluded widget item is displayed
        /// </summary>
        /// <param name="keyFeature">Key feature title to check</param>
        /// <returns>True if present, otherwise false</returns>
        public bool IsWidgetLabelDisplayed(KeyFeatures keyFeature)
            => DriverExtensions.WaitForElement(By.XPath(string.Format(LabelLocator, this.KeyFeaturesMap[keyFeature].Text))).Displayed;
    }
}