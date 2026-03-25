namespace Framework.Common.UI.Products.WestLawNext.Pages.SecondarySources.PublicationLandingPages
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;
    using System.Linq;

    /// <summary>
    /// The publication ten most recent docs landing page.
    /// </summary>
    public class PublicationTenMostRecentDocsLandingPage : PublicationLandingPage
    {
        private static readonly By TenMostRecentTabLocator = By.XPath("//h2[text()='10 most recent documents']");

        private static readonly By TenMostRecentDocsLocator = By.XPath("//ol[@class='co_searchResult_list']/li");

        private static readonly By SummarySectionLocator = By.XPath(".//div[contains(@id, 'co_searchResults_summary')]");

        /// <summary>
        /// The is number of present documents correct.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetDocumentsCount() => DriverExtensions.GetElements(TenMostRecentDocsLocator).Count;

        /// <summary>
        /// The is ten most recent tab displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsTenMostRecentTabDisplayed() => DriverExtensions.IsDisplayed(TenMostRecentTabLocator);

        /// <summary>
        /// Is any summary section displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsAnySummarySectionDisplayed() => DriverExtensions.GetElements(TenMostRecentDocsLocator, SummarySectionLocator).Any(el => el.Displayed);
    }
}