namespace Framework.Common.Api.Endpoints.TypeAhead.DataModel.V3DataModel
{
   
    using System.Runtime.Serialization;

    /// <summary>
    /// The TypeaheadV2Responce data contract
    /// </summary>
    [DataContract]
    public class TypeaheadV3Response
    {
        /// <summary>
        /// responses
        /// </summary>
        [DataMember(Name = "responses")]
        public Responses Responses { get; set; }
    }
}
    

