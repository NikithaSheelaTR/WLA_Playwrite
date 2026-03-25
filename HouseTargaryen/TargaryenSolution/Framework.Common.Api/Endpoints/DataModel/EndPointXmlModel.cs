
namespace Framework.Common.Api.Endpoints.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;
    
    /// <summary>
    /// EndPoint Xml datamodel for serialization - WlnCanadaApi
    /// </summary>
    [Serializable]
    public class EndPointXmlModel
    {
        /// <summary>
        /// The name.
        /// </summary>
        [XmlElement("Name")]
        public string Name;

        /// <summary>
        /// The method.
        /// </summary>
        [XmlElement("Method")]
        public string Method;

        /// <summary>
        /// The url.
        /// </summary>
        [XmlElement("Endpoint")]
        public string Url;

        /// <summary>
        /// The request headers.
        /// </summary>
        [XmlElement("RequestHeaders")]
        public List<EndPointXmlHeaderModel> RequestHeaders;

        /// <summary>
        /// The content type.
        /// </summary>
        [XmlElement("ContentType")]
        public string ContentType;

        /// <summary>
        /// The cobalt host.
        /// </summary>
        [XmlElement("CobaltHost")]
        public string CobaltHost;

        /// <summary>
        /// The data type.
        /// </summary>
        [XmlElement("DataType")]
        public string DataType;

        /// <summary>
        /// The data.
        /// </summary>
        [XmlElement("Data")]
        public string Data;
    }
}
