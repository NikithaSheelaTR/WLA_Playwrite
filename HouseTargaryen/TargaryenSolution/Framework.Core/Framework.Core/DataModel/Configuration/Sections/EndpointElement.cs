namespace Framework.Core.DataModel.Configuration.Sections
{
    using System.Configuration;

    using Framework.Core.DataModel.Configuration.Constants;

    /// <summary>
    /// Represents an endpoint to a product or module under test.
    /// </summary>
    internal sealed class EndpointElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the URI to a product or module.
        /// </summary>
        [ConfigurationProperty(ConfigurationConstants.UriPropertyName, IsRequired = true)]
        public string Uri => (string)this[ConfigurationConstants.UriPropertyName];

        [ConfigurationProperty(ConfigurationConstants.EnvironmentIdPropertyName, IsKey = true, IsRequired = true)]
        internal EnvironmentId EnvironmentId => (EnvironmentId)this[ConfigurationConstants.EnvironmentIdPropertyName];

        [ConfigurationProperty(ConfigurationConstants.ModuleIdPropertyName, IsKey = true, IsRequired = true)]
        internal CobaltModuleId ModuleId => (CobaltModuleId)this[ConfigurationConstants.ModuleIdPropertyName];

        [ConfigurationProperty(ConfigurationConstants.ProductIdPropertyName, IsKey = true, IsRequired = true)]
        internal CobaltProductId ProductId => (CobaltProductId)this[ConfigurationConstants.ProductIdPropertyName];
    }
}