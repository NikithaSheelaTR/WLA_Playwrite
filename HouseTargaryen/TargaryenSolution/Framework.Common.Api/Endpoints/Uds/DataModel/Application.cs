namespace Framework.Common.Api.Endpoints.Uds.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The application.
    /// </summary>
    [DataContract]
    public class Application
    {
        /// <summary>
        /// Gets or sets the alert admin.
        /// </summary>
        [DataMember(Name = "ALERT ADMIN")]
        public string AlertAdmin { get; set; }

        /// <summary>
        /// Gets or sets the dataroom admin.
        /// </summary>
        [DataMember(Name = "DATAROOM ADMIN")]
        public string DataroomAdmin { get; set; }
    }
}