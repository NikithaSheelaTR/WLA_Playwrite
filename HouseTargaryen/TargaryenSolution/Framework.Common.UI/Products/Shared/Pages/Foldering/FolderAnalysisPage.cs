namespace Framework.Common.UI.Products.Shared.Pages.Foldering
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Components.BreadCrumb;
    using Framework.Common.UI.Products.WestLawNext.Components.Folders;
    using Framework.Common.UI.Products.WestLawNext.Components.SearchResults;
    using Framework.Common.UI.Products.WestLawNext.Components.TabPanel;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Object representing smart folder analysis page.
    /// </summary>
    public class FolderAnalysisPage : BaseModuleRegressionPage
    {
        private static readonly By DocumentsLabelLocator = By.XPath("//div[@id='displayed-docs']//h1[@class='ng-binding']");

        /// <summary>
        /// The breadcrumb section
        /// </summary>
        public BreadCrumbComponent BreadCrumb { get; set; } = new BreadCrumbComponent();

        /// <summary>
        /// The folder analysis component for smart folders
        /// </summary>
        public FolderAnalysisComponent FolderAnalysis { get; set; } = new FolderAnalysisComponent();

        /// <summary>
        /// The folder analysis component for smart folders
        /// </summary>
        public WestlawNextHeaderComponent Header { get; set; } = new WestlawNextHeaderComponent();

        /// <summary>
        /// The breadcrumb section
        /// </summary>
        public LegalIssueComponent LegalIssue { get; set; } = new LegalIssueComponent();

        /// <summary>
        /// The tab panel for smart folders
        /// </summary>
        public SmartFoldersTabPanel TabPanel { get; set; } = new SmartFoldersTabPanel();

        /// <summary>
        /// Gets text of smart folders slider title
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public string GetSliderTitle() => DriverExtensions.WaitForElementDisplayed(DocumentsLabelLocator).Text;
    }
}