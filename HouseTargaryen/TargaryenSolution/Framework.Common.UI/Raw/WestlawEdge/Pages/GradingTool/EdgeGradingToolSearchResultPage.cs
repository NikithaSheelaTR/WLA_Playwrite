namespace Framework.Common.UI.Raw.WestlawEdge.Pages.GradingTool
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The indigo grading tool search result page.
    /// </summary>
    public class EdgeGradingToolSearchResultPage : EdgeCommonSearchResultPage
    {
        private static readonly By OverallGradeAndCommentsTextLocator = By.ClassName("gradingToolResultListGrading");

        /// <summary>
        /// Verifies that the overall grade and comments text is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the overall grade and comments text is displayed. </returns>
        public bool IsOverallGradeAndCommentsTextDisplayed() => DriverExtensions.IsDisplayed(
            OverallGradeAndCommentsTextLocator);
    }
}
