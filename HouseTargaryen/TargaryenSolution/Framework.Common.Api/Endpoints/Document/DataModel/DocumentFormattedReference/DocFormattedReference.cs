namespace Framework.Common.Api.Endpoints.Document.DataModel.DocumentFormattedReference
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The doc formatted ref.
    /// </summary>
    [DataContract]
    public class DocFormattedReference
    {
        /// <summary>
        /// Gets or sets the document guid.
        /// </summary>
        [DataMember(Name = "documentGuid")]
        public string DocumentGuid { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        [DataMember(Name = "errorMessage")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the formatted reference.
        /// </summary>
        [DataMember(Name = "formattedReference")]
        public FormattedReference FormattedReference { get; set; }
    }
}