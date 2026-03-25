namespace Framework.Core.Net
{
    /// <summary>
    /// A list of some known MIME types for use with <see cref="Framework.Core.Net.HttpUtils.ConstructHttpWebRequest"/>.
    /// </summary>
    public static class MimeType
    {
        /// <summary>
        /// A list of some known <c>application</c> MIME types.
        /// </summary>
        public static class Application
        {
            /// <summary>
            /// Gets the value <c>application/x-gtar</c>.
            /// </summary>
            public const string Gtar = "application/x-gtar";

            /// <summary>
            /// Gets the value <c>application/x-gzip</c>.
            /// </summary>
            public const string Gzip = "application/x-gzip";

            /// <summary>
            /// Gets the value <c>application/json</c>.
            /// </summary>
            public const string Json = "application/json";

            /// <summary>
            /// Gets the value <c>application/vnd.ms-excel</c>.
            /// </summary>
            public const string MsExcelXls = "application/vnd.ms-excel";

            /// <summary>
            /// Gets the value <c>application/vnd.openxmlformats-officedocument.spreadsheetml.sheet</c>.
            /// </summary>
            public const string MsExcelXlsx = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            /// <summary>
            /// Gets the value <c>application/vnd.ms-outlook</c>.
            /// </summary>
            public const string MsOutlookMsg = "application/vnd.ms-outlook";

            /// <summary>
            /// Gets the value <c>application/msword</c>.
            /// </summary>
            public const string MsWordDoc = "application/msword";

            /// <summary>
            /// Gets the value <c>application/vnd.openxmlformats-officedocument.wordprocessingml.document</c>.
            /// </summary>
            public const string MsWordDocx = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

            /// <summary>
            /// Gets the value <c>application/vnd.ms-powerpoint</c>.
            /// </summary>
            public const string MsPowerpointPpt = "application/vnd.ms-powerpoint";

            /// <summary>
            /// Gets the value <c>application/vnd.openxmlformats-officedocument.presentationml.presentation</c>.
            /// </summary>
            public const string MsPowerpointPptx = "application/vnd.openxmlformats-officedocument.presentationml.presentation";

            /// <summary>
            /// Gets the value <c>application/octet-stream</c>.
            /// </summary>
            public const string OctetStream = "application/octet-stream";

            /// <summary>
            /// Gets the value <c>application/pdf</c>.
            /// </summary>
            public const string Pdf = "application/pdf";

            /// <summary>
            /// Gets the value <c>application/rtf</c>.
            /// </summary>
            public const string Rtf = "application/rtf";

            /// <summary>
            /// Gets the value <c>application/soap+xml</c>.
            /// </summary>
            public const string Soap = "application/soap+xml";

            /// <summary>
            /// Gets the value <c>application/x-tar</c>.
            /// </summary>
            public const string Tar = "application/x-tar";

            /// <summary>
            /// Gets the value <c>application/x-wgsl</c>.
            /// </summary>
            public const string WestgroupSmartLabel = "application/x-wgsl";

            /// <summary>
            /// Gets the value <c>application/xml</c>.
            /// </summary>
            public const string Xml = "application/xml";

            /// <summary>
            /// Gets the value <c>application/zip</c>.
            /// </summary>
            public const string Zip = "application/zip";
        }

        /// <summary>
        /// A list of some known <c>binary</c> MIME types.
        /// </summary>
        public static class Binary
        {
            /// <summary>
            /// Gets the value <c>binary/octet-stream</c>.
            /// </summary>
            public const string OctetStream = "binary/octet-stream";
        }

        /// <summary>
        /// A list of some known <c>image</c> MIME types.
        /// </summary>
        public static class Image
        {
            /// <summary>
            /// Gets the value <c>image/bmp</c>.
            /// </summary>
            public const string Bitmap = "image/bmp";

            /// <summary>
            /// Gets the value <c>image/gif</c>.
            /// </summary>
            public const string Gif = "image/gif";

            /// <summary>
            /// Gets the value <c>image/x-icon</c>.
            /// </summary>
            public const string Icon = "image/x-icon";

            /// <summary>
            /// Gets the value <c>image/jpeg</c>.
            /// </summary>
            public const string Jpeg = "image/jpeg";

            /// <summary>
            /// Gets the value <c>image/png</c>.
            /// </summary>
            public const string Png = "image/png";

            /// <summary>
            /// Gets the value <c>image/tiff</c>.
            /// </summary>
            public const string Tiff = "image/tiff";
        }

        /// <summary>
        /// A list of some known <c>text</c> MIME types.
        /// </summary>
        public static class Text
        {
            /// <summary>
            /// Gets the value <c>text/calendar</c>.
            /// </summary>
            public const string Calendar = "text/calendar";

            /// <summary>
            /// Gets the value <c>text/css</c>.
            /// </summary>
            public const string Css = "text/css";

            /// <summary>
            /// Gets the value <c>text/html</c>.
            /// </summary>
            public const string Html = "text/html";

            /// <summary>
            /// Gets the value <c>text/javascript</c>.
            /// </summary>
            public const string Javascript = "text/javascript";

            /// <summary>
            /// Gets the value <c>text/plain</c>.
            /// </summary>
            public const string Plain = "text/plain";

            /// <summary>
            /// Gets the value <c>text/richtext</c>.
            /// </summary>
            public const string Richtext = "text/richtext";

            /// <summary>
            /// Gets the value <c>text/x-vcard</c>.
            /// </summary>
            public const string Vcard = "text/x-vcard";

            /// <summary>
            /// Gets the value <c>text/xml</c>.
            /// </summary>
            public const string Xml = "text/xml";
        }
    }
}
