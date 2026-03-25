namespace Framework.Common.Api.Endpoints.CobaltServices.DataModel.CommonPayloadSections
{
    using Newtonsoft.Json;

    /// <summary>
    /// The container root list.
    /// </summary>
    public class ContainerRootList
    {
        /// <summary>
        /// Gets or sets the container id.
        /// </summary>
        [JsonProperty("containerId")]
        public string ContainerId { get; set; }

        /// <summary>
        /// Gets or sets the container type.
        /// </summary>
        [JsonProperty("containerType")]
        public string ContainerType { get; set; }

        /// <summary>
        /// Gets or sets the container name.
        /// </summary>
        [JsonProperty("containerName")]
        public string ContainerName { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        [JsonProperty("createdDate")]
        public string CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        [JsonProperty("modifiedDate")]
        public string ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the children folder count.
        /// </summary>
        [JsonProperty("childrenFolderCount")]
        public long ChildrenFolderCount { get; set; }

        /// <summary>
        /// Gets or sets the children item count.
        /// </summary>
        [JsonProperty("childrenItemCount")]
        public long ChildrenItemCount { get; set; }

        /// <summary>
        /// Gets or sets the root type.
        /// </summary>
        [JsonProperty("rootType")]
        public string RootType { get; set; }

        /// <summary>
        /// Gets or sets the read user count.
        /// </summary>
        [JsonProperty("readUserCount")]
        public long ReadUserCount { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        [JsonProperty("permissions")]
        public Permissions Permissions { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        [JsonProperty("createdBy")]
        public EditedBy CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        [JsonProperty("modifiedBy")]
        public EditedBy ModifiedBy { get; set; }
    }
}