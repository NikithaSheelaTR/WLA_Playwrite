namespace Framework.Core.Utils.Configuration
{
    using System.Collections.Generic;
    using System.Configuration;

    /// <summary>
    /// The class to read the test repository.
    /// </summary>
    internal sealed class TestSettingsReader : TestReaderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestSettingsReader"/> class.
        /// </summary>
        internal TestSettingsReader()
        {
            this.Settings = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets the list of test settings.
        /// </summary>
        internal IDictionary<string, string> Settings { get; private set; }

        /// <summary>
        ///  Reads the repository data from the specified configuration file.
        /// </summary>
        /// <param name="configFileName">The configuration file name.
        /// </param>
        internal override void ReadSettings(string configFileName)
        {
            Configuration config = this.GetConfigurationObject(configFileName);
            KeyValueConfigurationCollection settings = config.AppSettings.Settings;

            this.Settings.Clear();

            foreach (KeyValueConfigurationElement setting in settings)
            {
                this.Settings.Add(setting.Key, setting.Value);
            }
        }
    }
}