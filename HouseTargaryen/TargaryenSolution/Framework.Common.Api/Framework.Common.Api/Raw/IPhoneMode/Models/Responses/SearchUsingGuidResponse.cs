namespace Framework.Common.Api.Raw.IPhoneMode.Models.Responses
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using Framework.Common.Api.Raw.IPhoneMode.Utilities;

    /// <summary>
    /// Search Using GUID Response
    /// </summary>
    [DataContract]
    public class SearchUsingGuidResponse : IResponse
    {
        /// <summary>
        /// Gets or sets the available facets.
        /// </summary>
        /// <value>
        /// The available facets.
        /// </value>
        [DataMember(Name = "availableFacets")]
        public List<AvailableFacets> AvailableFacetsList { get; set; }

        /// <summary>
        /// Is Valid
        /// </summary>
        /// <returns>true if valid</returns>
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

        /// <summary>
        /// AvailableFacets class and its properties
        /// </summary>
        [DataContract]
        public class AvailableFacets
        {
            /// <summary>
            /// Gets or sets the content type.
            /// </summary>
            [DataMember(Name = "contentType")]
            public string ContentType { get; set; }

            /// <summary>
            /// Gets or sets the display name.
            /// </summary>
            [DataMember(Name = "displayName")]
            public string DisplayName { get; set; }

            /// <summary>
            /// Gets or sets the total documents.
            /// </summary>
            [DataMember(Name = "totalDocuments")]
            public int TotalDocuments { get; set; }

            /// <summary>
            /// Gets or sets the total documents display.
            /// </summary>
            [DataMember(Name = "totalDocumentsDisplay")]
            public string TotalDocumentsDisplay { get; set; }
        }
    }
}