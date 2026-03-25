namespace Framework.Common.Api.Endpoints.Website.DataModel.CustomPages
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The custom pages section.
    /// </summary>
    [DataContract]
    public class CustomPagesSection
    {
        /// <summary>
        /// Sets the CP section type
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Sets the CP content section name
        /// </summary>
        [DataMember(Name = "label")]
        public string Label { get; set; }

        /// <summary>
        /// Sets the CP section model (with uris)
        /// </summary>
        [DataMember(Name = "model")]
        public CustomPagesSectionModel Model { get; set; }
    }
}
