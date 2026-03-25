namespace Framework.Common.UI.Products.Shared.Managers.AjaxServiceManagers.AjaxServiceClients
{
    using System.Collections.Generic;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    internal class DocumentServiceClient
    {
        /// <summary>
        /// Retrieve related info categories for a document
        /// </summary>
        /// <param name="documentGuid">Document guid</param>
        /// <returns>Response Object</returns>
        internal static object GetRelatedInfoCategories(string documentGuid)
        {
            string endpointUrl = $"/RelatedInfo/v4/categories/{documentGuid}";
            string preparedRequest = "return (function(){return($.ajax({async: false,type: \"GET\",url: \"" + endpointUrl + "\",beforeSend: function(request){request.setRequestHeader(\"x-cobalt-host\",window['Server/Routing']['RelatedInfo']);request.setRequestHeader(\"Accept\",\"application/json\");},contentType: \"application/json\"}).done(function(a){return a;})).responseText;})();";
            return DriverExtensions.ExecuteScript(preparedRequest);
        }

        /// <summary>
        /// Returns KeyCite Info tab counts
        /// </summary>
        /// <param name="documentGuid"></param>
        /// <param name="categoriesList"></param>
        /// <returns>KeyCite Info tab counts</returns>
        internal static object GetKeyCiteTabCountInfo(string documentGuid, List<string> categoriesList)
        {
            string endpointUrl = $"/RelatedInfo/v4/counts/{documentGuid}";
            string query = @"{categories: '"+string.Join(",",categoriesList) + "', execType: 'async', entityId: 'undefined'}";
            string preparedRequest = "return (function(){return($.ajax({async: false,type: \"GET\",url: \"" + endpointUrl + "\",beforeSend: function(request){request.setRequestHeader(\"x-cobalt-host\",window['Server/Routing']['RelatedInfo']);request.setRequestHeader(\"Accept\",\"application/json\");},contentType: \"application/json\", data:"+ query+"}).done(function(a){return a;})).responseText;})();";
            return DriverExtensions.ExecuteScript(preparedRequest);
        }
    }
}
