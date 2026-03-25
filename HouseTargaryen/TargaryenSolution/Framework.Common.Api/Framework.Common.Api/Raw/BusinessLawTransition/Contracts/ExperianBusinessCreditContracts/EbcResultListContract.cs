// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EbcResultListContract.cs" company="Thomson Reuters">
//   Copyright 2015: Thomson Reuters. All Rights Reserved. Proprietary
//   and Confidential information of Thomson Reuters. Disclosure, Use or
//   Reproduction without the written authorization of Thomson Reuters is
//   prohibited. 
// </copyright>
// <summary>
//   The ebc result list contract.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Common.Api.Raw.BusinessLawTransition.Contracts.ExperianBusinessCreditContracts
{
    using System.Collections.Generic;

    using Framework.Common.Api.Raw.BusinessLawTransition.Enums;

    /// <summary>
    /// The ebc result list contract.
    /// </summary>
    public class EbcResultListContract : IContract
    {
        /// <summary>
        /// The response type.
        /// </summary>
        public ResultListResponseType ResponseType = Enums.ResultListResponseType.BusinessSummary;

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public string createdDate { get; set; }

        /// <summary>
        /// Gets or sets the document count.
        /// </summary>
        public int documentCount { get; set; }

        /// <summary>
        /// Gets or sets the expiry.
        /// </summary>
        public Expiry expiry { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the orchestration.
        /// </summary>
        public string orchestration { get; set; }

        /// <summary>
        /// Gets or sets the orchestration meta.
        /// </summary>
        public OrchestrationMeta orchestrationMeta { get; set; }

        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        public List<Result> results { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        public string updatedDate { get; set; }

        /// <summary>
        /// The is valid.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsValid()
        {
            return this.Validations().Valid();
        }

        /// <summary>
        /// The validations.
        /// </summary>
        /// <returns>
        /// The <see cref="ContractValidations"/>.
        /// </returns>
        public ContractValidations Validations()
        {
            var validations = new ContractValidations();
            validations.Add("\nId is NOT NULL", !string.IsNullOrEmpty(this.id));
            validations.Add("Name is NOT NULL", !string.IsNullOrEmpty(this.name));
            validations.Add("Document Count is greater than Zero", this.documentCount > 0);
            validations.Add("Document Count Matches Results List Count", this.documentCount == this.results.Count);
            validations.Add("createdDate is NOT NULL", !string.IsNullOrEmpty(this.createdDate));
            validations.Add("updatedDate is NOT NULL", !string.IsNullOrEmpty(this.updatedDate));
            validations.Add(
                "Orchestration equals businesscreditreports",
                this.orchestration.Equals("businesscreditreports"));

            validations.Add(
                "Name equals Business Credit Reports Results",
                this.name.Equals("ExperianGateway Search Result"));
            validations.Add("State equals COMPLETE", this.state.Equals("COMPLETE"));

            switch (this.ResponseType)
            {
                case Enums.ResultListResponseType.BusinessSummary:
                    validations.Add(
                        "reportType is BUSINESS_SUMMARY",
                        this.orchestrationMeta.reportType == "BUSINESS_SUMMARY");
                    break;
                case Enums.ResultListResponseType.PremierProfile:
                    validations.Add(
                        "reportType is PREMIER_PROFILE",
                        this.orchestrationMeta.reportType == "PREMIER_PROFILE");
                    break;
                case Enums.ResultListResponseType.IntelliscorePlus:
                    validations.Add(
                        "reportType is INTELLISCORE_PLUS",
                        this.orchestrationMeta.reportType == "INTELLISCORE_PLUS");
                    break;
            }

            return validations;
        }

        /// <summary>
        /// The expiry.
        /// </summary>
        public class Expiry
        {
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
            /// Gets or sets the company address.
            /// </summary>
            public string companyAddress { get; set; }

            /// <summary>
            /// Gets or sets the company city.
            /// </summary>
            public string companyCity { get; set; }

            /// <summary>
            /// Gets or sets the company id.
            /// </summary>
            public string companyId { get; set; }

            /// <summary>
            /// Gets or sets the company name.
            /// </summary>
            public string companyName { get; set; }

            /// <summary>
            /// Gets or sets the company zip.
            /// </summary>
            public string companyZip { get; set; }

            /// <summary>
            /// Gets or sets the report type.
            /// </summary>
            public string reportType { get; set; }
        }

        /// <summary>
        /// The orchestration meta.
        /// </summary>
        public class OrchestrationMeta
        {
            /// <summary>
            /// Gets or sets the report type.
            /// </summary>
            public string reportType { get; set; }

            /// <summary>
            /// Gets or sets the transaction id.
            /// </summary>
            public string transactionId { get; set; }
        }

        /// <summary>
        /// The result.
        /// </summary>
        public class Result
        {
            /// <summary>
            /// Gets or sets the created date.
            /// </summary>
            public string createdDate { get; set; }

            /// <summary>
            /// Gets or sets the id.
            /// </summary>
            public string id { get; set; }

            /// <summary>
            /// Gets or sets the index.
            /// </summary>
            public int index { get; set; }

            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// Gets or sets the orchestration data.
            /// </summary>
            public OrchestrationData orchestrationData { get; set; }
        }
    }
}