namespace Framework.Common.Api.Endpoints.CobaltServices.DataModel
{
    using System.Collections.Generic;

    using Framework.Common.Api.Endpoints.CobaltServices.DataModel.CommonPayloadSections;

    using Newtonsoft.Json;

    /// <summary>
    /// The user folders info.
    /// </summary>
    public class UserFoldersInfo
    {
        /// <summary>
        /// Gets or sets the pagination.
        /// </summary>
        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }

        /// <summary>
        /// Gets or sets the container root list.
        /// </summary>
        [JsonProperty("containerRootList")]
        public List<ContainerRootList> ContainerRootList { get; set; }
    }
}
