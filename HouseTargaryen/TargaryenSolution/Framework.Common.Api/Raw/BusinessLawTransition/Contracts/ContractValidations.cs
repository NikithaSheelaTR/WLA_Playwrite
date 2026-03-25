// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContractValidations.cs" company="Thomson Reuters">
//   Copyright 2015: Thomson Reuters. All Rights Reserved. Proprietary
//   and Confidential information of Thomson Reuters. Disclosure, Use or
//   Reproduction without the written authorization of Thomson Reuters is
//   prohibited. 
// </copyright>
// <summary>
//   ContractValidations class
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Common.Api.Raw.BusinessLawTransition.Contracts
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// ContractValidations class
    /// </summary>
    public class ContractValidations
    {
        /// <summary>
        /// Validation Key Value Pair
        /// </summary>
        public Dictionary<string, bool> Validations;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContractValidations"/> class. 
        /// ContractValidation Constructor
        /// </summary>
        public ContractValidations()
        {
            this.Validations = new Dictionary<string, bool>();
        }

        /// <summary>
        /// Add validation using KeyValuePair
        /// </summary>
        /// <param name="validation">
        /// </param>
        public void Add(KeyValuePair<string, bool> validation)
        {
            this.Validations.Add(validation.Key, validation.Value);
        }

        /// <summary>
        /// Add validation using string check and boolean pass
        /// </summary>
        /// <param name="check">
        /// The check.
        /// </param>
        /// <param name="pass">
        /// The pass.
        /// </param>
        public void Add(string check, bool pass)
        {
            this.Validations.Add(check, pass);
        }

        /// <summary>
        /// Valid method returns false if any checks are false.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Valid()
        {
            return this.Validations.Aggregate(true, (current, validation) => current & validation.Value);
        }
    }
}