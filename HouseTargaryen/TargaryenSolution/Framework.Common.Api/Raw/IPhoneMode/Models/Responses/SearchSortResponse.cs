namespace Framework.Common.Api.Raw.IPhoneMode.Models.Responses
{
    using Framework.Common.Api.Raw.IPhoneMode.Utilities;

    /// <summary>
    /// SearchSortResponse class
    /// </summary>
    public class SearchSortResponse : IResponse
    {
        /// <summary>
        /// Gets or sets the display query.
        /// </summary>
        public string DisplayQuery { get; set; }

        /// <summary>
        /// Gets or sets the search GUID.
        /// </summary>
        public string SearchGuid { get; set; }

        /// <summary>
        /// Is Valid
        /// </summary>
        /// <returns>true if valid, false otherwise</returns>
        public bool IsValid()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Validate Method
        /// </summary>
        /// <returns>Contract Validations</returns>
        public ContractValidations Validations()
        {
            throw new System.NotImplementedException();
        }
    }
}