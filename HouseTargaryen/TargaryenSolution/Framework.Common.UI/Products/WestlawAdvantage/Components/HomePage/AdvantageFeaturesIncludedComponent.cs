namespace Framework.Common.UI.Products.WestlawAdvantage.Components.HomePage
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Advantage Features Included component
    /// </summary>
    public class AdvantageFeaturesIncludedComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class = 'Athens-features-wrapper']");
        private const string WidgetLinkLctMask = "//*[@class='Athens-features-card']//*[text()='{0}']";
        private const string WidgeContentLctMask = "//*[@class='Athens-features-card']//p[text()='{0}']";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get Link of a Key Features Inluded widget item
        /// </summary>
        public ILink GetWidgetLinkByTitle(string itemTitle)
            => new Link(By.XPath(string.Format(WidgetLinkLctMask, itemTitle)));

        /// <summary>
        /// Get content of a Feature Inluded widget item
        /// </summary>
        public IWebElement GetWidgetContentElement(string itemContent)
            => DriverExtensions.GetElement(By.XPath(string.Format(WidgeContentLctMask, itemContent)));
    }
}

