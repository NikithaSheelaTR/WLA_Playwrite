namespace Framework.Common.Api.Endpoints.Uds.DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The contacts info.
    /// </summary>
    [DataContract]
    public class ContactsInfo
    {
        /// <summary>
        /// Gets or sets the contacts.
        /// </summary>
        [DataMember(Name = "contacts")]
        public List<Contact> Contacts { get; set; }

        /// <summary>
        /// Gets or sets the pagination.
        /// </summary>
        [DataMember(Name = "pagination")]
        public Pagination Pagination { get; set; }
    }
}