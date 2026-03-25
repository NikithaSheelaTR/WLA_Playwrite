namespace Framework.Common.UI.Products.Shared.Managers.AjaxServiceManagers.AjaxServiceClients
{
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Alert Service Manager
    /// </summary>
    internal static class AlertServiceClient
    {
        /// <summary>
        /// Creates WestClip alert by given alertBody
        /// </summary>
        /// <param name="alertBody">Json alert body </param>
        /// <returns>Response object</returns>
        internal static object CreateWestClipAlert(string alertBody)
        {
            return AlertServiceClient.PostJsonByUrl(alertBody, "/Alert/v3/alert/WestClip");
        }

        /// <summary>
        /// Creates KeyCite alert by given alertBody
        /// </summary>
        /// <param name="alertBody"></param>
        /// <returns>Response object</returns>
        internal static object CreateKeyCiteAlert(string alertBody)
        {
            return AlertServiceClient.PostJsonByUrl(alertBody, "/Alert/v3/alert/KeyCite");
        }

        /// <summary>
        /// Creates Docket alert by given alertBody
        /// </summary>
        /// <param name="alertBody"></param>
        /// <returns>Response object</returns>
        internal static object CreateDocketAlert(string alertBody)
        {
            return AlertServiceClient.PostJsonByUrl(alertBody, "/Alert/v3/alert/DocketAlert");
        }

        /// <summary>
        /// Creates DocketTrack alert by given alertBody
        /// </summary>
        /// <param name="alertBody"></param>
        /// <returns>Response object</returns>
        internal static object CreateDocketTrackAlert(string alertBody)
        {
            return AlertServiceClient.PostJsonByUrl(alertBody, "/Alert/v3/alert/DocketTrack");
        }

        /// <summary>
        /// Creates DocketTrack alert by given alertBody
        /// </summary>
        /// <param name="alertBody"></param>
        /// <returns>Response object</returns>
        internal static object CreatePublicationAlert(string alertBody)
        {
            return AlertServiceClient.PostJsonByUrl(alertBody, "/Alert/v3/alert/internalationAlert");
        }

        /// <summary>
        /// Retrieve alerts
        /// </summary>
        /// <returns>Response object</returns>
        internal static object RetrieveAlerts()
        {
            string endpointUrl = "/Alert/v2/alerts/retrieveAlertsByFacets";
            string body = "{\"start\":1,\"end\":100,\"facetCriteria\":[],\"sort\":{\"direction\":\"ASCENDING\",\"fieldName\":\"name\"},\"alertType\":\"All\"}";
            return AlertServiceClient.PostJsonByUrl(body, endpointUrl);
        }

        /// <summary>
        /// Retrieves Alert info
        /// </summary>
        /// <param name="alertName">Alert Name</param>
        /// <returns></returns>
        internal static object GetAlertInfo(string alertName)
        {
            var alertList = JsonConvert.DeserializeObject<JToken>((string)AlertServiceClient.RetrieveAlerts());
            var alert = alertList["alerts"].FirstOrDefault(x => Extensions.Value<string>(x["name"]) == alertName);
            string alertType = alert?.Value<string>("alertType");
            string alertGuid = alert?.Value<string>("guid");
            string endpointAddress = $"/Alert/v3/alert/{alertType ?? ""}/{alertGuid ?? ""}";
            string preparedRequest = "return (function(){return($.ajax({async: false,type: \"GET\",url: \"" + endpointAddress + "\",beforeSend: function(request){request.setRequestHeader(\"x-cobalt-host\",window['Server/Routing']['Alert']);request.setRequestHeader(\"Accept\",\"application/json\");},contentType: \"application/json\"}).done(function(a){return a;})).responseText;})();";
            return DriverExtensions.ExecuteScript(preparedRequest);
        }

        /// <summary>
        /// Run Alert by name
        /// </summary>
        /// <param name="alertName">Alert Name</param>
        /// <returns>Response object</returns>
        internal static object RunAlertNow(string alertName)
        {
            JToken alertList = JsonConvert.DeserializeObject<JToken>((string)AlertServiceClient.RetrieveAlerts());
            JToken alertToRun = alertList?["alerts"].FirstOrDefault(x => x["name"].Value<string>() == alertName);
            string url = "/Alert/v2/alerts/activateAlerts?runNow=true";
            var jsonBody = new JArray(new JObject(new JProperty("alertType", alertToRun?["alertType"]), new JProperty("alertGuids", new JArray(alertToRun?["guid"]))));
            return AlertServiceClient.PostJsonByUrl(JsonConvert.SerializeObject(jsonBody), url);
        }

        /// <summary>
        /// Delete first alert by name
        /// </summary>
        /// <param name="name">Alert Name</param>
        /// <returns>Response object</returns>
        internal static object DeleteAlertByName(string name)
        {
            var alertList = JsonConvert.DeserializeObject<JToken>((string)AlertServiceClient.RetrieveAlerts());
            var alertToDelete = alertList["alerts"].FirstOrDefault(x => x["name"].Value<string>() == name);
            string url = "/Alert/v2/alerts/deleteAlerts";
            var jsonBody = new JArray(new JObject(new JProperty("alertType", alertToDelete?["alertType"]), new JProperty("alertGuids", new JArray(alertToDelete?["guid"]))));
            return AlertServiceClient.PostJsonByUrl(JsonConvert.SerializeObject(jsonBody), url);
        }

        /// <summary>
        /// Create custom alert
        /// </summary>
        /// <param name="body"></param>
        /// <param name="endpointUrl"></param>
        /// <returns>Response object</returns>
        private static object PostJsonByUrl(string body, string endpointUrl)
        {
            string preparedRequest = "return (function(){return($.ajax({async: false,type: \"POST\",url: \"" + endpointUrl + "\",beforeSend: function(request){request.setRequestHeader(\"x-cobalt-host\",window['Server/Routing']['Alert']);request.setRequestHeader(\"Accept\",\"application/json\");},contentType: \"application/json\",dataType: \"json\",data: JSON.stringify(" + body + ")}).done(function(a){return a;})).responseText;})();";
            return DriverExtensions.ExecuteScript(preparedRequest);
        }
    }
}
