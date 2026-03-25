namespace Framework.Core.Utils.Configuration
{
    using System;
    using System.Configuration;

    using Framework.Core.DataModel.Configuration.Collections;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Sections;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The class to read the test repository.
    /// </summary>
    internal sealed class TestRepositoryReader : TestReaderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestRepositoryReader"/> class.
        /// </summary>
        internal TestRepositoryReader()
        {
            this.EnvironmentDefinitions = new EnvironmentElementCollection();
            this.EndpointDefinitions = new EndpointElementCollection();
            this.ModuleDefinitions = new ModuleElementCollection();
            this.ProductDefinitions = new ProductElementCollection();
        }

        /// <summary>
        /// Gets the list of endpoints.
        /// </summary>
        internal EndpointElementCollection EndpointDefinitions { get; private set; }

        /// <summary>
        /// Gets the list of environments.
        /// </summary>
        internal EnvironmentElementCollection EnvironmentDefinitions { get; private set; }

        /// <summary>
        /// Gets the list of Cobalt modules.
        /// </summary>
        internal ModuleElementCollection ModuleDefinitions { get; private set; }

        /// <summary>
        /// Gets the list of Cobalt products.
        /// </summary>
        internal ProductElementCollection ProductDefinitions { get; private set; }

        /// <summary>
        /// Gets the list of test clients.
        /// </summary>
        internal TestClientElementCollection TestClientDefinitions { get; private set; }

        /// <summary>
        ///  Reads the repository data from the specified configuration file.
        /// </summary>
        /// <param name="configFileName">The configuration file name.
        /// </param>
        internal override void ReadSettings(string configFileName)
        {
            Configuration config = this.GetConfigurationObject(configFileName);
            Action<object, string> assertElementPresence =
                (section, sectionName) =>
                    Assert.IsNotNull(section, "Section '{0}' was not found in '{1}'.", sectionName, configFileName);
            var serverSideRepositorySection =
                (RemoteConfigurationSection)config.GetSection(ConfigurationConstants.RemoteRepositorySectionName);
            var localSideRepositorySection =
                (LocalConfigurationSection)config.GetSection(ConfigurationConstants.LocalRepositorySectionName);

            assertElementPresence(serverSideRepositorySection, ConfigurationConstants.RemoteRepositorySectionName);
            assertElementPresence(localSideRepositorySection, ConfigurationConstants.LocalRepositorySectionName);

            this.EnvironmentDefinitions = serverSideRepositorySection.EnvironmentDefinitions;
            assertElementPresence(this.EnvironmentDefinitions, ConfigurationConstants.EnvironmentCollectionSectionName);
            this.EndpointDefinitions = serverSideRepositorySection.EndpointDefinitions;
            assertElementPresence(this.EndpointDefinitions, ConfigurationConstants.EndpointCollectionSectionName);
            this.ModuleDefinitions = serverSideRepositorySection.ModuleDefinitions;
            assertElementPresence(this.ModuleDefinitions, ConfigurationConstants.ModuleCollectionSectionName);
            this.ProductDefinitions = serverSideRepositorySection.ProductDefinitions;
            assertElementPresence(this.ProductDefinitions, ConfigurationConstants.ProductCollectionSectionName);
            this.TestClientDefinitions = localSideRepositorySection.TestClientDefinitions;
            assertElementPresence(this.TestClientDefinitions, ConfigurationConstants.TestClientCollectionSectionName);
        }
    }
}