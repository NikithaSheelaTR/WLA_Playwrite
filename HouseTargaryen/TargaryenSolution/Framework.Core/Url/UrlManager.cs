namespace Framework.Core.Url
{
    using System;
    using System.Collections.Generic;

    using Framework.Core.CommonTypes.Settings;

    /// <summary>
    ///  UrlManager class
    /// 
    /// A class for keeping track of URLs based on specific testSettings.
    /// </summary>
    public class UrlManager
    {
        /*----------------- Class Variable Declarations -----------------*/

        /// <summary>
        /// 
        /// </summary>
        protected Dictionary<UrlKey, string> Urls = new Dictionary<UrlKey, string>();

        /// <summary>
        /// 
        /// </summary>
        protected TestSettings DefaultTestSettings;

        /*----------------- Class Constructors -----------------*/

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public UrlManager()
        {
            this.DefaultTestSettings = new TestSettings();
            this.AddUrls();
        }

        /// <summary>
        /// Alternate class constructor with default TestSettings.
        /// </summary>
        /// <param name="defaultSettings">The default TestSettings that should be used if none are specified.</param>
        public UrlManager(TestSettings defaultSettings)
        {
            this.DefaultTestSettings = defaultSettings;
            this.AddUrls();
        }

        /*----------------- Setter Functions -----------------*/

        /// <summary>
        /// Sets the default test settings, which are used when test settings aren't specified.
        /// </summary>
        /// <param name="defaultTestSettings">The default TestSettings that should be used when test settings aren't specified.</param>
        public void SetDefaultTestSettings(TestSettings defaultTestSettings)
        {
            this.DefaultTestSettings = defaultTestSettings;
        }

        /*----------------- Add Functions -----------------*/

        /// <summary>
        ///  Function called by the UrlManager constructor where all the addUrl() calls should go.
        /// </summary>
        protected virtual void AddUrls()
        {
            // The shouldn't be any URLs in the base UrlManager
        }

        /// <summary>
        /// Adds the specified URL to the URL HashMap with a UrlKey corresponding to the default URL type
        /// and the specified TestSettings and a priority of the specified value. 
        /// 
        /// The default priority for a URL is 0.
        /// </summary>
        /// <param name="url">The URL String being added.</param>
        /// <param name="testSettings">The priority of the URL being added. If there is a match conflict, the URL with a higher priority will be used.</param>
        /// <param name="priority"> The TestSettings corresponding to the URL String.</param>
        public void AddUrl(string url, IEnumerable<TestSetting> testSettings, int priority = 0)
        {
            var key = new UrlKey(UrlType.DEFAULT, testSettings, priority);
            this.AddUrl(url, key);
        }

        /// <summary>
        /// Adds the specified URL to the URL HashMap with a UrlKey corresponding to the specified URL type
        /// and the specified TestSettings and a priority of the specified value.
        /// 
        /// The default priority for a URL is 0.
        /// </summary>
        /// <param name="url">The URL String being added.</param>
        /// <param name="urlType">The priority of the URL being added. If there is a match conflict, the URL with a higher priority will be used.</param>
        /// <param name="testSettings">The type of the URL being added.</param>
        /// <param name="priority">The TestSettings corresponding to the URL String.</param>
        public void AddUrl(string url, IUrlType urlType, IEnumerable<TestSetting> testSettings, int priority = 0)
        {
            var key = new UrlKey(urlType, testSettings, priority);
            this.AddUrl(url, key);
        }

        /// <summary>
        /// Adds the specified URL to the URL HashMap with a UrlKey corresponding to the default URL type
        /// and the specified TestSettings and a priority of the specified value. 
        /// 
        /// The default priority for a URL is 0.
        /// </summary>
        /// <param name="url">The URL String being added.</param>
        /// <param name="testSetting">The priority of the URL being added. If there is a match conflict, the URL with a higher priority will be used.</param>
        /// <param name="priority"> The TestSettings corresponding to the URL String.</param>
        public void AddUrl(string url, TestSetting testSetting, int priority = 0)
        {
            var testSettings = new List<TestSetting>()
            {
                testSetting
            };
            var key = new UrlKey(UrlType.DEFAULT, testSettings, priority);
            this.AddUrl(url, key);
        }

        /// <summary>
        /// Adds the specified URL to the URL HashMap with a UrlKey corresponding to the specified URL type
        /// and the specified TestSettings and a priority of the specified value.
        /// 
        /// The default priority for a URL is 0.
        /// </summary>
        /// <param name="url">The URL String being added.</param>
        /// <param name="urlType">The priority of the URL being added. If there is a match conflict, the URL with a higher priority will be used.</param>
        /// <param name="testSetting">The type of the URL being added.</param>
        /// <param name="priority">The TestSettings corresponding to the URL String.</param>
        public void AddUrl(string url, IUrlType urlType, TestSetting testSetting, int priority = 0)
        {
            var testSettings = new List<TestSetting>()
            {
                testSetting
            };
            var key = new UrlKey(urlType, testSettings, priority);
            this.AddUrl(url, key);
        }

        /// <summary>
        /// Adds the specified URL to the URL Dictionary with the specified UrlKey. 
        /// </summary>
        /// <param name="url">The URL String being added.</param>
        /// <param name="urlKey">The UrlKey for the URL being added.</param>
        public void AddUrl(string url, UrlKey urlKey)
        {
            this.Urls.Add(urlKey, url);
        }

        /*----------------- Getter Functions -----------------*/

        /// <summary>
        /// Return the default URL for the default test settings.
        /// The specified test settings don't need to match exactly.
        /// If all the test settings match the corresponding test settings of a UrlKey in the HashMap, it is considered a "match". 
        /// If there are multiple matches, it will return the URL with a higher priority.
        /// If multiple matches have the same priority, it will then look at which match was more specific (had more matching test settings).
        /// </summary>
        /// <returns> The default URL for the default test settings.</returns>
        /// <exception cref="ArgumentException">Thrown if no URL is found matching the specified identifiers.</exception>
        public string GetUrl()
        {
            return this.GetUrl(this.DefaultTestSettings);
        }

        /// <summary>
        /// Returns the default URL for the specified test settings.
        /// The specified test settings don't need to match exactly.
        /// If all the test settings match the corresponding test settings of a UrlKey in the HashMap, it is considered a "match".
        /// If there are multiple matches, it will return the URL with a higher priority.
        /// If multiple matches have the same priority, it will then look at which match was more specific (had more matching test settings).
        /// </summary>
        /// <param name="testSettings">The test settings for the desired URL.</param>
        /// <returns>The default URL for the specified test settings.</returns>
        /// <exception cref="ArgumentException">Thrown if no URL is found matching the specified identifiers.</exception>
        public string GetUrl(TestSettings testSettings)
        {
            return this.GetUrl(testSettings.GetSettings());
        }

        /// <summary>
        /// Returns the default URL for the specified test settings.
        /// The specified test settings don't need to match exactly.
        /// If all the test settings match the corresponding test settings of a UrlKey in the HashMap, it is considered a "match".
        /// If there are multiple matches, it will return the URL with a higher priority. 
        /// If multiple matches have the same priority, it will then look at which match was more specific (had more matching test settings).
        /// </summary>
        /// <param name="testSettings"> The test settings for the desired URL.</param>
        /// <returns>The default URL for the specified test settings.</returns>
        /// <exception cref="ArgumentException">Thrown if no URL is found matching the specified identifiers.</exception>
        public string GetUrl(TestSetting[] testSettings)
        {
            var key = new Framework.Core.Url.UrlKey(UrlType.DEFAULT, testSettings);
            return this.GetUrl(key);
        }

        /// <summary>
        /// Returns a URL corresponding to the specified UrlKey.
        /// The specified UrlKey doesn't need to be an exact match.
        /// If all the values in the UrlKey match the corresponding values of a UrlKey in the HashMap, it is considered a "match". 
        /// If there are multiple matches, it will return the URL with a higher priority.
        /// If multiple matches have the same priority, it will then look at which match was more specific (had more matching test settings).
        /// </summary>
        /// <param name="urlType">The type of URL desired.</param>
        /// <returns>A URL of the specified type for the default test settings.</returns>
        /// <exception cref="ArgumentException">Thrown if no URL is found matching the specified identifiers.</exception>
        public string GetUrl(Framework.Core.Url.IUrlType urlType)
        {
            return this.GetUrl(urlType, this.DefaultTestSettings);
        }

        /// <summary>
        /// Returns a URL corresponding to the specified UrlKey.
        /// The specified UrlKey doesn't need to be an exact match.
        /// If all the values in the UrlKey match the corresponding values of a UrlKey in the HashMap, it is considered a "match". 
        /// If there are multiple matches, it will return the URL with a higher priority.
        /// If multiple matches have the same priority, it will then look at which match was more specific (had more matching test settings).
        /// </summary>
        /// <param name="urlType">The type of URL desired.</param>
        /// <param name="testSettings">The test settings for the desired URL.</param>
        /// <returns>A URL of the specified type for the specified test settings.</returns>
        /// <exception cref="ArgumentException">Thrown if no URL is found matching the specified identifiers.</exception>
        public String GetUrl(Framework.Core.Url.IUrlType urlType, TestSettings testSettings)
        {
            return this.GetUrl(urlType, testSettings.GetSettings());
        }

        /// <summary>
        /// Returns a URL corresponding to the specified UrlKey.
        /// The specified UrlKey doesn't need to be an exact match.
        /// If all the values in the UrlKey match the corresponding values of a UrlKey in the HashMap, it is considered a "match". 
        /// If there are multiple matches, it will return the URL with a higher priority.
        /// If multiple matches have the same priority, it will then look at which match was more specific (had more matching test settings).
        /// </summary>
        /// <param name="urlType">The type of URL desired.</param>
        /// <param name="testSettings">The test settings for the desired URL.</param>
        /// <returns>A URL of the specified type for the specified test settings.</returns>
        /// <exception cref="ArgumentException">Thrown if no URL is found matching the specified identifiers.</exception>
        public String GetUrl(Framework.Core.Url.IUrlType urlType, TestSetting[] testSettings)
        {
            return this.GetUrl(new Framework.Core.Url.UrlKey(urlType, testSettings));
        }

        /// <summary>
        /// Returns a URL corresponding to the specified UrlKey.
        /// The specified UrlKey doesn't need to be an exact match.
        /// If all the values in the UrlKey match the corresponding values of a UrlKey in the HashMap, it is considered a "match". 
        /// If there are multiple matches, it will return the URL with a higher priority.
        /// If multiple matches have the same priority, it will then look at which match was more specific (had more matching test settings).
        /// </summary>
        /// <param name="urlKey">A UrlKey used to locate URLs.</param>
        /// <returns>A URL corresponding to the specified UrlKey.</returns>
        /// <exception cref="ArgumentException">Thrown if no URL is found matching the specified identifiers.</exception>
        public string GetUrl(Framework.Core.Url.UrlKey urlKey)
        {
            String url = null;
            int urlPriority = -1;
            int urlIdMatches = -1;
            foreach (var entry in this.Urls.Keys)
            {
                Framework.Core.Url.UrlKey currentKey = entry;
                string currentUrl = this.Urls[currentKey];
                int matchCount = Framework.Core.Url.UrlKey.Matches(urlKey, currentKey);
                if (matchCount > -1)
                {
                    int currentPriority = currentKey.GetPriority();
                    if ((currentPriority > urlPriority) ||
                        (((currentPriority == urlPriority)) && (matchCount > urlIdMatches)))
                    {
                        url = currentUrl;
                        urlPriority = currentPriority;
                        urlIdMatches = matchCount;
                    }
                }
            }
            if (url == null)
            {
                throw new ArgumentException("No URL found matching the UrlKey: " + urlKey);
            }
            return url;
        }

        /// <summary>
        /// Returns the default URL for the specified test settings with the specified replaceValues inserted. 
        /// The specified test settings don't need to match exactly.
        /// If all the test settings match the corresponding test settings of a UrlKey in the HashMap, it is considered a "match".
        /// If there are multiple matches, it will return the URL with a higher priority.
        /// If multiple matches have the same priority, it will then look at which match was more specific (had more matching test settings).
        /// </summary>
        /// <param name="replaceValues">
        /// An array of Object arguments that should be inserted into the URL.
        /// To specify where a string should be inserted into a URL, use: "{x}" where "x" is the 0-based index of the string you want to insert.
        /// </param>
        /// <returns>The default URL for the default test settings.</returns>
        public String GetUrl(params string[] replaceValues)
        {
            return this.GetUrl(this.DefaultTestSettings, replaceValues);
        }

        /// <summary>
        /// Returns the default URL for the specified test settings with the specified replaceValues inserted. 
        /// The specified test settings don't need to match exactly.
        /// If all the test settings match the corresponding test settings of a UrlKey in the HashMap, it is considered a "match".
        /// If there are multiple matches, it will return the URL with a higher priority.
        /// If multiple matches have the same priority, it will then look at which match was more specific (had more matching test settings).
        /// </summary>
        /// <param name="testSettings">The test settings for the desired URL.</param>
        /// <param name="replaceValues">
        /// An array of Object arguments that should be inserted into the URL.
        /// To specify where a string should be inserted into a URL, use: "{x}" where "x" is the 0-based index of the string you want to insert.
        /// </param>
        /// <returns> The default URL for the specified test settings.</returns>
        public string GetUrl(TestSettings testSettings, params string[] replaceValues)
        {
            return this.GetUrl(testSettings.GetSettings(), replaceValues);
        }

        /// <summary>
        /// Returns the default URL for the specified test settings with the specified replaceValues inserted. 
        /// The specified test settings don't need to match exactly.
        /// If all the test settings match the corresponding test settings of a UrlKey in the HashMap, it is considered a "match".
        /// If there are multiple matches, it will return the URL with a higher priority.
        /// If multiple matches have the same priority, it will then look at which match was more specific (had more matching test settings).
        /// </summary>
        /// <param name="testSettings">The test settings for the desired URL.</param>
        /// <param name="replaceValues">
        /// An array of Object arguments that should be inserted into the URL.
        /// To specify where a string should be inserted into a URL, use: "{x}" where "x" is the 0-based index of the string you want to insert.
        /// </param>
        /// <returns>The default URL for the specified test settings.</returns>
        public string GetUrl(TestSetting[] testSettings, params string[] replaceValues)
        {
            return this.GetUrl(new Framework.Core.Url.UrlKey(UrlType.DEFAULT, testSettings), true, replaceValues);
        }

        /// <summary>
        /// Returns the default URL for the specified test settings with the specified replaceValues inserted. 
        /// The specified test settings don't need to match exactly.
        /// If all the test settings match the corresponding test settings of a UrlKey in the HashMap, it is considered a "match".
        /// If there are multiple matches, it will return the URL with a higher priority.
        /// If multiple matches have the same priority, it will then look at which match was more specific (had more matching test settings).
        /// </summary>
        /// <param name="withUriScheme">Determines whether or not the URL has the scheme in front (I.e. "http://").</param>
        /// <param name="replaceValues">
        /// An array of Object arguments that should be inserted into the URL.
        /// To specify where a string should be inserted into a URL, use: "{x}" where "x" is the 0-based index of the string you want to insert.
        /// </param>
        /// <returns>The default URL for the default test settings with the specified replaceValues inserted.</returns>
        public string GetUrl(bool withUriScheme, params string[] replaceValues)
        {
            return this.GetUrl(this.DefaultTestSettings, withUriScheme, replaceValues);
        }

        /// <summary>
        /// Returns the default URL for the specified test settings with the specified replaceValues inserted. 
        /// The specified test settings don't need to match exactly.
        /// If all the test settings match the corresponding test settings of a UrlKey in the HashMap, it is considered a "match".
        /// If there are multiple matches, it will return the URL with a higher priority.
        /// If multiple matches have the same priority, it will then look at which match was more specific (had more matching test settings).
        /// </summary>
        /// <param name="testSettings">The test settings for the desired URL.</param>
        /// <param name="withUriScheme">Determines whether or not the URL has the scheme in front (I.e. "http://").</param>
        /// <param name="replaceValues">
        /// An array of Object arguments that should be inserted into the URL.
        /// To specify where a string should be inserted into a URL, use: "{x}" where "x" is the 0-based index of the string you want to insert.
        /// </param>
        /// <returns>The default URL for the specified test settings with the specified replaceValues inserted.</returns>
        public string GetUrl(TestSettings testSettings, bool withUriScheme, params string[] replaceValues)
        {
            return this.GetUrl(testSettings.GetSettings(), withUriScheme, replaceValues);
        }

        /// <summary>
        /// Returns the default URL for the specified test settings with the specified replaceValues inserted. 
        /// The specified test settings don't need to match exactly.
        /// If all the test settings match the corresponding test settings of a UrlKey in the HashMap, it is considered a "match".
        /// If there are multiple matches, it will return the URL with a higher priority.
        /// If multiple matches have the same priority, it will then look at which match was more specific (had more matching test settings).
        /// </summary>
        /// <param name="testSettings">The test settings for the desired URL.</param>
        /// <param name="withUriScheme">Determines whether or not the URL has the scheme in front (I.e. "http://").</param>
        /// <param name="replaceValues">
        /// An array of Object arguments that should be inserted into the URL.
        /// To specify where a string should be inserted into a URL, use: "{x}" where "x" is the 0-based index of the string you want to insert.
        /// </param>
        /// <returns>The default URL for the specified test settings with the specified replaceValues inserted.</returns>
        public string GetUrl(TestSetting[] testSettings, bool withUriScheme, params string[] replaceValues)
        {

            return this.GetUrl(new Framework.Core.Url.UrlKey(UrlType.DEFAULT, testSettings), withUriScheme, replaceValues);
        }

        /// <summary>
        /// Returns the default URL for the specified test settings with the specified replaceValues inserted. 
        /// The specified test settings don't need to match exactly.
        /// If all the test settings match the corresponding test settings of a UrlKey in the HashMap, it is considered a "match".
        /// If there are multiple matches, it will return the URL with a higher priority.
        /// If multiple matches have the same priority, it will then look at which match was more specific (had more matching test settings).
        /// </summary>
        /// <param name="urlType">The type of URL desired.</param>
        /// <param name="testSettings">The test settings for the desired URL.</param>
        /// <param name="replaceValues">
        /// An array of Object arguments that should be inserted into the URL.
        /// To specify where a string should be inserted into a URL, use: "{x}" where "x" is the 0-based index of the string you want to insert.
        /// </param>
        /// <returns>A URL of the specified type for the specified test settings with the specified replaceValues inserted.</returns>
        public String GetUrl(Framework.Core.Url.IUrlType urlType, TestSettings testSettings, params string[] replaceValues)
        {
            return this.GetUrl(urlType, testSettings.GetSettings(), replaceValues);
        }

        /// <summary>
        /// Returns the default URL for the specified test settings with the specified replaceValues inserted. 
        /// The specified test settings don't need to match exactly.
        /// If all the test settings match the corresponding test settings of a UrlKey in the HashMap, it is considered a "match".
        /// If there are multiple matches, it will return the URL with a higher priority.
        /// If multiple matches have the same priority, it will then look at which match was more specific (had more matching test settings).
        /// </summary>
        /// <param name="urlType">The type of URL desired.</param>
        /// <param name="testSettings">The test settings for the desired URL.</param>
        /// <param name="replaceValues">
        /// An array of Object arguments that should be inserted into the URL.
        /// To specify where a string should be inserted into a URL, use: "{x}" where "x" is the 0-based index of the string you want to insert.
        /// </param>
        /// <returns>A URL of the specified type for the specified test settings with the specified replaceValues inserted.</returns>
        public String GetUrl(Framework.Core.Url.IUrlType urlType, TestSetting[] testSettings, params string[] replaceValues)
        {
            return this.GetUrl(new Framework.Core.Url.UrlKey(urlType, testSettings), true, replaceValues);
        }

        /// <summary>
        /// Returns the default URL for the specified test settings with the specified replaceValues inserted. 
        /// The specified test settings don't need to match exactly.
        /// If all the test settings match the corresponding test settings of a UrlKey in the HashMap, it is considered a "match".
        /// If there are multiple matches, it will return the URL with a higher priority.
        /// If multiple matches have the same priority, it will then look at which match was more specific (had more matching test settings).
        /// </summary>
        /// <param name="urlType">The type of URL desired.</param>
        /// <param name="testSettings">The test settings for the desired URL.</param>
        /// <param name="withUriScheme">Determines whether or not the URL has the scheme in front (I.e. "http://").</param>
        /// <param name="replaceValues">
        /// An array of Object arguments that should be inserted into the URL.
        /// To specify where a string should be inserted into a URL, use: "{x}" where "x" is the 0-based index of the string you want to insert.
        /// </param>
        /// <returns>A URL of the specified type for the specified test settings with the specified replaceValues inserted.</returns>
        public String GetUrl(Framework.Core.Url.IUrlType urlType, TestSettings testSettings, bool withUriScheme,
            params string[] replaceValues)
        {
            return this.GetUrl(urlType, testSettings.GetSettings(), withUriScheme, replaceValues);
        }

        /// <summary>
        /// Returns the default URL for the specified test settings with the specified replaceValues inserted. 
        /// The specified test settings don't need to match exactly.
        /// If all the test settings match the corresponding test settings of a UrlKey in the HashMap, it is considered a "match".
        /// If there are multiple matches, it will return the URL with a higher priority.
        /// If multiple matches have the same priority, it will then look at which match was more specific (had more matching test settings).
        /// </summary>
        /// <param name="urlType">The type of URL desired.</param>
        /// <param name="testSettings">The test settings for the desired URL.</param>
        /// <param name="withUriScheme">Determines whether or not the URL has the scheme in front (I.e. "http://").</param>
        /// <param name="replaceValues">
        /// An array of Object arguments that should be inserted into the URL.
        /// To specify where a string should be inserted into a URL, use: "{x}" where "x" is the 0-based index of the string you want to insert.
        /// </param>
        /// <returns>A URL of the specified type for the specified test settings with the specified replaceValues inserted.</returns>
        public string GetUrl(Framework.Core.Url.IUrlType urlType, TestSetting[] testSettings, bool withUriScheme,
            params string[] replaceValues)
        {
            return this.GetUrl(new Framework.Core.Url.UrlKey(urlType, testSettings), withUriScheme, replaceValues);
        }

        /// <summary>
        /// Returns the default URL for the specified test settings with the specified replaceValues inserted. 
        /// The specified test settings don't need to match exactly.
        /// If all the test settings match the corresponding test settings of a UrlKey in the HashMap, it is considered a "match".
        /// If there are multiple matches, it will return the URL with a higher priority.
        /// If multiple matches have the same priority, it will then look at which match was more specific (had more matching test settings).
        /// </summary>
        /// <param name="urlKey">A UrlKey used to locate URLs.</param>
        /// <param name="replaceValues">
        /// An array of Object arguments that should be inserted into the URL.
        /// To specify where a string should be inserted into a URL, use: "{x}" where "x" is the 0-based index of the string you want to insert.
        /// </param>
        /// <returns>A URL corresponding to the specified UrlKey with the specified replaceValues inserted.</returns>
        public String GetUrl(Framework.Core.Url.UrlKey urlKey, params string[] replaceValues)
        {
            return this.GetUrl(urlKey, true, replaceValues);
        }

        /// <summary>
        /// Returns the default URL for the specified test settings with the specified replaceValues inserted. 
        /// The specified test settings don't need to match exactly.
        /// If all the test settings match the corresponding test settings of a UrlKey in the HashMap, it is considered a "match".
        /// If there are multiple matches, it will return the URL with a higher priority.
        /// If multiple matches have the same priority, it will then look at which match was more specific (had more matching test settings).
        /// </summary>
        /// <param name="urlKey">A UrlKey used to locate URLs.</param>
        /// <param name="withUriScheme">Determines whether or not the URL has the scheme in front (I.e. "http://").</param>
        /// <param name="replaceValues">
        /// An array of Object arguments that should be inserted into the URL.
        /// To specify where a string should be inserted into a URL, use: "{x}" where "x" is the 0-based index of the string you want to insert.
        /// </param>
        /// <returns>A URL corresponding to the specified UrlKey with the specified replaceValues inserted.</returns>
        public String GetUrl(Framework.Core.Url.UrlKey urlKey, bool withUriScheme, params string[] replaceValues)
        {
            // If the scheme shouldn't be there, remove it
            String returnUrl = this.GetUrl(urlKey);
            if (!withUriScheme)
            {
                int index = returnUrl.IndexOf("://", StringComparison.Ordinal);
                if (index != -1)
                {
                    returnUrl = returnUrl.Substring(index + 3);
                }
            }

            // If there are replace values, make the replacements
            if (replaceValues.Length > 0)
            {
                returnUrl = String.Format(returnUrl, replaceValues);
            }
            return returnUrl;
        }
    }
}
