namespace Framework.Common.UI.Raw.WestlawEdge.Items.History
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items.FolderHistory;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.WestlawEdge.Elements.Judicial;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    /// <summary>
    /// Graphical history Grid Item for graphical view tab
    /// </summary>
    public class GraphicalHistoryGridItem : BaseGridItem
    {
        private static readonly By ZoomInButtonLocator = By.Id("zoomInButton");
        private static readonly By ZoomOutButtonLocator = By.Id("zoomOutButton");
        private static readonly By CollapseAllButtonLocator = By.XPath(".//button[contains(@class,'GH-Zoom-CollapseAll')]");
        private static readonly By ResetButtonLocator = By.XPath(".//button[contains(@class,'GH-Zoom-Reset')]");
        private static readonly By RotateButtonLocator = By.XPath(".//button[contains(@class,'GH-Zoom-Rotate')]");
        private static readonly By AnchorExpandButtonLocator = By.XPath(".//button[@class='GH-Timeline-Item-toggle']/span[contains(text(),'Show events')]");
        private static readonly By AnchorCloseButtonLocator = By.XPath(".//button[@class='GH-Timeline-Item-toggle']/span[contains(text(),'Hide events')]");
        private static readonly By AnchorTitleLabelLocator = By.XPath(".//div[@class='GH-Timeline-Item-title']");
        private static readonly By AnchorFullscreenButtonLocator = By.XPath(".//button[contains(@class,'GH-Timeline-Item-full')]");
        private static readonly By AnchorMetaTitleLabelLocator = By.XPath(".//div[@class='GH-Timeline-Item-meta']/span[1]");
        private static readonly By AnchorMetaClientIdLabelLocator = By.XPath(".//div[@class='GH-Timeline-Item-meta']/span[2]");
        private static readonly By TimelineDateLabelLocator = By.XPath(".//div[@class='GH-Timeline-ItemTimeMeta'][1]");
        private static readonly By GraphicalNodeLocator = By.CssSelector("*.Graphical-Node");
        private static readonly By SettingsButtonLocator = By.XPath(".//button[contains(@class,'GH-Zoom-Settings')]");
        private static readonly By MiniMapContainerLocator = By.XPath(".//div[@class='GH-Minimap-Container']");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="GraphicalHistoryGridItem"/> class. 
        /// </summary>
        /// <param name="tableEntryContainer"></param>
        public GraphicalHistoryGridItem(IWebElement tableEntryContainer) : base(tableEntryContainer)
        {
        }
        
        /// <summary>
        ///  Anchor title label
        /// </summary>
        public ILabel AnchorTitleLabel => new Label(this.Container, AnchorTitleLabelLocator);

        /// <summary>
        ///  Anchor meta title label
        /// </summary>
        public ILabel AnchorMetaTitleLabel => new Label(this.Container, AnchorMetaTitleLabelLocator);

        /// <summary>
        ///  Anchor meta client ID label
        /// </summary>
        public ILabel AnchorMetaClientIdLabel => new Label(this.Container, AnchorMetaClientIdLabelLocator);

        /// <summary>
        ///  Timeline label
        /// </summary>
        public ILabel TimelineLabel => new Label(this.Container, TimelineDateLabelLocator);

        /// <summary>
        /// Zoom in button
        /// </summary>
        public IButton ZoomInButton => new Button(this.Container, ZoomInButtonLocator);

        /// <summary>
        /// Zoom out button
        /// </summary>
        public IButton ZoomOutButton => new Button(this.Container, ZoomOutButtonLocator);

        /// <summary>
        /// Reset button
        /// </summary>
        public IButton ResetButton => new Button(this.Container, ResetButtonLocator);

        /// <summary>
        /// Collapse all button
        /// </summary>
        public IButton CollapseAllButton => new Button(this.Container, CollapseAllButtonLocator);

        /// <summary>
        /// Rotate button
        /// </summary>
        public IButton RotateButton => new Button(this.Container, RotateButtonLocator);

        /// <summary>
        /// Anchor expand button
        /// </summary>
        public IButton AnchorExpandButton => new CustomClickButton(this.Container, AnchorExpandButtonLocator);

        /// <summary>
        /// Anchor close button
        /// </summary>
        public IButton AnchorCloseButton => new Button(this.Container, AnchorCloseButtonLocator);

        /// <summary>
        /// Anchor fullscreen button
        /// </summary>
        public IButton AnchorFullscreenButton => new Button(this.Container, AnchorFullscreenButtonLocator);

        /// <summary>
        /// Settings button
        /// </summary>
        public IButton SettingsButton => new Button(this.Container, SettingsButtonLocator);

        /// <summary>
        /// Collection of Graphical nodes
        /// </summary>
        public ItemsCollection<GraphicalNodeItem> GraphicalNodes => new ItemsCollection<GraphicalNodeItem>(this.Container, GraphicalNodeLocator);

        /// <summary>
        /// Checks if the minimap is visible
        /// </summary>
        /// <returns> True if minimap is displayed </returns>
        public bool IsMiniMapDisplayed() => DriverExtensions.IsDisplayed(this.Container, MiniMapContainerLocator);

    }
}
