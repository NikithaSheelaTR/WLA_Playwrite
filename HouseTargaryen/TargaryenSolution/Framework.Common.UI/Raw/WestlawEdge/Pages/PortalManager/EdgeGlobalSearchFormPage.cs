
namespace Framework.Common.UI.Raw.WestlawEdge.Pages.PortalManager
{
    using Framework.Common.UI.Products.WestLawNext.Pages.PortalManagerSearch;
    using Framework.Common.UI.Products.WestlawEdge.Components.PortalManager;

    /// <summary>
    /// POM for the form displayed when user clicks on Tools > Portal Manager > Global Search tab > Create button
    /// </summary>
    public class EdgeGlobalSearchFormPage : GlobalSearchFormPage
    {
        /// <summary>
        /// The content widget.
        /// </summary>
        public new EdgeContentComponent WlnContentWidget { get; } = new EdgeContentComponent();

    }
}
