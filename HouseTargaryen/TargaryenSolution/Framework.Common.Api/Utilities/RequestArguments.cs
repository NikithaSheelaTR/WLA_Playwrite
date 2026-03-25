namespace Framework.Common.Api.Utilities
{
    using System.Collections.Generic;

    using RestSharp;

    /// <summary>
    /// The request arguments.
    /// </summary>
    public class RequestArguments
    {
        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        public Method Method { get; set; }

        /// <summary>
        /// Gets or sets the resource.
        /// </summary>
        public string Resource { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Gets or sets the json source path.
        /// </summary>
        public string JsonSourcePath { get; set; }

        /// <summary>
        /// Gets or sets the headers.
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        public List<Parameter> Parameters { get; set; }

        /// <summary>
        /// Gets or sets the data format.
        /// </summary>
        public DataFormat DataFormat { get; set; } = DataFormat.Xml;
    }
}

