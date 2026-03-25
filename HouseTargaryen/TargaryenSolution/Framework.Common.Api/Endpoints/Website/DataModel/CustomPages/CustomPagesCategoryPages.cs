namespace Framework.Common.Api.Endpoints.Website.DataModel.CustomPages
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The custom pages category pages.
    /// </summary>
    [DataContract]
    public class CustomPagesCategoryPages
    {
        /// <summary>
        /// CP name (in the format Home/Cases)
        /// </summary>
        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        /// <summary>
        /// CP label (e.g. Cases)
        /// </summary>
        [DataMember(Name = "label")]
        public string Label { get; set; }

        /// <summary>
        /// Identifies whether the content is searchable (bool)
        /// </summary>
        [DataMember(Name = "isSearchable")]
        public bool IsSearchable { get; set; }
    }
}
