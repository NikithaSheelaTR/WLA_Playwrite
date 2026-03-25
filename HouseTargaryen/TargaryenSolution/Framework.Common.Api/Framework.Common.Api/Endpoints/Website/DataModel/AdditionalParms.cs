namespace Framework.Common.Api.Endpoints.Website.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Additional parameters
    /// </summary>
    [DataContract]
    public class AdditionalParms
    {
        /// <summary>
        /// County
        /// </summary>
        [DataMember(Name = "COUNTY")]
        public string County { get; set; }
    }
}
