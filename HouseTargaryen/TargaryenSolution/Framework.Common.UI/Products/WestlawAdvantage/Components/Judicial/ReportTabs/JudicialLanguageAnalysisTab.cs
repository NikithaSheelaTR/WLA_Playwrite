namespace Framework.Common.UI.Products.WestlawAdvantage.Components.Judicial.ReportTabs
{
    using Framework.Common.UI.Products.WestlawEdge.Components.Judicial;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.ReportTabs;
    using Framework.Common.UI.Products.WestlawEdge.Items;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Judicial language analysis tab
    /// </summary>
    public sealed class JudicialLanguageAnalysisTab : LanguageQuotationAnalysisTab
    {
        private static readonly By ResultListLocator = By.XPath(".//div[@class ='DA-QuotationContainer']");
        private static readonly By ResultItemLocator = By.XPath(".//div[@class ='co_issueItemHeader']//ancestor::ul/li");

        /// <summary>
        /// Party switcher component
        /// </summary>
        public JudicialRecommendationsTabPartySwitcherComponent<JudicialLanguageAnalysisTab> PartySwitcher =>
            new JudicialRecommendationsTabPartySwitcherComponent<JudicialLanguageAnalysisTab>();

        /// <summary>
        /// The result list
        /// </summary>
        public override QuickCheckItemsCollection<QuotationAnalysisItem> ResultList =>
            new QuickCheckItemsCollection<QuotationAnalysisItem>(new ByChained(this.ComponentLocator, ResultListLocator), ResultItemLocator);
    }
}
