namespace Framework.Common.UI.Products.Shared.Managers.AjaxServiceManagers
{
    using Framework.Common.UI.Products.Shared.Managers.AjaxServiceManagers.AjaxServiceClients;

    using Newtonsoft.Json;

    /// <summary>
    /// Alert service manager
    /// </summary>
    public static class AjaxAlertManager
    {

        /// <summary>
        /// Represents details of last used operation
        /// </summary>
        public static object LastOperationDetails { get; set; }

        /// <summary>
        /// Creates WestClip alert by given alertBody
        /// </summary>
        /// <param name="alertBody">Json alert body </param>
        public static void CreateWestClipAlert(string alertBody)
        {
            LastOperationDetails = AlertServiceClient.CreateWestClipAlert(alertBody);
        }

        /// <summary>
        /// Creates KeyCite alert by given alertBody
        /// </summary>
        /// <param name="alertBody"></param>
        public static void CreateKeyCiteAlert(string alertBody)
        {
            LastOperationDetails = AlertServiceClient.CreateKeyCiteAlert(alertBody);
        }

        /// <summary>
        /// Creates Docket alert by given alertBody
        /// </summary>
        /// <param name="alertBody"></param>
        public static void CreateDocketAlert(string alertBody)
        {
           LastOperationDetails = AlertServiceClient.CreateDocketAlert(alertBody);
        }

        /// <summary>
        /// Creates DocketTrack alert by given alertBody
        /// </summary>
        /// <param name="alertBody"></param>
        public static void CreateDocketTrackAlert(string alertBody)
        {
            LastOperationDetails = AlertServiceClient.CreateDocketTrackAlert(alertBody);
        }

        /// <summary>
        /// Creates DocketTrack alert by given alertBody
        /// </summary>
        /// <param name="alertBody"></param>
        public static void CreatePublicationAlert(string alertBody)
        {
            LastOperationDetails = AlertServiceClient.CreatePublicationAlert(alertBody);
        }

        /// <summary>
        /// Retrieve alerts
        /// </summary>
        /// <returns> The Json object</returns>
        public static T RetrieveAlerts<T>()
        {
            return JsonConvert.DeserializeObject<T>((string)AlertServiceClient.RetrieveAlerts());
        }

        /// <summary>
        /// Retrieve serialized alert by name
        /// </summary>
        /// <param name="alertName"></param>
        /// <returns></returns>
        public static T GetAlertInfo<T>(string alertName)
        {
            return JsonConvert.DeserializeObject<T>((string)AlertServiceClient.GetAlertInfo(alertName));
        }

        /// <summary>
        /// Run Alert by name (first)
        /// </summary>
        /// <param name="alertName">Alert Name</param>
        public static void RunAlertNow(string alertName)
        {
            LastOperationDetails = AlertServiceClient.RunAlertNow(alertName);
        }

        /// <summary>
        /// Delete first alert by name
        /// </summary>
        /// <param name="alertName">Alert Name</param>
        public static void DeleteAlertByName(string alertName)
        {
            LastOperationDetails = AlertServiceClient.DeleteAlertByName(alertName);
        }
    }
}
