namespace Framework.Core.DataModel.Configuration.Proxies
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Provides a means of storing information about a release set.
    /// </summary>
    [Serializable, XmlRoot("ReleaseSet")]
    public sealed class ReleaseSetInfo
    {
        /// <summary>
        /// Get or sets the name of this release set.
        /// </summary>
        /// <value>The name of this release set.</value>
        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the environment in which this release set exists.
        /// </summary>
        /// <value>The environment in which this release set exists.</value>
        [XmlElement("Environment")]
        public string Environment { get; set; }
    }
}