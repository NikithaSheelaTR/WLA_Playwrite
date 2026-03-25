namespace Framework.Common.UI.Products.WestlawEdge.Components.Miscellaneous
{
    using Framework.Common.UI.Products.WestLawNext.Components;
    using OpenQA.Selenium;

    /// <summary>
    /// Custom Pages tab component
    /// </summary>
    public class HistoryTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("tab_HistoryPaneId");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "History";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the HistoryContentComponent
        /// </summary>
        public HistoryContentComponent HistoryContentComponent { get; } = new HistoryContentComponent();
    }
}
