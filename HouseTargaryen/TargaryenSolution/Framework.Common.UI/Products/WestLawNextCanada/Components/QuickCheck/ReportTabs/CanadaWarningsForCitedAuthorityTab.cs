namespace Framework.Common.UI.Products.WestLawNextCanada.Components.QuickCheck.ReportTabs
{
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.ReportTabs;
    using Framework.Common.UI.Products.WestlawEdge.Items;
    using Framework.Common.UI.Products.WestLawNextCanada.Items;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// The Canada Warnings for cited authority tab.
    /// </summary>

    public class CanadaWarningsForCitedAuthorityTab : WarningsForCitedAuthorityTab
    {
        private static readonly By ResultListLocator = By.XPath(".//div[@class='co_searchResultsList']");
        private static readonly By ResultItemLocator = By.XPath(".//div[@class='DA-KCWarning' or @class='DA-TOACase']");

        /// <summary>
        /// Result List of cited authority items in the warnings for cited authority tab.
        /// </summary>
        public new QuickCheckItemsCollection<CanadaCitedAuthorityItem> ResultList =>
           new QuickCheckItemsCollection<CanadaCitedAuthorityItem>(
               new ByChained(this.ComponentLocator, ResultListLocator), ResultItemLocator, "div");

    }
}