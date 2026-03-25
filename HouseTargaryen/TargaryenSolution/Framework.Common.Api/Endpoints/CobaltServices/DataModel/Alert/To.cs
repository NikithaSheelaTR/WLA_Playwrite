namespace Framework.Common.Api.Endpoints.CobaltServices.DataModel.Alert
{
    using System.Runtime.Serialization;

    /// <summary>
    /// To data model
    /// </summary>
    [DataContract]
    public class To
    {
        /// <summary>
        /// Id
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        [DataMember(Name = "value")]
        public string Value { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}
