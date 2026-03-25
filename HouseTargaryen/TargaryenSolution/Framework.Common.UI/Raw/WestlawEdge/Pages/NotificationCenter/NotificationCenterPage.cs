namespace Framework.Common.UI.Raw.WestlawEdge.Pages.NotificationCenter
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Toolbar.FooterToolbar;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Products.WestlawEdge.Components.NotificationCenter;
    using OpenQA.Selenium;

    /// <summary>
    /// The notification center page.
    /// </summary>
    public class NotificationCenterPage : EdgeCommonAuthenticatedWestlawNextPage
    {
        private static readonly By NotificationHeaderLabelLocator = By.XPath("//*[@class = 'NotificationTabPanelHeader-heading']");

        /// <summary>
        /// Notification Header Label
        /// </summary>
        public ILabel NotificationHeaderLabel => new Label(NotificationHeaderLabelLocator);

        /// <summary>
        /// Notification Center Tab Panel
        /// </summary>
        public NotificationCenterTabPanel NotificationCenterTabPanel { get; } = new NotificationCenterTabPanel();

        /// <summary>
        /// New Narrow Pane Component (left side of search results page)
        /// </summary>
        public EdgeNarrowPaneComponent NarrowPane { get; } = new EdgeNarrowPaneComponent();

        /// <summary>
        /// List of notifications on the Notification center View All page
        /// </summary>
        public NotificationCenterResultListComponent ResultList { get; } = new NotificationCenterResultListComponent();

        /// <summary>
        /// Gets the footer toolbar component.
        /// </summary>
        public FooterToolbarComponent FooterToolbarComponent { get; } = new FooterToolbarComponent();
    }
}