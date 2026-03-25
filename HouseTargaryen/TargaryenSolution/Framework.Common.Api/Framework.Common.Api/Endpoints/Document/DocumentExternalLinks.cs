namespace Framework.Common.Api.Endpoints.Document
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using System.Xml;
    using Framework.Common.Api.Endpoints.Document.DataModel;
    using Framework.Common.Api.Endpoints.Document.DataModel.Constants;
    using Framework.Common.Api.Endpoints.Uds.DataModel;
    using Framework.Common.Api.Endpoints.Website;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Utils;
    using RestSharp;

    /// <summary>
    /// The document chunk links.
    /// </summary>
    public class DocumentExternalLinks
    {
        /// <summary>
        /// The get all chunk bodies.
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        /// <param name="linkText">
        /// The link text.
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
        /// The <see cref="DocumentLinkServiceResponse"/>.
        /// </returns>
        public DocumentLinkServiceResponse GetLinkService(
            string guid,
            string linkText,
            EnvironmentInfo environmentInfo,
            CobaltProductId cobaltProductId,
            CookieContainer cookieContainer,
            NameValueCollection securityHeaders)
        {
            var chunkBodies = new List<string>();
            bool foundAllChunks = false;
            //this parameter is used to prevent from the looping if the environments are not stable
            int maxChunkCount = 15;
            int chunkNumb = 0;

            var websiteClient = new WebsiteClient(environmentInfo,cobaltProductId,cookieContainer,securityHeaders);
            websiteClient.GetDocument(guid);

            var documentClient = new DocumentClient(environmentInfo, cobaltProductId, cookieContainer, securityHeaders);

            while (!foundAllChunks)
            {
                FullDocumentResponse docResponseChunk = documentClient.GetFullHtmlDocumentResponse(guid, chunkNumb);

                // if we've hit the end of the chunks set the value so we break out
                Parameter cobaltEventsHeader =
                    docResponseChunk.ResponseHeader.SingleOrDefault(n => n.Name == "x-cobalt-events");

                foundAllChunks = docResponseChunk.StatusDescription == "No Content"
                                 && Convert.ToString(cobaltEventsHeader.Value)
                                           .Contains("The requested chunk was out of range");

                if (!foundAllChunks)
                {
                    chunkBodies.Add(docResponseChunk.ResposeContent);
                }
                // increment to the next chunk
                ++chunkNumb;
                if (chunkNumb > maxChunkCount)
                {
                    foundAllChunks = true;
                }
            }

            int lastChunkIndex = chunkBodies.Count;

            if (lastChunkIndex > 0)
            {
                --lastChunkIndex;
            }

            XmlNode getLink = DocumentExternalLinks.GetExternalLinkNode(chunkBodies[lastChunkIndex], linkText);

            if (getLink == null)
            {
                int indexCount = 0;

                do
                {
                    getLink = DocumentExternalLinks.GetExternalLinkNode(chunkBodies[indexCount], linkText);
                    ++indexCount;
                }
                while (getLink == null);
            }

            string queryString = DocumentExternalLinks.GetQueryStringFromXmlNode(getLink);
            string url = DocumentConstants.DocumentLinkV1 + queryString;
        
            FullDocumentResponse documentLinkServiceResponse = documentClient.GetDocumentBySource(url);

            DocumentLinkServiceResponse docLinkService =
                ObjectSerializer.DeserializeObject<DataContractJsonSerializer, DocumentLinkServiceResponse>(
                    documentLinkServiceResponse.ResposeContent);

            return docLinkService;
        }

        /// <summary>
        /// The cleanup link text.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string CleanupLinkText(string text)
        {
            while (text.Contains("<"))
            {
                int lessThanPos = text.IndexOf("<", StringComparison.Ordinal);
                int greaterThanPos = text.IndexOf(">", StringComparison.Ordinal);

                text = text.Remove(lessThanPos, greaterThanPos - lessThanPos + 1);
            }

            // fix section symbol+funky space issue
            if (text.Contains("§ "))
            {
                text = text.Replace("§ ", "§");
            }

            // ampersand encoding
            if (text.Contains("&amp;"))
            {
                text = text.Replace("&amp;", "&");
            }

            // strip out spaces?? not sure why
            text = text.Replace(" ", string.Empty);
            return text;
        }

        /// <summary>
        /// The get external link node.
        /// </summary>
        /// <param name="xhtmlString">
        /// The xhtml string.
        /// </param>
        /// <param name="linkText">
        /// The link text.
        /// </param>
        /// <returns>
        /// The <see cref="XmlNode"/>.
        /// </returns>
        private static XmlNode GetExternalLinkNode(string xhtmlString, string linkText)
        {
            // Calls the fulltext service to get the fulltext of the document.
            // Load the document in an xml document.
            var xd = new XmlDocument();

            xd.LoadXml("<div>" + xhtmlString + "</div>");
            XmlNodeList allLinks = xd.SelectNodes("//a");

            // create a list of xmlnodes that contain the external links
            var externalLinks = new List<XmlNode>();

            // foreach item in the all links list, if it has "fulltext?"
            // in it's href attribute, then add it to the external links list
            if (allLinks != null)
            {
                foreach (XmlNode node in allLinks)
                {
                    XmlNode href = node.Attributes["href"];

                    if (href != null && href.Value.ToLowerInvariant().Contains("fulltext?"))
                    {
                        externalLinks.Add(node);
                    }
                }
            }

            // node to return if the link was found in the document
            XmlNode returnLinkNode = null;

            // link coming from db may contain xml tags, so strip 'em out
            string cleanLinkText = DocumentExternalLinks.CleanupLinkText(linkText);

            // Get the link text from the document by comparing with the link text from the view, 
            // then call the CheckErrorCode method
            foreach (XmlNode link in externalLinks)
            {
                if (cleanLinkText.ToLower().Equals(link.InnerText.ToLower().Replace(" ", string.Empty)))
                {
                    // we found the link
                    returnLinkNode = link;
                    break;
                }
            }

            return returnLinkNode;
        }

        /// <summary>
        /// pull the query string out of the href for this link's node
        /// </summary>
        /// <param name="node">xml node that represents a link in a document</param>
        /// <returns>href string from the anchor tag</returns>
        private static string GetQueryStringFromXmlNode(XmlNode node)
        {
            // get the href value
            string href = node.Attributes["href"].Value;

            // strip off 'http://www.ci.westlaw.com/link/document/fulltext?' from front.
            return href.Substring(href.IndexOf("?") + 1);
        }
    }
}