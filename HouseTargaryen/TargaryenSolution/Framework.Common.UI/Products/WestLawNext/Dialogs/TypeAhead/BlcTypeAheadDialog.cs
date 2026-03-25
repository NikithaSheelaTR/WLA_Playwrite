namespace Framework.Common.UI.Products.WestLawNext.Dialogs.TypeAhead
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Enums.BusinessLawCenter;
    using Framework.Common.UI.Products.WestLawNext.Items.BusinessLawCenter;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// BLC Type Ahead Dialog
    /// </summary>
    public class BlcTypeAheadDialog : BaseModuleRegressionDialog
    {
        private static readonly By ItemLocator = By.XPath("//li[@class = 'co_typeAheadItem co_typeAheadScroll']");

        private static readonly By ViewExpandedListLocator = By.Id("viewMoreLink");

        private EnumPropertyMapper<TypeAheadDialogTabs, TypeAheadInfo> typeAheadMap;

        /// <summary>
        /// Gets the TypeAheadDialogTabs enumeration to TypeAheadInfo map.
        /// </summary>
        protected EnumPropertyMapper<TypeAheadDialogTabs, TypeAheadInfo> TypeAheadMap
            => this.typeAheadMap = this.typeAheadMap ?? EnumPropertyModelCache.GetMap<TypeAheadDialogTabs, TypeAheadInfo>();

        /// <summary>
        /// Click option
        /// </summary>
        /// <param name="option">Option</param>
        public void ClickTypeAheadOption(TypeAheadDialogTabs option)
            => DriverExtensions.WaitForElementDisplayed(By.XPath(this.TypeAheadMap[option].LocatorString)).Click();

        /// <summary>
        /// Checks that category is present in the suggest drop down
        /// </summary>
        /// <param name="category">Category</param>
        /// <returns>True - if it is displayed, false - otherwise</returns>
        public bool IsSearchCategoryDisplayed(TypeAheadDialogTabs category)
            => DriverExtensions.IsDisplayed(By.XPath(this.TypeAheadMap[category].LocatorString));

        /// <summary>
        /// Click found result by title
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="title">Title</param>
        /// <returns>New instance of object</returns>
        public T ClickFoundResultByItemTitle<T>(string title) where T : ICreatablePageObject
        {
            this.GetItems().FirstOrDefault(item => item.Title.Equals(title))?.ClickItem();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Is title of item displayed
        /// </summary>
        /// <param name="title">Title</param>
        /// <returns>True - if it is displayed, false - otherwise</returns>
        public bool IsTitleOfItemDisplayed(string title)
            => this.GetItems().First(item => item.Title.Equals(title)).IsTitleOfItemDisplayed();

        /// <summary>
        /// Clicks on the View Expanded List link
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <returns>New instance of object</returns>
        public T ClickViewExpandedListLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(ViewExpandedListLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get item title by cik
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>Title</returns>
        public string GetItemTitle(string cik) => this.GetItemByCik(cik).GetTitle();

        /// <summary>
        /// Get item Primary Sic by cik
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>Primary Sic</returns>
        public string GetItemPrimarySic(string cik) => this.GetItemByCik(cik).GetPrimarySic();

        /// <summary>
        /// Get item Ticker by cik
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>Ticker</returns>
        public string GetItemTicker(string cik) => this.GetItemByCik(cik).GetTicker();

        /// <summary>
        /// Get item Currently Known As Title by cik
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>Currently Known As Title</returns>
        public string GetItemCurrentlyKnownAsTitle(string cik) => this.GetItemByCik(cik).GetCurrentlyKnownAsTitle();

        /// <summary>
        /// Get item Former Name by cik
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>Former Name</returns>
        public string GetItemFormerName(string cik) => this.GetItemByCik(cik).GetFormerName();

        /// <summary>
        /// Get item Series Former Name by cik
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>Former Name</returns>
        public string GetItemSeriesFormerName(string cik) => this.GetItemByCik(cik).GetSeriesFormerName();

        /// <summary>
        /// Get Item Series Name
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>Series Name</returns>
        public string GetItemSeriesName(string cik) => this.GetItemByCik(cik).GetSeriesName();

        /// <summary>
        /// Get list of items
        /// </summary>
        /// <returns>List of items</returns>
        private List<BlcTypeAheadItem> GetItems()
            => DriverExtensions.GetElements(ItemLocator).Select(elem => new BlcTypeAheadItem(elem)).ToList();

        /// <summary>
        /// Get item by cik
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>The <see cref="BlcTypeAheadItem"/>.</returns>
        private BlcTypeAheadItem GetItemByCik(string cik)
            => this.GetItems().FirstOrDefault(item => item.IsItemContainsCik(cik));
    }
}