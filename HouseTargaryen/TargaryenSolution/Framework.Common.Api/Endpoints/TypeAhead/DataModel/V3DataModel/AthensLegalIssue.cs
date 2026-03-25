namespace Framework.Common.Api.Endpoints.TypeAhead.DataModel.V3DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// AthensLegalIssue
    /// </summary>
    
    [DataContract]
   public class AthensLegalIssue : AthensSuggestion
    {
        /// <summary>
        /// outcomes
        /// </summary>
        [DataMember(Name = "outcomes")]
        public List <string> Outcomes { get; set; }
    }
}
