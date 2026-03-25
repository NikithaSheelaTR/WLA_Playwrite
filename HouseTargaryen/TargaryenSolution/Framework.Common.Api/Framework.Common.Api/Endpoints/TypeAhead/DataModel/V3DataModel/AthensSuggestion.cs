namespace Framework.Common.Api.Endpoints.TypeAhead.DataModel.V3DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// suggestion from Search suggestions section (Athens)
    /// </summary>
    [DataContract]
    public class AthensSuggestion
    {
        /// <summary>
        /// display
        /// </summary>
        [DataMember(Name = "display")]
        public string Display { get; set; }

        /// <summary>
        /// value
        /// </summary>
        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}
