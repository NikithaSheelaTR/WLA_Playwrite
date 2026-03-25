namespace Framework.Core.DataModel.Security.Proxies
{
    /// <summary>
    /// The patron user name settings.
    /// </summary>
    public class PatronUserInfo : OnePassUserInfo
    {
        private string email;

        private string firstName;

        private string lastName;

        private string patronUserName;

        private string secretCode;

        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email
        {
            get
            {
                return this.email;
            }

            set
            {
                this.email = value == "null" ? string.Empty : value;
            }
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName
        {
            get
            {
                return this.firstName;
            }

            set
            {
                this.firstName = value == "null" ? string.Empty : value;
            }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName
        {
            get
            {
                return this.lastName;
            }

            set
            {
                this.lastName = value == "null" ? string.Empty : value;
            }
        }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string PatronUserName
        {
            get
            {
                return this.patronUserName;
            }

            set
            {
                this.patronUserName = value == "null" ? string.Empty : value;
            }
        }

        /// <summary>
        /// Gets or sets the secret code.
        /// </summary>
        public string SecretCode
        {
            get
            {
                return this.secretCode;
            }

            set
            {
                this.secretCode = value == "null" ? string.Empty : value;
            }
        }
    }
}