namespace Framework.Common.UI.Products.WestlawEdge.Components.History
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.FolderHistory;
    using Framework.Common.UI.Products.Shared.Components.Toolbar.FooterToolbar;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Elements;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Graphical history tab component
    /// </summary>
    public class GraphicalHistoryTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='historySubTabsMainContent']//a/span[text() = 'Graphical view']");
        private static readonly By FilterPanelLocator = By.XPath("//div[@id = 'co_leftColumn']");
        private static readonly By FilterToggleLocator = By.XPath(".//button[@id = 'co_collapseActionLeft']");
        private static readonly By GraphicalHeaderLocator = By.XPath("//*[@id='coid_graphicalHistorySubHeader']/h1[text()='History: Graphical view']");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Graphical history";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Left panel component
        /// </summary>
        public LeftHistoryComponent FilterPanelComponent { get; } = new LeftHistoryComponent();

        /// <summary>
        /// Filter panel toggle
        /// </summary>
        public IToggle FilterPanelToggle => new ToggleWithText(DriverExtensions.GetElement(FilterPanelLocator), FilterToggleLocator, "Collapse filters");

        /// <summary>
        /// Gets the Graphical history Grid
        /// </summary>
        public GraphicalHistoryGridComponent GraphicalGrid { get; } = new GraphicalHistoryGridComponent();

        /// <summary>
        /// Gets the page footer.
        /// </summary>
        public FooterToolbarComponent FooterToolbar { get; } = new FooterToolbarComponent();

        /// <summary>
        ///  Header label on graphical page
        /// </summary>
        public ILabel HeaderLabel => new Label(GraphicalHeaderLocator);
    }
}
