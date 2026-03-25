namespace Framework.Common.Api.Endpoints.Document
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using System.Xml;

    using Framework.Common.Api.Endpoints.DocPersist;
    using Framework.Common.Api.Endpoints.Document.DataModel;
    using Framework.Common.Api.Endpoints.Document.DataModel.Constants;
    using Framework.Common.Api.Endpoints.Uds.DataModel;
    using Framework.Common.Api.Utilities;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Utils;

    using HtmlAgilityPack;

    using RestSharp;

    /// <summary>
    /// Document Client provide document end point
    /// </summary>
    public class DocumentClient : BaseCobaltServiceClient
    {
        private const string ParameterName = "application/json; charset=utf-8";

        private readonly Dictionary<string, string> headers =
            new Dictionary<string, string> { { "Content-Type", "application/jsonrequest" } };

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentClient"/> class.
        /// </summary>
        /// <param name="environment"> The environment. </param>
        /// <param name="productId"> The product id. </param> 
        /// <param name="cobaltCookies"> The cobalt cookies. </param>
        /// <param name="securityHeaders"> The security headers. </param>
        public DocumentClient(
            EnvironmentInfo environment,
            CobaltProductId productId,
            CookieContainer cobaltCookies,
            NameValueCollection securityHeaders)
            : base(environment, CobaltModuleId.Document, productId, cobaltCookies, securityHeaders)
        {
            this.BaseUrl = TestConfigurationRepository.DefaultInstance.FindEndpoint(
                TestConfigurationRepository.DefaultInstance.FindModule(
                    CobaltModuleId.Document),
                TestConfigurationRepository.DefaultInstance.FindProduct(
                    this.ProductId),
                environment).Uri;
        }

        /// <summary>
        /// Gets the cookies.
        /// </summary>
        public CookieContainer Cookies => this.CobaltCookies;

        /// <summary>
        /// Gets the current environment.
        /// </summary>
        public EnvironmentInfo CurrentEnvironment => this.Environment;

        /// <summary>
        /// Gets ImageGuid mapping for docket document
        /// </summary>
        /// <param name="source"><see cref="ImageGuidsMapRequestModel"/>Request model</param>
        /// <param name="imageGuid">Docket document Guid</param>
        /// <returns><see cref="ImageGuidsMapResponseModel"/>ImageGuids map</returns>
        public ImageGuidsMapResponseModel GetDocketImageGuidsMap(ImageGuidsMapRequestModel source, string imageGuid)
        {
            IRestRequest request = this.RequestBuilder.BuildRequest(
                new RequestArguments
                {
                    DataFormat = DataFormat.Json,
                    Method = Method.POST,
                    Resource =
                        $"/Document/v1/Dockets/LocalPdfImagesMapping/{imageGuid}",
                    Data = source
                });
            return this.RestClient.Execute<ImageGuidsMapResponseModel>(request).Data;
        }

        /// <summary>
        /// The get document by source.
        /// </summary>
        /// <param name="source"> The source. </param>
        /// <returns> The <see cref="FullDocumentResponse"/>. </returns>
        public FullDocumentResponse GetDocumentBySource(string source)
        {
            IRestRequest request = this.RequestBuilder.BuildRequest(
                new RequestArguments
                {
                    Method = Method.GET, Resource = source, Headers = this.headers
                });

            this.LastResponse = this.RestClient.Execute(request);

            var documentResponse = new FullDocumentResponse
            {
                ResponseHeader = this.LastResponse.Headers,
                StatusDescription = this.LastResponse.StatusDescription,
                ResposeContent = this.LastResponse.Content
            };

            return documentResponse;
        }

        /// <summary>
        /// The get full html document response.
        /// </summary>
        /// <param name="guid"> The guid. </param>
        /// <param name="chunkNumber"> The chunk number. </param>
        /// <returns> The <see cref="FullDocumentResponse"/>. </returns>
        public FullDocumentResponse GetFullHtmlDocumentResponse(string guid, int chunkNumber = -1)
        {
            string source = DocumentConstants.FullTextPath + guid + "?" + DocumentConstants.FullTextRequiredParameters;

            if (chunkNumber > -1)
            {
                source = source + "&chunkNumber=" + chunkNumber;
            }

            IRestRequest request = this.RequestBuilder.BuildRequest(
                new RequestArguments
                {
                    Method = Method.GET, Resource = source, Headers = this.headers
                });

            this.LastResponse = this.RestClient.Execute(request);

            var xd = new HtmlDocument();
            xd.LoadHtml(this.LastResponse.Content);

            var fullDocumentResponse = new FullDocumentResponse
            {
                ResponseHeader = this.LastResponse.Headers,
                HtmlDocumentResponse = xd,
                StatusDescription = this.LastResponse.StatusDescription,
                ResposeContent = this.LastResponse.Content
            };

            return fullDocumentResponse;
        }

        /// <summary>
        /// The get meta info response.
        /// </summary>
        /// <param name="guid"> The guid. </param>
        /// <returns> The <see cref="DocumentMetaInfo"/>. </returns>
        public DocumentMetaInfo GetMetaInfoResponse(string guid)
        {
            IRestRequest request = this.RequestBuilder.BuildRequest(
                new RequestArguments
                {
                    Method = Method.GET,
                    Resource = DocumentConstants.MetaInfoPathV2 + guid,
                    Headers = this.headers
                });

            this.LastResponse = this.RestClient.Execute(request);

            DocumentMetaInfo documentMetaInfo =
                ObjectSerializer.DeserializeObject<DataContractJsonSerializer, DocumentMetaInfo>(
                    this.LastResponse.Content);

            return documentMetaInfo;
        }

        /// <summary>
        /// The get offline document.
        /// </summary>
        /// <param name="guid"> The guid. </param>
        /// <param name="chunkSizeInBytes"> The chunk size in bytes. </param>
        /// <param name="chunkableHeadnotes"> The chunkable headnotes. </param>
        /// <returns> The <see cref="Dictionary{TKey,Tvalue}"/>. </returns>
        public Dictionary<string, object> GetOfflineDocumentResponse(
            string guid,
            int chunkSizeInBytes,
            bool chunkableHeadnotes)
        {
            string source = DocumentConstants.OfflineFullTextPath + guid + "?tz=central+standard+time&chunkSize="
                            + chunkSizeInBytes + "&chunkableHeadnotes=" + chunkableHeadnotes;

            IRestRequest request = this.RequestBuilder.BuildRequest(
                new RequestArguments
                {
                    Method = Method.GET, Resource = source, Headers = this.headers
                });

            this.LastResponse = this.RestClient.Execute(request);

            return JsonUtilities.DeserializeFromJsonStringToDictionaryObject(this.LastResponse.Content);
        }

        /// <summary>
        /// Persist Document return XML 
        /// </summary>
        /// <param name="docGuid">Document Id parameter</param>
        /// <returns>XML document from Persist client </returns>
        public XmlDocument PersistDocument(string docGuid)
        {
            string parameterValue =
                @"{""DocumentGUIDS"":[{""docGuid"":""" + docGuid + @""",""novusSearchHandle"":""""}]}";

            IRestRequest request = this.RequestBuilder.BuildRequest(
                new RequestArguments
                {
                    Method = Method.POST,
                    Resource = DocumentConstants.DocumentPersistPath,
                    Headers = this.headers,
                    Parameters = new List<Parameter>
                    {
                        new Parameter
                        {
                            Name = ParameterName,
                            Value = parameterValue,
                            Type = ParameterType.RequestBody
                        }
                    }
                });

            this.LastResponse = this.RestClient.Execute(request);

            List<Dictionary<string, object>> responseList = JsonUtilities.DeserializeJsonArray(
                this.LastResponse.Content);
            Dictionary<string, object> persistDictionary = responseList[0];
            var persistInfoDictionary = (Dictionary<string, object>)persistDictionary["dpi"];
            var persistedDocInfoDictionary = (Dictionary<string, object>)persistInfoDictionary["persistedDocInfo"];
            string checksum = persistedDocInfoDictionary["checkSum"].ToString();

            var docPersistClient = new DocPersistClient(
                this.CurrentEnvironment,
                this.ProductId,
                this.Cookies,
                this.SecurityHeaders);
            XmlDocument persistedDocumentXmlResponse = docPersistClient.GetPersistedDocumentXml(docGuid, checksum);

            return persistedDocumentXmlResponse;
        }

        /// <summary>
        /// The get document delivery response. Call the delivery info endpoint and return the response in a dictionary object
        /// </summary>
        /// <param name="parameterValue"> The parameter Value. </param>
        /// <returns>
        /// The <see cref="DocumentDeliveryInfo"/>.
        /// </returns>
        public DocumentDeliveryInfo PostDocumentDeliveryResponse(string parameterValue)
        {
            IRestRequest request = this.RequestBuilder.BuildRequest(
                new RequestArguments
                {
                    Method = Method.POST,
                    Resource = DocumentConstants.DeliveryInfoV1,
                    Headers = this.headers,
                    Parameters = new List<Parameter>
                    {
                        new Parameter
                        {
                            Name = ParameterName,
                            Value = parameterValue,
                            Type = ParameterType.RequestBody
                        }
                    }
                });

            this.LastResponse = this.RestClient.Execute(request);

            DocumentDeliveryInfo deliveryInfo =
                ObjectSerializer.DeserializeObject<DataContractJsonSerializer, DocumentDeliveryInfo>(
                    this.LastResponse.Content);

            return deliveryInfo;
        }

        /// <summary>
        /// The get document fo uri path responce.
        /// </summary>
        /// <param name="parameterValue"> The parameter Value. </param>
        /// <returns> The <see cref="DocumentDeliveryFoUriPathInfo"/>. </returns>
        public DocumentDeliveryFoUriPathInfo[] PostDocumentFoUriPathResponse(string parameterValue)
        {
            IRestRequest request = this.RequestBuilder.BuildRequest(
                new RequestArguments
                {
                    Method = Method.POST,
                    Resource = DocumentConstants.FoUriPath,
                    Headers = this.headers,
                    Parameters = new List<Parameter>
                    {
                        new Parameter
                        {
                            Name = ParameterName,
                            Value = parameterValue,
                            Type = ParameterType.RequestBody
                        }
                    }
                });

            this.LastResponse = this.RestClient.Execute(request);

            DocumentDeliveryFoUriPathInfo[] deliveryoUriPathInfo =
                ObjectSerializer.DeserializeObject<DataContractJsonSerializer, DocumentDeliveryFoUriPathInfo[]>(
                    this.LastResponse.Content);

            deliveryoUriPathInfo[0].FullJsonResponce = this.LastResponse.Content;

            return deliveryoUriPathInfo;
        }

        /// <summary>
        /// The post response body.
        /// </summary>
        /// <param name="endpoint"> The endpoint. </param>
        /// <param name="parameterValue"> The parameter Value. </param>
        /// <returns> The <see cref="string"/>. </returns>
        public FullDocumentResponse PostResponseBody(string endpoint, string parameterValue)
        {
            IRestRequest request = this.RequestBuilder.BuildRequest(
                new RequestArguments
                {
                    Method = Method.POST,
                    Resource = endpoint,
                    Headers = this.headers,
                    Parameters = new List<Parameter>
                    {
                        new Parameter
                        {
                            Name = ParameterName, Value = parameterValue, Type = ParameterType.RequestBody
                        }
                    }
                });

            this.LastResponse = this.RestClient.Execute(request);

            var fullDocumentResponse = new FullDocumentResponse
            {
                ResponseHeader = this.LastResponse.Headers,
                ResposeContent = this.LastResponse.Content,
                StatusDescription = this.LastResponse.StatusDescription
            };

            return fullDocumentResponse;
        }
    }
}