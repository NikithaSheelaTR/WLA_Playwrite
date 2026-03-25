namespace Framework.Core.Url
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// 
    /// </summary>
    public class UrlUtils
    {
        /// <summary>
        /// Retrieves the given String URL's extension
        /// xtension = stripped of both main site(.com) and query strings
        /// </summary>
        /// <param name="url">Given String URL.</param>
        /// <returns>The URL Extension.</returns>
        public static String GetUrlExtensionString(string url)
        {
            return Regex.Split(url.Split("com".ToCharArray(), StringSplitOptions.None)[1], "\\?")[0];

        }

        /// <summary>
        /// Gets the full query string given the provided URL.
        /// </summary>
        /// <param name="url"> The URL you want the query string for.</param>
        /// <returns>The query string in the provided URL.</returns>
        public static String GetUrlQueryString(String url)
        {
            String returnValue = "";

            // Remove the fragment identifier section of the URL if it exists
            if (url.Contains("#"))
            {
                url = url.Substring(0, url.IndexOf("#", StringComparison.Ordinal));
            }

            // Check to see if a query string exists and set its value if it does
            if (url.Contains("?"))
            {
                returnValue = Regex.Split(url, "\\?")[1];
            }

            return returnValue;
        }

        /// <summary>
        /// Gets the data value of the specified data name within the provided URL.
        /// </summary>
        /// <param name="url">The URL you want the query string value for.</param>
        /// <param name="dataName">The name of the data pair that you want the value for.</param>
        /// <returns>The data value of the desired data pair.</returns>
        public static string GetUrlQueryStringDataValue(string url, string dataName)
        {
            String returnValue = null;

            // Break the URL up into the different query sections
            url = UrlUtils.GetUrlQueryString(url);
            String[] querySections;
            if (url.Contains("&"))
            {
                querySections = url.Split('&');
            }
            else
            {
                querySections = new[] { url };
            }

            // Loop through the different sections of the query and look for a data
            // name that matches the specified name
            foreach (var querySection in querySections)
            {
                // If the data name matches, return the value associated with that
                // data name
                String[] querySectionComponents = querySection.Split('=');
                if (querySectionComponents[0].Equals(dataName, StringComparison.CurrentCultureIgnoreCase))
                {
                    returnValue = querySectionComponents[1];
                    break;
                }
            }

            return returnValue;
        }
    }
}
