namespace Framework.Common.Api.Endpoints.Search.DataModel.TrdLa
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Default Sort Types
    /// </summary>
    [DataContract]
    public class DefaultSortTypes
    {
        /// <summary>
        /// Gets or sets REGULATION
        /// </summary>
        [DataMember(Name = "REGULATION")]
        public string Regulation { get; set; }

        /// <summary>
        /// Gets or sets DOCKET
        /// </summary>
        [DataMember(Name = "DOCKET")]
        public string Docket { get; set; }

        /// <summary>
        /// Gets or sets DOCKET-PRE
        /// </summary>
        [DataMember(Name = "DOCKET-PRE")]
        public string DocketPre { get; set; }

        /// <summary>
        /// Gets or sets NEWS
        /// </summary>
        [DataMember(Name = "NEWS")]
        public string News { get; set; }
    }
}