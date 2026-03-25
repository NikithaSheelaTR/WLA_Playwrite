namespace Framework.Common.Api.Endpoints.Website.DataModel.CustomPages
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Custom Page Super Admin Model
    /// </summary>
    [DataContract]
    public class CustomPageSuperAdminModel
    { 
        /// <summary>
        /// Sets Grantee Type (set by default)
        /// </summary>
        [DataMember(Name = "granteeType")]
        public string GranteeType { get; set; } = "userSapCobaltDomainId";

        /// <summary>
        /// Sets Grantee Id (set by default)
        /// </summary>
        [DataMember(Name = "granteeId")]
        public string GranteeId { get; set; }

        /// <summary>
        /// Describes CP ACL
        /// </summary>
        [DataMember(Name = "customPages")]
        public List<AdminCustomPageModel> AdminCustomPageModel { get; set; }
    }
}
