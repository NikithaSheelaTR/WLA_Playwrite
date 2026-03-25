namespace Framework.Common.Api.Utilities
{
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// Provides methods to serialize objects to xml string and xml string to objects
    /// </summary>
    public static class XmlUtility
    {
        /// <summary>
        /// Serialized object to xml string with specified encoding
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="encoding">
        /// The encoding.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Serialize<T>(T value, Encoding encoding)
        {
            if (value == null)
            {
                return null;
            }

            var serializer = new XmlSerializer(typeof(T));
            var ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);
            var settings = new XmlWriterSettings
                               {
                                   Encoding = new UnicodeEncoding(false, false),
                                   Indent = true,
                                   OmitXmlDeclaration = false
                               };

            using (var sw = new StringWriterWithEncoding(encoding))
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(sw, settings))
                {
                    serializer.Serialize(xmlWriter, value, ns);
                }

                return sw.ToString();
            }
        }
    }
}