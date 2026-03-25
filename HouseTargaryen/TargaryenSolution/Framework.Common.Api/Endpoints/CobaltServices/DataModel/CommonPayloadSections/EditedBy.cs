namespace Framework.Common.Api.Endpoints.CobaltServices.DataModel.CommonPayloadSections
{
    using Newtonsoft.Json;

    /// <summary>
    /// The edited by.
    /// </summary>
    public class EditedBy
    {
        /// <summary>
        /// Gets or sets the user guid.
        /// </summary>
        [JsonProperty("userGuid")]
        public string UserGuid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is current user.
        /// </summary>
        [JsonProperty("isCurrentUser")]
        public bool IsCurrentUser { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
    }
}