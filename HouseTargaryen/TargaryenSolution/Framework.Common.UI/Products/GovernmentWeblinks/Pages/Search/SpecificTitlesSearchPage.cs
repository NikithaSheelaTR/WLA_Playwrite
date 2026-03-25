namespace Framework.Common.UI.Products.GovernmentWeblinks.Pages.Search
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using OpenQA.Selenium;

    /// <summary>
    /// Specific titles search page
    /// </summary>
    public class SpecificTitlesSearchPage : BaseWeblinksSearchPage
    {
        private static readonly By TextboxLocator = By.Id("Query");

        private const string SpecificTitleCheckBoxLctMask = "//*[@id = 'node{0}']";

        /// <summary>
        /// Set check box by index
        /// </summary>
        /// <param name="index"> the index of the checkbox </param>
        /// <param name="isSet"> the status of the checkbox</param>
        public void SetSpecificTitleCheckBoxByIndex(int index, bool isSet =true) => 
            DriverExtensions.SetCheckbox(SafeXpath.BySafeXpath(string.Format(SpecificTitleCheckBoxLctMask, index)), isSet);

        /// <summary>
        /// Search any results
        /// </summary>
        /// <typeparam name="T">The type of a page</typeparam>
        /// <param name="query">The query for search</param>
        /// <returns>The instance of a page</returns>
        public T Search<T>(string query) where T : ICreatablePageObject
        {
            DriverExtensions.SetTextField(query, TextboxLocator);
            return this.ClickSearchButton<T>();
        }
    }
}
