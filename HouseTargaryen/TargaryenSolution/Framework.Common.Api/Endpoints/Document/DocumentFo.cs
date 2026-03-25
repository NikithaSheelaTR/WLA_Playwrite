namespace Framework.Common.Api.Endpoints.Document
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net;
    using System.Xml;

    using Framework.Common.Api.Endpoints.Document.DataModel;
    using Framework.Common.Api.Utilities;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Utils;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// An object to contain all the relevant parts of a FO response from 
    /// the document endpoint
    /// </summary>
    public class DocumentFo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentFo"/> class.
        /// If we've already made a call to the endpoint to get the fo, this guy will create the object
        /// </summary>
        /// <param name="options"> The options. </param>
        /// <param name="environmentInfo"> The environment info. </param>
        /// <param name="cobaltProductId"> The cobalt product id. </param>
        /// <param name="cookieContainer"> The cookie container. </param>
        /// <param name="securityHeaders"> The name value collection. </param>
        public DocumentFo(
            FoOptions options,
            EnvironmentInfo environmentInfo,
            CobaltProductId cobaltProductId,
            CookieContainer cookieContainer,
            NameValueCollection securityHeaders)
        {
            string jsonPostData = DeliveryUtilities.GetUriRequestJson(options);

            var documentClient = new DocumentClient(environmentInfo, cobaltProductId, cookieContainer, securityHeaders);
            DocumentDeliveryFoUriPathInfo[] docFoUriPath = documentClient.PostDocumentFoUriPathResponse(jsonPostData);
            var uriJsonObjectArray = ObjectSerializer.DeserializeJsonToObject<object[]>(docFoUriPath[0].FullJsonResponce);

            var uriStrings = new List<List<string>>();

            foreach (KeyValuePair<string, JToken> x in (JObject)uriJsonObjectArray[0])
            {
                if (x.Key.Equals("URIs"))
                {
                    var uris = (JArray)x.Value;
                    foreach (JToken u in uris)
                    {
                        uriStrings.Add(new List<string> { u.ToString() });
                    }
                }
            }

            var singleDocData = new List<string>();
            var coverPageData = new List<string>();

            foreach (List<string> s in uriStrings)
            {
                Dictionary<string, object> uriDict = JsonUtilities.DeserializeFromJsonStringToDictionaryObject(s[0]);
                s.Add((string)uriDict["URI"]);

                if (s[1].ToLowerInvariant().Contains("singledocument"))
                {
                    singleDocData = s;
                }
                else if (s[1].ToLowerInvariant().Contains("coverpage"))
                {
                    coverPageData = s;
                }
            }

            // CoverPage
            if (options.CoverPage)
            {
                string cpFoRequestJson = DeliveryUtilities.AssembleFoRequest(jsonPostData, coverPageData[0]);
                string cpFoResponseBody =
                    documentClient.PostResponseBody(coverPageData[1], cpFoRequestJson).ResposeContent;
                this.GetAndSetCoverPageData(cpFoResponseBody);
            }

            // Single Document
            string sdFoRequestJson = DeliveryUtilities.AssembleFoRequest(jsonPostData, singleDocData[0]);
            string sdFoResponseBody = documentClient.PostResponseBody(singleDocData[1], sdFoRequestJson).ResposeContent;
            this.CreateDocumentFo(sdFoResponseBody);
        }

        /// <summary>
        /// The CoverPage's xml in an xml document
        /// </summary>
        public XmlDocument CoverPageXmlDocument { get; private set; }

        /// <summary>
        /// The CoverPage's xml in a string
        /// </summary>
        public string CoverPageXmlString { get; private set; }

        /// <summary>
        /// The fo sections' header values
        /// </summary>
        public Dictionary<string, string> FoHeaders { get; private set; }

        /// <summary>
        /// the fo in an xml document object
        /// </summary>
        public XmlDocument FoXmlDocument { get; private set; }

        /// <summary>
        /// the fo xml in a string
        /// </summary>
        public string FoXmlString { get; private set; }

        /// <summary>
        /// The metadata's section's header values
        /// </summary>
        public Dictionary<string, string> MetaDataHeaders { get; private set; }

        /// <summary>
        /// the metadata xml in a string
        /// </summary>
        public string MetaDataString { get; private set; }

        /// <summary>
        /// The metadata in an xml document object
        /// </summary>
        public XmlDocument MetaDataXmlDocument { get; private set; }

        /// <summary>
        /// The fo namespace manager.
        /// </summary>
        /// <returns>
        /// The <see cref="XmlNamespaceManager"/>.
        /// </returns>
        public XmlNamespaceManager FoNamespaceManager()
        {
            // Setting up NSManager
            var man = new XmlNamespaceManager(this.FoXmlDocument.NameTable);
            man.AddNamespace("fo", "http://www.w3.org/1999/XSL/Format");
            return man;
        }

        /// <summary>
        /// Using the endpoints response string, create a documentfo object
        /// </summary>
        /// <param name="foString">the string from the endpoint</param>
        private void CreateDocumentFo(string foString)
        {
            // split it on the boundary
            string[] responseParts = foString.Split(new[] { "------=_boundary" }, StringSplitOptions.RemoveEmptyEntries);

            // the first part has the metadata
            string metadatapart = responseParts[1];
            string fopart = responseParts[2];

            // get the headers
            string metadataHeaders = metadatapart.Substring(0, metadatapart.IndexOf("<documentMetadata>")).Trim();
            this.MetaDataHeaders = this.CreateMetadataHeaders(metadataHeaders);

            #region get the metadata xml and set in the Xml Document
            this.MetaDataString = metadatapart.Substring(metadatapart.IndexOf("<documentMetadata>"));
            this.MetaDataXmlDocument = new XmlDocument();
            this.MetaDataXmlDocument.LoadXml(this.MetaDataString);
            #endregion

            #region get the headers from fo and set the dictionary object

            // get the headers
            string foHeaders = fopart.Substring(0, fopart.IndexOf("<fo:root ")).Trim();
            this.FoHeaders = this.CreateMetadataHeaders(foHeaders);
            #endregion

            #region get the fo xml and set in the xml document
            this.FoXmlString = fopart.Substring(fopart.IndexOf("<fo:root "));
            this.FoXmlDocument = new XmlDocument();
            this.FoXmlDocument.LoadXml(this.FoXmlString);
            #endregion
        }

        /// <summary>
        /// The create metadata headers.
        /// </summary>
        /// <param name="metadataHeaders">
        /// The metadata headers.
        /// </param>
        /// <returns>
        /// The <see cref="Dictionary{TKey,Tvalue}"/>.
        /// </returns>
        private Dictionary<string, string> CreateMetadataHeaders(string metadataHeaders)
        {
            string[] headerArray = metadataHeaders.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            this.MetaDataHeaders = new Dictionary<string, string>(headerArray.Length);

            foreach (string s in headerArray)
            {
                string[] val = s.Split(new[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                this.MetaDataHeaders.Add(val[0].Trim(), val[1].Trim());
            }

            return this.MetaDataHeaders;
        }

        /// <summary>
        /// Make the request for the coverpage data, and store in the local variables
        /// </summary>
        private void GetAndSetCoverPageData(string responseString)
        {
            // split it on the boundary
            string[] responseParts = responseString.Split(
                new[] { "------=_boundary" },
                StringSplitOptions.RemoveEmptyEntries);

            // the second part has the xml
            string fopart = responseParts[2];

            
            this.CoverPageXmlString = fopart.Substring(fopart.IndexOf("<fo:root "));
            this.CoverPageXmlDocument = new XmlDocument();
            this.CoverPageXmlDocument.LoadXml(this.CoverPageXmlString);
            
        }
    }
}