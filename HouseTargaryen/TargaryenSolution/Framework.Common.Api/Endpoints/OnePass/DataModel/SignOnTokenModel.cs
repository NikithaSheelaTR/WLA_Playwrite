namespace Framework.Common.Api.Endpoints.OnePass.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The sign on token model.
    /// </summary>
    [DataContract]
    public class SignOnTokenModel
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        [DataMember(Name = "Token")]
        public string Token { get; set; }
    }
}
