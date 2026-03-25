namespace Framework.Common.Api.Endpoints.Nlu.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The nlu intent v 3.
    /// </summary>
    [DataContract]
    public class NluIntentV3
    {
        /// <summary>
        /// The Intent
        /// </summary>
        [DataMember(Name = "intent")]
        public string Intent;
    }
}
