namespace Framework.Common.Api.Endpoints.TypeAhead.DataModel.V2DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The Trdiscover data contract
    /// </summary>
    [DataContract]
    public class Trdiscover
    {
        /// <summary>
        /// numOfPaths
        /// </summary>
        [DataMember(Name = "numOfPaths")]
        public long NumOfPaths { get; set; }

        /// <summary>
        /// currentSuggestions
        /// </summary>
        [DataMember(Name = "currentSuggestions", IsRequired = false)]
        public List<CurrentSuggestion> CurrentSuggestions { get; set; }
    }
}