namespace Framework.Core.Cobalt.Passwords
{
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Net.Mail;
    using System.Transactions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Provides the ability to checkin/checkout a set of WestlawNext passwords, aiding in multi-threaded execution of test cases.
    /// </summary>
    public static class PasswordUtils
    {
        private const string DevDatabase = @"Server=eg-dbasqldv-a04.tlr.thomson.com;User ID=cobaltqed;Password=c@baltQEd!;database=password_pool";
        private static string ProdDatabase => $@"Server=cr-pp.1129.aws-int.thomsonreuters.com;User ID=cobaltqeduser;Password={Environment.GetEnvironmentVariable("PROD_DB_VALUE")};database=password_pool";

        /// <summary>
        /// Enumerates all valid environments for the Password Database.
        /// </summary>
        public enum PasswordDatabaseEnvironment
        {
            /// <summary>
            /// Indicates an environment of Development.
            /// </summary>
            Development,

            /// <summary>
            /// Indicates an environment of Production.
            /// </summary>
            Production
        }

        /// <summary>
        /// Checks out a password.  If no password could be checked out, <c>null</c> is returned.  It is suggested ClearExpiredPasswords be executed prior to this method to ensure the greatest possibility of acquiring a password.
        /// </summary>
        /// <param name="testContext">A test context.</param>
        /// <param name="minutesPwdsExpire">The number of minutes from the current date and time at which the checked out password will be available for others to check out again.  Defaults to <c>10</c>.</param>
        /// <param name="module">A module name.  If <c>null</c>, defaults to <c>QED_TESTING</c>.</param>
        /// <param name="pool">A password pool.  If <c>null</c>, defaults to <c>GENERAL_PURPOSE</c>.</param>
        /// <returns>Information associated with the password.</returns>
        /// <remarks>An <see cref="ArgumentNullException"/> is thrown if <c>testContext</c> is <c>null</c>.</remarks>
        public static PasswordInfo CheckoutPassword(
            TestContext testContext,
            int minutesPwdsExpire = 10,
            string module = "QED_TESTING",
            string pool = "GENERAL_PURPOSE")
        {
            // validate input
            if (testContext == null)
            {
                throw new ArgumentNullException("testContext", "TestContext cannot be null.");
            }

            // determine if a Test Run identifier has already been generated
            if (!testContext.Properties.Contains("TestRunId"))
            {
                testContext.Properties["TestRunId"] = DateTime.UtcNow.ToString("MMddyyyyhhmmss") + new Random().Next(1000, 9999);
            }

            // determine fields for query
            string testRunId = (string)testContext.Properties["TestRunId"];
            string machineName = (string)testContext.Properties["ControllerName"];

            if (machineName == null)
            {
                machineName = Environment.MachineName;
            }

            DateTime currentDateTime = DateTime.UtcNow;
            DateTime expiresDateTime = currentDateTime.AddMinutes(minutesPwdsExpire);

            // execute atomic operation using LINQ to avoid racing condition
            using (DataContext db = PasswordUtils.GetPasswordDbDataContext(testContext))
            {
                // update a password to be checked out
                using (var transactionScope = new TransactionScope(
                    TransactionScopeOption.Required,
                    new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.Serializable }))
                {
                    db.ExecuteQuery<int>(
                        "UPDATE TOP(1) PASSWORD_POOL WITH (ROWLOCK) SET AVAILABLE = 0, TESTRUNID = {0}, CHECK_OUT_TIME = {1}, CHECKED_OUT_BY = {2}, TEST_USING_PWD = {3}, EXPIRATION_TIME = {4} WHERE VERTICAL = {5} AND POOL = {6} AND AVAILABLE = 1",
                        testRunId,
                        currentDateTime,
                        machineName,
                        testContext.TestName,
                        expiresDateTime,
                        module,
                        pool);
                    transactionScope.Complete();
                }

                // determine if any passwords were checked out and return their information
                List<PasswordInfo> checkedOutPasswords = (from passwords in db.GetTable<PasswordInfo>()
                                                          where passwords.TestRunId == testRunId && passwords.Available == 0
                                                          orderby passwords.CheckoutDateTime descending
                                                          select passwords).ToList();

                return checkedOutPasswords.Count == 0 ? null : checkedOutPasswords[0];
            }
        }

