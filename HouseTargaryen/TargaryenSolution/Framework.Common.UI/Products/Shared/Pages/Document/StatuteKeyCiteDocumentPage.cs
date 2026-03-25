namespace Framework.Common.UI.Products.Shared.Pages.Document
{
    using Framework.Common.UI.Products.Shared.Components.Document;

    /// <summary>
    /// StatuteDocumentPage
    /// </summary>
    public class StatuteKeyCiteDocumentPage : CommonDocumentPage
    {
        /// <summary>
        /// StatuteNegativeTreatmentInfoComponent
        /// </summary>
        public ReadingModeKeyCiteComponent KeyCiteComponent { get; } = new ReadingModeKeyCiteComponent("Statutes");
    }
}
