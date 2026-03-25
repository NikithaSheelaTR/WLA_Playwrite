using OpenQA.Selenium;

namespace TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Utils.Core
{
    /// <summary>
    /// Stub SafeXpath for QedArsenal compatibility.
    /// Provides XPath string escaping and By construction.
    /// </summary>
    public static class SafeXpath
    {
        public static By BySafeXpath(string format, params object[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = ConvertToXpathSafeString(values[i].ToString());
            }

            string xp = string.Format(format, values);
            return By.XPath(xp);
        }

        private static string ConvertToXpathSafeString(string value)
        {
            if (!value.Contains("\""))
            {
                return "\"" + value + "\"";
            }

            if (!value.Contains("'"))
            {
                return "'" + value + "'";
            }

            var sb = new System.Text.StringBuilder("concat(");
            foreach (string sub in value.Split('"'))
            {
                if (sub != "")
                {
                    sb.Append("\"" + sub + "\",'\"',");
                }
                else
                {
                    sb.Append("'\"',");
                }
            }

            string result = sb.ToString();
            result = result.Remove(result.LastIndexOf(",'")) + ")";
            return result;
        }
    }
}
