// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EbcGetDocumentRequest.cs" company="Thomson Reuters">
//   Copyright 2015: Thomson Reuters. All Rights Reserved. Proprietary
//   and Confidential information of Thomson Reuters. Disclosure, Use or
//   Reproduction without the written authorization of Thomson Reuters is
//   prohibited. 
// </copyright>
// <summary>
//   The ebc get document request.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Common.Api.Raw.BusinessLawTransition.Contracts.ExperianBusinessCreditContracts
{
    using System.Web.Script.Serialization;

    using Framework.Common.Api.Raw.BusinessLawTransition.Requests;
    using Framework.Common.Api.Raw.BusinessLawTransition.Requests.ExperianBusinessCreditRequests;

    /// <summary>
    /// The ebc get document request.
    /// </summary>
    public class EbcGetDocumentRequest : IRequest
    {
        /// <summary>
        /// The body.
        /// </summary>
        public SmartBusinessReporGetDocumentRequestBody Body;

        /// <summary>
        /// The doc guid.
        /// </summary>
        public string DocGuid;

        /// <summary>
        /// Initializes a new instance of the <see cref="EbcGetDocumentRequest"/> class.
        /// </summary>
        /// <param name="docGuidValue">
        /// The doc guid.
        /// </param>
        /// <param name="indexValue">
        /// The _index.
        /// </param>
        public EbcGetDocumentRequest(string docGuidValue, int indexValue)
        {
            this.DocGuid = docGuidValue;
            this.Body = new SmartBusinessReporGetDocumentRequestBody
                            {
                                index = indexValue,
                                expiry =
                                    new EbcResultsListRequest.Expiry
                                        {
                                            type
                                                =
                                                "temporal"

                                            // duration = "2015-07-31T12:00:00-05:00"
                                        }
                            };
        }

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
    }

    /// <summary>
    /// The smart business repor get document request body.
    /// </summary>
    public class SmartBusinessReporGetDocumentRequestBody
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