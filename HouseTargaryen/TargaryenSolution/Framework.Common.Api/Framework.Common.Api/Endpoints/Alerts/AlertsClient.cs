namespace Framework.Common.Api.Endpoints.Alerts
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization.Json;

    using Framework.Common.Api.Endpoints.Alerts.DataModel;
    using Framework.Common.Api.Endpoints.Uds.DataModel;
    using Framework.Common.Api.Utilities;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Utils;
    using Newtonsoft.Json;
    using RestSharp;

    /// <summary>
    /// Alerts client
    /// </summary>
    public class AlertsClient : BaseCobaltServiceClient
    {
        private const string ParameterName = "application/json; charset=utf-8";

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertsClient"/> class.
        /// </summary>
        /// <param name="environment"> The environment. </param>
        /// <param name="productId"> The product id. </param>
        /// <param name="cobaltCookies"> The cobalt cookies. </param>
        /// <param name="securityHeaders"> The security headers. </param>
        public AlertsClient(
            EnvironmentInfo environment,
            CobaltProductId productId,
            CookieContainer cobaltCookies,
            NameValueCollection securityHeaders)
            : base(environment, CobaltModuleId.Alerts, productId, cobaltCookies, securityHeaders)
        {
            this.BaseUrl =
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    TestConfigurationRepository.DefaultInstance.FindModule(CobaltModuleId.Alerts),
                    TestConfigurationRepository.DefaultInstance.FindProduct(this.ProductId),
                    environment).Uri;
        }

        /// <summary>
        ///  Retrieves all alerts info of any kind of alerts
        /// </summary>
        /// <returns><see cref="AlertResponse"/></returns>
        public AlertResponse RetriveAllAlertsInfo()
        {
            string url = "/Alert/v2/alerts/retrieveAlertsByFacets";
            string parameterValue = "{\"start\":1,\"end\":100,\"facetCriteria\":[],\"sort\":{\"direction\":\"ASCENDING\",\"fieldName\":\"name\"},\"alertType\":\"All\"}";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Parameters = new List<Parameter> { new Parameter { Name = ParameterName, Value = parameterValue, Type = ParameterType.RequestBody } }
            });

            this.LastResponse = this.RestClient.Execute(request);
            AlertResponse alertResponse = ObjectSerializer.DeserializeObject<DataContractJsonSerializer, AlertResponse>(this.LastResponse.Content);

            return alertResponse;
        }

        /// <summary>
        ///  Retrieves alert by Facet Filter
        /// </summary>
        /// <param name="facetFilter">Facet filter</param>
        /// <param name="sessionInfo">Session info</param>
        /// <returns><see cref="AlertResponse"/>alert response</returns>
        public AlertResponse RetriveAlertsByFacets(FacetFilter facetFilter, UdsSessionInfo sessionInfo)
        {
            string url = "/Alert/v2/alerts/retrieveAlertsByFacets";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Data = facetFilter,
                DataFormat = DataFormat.Json
            });

            request.Parameters.Remove(request.Parameters.Where(header => header.Name == "x-cobalt-security-userguid").Select(header => header).FirstOrDefault());
            request.AddCookie("Co_SessionToken", sessionInfo.LongToken);
            request.AddHeader("x-cobalt-security-userguid", sessionInfo.PrismGuid);

            this.LastResponse = this.RestClient.Execute(request);
            return ObjectSerializer.DeserializeObject<DataContractJsonSerializer, AlertResponse>(this.LastResponse.Content);
        }

        /// <summary>
        ///  Delete alert by guid
        /// </summary>
        /// <param name="alertRequest">Alert Request</param>
        /// <param name="sessionInfo">Session Info</param>
        /// <returns><see cref="DeleteAlertResponse"/>Delete Alert response</returns>
        public DeleteAlertResponse DeleteAlertByGuid(DeleteAlertRequest alertRequest, UdsSessionInfo sessionInfo)
        {
            string url = "/Alert/v2/alerts/deleteAlerts";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Data = new object[] { alertRequest },
                DataFormat = DataFormat.Json
            });

            request.Parameters.Remove(request.Parameters.Where(header => header.Name == "x-cobalt-security-userguid").Select(header => header).FirstOrDefault());
            request.AddCookie("Co_SessionToken", sessionInfo.LongToken);
            request.AddHeader("x-cobalt-security-userguid", sessionInfo.PrismGuid);

            this.LastResponse = this.RestClient.Execute(request);
            return ObjectSerializer.DeserializeObject<DataContractJsonSerializer, DeleteAlertResponse>(this.LastResponse.Content);
        }

        /// <summary>
        /// Deletes all alerts
        /// </summary>
        /// <param name="alertResponse"> The alert Response. </param>
        public void DeleteAllAlerts(AlertResponse alertResponse)
        {
            List<DeleteAlertRequest> deleteAlertRequest = this.BuildDeleteAlertRequest(alertResponse);

            string url = "/Alert/v2/alerts/deleteAlerts";
            string parameterValue = ObjectSerializer.SerializeJsonObject<List<DeleteAlertRequest>>(deleteAlertRequest);

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Parameters = new List<Parameter> { new Parameter { Name = ParameterName, Value = parameterValue, Type = ParameterType.RequestBody } },
                DataFormat = DataFormat.Json
            });

            this.LastResponse = this.RestClient.Execute(request);
        }

        /// <summary>
        /// Create Docket Track Alert
        /// </summary>
        /// <param name="requestBody"> requestBody  </param>
        /// <param name="sessionInfo"> sessionInfo</param>
        /// <param name="tries"> tries number, 5 by default</param>
        /// <returns> The <see cref="AlertCreateReponse"/> Alert creation response </returns>
        public AlertCreateReponse CreateDocketTrackAlert(string requestBody, UdsSessionInfo sessionInfo, int tries = 5)
        {
            dynamic json = JsonConvert.DeserializeObject(requestBody);
            string url = "Alert/v3/alert/DocketTrack";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Data = json,
                DataFormat = DataFormat.Json
            });

            request.Parameters.Remove(request.Parameters.Where(header => header.Name == "x-cobalt-security-userguid").Select(header => header).FirstOrDefault());
            request.AddCookie("Co_SessionToken", sessionInfo.LongToken);
            request.AddHeader("x-cobalt-security-userguid", sessionInfo.PrismGuid);

            this.LastResponse = this.RestClient.ExecuteUntil(request, new[] { HttpStatusCode.Created, HttpStatusCode.BadRequest }, tries);

            return JsonConvert.DeserializeObject<AlertCreateReponse>(this.LastResponse.Content);
        }

        /// <summary>
        /// Helper to build requst for deleteing alerts
        /// </summary>
        /// <param name="alertResponse"> alertResponse </param>
        /// <returns> List{DeleteAlertRequest} ></returns>
        private List<DeleteAlertRequest> BuildDeleteAlertRequest(AlertResponse alertResponse)
        {
            var deleteAlertRequest = new List<DeleteAlertRequest>();

            IEnumerable<IGrouping<string, Alert>> groupedAlertResponse = alertResponse.Alerts.GroupBy(u => u.AlertType);

            foreach (IGrouping<string, Alert> group in groupedAlertResponse)
            {
                deleteAlertRequest.Add(
                    new DeleteAlertRequest
                    {
                        AlertType = group.Key,
                        AlertGuids = group.Select(s => s.Guid).ToList()
                    });
            }

            return deleteAlertRequest;
        }

        /// <summary>
        ///  Status Code return for Alerts End point
        /// </summary>
        /// <returns><see cref="HttpStatusCode"/></returns>
        public HttpStatusCode RetriveAllAlertsInfoAndGetStatusCode()
        {
            string url = "/Alert/v2/alerts/retrieveAlertsByFacets";
            string parameterValue = "{\"start\":1,\"end\":100,\"facetCriteria\":[],\"sort\":{\"direction\":\"ASCENDING\",\"fieldName\":\"name\"},\"alertType\":\"All\"}";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Parameters = new List<Parameter> { new Parameter { Name = ParameterName, Value = parameterValue, Type = ParameterType.RequestBody } }
            });
            this.LastResponse = this.RestClient.Execute(request);
            
            return this.LastResponse.StatusCode;
        }

        /// <summary>
        /// The POST validate query request Smoke test
        /// </summary>
        /// <param name="data"> The alert Response. </param>
        /// <returns> The <see cref="HttpStatusCode"/>. </returns>
        public HttpStatusCode CreateWestClipAlertAndGetStatusCode(string data)
        {

            dynamic json = JsonConvert.DeserializeObject(data);
            string url = "Alert/v3/alert/WestClip";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Data = json,
                DataFormat = DataFormat.Json
            });

            this.LastResponse = this.RestClient.Execute(request);

            return this.LastResponse.StatusCode;
        }

        /// <summary>
        /// Deletes all alerts Smoke test
        /// </summary>
        /// <param name="data"> The alert Response. </param>
        /// /// <returns> The <see cref="HttpStatusCode"/>. </returns>
        public HttpStatusCode DeleteAlertsAndGetStatusCode(string data)
        {
            string url = "/Alert/v2/alerts/deleteAlerts";
            dynamic json = JsonConvert.DeserializeObject(data);

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Data = json,
                DataFormat = DataFormat.Json
            });

            this.LastResponse = this.RestClient.Execute(request);
            return this.LastResponse.StatusCode;
        }
    }
}
