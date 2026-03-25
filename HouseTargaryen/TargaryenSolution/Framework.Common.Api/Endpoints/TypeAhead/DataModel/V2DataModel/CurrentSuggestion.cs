namespace Framework.Common.Api.Endpoints.TypeAhead.DataModel.V2DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The Current Suggestion data contract
    /// </summary>
    [DataContract]
    public class CurrentSuggestion
    {
        /// <summary>
        /// subtype
        /// </summary>
        [DataMember(Name = "subtype")]
        public string Subtype { get; set; }

        /// <summary>
        /// text
        /// </summary>
        [DataMember(Name = "text")]
        public string Text { get; set; }

        /// <summary>
        /// replacementString
        /// </summary>
        [DataMember(Name = "replacementString")]
        public string ReplacementString { get; set; }

        /// <summary>
        /// suggestionType
        /// </summary>
        [DataMember(Name = "suggestionType")]
        public string SuggestionType { get; set; }

        /// <summary>
        /// isDefaultSuggestion
        /// </summary>
        [DataMember(Name = "isDefaultSuggestion")]
        public bool IsDefaultSuggestion { get; set; }

        /// <summary>
        /// pathIdentifier
        /// </summary>
        [DataMember(Name = "pathIdentifier")]
        public string PathIdentifier { get; set; }

        /// <summary>
        /// lastSegment
        /// </summary>
        [DataMember(Name = "lastSegment")]
        public string LastSegment { get; set; }

        /// <summary>
        /// exampleContinuationText
        /// </summary>
        [DataMember(Name ="exampleContinuationText")]
        public string ExampleContinuationText { get; set; }

        /// <summary>
        /// highlight
        /// </summary>
        [DataMember(Name ="highlight")]
        public Highlight Highlight { get; set; }
    }
}