namespace Framework.Common.UI.Products.Shared.Managers.AjaxServiceManagers
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Managers.AjaxServiceManagers.AjaxServiceClients;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Contains Ajax service methods for Document module.
    /// </summary>
    public static class AjaxDocumentManager
    {
        /// <summary>
        /// Retrieves Related Info for given document
        /// </summary>
        /// <param name="documentGuid">Document guid</param>
        /// <returns>Related info JToken</returns>
        public static T GetRelatedInfoCategories<T>(string documentGuid)
        {
            return JsonConvert.DeserializeObject<T>(
                (string)DocumentServiceClient.GetRelatedInfoCategories(documentGuid));
        }

        /// <summary>
        /// Returns KeyCite info
        /// </summary>
        /// <param name="docGuid"></param>
        /// <param name="categoriesList"></param>
        /// <returns>KeyCite info</returns>
        public static T GetKeyCiteTabCountInfo<T>(string docGuid, List<string> categoriesList)
        {
            return JsonConvert.DeserializeObject<T>(
                (string)DocumentServiceClient.GetKeyCiteTabCountInfo(docGuid, categoriesList));
        }
    }
}