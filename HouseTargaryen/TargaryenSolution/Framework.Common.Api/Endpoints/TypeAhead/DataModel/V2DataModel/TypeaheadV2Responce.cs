namespace Framework.Common.Api.Endpoints.TypeAhead.DataModel.V2DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The TypeaheadV2Responce data contract
    /// </summary>
    [DataContract]
    public class TypeaheadV2Responce
    {
        /// <summary>
        /// responses
        /// </summary>
        [DataMember(Name = "responses")]
        public Responses Responses { get; set; }
    }
}