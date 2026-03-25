namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo.StatuteHistoryPages
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Products.WestLawNext.Models.HistoryItem;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo.HistoryItem;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Validity Page
    /// </summary>
    public class ValidityPage : TabPage
    {
        private static readonly By EnactedLegislationItemsLocator =
            By.XPath("//*[contains(text(),'Enacted Legislation')]//div[@class='co_relatedInfo_HistoryItem_TitleArea']");

        private static readonly By TermlListLocator = By.XPath("//ol[@class='co_relatedInfo_orderedList']//a[@guid]");

        private static readonly By ItemInsideContainerLocator = By.XPath("//ol/ol/li | //ol/li");

        /// <summary>
        /// Get Enacted Legislation Items CountClickOnDocumentByIndex
        /// </summary>
        /// <returns>items count</returns>
        public int GetEnactedLegislationItemsCount()
        {
            DriverExtensions.WaitForElementDisplayed(EnactedLegislationItemsLocator);
            return DriverExtensions.GetElements(EnactedLegislationItemsLocator).Count;
        }

        /// <summary>
        /// Retrieves List of document guid
        /// </summary>
        /// <returns>List of Document guid</returns>
        public List<string> GetDocumentGuidList()
            => DriverExtensions.GetElements(TermlListLocator).Select(a => a.GetAttribute("guid")).ToList();

        /// <summary>
        /// Clicks on certain document
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="index">
        /// Index of document to be clicked
        /// </param>
        /// <returns> New instance of the page </returns>
        public T ClickOnDocumentByIndex<T>(int index) where T : CommonDocumentPage
        {
            DriverExtensions.GetElements(TermlListLocator).ElementAt(index).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get checkboxes count
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetCheckboxesCount() => this.GetChildCheckboxes(this.ContentResultContainer).Count;

        /// <summary>
        /// Is all checkboxes selected
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsAllCheckboxesSelected()
            => this.GetChildCheckboxes(this.ContentResultContainer).TrueForAll(c => c.Selected);

        /// <summary>
        /// Gets HistoryItemModel by title
        /// </summary>
        /// <param name="title">Item title</param>
        /// <returns>The <see cref="HistoryItemModel"/>.</returns>
        public HistoryItemModel GetValidityPageItemModel(string title)
            => this.GetItemsList().Find(u => u.Title.Text == title).ToHistoryItemModel();
        
        /// <summary>
        /// Gets list of all items
        /// </summary>
        /// <returns>List of ValidityPageItems</returns>
        public List<HistoryItem> GetItemsList()
            => DriverExtensions.GetElements(this.ContentResultContainer, ItemInsideContainerLocator)
                .Select(u => new HistoryItem(u)).ToList();
    }
}