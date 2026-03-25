namespace Framework.Common.Api.Raw.IPhoneMode.Utilities
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// ContractValidations
    /// </summary>
    public class ContractValidations
    {
        /// <summary>
        /// Validation Key Value Pair
        /// </summary>
        private readonly Dictionary<string, bool> validations;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContractValidations"/> class. 
        /// </summary>
        public ContractValidations()
        {
            this.validations = new Dictionary<string, bool>();
        }

        /// <summary>
        /// Add validation using string check and pass 
        /// </summary>
        /// <param name="check"> The check. </param>
        /// <param name="pass"> The pass. </param>
        public void Add(string check, bool pass)
        {
            this.validations.Add(check, pass);
        }

        /// <summary>
        /// Valid method returns fails if any checks are fails
        /// </summary>
        /// <returns> false if any checks are fails </returns>
        public bool Valid()
        {
            return this.validations.Aggregate(true, (current, validation) => current & validation.Value);
        }
    }
}