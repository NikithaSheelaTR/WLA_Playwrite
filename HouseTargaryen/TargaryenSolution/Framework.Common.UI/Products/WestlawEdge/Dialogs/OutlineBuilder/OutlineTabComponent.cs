namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.OutlineBuilder
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// Outlines Tab Panel
    /// </summary>
    public class OutlineTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("panel_outlineTab");
        private static readonly By OutlineCancelButtonLocator = By.CssSelector("button.OutlineBuilderModalCancel");
        private static readonly By OutlineTitleBackButtonLocator = By.CssSelector("button.OutlineModal-backButton");
        private static readonly By MoveThisTextUpButtonLocator = By.XPath("//span[contains(@class, 'icon_upCaretSm')]/..");
        private static readonly By MoveThisTextDownButtonLocator = By.XPath("//span[contains(@class, 'icon_downCaretSm')]/..");
        private static readonly By OutlineMovableTextLocator = By.CssSelector("span.OutlineBuilderModal-movableText");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Outline";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;        

        /// <summary>
        /// Cancel Outline modal button
        /// </summary>
        public IButton OutlineModalCancelButton => new Button(this.ComponentLocator, OutlineCancelButtonLocator);

        /// <summary>
        /// Back to List of Outlines button
        /// </summary>
        public IButton BackToListOfOutlinesButton => new Button(this.ComponentLocator, OutlineTitleBackButtonLocator);

        /// <summary>
        /// Move selected text up in current Outline button
        /// </summary>
        public IButton MoveBlockOfTextUpButton => new CustomEdgeButton(this.ComponentLocator, MoveThisTextUpButtonLocator);

        /// <summary>
        /// Move selected text down in current Outline button
        /// </summary>
        public IButton MoveBlockOfTextDownButton => new CustomEdgeButton(this.ComponentLocator, MoveThisTextDownButtonLocator);

        /// <summary>
        /// Add to Outline movable text
        /// </summary>
        public ILabel OutlineMovableText => new Label(this.ComponentLocator, OutlineMovableTextLocator);
    }
}
