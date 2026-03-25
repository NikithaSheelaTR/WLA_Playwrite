namespace Framework.Common.Api.Endpoints.WestKM.DataModel.CiteRecognition
{
    using System.Xml.Serialization;

    /// <summary>
    /// The response.
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class Response
    {
        /// <summary>
        /// Gets or sets the attribute n.
        /// </summary>
        [XmlAttribute("n")]
        public string AttributeN { get; set; }

        /// <summary>
        /// Gets or sets the attribute dt.
        /// </summary>
        [XmlAttribute("dt")]
        public string AttributeDt { get; set; }

        /// <summary>
        /// Gets or sets the attribute t.
        /// </summary>
        [XmlAttribute("t")]
        public string AttributeT { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [XmlText]
        public string Value { get; set; }
    }
}
