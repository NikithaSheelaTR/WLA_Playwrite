namespace Framework.Common.UI.Products.WestLawNext.Components.SearchResults
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Enums.Foldering;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// graph bar component for smart folders pages
    /// </summary>
    public class FolderAnalysisComponent : BaseModuleRegressionComponent
    {
        private const string GraphBarDocumentsLctMask = "//*[@class='highcharts-series-group']//*[@fill='{0}']";
        private const string FolderedDocumentColor = "#48a0ef";
        private const string RecommendedDocumentColor = "#15548c";

        private static readonly By GraphLabelLocator = By.ClassName("highcharts-title");

        private static readonly By IssueBreakDownButtonLocator = By.Id("collapseExpandButton");
        private static readonly By ContainerLocator = By.Id("smartFoldersDetailsContainer");
        private static readonly By SmartFolderGraphLocator = By.Id("smartFoldersVisualization_Graph");

        private EnumPropertyMapper<SmartFoldersDocumentType, WebElementInfo> smartFoldersDocumentTypeMap;

        /// <summary>
        /// Gets the SmartFoldersDocumentType enumeration to BaseTextModel map.
        /// </summary>
        protected EnumPropertyMapper<SmartFoldersDocumentType, WebElementInfo> SmartFoldersDocumentTypeMap
            => this.smartFoldersDocumentTypeMap = this.smartFoldersDocumentTypeMap
                                                  ?? EnumPropertyModelCache.GetMap<SmartFoldersDocumentType, WebElementInfo>();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click on the issue breakdown
        /// </summary>
        public void ClickRaizeGraphButton()
        {
            DriverExtensions.WaitForElement(IssueBreakDownButtonLocator).Click();
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForAnimation();
        }

        /// <summary>
        /// Verify graph title is displayed
        /// </summary>
        /// <returns>True if graph title is displayed</returns>
        public string GetGraphLabelText() => DriverExtensions.GetText(GraphLabelLocator);

        /// <summary>
        /// Verify that folder analysis content is displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, 5);

        /// <summary>
        /// Verify graph is displayed
        /// </summary>
        /// <returns>True if graph  is displayed</returns>
        public bool IsGraphDisplayed() => DriverExtensions.IsDisplayed(SmartFolderGraphLocator, 5);

        /// <summary>
        /// Verify Issue BreakDown is displayed
        /// </summary>
        /// <returns>True if Issue BreakDown is displayed</returns>
        public bool IsIssueBreakDownDisplayed() => DriverExtensions.IsDisplayed(IssueBreakDownButtonLocator, 5);

        /// <summary>
        /// Verify recommended documents are displayed in the graph
        /// </summary>
        /// <param name="type">The <see cref="SmartFoldersDocumentType"/></param>
        /// <returns>True if recommended documents are displayed</returns>
        public bool IsGraphDocumentTypeDisplayed(SmartFoldersDocumentType type) => DriverExtensions.GetElements(By.XPath(this.SmartFoldersDocumentTypeMap[type].LocatorString)).Count != 0;
    }
}