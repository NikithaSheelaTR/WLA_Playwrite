namespace Framework.Common.Api.Endpoints.CaseNoteBook.DataModel.RequestBody
{
    using System.Text;
    using System.Xml.Serialization;

    using Framework.Common.Api.Utilities;

    /// <summary>
    /// The key cite flag request.
    /// </summary>
    [XmlType(AnonymousType = true), XmlRoot("keycite.flag.update.set", Namespace = "", IsNullable = false)]
    public class KeyCiteFlagRequest
    {
        /// <summary>
        /// Gets or sets the dtdversion.
        /// </summary>
        [XmlAttribute("dtd.version")]
        public string Dtdversion { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [XmlElement("keycite.flag.update.username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [XmlElement("keycite.flag.update.password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        [XmlElement("keycite.flag.update.value")]
        public KeyCiteValue[] Values { get; set; }

        /// <summary>
        /// Returns serialized xml string
        /// </summary>
        /// <returns></returns>
        public string GetRequestBody()
        {
            return XmlUtility.Serialize(this, Encoding.UTF8);
        }
    }
}