namespace Framework.Common.UI.Products.Shared.Components.Document
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;

    /// <summary>
    /// The Application information  component.
    /// </summary>
    public class ApplicationInfoComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.CssSelector(".globalIP-Document>.a11yExpandBox");

        private static readonly By InfoBoxLocator = By.XPath(".//div[contains(@class,'a11yExpandBox-content')]");

        private static readonly By InfoBoxHeaderLocator = By.XPath(".//span[@id='co_app_info']");

        /// <summary>
        /// The  Application information component.
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// InfoBox  
        /// </summary>
        public ILabel InfoBoxLabel => new Label(ContainerLocator, InfoBoxLocator);

        /// <summary>
        /// Header label 
        /// </summary>
        public ILabel HeaderLabel => new Label(ContainerLocator, InfoBoxHeaderLocator);
    }
}
