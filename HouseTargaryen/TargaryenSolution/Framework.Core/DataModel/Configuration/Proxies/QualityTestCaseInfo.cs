namespace Framework.Core.DataModel.Configuration.Proxies
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Provides a means of storing information about QualityTestCase.
    /// </summary>
    [Serializable, XmlRoot("QualityTestCase")]
    public sealed class QualityTestCaseInfo
    {
        /// <summary>
        /// Gets or sets a name of test.
        /// </summary>
        [XmlElement("TestName")]
        public string TestName { get; set; }

        /// <summary>
        /// Gets or sets a path of test.
        /// </summary>
        [XmlElement("TestPath")]
        public string TestPath { get; set; }

        /// <summary>
        /// Gets or sets the attachments.
        /// </summary>
        [XmlElement("Attachments")]
        public AttachmentsInfo Attachments { get; set; } = new AttachmentsInfo();

        /// <summary>
        /// Gets or sets the QualityChecks.
        /// </summary>
        [XmlElement("QualityChecks")]
        public QualityChecksInfo QualityChecks { get; set; } = new QualityChecksInfo();
    }
}