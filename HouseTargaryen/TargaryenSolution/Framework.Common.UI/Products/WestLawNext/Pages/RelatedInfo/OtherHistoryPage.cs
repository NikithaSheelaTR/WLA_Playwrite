namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.WestLawNext.Models.HistoryItem;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo.HistoryItem;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// OtherHistoryPage
    /// </summary>
    public class OtherHistoryPage : TabPage
    {
        private static readonly By AnnotationHistorySubheadingLocator =
            By.XPath("//h3[@class='co_relatedInfo_history_subheading' and contains(text(),'Annotation History')]");

        private static readonly By DirectHistorySubheadingLocator =
             By.XPath("//h3[@class='co_relatedInfo_history_subheading' and (contains(text(),'Direct History') or contains(text(),'Historique direct'))]");

        private static readonly By OrderedListLocator = By.XPath("./ol[contains(@class,'co_relatedInfo_orderedList')]");

        private static readonly By ItemInsideContainerLocator =
            By.XPath(".//li[contains(@class,'co_relatedInfo_listItem')]");

        private static readonly By RelatedReferencesSubheadingLocator =
            By.XPath("//h3[@class='co_relatedInfo_history_subheading' and contains(text(),'Related References')]");

        private static readonly By GraphicalKeyCiteLocator = By.Id("coid_relatedInfo_appellateHistoryGraphicFilledDiv");

        /// <summary>
        /// Initializes a new instance of the <see cref="OtherHistoryPage"/> class. 
        /// </summary>
        public OtherHistoryPage()
        {
            this.Toolbar.DetailDropdown = new HistoryPageDetailDropdown();
        }

        /// <summary>
        /// Get Annotation History Subheading Text
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetAnnotationHistorySubheadingText()
            => DriverExtensions.GetText(AnnotationHistorySubheadingLocator);

        /// <summary>
        /// Get Direct History Subheading Text
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetDirectHistorySubheadingText() => DriverExtensions.GetText(DirectHistorySubheadingLocator);

        /// <summary>
        /// Get Related References Subheading Text
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetRelatedReferencesSubheadingText()
            => DriverExtensions.GetText(RelatedReferencesSubheadingLocator);

        /// <summary>
        /// Verify Result Container Block Name
        /// </summary>
        /// <param name="item">Block name</param>
        /// <returns>Item</returns>
        public bool VerifyResultContainerBlockName(string item)
            => DriverExtensions.GetElements(this.ContentResultContainer, OrderedListLocator)
                                .Select(el => el.Text)
                                .Any(text => text.Contains(item));

        /// <summary>
        /// Gets OtherHistoryItemModel by title
        /// </summary>
        /// <param name="title">Item title</param>
        /// <returns>The <see cref="OtherHistoryItemModel"/>.</returns>
        public OtherHistoryItemModel GetOtherHistoryPageItemModel(string title)
            => this.GetItemsList().Find(u => u.Title.Text == title).ToOtherHistoryItemModel();

        /// <summary>
        /// Are all OtherHistoryItems have rank > 0
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool AreHistoryItemsHaveRank() => this.GetItemsList().TrueForAll(u => u.Rank > 0);

        /// <summary>
        /// Is the graphical keycite displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsGraphicalKeyCiteDisplayed() => DriverExtensions.IsDisplayed(GraphicalKeyCiteLocator);

        /// <summary>
        /// Gets list of all items
        /// </summary>
        /// <returns>List of OtherHistoryItem</returns>
        private List<OtherHistoryItem> GetItemsList()
            => DriverExtensions.GetElements(this.ContentResultContainer, ItemInsideContainerLocator)
                .Select(u => new OtherHistoryItem(u)).ToList();
    }
}