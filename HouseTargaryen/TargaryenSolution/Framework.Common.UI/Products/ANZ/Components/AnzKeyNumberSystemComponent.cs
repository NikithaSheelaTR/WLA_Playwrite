namespace Framework.Common.UI.Products.ANZ.Components
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using OpenQA.Selenium;

    /// <summary>
    /// Key Number System component
    /// </summary>
    public class AnzKeyNumberSystemComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='co_mainContainer']//*[@class='Access-point Access-keyNumbers']");

        private static readonly By KeyNumberSystemLinkLocator = By.XPath(".//a[@class='Access-point-link']");

        /// <summary>
        /// Key Number System Link
        /// </summary>
        public ILink KeyNumberSystemLink => new Link(this.ComponentLocator, KeyNumberSystemLinkLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;        
    }
}
