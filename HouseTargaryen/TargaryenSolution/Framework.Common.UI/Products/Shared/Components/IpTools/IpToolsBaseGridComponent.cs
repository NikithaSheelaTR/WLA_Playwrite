namespace Framework.Common.UI.Products.Shared.Components.IpTools
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Enums.SortingTypes;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Models.IpTools;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// BaseGridComponent for ip tools pages
    /// </summary>
    public abstract class IpToolsBaseGridComponent : BaseModuleRegressionComponent
    {
        private const string TableRowCheckboxLocator = "//input[contains(@id,'coid_relatedInfo_resultList_checkbox_') and (@rank='{0}')]";

        private static readonly By RowLocator = By.XPath(".//tbody/tr");

        private static readonly By ColumnHeadersLocator = By.XPath("//th");

        private static readonly By TableLocator = By.XPath("//table[contains(@id,'co_relatedInfo_table')]");

        private static readonly By CitationElementLocator = By.XPath(".//span[contains(@id, 'citation')]");

        private static readonly By LinkLocator = By.XPath(".//a[contains(@class,'co_relatedInfo_grid_documentLink')]");

        private static readonly By SnippetLocator = By.XPath(".//div[@class='co_snippet' and contains(@id,'documentSummary')]//span");

        private static readonly By FlagLocator = By.XPath(".//a[contains(@id,'coid_relatedInfo_keyciteItem')]");

        /// <summary>
        /// Checks the grid item by index
        /// </summary>
        /// <param name="itemsNumberToCheck">
        /// The items Numbers To Click.
        /// </param>
        public void SelectItemByIndex(params int[] itemsNumberToCheck)
            => itemsNumberToCheck.ToList()
                .ForEach(item => DriverExtensions.WaitForElement(By.XPath(string.Format(TableRowCheckboxLocator, item))).SetCheckbox(true));

        /// <summary>
        /// Gets Titles of all columns
        /// </summary>
        /// <returns>List of strings</returns>
        public List<string> GetAllColumnsHeadersTitles() =>
         DriverExtensions.GetElements(DriverExtensions.WaitForElement(TableLocator), ColumnHeadersLocator)
                            .SkipWhile(
                                columnHeaderItem =>
                                    columnHeaderItem.Text == String.Empty && columnHeaderItem.FindElements(By.XPath("./input")).Count == 0)
                            .Select(
                                columnHeaderItem =>
                                    columnHeaderItem.Text != string.Empty
                                        ? columnHeaderItem.Text
                                        : DriverExtensions.GetElement(columnHeaderItem, By.XPath("./input"))
                                                          .GetAttribute("title")).ToList();

      

        /// <summary>
        /// Gets count of table rows
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetTableRowsCount() => DriverExtensions.GetElements(DriverExtensions.WaitForElement(TableLocator), RowLocator).Count;

        /// <summary>
        /// Gets Document models
        /// </summary>
        /// <returns>list of document models</returns>
        public List<DocumentInfoModel> GetDocumentInfoModels() => this.GetAllDocumentCellItems().Select(x => this.GetDocumentInfoModelByElement(x)).ToList();

        /// <summary>
        /// Clicks on document link from grid
        /// </summary>
        /// <param name="index">The index.</param>
        /// <typeparam name="T">T</typeparam>
        /// <returns>New Page Object</returns>
        public T ClickLink<T>(int index = 0) where T : ICreatablePageObject
        {
            DriverExtensions.GetElements(DriverExtensions.WaitForElement(TableLocator), LinkLocator).ElementAt(index).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// The verify sorting.
        /// </summary>
        /// <param name="columnItems">The column items.</param>
        /// <param name="sortByType">The sort by type.</param>
        /// <param name="stringComparerCase">The string Comparer Case.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns>
        /// True or false
        /// </returns>
        protected bool VerifySorting(
            List<string> columnItems,
            Type sortByType,
            StringComparer stringComparerCase,
            SortOrder sortOrder = SortOrder.Ascending)
                => columnItems.SequenceEqual(this.GetExpectedValues(sortByType, sortOrder, stringComparerCase, columnItems));

        /// <summary>
        /// Gets List of all document cell items
        /// </summary>
        /// <returns>List of IWebElements</returns>
        private List<IWebElement> GetAllDocumentCellItems()
            => DriverExtensions.GetElements(DriverExtensions.WaitForElement(TableLocator), RowLocator).ToList();

        /// <summary>
        /// Converts IWebElement to model
        /// </summary>
        /// <param name="element">IWebElement</param>
        /// <returns>The <see cref="DocumentInfoModel"/>.</returns>
        private DocumentInfoModel GetDocumentInfoModelByElement(IWebElement element)
            => new DocumentInfoModel
            {
                Guid = this.GetLinkElement(element).GetAttribute("guid"),
                Title = this.GetLinkElement(element).Text,
                LongTitle = this.GetLinkElement(element).GetAttribute("title"),
                Citation = DriverExtensions.SafeGetElement(element, CitationElementLocator)?.Text,
                Snippet = DriverExtensions.SafeGetElement(element, SnippetLocator)?.Text,
                Flag = this.GetFlag(element)
            };

        private KeyCiteFlag GetFlag(IWebElement element)
        {

            if (DriverExtensions.IsDisplayed(element, FlagLocator))
            {
                string flagClass = DriverExtensions.SafeGetElement(element, FlagLocator).GetAttribute("class");
                return flagClass.GetEnumValueByPropertyModel<KeyCiteFlag, WebElementInfo>(model => model.ClassName);
            }

            return KeyCiteFlag.NoFlag;
        }

        /// <summary>
        /// Gets Link IWebElement
        /// </summary>
        /// <param name="element">IWebElement</param>
        /// <returns>The <see cref="IWebElement"/>.</returns>
        private IWebElement GetLinkElement(IWebElement element) => DriverExtensions.WaitForElement(element, LinkLocator);

        /// <summary>
        /// The get expected values.
        /// </summary>
        /// <param name="sortByType">The sort by type.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <param name="stringComparerCase">The string Comparer Case.</param>
        /// <param name="actualValues">The actual values.</param>
        /// <returns>collection of expected values</returns>
        private IEnumerable<string> GetExpectedValues(
            Type sortByType,
            SortOrder sortOrder,
            StringComparer stringComparerCase,
            IEnumerable<string> actualValues)
        {
            IEnumerable<string> expectedValues = new List<string>();

            // Sort by type String, DateTime, Integer
            switch (Type.GetTypeCode(sortByType))
            {
                case TypeCode.String:
                    expectedValues = this.GetExpectedValuesString(actualValues, sortOrder, stringComparerCase);
                    break;

                case TypeCode.DateTime:
                    expectedValues = this.GetExpectedValuesDateTime(actualValues, sortOrder, stringComparerCase);
                    break;

                case TypeCode.Int32:
                    expectedValues = this.GetExpectedValuesInt(actualValues, sortOrder, stringComparerCase);
                    break;
            }

            return expectedValues;
        }

        private IEnumerable<string> GetExpectedValuesString(IEnumerable<string> actualValues, SortOrder sortOrder, StringComparer stringComparerCase)
        {
            string[] emptyDataArray = new[] { "\u2013", "\u2014", "\u2015", string.Empty };
            Func<string, string> keySelector =
                x => !emptyDataArray.Contains(x) ? x : sortOrder == SortOrder.Ascending ? "~~~~~~~" : "0000000";

            return sortOrder == SortOrder.Ascending
                ? actualValues.OrderBy(keySelector, stringComparerCase)
                : actualValues.OrderByDescending(keySelector, stringComparerCase);
        }

        private IEnumerable<string> GetExpectedValuesDateTime(IEnumerable<string> actualValues, SortOrder sortOrder, StringComparer stringComparerCase)
        {
            Func<string, DateTime> keySelector =
                x => x != "—"
                    ? DateTime.ParseExact(x.Replace(".", string.Empty).Replace("June", "Jun")
                        .Replace("July", "Jul"), "MMM dd, yyyy", CultureInfo.InvariantCulture)
                    : sortOrder == SortOrder.Ascending ? DateTime.MaxValue : DateTime.MinValue;

            return sortOrder == SortOrder.Ascending
                ? actualValues.OrderBy(keySelector)
                : actualValues.OrderByDescending(keySelector);
        }

        private IEnumerable<string> GetExpectedValuesInt(IEnumerable<string> actualValues, SortOrder sortOrder, StringComparer stringComparerCase)
            => sortOrder == SortOrder.Ascending
                ? actualValues.OrderBy(int.Parse)
                : actualValues.OrderByDescending(int.Parse);
    }
}