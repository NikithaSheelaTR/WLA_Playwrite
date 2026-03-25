using OpenQA.Selenium;

namespace TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions
{
    /// <summary>
    /// Extension methods for hovering elements via IWebDriver.
    /// </summary>
    public static class HoverExtensions
    {
        public static void Hover(this IWebDriver driver, By by)
        {
            var element = driver.FindElement(by);
            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.MoveToElement(element).Perform();
        }

        public static void Hover(this IWebDriver driver, IWebElement element)
        {
            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.MoveToElement(element).Perform();
        }

        public static void HoverOut(this IWebDriver driver, IWebElement element)
        {
            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            // Move to body to hover out of element
            var body = driver.FindElement(By.TagName("body"));
            actions.MoveToElement(body).Perform();
        }

        public static void HoverUsingJavaScript(this IWebDriver driver, By by)
        {
            var element = driver.FindElement(by);
            ((IJavaScriptExecutor)driver).ExecuteScript(
                "var event = new MouseEvent('mouseover', {bubbles: true, cancelable: true}); arguments[0].dispatchEvent(event);",
                element);
        }

        public static void SeleniumHover(this IWebDriver driver, IWebElement element)
        {
            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.MoveToElement(element).Perform();
        }
    }
}
