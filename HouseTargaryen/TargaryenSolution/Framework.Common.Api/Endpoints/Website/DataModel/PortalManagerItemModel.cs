namespace Framework.Common.Api.Endpoints.Website.DataModel
{
    /// <summary>
    /// Portal Manager Item model
    /// </summary>
    public class PortalManagerItemModel
    {
        /// <summary>
        /// Gets or sets the portal manager item identifier.
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// Gets or sets the display name of portal manager item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the portal manager item type.
        /// </summary>
        public string Type { get; set; }
    }
}