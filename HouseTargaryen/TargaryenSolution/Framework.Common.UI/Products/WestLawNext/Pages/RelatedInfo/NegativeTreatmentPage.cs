namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components.Document.RI;
    using Framework.Common.UI.Products.WestLawNext.Models.HistoryItem;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo.HistoryItem;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// NegativeTreatmentPage
    /// </summary>
    public class NegativeTreatmentPage : TabPage
    {
        private const string ReferenceGridLocator = "//table[@id='co_relatedInfo_table']";

        private static readonly By NegativeCitingReferenceHeaderLocator =
            By.XPath("//div[@class='co_relatedInfo_negativeCitingRefsTable']//h3[@class='co_relatedinfo_negativehistory_heading']");

        private static readonly By NegativeCitingReferencesSubheadingLocator =
            By.XPath("//div[@class='co_relatedInfo_negativeCitingRefsTable']//*[@class='co_relatedinfo_negativehistory_subheading']");

        private static readonly By NegativeCitingReferencesTableLocator =
            By.ClassName("co_relatedInfo_negativeCitingRefsTable");

        private static readonly By NegativeDirectHistoryHeaderLocator =
            By.XPath("//h3[@class='co_relatedinfo_negativehistory_heading'][1]");

        private static readonly By NegativeDirectHistoryOrderedItemLocator =
            By.XPath("./li[@class='co_relatedInfo_listItem']");

        private static readonly By NegativeDirectHistoryOrderedListLocator = By.Id("co_relatedInfo_orderedList");

        private static readonly By NegativeDirectHistorySubheadingLocator =
            By.XPath("//*[@class='co_relatedinfo_negativehistory_subheading'][1]");

        private static readonly By NoNegativeDirectHistoryMessageContainerLocator =
            By.XPath("//div[@class='co_relatedinfo_negativehistory_nohistory']");

        private static readonly By NegativeTreatmentTitleLocator = By.Id("co_categoryLabelContainer");

        private static readonly By PreviousPaginatonButtonLocator = By.Id("co_RI_PrevPage");

        /// <summary>
        /// Reference grid
        /// </summary>
        public ReferenceGridComponent NegativeTreatementReferenceGrid { get; private set; }
            = new ReferenceGridComponent(ReferenceGridLocator);

        /// <summary>
        /// Click previous pagination button
        /// </summary>
        public void ClickPreviousPaginationButton() => DriverExtensions.WaitForElement(PreviousPaginatonButtonLocator).Click();

        /// <summary>
        /// Get Negative Citing Reference Header Text
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetNegativeCitingReferenceHeaderText()
            => DriverExtensions.GetText(NegativeCitingReferenceHeaderLocator);

        /// <summary>
        /// IsNegativeCitingReferencesSubheadingTextContain
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetNegativeCitingReferencesSubheadingText()
            => DriverExtensions.WaitForElement(NegativeCitingReferencesSubheadingLocator).Text;

        /// <summary>
        /// Get NegativeDirectHistoryHeaderText
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetNegativeDirectHistoryHeaderText()
            => DriverExtensions.WaitForElement(NegativeDirectHistoryHeaderLocator).Text;

        /// <summary>
        /// Gets number of all NegativeTreatmentItems
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetCountOfAllItems() => this.GetItemsList().Count;

        /// <summary>
        /// Gets NegativeTreatmentItemModel by index
        /// </summary>
        /// <param name="index">Item index</param>
        /// <returns>The <see cref="NegativeTreatmentItemModel"/>.</returns>
        public NegativeTreatmentItemModel GetNegativeTreatmentItemModel(int index)
            => this.GetItemsList()[index].ToNegativeTreatmentItemModel();

        /// <summary>
        /// Are all NegativeTreatmentItemModel have checkboxes
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool AreAllItemsHaveCheckBox() => this.GetItemsList().TrueForAll(u => u.CheckBox.Displayed);

        /// <summary>
        /// Are all NegativeTreatmentItemModel's checkboxes selected
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool AreAllItemCheckBoxesSelected()
            => this.GetItemsList().TrueForAll(u => u.CheckBox.Selected);

        /// <summary>
        /// IsNoNegativeDirectHistoryMessageContain
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetNoNegativeDirectHistoryMessage()
            => DriverExtensions.WaitForElement(NoNegativeDirectHistoryMessageContainerLocator).Text;

        /// <summary>
        /// Is Negative Citing Reference Header Displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsNegativeCitingReferenceHeaderDisplayed()
            => DriverExtensions.IsDisplayed(NegativeCitingReferenceHeaderLocator);

        /// <summary>
        /// Is Negative Citing References Subheading Element Displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsNegativeCitingReferencesSubheadingElementDisplayed()
            => DriverExtensions.IsDisplayed(NegativeCitingReferencesSubheadingLocator);

        /// <summary>
        /// is Negative Citing References Table Displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsNegativeCitingReferencesTableDisplayed()
            => DriverExtensions.IsDisplayed(NegativeCitingReferencesTableLocator);

        /// <summary>
        /// Is Negative Direct History Header Displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsNegativeDirectHistoryHeaderDisplayed()
            => DriverExtensions.IsDisplayed(NegativeDirectHistoryHeaderLocator);

        /// <summary>
        /// Is Negative Direct History Subheading Element Displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsNegativeDirectHistorySubheadingElementDisplayed()
            => DriverExtensions.IsDisplayed(NegativeDirectHistorySubheadingLocator);

        /// <summary>
        /// IsNegativeDirectHistorySubheadingTextContain
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetNegativeDirectHistorySubheadingText()
            => DriverExtensions.WaitForElement(NegativeDirectHistorySubheadingLocator).Text;

        /// <summary>
        /// Determines whether [the flag link negatives to negative treatment page].
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsNegativeTreatmentPage()
            => DriverExtensions.WaitForElement(NegativeTreatmentTitleLocator).Text.Equals("Negative Treatment");

        /// <summary>
        /// Gets list of all items
        /// </summary>
        /// <returns>List of NegativeTreatmentItems</returns>
        private List<NegativeTreatmentItem> GetItemsList()
            => DriverExtensions.GetElements(DriverExtensions.WaitForElement(NegativeDirectHistoryOrderedListLocator), NegativeDirectHistoryOrderedItemLocator)
                .Select(u => new NegativeTreatmentItem(u)).ToList();
    }
}