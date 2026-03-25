namespace Framework.Common.Api.Endpoints.Omr.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The ip address check request model.
    /// </summary>
    [DataContract]
    public class IpAddressCheckRequestModel
    {
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [DataMember(Name = "address")]
        public string Address { get; set; }
    }
}