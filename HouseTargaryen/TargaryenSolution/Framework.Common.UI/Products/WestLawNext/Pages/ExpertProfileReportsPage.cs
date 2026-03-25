namespace Framework.Common.UI.Products.WestLawNext.Pages
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The expert profile reports page.
    /// </summary>
    public class ExpertProfileReportsPage : BaseModuleRegressionPage
    {
        private static readonly By ReportContentLocator = By.Id("co_profReportContent");

        private static readonly By JudicialReversalReportLocator = By.Id("coid_website_link_Professional_JudicialReversal");

        /// <summary>
        /// Click on 'Judicial Reversal Report' link
        /// </summary>
        /// <returns>'The Judicial Reversal Report' page</returns>
        public JudicalReversalReportPage ClickJudicalReversalReportLink()
        {
            DriverExtensions.WaitForElement(JudicialReversalReportLocator).Click();
            return new JudicalReversalReportPage();
        }

        /// <summary>
        /// The is this page displayed.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsPageDisplayed() => DriverExtensions.IsDisplayed(ReportContentLocator);
    }
}