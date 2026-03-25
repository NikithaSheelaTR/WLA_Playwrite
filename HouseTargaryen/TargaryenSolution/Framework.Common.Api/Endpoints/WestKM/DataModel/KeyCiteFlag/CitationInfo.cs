namespace Framework.Common.Api.Endpoints.WestKM.DataModel.KeyCiteFlag
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// The citation info.
    /// </summary>
    [Serializable]
    public class CitationInfo
    {
        /// <summary>
        /// Gets or sets the serial number
        /// </summary>
        [XmlElement("serial_num")]
        public string SerialNumber { get; set; }

        /// <summary>
        /// Gets or sets the flag
        /// </summary>
        [XmlElement("flag")]
        public string Flag { get; set; }
    }
}
