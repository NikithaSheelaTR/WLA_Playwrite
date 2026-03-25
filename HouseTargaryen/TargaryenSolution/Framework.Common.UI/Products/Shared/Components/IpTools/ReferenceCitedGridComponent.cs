namespace Framework.Common.UI.Products.Shared.Components.IpTools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.IpTools;
    using Framework.Common.UI.Products.Shared.Enums.SortingTypes;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Pages.IpTools;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Grid component for Reference Cited page
    /// </summary>
    public class ReferenceCitedGridComponent : IpToolsBaseGridComponent
    {
        private const string TableRowLctMask =
            "//tbody//span[@id='coid_relatedInfo_resultList_rank_{0}']//..//a[contains(@class,'co_relatedInfo_grid_documentLink')]";

        private static readonly By ContainerLocator = By.XPath("//table[@id='co_relatedInfo_table_ipTools']");

        private static readonly By KeyCiteFlagLocator = By.XPath("//a[contains(@id, 'coid_relatedInfo_keyciteItem')]");

        private EnumPropertyMapper<ReferenceCitedGridColumns, WebElementInfo> referenceCitedColumnsMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Patent textMate Grid Columns mapper
        /// </summary>
        protected EnumPropertyMapper<ReferenceCitedGridColumns, WebElementInfo> ReferenceCitedColumnsMap
            => this.referenceCitedColumnsMap = this.referenceCitedColumnsMap ?? EnumPropertyModelCache.GetMap<ReferenceCitedGridColumns, WebElementInfo>();

        /// <summary>
        /// Checks whether specific column is sortable or not
        /// </summary>
        /// <param name="column">Column title</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsColumnSortable(ReferenceCitedGridColumns column)
            => DriverExtensions.GetElement(By.XPath(string.Format(this.ReferenceCitedColumnsMap[column].LocatorString)))
                .GetAttribute("class").Contains("sortable");

        /// <summary>
        /// Clicks on specific column header
        /// </summary>
        /// <param name="column">Column title</param>
        /// <returns>The <see cref="ReferencesCitedResultPage"/>.</returns>
        public ReferencesCitedResultPage ClickColumnHeader(ReferenceCitedGridColumns column)
        {
            DriverExtensions.WaitForElement(
                DriverExtensions.WaitForElement(this.ComponentLocator),
                By.XPath(this.ReferenceCitedColumnsMap[column].LocatorString)).Click();
            return new ReferencesCitedResultPage();
        }

        /// <summary>
        /// Checks sorting of specific column
        /// </summary>
        /// <param name="column">Column title</param>
        /// <param name="sortByType">The sort by type.</param>
        /// <param name="stringComparerCase">The string Comparer Case.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool VerifyColumnSorting(
            ReferenceCitedGridColumns column,
            Type sortByType,
            StringComparer stringComparerCase,
            SortOrder sortOrder = SortOrder.Ascending) => this.VerifySorting(this.GetColumnItems(column), sortByType, stringComparerCase, sortOrder);

        /// <summary>
        /// Retrieves the value of a column, indicated by the column name, of an item indicated by item's numerical position in the grid
        /// </summary>
        /// <param name="itemIndexes">
        /// The item Index.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public List<string> GetDocumentTitleByRowIndexes(params int[] itemIndexes)
            => itemIndexes.ToList().Select(item => DriverExtensions.GetElement(By.XPath(string.Format(TableRowLctMask, item))).Text).ToList();

        /// <summary>
        /// Clicks on flag.
        /// </summary>
        /// <returns>The <see cref="NegativeTreatmentPage"/>.</returns>
        public NegativeTreatmentPage ClickFlag()
        {
            DriverExtensions.WaitForElement(DriverExtensions.WaitForElement(this.ComponentLocator), KeyCiteFlagLocator).Click();
            return DriverExtensions.CreatePageInstance<NegativeTreatmentPage>();
        }

        /// <summary>
        /// Gets all items of specific column
        /// </summary>
        /// <param name="column">Column title</param>
        /// <returns>List of IWebElements</returns>
        private List<string> GetColumnItems(ReferenceCitedGridColumns column)
            => DriverExtensions.GetElements(DriverExtensions.WaitForElement(this.ComponentLocator), By.XPath(this.ReferenceCitedColumnsMap[column].LocatorMask))
                .Select(x => x.Text != string.Empty ? x.Text : x.GetAttribute("title")).ToList();
    }
}