namespace Framework.Common.UI.Products.WestLawNext.Pages.MedicalLitigator
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// MedLit Category/Home Page
    /// </summary>
    public class MedLitCategoryPage : CommonBrowsePage
    {
        private static readonly By SearchTextboxLocator = By.Id("searchInputId");

        private static readonly By UseExpandedTermsCheckboxLocator = By.Id("co_expandedTerms");

        /// <summary>
        /// Option to use expanded terms search
        /// </summary>
        /// <param name="searchTerm">Term to search</param>
        /// <param name="useAdditionalTermsCheckBox">Set check box selected or unselected</param>
        /// <typeparam name="T">Page type</typeparam>
        /// <returns>Instance of the page T type</returns>
        public T RunSearch<T>(string searchTerm, bool useAdditionalTermsCheckBox) where T : ICreatablePageObject
        {
            this.Header.EnterSearchQuery(searchTerm);
            DriverExtensions.SetCheckbox(UseExpandedTermsCheckboxLocator, useAdditionalTermsCheckBox);
            return this.Header.ClickSearchButton<T>();
        }

        /// <summary>
        /// Get text from the search text box
        /// </summary>
        /// <returns> Search text </returns>
        public string GetSearchText() => DriverExtensions.WaitForElement(SearchTextboxLocator).Text;
    }
}