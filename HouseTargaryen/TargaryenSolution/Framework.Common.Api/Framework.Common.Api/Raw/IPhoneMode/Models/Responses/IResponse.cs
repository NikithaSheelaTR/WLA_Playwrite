namespace Framework.Common.Api.Raw.IPhoneMode.Models.Responses
{
    using Framework.Common.Api.Raw.IPhoneMode.Utilities;

    /// <summary>
    /// Contract Interface
    /// </summary>
    public interface IResponse
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