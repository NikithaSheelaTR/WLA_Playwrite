namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo.HistoryItem
{
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Models.HistoryItem;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// CaselawAppelateItem
    /// </summary>
    public class CaselawAppelateItem : HistoryItem
    {
        private static readonly By CheckBoxLocator = By.XPath(".//input[@type='checkbox']");

        private static readonly By LetterIconLocator =
            By.XPath(".//span[contains(@id, 'coid_relatedInfo_GraphicLetterTarget')]");

        private static readonly By RankLocator = By.XPath(".//span[@class='co_relatedInfo_resultList_rank']/strong");

        private static readonly By CourtLineLocator = By.XPath("./div/span[@class='co_relatedInfo_HistoryItem_CourtLine']");

        private static readonly By FlagLocator = By.XPath(".//a[contains(@id,'co_relatedInfo_keyciteItem_')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="CaselawAppelateItem"/> class. 
        /// </summary>
        /// <param name="itemContainer">IWebElement object</param>
        public CaselawAppelateItem(IWebElement itemContainer)
            : base(itemContainer)
        {
        }

        /// <summary>
        /// KeyCited
        /// </summary>
        public bool KeyCited => this.Container.GetAttribute("class").Contains("co_relatedInfo_listItem_Keycited");
        
        /// <summary>
        /// CourtLine
        /// </summary>
        public IWebElement CourtLine => DriverExtensions.WaitForElement(this.Container, CourtLineLocator);

        /// <summary>
        /// CheckBox
        /// </summary>
        public IWebElement CheckBox => DriverExtensions.WaitForElement(this.Container, CheckBoxLocator);

        /// <summary>
        /// LetterIcon
        /// </summary>
        public IWebElement LetterIcon => DriverExtensions.WaitForElement(this.Container, LetterIconLocator);

        /// <summary>
        /// Rank
        /// </summary>
        public int Rank => int.Parse(DriverExtensions.WaitForElement(this.Container, RankLocator).Text.Replace(".", string.Empty));

        /// <summary>
        ///Gets the KeyCite Flag from the document header
        /// </summary>
        /// <returns> The KeyCite Flag from the Document header </returns>
        public override KeyCiteFlag KeyCiteFlag
        {
            get
            {
                if (DriverExtensions.IsDisplayed(this.Container, FlagLocator))
                {
                    string flagClass = DriverExtensions.GetElement(this.Container, FlagLocator).GetAttribute("class");
                    return flagClass.GetEnumValueByPropertyModel<KeyCiteFlag, WebElementInfo>(model => model.ClassName);
                }

                return KeyCiteFlag.NoFlag;
            }
        }

        /// <summary>
        /// Convert item to model
        /// </summary>
        /// <returns>CaselawAppelateItemModel</returns>
        public CaselawAppelateItemModel ToCaselawAppelateItemModel()
        {
            var itemModel = new CaselawAppelateItemModel();

            itemModel.Title = this.Title.Text;
            itemModel.Date = this.Date.Text;
            itemModel.Flag = this.Flag;
            itemModel.ParallelCitation = this.ParallelCitation;
            itemModel.PrimaryCitation = this.PrimaryCitation.Text;
            itemModel.DocketNumber = this.DocketNumber;

            itemModel.KeyCited = this.KeyCited;
            itemModel.CourtLine = this.CourtLine.Text;
            itemModel.CheckBox = this.CheckBox.Displayed;
            itemModel.LetterIcon = this.LetterIcon.Displayed;
            itemModel.Rank = this.Rank; 
            itemModel.KeyCiteFlag = this.KeyCiteFlag;

            return itemModel;
        }
    }
}