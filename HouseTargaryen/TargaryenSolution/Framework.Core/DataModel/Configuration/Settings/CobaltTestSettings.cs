namespace Framework.Core.DataModel.Configuration.Settings
{
    using System;

    using Framework.Core.Cobalt.Passwords;
    using Framework.Core.CommonTypes.Settings;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// A class representing a collection of TestSetting objects used by Cobalt products.
    /// </summary>
    public class CobaltTestSettings : TestSettings
    {
        private string testRunId;

        /// <summary>
        /// Default TestSettings constructor.
        /// </summary>
        public CobaltTestSettings()
        {
            this.KeysClass = this.InitKeys().GetType();
            this.InitSettings();
        }

        /// <summary>
        /// InitKeys
        /// </summary>
        /// <returns>The <see cref="TestSettingKeys"/>.</returns>
        public override TestSettingKeys InitKeys()
        {
            var keys = new CobaltTestSettingKeys();
            keys.InitKeys();
            return keys;
        }

        /// <summary>
        /// Checks out a user if no username/password is currently set and sets the appropriate TestSettings.
        /// The password being checked out will check out using the TestSettings currently set.
        /// </summary>
        /// <param name="testContext">the current test context</param>
        /// <exception cref="NullReferenceException">Thrown if either PasswordPool or PasswordVertical is set to null.</exception>
        public override void CheckOutUser(TestContext testContext)
        {
            string username = this.GetValue<string>(CobaltTestSettingKeys.USER_USERNAME);
            string password = this.GetValue<string>(CobaltTestSettingKeys.USER_PASSWORD);

            if (username == null && password == null)
            {
                string pool = this.GetValue<string>(CobaltTestSettingKeys.USER_PASSWORD_POOL);
                string vertical = this.GetValue<string>(CobaltTestSettingKeys.USER_PASSWORD_VERTICAL);

                // Check if the password pool and vertical are set.
                if ((pool == null) || (vertical == null))
                {
                    throw new NullReferenceException(
                        "Either PasswordPool or PasswordVertical is set to null. You cannot check out a password without first specifying a password pool and vertical.");
                }

                PasswordInfo passwordInfo;
                try
                {
                    // Clear expired passwords.
                    PasswordUtils.ClearExpiredPasswords(testContext, vertical, pool);

                    // Check out a PasswordInfo object.
                    passwordInfo = PasswordUtils.CheckoutPassword(testContext, 30, vertical, pool);
                }
                catch (Exception)
                {
                    throw new Exception("Password check-out error.");
                }

                // If the returned PasswordInfo object is throw an exception because there was an issue.
                if (passwordInfo == null)
                {
                    throw new Exception("No password was found with a verical of " + vertical + " and a pool of " + pool);
                }

                // Set the TestSettings based on the checked out PasswordInfo
                this.SetPasswordInfoSettings(passwordInfo);
            }
        }

        /// <summary>
        /// Checks in the currently checked out user.
        /// If no user is checked out, no action is taken.
        /// </summary>
        /// <param name="testContext">the current test context</param>
        /// <returns>A copy of the current CobaltTestSettings for chaining purposes.</returns>
        public virtual void CheckInUser(TestContext testContext)
        {
            // Checks in the password being used if there is one in use
            if (this.testRunId != null)
            {
                try
                {
                    // Check in the password
                    PasswordUtils.CheckinPasswords(testContext);

                    // Reset the relevant user settings
                    this.ResetValue(CobaltTestSettingKeys.USER_EMAIL);
                    this.ResetValue(CobaltTestSettingKeys.USER_FIRST_NAME);
                    this.ResetValue(CobaltTestSettingKeys.USER_LAST_NAME);
                    this.ResetValue(CobaltTestSettingKeys.USER_USERNAME);
                    this.ResetValue(CobaltTestSettingKeys.USER_PASSWORD);
                    this.ResetValue(CobaltTestSettingKeys.USER_PRISM_GUID);
                    this.ResetValue(CobaltTestSettingKeys.USER_PRISM_USERNAME);
                    this.ResetValue(CobaltTestSettingKeys.USER_PRISM_PASSWORD);
                    this.ResetValue(CobaltTestSettingKeys.USER_GMAIL);
                    this.ResetValue(CobaltTestSettingKeys.USER_GMAIL_PASSWORD);
                }
                catch (Exception)
                {
                    throw new Exception("Password check-in error.");
                }
            }
        }

        /// <summary>
        /// Initializes the specified user's TestSettings based on the PasswordInfo object associated with the user.
        /// </summary>
        /// <param name="username">The username for the user being initialized.</param>
        /// <returns>A copy of the current CobaltTestSettings for chaining purposes.</returns>
        /// <exception cref="NullReferenceException">Thrown if the specified username is null.</exception>
        public virtual void InitUser(string username)
        {
            if (username == null)
            {
                throw new NullReferenceException("The username cannot be null.");
            }
            this.SetValue(CobaltTestSettingKeys.USER_USERNAME, username);
            this.InitUser();
        }

        /// <summary>
        /// Initializes the current user's TestSettings based on the PasswordInfo object associated with the user.
        /// </summary>
        /// <returns>A copy of the current CobaltTestSettings for chaining purposes.</returns>
        /// /// <exception cref="NullReferenceException">Thrown if no username has been set.</exception>
        public virtual void InitUser()
        {
            // Check if the username is set
            string username = this.GetValue<string>(CobaltTestSettingKeys.USER_USERNAME);
            if (username == null)
            {
                throw new NullReferenceException(
                    "Username is null. You cannot get user information without setting a user first.");
            }

            PasswordInfo passwordInfo;
            try
            {
                passwordInfo = PasswordUtils.GetPasswordInfo(PasswordUtils.PasswordDatabaseEnvironment.Production, username);
            }
            catch (Exception)
            {
                throw new Exception("Error retrieving user information for " + username + ".");
            }

            // If the returned PasswordInfo object is throw an exception because there was an issue.
            if (passwordInfo == null)
            {
                throw new Exception("No password was found with the username " + username + ".");
            }

            // Set the TestSettings based on the checked out PasswordInfo
            this.SetPasswordInfoSettings(passwordInfo);
        }

        /// <summary>
        /// SetPasswordInfoSettings
        /// </summary>
        /// <param name="passwordInfo">The password Info.</param>
        public virtual void SetPasswordInfoSettings(PasswordInfo passwordInfo)
        {
            // If a value has been explicitly set, that takes priority. If not, use the checked out value.
            this.SetValueIfNotSet(CobaltTestSettingKeys.USER_EMAIL, passwordInfo.Email);
            this.SetValueIfNotSet(CobaltTestSettingKeys.USER_FIRST_NAME, passwordInfo.FirstName);
            this.SetValueIfNotSet(CobaltTestSettingKeys.USER_LAST_NAME, passwordInfo.LastName);
            this.SetValueIfNotSet(CobaltTestSettingKeys.USER_USERNAME, passwordInfo.OnePassUsername);
            this.SetValueIfNotSet(CobaltTestSettingKeys.USER_PASSWORD, passwordInfo.OnePassPassword);
            this.SetValueIfNotSet(CobaltTestSettingKeys.USER_PRISM_GUID, passwordInfo.PrismGuid);
            this.SetValueIfNotSet(CobaltTestSettingKeys.USER_PRISM_USERNAME, passwordInfo.PrismUsername);
            this.SetValueIfNotSet(CobaltTestSettingKeys.USER_PRISM_PASSWORD, passwordInfo.PrismPassword);
            this.SetValueIfNotSet(CobaltTestSettingKeys.USER_GMAIL, passwordInfo.GmailUsername);
            this.SetValueIfNotSet(CobaltTestSettingKeys.USER_GMAIL_PASSWORD, passwordInfo.GmailPassword);
            this.testRunId = passwordInfo.TestRunId;
        }
    }
}
