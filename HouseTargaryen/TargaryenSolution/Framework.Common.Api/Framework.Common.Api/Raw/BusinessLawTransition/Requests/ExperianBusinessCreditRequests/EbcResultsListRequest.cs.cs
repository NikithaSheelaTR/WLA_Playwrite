// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EbcResultsListRequest.cs.cs" company="Thomson Reuters">
//   Copyright 2015: Thomson Reuters. All Rights Reserved. Proprietary
//   and Confidential information of Thomson Reuters. Disclosure, Use or
//   Reproduction without the written authorization of Thomson Reuters is
//   prohibited. 
// </copyright>
// <summary>
//   The ebc results list request.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Common.Api.Raw.BusinessLawTransition.Requests.ExperianBusinessCreditRequests
{
    using System;
    using System.Web.Script.Serialization;

    /// <summary>
    /// The ebc results list request.
    /// </summary>
    public class EbcResultsListRequest : IRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EbcResultsListRequest"/> class.
        /// </summary>
        /// <param name="companyName">
        /// The company name.
        /// </param>
        /// <param name="companyCity">
        /// The company city.
        /// </param>
        /// <param name="state">
        /// The state.
        /// </param>
        /// <param name="companyZip">
        /// The company zip.
        /// </param>
        /// <param name="reportType">
        /// The report type.
        /// </param>
        /// <param name="useTestData">
        /// The use test data.
        /// </param>
        public EbcResultsListRequest(
            string companyName,
            string companyCity,
            string state,
            string companyZip,
            string reportType,
            bool useTestData = true)
        {
            this.orchestrationData = new OrchestrationData
                                         {
                                             companyName = companyName,
                                             companyCity = companyCity,
                                             companyState = state,
                                             companyZip = companyZip,
                                             reportType = reportType,
                                             useTestData = useTestData
                                         };

            this.expiry = new Expiry { type = "temporal" /*, duration = "2015-07-31T12:00:00-05:00"*/ };
        }

        /// <summary>
        /// Gets or sets the expiry.
        /// </summary>
        public Expiry expiry { get; set; }

        /// <summary>
        /// Gets or sets the orchestration data.
        /// </summary>
        public OrchestrationData orchestrationData { get; set; }

        /// <summary>
        /// The get request body.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetRequestBody()
        {
            return new JavaScriptSerializer().Serialize(this);
        }

        /// <summary>
        /// The expiry.
        /// </summary>
        public class Expiry
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Expiry"/> class.
            /// </summary>
            public Expiry()
            {
                DateTime currentDateTime = DateTime.Now;
                currentDateTime = currentDateTime.AddDays(7);
                this.duration = currentDateTime.ToString("yyyy-MM-ddThh:mm:sszzz");
            }

            /// <summary>
            /// Gets or sets the duration.
            /// </summary>
            public string duration { get; set; }

            /// <summary>
            /// Gets or sets the type.
            /// </summary>
            public string type { get; set; }
        }

        /// <summary>
        /// The orchestration data.
        /// </summary>
        public class OrchestrationData
        {
            /// <summary>
            /// Gets or sets the company city.
            /// </summary>
            public string companyCity { get; set; }

            /// <summary>
            /// Gets or sets the company name.
            /// </summary>
            public string companyName { get; set; }

            /// <summary>
            /// Gets or sets the company state.
            /// </summary>
            public string companyState { get; set; }

            /// <summary>
            /// Gets or sets the company zip.
            /// </summary>
            public string companyZip { get; set; }

            /// <summary>
            /// Gets or sets the report type.
            /// </summary>
            public string reportType { get; set; }

            /// <summary>
            /// Set use static test data instead or making a live call to Experian.
            /// </summary>
            public bool useTestData { get; set; }
        }
    }
}