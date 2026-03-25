namespace Framework.Common.UI.Products.WestLawNext.Pages.CompanyInvestigator
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Components.Toolbar;
    using Framework.Common.UI.Products.Shared.Pages;

    /// <summary>
    /// The common report document page.
    /// </summary>
    public abstract class CommonReportDocumentPage : BaseModuleRegressionPage
    {
        /// <summary>
        /// Overrides the header section with foldering custom header Section 
        /// </summary>
        public WestlawNextHeaderComponent Header { get; protected set; } = new WestlawNextHeaderComponent();

        /// <summary>
        /// Toolbar Element
        /// </summary>
        public Toolbar Toolbar { get; protected set; } = new Toolbar();

        /// <summary>
        /// The get document title.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public abstract string GetDocumentTitle();
    }
}