        /// <summary>
        /// Makes available those passwords that were previously checked out and were not checked back in but whose checkout is now expired.
        /// </summary>
        /// <param name="testContext">A test context.</param>
        /// <param name="module">A module name.  If <c>null</c>, defaults to <c>QED_TESTING</c>.</param>
        /// <param name="pool">A password pool.  If <c>null</c>, defaults to <c>GENERAL_PURPOSE</c>.</param>
        /// <returns>The number of passwords made available for <c>module</c> and <c>pool</c>.</returns>
        public static int ClearExpiredPasswords(TestContext testContext, string module, string pool)
        {
            // validate input
            if (testContext == null)
            {
                throw new ArgumentNullException("testContext", "TestContext cannot be null.");
            }

            using (DataContext db = PasswordUtils.GetPasswordDbDataContext(testContext))
            {
                List<PasswordInfo> checkedOutPasswords = (from passwordInfo in db.GetTable<PasswordInfo>()
                                                          where passwordInfo.Available == 0
                                                                && passwordInfo.Module == module
                                                                && passwordInfo.Pool == pool
                                                                && (passwordInfo.CheckoutDateTime == null
                                                                    || passwordInfo.ExpirationDateTime < DateTime.UtcNow)
                                                          select passwordInfo).ToList();

                foreach (PasswordInfo checkedOutPassword in checkedOutPasswords)
                {
                    checkedOutPassword.Available = 1;
                    checkedOutPassword.TestRunId = null;
                    checkedOutPassword.CheckoutDateTime = null;
                    checkedOutPassword.CheckedOutBy = null;
                    checkedOutPassword.TestUsingPwd = null;
                    checkedOutPassword.ExpirationDateTime = null;
                }

                db.SubmitChanges(ConflictMode.FailOnFirstConflict);
                return checkedOutPasswords.Count;
            }
        }

        /// <summary>
        /// Makes available those passwords that were previously checked out and were not checked back in but whose checkout is now expired.
        /// </summary>
        /// <param name="testContext">A test context.</param>
        /// <returns>The number of passwords made available for <c>module</c> and <c>pool</c>.</returns>
        public static int ClearExpiredPasswords(TestContext testContext)
        {
            // validate input
            if (testContext == null)
            {
                throw new ArgumentNullException("testContext", "TestContext cannot be null.");
            }

            using (DataContext db = PasswordUtils.GetPasswordDbDataContext(testContext))
            {
                List<PasswordInfo> checkedOutPasswords =
                    (from passwordInfo in db.GetTable<PasswordInfo>()
                     where passwordInfo.Available == 0
                           && (passwordInfo.CheckoutDateTime == null
                               || passwordInfo.ExpirationDateTime < DateTime.UtcNow)
                     select passwordInfo).ToList();

                foreach (PasswordInfo checkedOutPassword in checkedOutPasswords)
                {
                    checkedOutPassword.Available = 1;
                    checkedOutPassword.TestRunId = null;
                    checkedOutPassword.CheckoutDateTime = null;
                    checkedOutPassword.CheckedOutBy = null;
                    checkedOutPassword.TestUsingPwd = null;
                    checkedOutPassword.ExpirationDateTime = null;
                }

                db.SubmitChanges(ConflictMode.FailOnFirstConflict);
                return checkedOutPasswords.Count;
            }
        }

        /// <summary>
        /// Checks in all passwords associated with the current test run.
        /// </summary>
        /// <param name="testContext">A test context.</param>
        /// <returns>The number of passwords checked back in.</returns>
        public static int CheckinPasswords(TestContext testContext)
        {
            // validate input
            if (testContext == null)
            {
                throw new ArgumentNullException("testContext", "TestContext cannot be null.");
            }

            using (DataContext db = PasswordUtils.GetPasswordDbDataContext(testContext))
            {
                List<PasswordInfo> checkedOutPasswords =
                    (from passwordInfo in db.GetTable<PasswordInfo>()
                     where passwordInfo.TestRunId == (string)testContext.Properties["TestRunId"]
                     select passwordInfo).ToList();

                foreach (PasswordInfo checkedOutPassword in checkedOutPasswords)
                {
                    checkedOutPassword.Available = 1;
                    checkedOutPassword.TestRunId = null;
                    checkedOutPassword.CheckoutDateTime = null;
                    checkedOutPassword.CheckedOutBy = null;
                    checkedOutPassword.TestUsingPwd = null;
                    checkedOutPassword.ExpirationDateTime = null;
                }

                db.SubmitChanges(ConflictMode.FailOnFirstConflict);
                return checkedOutPasswords.Count;
            }
        }

