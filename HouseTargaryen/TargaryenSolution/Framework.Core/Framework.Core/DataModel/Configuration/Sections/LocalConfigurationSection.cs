namespace Framework.Core.DataModel.Configuration.Sections
{
    using System.Configuration;

    using Framework.Core.DataModel.Configuration.Collections;
    using Framework.Core.DataModel.Configuration.Constants;

    /// <summary>
    /// Describes a section with the definitions of entities specific to a local machine.
    /// </summary>
    internal class LocalConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// Gets the test client definitions.
        /// </summary>
        [ConfigurationProperty(ConfigurationConstants.TestClientCollectionSectionName, IsDefaultCollection = false)]
        internal TestClientElementCollection TestClientDefinitions 
            => (TestClientElementCollection)base[ConfigurationConstants.TestClientCollectionSectionName];
    }
}