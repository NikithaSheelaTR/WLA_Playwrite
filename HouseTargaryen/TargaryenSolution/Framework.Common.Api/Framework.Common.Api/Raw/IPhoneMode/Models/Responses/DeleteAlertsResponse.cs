namespace Framework.Common.Api.Raw.IPhoneMode.Models.Responses
{
    using System.Collections.Generic;

    using Framework.Common.Api.Raw.IPhoneMode.Utilities;

    /// <summary>
    /// Delete Alerts Response
    /// </summary>
    public class DeleteAlertsResponse : IResponse
    {
        /// <summary>
        /// failureGuids
        /// </summary>
        public List<AlertSummary> FailureGuids { get; set; }

        /// <summary>
        /// successGuids
        /// </summary>
        public List<AlertSummary> SuccessGuids { get; set; }

        /// <summary>
        /// Is Valid
        /// </summary>
        /// <returns> true if valid </returns>
        public bool IsValid()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Contract Validations
        /// </summary>
        /// <returns> Validations </returns>
        public ContractValidations Validations()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// AlertSummary
        /// </summary>
        public class AlertSummary
        {
            /// <summary>
            /// Gets or sets the guid.
            /// </summary>
            public string Guid { get; set; }

            /// <summary>
            /// Gets or sets the title.
            /// </summary>
            public string Title { get; set; }
        }
    }
}