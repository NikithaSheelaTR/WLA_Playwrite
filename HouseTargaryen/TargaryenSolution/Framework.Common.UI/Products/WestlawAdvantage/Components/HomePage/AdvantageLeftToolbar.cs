namespace Framework.Common.UI.Products.WestlawAdvantage.Components.HomePage
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawAdvantage.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;


    /// <summary>
    /// Left toolbar
    /// </summary>
    public class AdvantageLeftToolbar : BaseModuleRegressionComponent
    {
        private readonly By ContainerLocator = By.XPath(".//div[contains(@class,'__sideNav')]");
        private static readonly By LogoTextLocator = By.XPath("//div[contains(@class, '__logoTitle')]");
        private static readonly By ExpandCollapsedButtonLocator = By.XPath("//div[contains(@class, '__collapseButtonDivider')]//saf-button");
        private static readonly By CollapseButtonLocator = By.XPath("//saf-button[@aria-label = 'Collapse')]");
        private static readonly By LogoButtonLocator = By.XPath("//button[contains(@class, '__logoSection')]");

        /// <summary>
        /// AdvantageLeftToolbar
        /// </summary>
        public AdvantageLeftToolbar() { }

        /// <summary>
        /// Get ToolsBar Button By Name
        /// </summary>
        /// <returns>IWebElement</returns>
        public IButton GetToolsBarButtonByName(AdvantageLeftToolbarItems tool)
        {
            IWebElement contentTypeElement = DriverExtensions.GetElement(By.CssSelector(this.ToolBarButton[tool].LocatorString));
            var element = (IWebElement)DriverExtensions.ExecuteScript(this.ToolBarButton[tool].LocatorMask, contentTypeElement);
            return new Button(element);
        }

        /// <summary>
        /// Logo text locator
        /// </summary>
        public ILabel LogoTextLabel => new Label(LogoTextLocator);

        /// <summary>
        ///  Tool Bar Button mapper
        /// </summary>
        protected EnumPropertyMapper<AdvantageLeftToolbarItems, WebElementInfo> ToolBarButton =>
            EnumPropertyModelCache.GetMap<AdvantageLeftToolbarItems, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawAdvantage");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Expand Button
        /// </summary>
        public IButton ExpandButtonCollapcedButton => new Button(ExpandCollapsedButtonLocator);

        /// <summary>
        /// Logo button
        /// </summary>
        public IButton LogoButton => new Button(LogoButtonLocator);

    }
}
