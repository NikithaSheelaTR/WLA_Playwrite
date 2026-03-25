namespace Framework.Common.Api.Endpoints.Nlu.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The nlu legal analytics intent v 3.
    /// </summary>
    [DataContract]
    public class NluLegalAnalyticsIntentV3 : NluIntentV3
    {
        /// <summary>
        /// The result for TRD
        /// </summary>
        [DataMember(Name = "result")]
        public LegalAnalyticsResult Result { get; set; }
    }
}
