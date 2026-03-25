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

        public static void ScrollPageToBottom(this IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
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

        public static void ScrollToElementInsideContainer(this IWebDriver driver, By container, By elementToScroll, int offset = 0)
        {
            var containerEl = driver.FindElement(container);
            var element = driver.FindElement(elementToScroll);
            ((IJavaScriptExecutor)driver).ExecuteScript(
                $"var c = arguments[0]; var e = arguments[1]; c.scrollTop = e.offsetTop - c.offsetTop + {offset};",
                containerEl, element);
        }

        public static void ScrollToContainerTop(this IWebDriver driver, By container, int offset = 0)
        {
            var containerEl = driver.FindElement(container);
            ((IJavaScriptExecutor)driver).ExecuteScript(
                $"arguments[0].scrollTop = {offset};", containerEl);
        }
    }
}
