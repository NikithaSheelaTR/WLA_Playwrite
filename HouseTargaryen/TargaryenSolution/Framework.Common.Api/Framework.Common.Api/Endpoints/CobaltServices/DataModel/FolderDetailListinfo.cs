namespace Framework.Common.Api.Endpoints.CobaltServices.DataModel
{
    using System.Collections.Generic;

    using Framework.Common.Api.Endpoints.CobaltServices.DataModel.CommonPayloadSections;

    using Newtonsoft.Json;

    /// <summary>
    /// The folder detail list.
    /// </summary>
    public class FolderDetailListInfo
    {
        /// <summary>
        /// Gets or sets the folder details list.
        /// </summary>
        [JsonProperty("folderDetailsList")]
        public List<FolderDetailsList> FolderDetailsList { get; set; }

        /// <summary>
        /// Gets or sets the pagination.
        /// </summary>
        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
    }
}
