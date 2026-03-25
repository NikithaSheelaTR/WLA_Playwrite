// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecurityTokenContract.cs" company="Thomson Reuters">
//   Copyright 2015: Thomson Reuters. All Rights Reserved. Proprietary
//   and Confidential information of Thomson Reuters. Disclosure, Use or
//   Reproduction without the written authorization of Thomson Reuters is
//   prohibited. 
// </copyright>
// <summary>
//   The security token contract.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Common.Api.Raw.BusinessLawTransition.Contracts
{
    using System;

    /// <summary>
    /// The security token contract.
    /// </summary>
    public class SecurityTokenContract : IContract
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The is valid.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The validations.
        /// </summary>
        /// <returns>
        /// The <see cref="ContractValidations"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public ContractValidations Validations()
        {
            throw new NotImplementedException();
        }
    }
}