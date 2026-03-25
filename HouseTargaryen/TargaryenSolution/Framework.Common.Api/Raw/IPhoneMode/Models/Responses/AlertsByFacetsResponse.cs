namespace Framework.Common.Api.Raw.IPhoneMode.Models.Responses
{
    using System.Collections.Generic;

    using Framework.Common.Api.Raw.IPhoneMode.Utilities;

    /// <summary>
    /// AlertsByFacetsResponse class and its properties
    /// </summary>
    public class AlertsByFacetsResponse : IResponse
    {
        /// <summary>
        /// Gets or sets the alerts.
        /// </summary>
        public List<Alert> Alerts { get; set; }

        /// <summary>
        /// Gets or sets the reports.
        /// </summary>
        public object Reports { get; set; }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Is Valid
        /// </summary>
        /// <returns> true if valid </returns>
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
    }

    /// <summary>
    /// Alert class and its properties
    /// </summary>
    public class Alert
    {
        /// <summary>
        /// Gets or sets the alert type.
        /// </summary>
        public string AlertType { get; set; }

        /// <summary>
        /// Gets or sets the GUID.
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}