namespace Framework.Common.UI.Products.Shared.Pages.Document
{
    using Framework.Common.UI.Products.Shared.Components.Document;

    /// <summary>
    /// KeyCiteDocumentPage
    /// </summary>
    public class KeyCiteDocumentPage : CommonDocumentPage
    {
        /// <summary>
        /// NegativeTreatmentInfoComponent
        /// </summary>
        public ReadingModeKeyCiteComponent KeyCiteComponent { get; } = new ReadingModeKeyCiteComponent();
    }
}
