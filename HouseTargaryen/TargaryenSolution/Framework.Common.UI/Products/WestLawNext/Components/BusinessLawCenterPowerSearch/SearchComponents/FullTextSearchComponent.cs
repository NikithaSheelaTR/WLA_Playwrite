namespace Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenterPowerSearch.SearchComponents
{
    using Framework.Common.UI.Products.WestLawNext.Pages.BusinessLawCenterPowerSearch;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The full text search section.
    /// </summary>
    public class FullTextSearchComponent : BaseSearchComponent<FullTextSearchResultsPage>
    {
        private static readonly By FormTypeTextBoxLocator = By.Id("formType");

        private static readonly By SearchCriteriaTextBoxLocator = By.Id("searchString");

        /// <summary>
        /// Clicks the Search button on the Full Text tab
        /// </summary>
        /// <returns>FullTextSearchResultsPage object</returns>
        public override FullTextSearchResultsPage ClickSearchButton()
        {
            this.BaseClickSearchButton();
            return new FullTextSearchResultsPage();
        }

        /// <summary>
        /// Type form type to search
        /// </summary>
        /// <param name="contentToSearch">Content to search</param>
        public void TypeinFormType(string contentToSearch) => DriverExtensions.SetTextField(contentToSearch, FormTypeTextBoxLocator);

        /// <summary>
        /// Types specified text into the Search Criteria text box 
        /// </summary>
        /// <param name="contentToSearch">Content to search</param>
        public void TypeSearchCriteria(string contentToSearch)
            => DriverExtensions.SetTextField(contentToSearch, SearchCriteriaTextBoxLocator);

    }
}