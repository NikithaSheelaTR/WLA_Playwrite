namespace Framework.Common.Api.Endpoints.TypeAhead.DataModel.V2DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The Lpa data contract
    /// </summary>
    [DataContract]
    public class Lpa
    {
        /// <summary>
        /// parentType
        /// </summary>
        [DataMember(Name = "parentType")]
        public string ParentType { get; set; }

        /// <summary>
        /// type
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// guid
        /// </summary>
        [DataMember(Name = "guid")]
        public string Guid { get; set; }

        /// <summary>
        /// name
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// firstName
        /// </summary>
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// lastName
        /// </summary>
        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// fullName
        /// </summary>
        [DataMember(Name = "fullName")]
        public string FullName { get; set; }

        /// <summary>
        /// title
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// address
        /// </summary>
        [DataMember(Name = "address")]
        public LpaAddress Address { get; set; }

        /// <summary>
        /// areasOfExpertise
        /// </summary>
        [DataMember(Name = "areasOfExpertise")]
        public List<string> AreasOfExpertise { get; set; }

        /// <summary>
        /// organization
        /// </summary>
        [DataMember(Name = "organization")]
        public string Organization { get; set; }

        /// <summary>
        /// position
        /// </summary>
        [DataMember(Name = "position")]
        public string Position { get; set; }

        /// <summary>
        /// practiceAreas
        /// </summary>
        [DataMember(Name = "practiceAreas")]
        public List<string> PracticeAreas { get; set; }
    }
}