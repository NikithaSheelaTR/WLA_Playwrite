namespace Framework.Core.DataModel.Security.Proxies
{
    /// <summary>
    /// OnePass user info
    /// </summary>
    public class OnePassUserInfo : IOnePassUserInfo
    {
        /// <summary>
        /// Gets or sets a value indicating whether is disposed.
        /// </summary>
        public bool IsDisposed { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string OnePassEmail { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User GUID
        /// </summary>
        public string PrismGuid { get; set; }

        /// <summary>
        /// Gets the unique key.
        /// </summary>
        public string UniqueKey => this.PrismGuid;

        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose() => this.IsDisposed = true;
    }
}