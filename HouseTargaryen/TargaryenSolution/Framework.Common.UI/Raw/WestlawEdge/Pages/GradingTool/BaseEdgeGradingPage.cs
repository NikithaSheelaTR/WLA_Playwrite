namespace Framework.Common.UI.Raw.WestlawEdge.Pages.GradingTool
{
    using Framework.Common.UI.Products.Shared.Pages;
    using OpenQA.Selenium;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    /// <summary>
    /// Edge grading tool base page.
    /// </summary>
    public class BaseEdgeGradingPage : BaseModuleRegressionPage
    {
        private static readonly By PageTitleLocator =
            By.XPath("//h1[text()='Grading Tool']");

        /// <summary>
        /// Verifies that the page title is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if page title is displayed. </returns>
        public bool IsPageTitleDisplayed() => DriverExtensions.IsDisplayed(PageTitleLocator);
    }
}
