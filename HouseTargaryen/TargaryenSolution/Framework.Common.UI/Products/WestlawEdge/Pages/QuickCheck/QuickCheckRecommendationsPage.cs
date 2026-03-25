namespace Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck
{
    using System.Linq;

    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Elements.QuickCheck;

    using OpenQA.Selenium;

    /// <summary>
    /// The Document analyzer Recommendations page.
    /// </summary>
    public class QuickCheckRecommendationsPage : QuickCheckBasePage
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

        /// <summary>
        /// Go to additional cases link under the header
        /// </summary>
        /// <param name="headingTitle">Heading to search</param>
        /// <returns>Current page</returns>
        public QuickCheckAdditionalCasesPage GoToAdditionalCasesLinkByHeaderName(string headingTitle) =>
            this.ReportTabsPanel.GetRecommendationsTab().ResultList.Headings
                .First(heading => heading.TitleLabel.Text.Contains(headingTitle)).AdditionalCasesLink
                .Click<QuickCheckAdditionalCasesPage>();
    }
}