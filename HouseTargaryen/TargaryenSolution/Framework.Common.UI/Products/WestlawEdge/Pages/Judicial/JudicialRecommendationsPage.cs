namespace Framework.Common.UI.Products.WestlawEdge.Pages.Judicial
{
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Elements.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck;

    using OpenQA.Selenium;

    /// <summary>
    /// The judicial recommendations page.
    /// </summary>
    public class JudicialRecommendationsPage : QuickCheckBasePage
    {
        private static readonly By SelectReportDropdownLocator = By.Id("DA-reportSelectorContainer");

        /// <summary>
        /// Gets the select report dropdown.
        /// </summary>
        public SelectReportDropdown SelectReportDropdown => new SelectReportDropdown(SelectReportDropdownLocator);

        /// <summary>
        /// Gets The report tabs component
        /// </summary>
        public QuickCheckReportTabPanel ReportTabsPanel { get; } = new QuickCheckReportTabPanel();
    }
}
