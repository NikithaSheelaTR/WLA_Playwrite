namespace Framework.Common.UI.Products.Shared.Components.CaseEvaluatorTab
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.Reports;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.DataModel;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// represents the Included tab of the Table of Contents component on Case Evaluator Report Page
    /// </summary>
    public class IncludedTabComponent : BaseModuleRegressionComponent
    {
        private const string SectionToClickLctMask =
            ".//div[@id={0}]//li[normalize-space(a/text())={1}]/a[normalize-space(text())={1}]";

        private const string SectionToRemoveLctMask =
            ".//div[@id={0}]//li[normalize-space(a/text())={1}]/a[text()='Remove item']";

        private const string IncludedTableTitlesLctMask =
            "//div[@id={0}]//li[@class='co_tocTabsListItem ng-scope']//a[not(text()='Remove item')]";

        /// <summary>
        /// The included tab id.
        /// </summary>
        private static readonly string IncludedTabIdLocator =
            EnumPropertyModelCache.GetEnumInfo<ReportPageTabs, WebElementInfo>(ReportPageTabs.IncludedTab)
                                  .Id;

        /// <summary>
        /// The included tables id.
        /// </summary>
        private static readonly string IncludedTablesIdLocator =
            EnumPropertyModelCache.GetEnumInfo<ReportPageTabs, WebElementInfo>(
                ReportPageTabs.IncludedTables).Id;

        /// <summary>
        /// The tab id.
        /// </summary>
        private static readonly By TabId = By.Id(IncludedTabIdLocator);

        /// <summary>
        /// The table id.
        /// </summary>
        private static readonly By TableIdLocator =
             SafeXpath.BySafeXpath(IncludedTableTitlesLctMask, IncludedTablesIdLocator);

        private EnumPropertyMapper<ReportPageTabSectionNames, BaseTextModel> sectionNamesMap;

        /// <summary>
        /// Gets the ReportPageTabSectionNames enumeration to BaseTextModel map.
        /// </summary>
        protected EnumPropertyMapper<ReportPageTabSectionNames, BaseTextModel> SectionNamesMap
            =>
                this.sectionNamesMap =
                    this.sectionNamesMap
                    ?? EnumPropertyModelCache.GetMap<ReportPageTabSectionNames, BaseTextModel>();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => By.Id(IncludedTablesIdLocator);

        /// <summary>
        /// click on the table in the included tab by name
        /// </summary>
        /// <param name="section">
        /// constant enumeration of valid table names
        /// </param>
        public void ClickTableName(ReportPageTabSectionNames section)
        {
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
            By sectionToClick = SafeXpath.BySafeXpath(
                SectionToClickLctMask,
                IncludedTablesIdLocator,
                this.SectionNamesMap[section].Text);
            DriverExtensions.WaitForElement(sectionToClick).Click();
        }

        /// <summary>
        /// Get a list of all tables in the included tab
        /// </summary>
        /// <returns>List of table names</returns>
        public List<string> GetIncludedTablesList()
        {
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForElement(TabId, 45000);
            DriverExtensions.Click(TabId);
            var tables = new List<IWebElement>(DriverExtensions.GetElements(TableIdLocator));

            return tables.Select(el => el.Text).ToList();
        }

        /// <summary>
        /// Remove a table from the Included list by name
        /// Included list text is behind an a>, Excluded is not
        /// </summary>
        /// <param name="section"> Constant enumeration of valid table names </param>
        public void RemoveSectionByName(ReportPageTabSectionNames section) =>
            DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(
                SectionToRemoveLctMask,
                IncludedTablesIdLocator,
                this.SectionNamesMap[section].Text)).Click();
    }
}