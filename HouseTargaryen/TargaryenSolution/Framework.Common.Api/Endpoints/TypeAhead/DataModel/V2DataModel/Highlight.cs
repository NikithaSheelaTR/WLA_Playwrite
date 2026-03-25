namespace Framework.Common.Api.Endpoints.TypeAhead.DataModel.V2DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The Highlight data contract
    /// </summary>
    [DataContract]
    public class Highlight
    {
        /// <summary>
        /// title.ngram
        /// </summary>
        [DataMember(Name = "title.ngram")]
        public List<string> TitleNgram { get; set; }
    }
}