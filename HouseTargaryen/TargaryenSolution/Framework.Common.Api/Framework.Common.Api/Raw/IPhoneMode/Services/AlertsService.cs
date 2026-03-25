namespace Framework.Common.Api.Raw.IPhoneMode.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    using Framework.Common.Api.Raw.IPhoneMode.Models.Requests;
    using Framework.Common.Api.Raw.IPhoneMode.Models.Responses;
    using Framework.Common.Api.Raw.IPhoneMode.Utilities;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Net;
    using Framework.Core.Utils;
    using Framework.Core.Utils.Configuration;

    /// <summary>
    /// Alerts Service
    /// </summary>
    public class AlertsService
    {
        /// <summary>
        /// Request Headers
        /// </summary>
        private readonly WebHeaderCollection headers;

        /// <summary>
        /// URL that these services target
        /// </summary>
        private readonly string hostUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertsService"/> class. 
        /// Constructs the object
        /// </summary>
        public AlertsService(EnvironmentInfo environment)
        {
            //string isDcExit = Environment.GetEnvironmentVariable(EnvironmentConstants.IsDcExit);
            string isDcExit = null;
            if (environment.Id.Equals(EnvironmentId.DemoAWS) | environment.Id.Equals(EnvironmentId.QedAWS) | environment.Id.Equals(EnvironmentId.QedAWS2))
            {
                isDcExit = "Yes";
            }
            else
            {
                isDcExit = "No";
            }
           
            this.hostUrl = environment.Id.GetUrlForWestlawNext();
           
            if (isDcExit != null && isDcExit.Equals("Yes"))
            {
                string environmentName = environment.Name.ToLower();
                string hostId = environmentName.StartsWith("demo") || environmentName.StartsWith("ci") ? "30962" : "92615";
                this.headers = new WebHeaderCollection
                               {
                                   {
                                       "x-cobalt-host",
                                       $"alert-int-next-{environmentName}-westlaw-com.{hostId}.aws-int.thomsonreuters.com"
                                   }
                               };
            }
            else
            {

                this.headers = new WebHeaderCollection
                               {
                                   {
                                       "x-cobalt-host",
                                       $"alert.int.next.{environment.Name.ToLower()}.westlaw.com"
                                   }
                               };
            }
        }

        /// <summary>
        /// Creates the practice area alert.
        /// </summary>
        /// <param name="request"> The request.  </param>
        /// <param name="headersList"> The headers.  </param>
        /// <param name="cookies"> The cookies.  </param>
        /// <returns> The Endpoint Response. </returns>
        public EndpointResponse<CreatePracticeAreaAlertsResponse> CreatePracticeAreaAlerts(
            CreatePracticeAreaAlertsRequest request,
            WebHeaderCollection headersList = null,
            CookieCollection cookies = null)
        {
            string url = this.hostUrl + "V1/ios/Alerts/CreatePracticeAreaAlerts";
            if (headersList != null)
            {
                this.headers.Add(headersList);
            }

            string a = request.GetRequestBody();
            return HttpRequestUtil.PostRequest<CreatePracticeAreaAlertsResponse>(url, a, headersList, cookies);
        }

        /// <summary>
        /// Delete Alerts
        /// </summary>
        /// <param name="request">request</param>
        /// <param name="headersList">headers List</param>
        /// <param name="cookies">cookies</param>
        /// <returns>Http response</returns>
        public EndpointResponse<DeleteAlertsResponse> DeleteAlerts(
            DeleteAlertsRequest request,
            WebHeaderCollection headersList = null,
            CookieCollection cookies = null)
        {
            string url = this.hostUrl + "Alert/v2/alerts/deleteAlerts";
            if (headersList != null)
            {
                this.headers.Add(headersList);
            }

            return HttpRequestUtil.PostRequest<DeleteAlertsResponse>(
                url,
                request.GetRequestBody(),
                this.headers,
                cookies);
        }

        /// <summary>
        /// Delete All Alerts
        /// </summary>
        /// <param name="cookies"> The cookies. </param>
        public void DeleteAllAlerts(CookieCollection cookies = null)
        {
            List<Alert> allAlerts = this.GetAllAlerts(cookies: cookies).ResponseBody.Alerts;
            var alerts = new List<AlertsToDelete>();

            // Add Alerts to AlertsToDelete Model for DeleteRequest Body
            if (allAlerts.Count > 0)
            {
                foreach (Alert a in allAlerts)
                {
                    var d = new AlertsToDelete { AlertGuids = new List<string>(), AlertType = a.AlertType };
                    d.AlertGuids.Add(a.Guid);
                    alerts.Add(d);
                }

                // Delete Alerts
                this.DeleteAlerts(new DeleteAlertsRequest(alerts), cookies: cookies);
            }
        }

        /// <summary>
        /// Get All Alerts by Facet
        /// </summary>
        /// <param name="headersList"> headers List </param>
        /// <param name="cookies"> cookies </param>
        /// <returns> Http response </returns>
        public EndpointResponse<AlertsByFacetsResponse> GetAllAlerts(
            WebHeaderCollection headersList = null,
            CookieCollection cookies = null)
        {
            string url = this.hostUrl + "Alert/v2/alerts/retrieveAlertsByFacets";
            const string Body =
                "{\"start\":1,\"end\":100,\"facetCriteria\":[],\"sort\":{\"direction\":\"ASCENDING\",\"fieldName\":\"alertType\"},\"alertType\":\"All\"}";

            if (headersList != null)
            {
                this.headers.Add(headersList);
            }

            return HttpRequestUtil.PostRequest<AlertsByFacetsResponse>(url, Body, this.headers, cookies);
        }

        /// <summary>
        /// Alerts by Group
        /// </summary>
        /// <param name="request">request</param>
        /// <param name="headersList">headers List</param>
        /// <param name="cookies">cookies</param>
        /// <param name="acceptableResponses">acceptable Responses</param>
        /// <returns>Http response</returns>
        public EndpointResponse<GroupedAlertsResponse> GroupedAlerts(
            GroupedAlertsRequest request,
            WebHeaderCollection headersList = null,
            CookieCollection cookies = null,
            List<HttpStatusCode> acceptableResponses = null)
        {
            string url = this.hostUrl + "Alert/v1/mobile/groupedAlerts";
            if (headersList != null)
            {
                this.headers.Add(headersList);
            }

            return HttpRequestUtil.PostRequest<GroupedAlertsResponse>(
                url,
                request.GetRequestBody(),
                this.headers,
                cookies,
                acceptableResponses: acceptableResponses);
        }

        /// <summary>
        /// Users Alert Notifications
        /// </summary>
        /// <param name="request">request</param>
        /// <param name="headersList">headers List</param>
        /// <param name="cookies">cookies</param>
        /// <returns>Http response</returns>
        public EndpointResponse<NotifcationsResponse> Notifications(
            NotificationsRequest request,
            WebHeaderCollection headersList = null,
            CookieCollection cookies = null)
        {
            string url = string.Format(
                this.hostUrl + "Alert/v5/notifications?&offset={0}&limit={1}",
                request.Offset,
                request.Limit);
            if (headersList != null)
            {
                this.headers.Add(headersList);
            }

            // The response has a dynamic root object name. Simply replace with a static name for ease of de-serialization
            EndpointResponse<string> res = HttpRequestUtil.PostRequest<string>(
                url,
                request.GetRequestBody(),
                this.headers,
                cookies);
            string x = res.ResponseBody.Remove(2, 33).Insert(2, "UserNotifications");

            return new EndpointResponse<NotifcationsResponse>(
                ObjectSerializer.DeserializeJsonToObject<NotifcationsResponse>(x),
                res.Headers);
        }

        /// <summary>
        /// Creates the company track alert.
        /// </summary>
        /// <param name="alertDefaults"> The alert defaults. </param>
        /// <param name="headersList"> headers List </param>
        /// <param name="cookies"> cookies </param>
        /// <returns> The Endpoint Response. </returns>
        public EndpointResponse<string> TrackCompanyAlerts(
            TrackCompanyAlertsRequest alertDefaults,
            WebHeaderCollection headersList = null,
            CookieCollection cookies = null)
        {
            string retrieveUrl = this.hostUrl + "Alert/v2/mobile/retrieveDefaultTrackCompanyAlerts";
            if (headersList != null)
            {
                this.headers.Add(headersList);
            }

            EndpointResponse<string> retrieveRequest = HttpRequestUtil.PostRequest<string>(
                retrieveUrl,
                alertDefaults.GetRequestBody(),
                this.headers,
                cookies);
            string createUrl = this.hostUrl + "Alert/v2/mobile/trackCompanyAlerts";
            return HttpRequestUtil.PostRequest<string>(createUrl, retrieveRequest.ResponseBody, this.headers, cookies);
        }

        /// <summary>
        /// Update Alerts Push Notifications Flags
        /// </summary>
        /// <param name="request">request</param>
        /// <param name="headersList">headers List</param>
        /// <param name="cookies">cookies</param>
        /// <returns>Http response</returns>
        public EndpointResponse<UpdatePushNotificationFlagsResponse> UpdatePushNotificationsFlags(
            UpdatePushNotificationFlagsRequest request,
            WebHeaderCollection headersList = null,
            CookieCollection cookies = null)
        {
            string url = this.hostUrl + "Alert/v1/alerts/updatePushNotificationFlags";
            if (headersList != null)
            {
                this.headers.Add(headersList);
            }

            return HttpRequestUtil.PostRequest<UpdatePushNotificationFlagsResponse>(
                url,
                request.GetRequestBody(),
                this.headers,
                cookies);
        }
    }
}