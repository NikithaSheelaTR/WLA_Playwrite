namespace Framework.Common.Api.Endpoints.Uds.DataModel
{
    using System.Collections.Generic;

    using HtmlAgilityPack;

    using RestSharp;

    /// <summary>
    /// The full document response.
    /// </summary>
    public class FullDocumentResponse
    {
        /// <summary>
        /// Gets or sets the html document response.
        /// </summary>
        public HtmlDocument HtmlDocumentResponse { get; set; }

        /// <summary>
        /// Gets or sets the response header.
        /// </summary>
        public IList<Parameter> ResponseHeader { get; set; }

        /// <summary>
        /// Gets or sets the respose content.
        /// </summary>
        public string ResposeContent { get; set; }

        /// <summary>
        /// Gets or sets the status desription.
        /// </summary>
        public string StatusDescription { get; set; }
    }
}