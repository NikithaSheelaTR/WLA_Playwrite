using OpenQA.Selenium;

namespace TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions
{
    /// <summary>
    /// Extension methods for scrolling and window management via IWebDriver.
    /// </summary>
    public static class WindowExtensions
    {
        public static void ScrollIntoView(this IWebDriver driver, By by, int offset = 0)
        {
            var element = driver.FindElement(by);
            ((IJavaScriptExecutor)driver).ExecuteScript(
                $"arguments[0].scrollIntoView({{block: 'center'}}); window.scrollBy(0, {offset});", element);
        }

        public static void ScrollTo(this IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].scrollIntoView({block: 'center'});", element);
        }

        public static void ScrollToTop(this IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 0);");
        }

    }
}
