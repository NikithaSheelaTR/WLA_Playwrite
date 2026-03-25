namespace Framework.Common.UI.Products.WestlawAdvantage.Pages.QuickCheck
{
    using System.Linq;
    using Framework.Common.UI.Products.WestlawAdvantage.Components.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Elements.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck;
    using OpenQA.Selenium;

    /// <summary>
    /// The Document analyzer Recommendations page.
    /// </summary>
    public class WestlawAdvantageQuickCheckRecommendationsPage : QuickCheckBasePage
    {
        private static readonly By SelectReportDropdownLocator = By.XPath("//*[@id='DA-reportSelectorContainer']");        

        /// <summary>
        /// Gets the select report dropdown.
        /// </summary>
        public SelectReportDropdown SelectReportDropdown => new SelectReportDropdown(SelectReportDropdownLocator);

        /// <summary>
        /// Gets The report tabs component
        /// </summary>
        public WestlawAdvantageReportTabPanel WestlawAdvantageReportTabsPanel { get; } = new WestlawAdvantageReportTabPanel();        
    }
}