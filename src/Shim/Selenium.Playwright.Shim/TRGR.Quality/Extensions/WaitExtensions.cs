using System;
using System.Threading;
using OpenQA.Selenium;

namespace TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions
{
    /// <summary>
    /// Extension methods for waiting on elements via IWebDriver.
    /// </summary>
    public static class WaitExtensions
    {
        private static int _defaultImplicitWaitSeconds = 10;

        public static void WaitForCondition(this IWebDriver driver, Func<object, bool> condition, int timeOut = 30)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
            wait.Until(d => condition(d));
        }

        public static void DisableTimeout(this IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
        }

        public static void ResetTimeout(this IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_defaultImplicitWaitSeconds);
        }

        public static void SetTimeout(this IWebDriver driver, int waitInSeconds)
        {
            _defaultImplicitWaitSeconds = waitInSeconds;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(waitInSeconds);
        }

        public static IWebElement SafeGetElement(this IWebDriver driver, By locator)
        {
            try { return driver.FindElement(locator); }
            catch (NoSuchElementException) { return null; }
        }

        public static IWebElement SafeGetElement(this IWebDriver driver, IWebElement parent, By locator)
        {
            try { return parent.FindElement(locator); }
            catch (NoSuchElementException) { return null; }
        }

        public static bool TryGetElement(this IWebDriver driver, By element, out IWebElement webElement)
        {
            try
            {
                webElement = driver.FindElement(element);
                return true;
            }
            catch (NoSuchElementException)
            {
                webElement = null;
                return false;
            }
        }

        public static bool TryGetElement(this IWebDriver driver, IWebElement parent, By element, out IWebElement webElement)
        {
            try
            {
                webElement = parent.FindElement(element);
                return true;
            }
            catch (NoSuchElementException)
            {
                webElement = null;
                return false;
            }
        }

        public static IWebElement WaitForElement(this IWebDriver driver, By by, int timeOut = 30000)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromMilliseconds(timeOut));
            return wait.Until(d =>
            {
                try { return d.FindElement(by); }
                catch (NoSuchElementException) { return null; }
            });
        }

        public static IWebElement WaitForElement(this IWebDriver driver, By by, IWebElement baseElement, int timeOut = 30000)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromMilliseconds(timeOut));
            return wait.Until(d =>
            {
                try { return baseElement.FindElement(by); }
                catch (NoSuchElementException) { return null; }
            });
        }

        public static void WaitForElementDisplayed(this IWebDriver driver, params By[] elementBy)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(d =>
            {
                try { return d.GetElementByChainedBys(elementBy).Displayed; }
                catch { return false; }
            });
        }

        public static void WaitForElementDisplayed(this IWebDriver driver, int timeoutInMilliseconds, params By[] elementBy)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromMilliseconds(timeoutInMilliseconds));
            wait.Until(d =>
            {
                try { return d.GetElementByChainedBys(elementBy).Displayed; }
                catch { return false; }
            });
        }

        public static void WaitForElementDisplayed(this IWebDriver driver, IWebElement containerElement, params By[] elementBy)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(d =>
            {
                try { return containerElement.FindElementByChainedBys(elementBy).Displayed; }
                catch { return false; }
            });
        }

        public static IWebElement WaitForElementDisplayed(this IWebDriver driver, By by, int timeOut)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromMilliseconds(timeOut));
            return wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(by);
                    return el.Displayed ? el : null;
                }
                catch { return null; }
            });
        }

        public static void WaitForElementDisplayed(this IWebDriver driver, int timeoutInMilliseconds,
            IWebElement containerElement, params By[] elementBy)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromMilliseconds(timeoutInMilliseconds));
            wait.Until(d =>
            {
                try { return containerElement.FindElementByChainedBys(elementBy).Displayed; }
                catch { return false; }
            });
        }

        public static void WaitForElementDisplayed(this IWebDriver driver, IWebElement element)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(d =>
            {
                try { return element.Displayed; }
                catch { return false; }
            });
        }

        public static void WaitForElementDisplayed(this IWebDriver driver, int timeoutInMilliseconds, IWebElement element)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromMilliseconds(timeoutInMilliseconds));
            wait.Until(d =>
            {
                try { return element.Displayed; }
                catch { return false; }
            });
        }

        public static void WaitForElementNotDisplayed(this IWebDriver driver, int timeOut, By elementBy)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromMilliseconds(timeOut));
            wait.Until(d =>
            {
                try { return !d.FindElement(elementBy).Displayed; }
                catch (NoSuchElementException) { return true; }
            });
        }

        public static void WaitForElementNotDisplayed(this IWebDriver driver, params By[] elementBy)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(d =>
            {
                try { return !d.GetElementByChainedBys(elementBy).Displayed; }
                catch (NoSuchElementException) { return true; }
            });
        }

        public static void WaitForElementNotDisplayed(this IWebDriver driver, IWebElement containerElement, By elementBy)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(d =>
            {
                try { return !containerElement.FindElement(elementBy).Displayed; }
                catch (NoSuchElementException) { return true; }
            });
        }

        public static void WaitForElementNotDisplayed(this IWebDriver driver, IWebElement element)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(d =>
            {
                try { return !element.Displayed; }
                catch { return true; }
            });
        }

        public static void WaitForElementNotPresent(this IWebDriver driver, By by, int timeOut = 5000)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromMilliseconds(timeOut));
            wait.Until(d =>
            {
                try
                {
                    d.FindElement(by);
                    return false;
                }
                catch (NoSuchElementException) { return true; }
            });
        }

        public static void WaitForElementNotPresent(this IWebDriver driver, IWebElement container, By by, int timeOut = 5000)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromMilliseconds(timeOut));
            wait.Until(d =>
            {
                try
                {
                    container.FindElement(by);
                    return false;
                }
                catch (NoSuchElementException) { return true; }
            });
        }

        public static void WaitForElementPresent(this IWebDriver driver, params By[] elementBy)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(d =>
            {
                try
                {
                    d.GetElementByChainedBys(elementBy);
                    return true;
                }
                catch { return false; }
            });
        }

        public static void WaitForNewTabLoaded(this IWebDriver driver, int tabsCount, int timeout = 15)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.Until(d => d.WindowHandles.Count > tabsCount);
        }

        public static void WaitForTabClosed(this IWebDriver driver, int tabsCount, int timeout = 15)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.Until(d => d.WindowHandles.Count < tabsCount);
        }

        public static bool WaitForTextInElement(this IWebDriver driver, string lookedFor, By by)
        {
            try
            {
                var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(30));
                return wait.Until(d =>
                {
                    try { return d.FindElement(by).Text.Contains(lookedFor); }
                    catch { return false; }
                });
            }
            catch { return false; }
        }

        public static bool WaitForTextInElement(this IWebDriver driver, long implicitWait, string text, params By[] elementBys)
        {
            try
            {
                var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromMilliseconds(implicitWait));
                return wait.Until(d =>
                {
                    try { return d.GetElementByChainedBys(elementBys).Text.Contains(text); }
                    catch { return false; }
                });
            }
            catch { return false; }
        }

        public static void WaitForElementEnabled(this IWebDriver driver, IWebElement element, int timeoutInMilliseconds = 2000)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromMilliseconds(timeoutInMilliseconds));
            wait.Until(d =>
            {
                try { return element.Enabled; }
                catch { return false; }
            });
        }

        public static void Focus(this IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].focus();", element);
        }
    }
}
