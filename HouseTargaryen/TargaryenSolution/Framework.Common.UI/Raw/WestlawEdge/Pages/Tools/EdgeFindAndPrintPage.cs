namespace Framework.Common.UI.Raw.WestlawEdge.Pages.Tools
{
    using Framework.Common.UI.Products.Shared.Pages.Tools;
    using Framework.Common.UI.Products.WestlawEdge.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    /// <summary>
    /// EdgeFindAndPrintPage
    /// </summary>
    public class EdgeFindAndPrintPage : FindAndPrintPage
    {
        /// <summary>
        /// Gets the Indigo header.
        /// </summary>
        public new EdgeHeaderComponent Header { get; } = new EdgeHeaderComponent();
    }
}