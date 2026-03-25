namespace Framework.Core.CommonTypes.Enums.Setup
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Enum for Browser
    /// </summary>
    public class BrowserUnderTest
    {
        private string _name;
        private string _webDriverName;
        private string _abbreviation;

        // ReSharper disable InconsistentNaming

        /// <summary>
        /// Edge
        /// </summary>
        public static BrowserUnderTest EDGE = new BrowserUnderTest("EDGE", "Edge", "EDGE");

        /// <summary>
        /// 
        /// </summary>
        public static BrowserUnderTest INTERNET_EXPLORER = new BrowserUnderTest("Internet Explorer", "Internet explorer", "IE");

        /// <summary>
        /// 
        /// </summary>
        public static BrowserUnderTest FIREFOX_AURORA = new BrowserUnderTest("Firefox Aurora", null, "FFA");

        /// <summary>
        /// 
        /// </summary>
        public static BrowserUnderTest FIREFOX_BETA = new BrowserUnderTest("Firefox Beta", null, "FFB");

        /// <summary>
        /// 
        /// </summary>
        public static BrowserUnderTest FIREFOX = new BrowserUnderTest("Firefox", "Firefox", "FF");

        /// <summary>
        /// 
        /// </summary>
        public static BrowserUnderTest CHROME_CANARY = new BrowserUnderTest("Chrome Canary", null, "CHC");

        /// <summary>
        /// 
        /// </summary>
        public static BrowserUnderTest CHROME = new BrowserUnderTest("Chrome", "Chrome", "CH");

        /// <summary>
        /// 
        /// </summary>
        public static BrowserUnderTest SAFARI = new BrowserUnderTest("Safari", "Safari", "SF");

        // ReSharper restore InconsistentNaming

        /// <summary>
        /// Collection containing all supported image filetypes
        /// </summary>
        public static List<BrowserUnderTest> Values = new List<BrowserUnderTest>
        {
            EDGE,
            INTERNET_EXPLORER,
            FIREFOX_AURORA,
            FIREFOX_BETA,
            FIREFOX,
            CHROME_CANARY,
            CHROME,
            SAFARI,
        };

        private BrowserUnderTest(string name, string webDriverName, string abbreviation)
        {
            this._name = name;
            this._webDriverName = webDriverName;
            this._abbreviation = abbreviation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return this._name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetWebDriverName()
        {
            return this._webDriverName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetAbbreviation()
        {
            return this._abbreviation;
        }

        /// <summary>
        /// Loops through all of the BrowserUnderTests and returns the one that matches the specified name (or abbreviation).
        /// </summary>
        /// <param name="name">The BrowserUnderTest corresponding to the specified name (or abbreviation).</param>
        /// <returns>Thrown if no BrowserUnderTest could be found matching the specified name (or abbreviation).</returns>
        public static BrowserUnderTest FromName(string name)
        {
            foreach (var browserUnderTest in Values)
            {
                if (browserUnderTest.GetName().ToLower().Equals(name.ToLower()) || browserUnderTest.GetAbbreviation().ToLower().Equals(name.ToLower()))
                {
                    return browserUnderTest;
                }
            }
            throw new ArgumentException("No BrowserUnderTest enum could be found corresponding to the name: \'" + name + "\'.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.GetAbbreviation();
        }
    }
}
