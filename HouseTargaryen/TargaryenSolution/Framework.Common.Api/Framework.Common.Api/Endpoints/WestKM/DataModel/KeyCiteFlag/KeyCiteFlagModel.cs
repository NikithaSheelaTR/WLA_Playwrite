namespace Framework.Common.Api.Endpoints.WestKM.DataModel.KeyCiteFlag
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;

    /// <summary>
    /// KeyCite Flag
    /// </summary>
    [Serializable]
    [XmlRoot("atlas")]
    public class KeyCiteFlagModel
    {
        /// <summary>
        /// status
        /// </summary>
        [XmlElement("status")]
        public string Status { get; set; }

        /// <summary>
        /// message
        /// </summary>
        [XmlElement("message")]
        public string Message { get; set; }

        /// <summary>
        /// citation_infoField
        /// </summary>
        [XmlElement("citation_info")]
        public List<CitationInfo> CitationInfo { get; set; }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            var formatter = new XmlSerializer(typeof(KeyCiteFlagModel));
            using (var writer = new StringWriter())
            {
                formatter.Serialize(writer, this);
                return writer.ToString();
            }
        }
    }
}
