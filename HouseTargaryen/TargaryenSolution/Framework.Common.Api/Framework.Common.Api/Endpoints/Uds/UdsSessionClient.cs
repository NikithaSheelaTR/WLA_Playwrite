namespace Framework.Common.Api.Endpoints.Uds
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using System.Threading;

    using Framework.Common.Api.Endpoints.Uds.DataModel;
    using Framework.Common.Api.Enums.Uds;
    using Framework.Common.Api.Utilities;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Utils;

    using RestSharp;

    using SessionBindings = DataModel.SessionBindings;

    /// <summary>
    /// Provide UDS authentication service
    /// </summary>
    public sealed class UdsSessionClient : BaseCobaltServiceClient
    {
        private const string ParameterName = "application/json; charset=utf-8";
        private readonly Dictionary<string, string> headers = new Dictionary<string, string> { { "Content-Type", "application/json" } };

        /// <summary>
        /// Initializes a new instance of the <see cref="UdsSessionClient"/> class.
        /// </summary>
        /// <param name="environment"> The environment. </param>
        /// <param name="cobaltCookies"> The cobalt cookies. </param>
        /// <param name="productId"> The product id. </param>
        /// <param name="securityHeaders"> The security headers. </param>
        public UdsSessionClient(
            EnvironmentInfo environment,
            CookieContainer cobaltCookies,
            CobaltProductId productId,
            NameValueCollection securityHeaders)
            : this(environment, productId, cobaltCookies, securityHeaders)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UdsSessionClient"/> class.
        /// </summary>
        /// <param name="environment"> The environment. </param>
        /// <param name="productId"> The product id. </param>
        /// <param name="cobaltCookies"> The cobalt cookies. </param>
        /// <param name="securityHeaders"> The security headers. </param>
        public UdsSessionClient(
            EnvironmentInfo environment,
            CobaltProductId productId,
            CookieContainer cobaltCookies,
            NameValueCollection securityHeaders)
            : base(environment, CobaltModuleId.Uds, productId, cobaltCookies, securityHeaders)
        {
            this.BaseUrl =
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    TestConfigurationRepository.DefaultInstance.FindModule(CobaltModuleId.Uds),
                    TestConfigurationRepository.DefaultInstance.FindProduct(this.ProductId),
                    environment).Uri;
            this.CobaltCookies.Add(new Cookie("site", environment.Site.ToLower(), "/", this.RestClient.BaseUrl.Host));
        }

	    /// <summary>
	    /// Start UDS authentication 
	    /// </summary>
	    /// <param name="sessionToken">Session ID value</param>
	    /// <param name="tries"></param>
	    /// <returns>Dictionary value</returns>
	    public UdsSessionInfo GetAuthSessionInfo(string sessionToken, int tries = 1)
        {
            string url = $"/UDS/v8/authsession/{sessionToken}";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url,
                Headers = this.headers
            });
            this.LastResponse = this.RestClient.ExecuteUntil(request, new []{ HttpStatusCode.OK}, tries);

	        UdsSessionInfo udsSessionInfo =
                ObjectSerializer.DeserializeObject<DataContractJsonSerializer, UdsSessionInfo>(
                    this.LastResponse.Content);

            return udsSessionInfo;
        }

        /// <summary>
        /// The get long token.
        /// </summary>
        /// <param name="userPrismGuid"> The user prism GUID. </param>
        /// <param name="product"> The product. </param>
        /// <param name="status"> The status. </param>
        /// <returns> The <see cref="string"/>. </returns>
        public string GetLongToken(string userPrismGuid, string product, string status = "online")
        {
            const int TimeoutBetweenRequests = 5000;
            string url =
                $"/UDS/v8/authsession/query?site={this.Environment.Site.ToUpper()}&status={status.ToUpper()}&userId={userPrismGuid}&product={product}";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url
            });

            this.LastResponse = this.RestClient.Execute(request);

            List<string> idValues =
                ObjectSerializer.DeserializeObject<DataContractJsonSerializer, List<string>>(this.LastResponse.Content);

            for (int i = 0; !idValues.Any() && i < 3; i++)
            {
                Thread.Sleep(TimeoutBetweenRequests);
                this.LastResponse = this.RestClient.Execute(request);
                idValues =
                    ObjectSerializer.DeserializeObject<DataContractJsonSerializer, List<string>>(
                        this.LastResponse.Content);
            }

           return idValues.FirstOrDefault() ?? string.Empty;
        }

        /// <summary>
        /// The set Session Bindings
        /// </summary>
        /// <param name="sessionId"> session Id </param>
        /// <param name="sessionBindings"> session Bindings </param>
        public void SetSessionBindings(string sessionId, SessionBindings sessionBindings)
        {
            string url = $"UDS/v4/sessionbindings/{sessionId}";
            string parameterValue = ObjectSerializer.SerializeJsonObject(sessionBindings);

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Parameters = new List<Parameter> { new Parameter { Name = ParameterName, Value = parameterValue, Type = ParameterType.RequestBody} }
            });

            this.LastResponse = this.RestClient.ExecuteUntil(request, new[] { HttpStatusCode.Created });

            HttpStatusCode statusCode = this.LastResponse.StatusCode;
            if (statusCode != HttpStatusCode.Created)
            {
                throw new Exception($"Unable to update session bindings. Status code:{statusCode.ToString()}");
            }
        }

        /// <summary>
        /// Gets the info about session bindings for a user (IAC, FAC, application configuration and routing table settings)
        /// </summary>
        /// <param name="sessionId">Session ID value</param>
        /// <returns> Session Bindings </returns>
        public SessionBindings GetSessionBindings(string sessionId)
        {
            string url = $"/UDS/v4/sessionbindings/{sessionId}";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url,
                Headers = this.headers,
            });

            this.LastResponse = this.RestClient.Execute(request);

            var results =
                new DataContractJsonSerializer(
                        typeof(SessionBindings),
                        new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true })
                    .DeserializeObject<SessionBindings>(this.LastResponse.Content);

            return results;
        }

        /// <summary>
        /// Kills given session
        /// </summary>
        /// <param name="session"> session </param>
        public void KillSession(UdsSessionInfo session)
        {
            session.Status = UdsSessionStatus.Killed.ToString();
            session.ExpiresReason = UdsSessionExpiresReason.Maintenance.GetEnumTextValue();
            this.UpdateSession(session);
        }

        /// <summary>
        /// The get Work product token
        /// </summary>
        /// <param name="sessionId"> session Id </param>
        /// <returns> WorkProductTokenResponse </returns>
        public WorkProductTokenResponse GetWorkProductToken(string sessionId)
        {
            string url = $"/UDS/v4/authentication/workproducttoken/DR/{sessionId}";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url,
                DataFormat = DataFormat.Json
            });

            this.LastResponse = this.RestClient.Execute(request);

            WorkProductTokenResponse response =
                ObjectSerializer.DeserializeObject<DataContractJsonSerializer, WorkProductTokenResponse>(
                    this.LastResponse.Content);

            return response;
        }

        /// <summary>
        /// The generate default UDS session
        /// TODO: Review naming
        /// </summary>
        /// <returns> UdsSessionInfo </returns>
        public UdsSessionInfo ResetSession(
            UdsSessionInfo session)
        {
            session.SessionId = System.Guid.NewGuid().ToString("N");
            return session;
        }

        /// <summary>
        /// The create UDS session
        /// </summary>
        /// <param name="session"> session </param>
        /// <returns> list of RestResponseCookie </returns>
        public IList<RestResponseCookie> StartSession(UdsSessionInfo session)
        {
            const string Url = "UDS/v8/authsession";
            string parameterValue = ObjectSerializer.SerializeJsonObject<UdsSessionInfo>(session);

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = Url,
                Parameters = new List<Parameter> { new Parameter { Name = ParameterName, Value = parameterValue, Type = ParameterType.RequestBody } },
                DataFormat = DataFormat.Json
            });

            this.LastResponse = this.RestClient.ExecuteUntil(request, new[] { HttpStatusCode.Created });

            HttpStatusCode statusCode = this.LastResponse.StatusCode;
            if (statusCode == HttpStatusCode.Created)
            {
                return this.LastResponse.Cookies;
            }

            throw new Exception($"Status code {statusCode} received on session create");
        }

        /// <summary>
        /// The Update session method. Updates session according to given session
        /// </summary>
        /// <param name="session"> UdsSessionInfo </param>
        private void UpdateSession(UdsSessionInfo session)
        {
            string url = $"UDS/v8/authsession/{session.LongToken}";
            string parameterValue = ObjectSerializer.SerializeJsonObject<UdsSessionInfo>(session);

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Parameters = new List<Parameter> { new Parameter { Name = ParameterName, Value = parameterValue, Type = ParameterType.RequestBody } }
            });

            this.LastResponse = this.RestClient.Execute(request);
        }
    }
}