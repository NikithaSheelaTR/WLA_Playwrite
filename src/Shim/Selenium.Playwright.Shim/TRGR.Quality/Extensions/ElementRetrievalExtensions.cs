using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions
{
    /// <summary>
    /// Extension methods for retrieving elements via IWebDriver.
    /// </summary>
    public static class ElementRetrievalExtensions
    {
        public static IWebElement GetElement(this IWebDriver driver, params By[] elementBys)
        {
            return driver.GetElementByChainedBys(elementBys);
        }

        public static IWebElement GetElement(this IWebDriver driver, By elementBy)
        {
            return driver.FindElement(elementBy);
        }

        public static IWebElement GetElement(this IWebDriver driver, IWebElement container, params By[] elementBys)
        {
            return container.FindElementByChainedBys(elementBys);
        }

        public static IList<IWebElement> GetElements(this IWebDriver driver, params By[] elementsBys)
        {
            if (elementsBys.Length == 1)
                return driver.FindElements(elementsBys[0]).ToList();

            // Navigate to the container, then find elements with the last By
            var containerBys = elementsBys.Take(elementsBys.Length - 1).ToArray();
            var container = driver.GetElementByChainedBys(containerBys);
            return container.FindElements(elementsBys.Last()).ToList();
        }

        public static IReadOnlyCollection<IWebElement> GetElements(this IWebDriver driver, By elementsBy)
        {
            return driver.FindElements(elementsBy);
        }

        public static IList<IWebElement> GetElements(this IWebDriver driver, IWebElement container, params By[] elementsBys)
        {
            if (elementsBys.Length == 1)
                return container.FindElements(elementsBys[0]).ToList();

            var containerBys = elementsBys.Take(elementsBys.Length - 1).ToArray();
            var innerContainer = container.FindElementByChainedBys(containerBys);
            return innerContainer.FindElements(elementsBys.Last()).ToList();
        }

        public static IList<IWebElement> GetAllChildrenElements(this IWebDriver driver,
            IList<IWebElement> parentElements, By childrenBy)
        {
            var allChildren = new List<IWebElement>();
            foreach (var parent in parentElements)
            {
                try
                {
                    allChildren.AddRange(parent.FindElements(childrenBy));
                }
                catch (NoSuchElementException) { }
            }
            return allChildren;
        }

        public static IWebElement GetElementByText(this IWebDriver driver, string searchText,
            TRGR.Quality.QedArsenal.QualityLibrary.Core.Enums.WebDriver.TextSearchOption[] optionsQl,
            params By[] elementBys)
        {
            var elements = driver.GetElements(elementBys);
            return FindElementByText(elements, searchText, optionsQl);
        }

        public static IWebElement GetElementByText(this IWebDriver driver, string searchText,
            TRGR.Quality.QedArsenal.QualityLibrary.Core.Enums.WebDriver.TextSearchOption[] optionsQl,
            IWebElement container, params By[] elementBys)
        {
            var elements = driver.GetElements(container, elementBys);
            return FindElementByText(elements, searchText, optionsQl);
        }

        public static List<IWebElement> GetElementsByText(this IWebDriver driver, string searchText,
            TRGR.Quality.QedArsenal.QualityLibrary.Core.Enums.WebDriver.TextSearchOption[] optionsQl,
            params By[] elementBys)
        {
            var elements = driver.GetElements(elementBys);
            return elements.Where(e => TextMatches(GetElementText(e, optionsQl), searchText, optionsQl)).ToList();
        }

        public static IWebElement GetParentElement(this IWebDriver driver, IWebElement element)
        {
            return (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].parentNode;", element);
        }

        public static IWebElement GetParentElement(this IWebDriver driver, IWebElement element, string containerCss)
        {
            return (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript(
                $"return arguments[0].closest('{containerCss}');", element);
        }

        private static IWebElement FindElementByText(IList<IWebElement> elements, string searchText,
            TRGR.Quality.QedArsenal.QualityLibrary.Core.Enums.WebDriver.TextSearchOption[] options)
        {
            return elements.FirstOrDefault(e => TextMatches(GetElementText(e, options), searchText, options))
                ?? throw new NoSuchElementException($"Could not find element with text: {searchText}");
        }

        private static string GetElementText(IWebElement element,
            TRGR.Quality.QedArsenal.QualityLibrary.Core.Enums.WebDriver.TextSearchOption[] options)
        {
            if (options != null)
            {
                if (options.Contains(TRGR.Quality.QedArsenal.QualityLibrary.Core.Enums.WebDriver.TextSearchOption.Value))
                    return element.GetAttribute("value") ?? string.Empty;
                if (options.Contains(TRGR.Quality.QedArsenal.QualityLibrary.Core.Enums.WebDriver.TextSearchOption.InnerHtml))
                    return element.GetAttribute("innerHTML") ?? string.Empty;
            }
            return element.Text ?? string.Empty;
        }

        private static bool TextMatches(string elementText, string searchText,
            TRGR.Quality.QedArsenal.QualityLibrary.Core.Enums.WebDriver.TextSearchOption[] options)
        {
            if (options != null && options.Contains(TRGR.Quality.QedArsenal.QualityLibrary.Core.Enums.WebDriver.TextSearchOption.TrimWhitespace))
            {
                elementText = elementText.Trim();
                searchText = searchText.Trim();
            }

            var comparison = (options != null && options.Contains(TRGR.Quality.QedArsenal.QualityLibrary.Core.Enums.WebDriver.TextSearchOption.IgnoreCase))
                ? StringComparison.OrdinalIgnoreCase
                : StringComparison.Ordinal;

            if (options != null && options.Contains(TRGR.Quality.QedArsenal.QualityLibrary.Core.Enums.WebDriver.TextSearchOption.Contains))
                return elementText.IndexOf(searchText, comparison) >= 0;

            return string.Equals(elementText, searchText, comparison);
        }
    }
}
