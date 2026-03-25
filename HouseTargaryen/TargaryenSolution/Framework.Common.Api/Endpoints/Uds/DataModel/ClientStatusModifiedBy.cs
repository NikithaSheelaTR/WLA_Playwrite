namespace Framework.Common.Api.Endpoints.Uds.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The client status modified by.
    /// </summary>
    [DataContract]
    public class ClientStatusModifiedBy
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is current user.
        /// </summary>
        [DataMember(Name = "isCurrentUser")]
        public bool IsCurrentUser { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the user guid.
        /// </summary>
        [DataMember(Name = "userGuid")]
        public string UserGuid { get; set; }
    }
}