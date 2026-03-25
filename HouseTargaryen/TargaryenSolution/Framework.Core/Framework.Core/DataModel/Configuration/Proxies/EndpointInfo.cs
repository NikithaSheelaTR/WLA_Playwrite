namespace Framework.Core.DataModel.Configuration.Proxies
{
    /// <summary>
    /// Represents an endpoint to a product or module under test.
    /// </summary>
    public sealed class EndpointInfo
    {
        /// <summary>
        /// Gets the environment.
        /// </summary>
        public EnvironmentInfo Environment { get; internal set; }

        /// <summary>
        /// Gets the module.
        /// </summary>
        public CobaltModuleInfo Module { get; internal set; }

        /// <summary>
        /// Gets the product.
        /// </summary>
        public CobaltProductInfo Product { get; internal set; }

        /// <summary>
        /// Gets the URI to a product or module.
        /// </summary>
        public string Uri { get; internal set; }
    }
}