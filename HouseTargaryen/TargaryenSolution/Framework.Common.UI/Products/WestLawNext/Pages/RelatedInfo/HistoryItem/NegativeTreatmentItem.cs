namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo.HistoryItem
{
    using Framework.Common.UI.Products.WestLawNext.Models.HistoryItem;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// NegativeTreatmentPageItem
    /// </summary>
    public class NegativeTreatmentItem : HistoryItem
    {
        private static readonly By CourtLineLocator = By.XPath("./div/span[@class='co_relatedInfo_HistoryItem_CourtLine']");

        private static readonly By BadgeLocator = By.XPath(".//div[@class='co_relatedInfo_HistoryItem_TitleArea']/img");

        private static readonly By CheckBoxLocator = By.XPath(".//input[@type='checkbox']");

        /// <summary>
        /// Initializes a new instance of the <see cref="NegativeTreatmentItem"/> class. 
        /// </summary>
        /// <param name="itemContainer">ItemContainer IWebElement</param>
        public NegativeTreatmentItem(IWebElement itemContainer)
            : base(itemContainer)
        {
        }

        /// <summary>
        /// CourtLine
        /// </summary>
        public IWebElement CourtLine => DriverExtensions.WaitForElement(this.Container, CourtLineLocator);

        /// <summary>
        /// Badge
        /// </summary>
        public string Badge => DriverExtensions.WaitForElement(this.Container, BadgeLocator).GetAttribute("title");

        /// <summary>
        /// CheckBox
        /// </summary>
        public IWebElement CheckBox => DriverExtensions.WaitForElement(this.Container, CheckBoxLocator);

        /// <summary>
        /// Convert item to model
        /// </summary>
        /// <returns>NegativeTreatmentItemModel</returns>
        public NegativeTreatmentItemModel ToNegativeTreatmentItemModel()
        {
            var itemModel = new NegativeTreatmentItemModel();

            itemModel.Title = this.Title.Text;
            itemModel.Date = this.Date.Text;
            itemModel.Flag = this.Flag;
            itemModel.ParallelCitation = this.ParallelCitation;
            itemModel.PrimaryCitation = this.PrimaryCitation.Text;
            itemModel.DocketNumber = this.DocketNumber;

            itemModel.CheckBox = this.CheckBox.Displayed;
            itemModel.CourtLine = this.CourtLine.Text;
            itemModel.Badge = this.Badge;

            return itemModel;
        }
    }
}