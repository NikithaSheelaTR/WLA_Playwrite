namespace Framework.Common.Api.Endpoints.Website.DataModel.CustomPages
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The custom pages acl model.
    /// </summary>
    [DataContract]
    public class CustomPagesAclModel
    {
        private List<object> permission;

        /// <summary>
        /// Sets Grantee Type (set by default)
        /// </summary>
        [DataMember(Name = "granteeType")]
        public string GranteeType { get; set; } = "userSapCobaltDomainId";

        /// <summary>
        /// Sets Grantee Id (set by default)
        /// </summary>
        [DataMember(Name = "granteeId")]
        public string GranteeId { get; set; } = "i0acc051b000001607a7d0c89f4dd391c";

        /// <summary>
        /// Sets owner rules (bool) (true by default)
        /// </summary>
        [DataMember(Name = "owner")]
        public bool Owner { get; set; } = true;

        /// <summary>
        /// Sets read rules (bool) (true by default)
        /// </summary>
        [DataMember(Name = "read")] 
        public bool Read { get; set; } = true;

        /// <summary>
        /// Sets write rules (bool) (true by default)
        /// </summary>
        [DataMember(Name = "write")]
        public bool Write { get; set; } = true;

        /// <summary>
        /// Sets delete rules (bool) (true by default)
        /// </summary>
        [DataMember(Name = "delete")]
        public bool Delete { get; set; } = true;

        /// <summary>
        /// Sets acl read rules (bool) (true by default)
        /// </summary>
        [DataMember(Name = "aclRead")]
        public bool AclRead { get; set; } = true;

        /// <summary>
        /// Sets acl write rules (bool) (true by default)
        /// </summary>
        [DataMember(Name = "aclWrite")]
        public bool AclWrite { get; set; } = true;

        /// <summary>
        /// Sets take ownership rules (bool) (false by default)
        /// </summary>
        [DataMember(Name = "takeOwnership")]
        public bool TakeOwnership { get; set; } = false;

        /// <summary>
        /// Sets custom permissions (list)
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

        /// <summary>
        /// Sets first name
        /// </summary>
        [DataMember(Name = "firstName", IsRequired = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// Sets Grantee Name
        /// </summary>
        [DataMember(Name = "granteeName", IsRequired = false)]
        public string GranteeName { get; set; }

        /// <summary>
        /// Sets is active contact (bool)
        /// </summary>
        [DataMember(Name = "isActiveContact", IsRequired = false)]
        public bool IsActiveContact { get; set; }

        /// <summary>
        /// Sets last name
        /// </summary>
        [DataMember(Name = "lastName", IsRequired = false)]
        public string LastName { get; set; }
    }
}
