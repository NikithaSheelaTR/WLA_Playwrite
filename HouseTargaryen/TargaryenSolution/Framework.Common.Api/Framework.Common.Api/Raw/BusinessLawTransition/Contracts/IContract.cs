// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IContract.cs" company="Thomson Reuters">
//   Copyright 2015: Thomson Reuters. All Rights Reserved. Proprietary
//   and Confidential information of Thomson Reuters. Disclosure, Use or
//   Reproduction without the written authorization of Thomson Reuters is
//   prohibited. 
// </copyright>
// <summary>
//   Contract Interface
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Common.Api.Raw.BusinessLawTransition.Contracts
{
    /// <summary>
    /// Contract Interface
    /// </summary>
    public interface IContract
    {
        /// <summary>
        /// Is Valid  
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool IsValid();

        /// <summary>
        /// Validate Method 
        /// </summary>
        /// <returns>
        /// The <see cref="ContractValidations"/>.
        /// </returns>
        ContractValidations Validations();
    }
}