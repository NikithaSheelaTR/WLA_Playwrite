namespace Framework.Common.UI.Products.WestlawEdge.Components.RightPanel
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Toggles;
    using Framework.Common.UI.Products.WestlawEdge.Components.RightPanel.OutlineBuilder;
    using OpenQA.Selenium;

    /// <summary>
    /// Class for Edge Right panel components
    /// </summary>
    public class EdgeRightPanelComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id = 'co_rightColumn']");
        private static readonly By NotesButtonLocator = By.XPath(".//button/span[contains(@class, 'annotation')]");
        private static readonly By OutlinesButtonLocator = By.XPath(".//button/span[contains(@class, 'hierarchy')]");
        private static readonly By QuickCheckButtonLocator = By.XPath(".//button/span[contains(@class, 'boxWithCheck')]");
        private static readonly By AiSummaryButtonLocator = By.XPath(".//button[@id = 'co_aiSummaryPanel-select']");
        private static readonly By ToggleLocator = By.XPath("//div[@id = 'co_collapseButtonRight']");
        private static readonly By ToggleStateLocator = By.XPath(".//a");

        /// <summary> 
        /// Notes Right Panel component 
        /// </summary>
        public NotesRightPanelComponent NotesPanel { get; } = new NotesRightPanelComponent();

        /// <summary> 
        /// Edge Outline Builder component 
        /// </summary>
        public OutlineBuilderGenericComponent OutlineBuilderPanel { get; } = new OutlineBuilderGenericComponent();

        /// <summary> 
        /// Quick Check Right Panel component 
        /// </summary>
        public QuickCheckRightPanelComponent QuickCheckPanel { get; } = new QuickCheckRightPanelComponent();

        /// <summary> 
        /// AI Summary Right Panel component 
        /// </summary>
        public AiSummaryRightPanelComponent AiSummaryPanel { get; } = new AiSummaryRightPanelComponent();

        /// <summary>
        /// Notes button in collapsed state.
        /// </summary>
        public IButton NotesPanelButton => new Button(this.ComponentLocator, NotesButtonLocator);

        /// <summary>
        /// Outline Builder button in right panel.
        /// </summary>
        public IButton OutlinesPanelButton => new Button(this.ComponentLocator, OutlinesButtonLocator);

        /// <summary>
        /// Quick Check button in collapsed state.
        /// </summary>
        public IButton QuickCheckPanelButton => new Button(this.ComponentLocator, QuickCheckButtonLocator);

        /// <summary>
        /// AI Summary button in collapsed state.
        /// </summary>
        public IButton AiSummaryPanelButton => new Button(this.ComponentLocator, AiSummaryButtonLocator);

        /// <summary>
        /// Expand/Collapse button
        /// </summary>
        public IToggle Toggle { get; } = new Toggle(ToggleLocator, ToggleStateLocator, "aria-expanded", "true");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}