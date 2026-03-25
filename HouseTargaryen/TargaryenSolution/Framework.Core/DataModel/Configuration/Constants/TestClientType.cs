namespace Framework.Core.DataModel.Configuration.Constants
{
    /// <summary>
    /// Represents the type of a test client <see cref="TestClientId"/>.
    /// </summary>
    public enum TestClientType : byte
    {
        /// <summary>
        /// A stub type of an entity which is not a test client or whose client identity is not significant.
        /// </summary>
        Undefined,

        /// <summary>
        /// A browser test client mediator.
        /// </summary>
        Browser,

        /// <summary>
        /// An API client test mediator.
        /// </summary>
        ApiClient,

        /// <summary>
        /// A desktop client test mediator.
        /// </summary>
        DesktopClient
    }
}