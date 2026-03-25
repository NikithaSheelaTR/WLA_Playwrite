namespace Framework.Common.UI.Products.WestlawEdge.Components.Toolbar
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    using OpenQA.Selenium;

    /// <summary>
    /// The navigate headings component.
    /// </summary>
    public class NavigateHeadingsComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class = 'DA-HeadingNavigationWidget']");
        private static readonly By NavigateHeadingsButtonLocator = By.XPath(".//button[@title = 'Navigate headings']");
        private static readonly By PreviousHeadingButtonLocator = By.XPath(".//a[@oldtitle = 'Previous heading']");
        private static readonly By NextHeadingButtonLocator = By.XPath(".//a[@oldtitle = 'Next heading']");

        /// <summary>
        /// Gets the component locator.
        /// </summary>
        protected override By ComponentLocator { get; } = ContainerLocator;

        /// <summary>
        /// Previous heading button
        /// </summary>
        public IButton PreviousHeadingButton => new Button(this.ComponentLocator, PreviousHeadingButtonLocator);

        /// <summary>
        /// Next heading button
        /// </summary>
        public IButton NextHeadingButton => new Button(this.ComponentLocator, NextHeadingButtonLocator);

        /// <summary>
        /// Navigate headings button
        /// </summary>
        public IButton NavigateHeadingsButton => new Button(this.ComponentLocator, NavigateHeadingsButtonLocator);
    }
}