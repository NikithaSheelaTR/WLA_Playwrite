namespace Framework.Common.UI.Products.WestlawEdge.Components.EdgeResponsive
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;

    /// <summary>
    /// Tools right pane component on document page or search result page
    /// </summary>
    public class ToolsRightPanelComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class='Panel-right']");
        private static readonly By SearchWithinLocator = By.XPath(".//span[contains(text(),'Search within')]");
        private static readonly By TableOfContentsLocator = By.XPath(".//span[contains(text(),'Table of contents')]");
        private static readonly By KeyCiteStatusLocator = By.XPath(".//span[contains(text(),'KeyCite status')]");
        private static readonly By ResultsPerPageLocator = By.XPath(".//span[contains(text(),'Results per page')]");
        private static readonly By SortLocator = By.XPath(".//span[contains(text(),'Sort')]");
        private static readonly By CloseButtonLocator = By.XPath(".//button[contains(text(),'Close')]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Search within button
        /// </summary>
        public IButton SearchWithinButton => new Button(this.ComponentLocator, SearchWithinLocator);

        /// <summary>
        /// Table of contents button
        /// </summary>
        public IButton TableOfContentsButton => new Button(this.ComponentLocator, TableOfContentsLocator);

        /// <summary>
        /// KeyCite status button
        /// </summary>
        public IButton KeyCiteStatusButton => new Button(this.ComponentLocator, KeyCiteStatusLocator);

        /// <summary>
        /// Results per page button
        /// </summary>
        public IButton ResultsPerPageButton => new Button(this.ComponentLocator, ResultsPerPageLocator);

        /// <summary>
        /// Sort button
        /// </summary>
        public IButton SortButton => new Button(this.ComponentLocator, SortLocator);

        /// <summary>
        /// Panel close button
        /// </summary>
        public IButton CloseButton => new Button(this.ComponentLocator, CloseButtonLocator);

        /// <summary>
        /// Click Sort on kebab menu to display Sort submenu right panel
        /// </summary>
        /// <returns>New instance of RightPanelSortComponent.</returns>
        public RightPanelSortComponent ClickSortButton()
        {
            this.SortButton.Click();
            return new RightPanelSortComponent();
        }
    }
}

