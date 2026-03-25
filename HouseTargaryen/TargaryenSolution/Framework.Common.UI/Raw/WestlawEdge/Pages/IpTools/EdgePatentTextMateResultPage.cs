namespace Framework.Common.UI.Raw.WestlawEdge.Pages.IpTools
{
    using Framework.Common.UI.Products.Shared.Components.IpTools;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo;

    /// <summary>
    /// Patent Text Mate Result Page
    /// </summary>
    public class EdgePatentTextMateResultPage : EdgeTabPage
    {
        /// <summary>
        /// Grid Component
        /// </summary>
        public PatentTextMateGridComponent GridComponent { get; } = new PatentTextMateGridComponent();
    }
}
