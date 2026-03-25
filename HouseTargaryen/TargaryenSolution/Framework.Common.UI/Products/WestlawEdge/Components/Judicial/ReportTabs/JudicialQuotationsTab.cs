namespace Framework.Common.UI.Products.WestlawEdge.Components.Judicial.ReportTabs
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components.Judicial;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.ReportTabs;
    using Framework.Common.UI.Products.WestlawEdge.Items;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Judicial quotations tab
    /// </summary>
    public sealed class JudicialQuotationsTab : LanguageQuotationAnalysisTab
    {
        private static readonly By ResultListLocator = By.XPath(".//div[@class ='DA-QuotationContainer']");
        private static readonly By ResultItemLocator = By.XPath(".//div[@class ='co_issueItemHeader']//ancestor::ul/li");
        
        /// <summary>
        /// Party switcher component
        /// </summary>
        public JudicialRecommendationsTabPartySwitcherComponent<JudicialQuotationsTab> PartySwitcher =>
            new JudicialRecommendationsTabPartySwitcherComponent<JudicialQuotationsTab>();

        /// <summary>
        /// The result list
        /// </summary>
        public override QuickCheckItemsCollection<QuotationAnalysisItem> ResultList =>
            new QuickCheckItemsCollection<QuotationAnalysisItem>(new ByChained(this.ComponentLocator, ResultListLocator), ResultItemLocator);     
    }
}