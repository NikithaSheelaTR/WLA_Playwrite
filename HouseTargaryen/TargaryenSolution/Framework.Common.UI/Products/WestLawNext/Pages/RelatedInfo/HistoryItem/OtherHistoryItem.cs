namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo.HistoryItem
{
    using Framework.Common.UI.Products.WestLawNext.Models.HistoryItem;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// OtherHistoryPageItem
    /// </summary>
    public class OtherHistoryItem : HistoryItem
    {
        private static readonly By CourtLineLocator = By.XPath("./div/span[@class='co_relatedInfo_HistoryItem_CourtLine']");

        private static readonly By CheckBoxLocator = By.XPath(".//input[@type='checkbox']");

        private static readonly By RankLocator = By.XPath(".//span[@class='co_relatedInfo_resultList_rank']/strong");

        /// <summary>
        /// Initializes a new instance of the <see cref="OtherHistoryItem"/> class. 
        /// </summary>
        /// <param name="itemContainer">ItemContainer IWebElement</param>
        public OtherHistoryItem(IWebElement itemContainer)
            : base(itemContainer)
        {
        }

        /// <summary>
        /// Rank
        /// </summary>
        public int Rank => int.Parse(DriverExtensions.WaitForElement(this.Container, RankLocator).Text.Replace(".", string.Empty));

        /// <summary>
        /// CourtLine
        /// </summary>
        public IWebElement CourtLine => DriverExtensions.WaitForElement(this.Container, CourtLineLocator);

        /// <summary>
        /// CheckBox
        /// </summary>
        public IWebElement CheckBox => DriverExtensions.WaitForElement(this.Container, CheckBoxLocator);

        /// <summary>
        /// Convert item to model
        /// </summary>
        /// <returns>OtherHistoryItemModel</returns>
        public OtherHistoryItemModel ToOtherHistoryItemModel()
        {
            var itemModel = new OtherHistoryItemModel();

            itemModel.Title = this.Title.Text;
            itemModel.Date = this.Date.Text;
            itemModel.Flag = this.Flag;
            itemModel.ParallelCitation = this.ParallelCitation;
            itemModel.PrimaryCitation = this.PrimaryCitation.Text;
            itemModel.DocketNumber = this.DocketNumber;

            itemModel.CourtLine = this.CourtLine.Text;
            itemModel.CheckBox = this.CheckBox.Displayed;
            itemModel.Rank = this.Rank;

            return itemModel;
        }
    }
}