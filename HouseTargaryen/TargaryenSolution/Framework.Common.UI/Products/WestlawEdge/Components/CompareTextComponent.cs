namespace Framework.Common.UI.Products.WestlawEdge.Components
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;

    /// <summary>
    /// Compare text component located on most of browse pages in the right panel.
    /// </summary>
    public class CompareTextComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[contains(@id,'coid_website_browseRightColumn_widget')][.//h3[text()='Compare text']]");

        private static readonly By CompareTextButtonLocator = By.XPath(".//button[@id = 'coid_redlineView_launchButton']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the Compare text button
        /// </summary>
        public IButton CompareTextButton { get; } = new Button(ContainerLocator, CompareTextButtonLocator);
    }
}