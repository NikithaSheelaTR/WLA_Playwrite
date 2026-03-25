namespace Framework.Core.DataModel.Security.Specialized
{
    using System;
    using System.Data.SqlClient;
    using System.Threading;

    using Framework.Core.Cobalt.Passwords;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils;
    using Framework.Core.Utils.Execution;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using PasswordPoolEnum = Framework.Core.CommonTypes.Configuration.PasswordPool;

    /// <summary>
    /// Contains all credential/log in related information for a test.
    /// An account from the specified password vertical and password pool will be reserved in the password pool database for exclusive use.
    /// </summary>
    public sealed class UserDbCredential : IOnePassUserInfo, IUserCredential, IDisposable
    {
        private const string DefaultClientId = "WlnRegressionTest";

        private const string FedRampSuffix = "_DCExit";

        private const int QrtPasswordsMinutesPwdExpires = 20;

        private readonly TestContext testContext;

        private readonly bool IsFedRampCredential;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDbCredential"/> class.
        /// </summary>
        /// <param name="testContext">
        /// The test Context.
        /// </param>
        /// <param name="passwordVertical">
        /// The password Vertical.
        /// </param>
        /// <param name="passwordPool">
        /// The password Pool.
        /// </param>
        public UserDbCredential(TestContext testContext, PasswordVertical? passwordVertical, string passwordPool)
        {
            string generalPurposePool =
                PasswordPoolEnum.WlnrGeneralPurposePreProdPool.GetEnumTextValue();
            this.ClientId = string.Equals(passwordPool, generalPurposePool, StringComparison.InvariantCultureIgnoreCase)
                                ? DefaultClientId
                                : passwordPool;
            this.PasswordInfo = new PasswordInfo();
            this.PasswordVertical = passwordVertical;
            this.IsFedRampCredential = !TestConfigurationRepository.IsDefaultInstance;
            this.testContext = testContext;
            this.PasswordPool = passwordPool;
            this.CheckoutCredentials();
            this.Password = this.PasswordInfo.OnePassPassword;
            this.PrismGuid = this.PasswordInfo.PrismGuid;
            this.RetryClientIdSelectionOnFailure = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDbCredential"/> class.
        /// An account with the specified name will be returned without reservation in the password pool database.
        /// Leave the ClientId for user empty. WlnSignonManager will expect HomePage after OnepassPage
        /// </summary>
        /// <param name="userName">The Name of user to get from production pool</param>
        public UserDbCredential(string userName)
            : this(userName, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDbCredential"/> class.
        /// An account with the specified name will be returned without reservation in the password pool database.
        /// </summary>
        /// <param name="userName">
        /// The Name of user to get
        /// </param>
        /// <param name="clientId">
        /// The client Id.
        /// </param>
        public UserDbCredential(string userName, string clientId)
        {
            this.PasswordInfo = PasswordUtils.GetPasswordInfo(
                PasswordUtils.PasswordDatabaseEnvironment.Production,
                userName);
            this.UserName = userName;
            this.Password = this.PasswordInfo.OnePassPassword;
            this.PrismGuid = this.PasswordInfo.PrismGuid;
            this.PasswordInfo.CheckoutDateTime = null;
            this.RetryClientIdSelectionOnFailure = false;
            this.ClientId = clientId;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="UserDbCredential"/> class. 
        /// </summary>
        ~UserDbCredential()
        {
            this.DisposeInternal();
        }

        /// <summary>
        /// Gets or sets the billing type.
        /// </summary>
        public string BillingType { get; set; }

        /// <summary>
        /// The client id to used when signing in
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets a value indicating whether is disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Text denotes Credential for Fedramp
        /// </summary>
        public string FedRampVerticalAndPoolText
        {
            get
            {
                if (this.IsFedRampCredential)
                {
                    return FedRampSuffix;
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets last name
        /// </summary>
        public string LastName => this.PasswordInfo.LastName;

        /// <summary>
        /// Gets or sets the matter id.
        /// </summary>
        public string MatterId { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The PasswordInfo object from QualityUtilities that contains the password info from the password pool
        /// TODO should be private
        /// </summary>
        public PasswordInfo PasswordInfo { get; private set; }

        /// <summary>
        ///  The pool to use when getting a password from the password tool
        /// </summary>
        public string PasswordPool { get; set; }

        /// <summary>
        /// The vertical to use when getting a password from the password tool
        /// </summary>
        public PasswordVertical? PasswordVertical { get; set; }

        /// <summary>
        /// Gets or sets the prism GUID.
        /// </summary>
        public string PrismGuid { get; set; }

        /// <summary>
        /// Set to true if the test should not fail immediately on a client id error, and should instead try again.
        /// </summary>
        public bool RetryClientIdSelectionOnFailure { get; set; }

        /// <summary>
        /// Gets the unique key.
        /// </summary>
        public string UniqueKey => this.PrismGuid;

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName
        {
            get
            {
                return this.PasswordInfo.OnePassUsername;
            }

            set
            {
                this.PasswordInfo.OnePassUsername = value;
            }
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.DisposeInternal();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The to one pass user info.
        /// </summary>
        /// <returns>The <see cref="OnePassUserInfo"/>.</returns>
        public OnePassUserInfo ToOnePassUserInfo() =>
            new OnePassUserInfo
            {
                UserName = this.PasswordInfo.OnePassUsername,
                Password = this.PasswordInfo.OnePassPassword,
                PrismGuid = this.PasswordInfo.PrismGuid,
                OnePassEmail = this.PasswordInfo.Email
            };

        /// <summary>
        /// The to patron user info.
        /// </summary>
        /// <returns> The <see cref="PatronUserInfo"/>. </returns>
        public PatronUserInfo ToPatronUserInfo() =>
            new PatronUserInfo
            {
                UserName = this.UserName,
                SecretCode = this.Password,
                ClientId = this.ClientId,
                FirstName = this.PasswordInfo.FirstName,
                LastName = this.LastName,
                Email = this.PasswordInfo.Email,
                PatronUserName = this.PasswordInfo.GmailUsername
            };

        /// <summary>
        /// Converts the current instance to <see cref="WlnUserInfo"/>.
        /// </summary>
        /// <returns> The <see cref="WlnUserInfo"/>.</returns>
        public WlnUserInfo ToWlnUserInfo() =>
            new WlnUserInfo
            {
                UserName = this.UserName,
                BillingType = this.BillingType,
                ClientId = this.ClientId,
                MatterId = this.MatterId,
                Password = this.Password,
                RetryClientIdSelectionOnFailure = this.RetryClientIdSelectionOnFailure,
                FirstName = this.PasswordInfo.FirstName,
                LastName = this.PasswordInfo.LastName,
                IsDisposed = this.IsDisposed,
                PrismGuid = this.PrismGuid,
                Email = this.PasswordInfo.Email
            };

        /// <summary>
        /// Checks a password into the Password tool
        /// </summary>
        private void CheckinCredentials()
        {
            if (this.PasswordInfo.CheckoutDateTime != null)
            {
                PasswordUtils.CheckinPasswords(this.testContext);
            }
        }

        /// <summary>
        /// The checkout credentials.
        /// </summary>
        private void CheckoutCredentials()
        {
            if (this.PasswordInfo != null && this.PasswordInfo.OnePassUsername == null)
            {
                Func<PasswordInfo> pwdInfoRetriever =
                    () =>
                        PasswordUtils.CheckoutPassword(
                            this.testContext,
                            QrtPasswordsMinutesPwdExpires,
                            $"{this.PasswordVertical?.GetEnumTextValue()}{this.FedRampVerticalAndPoolText}",
                            $"{this.PasswordPool}{this.FedRampVerticalAndPoolText}");

                SafeMethodExecutor.Execute(
                    () =>
                        PasswordUtils.ClearExpiredPasswords(
                            this.testContext,
                            this.PasswordVertical?.GetEnumTextValue(),
                            this.PasswordPool)).LogDetails();

                const int Timeout = 30000;
                const int MaxAttempts = 5;

                for (int i = 0; i < MaxAttempts; i++)
                {
                    try
                    {
                        this.PasswordInfo = pwdInfoRetriever();
                        i = MaxAttempts;
                    }
                    catch (SqlException e)
                    {
                        Logger.LogError(
                            "Unable to checkout password, due to SQL Exception: {0}\n. Retrying in {1} seconds.",
                            e.Message,
                            Timeout / 1000);
                        Thread.Sleep(Timeout);
                    }
                }

                if (this.PasswordInfo == null)
                {
                    Logger.LogError(
                         "Error checking out password from password tool - no passwords were available"
                         + Environment.NewLine);
                    throw new InvalidOperationException(
                        "No password has been checked out. There may not be any available passwords at the moment.");
                }
            }
        }

        /// <summary>
        /// The dispose internal.
        /// </summary>
        private void DisposeInternal()
        {
            if (!this.IsDisposed)
            {
                SafeMethodExecutor.Execute(this.CheckinCredentials).LogDetails();
                this.IsDisposed = true;
            }
        }
    }
}