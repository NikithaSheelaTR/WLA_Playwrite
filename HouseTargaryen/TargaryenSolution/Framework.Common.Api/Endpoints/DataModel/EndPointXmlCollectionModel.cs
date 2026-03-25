
namespace Framework.Common.Api.Endpoints.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// EndPoints xml collection 
    /// </summary>
    [Serializable]
    public class EndPointXmlCollectionModel
    {
        /// <summary>
        /// Field to set the serialized end points data - WlnCanadaApi
        /// </summary>
        [XmlElement("AjaxHeaders")]
        public List<EndPointXmlModel> EndpointsList;

        /// <summary>
        /// Constructor to initialise the EndPoints List
        /// </summary>
        public EndPointXmlCollectionModel()
        {
            this.EndpointsList = new List<EndPointXmlModel>();
        }
    }
}