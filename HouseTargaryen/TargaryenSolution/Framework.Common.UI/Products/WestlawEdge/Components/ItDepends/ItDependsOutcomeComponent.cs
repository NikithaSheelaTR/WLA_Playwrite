namespace Framework.Common.UI.Products.WestlawEdge.Components.ItDepends
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    using Framework.Common.UI.Products.WestlawEdge.Enums.ItDepends;
    using Framework.Common.UI.Products.WestlawEdge.Items.ItDepends;

    /// <summary>
    /// Outcome section
    /// </summary>
    public class ItDependsOutcomeComponent : BaseModuleRegressionComponent
    {
        private static readonly By ResultListLocator = By.XPath("//*[@id='result_list']");
        private static readonly By ResultLocator = By.XPath("//tbody[@id='result_list']/tr");
        private static readonly By SaveToFolderWidgetLocator = By.XPath("//*[@id='co_saveToWidget']");
        private static readonly By FolderInfoBoxMessageLocator = By.XPath("//div[@class= 'co_foldering_popupMessageContainer']");
        private static readonly By SelectAllDocumentCheckboxLocator = By.XPath("//*[@id='co_checkAll']");
        private static readonly By ContainerLocator = By.CssSelector("#co_resultsContent .ItDepends-table");
        private static readonly string SelectDocumentUsingGuidLocator = "//*[@id='result_list']//input[contains(@document-id,'{0}')]";
        private EnumPropertyMapper<ItDependsOutcomeSorting, WebElementInfo> sortingMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Annotation Options Map
        /// </summary>
        protected EnumPropertyMapper<ItDependsOutcomeSorting, WebElementInfo> SortingMap
            =>
                this.sortingMap =
                    this.sortingMap ?? EnumPropertyModelCache.GetMap<ItDependsOutcomeSorting, WebElementInfo>();

        /// <summary>
        /// Delivery Widget
        /// </summary>
        public DeliveryDropdown DeliveryDropdown { get; } = new DeliveryDropdown();

        /// <summary>
        /// Get List of Models
        /// </summary>
        public List<ItDependsResultListItem> GetResultListItems() => DriverExtensions
                                                                           .GetElements(ResultLocator)
                                                                           .Select(item => new ItDependsResultListItem(item)).ToList();
        /// <summary>
        /// Verify that result list is displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsResultListDisplayed() => DriverExtensions.IsDisplayed(ResultListLocator);

        /// <summary>
        /// Folder document
        /// </summary>
        public void ClickSaverToFolderWidget()
        {
            DriverExtensions.ScrollTo(SaveToFolderWidgetLocator);
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.GetElement(SaveToFolderWidgetLocator).Click();
        }

        /// <summary>
        /// Message for foldering
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetFolderMessageText() => DriverExtensions.GetText(FolderInfoBoxMessageLocator);
        

        /// <summary>
        /// Check if error message is displayed if user try to folder without selecting any document
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetErrorMessageText() => DriverExtensions.GetText(FolderInfoBoxMessageLocator);

        /// <summary>
        /// select all document
        /// </summary>
        public void ClickSelectAllDocumentsCheckbox() => DriverExtensions.GetElement(SelectAllDocumentCheckboxLocator).Click();

        /// <summary>
        /// Select document to folder or deliver
        /// </summary>
        /// <param name="documentGuid">Document Guid</param>
        public void SelectDocumentByGuid(string documentGuid) => DriverExtensions.GetElement(By.XPath(string.Format(SelectDocumentUsingGuidLocator , documentGuid))).Click();
        

        /// <summary>
        /// Sorting results
        /// </summary>
        /// <param name="sort">Sorting</param>
        public void SortResult(ItDependsOutcomeSorting sort) =>
            DriverExtensions.GetElement(By.XPath(this.SortingMap[sort].LocatorString)).Click();

        /// <summary>
        /// Verify sorting
        /// </summary>
        /// <typeparam name="T">
        /// T
        /// </typeparam>
        /// <param name="sortedList">
        /// Sorted Lit
        /// </param>
        /// <param name="list">
        /// List
        /// </param>
        /// <param name="asc">
        /// Asc
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool VerifySorting<T>(List<T> sortedList, List<T> list, bool asc = true)
        {
            if (typeof(T) == typeof(string))
            {
                return this.VerifyStringSorting(sortedList as List<string>, list as List<string>, asc);
            }

            return this.VerifyDateSorting(sortedList as List<DateTime>, list as List<DateTime>, asc);
        }

        /// <summary>
        /// Verify Date sorting
        /// </summary>
        /// <param name="sortedList">
        /// Sorted List
        /// </param>
        /// <param name="list">
        /// List
        /// </param>
        /// <param name="asc">
        /// Asc
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool VerifyDateSorting(List<DateTime> sortedList, List<DateTime> list, bool asc = true)
        {
            var orderedList = new List<DateTime>();
            if (asc)
            {
                orderedList = list.OrderBy(x => x).ToList();
            }
            else
            {
                orderedList = list.OrderByDescending(x => x).ToList();
            }

            return sortedList.SequenceEqual(orderedList);
        }

        /// <summary>
        /// Verify strings sorting
        /// </summary>
        /// <param name="sortedList">
        /// Sorted List
        /// </param>
        /// <param name="list">
        /// List
        /// </param>
        /// <param name="asc">
        /// Asc
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool VerifyStringSorting(List<string> sortedList, List<string> list, bool asc = true)
        {
            var sortedTempList = new List<string>();

            IList<string> orderedList = this.GetSortedList(list, asc);

            foreach (string item in sortedList)
            {
                sortedTempList.Add(this.SpecReplace(item));
            }

            return sortedTempList.SequenceEqual(orderedList);
        }

        /// <summary>
        /// method to sort input list
        /// </summary>
        /// <param name="list">
        /// List
        /// </param>
        /// <param name="asc">
        /// Asc
        /// </param>
        private IList<string> GetSortedList(List<string> list, bool asc = true)
        {
            var tempList = new List<string>();
            var orderedList = new List<string>();
            //Trimming special characters
            foreach (string item in list)
            {
                tempList.Add(this.SpecReplace(item));
            }
            if (asc)
            {
                orderedList = tempList.OrderBy(x => x).ToList();
            }
            else
            {
                orderedList = tempList.OrderByDescending(x => x).ToList();
            }

            return orderedList;
        }

        /// <summary>
        /// Special replace method used in VerifyStringSorting
        /// </summary>
        /// <param name="str">
        /// String
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string SpecReplace(string str) => str.Trim(
            new char[]
                {
                    '[',
                    '!',
                    '@',
                    '#',
                    '$',
                    '%',
                    '^',
                    '&',
                    '*',
                    '(',
                    ')',
                    '-',
                    '=',
                    '_',
                    '+',
                    '|',
                    ';',
                    '\'',
                    '\"',
                    ':',
                    ',',
                    '.',
                    '<',
                    '>',
                    '?',
                    ']',
                    ' '
                });
    }
}