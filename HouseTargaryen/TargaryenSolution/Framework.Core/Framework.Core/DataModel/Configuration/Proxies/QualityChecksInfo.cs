namespace Framework.Core.DataModel.Configuration.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Provides a means of storing information about QualityChecks.
    /// </summary>
    [Serializable, XmlRoot("QualityChecks")]
    public sealed class QualityChecksInfo
    {
        /// <summary>
        /// Gets or sets a list of QualityCheck.
        /// </summary>
        [XmlElement("QualityCheck")]
        public List<QualityCheckInfo> ListQualityCheck { get; set; } = new List<QualityCheckInfo>();
    }
}