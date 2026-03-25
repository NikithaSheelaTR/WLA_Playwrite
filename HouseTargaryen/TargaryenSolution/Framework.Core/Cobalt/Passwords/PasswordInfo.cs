namespace Framework.Core.Cobalt.Passwords
{
    using System;
    using System.Data.Linq.Mapping;
    using System.Text;

    using Framework.Core.Utils;

    /// <summary>
    /// Stores information regarding a set of passwords stored in the Password Database.
    /// </summary>
    [Table(Name = "PASSWORD_POOL")]
    public sealed class PasswordInfo
    {
        /// <summary>
        /// Gets or sets the Prism username associated with the Prism password.
        /// </summary>
        /// <value>The Prism username associated with the Prism password.</value>
        [Column(IsPrimaryKey = true, Name = "USER_ID")]
        public string PrismUsername { get; set; }

        /// <summary>
        /// Gets or sets the Prism password.
        /// </summary>
        /// <value>The Prism password.</value>
        [Column(Name = "PASSWORD")]
        public string PrismPassword { get; set; }

        /// <summary>
        /// Gets or sets the availability of the Prism password.
        /// </summary>
        /// <value>The availability of the Prism password</value>
        /// <remarks>A value of <c>0</c> indicates it is in use already.  Additionally, a value of <c>1</c> indicates it is available for checkout.  Finally, a value of <c>2</c> indicates it is blocked.</remarks>
        [Column(Name = "AVAILABLE")]
        public byte Available { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for a test currently using the Prism password.
        /// </summary>
        /// <value>The unique identifier for a test currently using the Prism password.</value>
        /// <remarks>This value is <c>null</c> if the Prism password is available.</remarks>
        [Column(Name = "TESTRUNID")]
        public string TestRunId { get; set; }

        /// <summary>
        /// Gets or sets the name of the machine currently using the Prism password.
        /// </summary>
        /// <value>The name of the machine currently using the Prism password.</value>
        /// <remarks>This value is <c>null</c> if the Prism password is available.</remarks>
        [Column(Name = "CHECKED_OUT_BY")]
        public string CheckedOutBy { get; set; }

        /// <summary>
        /// Gets or sets the name of the test currently using the Prism password.
        /// </summary>
        /// <value>The name of the test currently using the Prism password.</value>
        /// <remarks>This value is <c>null</c> if the Prism password is available.</remarks>
        [Column(Name = "TEST_USING_PWD")]
        public string TestUsingPwd { get; set; }

        /// <summary>
        /// Gets or sets the Prism GUID associated with the Prism password.
        /// </summary>
        /// <value>The Prism GUID associated with the Prism password.</value>
        [Column(Name = "USER_GUID")]
        public string PrismGuid { get; set; }

        /// <summary>
        /// Gets or sets the date and time that the Prism password was checked out.
        /// </summary>
        /// <value>The date and time that the Prism password was checked out.</value>
        [Column(Name = "CHECK_OUT_TIME")]
        public DateTime? CheckoutDateTime { get; set; }

        /// <summary>
        /// Gets or sets the OnePass username associated with the Prism password.
        /// </summary>
        /// <value>The OnePass username associated with the Prism password.</value>
        [Column(Name = "ONEPASS_USERNAME")]
        public string OnePassUsername { get; set; }

        /// <summary>
        /// Gets or sets the OnePass password associated with the Prism password.
        /// </summary>
        /// <value>The OnePass password associated with the Prism password.</value>
        [Column(Name = "ONEPASS_PASSWORD")]
        public string OnePassPassword { get; set; }

        /// <summary>
        /// Gets or sets the first name of the person associated with the Prism password.
        /// </summary>
        /// <value>The first name of the person associated with the Prism password.</value>
        [Column(Name = "FIRST_NAME")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the person associated with the Prism password.
        /// </summary>
        /// <value>The last name of the person associated with the Prism password.</value>
        [Column(Name = "LAST_NAME")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the person associated with the Prism password.
        /// </summary>
        /// <value>The email address of the person associated with the Prism password.</value>
        [Column(Name = "EMAIL")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Gmail username of the person associated with the Prism password.
        /// </summary>
        /// <value>The Gmail username of the person associated with the Prism password.</value>
        [Column(Name = "GMAIL_USERNAME")]
        public string GmailUsername { get; set; }

        /// <summary>
        /// Gets or sets the Gmail password of the person associated with the Prism password.
        /// </summary>
        /// <value>The Gmail password of the person associated with the Prism password.</value>
        [Column(Name = "GMAIL_PASSWORD")]
        public string GmailPassword { get; set; }

        /// <summary>
        /// Gets or sets the WestlawNext module or WestlawNext Platform module that owns the Prism password.
        /// </summary>
        /// <value>The WestlawNext module or WestlawNext Platform module that owns the Prism password.</value>
        [Column(Name = "VERTICAL")]
        public string Module { get; set; }

        /// <summary>
        /// Gets or sets the password pool associated with the Prism password.
        /// </summary>
        /// <value>The password pool associated with the Prism password.</value>
        [Column(Name = "POOL")]
        public string Pool { get; set; }

        /// <summary>
        /// Gets or sets the expiration date and time associated with the Prism password.
        /// </summary>
        /// <value>The expiration date and time associated with the Prism password.</value>
        /// <remarks>This value is <c>null</c> if the Prism password is available.</remarks>
        [Column(Name = "EXPIRATION_TIME")]
        public DateTime? ExpirationDateTime { get; set; }

        /// <summary>
        /// Determines whether this <see cref="PasswordInfo"/> is equal to another <see cref="PasswordInfo"/> by value.  The original <c>Equals</c> method must be retained for comparison during LINQ-to-SQL execution.
        /// </summary>
        /// <param name="aThat">Another <see cref="PasswordInfo"/> object.</param>
        /// <returns>An indication whether this <see cref="PasswordInfo"/> is equal to another <see cref="PasswordInfo"/> by value.</returns>
        public bool EqualsByValue(object aThat)
        {
            if (aThat == null || this.GetType() != aThat.GetType())
            {
                return false;
            }

            var that = (PasswordInfo)aThat;
            return EqualsUtils.AreEqual(this.PrismUsername, that.PrismUsername)
                   && EqualsUtils.AreEqual(this.PrismPassword, that.PrismPassword)
                   && EqualsUtils.AreEqual(this.Available, that.Available)
                   && EqualsUtils.AreEqual(this.TestRunId, that.TestRunId)
                   && EqualsUtils.AreEqual(this.CheckedOutBy, that.CheckedOutBy)
                   && EqualsUtils.AreEqual(this.TestUsingPwd, that.TestUsingPwd)
                   && EqualsUtils.AreEqual(this.PrismGuid, that.PrismGuid)
                   && EqualsUtils.AreEqual(this.CheckoutDateTime, that.CheckoutDateTime)
                   && EqualsUtils.AreEqual(this.OnePassUsername, that.OnePassUsername)
                   && EqualsUtils.AreEqual(this.OnePassPassword, that.OnePassPassword)
                   && EqualsUtils.AreEqual(this.FirstName, that.FirstName)
                   && EqualsUtils.AreEqual(this.LastName, that.LastName)
                   && EqualsUtils.AreEqual(this.Email, that.Email)
                   && EqualsUtils.AreEqual(this.GmailUsername, that.GmailUsername)
                   && EqualsUtils.AreEqual(this.GmailPassword, that.GmailPassword)
                   && EqualsUtils.AreEqual(this.Module, that.Module)
                   && EqualsUtils.AreEqual(this.Pool, that.Pool)
                   && EqualsUtils.AreEqual(this.ExpirationDateTime, that.ExpirationDateTime);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("PrismUsername: " + this.PrismUsername);
            sb.AppendLine("PrismPassword: " + this.PrismPassword);
            sb.AppendLine("Available: " + this.Available);
            sb.AppendLine("TestRunId: " + this.TestRunId);
            sb.AppendLine("CheckedOutBy: " + this.CheckedOutBy);
            sb.AppendLine("TestUsingPwd: " + this.TestUsingPwd);
            sb.AppendLine("PrismGuid: " + this.PrismGuid);
            sb.AppendLine("CheckoutDateTime: " + this.CheckoutDateTime);
            sb.AppendLine("OnePassUsername: " + this.OnePassUsername);
            sb.AppendLine("OnePassPassword: " + this.OnePassPassword);
            sb.AppendLine("FirstName: " + this.FirstName);
            sb.AppendLine("LastName: " + this.LastName);
            sb.AppendLine("Email: " + this.Email);
            sb.AppendLine("GmailUsername: " + this.GmailUsername);
            sb.AppendLine("GmailPassword: " + this.GmailPassword);
            sb.AppendLine("Module: " + this.Module);
            sb.AppendLine("Pool: " + this.Pool);
            sb.AppendLine("ExpirationDateTime: " + this.ExpirationDateTime);
            return sb.ToString();
        }
    }
}
