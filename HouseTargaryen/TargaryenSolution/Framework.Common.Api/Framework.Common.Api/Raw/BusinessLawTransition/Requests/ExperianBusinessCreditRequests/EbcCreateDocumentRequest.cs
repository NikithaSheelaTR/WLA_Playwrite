// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EbcCreateDocumentRequest.cs" company="Thomson Reuters">
//   Copyright 2015: Thomson Reuters. All Rights Reserved. Proprietary
//   and Confidential information of Thomson Reuters. Disclosure, Use or
//   Reproduction without the written authorization of Thomson Reuters is
//   prohibited. 
// </copyright>
// <summary>
//   The ebc create document request.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Common.Api.Raw.BusinessLawTransition.Requests.ExperianBusinessCreditRequests
{
    using System.Web.Script.Serialization;

    /// <summary>
    /// The ebc create document request.
    /// </summary>
    public class EbcCreateDocumentRequest : IRequest
    {
        /// <summary>
        /// The body.
        /// </summary>
        public SmartBusinessReportCreateDocumentRequestBody Body;

        /// <summary>
        /// The results list guid.
        /// </summary>
        public string ResultsListGuid;

        /// <summary>
        /// The get request body.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetRequestBody()
        {
            return new JavaScriptSerializer().Serialize(this.Body);
        }

        /// <summary>
        /// The smart business report create document request body.
        /// </summary>
        public class SmartBusinessReportCreateDocumentRequestBody
        {
            /// <summary>
            /// Gets or sets the expiry.
            /// </summary>
            public EbcResultsListRequest.Expiry expiry { get; set; }

            /// <summary>
            /// Gets or sets the index.
            /// </summary>
            public int index { get; set; }
        }
    }
}