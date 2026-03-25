namespace Framework.Common.UI.Products.WestLawNextMobile.Pages.Recent
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Recent documents page
    /// </summary>
    public class RecentDocumentsPage : BaseRecentPage
    {
        private const string PageTitle = "Recent Documents";
        private static readonly By RecentSearchesLinkLocator = By.Id("coid_website_recentSearchesLink");

        /// <summary>
        /// Initializes a new instance of the <see cref="RecentDocumentsPage"/> class. 
        /// </summary>
        public RecentDocumentsPage()
            : base(PageTitle)
        {
        }

        /// <summary>
        /// Click on the Recent Researches link
        /// </summary>
        /// <returns> The <see cref="RecentSearchesPage"/>. </returns>
        public RecentSearchesPage ClickRecentSearchesLink()
        {
            DriverExtensions.WaitForElement(RecentSearchesLinkLocator).Click();
            return new RecentSearchesPage();
        }

        /// <summary>
        /// Verify that Recent Searches Link displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsRecentSearchesLinkDisplayed() => DriverExtensions.IsDisplayed(RecentSearchesLinkLocator, 5);
    }
}