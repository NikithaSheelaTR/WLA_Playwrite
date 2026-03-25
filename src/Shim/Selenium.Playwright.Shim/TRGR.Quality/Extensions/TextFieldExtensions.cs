using System.Threading;
using OpenQA.Selenium;

namespace TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions
{
    /// <summary>
    /// Extension methods for text field operations via IWebDriver.
    /// </summary>
    public static class TextFieldExtensions
    {
        public static void SetTextField(this IWebDriver driver, string text, params By[] elementBy)
        {
            var element = driver.GetElementByChainedBys(elementBy);
            element.Clear();
            element.SendKeys(text);
        }

        public static void SetTextField(this IWebDriver driver, string text, IWebElement containerElement, params By[] elementBy)
        {
            var element = containerElement.FindElementByChainedBys(elementBy);
            element.Clear();
            element.SendKeys(text);
        }

        public static void SetTextField(this IWebDriver driver, string text, IWebElement element)
        {
            element.Clear();
            element.SendKeys(text);
        }

        public static void SendKeysSlow(this IWebDriver driver, IWebElement element, string text)
        {
            foreach (char c in text)
            {
                element.SendKeys(c.ToString());
                Thread.Sleep(50);
            }
        }
    }
}
