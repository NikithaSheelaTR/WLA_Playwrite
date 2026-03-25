namespace Framework.Core.CommonTypes.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Framework.Core.CommonTypes.Enums.Setup;
    using Framework.Core.CommonTypes.Enums.TestCapture;
    using Framework.Core.Utils;

    /// <summary>
    /// A class representing a collection of TestSettingKey objects.
    /// This is where the default TestSettingKeys are declared.
    /// </summary>
    public class TestSettingKeys
    {
        #region TestSettingKey Dictionary Declaration

        /// <summary>
        /// 
        /// </summary>
        protected static readonly Dictionary<string, TestSettingKey> Keys = new Dictionary<string, TestSettingKey>();

        #endregion

        #region TestSettingKey Declarations
        // ReSharper disable InconsistentNaming

        /// <summary>
        /// The name of the test case
        /// </summary>
        public static TestSettingKey<string> TEST_NAME = TestSettingKeys.AddKey<string>("TEST_NAME", null);

        /// <summary>
        /// The directory the test is executing within
        /// </summary>
        public static TestSettingKey<string> TEST_EXECUTION_DIR = TestSettingKeys.AddKey("TEST_EXECUTION_DIR", FileUtils.RootDir);

        /// <summary>
        /// The name of the directory where test results are copied to (not currently used)
        /// </summary>
        public static TestSettingKey<string> TEST_RESULTS_DIR = TestSettingKeys.AddKey("TEST_RESULTS_DIR", FileUtils.RootDir + "test-results/");

        /// <summary>
        /// The browser to use in the test (for Selenium based tests) - Note this probably doesn't need to be in the base TestSettings
        /// class, for now it is here mostly for convenience.
        /// </summary>
        public static TestSettingKey<BrowserUnderTest> TEST_BROWSER = TestSettingKeys.AddKey<BrowserUnderTest>("TEST_BROWSER", BrowserUnderTest.CHROME);

        /// <summary>
        /// The environment to test
        /// </summary>
        public static TestSettingKey<IEnvironment> TEST_ENVIRONMENT = TestSettingKeys.AddKey<IEnvironment>("TEST_ENVIRONMENT", null);

        /// <summary>
        /// The test capture setting to use
        /// </summary>
        public static TestSettingKey<TestCaptureSetting> TEST_CAPTURE_SETTING = TestSettingKeys.AddKey("TEST_CAPTURE_SETTING", TestCaptureSetting.NO_CAPTURE);

        /// <summary>
        /// The directory to save screenshots to 
        /// </summary>
        public static TestSettingKey<string> TEST_SCREENSHOT_DIR = TestSettingKeys.AddKey("TEST_SCREENSHOT_DIR", "\\Screenshots");

        /// <summary>
        /// The directory to save videos to 
        /// </summary>
        public static TestSettingKey<string> TEST_VIDEO_DIR = TestSettingKeys.AddKey("TEST_VIDEO_DIR", "\\Videos");

        /// <summary>
        /// The name of the results file (not currently used)
        /// </summary>
        public static TestSettingKey<string> TEST_RESULTS_FILE_NAME = TestSettingKeys.AddKey("TEST_RESULTS_FILE_NAME",
            "TestResults.trx");

        // USER
        /// <summary>
        /// The test account's username
        /// </summary>
        public static TestSettingKey<string> USER_USERNAME = TestSettingKeys.AddKey<string>("USER_USERNAME", null);

        /// <summary>
        /// The test account's password
        /// </summary>
        public static TestSettingKey<string> USER_PASSWORD = TestSettingKeys.AddKey<string>("USER_PASSWORD", null);

        /// <summary>
        /// The test account's email address (used this when your test needs to set an email address, such as 
        /// when testing WLN email delivery)
        /// </summary>
        public static TestSettingKey<string> USER_EMAIL = TestSettingKeys.AddKey<string>("USER_EMAIL", null);

        /// <summary>
        /// The test account's first name
        /// </summary>
        public static TestSettingKey<string> USER_FIRST_NAME = TestSettingKeys.AddKey<string>("USER_FIRST_NAME", null);

        /// <summary>
        /// The test account's last name
        /// </summary>
        public static TestSettingKey<string> USER_LAST_NAME = TestSettingKeys.AddKey<string>("USER_LAST_NAME", null);

        /// <summary>
        /// The test account's "GMAIL" address - corresponds to the GMAIL field in the QRT Password Tool, this will likely eventually
        /// go away
        /// </summary>
        public static TestSettingKey<string> USER_GMAIL = TestSettingKeys.AddKey<string>("USER_GMAIL", null);

        /// <summary>
        /// The test account's "GMAIL" password - corresponds to the GMAIL_PASSWORD field in the QRT Password Tool, this will likely eventually
        /// go away
        /// </summary>
        public static TestSettingKey<string> USER_GMAIL_PASSWORD = TestSettingKeys.AddKey<string>("USER_GMAIL_PASSWORD", null);


        // QRT
        /// <summary>
        /// The name of the local config file
        /// </summary>
        public static TestSettingKey<string> LOCAL_CONFIG_FILE = TestSettingKeys.AddKey("LOCAL_CONFIG_FILE", FileUtils.RootDir + "\\LocalTestConfig.xml");

        /// <summary>
        /// The name of the QrtTestConfig file that will be generated by the test
        /// </summary>
        public static TestSettingKey<string> QRT_CONFIG_FILE = TestSettingKeys.AddKey("QRT_CONFIG_FILE", FileUtils.RootDir + "\\QrtTestConfig.xml");

        /// <summary>
        /// The name of the business case to put in the generated QrtTestConfig file
        /// </summary>
        public static TestSettingKey<string> QRT_BUSINESS_CASE = TestSettingKeys.AddKey<string>("QRT_BUSINESS_CASE", "Local Test Run");

        /// <summary>
        /// The TestRunName to put in the generated QrtTestConfig file
        /// </summary>
        public static TestSettingKey<string> QRT_TEST_RUN_NAME = TestSettingKeys.AddKey("QRT_TEST_RUN_NAME", "Local Test Run");

        /// <summary>
        /// The Tags to put in the QrtTestConfig file that gets generated
        /// </summary>
        public static TestSettingKey<string[]> QRT_TAGS = TestSettingKeys.AddKey("QRT_TAGS", new String[0]);

        /// <summary>
        /// The notification email addresses to put in the QrtTestConfig file that gets generated
        /// </summary>
        public static TestSettingKey<string[]> QRT_NOTIFICATION_EMAILS = TestSettingKeys.AddKey("QRT_NOTIFICATION_EMAILS",
            new String[0]);

        /// <summary>
        /// The Release Set repository names to include in the generated QrtTestConfig file
        /// </summary>
        public static TestSettingKey<string[]> QRT_RELEASE_SET_REPOSITORIES = TestSettingKeys.AddKey("QRT_RELEASE_SET_REPOSITORIES",
            new String[0]);

        /// <summary>
        /// The Release Set repository environments to include in the generated QrtTestConfig file
        /// </summary>
        public static TestSettingKey<string[]> QRT_RELEASE_SET_ENVIRONMENTS = TestSettingKeys.AddKey("QRT_RELEASE_SET_ENVIRONMENTS",
            new String[0]);

        /// <summary>
        /// special setting for pulling user from credentials file for TNP3 suite
        /// </summary>
        public static TestSettingKey<string> TEST_USER = TestSettingKeys.AddKey<string>("TEST_USER", null);

        // ReSharper restore InconsistentNaming

        /// <summary>
        /// Needed to get around issues with how exactly the static key objects are initialized (especially since the core
        /// test settings functionality makes use of reflection) this helps ensure they have always been initialized by the 
        /// time they are referenced elsewhere in the code.
        /// </summary>
        public virtual void InitKeys()
        {
            TEST_NAME = TestSettingKeys.AddKey<string>("TEST_NAME", null);
            TEST_EXECUTION_DIR = TestSettingKeys.AddKey("TEST_EXECUTION_DIR", FileUtils.RootDir);
            TEST_RESULTS_DIR = TestSettingKeys.AddKey("TEST_RESULTS_DIR", FileUtils.RootDir + "test-results/");
            TEST_BROWSER = TestSettingKeys.AddKey<BrowserUnderTest>("TEST_BROWSER", BrowserUnderTest.CHROME);
            TEST_ENVIRONMENT = TestSettingKeys.AddKey<IEnvironment>("TEST_ENVIRONMENT", null);
            TEST_CAPTURE_SETTING = TestSettingKeys.AddKey("TEST_CAPTURE_SETTING", TestCaptureSetting.NO_CAPTURE);
            TEST_SCREENSHOT_DIR = TestSettingKeys.AddKey("TEST_SCREENSHOT_DIR",  "\\Screenshots");
            TEST_VIDEO_DIR = TestSettingKeys.AddKey("TEST_VIDEO_DIR", "\\Videos");
            TEST_RESULTS_FILE_NAME = TestSettingKeys.AddKey("TEST_RESULTS_FILE_NAME", "TestResults.trx");

            USER_USERNAME = TestSettingKeys.AddKey<string>("USER_USERNAME", null);
            USER_PASSWORD = TestSettingKeys.AddKey<string>("USER_PASSWORD", null);
            USER_EMAIL = TestSettingKeys.AddKey<string>("USER_EMAIL", null);
            USER_FIRST_NAME = TestSettingKeys.AddKey<string>("USER_FIRST_NAME", null);
            USER_LAST_NAME = TestSettingKeys.AddKey<string>("USER_LAST_NAME", null);
            USER_GMAIL = TestSettingKeys.AddKey<string>("USER_GMAIL", null);
            USER_GMAIL_PASSWORD = TestSettingKeys.AddKey<string>("USER_GMAIL_PASSWORD", null);

            LOCAL_CONFIG_FILE = TestSettingKeys.AddKey("LOCAL_CONFIG_FILE", FileUtils.RootDir + "\\LocalTestConfig.xml");
            QRT_CONFIG_FILE = TestSettingKeys.AddKey("QRT_CONFIG_FILE", FileUtils.RootDir + "\\QrtTestConfig.xml");
            QRT_BUSINESS_CASE = TestSettingKeys.AddKey<string>("QRT_BUSINESS_CASE", "Local Test Run");
            QRT_TEST_RUN_NAME = TestSettingKeys.AddKey("QRT_TEST_RUN_NAME", "Local Test Run");
            QRT_TAGS = TestSettingKeys.AddKey("QRT_TAGS", new String[0]);
            QRT_NOTIFICATION_EMAILS = TestSettingKeys.AddKey("QRT_NOTIFICATION_EMAILS", new String[0]);
            QRT_RELEASE_SET_REPOSITORIES = TestSettingKeys.AddKey("QRT_RELEASE_SET_REPOSITORIES", new String[0]);
            QRT_RELEASE_SET_ENVIRONMENTS = TestSettingKeys.AddKey("QRT_RELEASE_SET_ENVIRONMENTS", new String[0]);

            TEST_USER = TestSettingKeys.AddKey<string>("TEST_USER", null);
        }

        #endregion

        #region Factory Functions

        /// <summary>
        /// Factory function used to create a new test setting key object.
        /// </summary>
        /// <param name="name"> The name of the test setting key.</param>
        /// <returns>The newly-created test setting key object.</returns>
        public static TestSettingKey<TK> AddKey<TK>(string name)
        {
            var testSettingKey = new TestSettingKey<TK>(name, null);
            Keys.Add(name, testSettingKey);
            return testSettingKey;
        }

        /// <summary>
        /// Factory function used to create a new test setting key object.
        /// </summary>
        /// <param name="name">The name of the test setting key.</param>
        /// <param name="defaultValue">The default value that should be used for the test setting key if none are specified.</param>
        /// <returns>The newly-created test setting key object.</returns>
        public static TestSettingKey<TN> AddKey<TN>(string name, TN defaultValue)
        {
            
            TestSettingKey<TN> testSettingKey = new TestSettingKey<TN>(name, defaultValue, null);
            if (!Keys.ContainsKey(name))
            {
                Keys.Add(name, testSettingKey);
                return testSettingKey;
            }
            else
            {
                return (TestSettingKey<TN>)Keys[name];
            }
        }

        /// <summary>
        /// Factory function used to create a new test setting key object.
        /// </summary>
        /// <param name="name">The name of the test setting key.</param>
        /// <param name="group">The name of the group that the test setting key belongs to.</param>
        /// <param name="defaultValue">The default value that should be used for the test setting key if none are specified.</param>
        /// <returns> The newly-created test setting key object.</returns>
        public static TestSettingKey<TN> AddKey<TN>(string name, string group, TN defaultValue)
        {
            var testSettingKey = new TestSettingKey<TN>(name, defaultValue, group);
            if (!Keys.ContainsKey(name))
            {
                Keys.Add(name, testSettingKey);
                return testSettingKey;
            }
            else
            {
                return (TestSettingKey<TN>)Keys[name];
            }
        }

        #endregion

        #region Getter Functions 


        /// <summary>
        /// Returns an array of all declared TestSettingKey objects.
        /// </summary>
        /// <returns>An array of all declared TestSettingKey objects.</returns>
        public static TestSettingKey[] GetKeys()
        {
            return Keys.Values.ToArray();
        }

        /// <summary>
        /// Returns an array of all declared TestSettingKey objects that match the specified key group.
        /// </summary>
        /// <param name="keyGroup">The key group that the desired TestSettingKeys should belong to.</param>
        /// <returns> An array of all declared TestSettingKey objects that match the specified key group.</returns>
        public static TestSettingKey[] GetKeys(string keyGroup)
        {
            if (keyGroup == null)
            {
                return TestSettingKeys.GetKeys();
            }
            else
            {
                return TestSettingKeys.GetKeys().Where(key => keyGroup.Equals(key.GetGroup())).ToArray();
            }
        }

        /// <summary>
        /// Returns a setting Key matching the specified key name.
        /// </summary>
        /// <param name="keyName"> The name of the desired key.</param>
        /// <returns>A setting Key matching the specified name.</returns>
        public static TestSettingKey GetKey(string keyName)
        {
            return Keys[keyName];
        }

        #endregion

        #region Object Functions 

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            foreach (var key in TestSettingKeys.GetKeys())
            {
                sb.Append(key + ", ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append("]");
            return sb.ToString();
        }

        #endregion

    }
}
