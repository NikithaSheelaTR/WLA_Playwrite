namespace Framework.Core.DataModel.Configuration.Sections
{
    using System.Configuration;

    using Framework.Core.DataModel.Configuration.Collections;
    using Framework.Core.DataModel.Configuration.Constants;

    /// <summary>
    /// Describes a section with the definitions of configuration entities on a client.
    /// </summary>
    internal class RemoteConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// Gets the endpoint definitions.
        /// </summary>
        [ConfigurationProperty(ConfigurationConstants.EndpointCollectionSectionName, IsDefaultCollection = false)]
        internal EndpointElementCollection EndpointDefinitions
            => (EndpointElementCollection)base[ConfigurationConstants.EndpointCollectionSectionName];

        /// <summary>
        /// Gets the environment definitions.
        /// </summary>
        [ConfigurationProperty(ConfigurationConstants.EnvironmentCollectionSectionName, IsDefaultCollection = false)]
        internal EnvironmentElementCollection EnvironmentDefinitions
            => (EnvironmentElementCollection)base[ConfigurationConstants.EnvironmentCollectionSectionName];

        /// <summary>
        /// Gets the module definitions.
        /// </summary>
        [ConfigurationProperty(ConfigurationConstants.ModuleCollectionSectionName, IsDefaultCollection = false)]
        internal ModuleElementCollection ModuleDefinitions
            => (ModuleElementCollection)base[ConfigurationConstants.ModuleCollectionSectionName];

        /// <summary>
        /// Gets the product definitions.
        /// </summary>
        [ConfigurationProperty(ConfigurationConstants.ProductCollectionSectionName, IsDefaultCollection = false)]
        internal ProductElementCollection ProductDefinitions
            => (ProductElementCollection)base[ConfigurationConstants.ProductCollectionSectionName];
    }
}