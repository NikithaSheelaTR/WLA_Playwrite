
namespace Framework.Common.Api.Endpoints.DataModel
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// End Point Xml's Request Header field data model - WlnCanadaApi
    /// </summary>
    [Serializable]
    public class EndPointXmlHeaderModel
    {
        /// <summary>
        /// Header Field Name
        /// </summary>
        [XmlElement("HeaderFieldName")]
        public string HeaderFieldName;

        /// <summary>
        /// Header Field Value
        /// </summary>
        [XmlElement("HeaderFieldValue")]
        public string HeaderFieldValue;

        /// <summary>
        /// Prevents a default instance of the <see cref="EndPointXmlHeaderModel"/> class from being created. 
        /// Empty required for 
        /// </summary>
        private EndPointXmlHeaderModel()
        {
        }
    }
}
