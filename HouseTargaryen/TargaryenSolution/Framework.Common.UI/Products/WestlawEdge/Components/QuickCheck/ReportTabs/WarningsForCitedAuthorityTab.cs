namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.ReportTabs
{
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults;
    using Framework.Common.UI.Products.WestlawEdge.Items;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// The warnings for cited authority tab.
    /// </summary>
    public class WarningsForCitedAuthorityTab : BaseQuickCheckTabComponent
    {
        private static readonly By TabContainerLocator = By.ClassName("DA-WarningPage");
        private static readonly By ResultListLocator = By.XPath(".//div[@class='co_searchResultsList']");
        private static readonly By ResultItemLocator = By.XPath(".//div[@class='DA-KCWarning' or @class='DA-TOACase']");

        /// <summary>
        /// Gets the narrow pane.
        /// </summary>
        public WarningsNarrowPaneComponent NarrowPane { get; } = new WarningsNarrowPaneComponent();
        
        /// <summary>
        /// The result list.
        /// </summary>
        public QuickCheckItemsCollection<CitedAuthorityItem> ResultList =>
            new QuickCheckItemsCollection<CitedAuthorityItem>(
                new ByChained(this.ComponentLocator, ResultListLocator), ResultItemLocator, "div");   
        
        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TabContainerLocator;
    }
}