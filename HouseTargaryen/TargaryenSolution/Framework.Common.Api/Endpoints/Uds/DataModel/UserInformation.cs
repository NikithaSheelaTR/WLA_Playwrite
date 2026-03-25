namespace Framework.Common.Api.Endpoints.Uds.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The user information.
    /// </summary>
    [DataContract]
    public class UserInformation
    {
        /// <summary>
        /// Gets or sets a value indicating whether is confirmed user.
        /// </summary>
        [DataMember(Name = "isConfirmedUser")]
        public bool IsConfirmedUser { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is messaging confirmed.
        /// </summary>
        [DataMember(Name = "isMessagingConfirmed")]
        public bool IsMessagingConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the issuer user id.
        /// </summary>
        [DataMember(Name = "issuerUserId")]
        public string IssuerUserId { get; set; }

        /// <summary>
        /// Gets or sets the user guid.
        /// </summary>
        [DataMember(Name = "userGuid")]
        public string UserGuid { get; set; }

        /// <summary>
        /// Gets or sets the user type.
        /// </summary>
        [DataMember(Name = "userType")]
        public string UserType { get; set; }
    }
}