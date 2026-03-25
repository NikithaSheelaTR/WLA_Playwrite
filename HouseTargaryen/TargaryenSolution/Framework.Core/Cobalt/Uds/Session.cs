namespace Framework.Core.Cobalt.Uds
{
    using System;
    using System.IO;
    using System.Text;

    using Framework.Core.Utils;
    using Newtonsoft.Json;



    /// <summary>
    /// Stores information regarding a UDS Session.
    /// </summary>
    public sealed class Session
    {
        /// <summary>
        /// Gets or sets the UDS Session identifier.
        /// </summary>
        /// <value>The UDS Session identifier.</value>
        public string SessionId { get; set; }

        /// <summary>
        /// Gets or sets the Prism GUID associated with owner of the UDS Session.
        /// </summary>
        /// <value>The Prism GUID associated with owner of the UDS Session.</value>
        public string PrismGuid { get; set; }

        /// <summary>
        /// Gets or sets the site on which the UDS Session has been created.
        /// </summary>
        /// <value>The site on which the UDS Session has been created.</value>
        public string Site { get; set; }

        /// <summary>
        /// Gets or sets the status of the UDS Session.
        /// </summary>
        /// <value>The status of the UDS Session.</value>
        public Status? SessionStatus { get; set; }

        /// <summary>
        /// Gets or sets the long token for the UDS Session.
        /// </summary>
        /// <value>The long token for the UDS Session.</value>
        public string LongToken { get; set; }

        /// <summary>
        /// Gets or sets the seamless authentication token for the UDS Session.
        /// </summary>
        /// <value>The seamless authentication token for the UDS Session.</value>
        public string SeamlessAuthenticationToken { get; set; }

        /// <summary>
        /// Gets or sets the expiration reason for the UDS Session.
        /// </summary>
        /// <value>The expiration reason for the UDS Session.</value>
        public Reason? ExpiresReason { get; set; }

        /// <summary>
        /// Gets or sets the session ended reason for the UDS Session.
        /// </summary>
        /// <value>The session ended reason for the UDS Session.</value>
        public Reason? SessionEndedReason { get; set; }

        /// <summary>
        /// Gets or sets the user classification for the UDS Session.
        /// </summary>
        /// <value>The user classification for the UDS Session.</value>
        public string UserClassification { get; set; }

        /// <summary>
        /// Gets or sets the first name of the owner of the UDS Session.
        /// </summary>
        /// <value>The first name of the owner of the UDS Session.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the owner of the UDS Session.
        /// </summary>
        /// <value>The last name of the owner of the UDS Session.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the owner of the UDS Session.
        /// </summary>
        /// <value>The email address of the owner of the UDS Session.</value>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the OnePass user name of the owner of the UDS Session.
        /// </summary>
        /// <value>The OnePass user name of the owner of the UDS Session.</value>
        public string OnePassUserName { get; set; }

        /// <summary>
        /// Gets or sets the date and time the UDS Session was created.
        /// </summary>
        /// <value>The date and time the UDS Session was created.</value>
        public DateTime? CreatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the date/time the UDS Session expired.
        /// </summary>
        /// <value>The date and time the UDS Session expired.</value>
        public DateTime? ExpiresDateTime { get; set; }

        /// <summary>
        /// Gets or sets the orphan expires date/time of the UDS Session.
        /// </summary>
        /// <value>The orphan expires date and time of the UDS Session.</value>
        public DateTime? OrphanExpiresDateTime { get; set; }

        /// <summary>
        /// Gets or sets the date/time the UDS Session ended.
        /// </summary>
        /// <value>The date and time the UDS Session ended.</value>
        public DateTime? SessionEndedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the date/time the UDS Session expired.
        /// </summary>
        /// <value>The date/time the UDS Session expired.</value>
        public DateTime? SessionExpiresDateTime { get; set; }

        /// <summary>
        /// Gets or sets the WestlawNext product for which the UDS Session was created.
        /// </summary>
        /// <value>The WestlawNext product for which the UDS Session was created.</value>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the Prism GUID of the user whose UDS Session is being emulated by this UDS Session.
        /// </summary>
        /// <value>The Prism GUID of the user whose UDS Session is being emulated by this UDS Session.</value>
        public string EmulateePrismGuid { get; set; }

        /// <summary>
        /// Gets or sets the Prism authentication token of the user whose UDS Session is being emulated by this UDS Session.
        /// </summary>
        /// <value>The Prism authentication token of the user whose UDS Session is being emulated by this UDS Session.</value>
        public string EmulateePrismAuthToken { get; set; }

        /// <summary>
        /// Gets or sets the Prism authentication token of the owner of the UDS Session.
        /// </summary>
        /// <value>The Prism authentication token of the owner of the UDS Session.</value>
        public string PrismAuthToken { get; set; }

        /// <summary>
        /// Gets or sets the tier associated with the owner of the UDS Session.
        /// </summary>
        /// <value>The tier associated with the owner of the UDS Session.</value>
        public int? Tier { get; set; }

        /// <summary>
        /// Gets or sets an indication whether the preferences for the owner of the UDS Session are specific to this session.
        /// </summary>
        /// <value>An indication whether the preferences for the owner of the UDS Session are specific to this session.</value>
        public bool? SessionBasedPreferences { get; set; }

        /// <summary>
        /// Gets or sets the PMD data version associated with the UDS Session.
        /// </summary>
        /// <value>The PMD data version associated with the UDS Session.</value>
        public string PmdDataVersion { get; set; }

        /// <summary>
        /// Gets or sets the source of the UDS Session.
        /// </summary>
        /// <value>The source of the UDS Session.</value>
        public string SessionSource { get; set; }

        /// <summary>
        /// Gets or sets the service type of the UDS Session.
        /// </summary>
        /// <value>The service type of the UDS Session.</value>
        public string ServiceType { get; set; }

        /// <summary>
        /// Gets or sets the Prism registration key of the UDS Session.
        /// </summary>
        /// <value>The Prism registration key of the UDS Session.</value>
        public string PrismRegistrationKey { get; set; }

        /// <summary>
        /// Gets or sets the payment type of the UDS Session.
        /// </summary>
        /// <value>The payment type of the UDS Session.</value>
        public PaymentType? SessionPaymentType { get; set; }

        /// <summary>
        /// Gets or sets the OnePass product name of the UDS Session.
        /// </summary>
        /// <value>The OnePass product name of the UDS Session.</value>
        public string OnePassProductName { get; set; }

        /// <summary>
        /// Gets or sets the Product View of the current UDS Session
        /// </summary>
        public string ProductView { get; set; }

        /// <summary>
        /// Gets or sets the IP Address of the UDS session
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the User Category of the UDS Session
        /// </summary>
        public string UserCategory { get; set; }

        /// <summary>
        /// Gets or sets the Integration Info of the UDS Session
        /// </summary>
        public string IntegrationInfo { get; set; }

        /// <summary>
        /// Gets or sets the BillingMethod of the UDS Session
        /// </summary>
        public string BillingMethod { get; set; }

        /// <summary>
        /// Enumerates all valid UDS Session statuses.
        /// </summary>
        public enum Status
        {
            /// <summary>
            /// Indicates a status of not set.
            /// </summary>
            Notset,

            /// <summary>
            /// Indicates a status of online.
            /// </summary>
            Online,

            /// <summary>
            /// Indicates a status of offline.
            /// </summary>
            Offline,

            /// <summary>
            /// Indicates a status of killed.
            /// </summary>
            Killed,

            /// <summary>
            /// Indicates a status of authenticated.
            /// </summary>
            Authenticated,

            /// <summary>
            /// Indicates a status of authenticated stale.
            /// </summary>
            AuthenticatedStale,

            /// <summary>
            /// Indicates a status of offline stale.
            /// </summary>
            Offlinestale,

            /// <summary>
            /// Indicates a status of system online.
            /// </summary>
            SystemOnline
        }

        /// <summary>
        /// Enumerates all valid reasons a UDS Session <see cref="Reason"/> has changed.
        /// </summary>
        public enum Reason
        {
            /// <summary>
            /// Indicates a reason of not set.
            /// </summary>
            Notset,

            /// <summary>
            /// Indicates a reason of concurrent users.
            /// </summary>
            Concurrent_Users,

            /// <summary>
            /// Indicates a reason of customer support.
            /// </summary>
            Customer_Support,

            /// <summary>
            /// Indicates a reason of SSO token expiration.
            /// </summary>
            Sso_Token_Expired,

            /// <summary>
            /// Indicates a reason of user signed off.
            /// </summary>
            User_Signed_Off,

            /// <summary>
            /// Indicates a reason of user inactivity.
            /// </summary>
            User_Inactivity,

            /// <summary>
            /// Indicates a reason of no active browser.
            /// </summary>
            No_Active_Browser,

            /// <summary>
            /// Indicates a reason of maintenance.
            /// </summary>
            Maintenance,

            /// <summary>
            /// Indicates a reason of incomplete signon.
            /// </summary>
            Incomplete_Signon,

            /// <summary>
            /// Indicates a reason of user throttling.
            /// </summary>
            User_Throttling,

            /// <summary>
            /// Indicates a reason of targeted user.
            /// </summary>
            Targeted_User
        }

        /// <summary>
        /// Enumerates all valid payment types for a UDS Session.
        /// </summary>
        public enum PaymentType
        {
            /// <summary>
            /// Indicates that standard billing applies.
            /// </summary>
            StandardBilling,

            /// <summary>
            /// Indicates that credit card billing applies.
            /// </summary>
            CreditCardBilling,

            /// <summary>
            /// Indicates that Indigo billing applies.
            /// </summary>
            IndigoBilling
        }

        /// <summary>
        /// Determines whether the specified <see cref="Session"/> is equal to the current <see cref="Session"/>.
        /// </summary>
        /// <param name="aThat">The <see cref="Session"/> to compare with the current <see cref="Session"/>.</param>
        /// <returns><b>true</b> if the specified Object is equal to the current <see cref="Session"/>; otherwise, <b>false</b>.</returns>
        public override bool Equals(object aThat)
        {
            if (aThat == null || this.GetType() != aThat.GetType())
            {
                return false;
            }

            var that = (Session)aThat;
            return EqualsUtils.AreEqual(this.SessionId, that.SessionId)
                   && EqualsUtils.AreEqual(this.PrismGuid, that.PrismGuid)
                   && EqualsUtils.AreEqual(this.Site, that.Site)
                   && EqualsUtils.AreEqual(this.SessionStatus, that.SessionStatus)
                   && EqualsUtils.AreEqual(this.LongToken, that.LongToken)
                   && EqualsUtils.AreEqual(this.SeamlessAuthenticationToken, that.SeamlessAuthenticationToken)
                   && EqualsUtils.AreEqual(this.ExpiresReason, that.ExpiresReason)
                   && EqualsUtils.AreEqual(this.SessionEndedReason, that.SessionEndedReason)
                   && EqualsUtils.AreEqual(this.UserClassification, that.UserClassification)
                   && EqualsUtils.AreEqual(this.FirstName, that.FirstName)
                   && EqualsUtils.AreEqual(this.LastName, that.LastName)
                   && EqualsUtils.AreEqual(this.EmailAddress, that.EmailAddress)
                   && EqualsUtils.AreEqual(this.OnePassUserName, that.OnePassUserName)
                   && Session.DateTimesEqual(this.CreatedDateTime, that.CreatedDateTime)
                   && Session.DateTimesEqual(this.ExpiresDateTime, that.ExpiresDateTime)
                   && Session.DateTimesEqual(this.OrphanExpiresDateTime, that.OrphanExpiresDateTime)
                   && Session.DateTimesEqual(this.SessionEndedDateTime, that.SessionEndedDateTime)
                   && Session.DateTimesEqual(this.SessionExpiresDateTime, that.SessionExpiresDateTime)
                   && EqualsUtils.AreEqual(this.ProductName, that.ProductName)
                   && EqualsUtils.AreEqual(this.EmulateePrismGuid, that.EmulateePrismGuid)
                   && EqualsUtils.AreEqual(this.EmulateePrismAuthToken, that.EmulateePrismAuthToken)
                   && EqualsUtils.AreEqual(this.PrismAuthToken, that.PrismAuthToken)
                   && EqualsUtils.AreEqual(this.Tier, that.Tier)
                   && EqualsUtils.AreEqual(this.SessionBasedPreferences, that.SessionBasedPreferences)
                   && EqualsUtils.AreEqual(this.PmdDataVersion, that.PmdDataVersion)
                   && EqualsUtils.AreEqual(this.SessionSource, that.SessionSource)
                   && EqualsUtils.AreEqual(this.ServiceType, that.ServiceType)
                   && EqualsUtils.AreEqual(this.PrismRegistrationKey, that.PrismRegistrationKey)
                   && EqualsUtils.AreEqual(this.SessionPaymentType, that.SessionPaymentType)
                   && EqualsUtils.AreEqual(this.OnePassProductName, that.OnePassProductName)
                   && EqualsUtils.AreEqual(this.ProductView, that.ProductView)
                   && EqualsUtils.AreEqual(this.IpAddress, that.IpAddress)
                   && EqualsUtils.AreEqual(this.UserCategory, that.UserCategory);
        }

        /// <summary>
        /// Determines whether two <see cref="DateTime"/> objects are equal down to thousandths of a second.
        /// </summary>
        /// <param name="aThis">A <see cref="DateTime"/>.</param>
        /// <param name="aThat">A second <see cref="DateTime"/> to compare against<c>aThis</c>.</param>
        /// <returns>An indication whether the two <see cref="DateTime"/> objects are equal down to thousandths of a second.</returns>
        /// <remarks>Equality is determined in this manner as UDS only stores <see cref="DateTime"/> objects to the thousandth of a second.</remarks>
        private static bool DateTimesEqual(DateTime? aThis, DateTime? aThat)
        {
            // determine if both are valued or both are null
            if ((aThis == null) != (aThat == null))
            {
                return false;
            }

            // determine if both are the same object
            if (!object.ReferenceEquals(aThis, aThat))
            {
                return EqualsUtils.AreEqual(
                    aThis.Value.ToUniversalTime().ToString("MM/dd/yyy hh:mm:ss.fff"),
                    aThat.Value.ToUniversalTime().ToString("MM/dd/yyy hh:mm:ss.fff"));
            }

            return true;
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="Session"/>.</returns>
        public override int GetHashCode()
        {
            int result = HashCodeUtils.Seed;
            result = HashCodeUtils.Hash(result, this.SessionId);
            result = HashCodeUtils.Hash(result, this.PrismGuid);
            result = HashCodeUtils.Hash(result, this.Site);
            result = HashCodeUtils.Hash(result, this.SessionStatus);
            result = HashCodeUtils.Hash(result, this.LongToken);
            result = HashCodeUtils.Hash(result, this.SeamlessAuthenticationToken);
            result = HashCodeUtils.Hash(result, this.ExpiresReason);
            result = HashCodeUtils.Hash(result, this.SessionEndedReason);
            result = HashCodeUtils.Hash(result, this.UserClassification);
            result = HashCodeUtils.Hash(result, this.FirstName);
            result = HashCodeUtils.Hash(result, this.LastName);
            result = HashCodeUtils.Hash(result, this.EmailAddress);
            result = HashCodeUtils.Hash(result, this.OnePassUserName);
            result = HashCodeUtils.Hash(result, this.CreatedDateTime);
            result = HashCodeUtils.Hash(result, this.ExpiresDateTime);
            result = HashCodeUtils.Hash(result, this.OrphanExpiresDateTime);
            result = HashCodeUtils.Hash(result, this.SessionEndedDateTime);
            result = HashCodeUtils.Hash(result, this.SessionExpiresDateTime);
            result = HashCodeUtils.Hash(result, this.ProductName);
            result = HashCodeUtils.Hash(result, this.EmulateePrismGuid);
            result = HashCodeUtils.Hash(result, this.EmulateePrismAuthToken);
            result = HashCodeUtils.Hash(result, this.PrismAuthToken);
            result = HashCodeUtils.Hash(result, this.Tier);
            result = HashCodeUtils.Hash(result, this.SessionBasedPreferences);
            result = HashCodeUtils.Hash(result, this.PmdDataVersion);
            result = HashCodeUtils.Hash(result, this.SessionSource);
            result = HashCodeUtils.Hash(result, this.ServiceType);
            result = HashCodeUtils.Hash(result, this.PrismRegistrationKey);
            result = HashCodeUtils.Hash(result, this.SessionPaymentType);
            result = HashCodeUtils.Hash(result, this.OnePassProductName);
            result = HashCodeUtils.Hash(result, this.ProductView);
            result = HashCodeUtils.Hash(result, this.IpAddress);
            result = HashCodeUtils.Hash(result, this.UserCategory);
            return result;
        }

        /// <summary>
        /// Returns a string that represents the current <see cref="Session"/>.
        /// </summary>
        /// <returns>A string that represents the current <see cref="Session"/>.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            using (var jsonWriter = new JsonTextWriter(new StringWriter(sb)))
            {
                jsonWriter.Formatting = Formatting.Indented;
                jsonWriter.WriteStartObject();
                jsonWriter.WritePropertyName("SessionId");
                jsonWriter.WriteValue(this.SessionId);
                jsonWriter.WritePropertyName("PrismGuid");
                jsonWriter.WriteValue(this.PrismGuid);
                jsonWriter.WritePropertyName("PrismAuthToken");
                jsonWriter.WriteValue(this.PrismAuthToken);
                jsonWriter.WritePropertyName("Site");
                jsonWriter.WriteValue(this.Site);
                jsonWriter.WritePropertyName("Status");
                jsonWriter.WriteValue((this.SessionStatus == null) ? null : this.SessionStatus.ToString());
                jsonWriter.WritePropertyName("ExpiresReason");
                jsonWriter.WriteValue((this.ExpiresReason == null) ? null : this.ExpiresReason.ToString());
                jsonWriter.WritePropertyName("UserClassification");
                jsonWriter.WriteValue(this.UserClassification);
                jsonWriter.WritePropertyName("FirstName");
                jsonWriter.WriteValue(this.FirstName);
                jsonWriter.WritePropertyName("LastName");
                jsonWriter.WriteValue(this.LastName);
                jsonWriter.WritePropertyName("EmailAddress");
                jsonWriter.WriteValue(this.EmailAddress);
                jsonWriter.WritePropertyName("OnePassUserName");
                jsonWriter.WriteValue(this.OnePassUserName);
                jsonWriter.WritePropertyName("SessionExpiresDateTime");
                jsonWriter.WriteValue((this.SessionExpiresDateTime == null) ? null : ((DateTime)this.SessionExpiresDateTime).ToUniversalTime().ToString("o"));
                jsonWriter.WritePropertyName("OrphanExpiresDateTime");
                jsonWriter.WriteValue((this.OrphanExpiresDateTime == null) ? null : ((DateTime)this.OrphanExpiresDateTime).ToUniversalTime().ToString("o"));
                jsonWriter.WritePropertyName("ProductName");
                jsonWriter.WriteValue(this.ProductName);
                jsonWriter.WritePropertyName("Tier");
                jsonWriter.WriteValue(this.Tier);
                jsonWriter.WritePropertyName("SessionBasedPreferences");
                jsonWriter.WriteValue(this.SessionBasedPreferences);
                jsonWriter.WritePropertyName("PmdDataVersion");
                jsonWriter.WriteValue(this.PmdDataVersion);
                jsonWriter.WritePropertyName("LongToken");
                jsonWriter.WriteValue(this.LongToken);
                jsonWriter.WritePropertyName("CreatedDateTime");
                jsonWriter.WriteValue((this.CreatedDateTime == null) ? null : ((DateTime)this.CreatedDateTime).ToUniversalTime().ToString("o"));
                jsonWriter.WritePropertyName("ExpiresDateTime");
                jsonWriter.WriteValue((this.ExpiresDateTime == null) ? null : ((DateTime)this.ExpiresDateTime).ToUniversalTime().ToString("o"));
                jsonWriter.WritePropertyName("SessionEndedDateTime");
                jsonWriter.WriteValue((this.SessionEndedDateTime == null) ? null : ((DateTime)this.SessionEndedDateTime).ToUniversalTime().ToString("o"));
                jsonWriter.WritePropertyName("SessionEndedReason");
                jsonWriter.WriteValue((this.SessionEndedReason == null) ? null : this.SessionEndedReason.ToString());
                jsonWriter.WritePropertyName("SeamlessAuthenticationToken");
                jsonWriter.WriteValue(this.SeamlessAuthenticationToken);
                jsonWriter.WritePropertyName("SessionSource");
                jsonWriter.WriteValue(this.SessionSource);
                jsonWriter.WritePropertyName("ServiceType");
                jsonWriter.WriteValue(this.ServiceType);
                jsonWriter.WritePropertyName("EmulateePrismGuid");
                jsonWriter.WriteValue(this.EmulateePrismGuid);
                jsonWriter.WritePropertyName("EmulateePrismAuthToken");
                jsonWriter.WriteValue(this.EmulateePrismAuthToken);
                jsonWriter.WritePropertyName("PrismRegistrationKey");
                jsonWriter.WriteValue(this.PrismRegistrationKey);
                jsonWriter.WritePropertyName("PaymentType");
                jsonWriter.WriteValue((this.SessionPaymentType == null) ? null : this.SessionPaymentType.ToString());
                jsonWriter.WritePropertyName("OnePassProductName");
                jsonWriter.WriteValue(this.OnePassProductName);
                jsonWriter.WritePropertyName("ProductView");
                jsonWriter.WriteValue(this.ProductView);
                jsonWriter.WritePropertyName("IpAddress");
                jsonWriter.WriteValue(this.IpAddress);
                jsonWriter.WritePropertyName("UserCategory");
                jsonWriter.WriteValue(this.UserCategory);
                jsonWriter.WriteEndObject();
            }

            return sb.ToString();
        }
    }
}
