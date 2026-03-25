namespace Framework.Common.Api.Endpoints.Foldering.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The collaborator.
    /// </summary>
    [DataContract]
    public class Collaborator
    {
        /// <summary>
        /// Gets or sets the collaborator id.
        /// </summary>
        [DataMember(Name = "collaboratorId")]
        public string CollaboratorId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether current user.
        /// </summary>
        [DataMember(Name = "currentUser")]
        public bool CurrentUser { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [DataMember(Name = "email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        [DataMember(Name = "role")]
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}