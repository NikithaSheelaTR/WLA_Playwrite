namespace Framework.Common.Api.Endpoints.TypeAhead.DataModel.V2DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The Category Page data contract
    /// </summary>
    [DataContract]
    public class CategoryPage
    {
        /// <summary>
        /// cpURI
        /// </summary>
        [DataMember(Name = "cpURI")]
        public string CpUri { get; set; }

        /// <summary>
        /// cpGuid
        /// </summary>
        [DataMember(Name = "cpGuid")]
        public string CpGuid { get; set; }

        /// <summary>
        /// display
        /// </summary>
        [DataMember(Name = "display")]
        public string Display { get; set; }

        /// <summary>
        /// alerts
        /// </summary>
        [DataMember(Name = "alerts")]
        public List<string> Alerts { get; set; }

        /// <summary>
        /// favoritable
        /// </summary>
        [DataMember(Name = "favoritable")]
        public bool Favoritable { get; set; }

        /// <summary>
        /// searchableFavorite
        /// </summary>
        [DataMember(Name = "searchableFavorite")]
        public bool SearchableFavorite { get; set; }
    }
}