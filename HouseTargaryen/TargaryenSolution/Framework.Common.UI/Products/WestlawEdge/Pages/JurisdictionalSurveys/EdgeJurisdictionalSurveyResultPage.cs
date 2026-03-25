namespace Framework.Common.UI.Products.WestlawEdge.Pages.JurisdictionalSurveys
{
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Edge Jurisdictional Survey Result Page
    /// </summary>
    public class EdgeJurisdictionalSurveyResultPage : CommonAuthenticatedWestlawNextPage
    {
        private const string ResultListItemLctMask =
            "//*[@id='co_document_0']//li[@class='SurveyTool-jurisdiction'][{0}]//li[@class='SurveyTool-jurisdiction-item '][{1}]//h3/a";

        /// <summary>
        /// Clicking the By topic tab
        /// </summary>
        /// <param name="groupIndex"> result group index </param>
        /// <param name="resultIndex"> result index  </param>
        /// <returns>The <see cref="EdgeCommonDocumentPage"/>.</returns>
        public EdgeCommonDocumentPage ClickResultListItemTitleByIndex(int groupIndex, int resultIndex)
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(ResultListItemLctMask, groupIndex, resultIndex))).Click();
            return new EdgeCommonDocumentPage();
        }
    }
}
