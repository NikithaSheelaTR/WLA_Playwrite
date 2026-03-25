namespace Framework.Common.UI.Products.WestLawNext.Pages
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Enums.Report;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Judical Reversal Report page
    /// </summary>
    public sealed class JudicalReversalReportPage : BaseModuleRegressionPage
    {
        private const string ColumnNameLctMask = "//th[@data-report-type='JRJudgesAppellateAppealsTo{0}' and @data-sort-key='row_name']";

        private const string SortByLctMask = "co_SortByJRJudgesAppellateAppealsTo{0}";
        
        private static readonly By AppellateJudgeReportLinkLocator = By.Id("co_jrAppellateSubHeader");

        /// <summary>
        /// Initializes a new instance of the <see cref="JudicalReversalReportPage" /> class.
        /// </summary>
        public JudicalReversalReportPage()
        {
            DriverExtensions.WaitForElement(AppellateJudgeReportLinkLocator);
        }

        /// <summary>
        /// CLick on 'Supreme/Appellate Judge Report' link
        /// </summary>
        /// <typeparam name="T">Page returned from clicking the link</typeparam>
        /// <returns>The page.</returns>
        public T ClickAppellateJudgeReportLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(AppellateJudgeReportLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get 'sort by' options from dropdown in the 'Trial Judges' table
        /// </summary>
        /// <param name="option">Selected option from dropdown.</param>
        /// <returns>'Sort by' options.</returns>
        public IList<string> GetTrialJudgesTableSortByOptions(AppealsOfDecisionsDropdownOptions option) =>
            DriverExtensions.GetDropdownOptionTexts(By.Id(string.Format(SortByLctMask, option)));

        /// <summary>
        /// Get first column name in the 'Trial Judges table'
        /// </summary>
        /// <param name="option">Selected option from dropdown.</param>
        /// <returns>First column name.</returns>
        public string GetTrialJudgesTableFirstColumnName(AppealsOfDecisionsDropdownOptions option) =>
            DriverExtensions.GetImmediateText(By.XPath(string.Format(ColumnNameLctMask, option)));
    }
}
