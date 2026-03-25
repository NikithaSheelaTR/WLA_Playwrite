namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;

    using OpenQA.Selenium;

    /// <summary>
    /// Toolbar component on the document toolbar
    /// </summary>
    public class QuickCheckToolbarComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("co_docToolbarSubmitToQuickCheckWidget");

        private static readonly By QuickCheckButtonLocator = By.XPath(".//button");

        private static readonly By DropdownArrowLocator = By.XPath(".//a");

        /// <summary>
        /// Current component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Quick check button return AnalyzeWithQuickCheckDialog
        /// </summary>
        public IButton QuickCheckButton => new Button(this.ComponentLocator, QuickCheckButtonLocator);

        /// <summary>
        /// Drop down arrow
        /// Click on this link will expand SubmitToQuickCheckMenu
        /// </summary>
        public ILink DropDownArrow => new Link(this.ComponentLocator, DropdownArrowLocator);
    }
}
