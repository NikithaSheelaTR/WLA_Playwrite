namespace Framework.Core.Url
{
    using System.Collections.Generic;
    using System.Text;

    using Framework.Core.CommonTypes.Settings;

    /// <summary>
    ///  UrlKey class.
    /// 
    /// A class that stores various test settings. This class is used as a key to locate URLs in the HashMap of URLs.
    /// </summary>
    public class UrlKey
    {
        /*----------------- Class Variable Declarations -----------------*/

        private readonly IUrlType _urlType;
        private readonly Dictionary<TestSettingKey, TestSetting> _settings;
        private readonly int _priority;

        /*----------------- Class Constructor Declarations -----------------*/

        /// <summary>
        /// UrlKey constructor.
        /// </summary>
        /// <param name="urlType">The type of the URL.</param>
        /// <param name="priority">The priority of the URL. If there is a match conflict, the URL with a higher priority will be used.</param>
        /// <param name="settings">The settings being used to identify the desired URL.</param>
        public UrlKey(IUrlType urlType, IEnumerable<TestSetting> settings, int priority = 0)
        {
            this._priority = priority;
            this._urlType = urlType;
            this._settings = new Dictionary<TestSettingKey, TestSetting>();
            foreach (var setting in settings)
            {

                if (setting.IsSet())
                {
                    this._settings.Add(setting.GetKey(), setting);
                }
            }
        }

        /*----------------- Getter Functions -----------------*/

        /// <summary>
        /// Returns the priority of the URL.
        /// If there is a match conflict, the URL with a higher priority will be used.
        /// </summary>
        /// <returns>The priority of the URL.</returns>
        public int GetPriority()
        {
            return this._priority;
        }

        /*----------------- Static Functions -----------------*/

        /// <summary>
        /// Checks to see if the specified UrlKeys are matches and how close of a match they are.
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <returns>
        /// An integer representing how close of a match the UrlKeys are. 
        /// If the URL type doesn't match, this will return -1. 
        /// Otherwise, it returns the number of test settings that match between the two being compared.
        /// </returns>
        public static int Matches(UrlKey key1, UrlKey key2)
        {
            int matchCount = 0;

            if (!key1._urlType.GetName().Equals(key2._urlType.GetName()))
            {
                return -1;
            }
            foreach (var entry in key1._settings.Keys)
            {
                TestSettingKey key = entry;
                TestSetting value1 = key1._settings[key];

                TestSetting value2 = null;

                if (key2._settings.ContainsKey(key))
                {
                    value2 = key2._settings[key];
                }

                if (value1 != null && value2 != null)
                {
                    if (value1.Equals(value2))
                    {
                        matchCount++;
                    }
                    else if (value1.IsSet() && value2.IsSet())
                    {
                        return -1;
                    }
                }
            }
            return matchCount;
        }

        /*----------------- Object Functions -----------------*/

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("[UrlKey] { ");
            builder.Append("URL Type = " + this._urlType + ", ");
            builder.Append("Priority = " + this._priority + ", ");
            var testSettings = this._settings.Values;
            foreach (var testSetting in testSettings)
            {
                builder.Append(testSetting + ", ");
            }
            builder.Remove(builder.Length - 2, 2);
            builder.Append(" }");
            return builder.ToString();
        }
    }
}
