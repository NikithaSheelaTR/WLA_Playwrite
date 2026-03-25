using System;
using System.Threading;
using OpenQA.Selenium;

namespace TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions
{
    /// <summary>
    /// Extension methods for JavaScript operations via IWebDriver.
    /// </summary>
    public static class JavaScriptWebDriverExtensions
    {
        public static void ClickUsingJavaScript(this IWebDriver driver, By by)
        {
            var element = driver.FindElement(by);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
        }

        public static object ExecuteScript(this IWebDriver driver, string script, params object[] arguments)
        {
            return ((IJavaScriptExecutor)driver).ExecuteScript(script, arguments);
        }

        public static IJavaScriptExecutor Scripts(this IWebDriver driver)
        {
            return (IJavaScriptExecutor)driver;
        }

        public static bool WaitForAnimation(this IWebDriver driver, int timeoutInMilliseconds = 30000)
        {
            try
            {
                var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromMilliseconds(timeoutInMilliseconds));
                return wait.Until(d =>
                {
                    var result = ((IJavaScriptExecutor)d).ExecuteScript(
                        "return document.readyState === 'complete' && (typeof jQuery === 'undefined' || jQuery.active === 0);");
                    return result is bool b && b;
                });
            }
            catch { return true; }
        }

        public static bool WaitForJavaScript(this IWebDriver driver, int timeoutInMilliseconds = 30000)
        {
            try
            {
                var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromMilliseconds(timeoutInMilliseconds));
                return wait.Until(d =>
                {
                    var result = ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState;");
                    return result?.ToString() == "complete";
                });
            }
            catch { return true; }
        }

        public static void GeckoWait(this IWebDriver driver)
        {
            Thread.Sleep(500);
        }

        public static bool WaitForPageLoad(this IWebDriver driver, int timeoutInMilliseconds = 30000)
        {
            try
            {
                var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromMilliseconds(timeoutInMilliseconds));
                return wait.Until(d =>
                {
                    var result = ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState;");
                    return result?.ToString() == "complete";
                });
            }
            catch { return true; }
        }
    }
}
