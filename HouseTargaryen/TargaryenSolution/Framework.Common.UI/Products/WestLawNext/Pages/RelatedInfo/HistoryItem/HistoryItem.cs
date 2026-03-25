namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo.HistoryItem
{
    using System;

    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Models.HistoryItem;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// HistoryItem
    /// </summary>
    public class HistoryItem : BaseItem
    {
        private static readonly By DateLocator = By.XPath("./div/span[@class='co_relatedInfo_HistoryItem_Date']");

        private static readonly By DocketNumberLocator = By.XPath("./div/span[@class='co_relatedInfo_HistoryItem_Docket'] | .//span[@class='coid_relatedInfo_docket']");

        private static readonly By ParallelCitationLocator =
            By.XPath("./div/span[@class='co_relatedInfo_HistoryItem_parallel_citation']");

        private static readonly By PrimaryCitationLocator =
            By.XPath("./div/span[@class='co_relatedInfo_HistoryItem_primary_citation']");

        private static readonly By TitleLocator = By.XPath(".//a[@guid]");

        private static readonly By FlagLinkLocator = By.XPath(".//a[.//img[contains(@src, 'flag')]]");

        private static readonly By KeyCiteFlagLocator = By.XPath(".//div[@class='co_relatedInfo_HistoryItem_TitleArea']/a");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="HistoryItem"/> class. 
        /// </summary>
        /// <param name="historyItemContainer">
        /// The history Item Container.
        /// </param>
        public HistoryItem(IWebElement historyItemContainer) : base(historyItemContainer)
        {
        }

        /// <summary>
        /// Title
        /// todo make private
        /// </summary>
        public IWebElement Title => DriverExtensions.WaitForElement(this.Container, TitleLocator);

        /// <summary>
        /// Date
        /// todo make private
        /// </summary>
        public IWebElement Date => DriverExtensions.WaitForElement(this.Container, DateLocator);

        /// <summary>
        /// Flag
        /// </summary>
        public bool Flag => DriverExtensions.IsDisplayed(this.Container, FlagLinkLocator);

        /// <summary>
        /// ParallelCitation
        /// </summary>
        public string ParallelCitation => DriverExtensions.IsElementPresent(this.Container, ParallelCitationLocator)
            ? DriverExtensions.GetElement(this.Container, ParallelCitationLocator).Text
            : string.Empty;

        /// <summary>
        /// PrimaryCitation
        /// todo make private
        /// </summary>
        public IWebElement PrimaryCitation => DriverExtensions.WaitForElement(this.Container, PrimaryCitationLocator);

        /// <summary>
        /// DocketNumber
        /// </summary>
        public string DocketNumber => DriverExtensions.IsElementPresent(this.Container, DocketNumberLocator)
            ? DriverExtensions.GetElement(this.Container, DocketNumberLocator).Text
            : string.Empty;

        /// <summary>
        /// Gets the key cite flag.
        /// </summary>
        public virtual KeyCiteFlag KeyCiteFlag
        {
            get
            {
                if (DriverExtensions.IsDisplayed(KeyCiteFlagLocator))
                {
                    string flagClass = DriverExtensions.GetAttribute("class",this.Container, KeyCiteFlagLocator);
                    return flagClass.GetEnumValueByPropertyModel<KeyCiteFlag, WebElementInfo>(
                        model => model.ClassName,
                        String.Empty,
                        @"Resources/EnumPropertyMaps");
                }

                return KeyCiteFlag.NoFlag;
            }
        }

        /// <summary>
        /// Convert item to model
        /// </summary>
        /// <returns>HistoryItemModel</returns>
        public HistoryItemModel ToHistoryItemModel()
        {
            var itemModel = new HistoryItemModel();

            itemModel.Title = this.Title.Text;
            itemModel.Date = this.Date.Text;
            itemModel.Flag = this.Flag;
            itemModel.ParallelCitation = this.ParallelCitation;
            itemModel.PrimaryCitation = this.PrimaryCitation.Text;
            itemModel.DocketNumber = this.DocketNumber;
            itemModel.KeyCiteFlag = this.KeyCiteFlag;

            return itemModel;
        }
    }
}