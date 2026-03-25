namespace Framework.Core.DataModel.Configuration.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Provides a means of storing information about Attachments.
    /// </summary>
    [Serializable, XmlRoot("Attachments")]
    public sealed class AttachmentsInfo
    {
        /// <summary>
        /// Gets or sets attachment
        /// </summary>
        [XmlElement("Attachment")]
        public List<string> Attachment { get; set; }
    }
}