namespace Framework.Common.UI.Products.WestlawEdge.Components.RightPanel.OutlineBuilder
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;

    /// <summary>
    /// Class for Edge Outline Builder components
    /// </summary>
    public class OutlineBuilderGenericComponent : BaseEdgeRightPanelComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class='DocumentPanel-featureSelect']");
        private static readonly By OutlinesButtonLocator = By.XPath(".//button[@id='co_outlineBuilderPanel-select']");

        /// <summary> 
        /// Outline Builder Right Panel component 
        /// </summary>
        public OutlineBuilderRightPanelComponent OutlineBuilderRightPanel { get; } = new OutlineBuilderRightPanelComponent();

        /// <summary> 
        /// Outline Builder Full Page mode component 
        /// </summary>
        public OutlineBuilderFullPageComponent OutlineBuilderFullPagePanel { get; } = new OutlineBuilderFullPageComponent();

        /// <summary> 
        /// Outline Builder internal Right Panel component 
        /// </summary>
        public OutlineInternalRightPanelComponent OutlineInternalRightPanel { get; } = new OutlineInternalRightPanelComponent();

        /// <summary> 
        /// Outline Builder internal Full Page mode component 
        /// </summary>
        public OutlineInternalFullPageComponent OutlineInternalFullPagePanel { get; } = new OutlineInternalFullPageComponent();

        /// <summary>
        /// Outline Builder button in right panel.
        /// </summary>
        public IButton OutlinesPanelButton => new Button(this.ComponentLocator, OutlinesButtonLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
