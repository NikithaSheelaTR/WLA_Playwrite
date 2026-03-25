namespace Framework.Core.DataModel.Configuration.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Provides a means of storing information about QualityTestCases.
    /// </summary>
    [Serializable, XmlRoot("QualityTestCases")]
    public sealed class QualityTestCases
    {
        /// <summary>
        /// Gets or sets a lst of QualityTestCase
        /// </summary>
        [XmlElement("QualityTestCase")]
        public List<QualityTestCaseInfo> ListQualityTestCase { get; set; }  = new List<QualityTestCaseInfo>();
    }
}