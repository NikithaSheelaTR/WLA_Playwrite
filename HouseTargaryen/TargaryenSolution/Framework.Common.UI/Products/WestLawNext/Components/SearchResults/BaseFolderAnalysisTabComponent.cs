namespace Framework.Common.UI.Products.WestLawNext.Components.SearchResults
{
    using Framework.Common.UI.Products.WestlawEdge.Components.ResultList;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The base folder analysis tab component.
    /// </summary>
    public abstract class BaseFolderAnalysisTabComponent : BaseTabComponent
    {
        private static readonly By ContentContainerLocator = By.Id("smartFoldersDetailsContent");
        private static readonly By NewRecommendationIconLocator =
           By.XPath(
               "//*[@id='coid_folderAnalysisRecommendations_newRecommendation' and not(contains(@class,'ng-hide'))]");
        
        /// <summary>
        /// The result list section of the page
        /// </summary>
        public FolderAnalysisResultListComponent ResultList { get; set; } = new FolderAnalysisResultListComponent(DriverExtensions.WaitForElementDisplayed(ContentContainerLocator));

        /// <summary>
        /// The toolbar for smart folders
        /// </summary>
        public CustomToolbarComponent Toolbar { get; set; } = new CustomToolbarComponent();

        /// <summary>
        /// Returns count of new Recommendations for the tab
        /// </summary>
        /// <returns></returns>
        public int GetNewRecommendationsCount() => DriverExtensions.GetElements(NewRecommendationIconLocator).Count;
    }
}
