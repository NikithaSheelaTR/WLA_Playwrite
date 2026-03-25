// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EbcCreateDocumentContract.cs" company="Thomson Reuters">
//   Copyright 2015: Thomson Reuters. All Rights Reserved. Proprietary
//   and Confidential information of Thomson Reuters. Disclosure, Use or
//   Reproduction without the written authorization of Thomson Reuters is
//   prohibited. 
// </copyright>
// <summary>
//   The ebc create document contract.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Common.Api.Raw.BusinessLawTransition.Contracts.ExperianBusinessCreditContracts
{
    /// <summary>
    /// The ebc create document contract.
    /// </summary>
    public class EbcCreateDocumentContract : IContract
    {
        /// <summary>
        /// Gets or sets the document guid.
        /// </summary>
        public string documentGuid { get; set; }

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
            validations.Add("\nDocumentGuid is NOT NULL", !string.IsNullOrEmpty(this.documentGuid));
            return validations;
        }
    }
}