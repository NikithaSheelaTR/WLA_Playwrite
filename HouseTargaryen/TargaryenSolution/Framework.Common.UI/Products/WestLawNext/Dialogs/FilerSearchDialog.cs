namespace Framework.Common.UI.Products.WestLawNext.Dialogs
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Pages.AdvancedSearchTemplates;
    using Framework.Common.UI.Products.WestLawNext.Enums.BusinessLawCenter;
    using Framework.Common.UI.Products.WestLawNext.Items.BusinessLawCenter;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Filer Search dialog
    /// </summary>
    public class FilerSearchDialog : BaseModuleRegressionDialog
    {
        private static readonly By ItemLocator = By.XPath("//div[@id='coid_filerSearch_lightbox']//li[contains(@id,'cobalt_search_results_filers')]");
        private static readonly By TitleLocator = By.XPath("//div[@class='co_overlayBox_topLeft']//h3[contains(@id,'coid_lightboxAriaLabel')] | //div[@class='co_overlayBox_topLeft']//h2[contains(@id,'coid_lightboxAriaLabel')]");
        private static readonly By CompanyInvestigatorLinkLocator = By.XPath("//a[contains(text(),'Company Investigator')]");
        private static readonly By ResultNumberLocator = By.XPath("id('coid_filerSearch_contentDiv')/div/h3");
        private static readonly By CloseButtonLocator = By.Id("coid_filerSearch_closeImage");
        private static readonly By TitlesLocator = By.XPath("//li[contains(@id,'cobalt_search_results_filers')]//a");
        private static readonly string ItemFormerNameLocatorPattern =
            "//li[contains(@id,'cobalt_search_results_filers')]//a[text()='{0}']/parent::h3/following-sibling::div//div[@ng-if='company.showFormerName']";

        private static readonly string ItemHQLocationLocatorPattern =
            "//li[contains(@id,'cobalt_search_results_filers')]//a[text()='{0}']/parent::h3/following-sibling::div//div[@ng-if='company.showHQLoc']";

        private static readonly string ItemTickerLocatorPattern =
            "//li[contains(@id,'cobalt_search_results_filers')]//a[text()='{0}']/parent::h3/following-sibling::div//div[@ng-if='company.showTicker']";

        private static readonly string ItemStatusLocatorPattern =
            "//li[contains(@id,'cobalt_search_results_filers')]//a[text()='{0}']/parent::h3/following-sibling::span[contains(@class, 'co_floatRight co_disabled')]";
        
        /// <summary>
        /// Is Company Investigator link displayed
        /// </summary>
        /// <returns>True - if it is displayed, false - otherwise</returns>
        public bool IsCompanyInvestigatorLinkDisplayed() => DriverExtensions.IsDisplayed(CompanyInvestigatorLinkLocator, 5);

        /// <summary>
        /// Is number of search results displayed 
        /// </summary>
        /// <returns>True - if it is displayed, false - otherwise</returns>
        public bool IsNumberOfSearchResultsDisplayed() => DriverExtensions.IsDisplayed(ResultNumberLocator, 5);

        /// <summary>
        /// Get the number of search result
        /// </summary>
        /// <returns>Title of dialog</returns>
        public string GetFilerSearchDialogTitle() => DriverExtensions.WaitForElement(TitleLocator).GetText();

        /// <summary>
        /// Click close button
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <returns>New instance of object</returns>
        public T ClickCloseButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(CloseButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click Company Investigator link
        /// </summary>
        /// <returns>Company Investigator page</returns>
        public CompanyInvestigatorAstPage ClickCompanyInvestigatorLink()
        {
            DriverExtensions.WaitForElement(CompanyInvestigatorLinkLocator).Click();
            return new CompanyInvestigatorAstPage();
        }

        /// <summary>
        /// Get title by cik code
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>Title</returns>
        public string GetItemTitle(string cik) => this.GetItemByCik(cik).GetTextFromOption(BlcItemOptions.Title);

        /// <summary>
        /// Get Former Name By Cik
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>Former Name</returns>
        public string GetItemFormerName(string cik) => this.GetItemByCik(cik).GetTextFromOption(BlcItemOptions.FormerName);

        /// <summary>
        /// Get Count Of Former Names
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>Count of Former Names</returns>
        public int GetCountOfItemFormerNames(string cik)
        {
            string formerName = this.GetItemByCik(cik).GetTextFromOption(BlcItemOptions.FormerName);
            return formerName == string.Empty ? 0 : formerName.Substring(formerName.IndexOf(':') + 1).Trim().Split(',').Select(str => str.Trim()).AsEnumerable().Count();
        }

        /// <summary>
        /// Get company Ticker by cik
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>Ticker</returns>
        public string GetItemTicker(string cik) => this.GetItemByCik(cik).GetTickerText(BlcItemOptions.Ticker);

        /// <summary>
        /// Get company Location Of Inc by cik
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>Location Of Inc</returns>
        public string GetItemLocationOfInc(string cik) => this.GetItemByCik(cik).GetTextFromOption(BlcItemOptions.LocationInc);

        /// <summary>
        /// Get Location Of HQ by cik
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>Location Of HQ</returns>
        public string GetItemLocationOfHq(string cik) => this.GetItemByCik(cik).GetTextFromOption(BlcItemOptions.LocationHq);

        /// <summary>
        /// Get Primary sic by cik
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>Primary sic</returns>
        public string GetItemPrimarySic(string cik) => this.GetItemByCik(cik).GetTextFromOption(BlcItemOptions.PrimarySic);

        /// <summary>
        /// Get Status by cik
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>Status</returns>
        public string GetItemStatus(string cik) => this.GetItemByCik(cik).GetTextFromOption(BlcItemOptions.Status);

        /// <summary>
        /// Get Naic by cik
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>Naic</returns>
        public string GetItemNaic(string cik) => this.GetItemByCik(cik).GetTextFromOption(BlcItemOptions.Naic);

        /// <summary>
        /// Get all item titles 
        /// </summary>
        /// <returns>Titles</returns>
        public List<string> GetAllItemTitles()
        {
            return DriverExtensions.GetElements(TitlesLocator).Select(item => item.GetText()).ToList();
        }

        /// <summary>
        /// Get item status by title
        /// </summary>
        public string GetItemStatusByTitle(string title)
        {
            By elementLocator = By.XPath(string.Format(ItemStatusLocatorPattern, title));
            return DriverExtensions.IsElementPresent(elementLocator) ? DriverExtensions.WaitForElement(elementLocator).GetText() : string.Empty;
        }

        /// <summary>
        /// Get item HQ Location by title
        /// </summary>
        public string GetItemHQLocationByTitle(string title)
        {
            By elementLocator = By.XPath(string.Format(ItemHQLocationLocatorPattern, title));
            return DriverExtensions.IsElementPresent(elementLocator) ? DriverExtensions.WaitForElement(elementLocator).GetText() : string.Empty;
        }

        /// <summary>
        /// Get item Ticker by title
        /// </summary>
        public string GetItemTickerByTitle(string title)
        {
            By elementLocator = By.XPath(string.Format(ItemTickerLocatorPattern, title));
            return DriverExtensions.IsElementPresent(elementLocator) ? DriverExtensions.WaitForElement(elementLocator).GetText() : string.Empty;
        }

        /// <summary>
        /// Get item former name by title
        /// </summary>
        public string GetItemFormerNameByTitle(string title)
        {
             By elementLocator = By.XPath(string.Format(ItemFormerNameLocatorPattern, title));
             return DriverExtensions.IsElementPresent(elementLocator) ? DriverExtensions.WaitForElement(elementLocator).GetText() : string.Empty;
        }

        /// <summary>
        /// Get Count Of Former Names
        /// </summary>
        /// <param name="title">TITLE</param>
        /// <returns>Count of Former Names</returns>
        public int GetCountOfItemFormerNamesByTitle(string title)
        {
            string formerName = this.GetItemFormerNameByTitle(title);
            return formerName == string.Empty ? 0 : formerName.Substring(formerName.IndexOf(':') + 1).Trim().Split(',').Select(str => str.Trim()).AsEnumerable().Count();
        }

        /// <summary>
        /// Get list of items
        /// </summary>
        /// <returns>List of items</returns>
        private List<FilerSearchItem> GetItems()
            => DriverExtensions.GetElements(ItemLocator)
                               .Select(elem => new FilerSearchItem(elem))
                               .ToList();

        /// <summary>
        /// Get item by cik
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>Item</returns>
        private FilerSearchItem GetItemByCik(string cik) => this.GetItems().FirstOrDefault(item => item.IsItemContainsCik(cik));
    }
}