        /// <summary>
        /// Blocks all passwords associated with a Prism username.  A blocked password can no longer be checked out.  Only those passwords currently not checked out may be blocked.
        /// </summary>
        /// <param name="passwordDatabaseEnvironment">A Password Database environment.</param>
        /// <param name="prismUsername">A Prism username.</param>
        /// <param name="recipients">A list of email addresses to which a notification of unblockage will be sent.</param>
        /// <returns>The number of passwords successfully blocked.</returns>
        /// <remarks>An <see cref="ArgumentNullException"/> is thrown if <c>prismUsername</c> is <c>null</c>.</remarks>
        public static int BlockPassword(PasswordDatabaseEnvironment passwordDatabaseEnvironment, string prismUsername, IEnumerable<string> recipients = null)
        {
            // validate input
            if (prismUsername == null)
            {
                throw new ArgumentNullException("prismUsername", "Prism Username cannot be null.");
            }

            // determine connection string
            string dbConnectionString;
            switch (passwordDatabaseEnvironment)
            {
                case PasswordDatabaseEnvironment.Production:
                    dbConnectionString = ProdDatabase;
                    break;
                case PasswordDatabaseEnvironment.Development:
                    dbConnectionString = DevDatabase;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("passwordDatabaseEnvironment", "The Password Database Environment contained an unexpected value.");
            }

            // block password(s)
            using (var db = new DataContext(dbConnectionString))
            {
                List<PasswordInfo> passwords = (from passwordInfo in db.GetTable<PasswordInfo>()
                                                where passwordInfo.PrismUsername == prismUsername
                                                      && passwordInfo.Available == 1
                                                select passwordInfo).ToList();

                foreach (PasswordInfo password in passwords)
                {
                    password.Available = 2;
                    password.TestRunId = null;
                    password.CheckoutDateTime = null;
                    password.CheckedOutBy = "Blocked";
                    password.TestUsingPwd = "Password Blocked";
                    password.ExpirationDateTime = null;
                }

                db.SubmitChanges(ConflictMode.FailOnFirstConflict);

                // send email, if valued
                if (recipients != null)
                {
                    var message = new MailMessage
                    {
                        From = new MailAddress("DoNotReplyQED@thomsonreuters.com")
                    };
                    if (passwords.Count == 0)
                    {
                        message.Subject = "Password Blockage Failed";
                        message.Body = "A blockage of the password(s) for Prism username '" + prismUsername +
                                       "' was attempted but failed, as the Prism username was not found.";
                    }
                    else
                    {
                        message.Subject = "Password Blocked";
                        message.Body = "Blockage of the password(s) for Prism username '" + prismUsername +
                                       "' was successful.  The passwords associated with this Prism username can no longer be checked out until unblocked.";
                    }

                    foreach (string recipient in recipients)
                    {
                        message.To.Add(new MailAddress(recipient.Trim()));
                    }

                    var emailClient = new SmtpClient
                    {
                        Host = "relay.westlawn.com",
                        Port = 25
                    };
                    emailClient.Send(message);
                }

                return passwords.Count;
            }
        }

