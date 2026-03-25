namespace Framework.Common.Api.Endpoints.Foldering.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The keycite info.
    /// </summary>
    [DataContract]
    public class KeyciteInfo
    {
        /// <summary>
        /// Gets or sets the domain type.
        /// </summary>
        [DataMember(Name = "domainType")]
        public string DomainType { get; set; }

        /// <summary>
        /// Gets or sets the truncated title.
        /// </summary>
        [DataMember(Name = "truncatedTitle")]
        public string TruncatedTitle { get; set; }

        /// <summary>
        /// Gets or sets the doc link.
        /// </summary>
        [DataMember(Name = "docLink")]
        public string DocLink { get; set; }

        /// <summary>
        /// Gets or sets the short text.
        /// </summary>
        [DataMember(Name = "shortText")]
        public string ShortText { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the kc flag color.
        /// </summary>
        [DataMember(Name = "kcFlagColor")]
        public string KcFlagColor { get; set; }

        /// <summary>
        /// Gets or sets the kc flag link.
        /// </summary>
        [DataMember(Name = "kcFlagLink")]
        public string KcFlagLink { get; set; }

        /// <summary>
        /// Gets or sets the treatment.
        /// </summary>
        [DataMember(Name = "treatment")]
        public string Treatment { get; set; }
    }
}
