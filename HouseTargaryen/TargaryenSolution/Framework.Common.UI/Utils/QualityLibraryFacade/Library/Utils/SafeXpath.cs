namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils
{
    using OpenQA.Selenium;

    /// <summary>
    /// This provides some string utilities
    /// </summary>
    public static class SafeXpath
    {
        /// <summary>
        /// Formats an xpath based on the string and list of values, then returns a By object using those values
        /// </summary>
        /// <param name="format">The xpath format string.  Must include {0}, {1}, {n} where the values go</param>
        /// <param name="values">The values to escape and insert in the xpath format string</param>
        /// <returns>By object</returns>
        public static By BySafeXpath(string format, params object[] values)
        {
            return Core.SafeXpath.BySafeXpath(format, values);
        }
    }
}