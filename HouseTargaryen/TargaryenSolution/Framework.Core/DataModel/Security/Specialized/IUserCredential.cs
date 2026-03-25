namespace Framework.Core.DataModel.Security.Specialized
{
    using Framework.Core.DataModel.Security.Proxies;

    /// <summary>
    /// The UserCredential interface.
    /// </summary>
    public interface IUserCredential
    {
        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the matter id.
        /// </summary>
        string MatterId { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether retry client id selection on failure.
        /// </summary>
        bool RetryClientIdSelectionOnFailure { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// The to one pass user info.
        /// </summary>
        /// <returns>
        /// The <see cref="OnePassUserInfo"/>.
        /// </returns>
        OnePassUserInfo ToOnePassUserInfo();
    }
}