namespace Framework.Core.DataModel.Configuration.Sections
{
    using System.Configuration;

    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;

    /// <summary>
    /// Represents an environment under test.
    /// </summary>
    internal sealed class EnvironmentElement : EntityElement<EnvironmentId, EnvironmentType, EnvironmentInfo>
    {
        /// <summary>
        /// Gets the value whether the environment is a test or production one.
        /// </summary>
        [ConfigurationProperty(ConfigurationConstants.IsLowerPropertyName, IsRequired = true)]
        public bool IsLower => (bool)this[ConfigurationConstants.IsLowerPropertyName];

        /// <summary>
        /// Gets the environment name.
        /// </summary>
        [ConfigurationProperty(ConfigurationConstants.NamePropertyName, IsRequired = true)]
        public string Name => (string)this[ConfigurationConstants.NamePropertyName];

        /// <summary>
        /// Gets a light-weight proxy object for a corresponding configuration element.
        /// </summary>
        public override EnvironmentInfo ProxyObject =>
            new EnvironmentInfo
                {
                    Id = this.Id,
                    Name = this.Name,
                    Site = this.Site,
                    IsLower = this.IsLower,
                    Type = this.Type,
                    TagName = this.TagName
                };

        /// <summary>
        /// Gets the environment site name.
        /// </summary>
        [ConfigurationProperty(ConfigurationConstants.SitePropertyName, IsRequired = true, DefaultValue = "")]
        public string Site => (string)this[ConfigurationConstants.SitePropertyName];
    }
}