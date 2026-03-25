namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Precision Features Included component
    /// </summary>
    public class PrecisionFeaturesIncludedComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class = 'Athens-features-wrapper']");
        private static readonly By HeaderLocator = By.XPath(".//h2[@class='Athens-features-heading']");
        private static readonly By LabsWidgetLinkDescriptionLocator = By.XPath("./following-sibling::p");

        private const string InfoLabelLctMask = "//*[text()='{0}']/ancestor::div[@class='Athens-features-card']//div[@class='Athens-features-card-contentContainer']";
        private const string WidgetLinkLctMask = "//*[@class='Athens-features-card']//*[text()='{0}']";
        private const string LabsWidgetLinkLctMask = "//*[contains(@class,'SkillCard-module__cardContainer')]//div[text()='{0}']";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

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
        /// Get Description of a WL Labs widget item
        /// </summary>
        /// /// <param name="itemTitle"> The widget title text</param>
        /// <returns>The widget description</returns>
        public ILink GetLabsWidgetTextByTitle(string itemTitle)
            => new Link(By.XPath(string.Format(LabsWidgetLinkLctMask, itemTitle)), LabsWidgetLinkDescriptionLocator);

        /// <summary>
        /// Get Link of a WL Labs widget item
        /// </summary>
        /// /// <param name="widgetTitle"> The widget title text</param>
        /// <returns>The widget link</returns>
        public ILink GetLabsWidgetLinkByTitle(string widgetTitle)
            => new Link(By.XPath(string.Format(LabsWidgetLinkLctMask, widgetTitle)));
    }
}
