namespace Framework.Common.Api.Raw.IPhoneMode.Models.Responses
{
    using System.Collections.Generic;

    using Framework.Common.Api.Raw.IPhoneMode.Utilities;

    /// <summary>
    /// Create Practice Area Alerts Response
    /// </summary>
    public class CreatePracticeAreaAlertsResponse : IResponse
    {
        /// <summary>
        /// Failure GUIDs
        /// </summary>
        public List<object> FailureGuids { get; set; }

        /// <summary>
        /// Success GUIDs
        /// </summary>
        public List<SuccessGuidsItem> SuccessGuids { get; set; }

        /// <summary>
        /// Is Valid
        /// </summary>
        /// <returns>true if valid, false otherwise</returns>
        public bool IsValid()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Validations
        /// </summary>
        /// <returns> Contract Validations </returns>
        public ContractValidations Validations()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Success GUIDs Item
        /// </summary>
        public class SuccessGuidsItem
        {
            /// <summary>
            /// Gets or sets the guid.
            /// </summary>
            public string Guid { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether is news letters updated.
            /// </summary>
            public bool IsNewsLettersUpdated { get; set; }

            /// <summary>
            /// Gets or sets the title.
            /// </summary>
            public string Title { get; set; }
        }
    }
}