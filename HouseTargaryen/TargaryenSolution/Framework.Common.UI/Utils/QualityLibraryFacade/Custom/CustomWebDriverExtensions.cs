namespace Framework.Common.UI.Utils.QualityLibraryFacade.Custom
{
    using Framework.Core.Utils.Execution;

    using OpenQA.Selenium;

    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// The custom extensions.
    /// </summary>
    public static partial class CustomExtensions
    {
        /// <summary>
        /// Returns the value of the "site" cookie (e.g. "b" or "pc1" for Demo)
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>
        /// Returns string.Empty in case the extraction fails.
        /// </returns>
        public static string GetSiteCookieValue(this IWebDriver driver)
        {
            string site = string.Empty;
            SafeMethodExecutor.Execute(() => site = driver.GetCookieValue("SITE"));
            return site;
        }
    }
}
