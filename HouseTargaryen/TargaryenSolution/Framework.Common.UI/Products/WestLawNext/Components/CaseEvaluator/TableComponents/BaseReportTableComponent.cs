namespace Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator.TableComponents
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Enums.Report;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// BaseReportTableComponent
    /// </summary>
    public abstract class BaseReportTableComponent : BaseModuleRegressionComponent
    {
        private const string TableElementLocatorByPositionLctMask = ".//tbody/tr[{0}]/td[{1}]//a";

        private const string TableElementLocatorByTextLctMask = ".//tbody//tr//a[contains(.,'{0}')]";

        private const string TableRootLctMask = "//*[contains(@id,'{0}')]";

        private static readonly By ShowMoreLinkLocator = By.XPath(".//a[contains(@class, 'co_documentReportMore')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseReportTableComponent"/> class.
        /// </summary>
        /// <param name="table">The table.</param>
        protected BaseReportTableComponent(ReportPageTables table)
            : this(EnumPropertyModelCache.GetEnumInfo<ReportPageTables, WebElementInfo>(table).Id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseReportTableComponent"/> class. 
        /// BaseReportTableComponent Constructor
        /// </summary>
        /// <param name="tableId">tab id</param>
        private BaseReportTableComponent(string tableId)
        {
            this.TableId = tableId;
        }

        /// <summary>
        /// The table id.
        /// </summary>
        protected string TableId { get; set; }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => By.XPath(string.Format(TableRootLctMask, this.TableId));

        /// <summary>
        /// generic clicking of links in tables
        /// </summary>
        /// <typeparam name="T">page</typeparam>
        /// <param name="colNum">The col number.</param>
        /// <param name="rowNum">The row number.</param>
        /// <returns>New instance of T page</returns>
        public T ClickDocLink<T>(int colNum, int rowNum) where T : ICreatablePageObject
        {
            this.GetWebElement(colNum, rowNum).ScrollToElementCenter();
            this.GetWebElement(colNum, rowNum).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks on a link with a specific text in the given table
        /// </summary>
        /// <param name="linkText">The link Text.</param>
        /// <typeparam name="T">page</typeparam>
        /// <returns>New instance of T page</returns>
        public T ClickDocLink<T>(string linkText) where T : ICreatablePageObject
        {
            this.ClickViewMoreLink();

            IWebElement doclinkElement = DriverExtensions.WaitForElement(
                DriverExtensions.GetElement(this.ComponentLocator),
                By.XPath(string.Format(TableElementLocatorByTextLctMask, linkText)));

            doclinkElement.ScrollToElementCenter();
            doclinkElement.Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// get the web element in the specified table location
        /// </summary>
        /// <param name="colNum">The col number.</param>
        /// <param name="rowNum">The row number.</param>
        /// <returns>The <see cref="IWebElement"/>.</returns>
        public IWebElement GetWebElement(int colNum, int rowNum)
            => DriverExtensions.WaitForElement(DriverExtensions.GetElement(this.ComponentLocator), By.XPath(string.Format(TableElementLocatorByPositionLctMask, rowNum, colNum)));

        /// <summary>
        /// Clicks on View More link
        /// </summary>
        protected void ClickViewMoreLink()
        {
            IWebElement viewMoreLinkElement = DriverExtensions.WaitForElement(DriverExtensions.GetElement(this.ComponentLocator), ShowMoreLinkLocator);
            viewMoreLinkElement.ScrollToElementCenter();
            viewMoreLinkElement.Click();
        }
    }
}