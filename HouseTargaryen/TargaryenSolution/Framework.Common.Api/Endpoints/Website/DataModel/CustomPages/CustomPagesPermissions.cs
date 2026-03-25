namespace Framework.Common.Api.Endpoints.Website.DataModel.CustomPages
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The custom pages permissions.
    /// </summary>
    [DataContract]
    public class CustomPagesPermissions
    {
        private List<object> permission;

        /// <summary>
        /// Sets owner permissions (true by default)
        /// </summary>
        [DataMember(Name = "owner")]
        public bool Owner { get; set; } = true;

        /// <summary>
        /// Sets read permissions (true by default)
        /// </summary>
        [DataMember(Name = "read")]
        public bool Read { get; set; } = true;

        /// <summary>
        /// Sets write permissions (true by default)
        /// </summary>
        [DataMember(Name = "write")]
        public bool Write { get; set; } = true;

        /// <summary>
        /// Sets delete permissions (true by default)
        /// </summary>
        [DataMember(Name = "delete")]
        public bool Delete { get; set; } = true;

        /// <summary>
        /// Sets acl read permissions (true by default)
        /// </summary>
        [DataMember(Name = "aclRead")]
        public bool AclRead { get; set; } = true;

        /// <summary>
        /// Sets acl write permissions (true by default)
        /// </summary>
        [DataMember(Name = "aclWrite")]
        public bool AclWrite { get; set; } = true;

        /// <summary>
        /// Sets take ownership (false by default)
        /// </summary>
        [DataMember(Name = "takeOwnership")]
        public bool TakeOwnership { get; set; } = false;

        /// <summary>
        /// Sets custom permissions (empty by default)
        /// </summary>
        [DataMember(Name = "customPermissions")]
        public List<object> CustomPermissions
        {
            get
            {
                return this.permission ?? (this.permission = new List<object>());
            }

            set
            {
                this.permission = value;
            }
        }
    }
}
