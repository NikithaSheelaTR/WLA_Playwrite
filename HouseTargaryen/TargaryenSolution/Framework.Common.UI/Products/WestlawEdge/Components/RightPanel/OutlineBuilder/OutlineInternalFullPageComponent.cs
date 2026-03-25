namespace Framework.Common.UI.Products.WestlawEdge.Components.RightPanel.OutlineBuilder
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;    
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium.Support.PageObjects;
    using OpenQA.Selenium;

    /// <summary>
    /// Outline's internal Full Page mode component
    /// </summary>
    public class OutlineInternalFullPageComponent : BaseOutlineInternalComponent
    {
        private static readonly By OutlineBuilderContainerLocator = By.XPath("//div[@id='co_body']");
        private static readonly By CancelHeadingButtonLocator = By.CssSelector("div.OutlineBuilder-editingContainer button.cancelNode");
        private static readonly By CancelHeadingDeleteButtonLocator = By.CssSelector("button#OutlineBuilder-cancelDeleteButton");
        private static readonly By PanelCollapseStatusLocator = By.CssSelector("button.OutlineBuilder-togglePanelButton span");

        /// <summary>
        /// Cancel Note or Heading editing button
        /// </summary>
        public IButton CancelHeadingButton => new Button(this.ComponentLocator, CancelHeadingButtonLocator);

        /// <summary>
        /// Cancel Note or Heading component deletion button
        /// </summary>
        public IButton CancelDeleteHeadingButton => new Button(this.ComponentLocator, CancelHeadingDeleteButtonLocator);

        /// <summary>
        /// Check is Headings Auto-numbering applied
        /// </summary>
        /// <returns> True if auto-numbers are displayed, false otherwise </returns>
        public bool IsAutonumberingApplied() => DriverExtensions.GetElements(this.ComponentLocator,
                By.CssSelector("span.OutlineBuilder-autoNumber")).Count > 0;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => OutlineBuilderContainerLocator;

        /// <summary>
        /// Verify that component is displayed
        /// </summary>
        /// <returns> True if component is displayed, false otherwise </returns>
        public override bool IsDisplayed()
        {
            var element = DriverExtensions.SafeGetElement(new ByChained(ComponentLocator, PanelCollapseStatusLocator));
            return element != null && !element.Text.Equals("Expand outlines");
        }
    }
}
