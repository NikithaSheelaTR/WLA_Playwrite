namespace Framework.Common.Api.Endpoints.Website.DataModel.CustomPages
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The custom pages value.
    /// </summary>
    [DataContract]
    public class CustomPagesValue
    {
        /// <summary>
        /// Sets the CP Value Type
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Sets the CP Columns
        /// </summary>
        [DataMember(Name = "columns")]
        public List<CustomPagesColumn> Columns { get; set; }
    }
}
