namespace Framework.Common.UI.Raw.WestlawEdge.Pages.GradingTool
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Raw.WestlawEdge.Items.GradingTool;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The indigo grading tool assignments page.
    /// </summary>
    public class EdgeAssignmentsPage : BaseEdgeGradingPage
    {
        private static readonly By AssignmentItemContainerLocator =
            By.XPath("//table[@class='co_detailsTable']/tbody/tr");

        /// <summary>
        /// Clicks query name link by name.
        /// </summary>
        /// <param name="queryName"> The query name. </param>
        /// <returns> The <see cref="EdgeGradingToolSearchResultPage"/>. </returns>
        public EdgeGradingToolSearchResultPage ClickQueryLinkByName(string queryName)
            => this.GetListOfAssignmentItems().First(item => item.QueryName.Equals(queryName)).ClickQueryNameLink();

        private List<AssignmentItem> GetListOfAssignmentItems()
            => DriverExtensions.GetElements(AssignmentItemContainerLocator)
                               .Select(item => new AssignmentItem(item)).ToList();
    }
}
