namespace Framework.Core.DataModel.Configuration.Proxies
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    using Framework.Core.QualityChecks.Result;
    using Framework.Core.Utils;
    using Framework.Core.Utils.Verification;

    /// <summary>
    /// Represents a Quality Test Results file.
    /// </summary>
    [Serializable, XmlRoot("QualityTestRun")]
    public sealed class QualityTestRunInfo
    {
        /// <summary>
        /// Gets or sets QualityTestCases
        /// </summary>
        [XmlElement("QualityTestCases")]
        public QualityTestCases QualityTestCases { get; set; } = new QualityTestCases();


        /// <summary>
        /// Convert QualityTestRun (info of test results from another one dll) to QualityTestRunInfo
        /// </summary>
        /// <param name="qualityTestRun">The value of the Quality Test Run</param>
        /// <returns>bfbf</returns>
        public QualityTestRunInfo ConvertToQualityTestRunInfo(QualityTestRun qualityTestRun)
        {
            var resultQualityTestRun = new QualityTestRunInfo();
            foreach (QualityTestCase qualityTestCase in qualityTestRun.QualityTestCases)
            {
                var tempQualityTestCaseInfo = new QualityTestCaseInfo
                               {
                                   TestName = qualityTestCase.TestName,
                                   TestPath = qualityTestCase.TestPath
                               };

                if (qualityTestCase.Attachments.Count != 0)
                {
                    tempQualityTestCaseInfo.Attachments.Attachment = qualityTestCase.Attachments;
                }

                foreach (QualityCheck qualityCheck in qualityTestCase.QualityChecks)
                {
                    var tempQualityCheckInfo = new QualityCheckInfo
                                                     {
                                                         Order = qualityCheck.Order.ToString(),
                                                         DateTime = qualityCheck.DateTime.Value.ToUniversalTime(),
                                                         Message = qualityCheck.Message,
                                                         Outcome = qualityCheck.Outcome.ToString(),
                                                         Type = qualityCheck.QualityCheckType.ToString(),
                                                         Name = qualityCheck.Name
                                                     };
                    tempQualityTestCaseInfo.QualityChecks.ListQualityCheck.Add(tempQualityCheckInfo);
                }

                resultQualityTestRun.QualityTestCases.ListQualityTestCase.Add(tempQualityTestCaseInfo);
            }

            return resultQualityTestRun;
        }

        /// <summary>
        ///  Deserialize QualityTestRun.xml
        /// </summary>
        /// <param name="configFilePath">QualityTestRun.xml</param>
        /// <returns>Object QualityTestRunInfo</returns>
        public QualityTestRunInfo DeserializeQualityTestRun(string configFilePath)
        {
            QualityTestRunInfo result;
            Assertion.FileExists(configFilePath);

            using (Stream reader = new FileStream(configFilePath, FileMode.Open))
            {
                result = ObjectSerializer.DeserializeObject<QualityTestRunInfo>(reader);
            }

            return result;
        }

        /// <summary>
        /// Serialize QualityTestRun to QualityTestRun.xml
        /// </summary>
        public void SerializeQualityTestRun()
        {
            var settings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };
            using (var stream = new StreamWriter("QualityTestRun.xml"))
            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                var ns = new XmlSerializerNamespaces();
                ns.Add(string.Empty, string.Empty);
                var serializer = new XmlSerializer(this.GetType());

                serializer.Serialize(writer, this, ns);
            }
        }
    }
}