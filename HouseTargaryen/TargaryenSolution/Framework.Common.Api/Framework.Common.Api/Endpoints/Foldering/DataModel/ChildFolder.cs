namespace Framework.Common.Api.Endpoints.Foldering.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The child folder.
    /// </summary>
    [DataContract]
    public class ChildFolder
    {
        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        [DataMember(Name = "categoryId")]
        public string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the collaborator role.
        /// </summary>
        [DataMember(Name = "collaboratorRole")]
        public string CollaboratorRole { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is leaf.
        /// </summary>
        [DataMember(Name = "isLeaf")]
        public bool IsLeaf { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is root.
        /// </summary>
        [DataMember(Name = "isRoot")]
        public bool IsRoot { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is shared.
        /// </summary>
        [DataMember(Name = "isShared")]
        public bool IsShared { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        [DataMember(Name = "label")]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the label style.
        /// </summary>
        [DataMember(Name = "labelStyle")]
        public string LabelStyle { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        [DataMember(Name = "permissions")]
        public Permissions Permissions { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}