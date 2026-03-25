using System.Runtime.Serialization;

namespace Framework.Common.Api.Endpoints.Document.DataModel.FocusHighlighting
{
    /// <summary>
    /// Concept 
    /// </summary>
    public class Concept
    {
        /// <summary>
        /// Offsets
        /// </summary>
        [DataMember(Name = "offsets")]
        public string Offsets { get; set; }

        /// <summary>
        /// Count
        /// </summary>
        [DataMember(Name = "count")]
        public int Count { get; set; }
    }
}
