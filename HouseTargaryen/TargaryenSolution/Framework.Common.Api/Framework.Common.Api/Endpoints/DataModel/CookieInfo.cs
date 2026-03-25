namespace Framework.Common.Api.Endpoints.DataModel
{
    /// <summary>
    /// The cookie info.
    /// </summary>
    public struct CookieInfo
    {
        /// <summary>
        /// Gets or sets the web session id.
        /// </summary>
        public string WebSessionId { get; set; }

        /// <summary>
        /// Gets or sets the co session token.
        /// </summary>
        public string CoSessionToken { get; set; }

        /// <summary>
        /// Gets or sets the site specific info.
        /// </summary>
        public string Site { get; set; }
    }
}