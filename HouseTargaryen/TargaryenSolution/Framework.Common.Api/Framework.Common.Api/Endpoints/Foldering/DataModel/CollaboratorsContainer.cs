namespace Framework.Common.Api.Endpoints.Foldering.DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The collaborators container.
    /// </summary>
    [DataContract]
    public class CollaboratorsContainer
    {
        /// <summary>
        /// Gets or sets the collaborators.
        /// </summary>
        [DataMember(Name = "collaborators")]
        public List<Collaborator> Collaborators { get; set; }
    }
}