namespace Framework.Core.DataModel.Configuration.Proxies
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Provides a means of storing information about QualityCheck .
    /// </summary>
    [Serializable, XmlRoot("QualityCheck")]
    public sealed class QualityCheckInfo
    {
        /// <summary>
        /// Gets or sets a name.
        /// </summary>
        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a type.
        /// </summary>
        [XmlElement("Type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets an outcome.
        /// </summary>
        [XmlElement("Outcome")]
        public string Outcome { get; set; }

        /// <summary>
        /// Gets or sets a message.
        /// </summary>
        [XmlElement("Message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets an order.
        /// </summary>
        [XmlElement("Order")]
        public string Order { get; set; }

        /// <summary>
        /// Gets or sets a date time.
        /// </summary>
        [XmlElement("DateTime")]
        public DateTime DateTime { get; set; }
    }
}