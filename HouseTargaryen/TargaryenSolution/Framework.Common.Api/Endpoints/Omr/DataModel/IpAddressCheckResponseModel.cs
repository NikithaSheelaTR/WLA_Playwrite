namespace Framework.Common.Api.Endpoints.Omr.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The ip address check response model.
    /// </summary>
    [DataContract]
    public class IpAddressCheckResponseModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether check result.
        /// </summary>
        [DataMember(Name = "CheckResult")]
        public bool CheckResult { get; set; }
    }
}