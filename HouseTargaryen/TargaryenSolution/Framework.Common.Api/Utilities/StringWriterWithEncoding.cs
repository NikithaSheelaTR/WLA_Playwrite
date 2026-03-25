namespace Framework.Common.Api.Utilities
{
    using System.IO;
    using System.Text;

    /// <summary>
    /// The string writer with encoding.
    /// </summary>
    internal class StringWriterWithEncoding : StringWriter
    {
        /// <summary>
        /// The encoding.
        /// </summary>
        private readonly Encoding encoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringWriterWithEncoding"/> class.
        /// </summary>
        /// <param name="encoding">
        /// The encoding.
        /// </param>
        public StringWriterWithEncoding(Encoding encoding)
        {
            this.encoding = encoding;
        }

        /// <summary>
        /// Gets the encoding.
        /// </summary>
        public override Encoding Encoding
        {
            get
            {
                return this.encoding;
            }
        }
    }
}