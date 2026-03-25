using System;
using System.Collections.ObjectModel;

namespace OpenQA.Selenium.Support.UI
{
    public static class ExpectedConditions
    {
        public static Func<IWebDriver, IWebElement> ElementExists(By locator)
        {
            return driver =>
            {
                try { return driver.FindElement(locator); }
                catch (NoSuchElementException) { return null; }
            };
        }

        public static Func<IWebDriver, IWebElement> ElementIsVisible(By locator)
        {
            return driver =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    return element.Displayed ? element : null;
                }
                catch (NoSuchElementException) { return null; }
                catch (StaleElementReferenceException) { return null; }
            };
        }

        public static Func<IWebDriver, IWebElement> ElementToBeClickable(By locator)
        {
            return driver =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    return (element.Displayed && element.Enabled) ? element : null;
                }
                catch (NoSuchElementException) { return null; }
                catch (StaleElementReferenceException) { return null; }
            };
        }

        public static Func<IWebDriver, IWebElement> ElementToBeClickable(IWebElement element)
        {
            return driver =>
            {
                try
                {
                    return (element.Displayed && element.Enabled) ? element : null;
                }
                catch (StaleElementReferenceException) { return null; }
            };
        }

        public static Func<IWebDriver, bool> InvisibilityOfElementLocated(By locator)
        {
            return driver =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    return !element.Displayed;
                }
                catch (NoSuchElementException) { return true; }
                catch (StaleElementReferenceException) { return true; }
            };
        }

        public static Func<IWebDriver, bool> TextToBePresentInElement(IWebElement element, string text)
        {
            return driver =>
            {
                try { return element.Text.Contains(text); }
                catch (StaleElementReferenceException) { return false; }
            };
        }

        public static Func<IWebDriver, bool> TextToBePresentInElementLocated(By locator, string text)
        {
            return driver =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    return element.Text.Contains(text);
                }
                catch (NoSuchElementException) { return false; }
                catch (StaleElementReferenceException) { return false; }
            };
        }

        public static Func<IWebDriver, bool> TitleContains(string title)
        {
            return driver => driver.Title.Contains(title);
        }

        public static Func<IWebDriver, bool> TitleIs(string title)
        {
            return driver => driver.Title == title;
        }

        public static Func<IWebDriver, bool> UrlContains(string fraction)
        {
            return driver => driver.Url.Contains(fraction);
        }

        public static Func<IWebDriver, IWebDriver> FrameToBeAvailableAndSwitchToIt(By locator)
        {
            return driver =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    return driver.SwitchTo().Frame(element);
                }
                catch (NoSuchElementException) { return null; }
                catch (NoSuchFrameException) { return null; }
            };
        }

        public static Func<IWebDriver, IAlert> AlertIsPresent()
        {
            return driver =>
            {
                try { return driver.SwitchTo().Alert(); }
                catch (NoAlertPresentException) { return null; }
            };
        }

        public static Func<IWebDriver, bool> StalenessOf(IWebElement element)
        {
            return driver =>
            {
                try
                {
                    _ = element.Enabled;
                    return false;
                }
                catch (StaleElementReferenceException) { return true; }
            };
        }

        public static Func<IWebDriver, ReadOnlyCollection<IWebElement>> PresenceOfAllElementsLocatedBy(By locator)
        {
            return driver =>
            {
                var elements = driver.FindElements(locator);
                return elements.Count > 0 ? elements : null;
            };
        }

        public static Func<IWebDriver, ReadOnlyCollection<IWebElement>> VisibilityOfAllElementsLocatedBy(By locator)
        {
            return driver =>
            {
                var elements = driver.FindElements(locator);
                if (elements.Count == 0) return null;
                foreach (var element in elements)
                {
                    if (!element.Displayed) return null;
                }
                return elements;
            };
        }
    }
}
