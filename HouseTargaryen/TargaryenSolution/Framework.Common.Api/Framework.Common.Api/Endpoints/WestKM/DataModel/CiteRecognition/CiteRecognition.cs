namespace Framework.Common.Api.Endpoints.WestKM.DataModel.CiteRecognition
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;

    /// <summary>
    /// CiteRecognition model
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "root")]
    public class CiteRecognition
    {
        /// <summary>
        /// Gets or sets the root element.
        /// </summary>
        [XmlElement("p")]
        public RootCiteRecognition RootElement { get; set; }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// The get response values.
        /// </summary>
        /// <returns>
        /// The <see cref="List{Response}"/>.
        /// </returns>
        public List<Response> GetResponseValues() => new List<Response>(this.RootElement.FirstChild.ResponcesList);
        
        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            var formatter = new XmlSerializer(typeof(CiteRecognition));
            using (var writer = new StringWriter())
            {
                formatter.Serialize(writer, this);
                return writer.ToString();
            }
        }
    }
}
