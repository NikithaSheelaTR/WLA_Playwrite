namespace Framework.Common.UI.Products.Shared.Components.CaseEvaluatorTab
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.Reports;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// represents the no data tab of the table of contents tab component of the case evaluator report page
    /// </summary>
    public class NoDataTabComponent : BaseModuleRegressionComponent
    {
        /// <summary>
        /// The no data tab id.
        /// </summary>
        private static readonly string NoDataTabId =
            EnumPropertyModelCache.GetEnumInfo<ReportPageTabs, WebElementInfo>(ReportPageTabs.NoDataTab).Id;

        /// <summary>
        /// The no data table id.
        /// </summary>
        private static readonly string NoDataTableId =
            EnumPropertyModelCache.GetEnumInfo<ReportPageTabs, WebElementInfo>(ReportPageTabs.NoDataTables)
                                  .Id;

        /// <summary>
        /// The tab id.
        /// </summary>
        private static readonly By TabIdLocator = By.Id(NoDataTabId);

        /// <summary>
        /// The table id.
        /// </summary>
        private static readonly By TableIdLocator =
            By.XPath("//div[@id='" + NoDataTableId + "']//li[not(contains(@class, 'ng-hide'))]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => By.Id(NoDataTabId);

        /// <summary>
        /// Get list of tables in the no data tab
        /// </summary>
        /// <returns> List of table names</returns>
        public List<string> GetNoDataTablesList()
        {
            DriverExtensions.WaitForElement(TabIdLocator).Click();
            List<IWebElement> tables = DriverExtensions.GetElements(TableIdLocator).ToList();
            return tables.Select(el => el.Text).ToList();
        }
    }
}