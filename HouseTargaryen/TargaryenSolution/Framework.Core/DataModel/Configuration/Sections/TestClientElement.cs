namespace Framework.Core.DataModel.Configuration.Sections
{
    using System;
    using System.Configuration;

    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;

    /// <summary>
    /// Represents a test client element.
    /// </summary>
    internal sealed class TestClientElement : EntityElement<TestClientId, TestClientType, TestClientInfo>
    {
        /// <summary>
        /// Gets the alias of the test client.
        /// </summary>
        [ConfigurationProperty(ConfigurationConstants.AliasPropertyName, IsRequired = true)]
        public string Alias => (string)this[ConfigurationConstants.AliasPropertyName];

        /// <summary>
        /// Gets the alias of the test client.
        /// </summary>
        [ConfigurationProperty(ConfigurationConstants.FamilyPropertyName, IsRequired = true)]
        public TestClientFamily Family => (TestClientFamily)this[ConfigurationConstants.FamilyPropertyName];

        /// <summary>
        /// Gets the path.
        /// </summary>
        [ConfigurationProperty(ConfigurationConstants.UriPropertyName, IsRequired = false)]
        public string Path
        {
            get
            {
                string path = (string)this[ConfigurationConstants.UriPropertyName];
                return string.IsNullOrWhiteSpace(path)
                           ? string.Empty
                           : Environment.ExpandEnvironmentVariables(path.Trim());
            }
        }

        /// <summary>
        /// Gets the Remote Driver.
        /// </summary>
        [ConfigurationProperty(ConfigurationConstants.RemoteDriverUriPropertyName, IsRequired = false)]
        public string RemoteDriverUri => (string)this[ConfigurationConstants.RemoteDriverUriPropertyName];

        /// <summary>
        /// Gets a light-weight proxy object for a corresponding configuration element.
        /// </summary>
        public override TestClientInfo ProxyObject =>
            new TestClientInfo
            {
                Id = this.Id,
                Alias = this.Alias,
                Type = this.Type,
                TagName = this.TagName,
                Family = this.Family,
                PathToExecutable = this.Path,
                RemoteDriverUri = string.IsNullOrEmpty(this.RemoteDriverUri) ? null : new Uri(this.RemoteDriverUri)
            };
    }
}