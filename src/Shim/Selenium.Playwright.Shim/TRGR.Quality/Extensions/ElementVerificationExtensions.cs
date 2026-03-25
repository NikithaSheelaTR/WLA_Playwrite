using System;
using System.Linq;
using OpenQA.Selenium;

namespace TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions
{
    /// <summary>
    /// Extension methods for verifying element state via IWebDriver.
    /// </summary>
    public static class ElementVerificationExtensions
    {
        public static bool AreNotDisplayed(this IWebDriver driver, By by)
        {
            try
            {
                var elements = driver.FindElements(by);
                return elements.Count == 0 || elements.All(e => !e.Displayed);
            }
            catch (NoSuchElementException) { return true; }
        }

        public static bool IsDisplayed(this IWebDriver driver, params By[] elementBy)
        {
            try
            {
                var element = driver.GetElementByChainedBys(elementBy);
                return element.Displayed;
            }
            catch (NoSuchElementException) { return false; }
        }

        public static bool IsDisplayed(this IWebDriver driver, IWebElement containerElement, params By[] elementBy)
        {
            try
            {
                var element = containerElement.FindElementByChainedBys(elementBy);
                return element.Displayed;
            }
            catch (NoSuchElementException) { return false; }
        }

        public static bool IsDisplayed(this IWebDriver driver, IWebElement element)
        {
            try { return element.Displayed; }
            catch { return false; }
        }

        public static bool IsElementDisplayed(this IWebDriver driver, By by, int timeoutInSeconds = 0)
        {
            try
            {
                if (timeoutInSeconds > 0)
                {
                    var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                    var element = wait.Until(d =>
                    {
                        try
                        {
                            var el = d.FindElement(by);
                            return el.Displayed ? el : null;
                        }
                        catch { return null; }
                    });
                    return element != null;
                }
                return driver.FindElement(by).Displayed;
            }
            catch { return false; }
        }

        public static bool IsElementPresent(this IWebDriver driver, params By[] elementBy)
        {
            try
            {
                driver.GetElementByChainedBys(elementBy);
                return true;
            }
            catch (NoSuchElementException) { return false; }
        }

        public static bool IsElementPresent(this IWebDriver driver, IWebElement containerElement, params By[] elementBy)
        {
            try
            {
                containerElement.FindElementByChainedBys(elementBy);
                return true;
            }
            catch (NoSuchElementException) { return false; }
        }

        public static bool IsElementPresent(this IWebDriver driver, By elementBy, int timeout)
        {
            try
            {
                var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromMilliseconds(timeout));
                wait.Until(d =>
                {
                    try { return d.FindElement(elementBy) != null; }
                    catch { return false; }
                });
                return true;
            }
            catch { return false; }
        }

        public static bool IsElementPresent(this IWebDriver driver, IWebElement containerElement, By elementBy, int timeout)
        {
            try
            {
                var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromMilliseconds(timeout));
                wait.Until(d =>
                {
                    try { return containerElement.FindElement(elementBy) != null; }
                    catch { return false; }
                });
                return true;
            }
            catch { return false; }
        }

    }
}
