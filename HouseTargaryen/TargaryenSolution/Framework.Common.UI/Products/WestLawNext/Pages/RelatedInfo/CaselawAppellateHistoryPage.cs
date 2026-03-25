namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Products.WestLawNext.Models.HistoryItem;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo.HistoryItem;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// CaselawAppellateHistoryPage
    /// </summary>
    public class CaselawAppellateHistoryPage : TabPage
    {
        private static readonly By CheckBoxInListLocator =
            By.XPath("./li[contains(@class,'co_relatedInfo_listItem')]//input[@type='checkbox']");

        private static readonly By DirectHistorySubheadingLocator =
            By.XPath("//h3[contains(@class, 'co_relatedInfo_history_subheading')]");

        private static readonly By GraphicViewBoxLocator = By.Id("viewbox");

        private static readonly By HistoryGraphicLocator = By.Id("co_relatedInfo_graphicalKC");

        private static readonly By LinkToGuidLocator = By.XPath(".//a[contains(@id, 'linkToGuid_')]");

        private static readonly By LegendLocator = By.Id("co_relatedInfo_legend");

        private static readonly By RelatedInfoDirectHistoryItemsListLocator = By.ClassName("co_relatedInfo_orderedList");

        private static readonly By ItemInsideContainerLocator = By.XPath("./li[contains(@class,'co_relatedInfo_listItem')]");

        private static readonly By RelatedReferencesLocator = By.XPath("//h3[text()='Related References (']");

        /// <summary>
        /// Initializes a new instance of the <see cref="CaselawAppellateHistoryPage"/> class. 
        /// </summary>
        public CaselawAppellateHistoryPage()
        {
            this.Toolbar.DetailDropdown = new HistoryPageDetailDropdown();
        }

        /// <summary>
        /// History GraphicComponent
        /// </summary>
        private IWebElement HistoryGraphic => DriverExtensions.WaitForElement(HistoryGraphicLocator);

        /// <summary>
        /// Related InfoDirect History Items List
        /// </summary>
        private IWebElement RelatedInfoDirectHistoryItemsList => DriverExtensions.WaitForElement(RelatedInfoDirectHistoryItemsListLocator);

        /// <summary>
        /// Is Related References subheading Displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsRelatedReferencesDisplayed()
            => DriverExtensions.WaitForElement(RelatedReferencesLocator).Displayed;

        /// <summary>
        /// Determines whether checkboxes with given indexes are selected or not
        /// </summary>
        /// <param name="indexesArray">
        /// The indexes Array.
        /// </param>
        /// <returns>
        /// True if checkboxes with given indexes are selected
        /// </returns>
        public bool AreCheckboxesSelected(params int[] indexesArray)
            => indexesArray.ToList().Select(i => this.GetCheckBoxes().ElementAt(i)).ToList().TrueForAll(u => u.Selected);

        /// <summary>
        /// Gets the number of checkboxes
        /// </summary>
        /// <returns>number of checkboxes</returns>
        public int GetCheckBoxesCount() => this.GetCheckBoxes().Count;

        /// <summary>
        /// Gets the count of DirectHistory items from Subheading 
        /// </summary>
        /// <returns>count of items</returns>
        public int GetCountOfCasselawAppelateItemsFromSubheading()
            => DriverExtensions.WaitForElement(DirectHistorySubheadingLocator).GetText().RetrieveCountFromBrackets();

        /// <summary>
        /// Gets the count of DirectHistory items from drop-down 
        /// </summary>
        /// <param name="historyOption"> The history Option. </param>
        /// <returns>Count of items </returns>
        public int GetCountOfHistoryItemsFromDropdown(HistoryOptions historyOption)
            => this.Toolbar.HistoryDropdown.Options.Contains(historyOption) ? this.Toolbar.HistoryDropdown.GetOptionCount(historyOption) : -1;

        /// <summary>
        /// Gets CaselawAppelateItemModels
        /// </summary>
        /// <returns>The <see cref="CaselawAppelateItemModel"/>.</returns>
        public List<CaselawAppelateItemModel> GetCaselawAppelateItemModels() =>
            this.GetItemsList().ConvertAll(element => element.ToCaselawAppelateItemModel());

        /// <summary>
        /// Gets CaselawAppelateItemModel by title
        /// </summary>
        /// <param name="title">Item title</param>
        /// <returns>The <see cref="CaselawAppelateItemModel"/>.</returns>
        public CaselawAppelateItemModel GetCaselawAppelateItemModel(string title)
            => this.GetItemsList().Find(u => u.Title.Text == title).ToCaselawAppelateItemModel();
        
        /// <summary>
        /// Gets CaselawAppelateItemModel by title and flag
        /// </summary>
        /// <param name="title">Item title</param>
        /// <param name="flag">Is item has flag</param>
        /// <returns>The <see cref="CaselawAppelateItemModel"/>.</returns>
        public CaselawAppelateItemModel GetCaselawAppelateItemModel(string title, bool flag)
            => this.GetItemsList().Find(u => u.Title.Text == title && u.Flag == flag).ToCaselawAppelateItemModel();

        /// <summary>
        /// Gets CaselawAppelateItemModel by title and key cited
        /// </summary>
        /// <param name="title">Item title </param>
        /// <param name="keyCited">Is item key Cited.</param>
        /// <returns>The <see cref="CaselawAppelateItemModel"/>.</returns>
        public CaselawAppelateItemModel GetCaselawAppelateItemModelKeyCited(string title, bool keyCited)
            => this.GetItemsList().Find(u => u.Title.Text == title && u.KeyCited == keyCited).ToCaselawAppelateItemModel();

        /// <summary>
        /// Is desired CaselawAppelateItemModel exist on page
        /// </summary>
        /// <param name="model">CaselawAppelateItemModel expected model</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsHistoryItemModelExist(CaselawAppelateItemModel model)
            => this.GetItemsList().Any(u => u.Title.Text == model.Title && u.KeyCited == model.KeyCited
                && u.PrimaryCitation.Text == model.PrimaryCitation && u.Flag == model.Flag
                && u.CourtLine.Text == model.CourtLine
                && u.Date.Text == model.Date && u.DocketNumber == model.DocketNumber && u.ParallelCitation == model.ParallelCitation);

        /// <summary>
        /// Is desired CaselawAppelateItemModel exist on page
        /// </summary>
        /// <param name="index">Item index</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsItemCheckboxSelected(int index) => this.GetItemsList()[index].CheckBox.Selected;

        /// <summary>
        /// Get Direct History Subheading Text
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetDirectHistorySubheadingText() => DriverExtensions.GetText(DirectHistorySubheadingLocator);

        /// <summary>
        /// Determine the scale of the graphic. This changes based on clicking the + and - buttons in the toolbar.
        /// </summary>
        /// <returns>Value of the scale</returns>
        public double GetGraphicViewBoxScale()
        {
            string transform = DriverExtensions.WaitForElement(GraphicViewBoxLocator).GetAttribute("transform");
            string scaleValue = Regex.Matches(transform, "\\d.\\d+")[0].Value;
            double scale;

            double.TryParse(scaleValue, out scale);
            return scale;
        }

        /// <summary>
        /// Gets the number of historyItems
        /// </summary>
        /// <returns>count of items</returns>
        public int GetNumberOfHistoryItems() => this.GetItemsList().Count;

        /// <summary>
        /// Gets the number of links that have a link containing linkToGuid
        /// </summary>
        /// <returns>count of links</returns>
        public int GetNumberOfLinkToGuid()
            => DriverExtensions.GetElements(this.RelatedInfoDirectHistoryItemsList, LinkToGuidLocator).Count;

        /// <summary>
        /// Determines if the large history graphic in the body of the page is displayed or not.
        /// </summary>
        /// <returns>True if history graphic is displayed</returns>
        public bool IsHistoryGraphicDisplayed()
            => this.HistoryGraphic.Displayed && this.HistoryGraphic.GetAttribute("style").Contains("display: block;");

        /// <summary>
        /// Determines if the legend is displayed or not.
        /// </summary>
        /// <returns>True if legend is displayed</returns>
        public bool IsLegendDisplayed() => DriverExtensions.IsDisplayed(LegendLocator);

        /// <summary>
        /// Set checkboxes by index of Direct History Item
        /// </summary>
        /// <param name="selected">is text selected</param>
        /// <param name="indexesArray">numbers</param>
        public void SetCaselawAppelateItemCheckoxeByIndexes(bool selected, params int[] indexesArray)
            => indexesArray.ToList().ForEach(i => this.GetCheckBoxes().ElementAt(i).SetCheckbox(selected));

        /// <summary>
        /// Click on the first link to guide
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickLinkToGuide<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(LinkToGuidLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Gets the checkboxes
        /// </summary>
        /// <returns>List of IWebElements</returns>
        private IList<IWebElement> GetCheckBoxes()
            => DriverExtensions.GetElements(this.RelatedInfoDirectHistoryItemsList, CheckBoxInListLocator);

        /// <summary>
        /// Gets list of all items
        /// </summary>
        /// <returns>List of CaselawAppelateItems</returns>
        private List<CaselawAppelateItem> GetItemsList()
            => DriverExtensions.GetElements(this.RelatedInfoDirectHistoryItemsList, ItemInsideContainerLocator)
                .Select(u => new CaselawAppelateItem(u)).ToList();
    }
}