namespace Framework.Common.Api.Endpoints.Website.DataModel.CustomPages
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The custom pages layout.
    /// </summary>
    [DataContract]
    public class CustomPagesLayout
    {
        /// <summary>
        /// Identifies the type of the CP
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Identifies the CP sections
        /// </summary>
        [DataMember(Name = "columns")]
        public List<CustomPagesColumn> Columns { get; set; }
    }
}
