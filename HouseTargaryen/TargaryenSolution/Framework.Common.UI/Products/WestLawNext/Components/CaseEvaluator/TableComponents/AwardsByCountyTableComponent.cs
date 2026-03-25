namespace Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator.TableComponents
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.Reports;
    using Framework.Common.UI.Products.WestLawNext.Enums.Report;
    using Framework.Common.UI.Products.WestLawNext.Utils.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Represents the Awards By County table on the case evaluator report page
    /// </summary>
    public class AwardsByCountyTableComponent : AwardsAndExpertsBaseReportTableComponent
    {
        private static readonly int HighestAwardColNum = (int)AwardsByCountyHeaders.HighestAward;

        private static readonly int NumAwardsColNum = (int)AwardsByCountyHeaders.NumberOfAwards;

        /// <summary>
        /// Initializes a new instance of the <see cref="AwardsByCountyTableComponent"/> class. 
        /// constructor - dual function
        /// </summary>
        /// <param name="expandTableCompletely">Fully expand table upon instantiation</param>
        public AwardsByCountyTableComponent(bool expandTableCompletely = false)
            : base(ReportPageTables.AwardsByCounty)
        {
            if (expandTableCompletely)
            {
                this.ExpandTableCompletely();
            }
        }

        /// <summary>
        /// Gets the SortMenuAwardsByCountyDropdown
        /// </summary>
        public SortMenuAwardsByCountyDropdown Dropdown { get; } = new SortMenuAwardsByCountyDropdown();

        /// <summary>
        /// Clicks on highest award link by specific row number
        /// </summary>
        /// <param name="rowNumber">The row Number.</param>
        /// <returns>New CaseEvaluatorDocumentPage </returns>
        public T ClickOnHighestAwardByRowNumber<T>(int rowNumber) where T : ICreatablePageObject
            => this.ClickOnAwardByRowNumber<T>(rowNumber, HighestAwardColNum);

        /// <summary>
        /// Clicks on a number of awards link by specific row number
        /// </summary>
        /// <param name="rowNumber">Row number</param>
        /// <returns>New ReportDocumentListPage</returns>
        public T ClickOnNumberOfAwardsByRowNumber<T>(int rowNumber) where T : ICreatablePageObject
            => this.ClickOnAwardByRowNumber<T>(rowNumber, NumAwardsColNum);

        /// <summary>
        /// Verifys View More Functionality 
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsViewMoreFunctionCorrect() => this.IsViewMoreFunctionCorrect((int)AwardsByCountyHeaders.CountyDistrict);

        /// <summary>
        /// Click On Award By Row Number
        /// </summary>
        /// <param name="rowNumber"></param>
        /// <param name="columnNumber"></param>
        /// <returns></returns>
        private T ClickOnAwardByRowNumber<T>(int rowNumber, int columnNumber) where T : ICreatablePageObject
        {
            IWebElement element = this.GetWebElement(columnNumber, rowNumber);
            ActionExtensions.DoUntilConditionWillBecomeTrue(
                () => this.GetWebElement(columnNumber, rowNumber).JavascriptClick(),
                () => !element.IsDisplayed(),
                3);
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}