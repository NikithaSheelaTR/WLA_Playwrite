namespace Framework.Common.UI.Products.WestLawNext.Components.IpTools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components.IpTools;
    using Framework.Common.UI.Products.Shared.Enums.SortingTypes;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Enums.IpTools;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Grid Component on PatentFileHistoryResultPage
    /// </summary>
    public class PatentFileHistoryGridComponent : IpToolsBaseGridComponent
    {
        private static readonly By TableLocator
            = By.XPath("//div[@id='coid_relatedInfo_contentResult_container']//table[contains(@class,'co_ipDetailsTable')]");

        private static readonly By ContainerLocator = By.Id("co_patentFileHistory_contentContainer");

        private EnumPropertyMapper<PatentFileHistoryGridColumns, WebElementInfo> patentFileHistoryColumnsMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Patent File History Grid Columns mapper
        /// </summary>
        private EnumPropertyMapper<PatentFileHistoryGridColumns, WebElementInfo> PatentFileHistoryColumnsMap
            => this.patentFileHistoryColumnsMap
                = this.patentFileHistoryColumnsMap ?? EnumPropertyModelCache.GetMap<PatentFileHistoryGridColumns, WebElementInfo>();

        /// <summary>
        /// Checks whether specific column is displayed or not
        /// </summary>
        /// <param name="column">Column title </param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsColumnHeaderDisplayed(PatentFileHistoryGridColumns column)
            => DriverExtensions.GetElement(DriverExtensions.WaitForElement(TableLocator), By.XPath(this.PatentFileHistoryColumnsMap[column].LocatorString))
                .Displayed;

        /// <summary>
        /// Clicks on specific column header
        /// </summary>
        /// <param name="column">Column title</param>
        public void ClickColumnHeader(PatentFileHistoryGridColumns column)
            => DriverExtensions.WaitForElement(DriverExtensions.WaitForElement(TableLocator), By.XPath(this.PatentFileHistoryColumnsMap[column].LocatorString)).Click();

        /// <summary>
        /// Checks sorting of specific column
        /// </summary>
        /// <param name="column">Column title</param>
        /// <param name="sortByType">The sort by type.</param>
        /// <param name="stringComparerCase">The string Comparer Case.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool VerifyColumnSorting(
            PatentFileHistoryGridColumns column,
            Type sortByType,
            StringComparer stringComparerCase,
            SortOrder sortOrder = SortOrder.Ascending) => this.VerifySorting(this.GetColumnItems(column), sortByType, stringComparerCase, sortOrder);

        /// <summary>
        /// Gets all items of specific column
        /// </summary>
        /// <param name="column">Column title</param>
        /// <returns>List of IWebElements</returns>
        private List<string> GetColumnItems(PatentFileHistoryGridColumns column)
            => DriverExtensions.GetElements(DriverExtensions.WaitForElement(TableLocator), By.XPath(this.PatentFileHistoryColumnsMap[column].LocatorString))
                .Skip(1).Select(x => x.Text).ToList();
    }
}
