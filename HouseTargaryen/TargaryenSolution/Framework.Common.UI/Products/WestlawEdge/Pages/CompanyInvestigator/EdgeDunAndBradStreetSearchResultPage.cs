namespace Framework.Common.UI.Products.WestlawEdge.Pages.CompanyInvestigator
{
    using Framework.Common.UI.Products.WestLawNext.Pages.SearchResult;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    ///  EdgeDunAndBradStreetSearchResultPage Search Result Page
    /// </summary>
    public class EdgeDunAndBradStreetSearchResultPage : CategorySearchResultPage
    {
        private const string OverviewItemLctMask = "//a[@resulttypekey='DUN-BRADSTREET' and contains(.,'{0}')]";

        /// <summary>
        /// EdgeDunAndBradStreetReportsOverviewPage Reports overview page
        /// </summary>
        /// <param name="itemName">Item name</param>
        /// <returns>The <see cref="EdgeDunAndBradStreetReportsOverviewPage"/></returns>
        public EdgeDunAndBradStreetReportsOverviewPage ClickOverViewItemByName(string itemName)
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(OverviewItemLctMask, itemName))).CustomClick();
            return new EdgeDunAndBradStreetReportsOverviewPage();
        }
    }
}
