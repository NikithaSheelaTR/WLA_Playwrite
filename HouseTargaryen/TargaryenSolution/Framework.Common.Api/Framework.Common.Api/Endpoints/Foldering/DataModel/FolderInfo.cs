namespace Framework.Common.Api.Endpoints.Foldering.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Folder info
    /// </summary>
    [DataContract]
    public class FolderInfo
    {
        /// <summary>
        /// Gets or sets the folder name.
        /// </summary>
        [DataMember(Name = "folderName")]
        public string FolderName { get; set; }

        /// <summary>
        /// Gets or sets the folder id.
        /// </summary>
        [DataMember(Name = "folderId")]
        public string FolderId { get; set; }
    }
}
