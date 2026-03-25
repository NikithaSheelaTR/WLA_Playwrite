namespace Framework.Core.DataModel.Configuration.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    using Framework.Core.Utils;
    using Framework.Core.Utils.Verification;

    /// <summary>
    /// Represents a Quality Results Tracker (QRT) configuration file.
    /// </summary>
    [Serializable, XmlRoot(ElementName = "QrtTestConfig")]
    public sealed class QrtTestConfigInfo
    {
        #region Fields
        /// <summary>
        /// The default location of the local test run configuration file, used if one is not specified.
        /// </summary>
        [XmlIgnore]
        public const string DefaultLocalTestConfigFileName = "Resources/LocalTestConfig.xml";
        #endregion

        #region .ctors
        /// <summary>
        /// Initializes a new instance of the <see cref="QrtTestConfigInfo"/> class.
        /// </summary>
        public QrtTestConfigInfo()
        {
            this.NotificationEmailAddresses = new List<string>();
            this.ReleaseSets = new List<ReleaseSetInfo>();
            this.Tags = new List<string>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a name of a business case.
        /// </summary>
        /// <value>The name of a business case.</value>
        [XmlElement("BusinessCase")]
        public string BusinessCase { get; set; }

        /// <summary>
        /// Gets or set a name of a test run.
        /// </summary>
        /// <value>The name of a test run.</value>
        [XmlElement("TestRunName")]
        public string TestRunName { get; set; }

        /// <summary>
        /// Gets or sets a list of tags.
        /// </summary>
        /// <value>A list of tags.</value>
        [XmlArray("Tags"), XmlArrayItem("Tag")]
        public List<string> Tags { get; set; }

        /// <summary>
        /// Gets or sets a list of release sets.
        /// </summary>
        /// <value>A list of release sets.</value>
        [XmlArray("ReleaseSets")]
        public List<ReleaseSetInfo> ReleaseSets { get; set; }

        /// <summary>
        /// Gets or sets a list of notification email addresses.
        /// </summary>
        /// <value>A list of notification email addresses.</value>
        [XmlArray("NotificationEmail"), XmlArrayItem("Address")]
        public List<string> NotificationEmailAddresses { get; set; }
        #endregion

        #region PUBLIC methods
        /// <summary>
        /// Constructs a a Quality Results Tracker (QRT) configuration file.
        /// </summary>
        /// <param name="configFilePath">The configuration file path.</param>
        /// <returns>A QRT Test Configuration.</returns>
        public static QrtTestConfigInfo ParseQrtTestConfig(string configFilePath)
        {
            QrtTestConfigInfo result;
            Assertion.FileExists(configFilePath);

            using (Stream reader = new FileStream(configFilePath, FileMode.Open))
            {
                result = ObjectSerializer.DeserializeObject<QrtTestConfigInfo>(reader);
            }

            return result;
        }

        /// <summary>
        /// Returns a string that represents the current <see cref="QrtTestConfigInfo"/>.
        /// </summary>
        /// <returns>A string that represents the current <see cref="QrtTestConfigInfo"/>.</returns>
        public override string ToString()
        {
            // A workaround for QRT2.0 loader, which requires that no empty collection be present in the configuration file.
            // At the same time, the best practice is not to have NULL collection or array in code.
            IList<string> tags = this.Tags, emails = this.NotificationEmailAddresses;
            IList<ReleaseSetInfo> releaseSets = this.ReleaseSets;

            if (!this.Tags.Any())
            {
                this.Tags = null;
            }

            if (!this.NotificationEmailAddresses.Any())
            {
                this.NotificationEmailAddresses = null;
            }

            if (!this.ReleaseSets.Any())
            {
                this.ReleaseSets = null;
            }

            string serialisedObject = ObjectSerializer.SerializeObject(this);

            // Restore values.
            this.Tags = (List<string>)tags;
            this.NotificationEmailAddresses = (List<string>)emails;
            this.ReleaseSets = (List<ReleaseSetInfo>)releaseSets;
            return serialisedObject;
        }

        /// <summary>
        /// Writes a XML file containing the serialized <see cref="QrtTestConfigInfo"/> object.
        /// </summary>
        /// <param name="configFilePath">The configuration file path.</param>
        public void ToXmlFile(string configFilePath)
        {
            string qrtTestConfigXml = this.ToString();
            File.WriteAllText(configFilePath, qrtTestConfigXml);
        }
        #endregion
    }
}
