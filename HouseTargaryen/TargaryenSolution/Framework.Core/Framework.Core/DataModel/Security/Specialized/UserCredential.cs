namespace Framework.Core.DataModel.Security.Specialized
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;

    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Security.Proxies;

    /// <summary>
    /// User class,  contains credentials and other information
    /// </summary>
    [Serializable]
    public class UserCredential : IOnePassUserInfo, IUserCredential, IDisposable
    {
        /// <summary>
        /// Username
        /// </summary>
        [XmlElement(ElementName = "userName")]
        public string UserName { get; set; }
        
        /// <summary>
        /// Password
        /// </summary>
        [XmlElement(ElementName = "password")]
        public string Password { get; set; }       

        /// <summary>
        /// Email
        /// </summary>
        [XmlElement(ElementName = "email")]
        public string Email { get; set; }

        /// <summary>
        /// Client ID
        /// </summary>
        [XmlElement(ElementName = "clientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// Tag used to get specific users
        /// </summary>
        [XmlElement(ElementName = "tag")]
        public string Tag { get; set; }

        /// <summary>
        /// User's type in Riptide
        /// </summary>
        [XmlElement(ElementName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Any registration keys the user might have
        /// </summary>
        [XmlElement(ElementName = "regKey")]
        public List<string> RegKeys { get; set; } = new List<string>();

        /// <summary>
        /// List of WestlawNextEnvironment values signifying what environments this user exists in
        /// </summary>
        [XmlElement(ElementName = "environment")]
        public List<EnvironmentId> Environments { get; set; }

        /// <summary>
        /// FirstName
        /// </summary>
        [XmlIgnore]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets a value indicating whether is disposed.
        /// </summary>
        [XmlIgnore]
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// LastName
        /// </summary>
        [XmlIgnore]
        public string LastName { get; set; }

        /// <summary>
        /// Prism GUID
        /// </summary>
        [XmlElement(ElementName = "prismGuid")]
        public string PrismGuid { get; set; }

        /// <summary>
        /// Matter ID for the user
        /// </summary>
        [XmlElement(ElementName = "matterId")]
        public string MatterId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether retry client id selection on failure.
        /// </summary>
        public bool RetryClientIdSelectionOnFailure { get; set; }

        /// <summary>
        /// Gets the unique key.
        /// </summary>
        [XmlIgnore]
        public string UniqueKey => this.PrismGuid;

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose() => this.IsDisposed = true;

        /// <summary>
        /// The to one pass user info.
        /// </summary>
        /// <returns>
        /// The <see cref="OnePassUserInfo"/>.
        /// </returns>
        public OnePassUserInfo ToOnePassUserInfo() =>
            new OnePassUserInfo
            {
                UserName = this.UserName,              
                Password = this.Password,
                PrismGuid = this.PrismGuid,
                OnePassEmail = this.Email
            };

        /// <summary>
        /// The to wln user info.
        /// </summary>
        /// <returns>
        /// The <see cref="WlnUserInfo"/>.
        /// </returns>
        public WlnUserInfo ToWlnUserInfo() =>
            new WlnUserInfo
            {
                UserName = this.UserName,
                Email = this.Email,
                ClientId = this.ClientId,
                MatterId = this.MatterId,
                Password = this.Password,
                RetryClientIdSelectionOnFailure = this.RetryClientIdSelectionOnFailure,
                CurrentRegKey = this.RegKeys.Count > 0 ? this.RegKeys.First() : string.Empty
            };
    }
}