        /// <summary>
        /// Unblocks all passwords associated with a Prism username.  An unblocked password can be checked out.  Only those passwords currently blocked may be unblocked.
        /// </summary>
        /// <param name="passwordDatabaseEnvironment">A Password Database environment.</param>
        /// <param name="prismUsername">A Prism username.</param>
        /// <param name="recipients">A list of email addresses to which a notification of blockage will be sent.</param>
        /// <returns>The number of passwords successfully unblocked.</returns>
        /// <remarks>An <see cref="ArgumentNullException"/> is thrown if <c>prismUsername</c> is <c>null</c>.</remarks>
        public static int UnblockPassword(PasswordDatabaseEnvironment passwordDatabaseEnvironment, string prismUsername, IEnumerable<string> recipients = null)
        {
            // validate input
            if (prismUsername == null)
            {
                throw new ArgumentNullException("prismUsername", "Prism Username cannot be null.");
            }

            // determine connection string
            string dbConnectionString;
            switch (passwordDatabaseEnvironment)
            {
                case PasswordDatabaseEnvironment.Production:
                    dbConnectionString = ProdDatabase;
                    break;
                case PasswordDatabaseEnvironment.Development:
                    dbConnectionString = DevDatabase;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("passwordDatabaseEnvironment", "The Password Database Environment contained an unexpected value.");
            }

            // unblock password(s)
            using (var db = new DataContext(dbConnectionString))
            {
                List<PasswordInfo> passwords = (from passwordInfo in db.GetTable<PasswordInfo>()
                                                where passwordInfo.PrismUsername == prismUsername
                                                      && passwordInfo.Available == 2
                                                select passwordInfo).ToList();

                foreach (PasswordInfo password in passwords)
                {
                    password.Available = 1;
                    password.TestRunId = null;
                    password.CheckoutDateTime = null;
                    password.CheckedOutBy = null;
                    password.TestUsingPwd = null;
                    password.ExpirationDateTime = null;
                }

                db.SubmitChanges(ConflictMode.FailOnFirstConflict);

                // send email, if valued
                if (recipients != null)
                {
                    var message = new MailMessage
                    {
                        From = new MailAddress("DoNotReplyQED@thomsonreuters.com")
                    };
                    if (passwords.Count == 0)
                    {
                        message.Subject = "Password Unblocking Failed";
                        message.Body = "An unblocking of the password(s) for Prism username '" + prismUsername +
                                       "' was attempted but failed, as the Prism username was not found.";
                    }
                    else
                    {
                        message.Subject = "Password Unblocked";
                        message.Body = "Unblocking of the password(s) for Prism username '" + prismUsername +
                                       "' was successful.  The passwords associated with this Prism username can now be checked out.";
                    }

                    foreach (string recipient in recipients)
                    {
                        message.To.Add(new MailAddress(recipient.Trim()));
                    }

                    var emailClient = new SmtpClient
                    {
                        Host = "relay.westlawn.com",
                        Port = 25
                    };
                    emailClient.Send(message);
                }

                return passwords.Count;
            }
        }

        /// <summary>
        /// Retrieves information associated with a password.
        /// </summary>
        /// <param name="passwordDatabaseEnvironment">A Password Database environment.</param>
        /// <param name="onePassUsername">A OnePass username.</param>
        /// <returns>Information associated with a password.</returns>
        public static PasswordInfo GetPasswordInfo(PasswordDatabaseEnvironment passwordDatabaseEnvironment, string onePassUsername)
        {
            // validate input
            if (onePassUsername == null)
            {
                throw new ArgumentNullException("onePassUsername", "OnePass Username cannot be null.");
            }

            // determine connection string
            string dbConnectionString;
            switch (passwordDatabaseEnvironment)
            {
                case PasswordDatabaseEnvironment.Production:
                    dbConnectionString = ProdDatabase;
                    break;
                case PasswordDatabaseEnvironment.Development:
                    dbConnectionString = DevDatabase;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("passwordDatabaseEnvironment", "The Password Database Environment contained an unexpected value.");
            }

            // retrieve password
            using (var db = new DataContext(dbConnectionString))
            {
                List<PasswordInfo> passwords = (from passwordInfo in db.GetTable<PasswordInfo>()
                                                where passwordInfo.OnePassUsername == onePassUsername
                                                select passwordInfo).ToList();

                return passwords.Count == 0 ? null : passwords[0];
            }
        }

