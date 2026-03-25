namespace Framework.Core.QualityChecks.Result
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;

    using Framework.Core.Utils;

    /// <summary>
    /// Provides a means of storing information about a test run.
    /// </summary>
    /// <remarks>A test run is an execution of a single test case or set of test cases.</remarks>
    public class QualityTestRun
    {
        /// <summary>
        /// Gets or sets the list of Quality Test Cases.
        /// </summary>
        /// <value>The list of Quality Test Cases.</value>
        public IList<QualityTestCase> QualityTestCases { get; set; }

        /// <summary>
        /// Initializes a new instance of the QualityTestRun class.
        /// </summary>
        public QualityTestRun()
        {
            this.QualityTestCases = new List<QualityTestCase>();
        }

        /// <summary>
        /// Determines whether the specified <see cref="QualityTestRun"/> is equal to the current <see cref="QualityTestRun"/>.
        /// </summary>
        /// <param name="aThat">The <see cref="QualityTestRun"/> to compare with the current <see cref="QualityTestRun"/>.</param>
        /// <returns><b>true</b> if the specified Object is equal to the current <see cref="QualityTestRun"/>; otherwise, <b>false</b>.</returns>
        public override bool Equals(Object aThat)
        {
            if (aThat == null || this.GetType() != aThat.GetType())
            {
                return false;
            }
            var that = (QualityTestRun)aThat;
            return EqualsUtils.AreEqual(this.QualityTestCases, that.QualityTestCases);
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="QualityTestRun"/>.</returns>
        public override int GetHashCode()
        {
            var result = HashCodeUtils.Seed;
            result = HashCodeUtils.Hash(result, this.QualityTestCases);
            return result;
        }

        /// <summary>
        /// Returns a string that represents the current <see cref="QualityTestRun"/>.
        /// </summary>
        /// <returns>A string that represents the current <see cref="QualityTestRun"/>.</returns>
        public override string ToString()
        {
            // write the list of checks out to XML
            var sb = new StringBuilder();
            var xmlSettings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "\t",
                OmitXmlDeclaration = true
            };
            using (var xmlWriter = XmlWriter.Create(sb, xmlSettings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("QualityTestRun");
                if (this.QualityTestCases != null && this.QualityTestCases.Any())
                {
                    xmlWriter.WriteStartElement("QualityTestCases");
                    foreach (QualityTestCase qualityTestCase in this.QualityTestCases)
                    {
                        qualityTestCase.ToString(xmlWriter);
                    }
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }
            return sb.ToString();
        }

        /// <summary>
        /// Writes a QualityTestRun.xml file containing the output of <see cref="ToString"/>.
        /// </summary>
        public void ToXmlFile()
        {
            string testRunXml = this.ToString();
            File.WriteAllText("QualityTestRun.xml", testRunXml);
        }
    }
}
