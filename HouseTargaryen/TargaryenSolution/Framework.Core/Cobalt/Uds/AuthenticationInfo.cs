namespace Framework.Core.Cobalt.Uds
{
    using System;
    using System.IO;
    using System.Text;

    using Framework.Core.Utils;
    using Newtonsoft.Json;


    /// <summary>
    /// Stores information regarding Prism Authentication.
    /// </summary>
    internal sealed class AuthenticationInfo
    {
        /// <summary>
        /// Constructs a default set of authentication information.
        /// </summary>
        public AuthenticationInfo()
        {
            // default constructor
        }

        /// <summary>
        /// Used in case of authentication failure.
        /// </summary>
        /// <param name="prismAuthStatusCode">A Prism authentication status code.</param>
        /// <param name="prismAuthFailureReason">A Prism authentication failure reason.</param>
        public AuthenticationInfo(int prismAuthStatusCode, string prismAuthFailureReason)
        {
            this.PrismAuthStatusCode = prismAuthStatusCode;
            this.PrismAuthFailureReason = prismAuthFailureReason;
        }

        /// <summary>
        /// Used in case of authentication success.
        /// </summary>
        /// <param name="prismGuid">A Prism GUID</param>
        /// <param name="prismAuthToken">A Prism authentication token.</param>
        /// <param name="endDate">An end date for the Prism authentication.</param>
        /// <param name="prismAuthStatusCode">A Prism authentication status code.</param>
        public AuthenticationInfo(string prismGuid, string prismAuthToken, DateTime endDate, int prismAuthStatusCode)
        {
            this.PrismGuid = prismGuid;
            this.PrismAuthToken = prismAuthToken;
            this.EndDate = endDate;
            this.PrismAuthStatusCode = prismAuthStatusCode;
        }

        /// <summary>
        /// Used in case of authentication success.
        /// </summary>
        /// <param name="prismGuid">A Prism GUID.</param>
        /// <param name="prismAuthToken">A Prism authentication token.</param>
        /// <param name="prismUserId">A Prism user identifier.</param>
        /// <param name="firstName">The first name of the user associated with the Prism GUID.</param>
        /// <param name="lastName">The last name of the user associated with the Prism GUID.</param>
        /// <param name="endDate">The end date for the Prism authentication.</param>
        /// <param name="prismAuthStatusCode">A Prism authentication status code.</param>
        public AuthenticationInfo(string prismGuid, string prismAuthToken, string prismUserId, string firstName, string lastName, DateTime endDate, int prismAuthStatusCode)
        {
            this.PrismGuid = prismGuid;
            this.PrismAuthToken = prismAuthToken;
            this.PrismUserId = prismUserId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.EndDate = endDate;
            this.PrismAuthStatusCode = prismAuthStatusCode;
        }

        /// <summary>
        /// Gets or sets the Prism GUID.
        /// </summary>
        /// <value>The Prism GUID.</value>
        public string PrismGuid { get; set; }

        /// <summary>
        /// Gets or sets the Prism authentication token.
        /// </summary>
        /// <value>The Prism authentication token.</value>
        public string PrismAuthToken { get; set; }

        /// <summary>
        /// Gets or sets the Prism authentication failure reason.
        /// </summary>
        /// <value>The Prism authentication failure reason.</value>
        public string PrismAuthFailureReason { get; set; }

        /// <summary>
        /// Gets or sets the Prism user identifier.
        /// </summary>
        /// <value>The Prism user identifier.</value>
        public string PrismUserId { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user associated with the Prism information.
        /// </summary>
        /// <value>The first name of the user associated with the Prism information.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user associated with the Prism information.
        /// </summary>
        /// <value>The last name of the user associated with the Prism information.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the end date for the Prism authentication.
        /// </summary>
        /// <value>The end date for the Prism authentication.</value>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the Prism authentication status code.
        /// </summary>
        /// <value>The Prism authentication status code.</value>
        /// <remarks>A value of zero indicates success.  Any other values are negative, and string representation can be retrieved via <see cref="PrismAuthFailureReason"/>.</remarks>
        public int? PrismAuthStatusCode { get; set; }
        
        /// <summary>
        /// Determines whether the specified <see cref="AuthenticationInfo"/> is equal to the current <see cref="AuthenticationInfo"/>.
        /// </summary>
        /// <param name="aThat">The <see cref="AuthenticationInfo"/> to compare with the current <see cref="AuthenticationInfo"/>.</param>
        /// <returns><b>true</b> if the specified Object is equal to the current <see cref="AuthenticationInfo"/>; otherwise, <b>false</b>.</returns>
        public override bool Equals(object aThat)
        {
            if (aThat == null || this.GetType() != aThat.GetType())
            {
                return false;
            }

            var that = (AuthenticationInfo)aThat;
            return EqualsUtils.AreEqual(this.PrismGuid, that.PrismGuid)
                   && EqualsUtils.AreEqual(this.PrismAuthToken, that.PrismAuthToken)
                   && EqualsUtils.AreEqual(this.PrismAuthFailureReason, that.PrismAuthFailureReason)
                   && EqualsUtils.AreEqual(this.PrismUserId, that.PrismUserId)
                   && EqualsUtils.AreEqual(this.FirstName, that.FirstName)
                   && EqualsUtils.AreEqual(this.LastName, that.LastName)
                   && EqualsUtils.AreEqual(this.EndDate, that.EndDate) 
                   && EqualsUtils.AreEqual(this.PrismAuthStatusCode, that.PrismAuthStatusCode);
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="AuthenticationInfo"/>.</returns>
        public override int GetHashCode()
        {
            int result = HashCodeUtils.Seed;
            result = HashCodeUtils.Hash(result, this.PrismGuid);
            result = HashCodeUtils.Hash(result, this.PrismAuthToken);
            result = HashCodeUtils.Hash(result, this.PrismAuthFailureReason);
            result = HashCodeUtils.Hash(result, this.PrismUserId);
            result = HashCodeUtils.Hash(result, this.FirstName);
            result = HashCodeUtils.Hash(result, this.LastName);
            result = HashCodeUtils.Hash(result, this.EndDate);
            result = HashCodeUtils.Hash(result, this.PrismAuthStatusCode);
            return result;
        }

        /// <summary>
        /// Returns a string that represents the current <see cref="AuthenticationInfo"/>.
        /// </summary>
        /// <returns>A string that represents the current <see cref="AuthenticationInfo"/>.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            using (var jsonWriter = new JsonTextWriter(new StringWriter(sb)))
            {
                jsonWriter.Formatting = Formatting.Indented;
                jsonWriter.WriteStartObject();
                jsonWriter.WritePropertyName("PrismGuid");
                jsonWriter.WriteValue(this.PrismGuid);
                jsonWriter.WritePropertyName("PrismAuthToken");
                jsonWriter.WriteValue(this.PrismAuthToken);
                jsonWriter.WritePropertyName("PrismAuthFailureReason");
                jsonWriter.WriteValue(this.PrismAuthFailureReason);
                jsonWriter.WritePropertyName("PrismUserId");
                jsonWriter.WriteValue(this.PrismUserId);
                jsonWriter.WritePropertyName("FirstName");
                jsonWriter.WriteValue(this.FirstName);
                jsonWriter.WritePropertyName("LastName");
                jsonWriter.WriteValue(this.LastName);
                jsonWriter.WritePropertyName("EndDate");
                jsonWriter.WriteValue((this.EndDate == null) ? null : ((DateTime)this.EndDate).ToUniversalTime().ToString("o"));
                jsonWriter.WritePropertyName("PrismAuthStatusCode");
                jsonWriter.WriteValue(this.PrismAuthStatusCode);
                jsonWriter.WriteEndObject();
            }

            return sb.ToString();
        }
    }
}
