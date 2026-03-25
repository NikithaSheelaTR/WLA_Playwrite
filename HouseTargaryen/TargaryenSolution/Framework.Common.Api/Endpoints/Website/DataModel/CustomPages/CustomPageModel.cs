namespace Framework.Common.Api.Endpoints.Website.DataModel.CustomPages
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Custom page model
    /// </summary>
    [DataContract]
    public class CustomPageModel
    {
        /// <summary>
        /// Gets or sets the custom page identifier.
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
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
        public bool CustomPageFreeZone { get; set; } = false;

        /// <summary>
        /// Identifies whether the page is eLibrary (bool)
        /// </summary>
        [DataMember(Name = "eLibrary")]
        public bool CustomPageElibrary { get; set; } = false;

        /// <summary>
        /// Identifies whether the all content options selected (bool)
        /// </summary>
        [DataMember(Name = "allSelected")]
        public bool CustomPageAllSelected { get; set; }

        /// <summary>
        /// Identifies whether the custom page is a start page (bool)
        /// </summary>
        [DataMember(Name = "startPage")]
        public bool CustomPageStartPage { get; set; }

        /// <summary>
        /// Describes columns and category page section (when create CP with content)
        /// </summary>
        [DataMember(Name = "value")]
        public CustomPagesValue CustomPageValue { get; set; }

        /// <summary>
        /// Identifies whether the custom page content searchable (bool)
        /// </summary>
        [DataMember(Name = "hasSearchableContent")]
        public bool CustomPageHasSearchableContent { get; set; }

        /// <summary>
        /// Custom pages permissions (to delete, read, write, etc.)
        /// </summary>
        [DataMember(Name = "_permissions")]
        public CustomPagesPermissions CustomPagesPermissions { get; set; }

        /// <summary>
        /// Sets content
        /// </summary>
        [DataMember(Name = "content")]
        public CustomPagesContent CustomPageContent { get; set; }

        /// <summary>
        /// Describes columns and category page section (when create CP without content)
        /// </summary>
        [DataMember(Name = "layout", EmitDefaultValue = false)]
        public CustomPagesLayout CustomPageLayout { get; set; }

        /// <summary>
        /// Describes CP ACL
        /// </summary>
        [DataMember(Name = "_acl", EmitDefaultValue = false)]
        public List<CustomPagesAclModel> CustomPageAcl { get; set; }
    }
}
