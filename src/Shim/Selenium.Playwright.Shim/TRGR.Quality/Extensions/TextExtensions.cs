using System;
using System.Linq;
using OpenQA.Selenium;
using TRGR.Quality.QedArsenal.QualityLibrary.Core.Enums.WebDriver;

namespace TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions
{
    /// <summary>
    /// Extension methods for text retrieval via IWebDriver.
    /// </summary>
    public static class TextExtensions
    {
        public static string GetText(this IWebDriver driver, params By[] elementBy)
        {
            var element = driver.GetElementByChainedBys(elementBy);
            return GetTextFromElement(element);
        }

        public static string GetText(this IWebDriver driver, IWebElement element)
        {
            return GetTextFromElement(element);
        }

        public static string GetText(this IWebDriver driver, IWebElement element, params TextSearchOption[] textSearchOptionsQl)
        {
            if (textSearchOptionsQl != null && textSearchOptionsQl.Contains(TextSearchOption.Value))
                return element.GetAttribute("value") ?? string.Empty;
            if (textSearchOptionsQl != null && textSearchOptionsQl.Contains(TextSearchOption.InnerHtml))
                return element.GetAttribute("innerHTML") ?? string.Empty;
            return GetTextFromElement(element);
        }

        public static string GetText(this IWebDriver driver, By by, IWebElement baseElement, int timeOut)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromMilliseconds(timeOut));
            var element = wait.Until(d =>
            {
                try
                {
                    var el = baseElement.FindElement(by);
                    return el.Displayed ? el : null;
                }
                catch { return null; }
            });
            return GetTextFromElement(element);
        }

        public static string GetImmediateText(this IWebDriver driver, params By[] elementBy)
        {
            var element = driver.GetElementByChainedBys(elementBy);
            var text = (string)((IJavaScriptExecutor)driver).ExecuteScript(
                @"var parent = arguments[0];
                  var child = parent.firstChild;
                  var texts = [];
                  while (child) {
                      if (child.nodeType === 3) texts.push(child.textContent);
                      child = child.nextSibling;
                  }
                  return texts.join('');", element);
            return text ?? string.Empty;
        }

        public static string GetImmediateText(this IWebDriver driver, IWebElement container, params By[] elementBy)
        {
            var element = container.FindElementByChainedBys(elementBy);
            var text = (string)((IJavaScriptExecutor)driver).ExecuteScript(
                @"var parent = arguments[0];
                  var child = parent.firstChild;
                  var texts = [];
                  while (child) {
                      if (child.nodeType === 3) texts.push(child.textContent);
                      child = child.nextSibling;
                  }
                  return texts.join('');", element);
            return text ?? string.Empty;
        }

        public static bool IsTextInElement(this IWebDriver driver, By by, string text)
        {
            try
            {
                var element = driver.FindElement(by);
                return GetTextFromElement(element).Contains(text);
            }
            catch { return false; }
        }

        public static bool IsTextOnPage(this IWebDriver driver, string text)
        {
            return driver.PageSource.Contains(text);
        }

        private static string GetTextFromElement(IWebElement element)
        {
            if (element == null) return string.Empty;

            var text = element.Text;
            if (string.IsNullOrEmpty(text))
            {
                // For input elements, try the value attribute
                var tagName = element.TagName?.ToLower();
                if (tagName == "input" || tagName == "textarea")
                    text = element.GetAttribute("value");
            }
            return text ?? string.Empty;
        }
    }
}
