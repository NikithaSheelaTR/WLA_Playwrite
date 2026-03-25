namespace Framework.Common.UI.Raw.WestlawEdge.Pages.SecondarySources
{
    using Framework.Common.UI.Products.WestLawNext.Pages.SecondarySources.PublicationLandingPages;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;

    /// <summary>
    /// The publication toc landing page.
    /// </summary>
    public class EdgePublicationTocLandingPage : PublicationTocLandingPage
    {
        /// <summary>
        /// Gets the Indigo toolbar.
        /// </summary>
        public new EdgeToolbarComponent Toolbar { get; } = new EdgeToolbarComponent();
    }
}