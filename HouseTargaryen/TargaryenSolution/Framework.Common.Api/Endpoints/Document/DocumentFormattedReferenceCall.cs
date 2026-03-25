namespace Framework.Common.Api.Endpoints.Document
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization.Json;

    using Framework.Common.Api.Endpoints.Document.DataModel.Constants;
    using Framework.Common.Api.Endpoints.Document.DataModel.DocumentFormattedReference;
    using Framework.Common.Api.Endpoints.Uds.DataModel;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Utils;

    /// <summary>
    /// The document formatted reference utilities.
    /// </summary>
    public class DocumentFormattedReferenceCall
    {
        /// <summary>
        /// The orchestrate batch document formatted reference call.
        /// </summary>
        /// <param name="guids">
        /// The guids.
        /// </param>
        /// <param name="citeAdvisorRulesSet">
        /// The cite advisor rules set.
        /// </param>
        /// <param name="citationStyle">
        /// The citation style.
        /// </param>
        /// <param name="citeAdvisorParallelCites">
        /// The cite advisor parallel cites.
        /// </param>
        /// <param name="citeAdvisorIncludeHistory">
        /// The cite advisor include history.
        /// </param>
        /// <param name="citeAdvisorItalicizeTile">
        /// The cite advisor italicize tile.
        /// </param>
        /// <param name="citeAdvisorUnderlineTitle">
        /// The cite advisor underline title.
        /// </param>
        /// <param name="environmentInfo">
        /// The environment info.
        /// </param>
        /// <param name="cobaltProductId">
        /// The cobalt product id.
        /// </param>
        /// <param name="cookieContainer">
        /// The cookie container.
        /// </param>
        /// <param name="securityHeaders">
        /// The security headers.
        /// </param>
        /// <returns>
        /// The <see cref="DocFormattedReference"/>.
        /// </returns>
        public DocFormattedReference[] OrchestrateBatchDocumentFormattedReferenceCall(
            List<string> guids,
            string citeAdvisorRulesSet,
            string citationStyle,
            string citeAdvisorParallelCites,
            bool citeAdvisorIncludeHistory,
            bool citeAdvisorItalicizeTile,
            bool citeAdvisorUnderlineTitle,
            EnvironmentInfo environmentInfo,
            CobaltProductId cobaltProductId,
            CookieContainer cookieContainer,
            NameValueCollection securityHeaders)
        {
            string postBody = this.CreateBatchFormattedRequestBody(
                guids,
                citeAdvisorRulesSet,
                citationStyle,
                citeAdvisorParallelCites,
                citeAdvisorIncludeHistory,
                citeAdvisorItalicizeTile,
                citeAdvisorUnderlineTitle);

            var documentClient = new DocumentClient(environmentInfo, cobaltProductId, cookieContainer, securityHeaders);

            string responceContent =
                documentClient.PostResponseBody(DocumentConstants.BatchDocumentFormattedReference, postBody)
                              .ResposeContent;

            DocFormattedReference[] deliveryoUriPathInfo =
                ObjectSerializer.DeserializeObject<DataContractJsonSerializer, DocFormattedReference[]>(responceContent);

            return deliveryoUriPathInfo;
        }

        /// <summary>
        /// The orchestrate document formatted reference call.
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        /// <param name="citeAdvisorRulesSet">
        /// The cite advisor rules set.
        /// </param>
        /// <param name="citationStyle">
        /// The citation style.
        /// </param>
        /// <param name="citeAdvisorParallelCites">
        /// The cite advisor parallel cites.
        /// </param>
        /// <param name="citeAdvisorIncludeHistory">
        /// The cite advisor include history.
        /// </param>
        /// <param name="citeAdvisorItalicizeTile">
        /// The cite advisor italicize tile.
        /// </param>
        /// <param name="citeAdvisorUnderlineTitle">
        /// The cite advisor underline title.
        /// </param>
        /// <param name="selectedText">
        /// The selected text.
        /// </param>
        /// <param name="selectedHtml">
        /// The selected html.
        /// </param>
        /// <param name="environmentInfo">
        /// The environment info.
        /// </param>
        /// <param name="cobaltProductId">
        /// The cobalt product id.
        /// </param>
        /// <param name="cookieContainer">
        /// The cookie container.
        /// </param>
        /// <param name="securityHeaders">
        /// The security headers.
        /// </param>
        /// <returns>
        /// The <see cref="FormattedReference"/>.
        /// </returns>
        public FormattedReference OrchestrateDocumentFormattedReferenceCall(
            string guid,
            string citeAdvisorRulesSet,
            string citationStyle,
            string citeAdvisorParallelCites,
            bool citeAdvisorIncludeHistory,
            bool citeAdvisorItalicizeTile,
            bool citeAdvisorUnderlineTitle,
            string selectedText,
            string selectedHtml,
            EnvironmentInfo environmentInfo,
            CobaltProductId cobaltProductId,
            CookieContainer cookieContainer,
            NameValueCollection securityHeaders)
        {
            string postBody = this.CreateFormattedRequestBody(
                guid,
                citeAdvisorRulesSet,
                citationStyle,
                citeAdvisorParallelCites,
                citeAdvisorIncludeHistory,
                citeAdvisorItalicizeTile,
                citeAdvisorUnderlineTitle,
                selectedText,
                selectedHtml);

            var documentClient = new DocumentClient(environmentInfo, cobaltProductId, cookieContainer, securityHeaders);

            FullDocumentResponse fullResponce =
                documentClient.PostResponseBody(DocumentConstants.DocumentFormattedReference, postBody);

            FormattedReference deliveryoUriPathInfo =
                ObjectSerializer.DeserializeObject<DataContractJsonSerializer, FormattedReference>(
                    fullResponce.ResposeContent);

            return deliveryoUriPathInfo;
        }

        /// <summary>
        /// The Create post body for a batch document format request.
        /// </summary>
        /// <param name="guids">
        /// The guids.
        /// </param>
        /// <param name="citeAdvisorRulesSet">
        /// The cite advisor rules set.
        /// </param>
        /// <param name="citationStyle">
        /// The citation style.
        /// </param>
        /// <param name="citeAdvisorParallelCites">
        /// The cite advisor parallel cites.
        /// </param>
        /// <param name="citAdvisorIncludeHistory">
        /// The cit advisor include history.
        /// </param>
        /// <param name="citeAdvisorItalicizeTitle">
        /// The cite advisor italicize title.
        /// </param>
        /// <param name="citeAdvisorUndrlineTitle">
        /// The cite advisor undrline title.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string CreateBatchFormattedRequestBody(
            IEnumerable<string> guids,
            string citeAdvisorRulesSet,
            string citationStyle,
            string citeAdvisorParallelCites,
            bool citAdvisorIncludeHistory,
            bool citeAdvisorItalicizeTitle,
            bool citeAdvisorUndrlineTitle)
        {
            string guidString = guids.Aggregate(string.Empty, (current, guid) => current + @"""" + guid + @""",");

            guidString = guidString.Substring(0, guidString.Length - 1);

            string postBody = @"{
                ""documentGuids"":[" + guidString + @"],
                ""preferences"":{
                    ""CiteAdvisorRulesSet"":""" + citeAdvisorRulesSet + @""",
                    ""CiteAdvisorCitationStyle"":""" + citationStyle + @""",
                    ""CiteAdvisorParallelCites"":""" + citeAdvisorParallelCites + @""",
                    ""CiteAdvisorIncludeHistory"":" + citAdvisorIncludeHistory.ToString().ToLower() + @",
                    ""CiteAdvisorItalicizeTitle"":" + citeAdvisorItalicizeTitle.ToString().ToLower() + @",
                    ""CiteAdvisorUnderlineTitle"":" + citeAdvisorUndrlineTitle.ToString().ToLower() + @"
                    }
                }";

            return postBody;
        }

        /// <summary>
        /// The create formatted request body.
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        /// <param name="citeAdvisorRulesSet">
        /// The cite advisor rules set.
        /// </param>
        /// <param name="citationStyle">
        /// The citation style.
        /// </param>
        /// <param name="citeAdvisorParallelCites">
        /// The cite advisor parallel cites.
        /// </param>
        /// <param name="citAdvisorIncludeHistory">
        /// The cit advisor include history.
        /// </param>
        /// <param name="citeAdvisorItalicizeTitle">
        /// The cite advisor italicize title.
        /// </param>
        /// <param name="citeAdvisorUndrlineTitle">
        /// The cite advisor undrline title.
        /// </param>
        /// <param name="selectedText">
        /// The selected text.
        /// </param>
        /// <param name="selectedHtml">
        /// The selected html.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string CreateFormattedRequestBody(
            string guid,
            string citeAdvisorRulesSet,
            string citationStyle,
            string citeAdvisorParallelCites,
            bool citAdvisorIncludeHistory,
            bool citeAdvisorItalicizeTitle,
            bool citeAdvisorUndrlineTitle,
            string selectedText,
            string selectedHtml)
        {
            string guidString = @"""" + guid + @"""";
            string postBody = @"{
                ""documentGuid"":" + guidString + @",
                ""preferences"":{
                    ""CiteAdvisorRulesSet"":""" + citeAdvisorRulesSet + @""",
                    ""CiteAdvisorCitationStyle"":""" + citationStyle + @""",
                    ""CiteAdvisorParallelCites"":""" + citeAdvisorParallelCites + @""",
                    ""CiteAdvisorIncludeHistory"":" + citAdvisorIncludeHistory.ToString().ToLower() + @",
                    ""CiteAdvisorItalicizeTitle"":" + citeAdvisorItalicizeTitle.ToString().ToLower() + @",
                    ""CiteAdvisorUnderlineTitle"":" + citeAdvisorUndrlineTitle.ToString().ToLower() + @"
                    },
                        ""selectedText"":""" + selectedText + @""",
                    ""selectedHTML"":""" + selectedHtml + @"""
                        }";

            return postBody;
        }
    }
}