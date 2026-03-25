namespace Framework.Common.Api.Endpoints.Website
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization.Json;

    using Framework.Common.Api.Endpoints.Uds.DataModel;
    using Framework.Common.Api.Utilities;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils;

    using HtmlAgilityPack;

    using RestSharp;

    /// <summary>
    /// The website client for work with the session
    /// </summary>
    public sealed partial class WebsiteClient
    {
        /// <summary>
        /// Set client Id for session.
        /// </summary>
        /// <param name="clientId">
        /// The client id.
        /// </param>
        public void SetClientId(string clientId)
        {
            string url = "/V1/Session/BeginResearch";
            var body = new { ClientMatter = new { ClientID = clientId } };
            var headers = new Dictionary<string, string>
                              {
                                  { "Content-Type", "application/json" }
                              };

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Data = body,
                Headers = headers,
                DataFormat = DataFormat.Json
            });

            this.LastResponse = this.RestClient.Execute(request);

            if (this.LastResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(
                    $"BeginResearch failed due to HTTP{(int)this.LastResponse.StatusCode} - {this.LastResponse.StatusCode}");
            }
        }

        /// <summary>
        /// The sign off session.
        /// </summary>
        public void SignOffCurrentSession()
        {
            string url = "/V1/Session";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.DELETE,
                Resource = url
            });

            this.LastResponse = this.RestClient.Execute(request);

            if (this.LastResponse.StatusCode != 0)
            {
                SessionEndedStatus sessionEndedStatus =
                    ObjectSerializer.DeserializeObject<DataContractJsonSerializer, SessionEndedStatus>(
                        this.LastResponse.Content);

                if (!sessionEndedStatus.IsSessionEnded)
                {
                    throw new Exception("Sing off session failed due to: " + sessionEndedStatus.Error.OnePass);
                }
            }
            else
            {
                throw new Exception(
                    $"Sing off session failed due to HTTP{(int)this.LastResponse.StatusCode} - {this.LastResponse.StatusCode}");
            }
        }

        /// <summary>
        /// Execute Post API To Website. Start Session 
        /// </summary>
        /// <param name="username"> The username. </param>
        /// <param name="password"> Password </param>
        /// <returns> list  Rest Response Cookie  </returns>
        public IList<RestResponseCookie> StartSession(string username, string password)
        {
            string url = "/V1/Session";
            var headers = new Dictionary<string, string> { { "Content-Type", "application/json; charset=utf-8" } };

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Headers = headers,
                DataFormat = DataFormat.Json
            });

            if (this.Environment.IsLower)
            {
                request.AddBody(new { UserId = username, Password = password, SourceType = "Web" });
            }
            else
            {
                request.AddBody(new { UserId = username, Password = password });
            }

            this.LastResponse = this.RestClient.ExecuteUntil(request);

            if (this.LastResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(
                    $"Did not get all required cookies back from the session request for user: '{username}', /V1/Session enpoint returned {this.LastResponse.StatusCode}");
            }

            return this.LastResponse.Cookies;
        }

        /// <summary>
        /// Convert grabbed form data into dictionary id:value
        /// </summary>
        /// <param name="userDefinedSettings"> The user Defined Settings. </param>
        /// <returns> <see cref="Dictionary{TKey,TValue}"/> </returns>
        internal Dictionary<string, string> BuildRoutingFormData(Dictionary<string, string> userDefinedSettings)
        {
            var routingFormData = new Dictionary<string, string>();
            var formData = new HtmlDocument();
            formData.LoadHtml(this.GetRoutingFormData().Content);
            HtmlNodeCollection routInfo = formData.DocumentNode.SelectNodes("//*[@name and(name()='select' or name()='input' or name()='textarea')]");

            foreach (HtmlNode node in routInfo)
            {
                if (node.Name == "input" || node.Name == "textarea")
                {
                    routingFormData.Add(node.GetAttributeValue("name", "id"), node.GetAttributeValue("value", string.Empty));
                }

                if (node.Name == "select")
                {
                    routingFormData.Add(node.GetAttributeValue("name", "id"), node.Elements("option").FirstOrDefault(e => e.Attributes.Contains("selected"))?.GetAttributeValue("value", string.Empty) ?? string.Empty);
                }
            }

            foreach (KeyValuePair<string, string> configuration in userDefinedSettings)
            {
                if (!string.IsNullOrEmpty(configuration.Value) && routingFormData.ContainsKey(configuration.Key))
                {
                    if (!string.IsNullOrEmpty(userDefinedSettings[configuration.Key]))
                    {
                        routingFormData[configuration.Key] = configuration.Value;
                    }
                }
            }

            return routingFormData;
        }

        /// <summary>
        /// Post routing settings
        /// </summary>
        /// <param name="routingSettings"> The routing Settings. </param>
        /// <returns> IRestResponse </returns>
        internal IRestResponse PostRoutingSettings(Dictionary<string, string> routingSettings)
        {
            string referer = this.BaseUrl + "routin";
            string url = "Routing/SaveRoutesWithKey.html?noRedirect=False&isKM=False";
            var headers = new Dictionary<string, string>
                              {
                                  { "Upgrade-Insecure-Requests", "1" },
                                  { "Referer", referer },
                                  { "Origin", this.BaseUrl },
                                  { "Content-Type", "application/x-www-form-urlencoded" }
                              };

            var parameters = new List<Parameter>();
            foreach (KeyValuePair<string, string> keyValuePair in routingSettings)
            {
                parameters.Add(new Parameter { Name = keyValuePair.Key, Value = keyValuePair.Value, Type = ParameterType.GetOrPost });
            }

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Headers = headers,
                Parameters = parameters
            });

            return this.RestClient.Execute(request);
        }

        /// <summary>
        /// Grabs Routing form data
        /// </summary>
        /// <returns><see cref="IRestResponse"/></returns>
        internal IRestResponse GetRoutingFormData()
        {
            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = "Routing"
            });

            return this.RestClient.Execute(request);
        }

        /// <summary>
        /// Submits session data
        /// </summary>
        /// <param name="userInfo"> The user Info. </param>
        /// <returns> <see cref="IRestResponse"/> </returns>
        internal IRestResponse BeginResearch(WlnUserInfo userInfo)
        {
            string resource = "/V1/Session/BeginResearch";
            var headers = new Dictionary<string, string>
                              {
                                  { "Content-Type", "application/x-www-form-urlencoded; charset=UTF-8" }
                              };
            var body = new
            {
                ClientMatter =
                               new
                               {
                                   ClientId = userInfo.ClientId,
                                   IsClientNonChargeable = userInfo.BillingType ?? "false"
                               },
                IsLinkIn = "true",
                IsTargetNonTrackableZone = "false"
            };

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = resource,
                Data = body,
                Headers = headers,
                DataFormat = DataFormat.Json
            });

            return this.RestClient.Execute(request);
        }
    }
}
