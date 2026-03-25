namespace Framework.Common.UI.Products.WestLawNext.Pages.Document
{
   using Framework.Common.UI.Products.Shared.Components;

    /// <summary>
    /// Docket Page with Chunk component
    /// </summary>
    public class ChunkingDocketDocumentPage : DocketDocumentPage
    {
        /// <summary> Chunk Navigation component  </summary>
        public ChunkNavigationComponent ChunkNavigation { get; protected set; } = new ChunkNavigationComponent();
    }
}
