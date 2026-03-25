namespace Framework.Common.UI.Products.WestlawEdge.Components.ResultList
{
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// FolderAnalysisResultList
    /// </summary>
    public class FolderAnalysisResultListComponent : EdgeLegacyResultListComponent
    {
        private static readonly By SaveToFolderButtonLocator = By.XPath(".//li[@id='co_smartFolders_saveTo']//a");
        private static readonly By DeliveryDropdownLocator = By.Id("deliveryWidget1");
        private static readonly By DocumentLocator = By.XPath(".//li//div[contains(@class,'co_searchContent')]");
        private static readonly By SelectAllCheckboxLocator = By.Id("co_result_selectAll");
        private static readonly By DocLinkLocator = By.XPath("//h3[@id='co_docTitle']/a");

        private IWebElement Container { get; }

        /// <summary>
        /// Initializes a new instance of the  class. 
        /// </summary>
        /// <param name="container"></param>
        public FolderAnalysisResultListComponent(IWebElement container) : base(container)
        {
            this.Container = container;
        }

        /// <summary>
        /// Verify if save to folder button is displayed
        /// </summary>
        /// <returns></returns>
        public bool IsSaveToFolderButtonDisplayed() => DriverExtensions.IsDisplayed(SaveToFolderButtonLocator);

        /// <summary>
        /// Verify if delivery dropdown is displayed
        /// </summary>
        /// <returns></returns>
        public bool IsDeliveryDropdownDisplayed() => DriverExtensions.IsDisplayed(DeliveryDropdownLocator);

        /// <summary>
        /// Verify if Select All checkbox is displayed
        /// </summary>
        /// <returns></returns>
        public bool IsSelectAllDisplayed() => DriverExtensions.IsDisplayed(SelectAllCheckboxLocator);

        /// <summary>
        /// Get count of document
        /// </summary>
        /// <returns></returns>
        public int GetDocumentsCount() => DriverExtensions.GetElements(this.Container, DocumentLocator).Count;

        /// <summary>
        /// Click on a specific document based on the documents index
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="docIndex"> Document index to open </param>
        /// <returns> New instance of the page </returns>
        public new T ClickOnSearchResultDocumentByIndex<T>(int docIndex)
            where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(DocLinkLocator);
            DriverExtensions.Click(DriverExtensions.GetElements(DocLinkLocator).ElementAt(docIndex));
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
