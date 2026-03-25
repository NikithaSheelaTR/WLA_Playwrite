namespace Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator.TableComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Enums.Report;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The awards and experts base report table component.
    /// </summary>
    public abstract class AwardsAndExpertsBaseReportTableComponent : BaseReportTableComponent
    {
        private const string ColumnByNumberLctMask = "//div[@id='{0}']//table/tbody/tr/td[{1}]";

        private const string ShowMoreLinkLocatorLctMask = "//*[@id={0}]//a[@ng-show='showViewMore']";
        
        private readonly By showMoreLinkContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="AwardsAndExpertsBaseReportTableComponent"/> class.
        /// </summary>
        /// <param name="table">The table.</param>
        protected AwardsAndExpertsBaseReportTableComponent(ReportPageTables table) : base(table)
        {
            this.showMoreLinkContainer =
                SafeXpath.BySafeXpath(ShowMoreLinkLocatorLctMask, EnumPropertyModelCache.GetEnumInfo<ReportPageTables, WebElementInfo>(table).Id);
        }

        /// <summary>
        /// Clicks on the show more results link
        /// </summary>
        public void ClickShowMoreResults()
        {
            DriverExtensions.ScrollTo(this.showMoreLinkContainer);
            DriverExtensions.Click(this.showMoreLinkContainer);
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Clicks on the show more results link until it can't be expanded anymore (show all)
        /// </summary>
        public void ExpandTableCompletely()
        {
            while (DriverExtensions.IsDisplayed(this.showMoreLinkContainer, 2))
            {
                this.ClickShowMoreResults();
            }
        }

        /// <summary>
        /// Gets specified column as a list of strings
        /// </summary>
        /// <param name="columnNo">Number of the column</param>
        /// <param name="fullUiExpansion">Switch to expand UI table fully</param>
        /// <returns>List of strings of column elements</returns>
        public List<string> GetColumnAsStringList(int columnNo, bool fullUiExpansion = true)
        {
            if (fullUiExpansion)
            {
                this.ExpandTableCompletely();
            }

            IReadOnlyCollection<IWebElement> listOfElements
                = DriverExtensions.GetElements(By.XPath(string.Format(ColumnByNumberLctMask, this.TableId, columnNo)));

            return listOfElements.Select(elements => elements.Text).ToList();
        }

        /// <summary>
        /// Returns pair of View (item1) more of (item2)
        /// </summary>
        /// <returns>The <see cref="Tuple"/>.</returns>
        public KeyValuePair<int, int> GetViewMoreRange()
        {
            int viewMore = 0;
            int total = 0;

            string line = DriverExtensions.GetElement(this.showMoreLinkContainer).Text;
            string[] words = line.Split(' ');

            if (words.Length > 1)
            {
                viewMore = words[1].ConvertCountToInt();
            }

            if (words.Length > 4)
            {
                total = words[4].ConvertCountToInt();
            }

            return new KeyValuePair<int, int>(viewMore, total);
        }

        /// <summary>
        /// Verify View More Functionality 
        /// </summary>
        /// <param name="columnNumber">The column Number.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        protected bool IsViewMoreFunctionCorrect(int columnNumber)
        {
            List<string> actualColumns = this.GetColumnAsStringList(columnNumber, false);

            // initial count of awards shown, keep track of list count
            int initialColumnsCount = actualColumns.Count;
            int expectedColumnsCount = actualColumns.Count;

            // GetViewMoreRange returns "View (key) more of (Value)"
            KeyValuePair<int, int> initialRange = this.GetViewMoreRange();
            int totalViewMore = initialRange.Value;

            // True if view more functionality is working correctly, false if not; 
            // while loop continue until list-initial count == total view more
            bool verifyViewMore = false;

            while (true)
            {
                if (actualColumns.Count - initialColumnsCount == totalViewMore)
                {
                    verifyViewMore = true;
                    break;
                }

                if (actualColumns.Count == expectedColumnsCount)
                {
                    // Gets current range, add item1 to expected list count, click show more results, update actual list
                    KeyValuePair<int, int> currentRange = this.GetViewMoreRange();
                    expectedColumnsCount += currentRange.Key;
                    this.ClickShowMoreResults();
                    actualColumns = this.GetColumnAsStringList(columnNumber, false);
                }
                else
                {
                    // view more function not working correctly
                    break;
                }
            }

            return verifyViewMore;
        }
    }
}