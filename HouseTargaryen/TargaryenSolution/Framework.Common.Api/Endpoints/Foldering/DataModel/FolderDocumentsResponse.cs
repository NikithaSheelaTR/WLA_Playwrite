namespace Framework.Common.Api.Endpoints.Foldering.DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Folder documents response
    /// </summary>
    [DataContract]
    public class FolderDocumentsResponse
    {
        /// <summary>
        /// Gets or sets the folder info.
        /// </summary>
        [DataMember(Name = "folderInfo")]
        public FolderInfo FolderInfo { get; set; }
        
        /// <summary>
        /// Gets or sets the collaborators.
        /// </summary>
        [DataMember(Name = "docGuids")]
        public List<string> DocumentGuids { get; set; }
    }
}
