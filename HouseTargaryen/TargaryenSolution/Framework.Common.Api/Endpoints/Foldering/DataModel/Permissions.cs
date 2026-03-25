namespace Framework.Common.Api.Endpoints.Foldering.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The permissions.
    /// </summary>
    [DataContract]
    public class Permissions
    {
        /// <summary>
        /// Gets or sets a value indicating whether add containers allowed.
        /// </summary>
        [DataMember(Name = "addContainersAllowed")]
        public bool AddContainersAllowed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether add items allowed.
        /// </summary>
        [DataMember(Name = "addItemsAllowed")]
        public bool AddItemsAllowed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether copy allowed.
        /// </summary>
        [DataMember(Name = "copyAllowed")]
        public bool CopyAllowed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether delete allowed.
        /// </summary>
        [DataMember(Name = "deleteAllowed")]
        public bool DeleteAllowed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether edit description allowed.
        /// </summary>
        [DataMember(Name = "editDescriptionAllowed")]
        public bool EditDescriptionAllowed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether move allowed.
        /// </summary>
        [DataMember(Name = "moveAllowed")]
        public bool MoveAllowed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether rename allowed.
        /// </summary>
        [DataMember(Name = "renameAllowed")]
        public bool RenameAllowed { get; set; }
    }
}