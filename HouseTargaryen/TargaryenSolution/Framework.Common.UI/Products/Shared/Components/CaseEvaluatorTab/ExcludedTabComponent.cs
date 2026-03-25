namespace Framework.Common.UI.Products.Shared.Components.CaseEvaluatorTab
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.Reports;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// represents the excluded tab of the table of contents component of the case evaluator report page
    /// </summary>
    public class ExcludedTabComponent : BaseModuleRegressionComponent
    {
        private const string SectionLctMask = ".//div[@id={0}]//li[normalize-space(text())={1}]/a[text()='Add item']";

        /// <summary>
        /// The excluded tables id.
        /// </summary>
        private static readonly string ExcludedTablesId =
            EnumPropertyModelCache.GetEnumInfo<ReportPageTabs, WebElementInfo>(
                ReportPageTabs.ExcludedTables).Id;

        /// <summary>
        /// The tab id.
        /// </summary>
        private static readonly By TabIdLocator = By.Id("excludedTab");

        /// <summary>
        /// The table id.
        /// </summary>
        private static readonly By TableIdLocator =
            By.XPath("//div[@id='" + ExcludedTablesId + "']//li[not(contains(@class, 'ng-hide'))]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => By.Id(ExcludedTablesId);

        /// <summary>
        /// Get list of all tables in the excluded tab
        /// </summary>
        /// <returns>List of table names</returns>
        public List<string> GetExcludedTablesList()
        {
            DriverExtensions.WaitForElement(TabIdLocator).Click();
            IReadOnlyCollection<IWebElement> tables = DriverExtensions.GetElements(TableIdLocator);

            return
                tables.Where(el => !string.IsNullOrEmpty(el.Text))
                      .Skip(1)
                      .Select(
                          el =>
                              el.Text.Substring(0, el.Text.IndexOf("\r", StringComparison.InvariantCultureIgnoreCase)))
                      .ToList();
        }

        /// <summary>
        /// Restore a table in the Excluded tab by name
        /// </summary>
        /// <param name="section"> constant enumeration of table names </param>
        public void RestoreSectionByName(ReportPageTabSectionNames section)
        {
            // Included list text is behind an <a>, Excluded is not
            By sectionToRemove = SafeXpath.BySafeXpath(SectionLctMask, ExcludedTablesId, section.GetEnumTextValue());
            DriverExtensions.WaitForElement(sectionToRemove).Click();
        }
    }
}