namespace Framework.Common.UI.Products.WestlawEdge.Components.RightPanel
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Toggles;
    using Framework.Common.UI.Products.WestlawEdge.DropDowns;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Base class for Edge Right panel components
    /// </summary>
    public abstract class BaseEdgeRightPanelComponent : BaseModuleRegressionComponent
    {
        private static readonly By ToggleLocator = By.XPath("//div[@id = 'co_collapseButtonRight']");
        private static readonly By RightPanelContainerLocator = By.XPath(".//*[@id='co_rightColumn']");
        private static readonly By ToggleStateLocator = By.XPath(".//a");
        private static readonly By CloseButtonLocator = By.XPath(".//button[@aria-label='Close dialog']");
        private static readonly By HeadingToolsMenuButtonLocator = By.XPath(".//button[contains(@class,'DocumentPanel-headingTools')]");

        /// <summary>
        /// Panel Close button
        /// </summary>
        public IButton CloseButton => new Button(this.ComponentLocator, CloseButtonLocator);

        /// <summary>
        /// Heading Tools menu button
        /// </summary>
        public IButton HeadingToolsMenuButton => new Button(this.ComponentLocator, HeadingToolsMenuButtonLocator);

        /// <summary>
        /// Right panel menu dropdown.
        /// </summary>
        public RightPanelMenuDropdown MenuDropdown => new RightPanelMenuDropdown(new ByChained(RightPanelContainerLocator, this.ComponentLocator));     

        /// <summary>
        /// Expand/Collapse button
        /// </summary>
        public IToggle Toggle { get; } = new Toggle(ToggleLocator, ToggleStateLocator, "aria-expanded", "true");  
    }
}