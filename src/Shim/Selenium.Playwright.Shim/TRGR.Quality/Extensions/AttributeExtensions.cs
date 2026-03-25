using OpenQA.Selenium;

namespace TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions
{
    /// <summary>
    /// Extension methods for attribute retrieval via IWebDriver.
    /// </summary>
    public static class AttributeExtensions
    {
        public static string GetAttribute(this IWebDriver driver, string attributeName, params By[] elementBy)
        {
            var element = driver.GetElementByChainedBys(elementBy);
            return element.GetAttribute(attributeName);
        }

        public static string GetAttribute(this IWebDriver driver, string attributeName, IWebElement container, params By[] elementBy)
        {
            var element = container.FindElementByChainedBys(elementBy);
            return element.GetAttribute(attributeName);
        }

        public static string GetHiddenText(this IWebDriver driver, IWebElement container, params By[] elementBys)
        {
            var element = container.FindElementByChainedBys(elementBys);
            return (string)((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].textContent;", element) ?? string.Empty;
        }
    }
}
