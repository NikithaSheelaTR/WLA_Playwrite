namespace Framework.Common.Api.Endpoints.TypeAhead.DataModel.V2DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The Display Citation data contact
    /// </summary>
    [DataContract]
    public class DisplayCitation
    {
        /// <summary>
        /// citation
        /// </summary>
        [DataMember(Name = "citation")]
        public string Citation { get; set; }
    }
}