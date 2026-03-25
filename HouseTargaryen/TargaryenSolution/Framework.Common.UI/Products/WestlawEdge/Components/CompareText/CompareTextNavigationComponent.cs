namespace Framework.Common.UI.Products.WestlawEdge.Components.CompareText
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Navigation component
    /// </summary>
    public class CompareTextNavigationComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath(".//li[@class='co_statutesCompare_navigation']");
        private static readonly By NextArrowLocator = By.XPath(".//button[@class = 'co_next co_tbButton']");
        private static readonly By PreviousArrowLocator = By.XPath(".//button[@class = 'co_prev co_tbButton']");
        private static readonly By NavigationComponentTextLocator = By.XPath(".//span[@class ='co_totalNavigationElements']");
        private static readonly By DeletionsAdditionsCountsLabelLocator = By.XPath("//li[@class = 'co_statutesCompare_count']");

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareTextNavigationComponent"/> class. 
        /// Constructor
        /// </summary>
        /// <param name="contentComponentLocator">
        /// </param>
        public CompareTextNavigationComponent(By contentComponentLocator)
        {
            this.ComponentLocator = new ByChained(contentComponentLocator, ContainerLocator);
        }

        /// <summary>
        /// Next arrow button
        /// </summary>
        public IButton NextArrowButton => new Button(this.ComponentLocator, NextArrowLocator);

        /// <summary>
        /// Previous arrow button
        /// </summary>
        public IButton PreviousArrowButton => new Button(this.ComponentLocator, PreviousArrowLocator);

        /// <summary>
        /// Text label
        /// </summary>
        public ILabel TextLabel => new Label(this.ComponentLocator, NavigationComponentTextLocator);

        /// <summary>
        /// Deletions and Additions counts
        /// </summary>
        public ILabel DeletionsAdditionsCountsLabel => new Label(DeletionsAdditionsCountsLabelLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator { get; }
    }
}
