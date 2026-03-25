namespace Framework.Common.UI.Products.Shared.Components.IpTools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.IpTools;
    using Framework.Common.UI.Products.Shared.Enums.SortingTypes;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Pages.IpTools;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Base Grid Component for IP Tools pages
    /// </summary>
    public class PatentTextMateGridComponent : IpToolsBaseGridComponent
    {
        private const string TableRowLctMask = "//tbody//span[@id='coid_relatedInfo_resultList_rank_{0}']//..//a";

        private const string RowTitleLctMask = "//td[@class='co_detailsTable_owner' and contains(text(),'{0}')]/..//td//a[contains(@class,'co_relatedInfo')]";

        private const string TableRowCheckBoxByOwnerLocator = "//td[(@class='co_detailsTable_owner') and contains(text(),'{0}')]/..//input";

        private static readonly By ContainerLocator = By.Id("co_relatedInfo_table_claimsAnalyzer");

        private EnumPropertyMapper<PatentTextMateGridColumns, WebElementInfo> textMateColumnsMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Patent textMate Grid Columns mapper
        /// </summary>
        protected EnumPropertyMapper<PatentTextMateGridColumns, WebElementInfo> PatentTextMateColumnsMap
            => this.textMateColumnsMap = this.textMateColumnsMap ?? EnumPropertyModelCache.GetMap<PatentTextMateGridColumns, WebElementInfo>();

        /// <summary>
        /// Checks whether specific column is sortable or not
        /// </summary>
        /// <param name="column">Column title</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsColumnSortable(PatentTextMateGridColumns column)
            => DriverExtensions.GetElement(By.XPath(string.Format(this.PatentTextMateColumnsMap[column].LocatorString)))
                .GetAttribute("class").Contains("sortable");

        /// <summary>
        /// Clicks on the specific Column Header
        /// </summary>
        /// <param name="column">Column name</param>
        /// <returns>The <see cref="PatentTextMateResultPage"/>.</returns>
        public PatentTextMateResultPage ClickColumnHeader(PatentTextMateGridColumns column)
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(this.PatentTextMateColumnsMap[column].LocatorString))).Click();
            return new PatentTextMateResultPage();
        }

        /// <summary>
        /// Checks the grid items by owner
        /// </summary>
        /// <param name="owner">
        /// The items Numbers To Click.
        /// </param>
        public void CheckGridItemsByOwner(string owner)
            => DriverExtensions.GetElements(DriverExtensions.WaitForElement(this.ComponentLocator), By.XPath(string.Format(TableRowCheckBoxByOwnerLocator, owner)))
                .ToList().ForEach(item => item.SetCheckbox(true));

        /// <summary>
        /// Gets the document title at specific place
        /// </summary>
        /// <param name="itemIndexes">The item Index.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public List<string> GetDocumentTitlesByIndexes(params int[] itemIndexes)
            => itemIndexes.ToList()
                .Select(item => DriverExtensions.GetElement(DriverExtensions.WaitForElement(this.ComponentLocator), By.XPath(string.Format(TableRowLctMask, item))).Text)
                .ToList();

        /// <summary>
        /// Retrieves the value of a column, indicated by the column name, of an item indicated by item's numerical position in the grid
        /// </summary>
        /// <param name="owner">
        /// The item Index.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public List<string> GetColumnTitleValueByOwner(string owner)
            => DriverExtensions.GetElements(DriverExtensions.WaitForElement(this.ComponentLocator), By.XPath(string.Format(RowTitleLctMask, owner)))
                .Select(x => x.GetAttribute("title")).ToList();

        /// <summary>
        /// Checks sorting of specific column
        /// </summary>
        /// <param name="column">Column title</param>
        /// <param name="sortByType">The sort by type.</param>
        /// <param name="stringComparerCase">The string Comparer Case.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool VerifyColumnSorting(
            PatentTextMateGridColumns column,
            Type sortByType,
            StringComparer stringComparerCase,
            SortOrder sortOrder = SortOrder.Ascending) => this.VerifySorting(this.GetColumnItems(column), sortByType, stringComparerCase, sortOrder);

        /// <summary>
        /// Gets all items of specific column
        /// </summary>
        /// <param name="column">Column title</param>
        /// <returns>List of IWebElements</returns>
        private List<string> GetColumnItems(PatentTextMateGridColumns column)
            => DriverExtensions.GetElements(DriverExtensions.WaitForElement(this.ComponentLocator), By.XPath(this.PatentTextMateColumnsMap[column].LocatorMask))
                .Select(x => x.Text != string.Empty ? x.Text : x.GetAttribute("title")).ToList();
    }
}