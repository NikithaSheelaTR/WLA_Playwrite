namespace Framework.Common.UI.Raw.WestlawEdge.Pages.Document
{
    using Framework.Common.UI.Products.Shared.Components.Document;

    /// <summary>
    /// EdgeKeyCiteDocumentPage
    /// </summary>
    public class EdgeKeyCiteDocumentPage : EdgeCommonDocumentPage
    {
        /// <summary>
        /// NegativeTreatmentInfoComponent
        /// </summary>
        public ReadingModeKeyCiteComponent KeyCiteComponent { get; } = new ReadingModeKeyCiteComponent();
    }
}
