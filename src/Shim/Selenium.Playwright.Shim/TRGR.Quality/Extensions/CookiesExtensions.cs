using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions
{
    /// <summary>
    /// Extension methods for cookie operations via IWebDriver.
    /// </summary>
    public static class CookiesExtensions
    {
        public static ReadOnlyCollection<Cookie> GetCookies(this IWebDriver driver)
        {
            return driver.Manage().Cookies.AllCookies;
        }

        public static string GetCookieValue(this IWebDriver driver, string cookieName)
        {
            var cookie = driver.Manage().Cookies.GetCookieNamed(cookieName);
            return cookie?.Value ?? string.Empty;
        }

        public static string GetSiteCookieValue(this IWebDriver driver)
        {
            return driver.GetCookieValue("site");
        }

        public static void RefreshPage(this IWebDriver driver)
        {
            driver.Navigate().Refresh();
        }
    }
}
