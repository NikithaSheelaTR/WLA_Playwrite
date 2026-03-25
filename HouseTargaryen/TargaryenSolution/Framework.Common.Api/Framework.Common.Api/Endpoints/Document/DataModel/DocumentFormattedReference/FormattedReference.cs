namespace Framework.Common.Api.Endpoints.Document.DataModel.DocumentFormattedReference
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The formatted reference.
    /// </summary>
    [DataContract]
    public class FormattedReference
    {
        /// <summary>
        /// Gets or sets the html text.
        /// </summary>
        [DataMember(Name = "htmlText")]
        public string HtmlText { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the plain text.
        /// </summary>
        [DataMember(Name = "plainText")]
        public string PlainText { get; set; }

        /// <summary>
        /// Gets or sets the rich text.
        /// </summary>
        [DataMember(Name = "richText")]
        public string RichText { get; set; }
    }
}