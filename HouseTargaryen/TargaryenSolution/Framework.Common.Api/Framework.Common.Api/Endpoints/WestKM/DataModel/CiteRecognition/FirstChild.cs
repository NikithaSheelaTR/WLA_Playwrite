namespace Framework.Common.Api.Endpoints.WestKM.DataModel.CiteRecognition
{
    using System.Xml.Serialization;

    /// <summary>
    /// The first child.
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class FirstChild
    {
        /// <summary>
        /// Gets or sets the t.
        /// </summary>
        [XmlAttribute("t")]
        public string AttributeT { get; set; }

        /// <summary>
        /// Gets or sets the responces list.
        /// </summary>
        [XmlElement("p")]
        public Response[] ResponcesList { get; set; }
    }
}
