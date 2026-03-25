// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EbcResultListErrorContract.cs" company="Thomson Reuters">
//   Copyright 2015: Thomson Reuters. All Rights Reserved. Proprietary
//   and Confidential information of Thomson Reuters. Disclosure, Use or
//   Reproduction without the written authorization of Thomson Reuters is
//   prohibited. 
// </copyright>
// <summary>
//   The ebc result list error contract.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Common.Api.Raw.BusinessLawTransition.Contracts.ExperianBusinessCreditContracts
{
    using Framework.Common.Api.Raw.BusinessLawTransition.Enums;

    /// <summary>
    /// The ebc result list error contract.
    /// </summary>
    public class EbcResultListErrorContract : EbcResultListContract
    {
        /// <summary>
        /// The response type.
        /// </summary>
        public new ResultListErrorResponseType ResponseType = Enums.ResultListErrorResponseType.MissingCompanyName;

        /// <summary>
        /// Gets or sets the orchestration meta.
        /// </summary>
        public new OrchestrationMeta orchestrationMeta { get; set; }

        /// <summary>
        /// The is valid.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public new bool IsValid()
        {
            return this.Validations().Valid();
        }

        /// <summary>
        /// The validations.
        /// </summary>
        /// <returns>
        /// The <see cref="ContractValidations"/>.
        /// </returns>
        public new ContractValidations Validations()
        {
            var validations = new ContractValidations();
            validations.Add("\nError Flag is set to true", this.orchestrationMeta.isError);
            validations.Add("Document Count is Zero", this.documentCount == 0);
            validations.Add(
                "Orchestration text is businesscreditreports",
                this.orchestration.Equals("businesscreditreports"));

            // Missing state error, has name as "Error - IllegalArgumentException", other error conditions have name as "Error - GatewayException"
            // validations.Add("Name is Error - GatewayException", name.Equals("Error - GatewayException"));
            validations.Add("State is COMPLETE", this.state.Equals("COMPLETE"));

            switch (this.ResponseType)
            {
                case Enums.ResultListErrorResponseType.MissingCompanyName:
                    validations.Add(
                        "Error Text for Missing Company Name is correct",
                        this.orchestrationMeta.errorInformation.exceptionText.Equals(
                            "companyName is required and cannot exceed 40 characters"));
                    break;
                case Enums.ResultListErrorResponseType.MissingReportType:
                    validations.Add(
                        "Error Text for Missing Report Type is correct",
                        this.orchestrationMeta.errorInformation.exceptionText.Equals(
                            "reportType must not be null and must be valid"));
                    break;
                case Enums.ResultListErrorResponseType.MissingCity:
                    validations.Add(
                        "Error Text for Missing City is correct",
                        this.orchestrationMeta.errorInformation.exceptionText.Equals(
                            "companyCity is required and cannot exceed 30 characters"));
                    break;
                case Enums.ResultListErrorResponseType.MissingState:
                    validations.Add(
                        "Error Text for Missing State is correct",
                        this.orchestrationMeta.errorInformation.exceptionText.Equals(
                            "companyState is required and must be 2 characters numeric"));
                    break;
                case Enums.ResultListErrorResponseType.MissingZip:
                    validations.Add(
                        "Error Text for Missing Zip Code is correct",
                        this.orchestrationMeta.errorInformation.exceptionText.Equals(
                            "companyZip is required and must be 5 characters numeric"));
                    break;
            }

            return validations;
        }

        /// <summary>
        /// The error information.
        /// </summary>
        public class ErrorInformation
        {
            /// <summary>
            /// Gets or sets the exception text.
            /// </summary>
            public string exceptionText { get; set; }

            /// <summary>
            /// Gets or sets the exception type.
            /// </summary>
            public string exceptionType { get; set; }
        }

        /// <summary>
        /// The orchestration meta.
        /// </summary>
        public new class OrchestrationMeta
        {
            /// <summary>
            /// Gets or sets the error information.
            /// </summary>
            public ErrorInformation errorInformation { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether is error.
            /// </summary>
            public bool isError { get; set; }
        }
    }
}