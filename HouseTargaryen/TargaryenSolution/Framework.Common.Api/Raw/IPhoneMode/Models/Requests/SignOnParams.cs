namespace Framework.Common.Api.Raw.IPhoneMode.Models.Requests
{
    using System;
    using System.Net;

    using Framework.Common.Api.Raw.IPhoneMode.Utilities.Enums;

    /// <summary>
    /// The sign on parameters.
    /// </summary>
    public class SignOnParams
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignOnParams"/> class.
        /// </summary>
        /// <param name="url"> The URL. </param>
        /// <param name="userId"> The user id. </param>
        /// <param name="password"> The password. </param>
        /// <param name="clientId"> The client id. </param>
        public SignOnParams(string url, string userId, string password, string clientId)
            : this(url, userId, password, clientId, new CobaltApplication?(), false, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignOnParams"/> class.
        /// </summary>
        /// <param name="url"> The URL. </param>
        /// <param name="userId"> The user id. </param>
        /// <param name="password"> The password. </param>
        /// <param name="clientId"> The client id. </param>
        /// <param name="app"> The app. </param>
        /// <param name="bypassClientId"> The bypass client id. </param>
        /// <param name="cookies"> The cookies. </param>
        private SignOnParams(
            string url,
            string userId,
            string password,
            string clientId,
            CobaltApplication? app,
            bool bypassClientId,
            CookieCollection cookies)
        {
            this.Url = url;
            this.Domain = url.Substring(url.IndexOf(".", StringComparison.Ordinal));
            this.Domain = this.Domain.Substring(1, this.Domain.IndexOf(".com", StringComparison.Ordinal) + 3);
            this.UserId = userId;
            this.Password = password;
            this.ClientId = clientId;
            this.Cookies = cookies ?? new CookieCollection();
            this.BypassClientId = bypassClientId;
            CobaltApplication? nullable = app;
            this.CobaltApplication = nullable.HasValue ? nullable.GetValueOrDefault() : CobaltApplication.Website;
        }

        /// <summary>
        /// Gets a value indicating whether bypass client id.
        /// </summary>
        public bool BypassClientId { get; private set; }

        /// <summary>
        /// Gets the client id.
        /// </summary>
        public string ClientId { get; private set; }

        /// <summary>
        /// Gets the cobalt application.
        /// </summary>
        public CobaltApplication CobaltApplication { get; private set; }

        /// <summary>
        /// Gets the cobalt environment.
        /// </summary>
        public CobaltEnvironment CobaltEnvironment { get; private set; }

        /// <summary>
        /// Gets or sets the cookies.
        /// </summary>
        public CookieCollection Cookies { get; set; }

        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Gets the password.
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets the user id.
        /// </summary>
        public string UserId { get; private set; }
    }
}