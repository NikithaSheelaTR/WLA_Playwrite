namespace Framework.Common.UI.Products.WestLawNext.Dialogs
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.WestLawNext.Enums.BusinessLawCenter;
    using Framework.Common.UI.Products.WestLawNext.Items.BusinessLawCenter;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Filer Search Dialog for funds search
    /// </summary>
    public class FundsFilerSearchDialog : BaseModuleRegressionDialog
    {
        private static readonly By ItemLocator = By.XPath("//div[@class='co_pickListSectionHeader']");
        private static readonly By CloseButtonLocator = By.Id("coid_filerSearch_closeImage");
        private static readonly By SubItemLocator = By.XPath("//div[@class='co_pickListSectionBody ng-scope']");
        private static readonly By TitlesLocator = By.XPath("//a[@class='co_pickListTitle ng-binding']");

        private static readonly string ItemFormerNameLocatorPatternByTitle =
            "//a[text()='{0}']/parent::h3/following-sibling::div//span[@ng-if='fund.showFormerName']";

        private static readonly string ItemFormerNameLocatorPatternByCik =
            "//span[contains(.,  '{0}')]/parent::div/following-sibling::div//span[@ng-if='fund.showFormerName']";

        private static readonly string ItemCikLocatorPatternByFormerName =
            "//span[text()='{0}']/parent::div/preceding-sibling::div/span";

        /// <summary>
        /// Click close button
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <returns>New instance of object</returns>
        public T ClickCloseButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(CloseButtonLocator);

        /// <summary>
        /// Get Former Name By Cik
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>Former Name</returns>
        public string GetFundsItemTitle(string cik) => this.GetItemByCik(cik).GetTextFromOption(BlcItemOptions.Title);

        /// <summary>
        /// Get Former Name By Cik
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>Former Name</returns>
        public string GetFundsItemFormerName(string cik) => this.GetItemByCik(cik).GetTextFromOption(BlcItemOptions.FormerName);

        /// <summary>
        /// Get Count Of Former Names
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>Count of Former Names</returns>
        public int GetFundsItemFormerNamesCount(string cik)
        {
            string formerName = this.GetItemByCik(cik).GetTextFromOption(BlcItemOptions.FormerName);
            return formerName == string.Empty
                       ? 0
                       : formerName.Substring(formerName.IndexOf(':') + 1).Trim().Split(',').Select(str => str.Trim())
                                   .ToList().Count;
        }

        /// <summary>
        /// Get Funds Item Cik
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>Cik code</returns>
        public string GetFundsItemCik(string cik) => this.GetItemByCik(cik).GetTextFromOption(BlcItemOptions.Cik);

        /// <summary>
        /// Is Series Link Highlighted
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <param name="seriesName">Series Name</param>
        /// <returns>True - if it is highlighted, false - otherwise</returns>
        public bool IsSeriesLinkHighlighted(string cik, string seriesName) => this
            .GetSubItemByCik(cik).VerifyHighlightSeriesLink(seriesName, BlcItemOptions.HighlightedSeriesLink);

        /// <summary>
        /// Is Not Series Link Highlighted
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <param name="seriesName">Series Name</param>
        /// <returns>True - if it is NOT highlighted, false - otherwise</returns>
        public bool IsSeriesLinkNotHighlighted(string cik, string seriesName) => this
            .GetSubItemByCik(cik).VerifyHighlightSeriesLink(seriesName, BlcItemOptions.NotHighlightedSeriesLink);

        /// <summary>
        /// Is Series Class Highlighted
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <param name="classId">Class Id</param>
        /// <returns>True - if it is highlighted, false - otherwise</returns>
        public bool IsSeriesClassHighlighted(string cik, string classId) => this
            .GetSubItemByCik(cik).VerifyHighlightSeriesClass(classId, BlcItemOptions.HighlightedSeriesClass);

        /// <summary>
        /// Is Not Series Class Highlighted
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <param name="classId">Class Id</param>
        /// <returns>True - if it is NOT highlighted, false - otherwise</returns>
        public bool IsSeriesClassNotHighlighted(string cik, string classId) => this
            .GetSubItemByCik(cik).VerifyHighlightSeriesClass(classId, BlcItemOptions.NotHighlightedSeriesClass);

        /// <summary>
        /// Is Ticker Highlighted
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <param name="classId">Class Id</param>
        /// <param name="ticker">Ticker</param>
        /// <returns>True - if it is highlighted, false - otherwise</returns>
        public bool IsTickerHighlighted(string cik, string classId, string ticker) => this
            .GetSubItemByCik(cik).VerifyHighlightTicker(classId, ticker, BlcItemOptions.HighlightedTicker);

        /// <summary>
        /// Is Not Ticker Highlighted
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <param name="classId">Class Id</param>
        /// <param name="ticker">Ticker</param>
        /// <returns>True - if it is NOT highlighted, false - otherwise</returns>
        public bool IsTickerNotHighlighted(string cik, string classId, string ticker) => this
            .GetSubItemByCik(cik).VerifyHighlightTicker(classId, ticker, BlcItemOptions.NotHighlightedTicker);

        /// <summary>
        /// Get Count Of Series Former Names
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <param name="sid">SID</param>
        /// <param name="checkHighlighted">CheckHighlighted</param>
        /// <returns>Amount of Series Former Names</returns>
        public int GetCountOfSeriesFormerNames(string cik, string sid, bool checkHighlighted) => this
            .GetSubItemByCik(cik).GetSeriesFormerNames(sid, checkHighlighted).Count;

        /// <summary>
        /// Is Fund Series Former Name Displayed
        /// </summary>
        /// <param name="seriesFormerNameToCheck">Series Former Name To Check</param>
        /// <param name="sid">SID</param>
        /// <param name="cik">CIK</param>
        /// <param name="checkHighlighted">CheckHighlighted</param>
        /// <returns>True - if it is displayed, false - otherwise</returns>
        public bool IsFundSeriesFormerNameDisplayed(
            string seriesFormerNameToCheck,
            string sid,
            string cik,
            bool checkHighlighted) => seriesFormerNameToCheck == "none" || this
                                          .GetSubItemByCik(cik).IsFundSeriesFormerNameDisplayed(
                                              seriesFormerNameToCheck,
                                              sid,
                                              checkHighlighted);

        /// <summary>
        /// Get all fund titles
        /// </summary>
        /// <returns>Titles</returns>
        public List<string> GetAllFundTitles()
        {
            return DriverExtensions.GetElements(TitlesLocator).Select(item => item.GetText()).ToList();
        }

        /// <summary>
        /// Get item former name by title
        /// </summary>
        public string GetFundItemFormerNameByTitle(string title)
        {
            By elementLocator = By.XPath(string.Format(ItemFormerNameLocatorPatternByTitle, title));
            return DriverExtensions.IsElementPresent(elementLocator) ? DriverExtensions.WaitForElement(elementLocator).GetText() : string.Empty;
        }

        /// <summary>
        /// Get item former name by cik
        /// </summary>
        public string GetFundItemFormerNameByCik(string cik)
        {
            By elementLocator = By.XPath(string.Format(ItemFormerNameLocatorPatternByCik, cik));
            return DriverExtensions.IsElementPresent(elementLocator) ? DriverExtensions.WaitForElement(elementLocator).GetText() : string.Empty;
        }

        /// <summary>
        /// Get item CIK by Former name
        /// </summary>
        public string GetFundItemCikByFormerName(string formerName)
        {
            By elementLocator = By.XPath(string.Format(ItemCikLocatorPatternByFormerName, formerName));
            return DriverExtensions.IsElementPresent(elementLocator) ? DriverExtensions.WaitForElement(elementLocator).GetText() : string.Empty;
        }

        /// <summary>
        /// Get Count Of Former Names
        /// </summary>
        /// <param name="title">TITLE</param>
        /// <returns>Count of Former Names</returns>
        public int GetCountOfItemFormerNamesByTitle(string title)
        {
            string formerName = this.GetFundItemFormerNameByTitle(title);
            return formerName == string.Empty ? 0 : formerName.Substring(formerName.IndexOf(':') + 1).Trim().Split(',').Select(str => str.Trim()).AsEnumerable().Count();
        }

        /// <summary>
        /// Get list of items
        /// </summary>
        /// <returns>List of items</returns>
        private List<FundsFilerSearchItem> GetItems() => DriverExtensions
                                                         .GetElements(ItemLocator).Select(elem => new FundsFilerSearchItem(elem)).ToList();

        /// <summary>
        /// Get item by cik
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>Item</returns>
        private FundsFilerSearchItem GetItemByCik(string cik) =>
            this.GetItems().FirstOrDefault(item => item.IsItemContainsCik(cik));

        /// <summary>
        /// Get list of items
        /// </summary>
        /// <returns>List of items</returns>
        private List<FundsFilerSearchSubItem> GetSubItems() => DriverExtensions
                                                               .GetElements(SubItemLocator).Select(elem => new FundsFilerSearchSubItem(elem)).ToList();

        /// <summary>
        /// Get item by cik
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>Item</returns>
        private FundsFilerSearchSubItem GetSubItemByCik(string cik) =>
            this.GetSubItems().FirstOrDefault(item => item.IsItemContainsCik(cik));
    }
}