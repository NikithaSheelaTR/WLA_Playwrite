namespace Framework.Common.UI.Products.Shared.Pages.AdvancedSearchTemplates
{
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// BlackLawDictionaryAstPage
    /// </summary>
    public class BlackLawDictionaryAstPage : CommonAdvancedSearchPage
    {
        private static readonly By BlacksLawDictionaryDocumentFieldsAreaLocator =
            By.CssSelector("#co_search_advancedSearchDocumentFieldsBox_0");

        private static readonly By BlacksLawDictionaryExcludeFieldsAreaLocator =
            By.CssSelector("#co_search_advancedSearchDocumentFieldsBox_1");

        private static readonly By ConnectorsAndExpandersLegendListLocator =
            By.CssSelector("dl#co_search_advancedSearch_tncLegendList");

        /// <summary>
        /// This method verifies that Black's Law Dictionary Document Fields Area is displayed
        /// According BS #652120 should be in the top of search areas (before ExcludeFields Area)
        /// </summary>
        /// <returns>True if Black's Law Dictionary Document Fields Area is present on the page</returns>
        public bool IsDocumentFieldsSectionDisplayed()
            => DriverExtensions.IsDisplayed(BlacksLawDictionaryDocumentFieldsAreaLocator);

        /// <summary>
        /// This method verifies that Black's Law Dictionary Exclude Fields Area is displayed
        /// </summary>
        /// <returns>True if Black's Law Dictionary Exclude Fields Area is present on the page</returns>
        public bool IsExcludeDocumentsWithTheseTermsSectionDisplayed()
            => DriverExtensions.IsDisplayed(BlacksLawDictionaryExcludeFieldsAreaLocator);

        /// <summary>
        /// This method verifies that Black's Law Dictionary Connectors And Expanders Legend List is displayed
        /// </summary>
        /// <returns>True if Black's Law Dictionary Connectors And Expanders Legend List is present on the page</returns>
        public bool IsConnectorsAndExpandersLegendListDisplayed()
            => DriverExtensions.IsDisplayed(ConnectorsAndExpandersLegendListLocator);
    }
}