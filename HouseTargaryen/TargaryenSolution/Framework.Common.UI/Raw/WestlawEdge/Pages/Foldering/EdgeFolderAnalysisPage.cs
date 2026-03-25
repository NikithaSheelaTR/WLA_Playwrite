namespace Framework.Common.UI.Raw.WestlawEdge.Pages.Foldering
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.WestlawEdge.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Enums.Document;
    using Framework.Common.UI.Products.Shared.Pages.Foldering;
    using Framework.Common.UI.Products.WestLawNext.Components.SearchResults;
    using Framework.Common.UI.Raw.WestlawEdge.Models;

    /// <summary>
    /// Object representing indigo smart folder analysis page.
    /// </summary>
    public class EdgeFolderAnalysisPage : FolderAnalysisPage
    {
        /// <summary>
        /// The get new recommendations count.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetNewRecommendationsCount() =>
            this.TabPanel.GetAvailableTabs().Sum(
                x => this.TabPanel.SetActiveTab<BaseFolderAnalysisTabComponent>(x).GetNewRecommendationsCount());


        /// <summary>
        /// The tab panel for smart folders
        /// </summary>
        public new EdgeSmartFoldersTabPanel TabPanel { get; set; } = new EdgeSmartFoldersTabPanel();

        /// <summary>
        /// The is new items on top.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsNewItemsOnTop()
        {
            bool result = true;

            foreach (DocumentTypeTabs tab in this.TabPanel.GetAvailableTabs())
            {
                List<BaseFolderAnalysisResultListItemModel> models =
                    this.TabPanel.SetActiveTab<BaseFolderAnalysisTabComponent>(tab)
                    .ResultList.GetResultListItemModels<BaseFolderAnalysisResultListItemModel>().ToList();

                int lastNewItem = models.Last(i => i.IsItemNew).Index;

                if (lastNewItem == models.Count)
                {
                    continue;
                }

                int firstOldItem = models.First(i => i.IsItemNew == false).Index;

                if (lastNewItem > firstOldItem)
                {
                    result = false;
                }
            }

            return result;
        }
    }
}