namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// Right column header component
    /// </summary>
    public class RightColumnHeaderComponent : BaseModuleRegressionComponent
    {
        private static readonly By HeaderModuleLocator = By.XPath("//div[contains(@class,'RightColumnHeader-module__rightColumnHeaderWrapper')]");
        private static readonly By HeaderQueryLabelLocator = By.XPath(".//h2[contains(@class,'RightColumnHeader-module__rightColumnHeaderQuery')]");
        private static readonly By CloseRightButtonLocator = By.XPath(".//saf-button-v3[@data-testid='close-right-button']");
        private static readonly By ToggleLeftButtonLocator = By.XPath(".//saf-button-v3[@data-testid='toggle-left-button']");

        /// <summary>
        /// Header query label
        /// </summary>
        public ILabel HeaderQueryLabel => new Label(HeaderQueryLabelLocator);

        /// <summary>
        /// Close Right Button
        /// </summary>
        public IButton CloseRightButton => new Button(CloseRightButtonLocator);

        /// <summary>
        /// Toggle Left Button
        /// </summary>
        public IButton ToggleLeftButton => new Button(ToggleLeftButtonLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => HeaderModuleLocator;
    }
}


