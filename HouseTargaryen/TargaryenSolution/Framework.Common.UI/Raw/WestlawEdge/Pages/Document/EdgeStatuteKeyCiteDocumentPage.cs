namespace Framework.Common.UI.Raw.WestlawEdge.Pages.Document
{
    using Framework.Common.UI.Products.Shared.Components.Document;

    /// <summary>
    /// IndigoStatuteDocumentPage
    /// </summary>
    public class EdgeStatuteKeyCiteDocumentPage : EdgeCommonDocumentPage
    {
        /// <summary>
        /// StatuteNegativeTreatmentInfoComponent
        /// </summary>
        public ReadingModeKeyCiteComponent KeyCiteComponent { get; } = new ReadingModeKeyCiteComponent("Statutes");
    }
}
