namespace Framework.Common.Api.Endpoints.CaseNoteBook.DataModel.RequestBody
{
    using System.Xml.Serialization;

    /// <summary>
    /// The key cite value.
    /// </summary>
    [XmlType(AnonymousType = true), XmlRoot("keycite.flag.update.value", Namespace = "", IsNullable = true)]
    public class KeyCiteValue
    {
        /// <summary>
        /// Gets or sets the findorig.
        /// </summary>
        [XmlElement("findorig")]
        public string Findorig { get; set; }

        /// <summary>
        /// Gets or sets the serialnumber.
        /// </summary>
        [XmlElement("serial.number")]
        public string SerialNumber { get; set; }

        /// <summary>
        /// Gets or sets the pubnumber.
        /// </summary>
        [XmlElement("pub.number")]
        public string PubNumber { get; set; }
    }
}