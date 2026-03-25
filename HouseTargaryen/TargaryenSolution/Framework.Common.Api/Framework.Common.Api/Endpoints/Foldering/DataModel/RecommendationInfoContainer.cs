namespace Framework.Common.Api.Endpoints.Foldering.DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The recommendation info container.
    /// </summary>
    [DataContract]
    public class RecommendationInfoContainer
    {
        /// <summary>
        /// Recommended
        /// </summary>
        [DataMember(Name = "recommended")]
        public Dictionary<string, List<string>> Recommended { get; set; }
    }
}
