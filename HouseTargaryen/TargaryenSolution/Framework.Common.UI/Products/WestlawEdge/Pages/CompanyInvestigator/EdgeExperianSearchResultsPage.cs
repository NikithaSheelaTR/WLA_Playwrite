namespace Framework.Common.UI.Products.WestlawEdge.Pages.CompanyInvestigator
{
    using System.Linq;

    using Framework.Common.UI.Products.WestLawNext.Pages.SearchResult;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Experian Search Results Page
    /// </summary>
    public class EdgeExperianSearchResultsPage : CategorySearchResultPage
    {
        private static readonly By BusinessCreditReportLocator =
            By.XPath("//a[contains(@id,'cobalt_result_businesscreditreports_title')]");
        
        /// <summary>
        /// Clicks on business by index
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>Experian Report Document Page</returns>
        public EdgeCommonReportDocumentPage ClickOnBusinessByIndex(int index)
        {
            DriverExtensions.GetElements(BusinessCreditReportLocator).ElementAt(index).CustomClick();
            return new EdgeCommonReportDocumentPage();
        }
    }
}
