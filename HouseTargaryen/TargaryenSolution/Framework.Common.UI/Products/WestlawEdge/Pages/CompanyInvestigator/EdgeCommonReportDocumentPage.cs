namespace Framework.Common.UI.Products.WestlawEdge.Pages.CompanyInvestigator
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestlawEdge.Components;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The common report document page.
    /// </summary>
    public class EdgeCommonReportDocumentPage : BaseModuleRegressionPage
    {
        private static readonly By TitleLocator = By.Id("co_pm_headerName");

        /// <summary>
        /// Overrides the header section with foldering custom header Section 
        /// </summary>
        public EdgeHeaderComponent Header { get; } = new EdgeHeaderComponent();

        /// <summary>
        /// Toolbar Element
        /// </summary>
        public EdgeToolbarComponent Toolbar { get; } = new EdgeToolbarComponent();

        /// <summary>
        /// The get document title.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public virtual string GetDocumentTitle() => DriverExtensions.GetText(TitleLocator);
    }
}