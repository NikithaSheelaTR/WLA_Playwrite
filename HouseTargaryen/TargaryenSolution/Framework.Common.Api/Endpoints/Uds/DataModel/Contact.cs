namespace Framework.Common.Api.Endpoints.Uds.DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The contact.
    /// </summary>
    [DataContract]
    public class Contact
    {
        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        [DataMember(Name = "addresses")]
        public List<object> Addresses { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether archived.
        /// </summary>
        [DataMember(Name = "archived")]
        public bool Archived { get; set; }

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        [DataMember(Name = "client")]
        public Client Client { get; set; }

        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        [DataMember(Name = "companyName")]
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        [DataMember(Name = "createdBy")]
        public UserByInfo CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        [DataMember(Name = "createdDate")]
        public string CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the emails.
        /// </summary>
        [DataMember(Name = "emails")]
        public List<object> Emails { get; set; }

        /// <summary>
        /// Gets or sets the external references.
        /// </summary>
        [DataMember(Name = "externalReferences")]
        public List<object> ExternalReferences { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether hidden.
        /// </summary>
        [DataMember(Name = "hidden")]
        public bool Hidden { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is public.
        /// </summary>
        [DataMember(Name = "isPublic")]
        public bool IsPublic { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is restricted.
        /// </summary>
        [DataMember(Name = "isRestricted")]
        public bool IsRestricted { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        [DataMember(Name = "modifiedBy")]
        public UserByInfo ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        [DataMember(Name = "modifiedDate")]
        public string ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the phones.
        /// </summary>
        [DataMember(Name = "phones")]
        public List<object> Phones { get; set; }

        /// <summary>
        /// Gets or sets the practice areas.
        /// </summary>
        [DataMember(Name = "practiceAreas")]
        public List<object> PracticeAreas { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        [DataMember(Name = "roles")]
        public List<object> Roles { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether trashed.
        /// </summary>
        [DataMember(Name = "trashed")]
        public bool Trashed { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the user information.
        /// </summary>
        [DataMember(Name = "userInformation")]
        public UserInformation UserInformation { get; set; }
    }
}