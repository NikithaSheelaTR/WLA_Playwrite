namespace Framework.Common.Api.Endpoints.CobaltServices.DataModel.CommonPayloadSections
{
    using Newtonsoft.Json;

    /// <summary>
    /// The folder details list.
    /// </summary>
    public class FolderDetailsList
    {
        /// <summary>
        /// Gets or sets the folder id.
        /// </summary>
        [JsonProperty("folderId")]
        public string FolderId { get; set; }

        /// <summary>
        /// Gets or sets the folder name.
        /// </summary>
        [JsonProperty("folderName")]
        public string FolderName { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        [JsonProperty("createdDate")]
        public string CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the created by name.
        /// </summary>
        [JsonProperty("createdByName")]
        public EditedBy CreatedByName { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        [JsonProperty("modifiedBy")]
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        [JsonProperty("modifiedDate")]
        public string ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified by name.
        /// </summary>
        [JsonProperty("modifiedByName")]
        public EditedBy ModifiedByName { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        [JsonProperty("parent")]
        public Parent Parent { get; set; }

        /// <summary>
        /// Gets or sets the collaboration type.
        /// </summary>
        [JsonProperty("collaborationType")]
        public string CollaborationType { get; set; }

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
        /// Gets or sets the read user count.
        /// </summary>
        [JsonProperty("readUserCount")]
        public long ReadUserCount { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        [JsonProperty("permissions")]
        public Permissions Permissions { get; set; }
    }
}