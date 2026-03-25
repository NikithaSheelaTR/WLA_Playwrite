namespace Framework.Common.UI.Products.WestLawNext.Pages.BusinessLawCenterPowerSearch
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The global header footer page.
    /// </summary>
    public abstract class GlobalHeaderFooterPage : BaseModuleRegressionPage
    {
        private static readonly By BlcPowerSearchLinkLocator = By.LinkText("BLC PowerSearch");

        private static readonly By LctLinkBlcSignOffLocator = By.Id("signOffLink");

        private static readonly By ProjectsLinkLocator = By.LinkText("Projects");

        /// <summary>
        /// The sign off.
        /// </summary>
        public static void SignOff()
        {
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForElement(LctLinkBlcSignOffLocator);
            DriverExtensions.ScrollTo(LctLinkBlcSignOffLocator);
            DriverExtensions.Click(LctLinkBlcSignOffLocator);
        }

        // TODO 1/14/15 CCL - need to re-work; clicking on BLC Link, doesn't refresh page if no search run; can get stuck on full text tab

        /// <summary>
        /// Click on the BLC PowerSearch (go to home page) link
        /// </summary>
        /// <returns>
        /// The <see cref="CompanySearchPage"/>.
        /// </returns>
        public CompanySearchPage ClickOnBlcPowerSearchLink()
        {
            DriverExtensions.Click(BlcPowerSearchLinkLocator);
            DriverExtensions.WaitForPageLoad();
            return new CompanySearchPage();
        }

        /// <summary>
        /// Click on the Projects list (go to Projects page) link
        /// </summary>
        /// <returns>
        /// The <see cref="ProjectListPage"/>.
        /// </returns>
        public virtual ProjectListPage ClickOnProjectsLink()
        {
            DriverExtensions.Click(ProjectsLinkLocator);
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
            return new ProjectListPage();
        }
    }
}