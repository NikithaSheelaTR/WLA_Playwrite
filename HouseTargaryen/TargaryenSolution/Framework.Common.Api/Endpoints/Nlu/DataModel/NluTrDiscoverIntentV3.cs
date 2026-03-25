namespace Framework.Common.Api.Endpoints.Nlu.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The nlu tr discover intent v 3.
    /// </summary>
    [DataContract]
    public class NluTrDiscoverIntentV3 : NluIntentV3
    {
        /// <summary>
        /// The result for TRD
        /// </summary>
        [DataMember(Name = "result")]
        public string Result { get; set; }
    }
}
