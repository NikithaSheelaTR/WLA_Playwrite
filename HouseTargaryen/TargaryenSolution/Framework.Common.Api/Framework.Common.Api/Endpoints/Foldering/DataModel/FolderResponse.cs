namespace Framework.Common.Api.Endpoints.Foldering.DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The folder response.
    /// </summary>
    [DataContract]
    public class RootFolderResponse : ChildFolder
    {
        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        [DataMember(Name = "children")]
        public List<ChildFolder> Children { get; set; }
    }
}