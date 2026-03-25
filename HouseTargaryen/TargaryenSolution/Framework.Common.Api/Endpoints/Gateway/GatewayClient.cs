namespace Framework.Common.Api.Endpoints.Gateway
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net;

    using Framework.Common.Api.Endpoints.Gateway.DataModel.Docket;
    using Framework.Common.Api.Endpoints.Uds.DataModel;
    using Framework.Common.Api.Utilities;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;

    using RestSharp;

    /// <summary>
    /// The gateway client.
    /// </summary>
    public class GatewayClient : BaseCobaltServiceClient
    {
        /// <summary>
        /// Pointer on the udsSessionInfo.
        /// </summary>
        private UdsSessionInfo udsSessionInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayClient"/> class.
        /// </summary>
        /// <param name="environment">
        /// The environment.
        /// </param>
        /// <param name="cobaltCookies">
        /// The cobalt cookies.
        /// </param>
        /// <param name="securityHeaders">
        /// The security headers.
        /// </param>
        /// <param name="udsSessionInfo">
        /// The uds session info.
        /// </param>
        public GatewayClient(
            EnvironmentInfo environment,
            CookieContainer cobaltCookies,
            NameValueCollection securityHeaders,
            UdsSessionInfo udsSessionInfo)
            : base(environment, CobaltModuleId.Gateway, CobaltProductId.None, cobaltCookies, securityHeaders)
        {
            this.udsSessionInfo = udsSessionInfo;
            this.SecurityHeaders.Set("x-cobalt-security-userguid", udsSessionInfo.PrismGuid);
            this.SecurityHeaders.Set("x-cobalt-security-sessionid", udsSessionInfo.SessionId);
        }

        /// <summary>
        /// Retrieve all users docket requests
        /// </summary>
        /// <returns><see cref="DocketRequestsResponseModel"/></returns>
        public DocketRequestsResponseModel GetAllUsersDocketRequests()
        {
            var requestArguments = new RequestArguments
                                                    {
                                                        Method = Method.GET,
                                                        Resource = $"/Gateway/v1/docket/imagebinary/{this.udsSessionInfo.PrismGuid}"
                                                    };
            return this.RestClient.Execute<DocketRequestsResponseModel>(this.RequestBuilder.BuildRequest(requestArguments)).Data;
        }

        /// <summary>
        /// Delete requests by requestId
        /// </summary>
        /// <param name="requestIdList">list of request id to delete</param>
        /// <returns><see cref="DeleteRequestsResponseModel"/></returns>
        public DeleteRequestsResponseModel DeleteDocketBatchDownloadRequests(List<string> requestIdList)
        {
            DeleteRequestsRequestModel deleteRequestModel = new DeleteRequestsRequestModel { RequestIds = requestIdList };
            var requestArguments = new RequestArguments
                                                    {
                                                        Method = Method.POST,
                                                        Resource = "/Gateway/v1/docket/imagebinary/delete",
                                                        DataFormat = DataFormat.Json,
                                                        Data = deleteRequestModel
                                                    };

            return this.RestClient.Execute<DeleteRequestsResponseModel>(this.RequestBuilder.BuildRequest(requestArguments)).Data;
        }

        /// <summary>
        /// Retrieve all users docket updates
        /// </summary>
        /// <returns><see cref="DocketUpdatesResponseModel"/></returns>
        public DocketUpdatesResponseModel GetAllUsersDocketUpdates()
        {
            var requestArguments = new RequestArguments
            {
                Method = Method.GET,
                Resource = $"/Gateway/v1/docket/document/{this.udsSessionInfo.PrismGuid}"
            };
            return this.RestClient.Execute<DocketUpdatesResponseModel>(this.RequestBuilder.BuildRequest(requestArguments)).Data;
        }

        /// <summary>
        /// Delete updates by requestId
        /// </summary>
        /// <param name="requestIdList">list of request id to delete</param>
        /// <returns><see cref="DeleteRequestsResponseModel"/></returns>
        public DeleteRequestsResponseModel DeleteDocketBatchDownloadUpdates(List<string> requestIdList)
        {
            DeleteRequestsRequestModel deleteRequestModel = new DeleteRequestsRequestModel { RequestIds = requestIdList };
            var requestArguments = new RequestArguments
            {
                Method = Method.POST,
                Resource = "/Gateway/v1/docket/document/delete",
                DataFormat = DataFormat.Json,
                Data = deleteRequestModel
            };

            return this.RestClient.Execute<DeleteRequestsResponseModel>(this.RequestBuilder.BuildRequest(requestArguments)).Data;
        }
    }
}
