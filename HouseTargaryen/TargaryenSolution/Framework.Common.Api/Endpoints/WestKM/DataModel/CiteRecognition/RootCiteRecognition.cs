namespace Framework.Common.Api.Endpoints.WestKM.DataModel.CiteRecognition
{
    using System.Xml.Serialization;

    /// <summary>
    /// The root p.
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class RootCiteRecognition
    {
        /// <summary>
        /// Gets or sets the t.
        /// </summary>
        [XmlAttribute("t")]
        public string AttributeT { get; set; }

        /// <summary>
        /// Gets or sets the first child.
        /// </summary>
        [XmlElement("p")]
        public FirstChild FirstChild { get; set; }
    }
}
