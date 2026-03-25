namespace Framework.Core.Utils.Configuration
{
    using System.Configuration;

    using Framework.Core.Utils.Verification;

    /// <summary>
    /// The class to read the test repository.
    /// </summary>
    internal abstract class TestReaderBase
    {
        /// <summary>
        ///  Reads the repository data from the specified configuration file.
        /// </summary>
        /// <param name="configFileName">The configuration file name.
        /// </param>
        internal abstract void ReadSettings(string configFileName);

        /// <summary>
        /// Gets the specified configuration file structure by its name.
        /// </summary>
        /// <param name="configFileName">The configuration file name.</param>
        /// <returns>The <see cref="Configuration"/>.</returns>
        protected Configuration GetConfigurationObject(string configFileName)
        {
            Assertion.FileExists(configFileName);

            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = configFileName };
            return ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        }
    }
}