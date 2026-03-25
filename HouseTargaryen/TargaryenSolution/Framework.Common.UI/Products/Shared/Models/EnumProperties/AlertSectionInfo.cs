namespace Framework.Common.UI.Products.Shared.Models.EnumProperties
{
    using Framework.Core.DataModel;

    /// <summary>
    /// Alerts Section Info
    /// </summary>
    public class AlertSectionInfo : BaseTextModel
    {
        /// <summary>
        /// Gets or sets Id for alert section
        /// </summary>
        public string SectionId { get; set; }

        /// <summary>
        /// Gets or sets Edit Link Id for alert section
        /// </summary>
        public string EditSectionLinkId { get; set; }
    }
}
