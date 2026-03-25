namespace Framework.Common.UI.Utils.Core
{
    using System;
    using System.Text;

    using OpenQA.Selenium;

    /// <summary>
    /// This provides some string utilities
    /// </summary>
    public class SafeXpath
    {
        /// <summary>
        /// This converts a string literal and makes it ready for xpaths. It can handle double quotes, single quotes and a mixture of both.
        /// </summary>
        /// <param name="value">Literal you want to convert, DO NOT pass surrounding quotes with it, those will be added</param>
        /// <returns>The literal with quotes or single quotes around it, no need to add more.</returns>
        private static string ConvertToXpathSafeString(string value)
        {
            string returnVal = string.Empty;

            //simplest case, if it contains no double quotes we can just use double quotes an the value will work fine with any single quotes in it
            if (!value.Contains("\""))
            {
                returnVal = "\"" + value + "\"";
            }

            //if there are double quotes, but no single quotes, no problem, just surround with single quotes and it will work fine with double quotes in it
            else if (value.Contains("\"") && !value.Contains("\'"))
            {
                returnVal = "\'" + value + "\'";
            }

            // if there is both a single quote and double quote we need to use the concat function to assemble the string
            else if (value.Contains("\"") && value.Contains("\'"))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("concat(");

                foreach (string sub in value.Split('\"'))
                {
                    //empty substring means there was a quotation mark there
                    if (sub != "")
                    {
                        sb.Append("\"" + sub + "\",'\"',");
                    }
                    else
                    {
                        sb.Append("'\"',");
                    }
                }

                returnVal = sb.ToString();
                returnVal = returnVal.Remove(returnVal.LastIndexOf(",'")) + ")";
            }

            return returnVal;
        }

        /// <summary>
        /// This converts an array of string literals and makes them ready for xpaths. It can handl double quotes, single quotes and a mixture of both.
        /// </summary>
        /// <param name="values">The list of literals yo uwant to convert, do not pass surrounding quotes with them, those will be added</param>
        /// <returns>The list of literals qith the correct quotes around them, no need to add more</returns>
        private static object[] ConvertToXpathSafeString(params object[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = SafeXpath.ConvertToXpathSafeString(values[i].ToString());
            }

            return values;
        }

        /// <summary>
        /// Formats an xpath based on the string and list of values, then returns a By object using those values
        /// </summary>
        /// <param name="format">The xpath format string.  Must include {0}, {1}, {n} where the values go</param>
        /// <param name="values">The values to escape and insert in the xpath format string</param>
        /// <returns>By object</returns>
        public static By BySafeXpath(string format, params object[] values)
        {
            values = SafeXpath.ConvertToXpathSafeString(values);
            string xp = String.Format(format, values);
            return By.XPath(xp);
        }
    }
}
