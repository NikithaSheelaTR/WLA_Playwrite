namespace Framework.Common.Api.Endpoints.Website.DataModel.CustomPages
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Admin Custom page model
    /// </summary>
    [DataContract]
    public class AdminCustomPageModel
    {
        /// <summary>
        /// Gets or sets the custom page identifier.
        /// </summary>
        [DataMember(Name = "id")]
        public string CustomPageId { get; set; }

        /// <summary>
        /// Gets or sets the display name of custom page.
        /// </summary>
        [DataMember(Name = "label")]
        public string CustomPageLabel { get; set; }

        /// <summary>
        /// Sets master or non master page type
        /// </summary>
        [DataMember(Name = "pageType")]
        public string CustomPageType { get; set; }

        /// <summary>
        /// Identifies whether the page is free zone (bool)
        /// </summary>
        [DataMember(Name = "freeZone")]
        public bool CustomPageFreeZone { get; set; }

        /// <summary>
        /// Identifies whether the page is eLibrary (bool)
        /// </summary>
        [DataMember(Name = "eLibrary")]
        public bool CustomPageElibrary { get; set; }

        /// <summary>
        /// Describes CP ACL
        /// </summary>
        [DataMember(Name = "_acl")]
        public List<CustomPagesAclModel> CustomPagesAcl { get; set; }

    }
}