        /// <summary>
        /// Updates a password.  Only passwords that are currently blocked may be updated.  The following fields in <c>passwordInfo</c> will not be included in the update:
        /// <ul>
        /// <li><c>Available</c></li>
        /// <li><c>TestRunId</c></li>
        /// <li><c>CheckoutDateTime</c></li>
        /// <li><c>CheckedOutBy</c></li>
        /// <li><c>TestUsingPwd</c></li>
        /// <li><c>ExpirationDateTime</c></li>
        /// <li><c>PrismUsername</c></li>
        /// <li><c>PrismPassword</c></li>
        /// </ul>
        /// </summary>
        /// <param name="passwordDatabaseEnvironment">A Password Database environment.</param>
        /// <param name="passwordInfo">Password information.  The <c>PrismUsername</c> field must be set for the correct records to be updated.</param>
        /// <param name="recipients">A list of email addresses to which a notification will be sent indicating information associated with the password has been changed.</param>
        /// <returns>The number of passwords whose information was updated.</returns>
        /// <remarks>An <see cref="ArgumentNullException"/> is thrown if <c>passwordInfo</c> is <c>null</c>.</remarks>
        public static int UpdatePassword(PasswordDatabaseEnvironment passwordDatabaseEnvironment, PasswordInfo passwordInfo, IEnumerable<string> recipients = null)
        {
            // validate input
            if (passwordInfo == null)
            {
                throw new ArgumentNullException("passwordInfo", "Password Info cannot be null.");
            }

            // set fields that should not be changed from blocked status
            passwordInfo.Available = 2;
            passwordInfo.TestRunId = null;
            passwordInfo.CheckoutDateTime = null;
            passwordInfo.CheckedOutBy = "Blocked";
            passwordInfo.TestUsingPwd = "Password Blocked";
            passwordInfo.ExpirationDateTime = null;

            // determine connection string
            string dbConnectionString;
            switch (passwordDatabaseEnvironment)
            {
                case PasswordDatabaseEnvironment.Production:
                    dbConnectionString = ProdDatabase;
                    break;
                case PasswordDatabaseEnvironment.Development:
                    dbConnectionString = DevDatabase;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("passwordDatabaseEnvironment", "The Password Database Environment contained an unexpected value.");
            }

            // update password
            using (var db = new DataContext(dbConnectionString))
            {
                List<PasswordInfo> passwords = (from dbPasswordInfo in db.GetTable<PasswordInfo>()
                                                where dbPasswordInfo.PrismUsername == passwordInfo.PrismUsername
                                                      && dbPasswordInfo.Available == 2
                                                select passwordInfo).ToList();

                foreach (PasswordInfo password in passwords)
                {
                    password.Email = passwordInfo.Email;
                    password.FirstName = passwordInfo.FirstName;
                    password.GmailPassword = passwordInfo.GmailPassword;
                    password.GmailUsername = passwordInfo.GmailUsername;
                    password.LastName = passwordInfo.LastName;
                    password.OnePassPassword = passwordInfo.OnePassPassword;
                    password.OnePassUsername = passwordInfo.OnePassUsername;
                    password.PrismGuid = passwordInfo.PrismGuid;
                }

                db.SubmitChanges(ConflictMode.FailOnFirstConflict);
                return passwords.Count;
            }
        }

        /// <summary>
        /// Retrieves a <see cref="DataContext"/> associated with the Password Database.
        /// </summary>
        /// <param name="testContext">A test context.</param>
        /// <returns>A <see cref="DataContext"/> associated with the Password Database.</returns>
        private static DataContext GetPasswordDbDataContext(TestContext testContext)
        {
            // validate input
            if (testContext == null)
            {
                throw new ArgumentNullException("testContext", "TestContext cannot be null.");
            }

            // Password DB environment *NOT* previously set
            if (!testContext.Properties.Contains("PwdDbEnv"))
            {
                // attempt production connection
                using (var connection = new SqlConnection(ProdDatabase))
                {
                    try
                    {
                        connection.Open();
                        testContext.Properties["PwdDbEnv"] = PasswordDatabaseEnvironment.Production;
                        return new DataContext(ProdDatabase);
                    }
                    catch (Exception e)
                    {
                        using (TextWriter textWriter = Console.Error)
                        {
                            textWriter.Write("Connection to Production Password Database failed:" + e.Message);
                        }
                    }
                }

                // if production connection fails, attempt development connection
                using (var connection = new SqlConnection(DevDatabase))
                {
                    try
                    {
                        connection.Open();
                        testContext.Properties["PwdDbEnv"] = PasswordDatabaseEnvironment.Development;
                        return new DataContext(DevDatabase);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Connection to both Production & Development Password Databases failed:" + e.Message);
                    }
                }
            }

            // Password DB environment previously set
            var passwordDatabaseEnvironment = (PasswordDatabaseEnvironment)testContext.Properties["PwdDbEnv"];
            switch (passwordDatabaseEnvironment)
            {
                case PasswordDatabaseEnvironment.Production:
                    return new DataContext(ProdDatabase);
                case PasswordDatabaseEnvironment.Development:
                    return new DataContext(DevDatabase);
                default:
                    throw new ArgumentOutOfRangeException("testContext", "The TestContext contained an invalid value in its 'PwdDbEnv' property.");
            }
        }
    }
}
