namespace Framework.Core.DataModel.Configuration.Constants
{
    /// <summary>
    /// Represents the type of a test client family.
    /// </summary>
    public enum TestClientFamily : byte
    {
        /// <summary>
        /// A stub family of an entity which is not a test client or whose client identity is not significant.
        /// </summary>
        Undefined,

        /// <summary>
        /// An Internet Explorer test client family.
        /// </summary>
        InternetExplorer,

        /// <summary>
        /// A Google Chrome test client family.
        /// </summary>
        Chrome,

        /// <summary>
        /// A Mozilla Firefox test client family.
        /// </summary>
        Firefox,

        /// <summary>
        /// An Apple Safari test client family.
        /// </summary>
        Safari,

        /// <summary>
        /// An API test client family.
        /// </summary>
        Api,

        /// <summary>
        /// A Coded UI test client family.
        /// </summary>
        CodedUi,

        /// <summary>
        /// A Microsoft Edge test client family.
        /// </summary>
        MicrosoftEdge
    }
}