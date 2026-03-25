namespace Framework.Common.Api.Endpoints.Uds.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The access controls map.
    /// </summary>
    [DataContract]
    public class AccessControlsMap
    {
        /// <summary>
        /// Gets or sets the application.
        /// </summary>
        [DataMember(Name = "APPLICATION")]
        public Application Application { get; set; }
    }
}