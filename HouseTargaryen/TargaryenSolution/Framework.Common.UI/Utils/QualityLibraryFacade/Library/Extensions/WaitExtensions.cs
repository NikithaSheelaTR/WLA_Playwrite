namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions
{
    using System;

    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.Core;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Custom;

    using OpenQA.Selenium;

    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// Facade for driver extensions in QualityLibrary
    /// </summary>
    public static partial class DriverExtensions
    {
        /// <summary>
        /// The wait for state.
        /// </summary>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <param name="timeOut">
        /// The time out.
        /// </param>
        public static void WaitForCondition(Func<object, bool> condition, int timeOut = 30)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.WaitForCondition(condition, timeOut));
        }

        /// <summary>
        /// This method disables (sets to 0) the implicit wait time for time out when searching for element
        /// </summary>
        public static void DisableTimeout()
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.DisableTimeout());
        }

        /// <summary>
        /// This method resets the implicit wait time for time out when searching for elements back to the default
        /// </summary>
        public static void ResetTimeout()
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.ResetTimeout());
        }

        /// <summary>Safe Get Element</summary>
        /// <param name="locator">The locator.</param>
        /// <returns>result element</returns>
        public static IWebElement SafeGetElement(By locator)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.SafeGetElement(locator));
        }

        /// <summary>Safe Get Element</summary>
        /// <param name="parent">parent</param>
        /// <param name="locator">locator</param>
        /// <returns>result element</returns>
        public static IWebElement SafeGetElement(IWebElement parent, By locator)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.SafeGetElement(parent, locator));
        }

        /// <summary>
        /// This method reduces the implicit wait time for time out when searching for elements
        /// </summary>
        /// <param name="waitInSeconds">The time in seconds that you want to set the implicit wait to</param>
        public static void SetTimeout(int waitInSeconds)
        {
            WaitExtensions.CurrentImplicitWait = waitInSeconds;
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.SetTimeout(waitInSeconds));
        }

        /// <summary>
        /// Try to get element
        /// </summary>
        /// <param name="element"> element</param>
        /// <param name="webElement"> result element </param>
        /// <returns>Result</returns>
        public static bool TryGetElement(By element, out IWebElement webElement)
        {
            IWebElement resultElement = null;
            bool result = BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.TryGetElement(element, out resultElement));
            webElement = resultElement;
            return result;
        }

        /// <summary>
        /// Try to get element
        /// </summary>
        /// <param name="parent"> parent element </param>
        /// <param name="element"> selector </param>
        /// <param name="webElement"> result element </param>
        /// <returns> result </returns>
        public static bool TryGetElement(IWebElement parent, By element, out IWebElement webElement)
        {
            IWebElement resultElement = null;
            bool result =
                BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.TryGetElement(parent, element, out resultElement));
            webElement = resultElement;
            return result;
        }

        /// <summary>
        /// WaitForElement will wait the specified number of milliseconds (default is 30000) for an element to be found via the driver.
        /// This does not wait for element to be displayed or visible.
        /// </summary>
        /// <param name="by">how to find the element</param>
        /// <param name="timeOut">The number of milliseconds to wait for the element to exist.</param>
        /// <returns>the WebElement if found</returns>
        /// <exception cref="NoSuchElementException">if the element is not found</exception>
        public static IWebElement WaitForElement(By by, int timeOut = WebDriverConstants.DefaultTimeoutInMilliseconds)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.WaitForElement(by, timeOut));
        }

        /// <summary>
        /// WaitForElement will wait the specified number of milliseconds (default is 30000) for an element to be found via the driver.
        /// </summary>
        /// <param name="baseElement"> Parent element </param>
        /// <param name="by"> How to find the element </param>
        /// <param name="timeOut"> The number of milliseconds to wait for the element to exist. </param>
        /// <returns>the WebElement if found </returns>
        public static IWebElement WaitForElement(
            IWebElement baseElement,
            By by,
            int timeOut = WebDriverConstants.DefaultTimeoutInMilliseconds)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.WaitForElement(by, baseElement, timeOut));
        }

        /// <summary> Has Selenium wait until the element with the specified identifier has become visible </summary>
        /// <param name="elementBy">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement while the others correspond to any containers it might have
        /// </param>
        public static void WaitForElementDisplayed(params By[] elementBy)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.WaitForElementDisplayed(elementBy));
        }

        /// <summary>  Has Selenium wait until the element with the specified identifier has become visible   </summary>
        /// <param name="timeoutInMilliseconds"> The maximum number of milliseconds that Selenium should wait.  </param>
        /// <param name="elementBy">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement while the others correspond to any containers it might have
        /// </param>
        public static void WaitForElementDisplayed(int timeoutInMilliseconds, params By[] elementBy)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.WaitForElementDisplayed(timeoutInMilliseconds, elementBy));
        }

        /// <summary>
        /// Has Selenium wait until the element with the specified identifier has become visible
        /// </summary>
        /// <param name="containerElement">A WebElement containing the desired WebElement</param>
        /// <param name="elementBy">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement while the others correspond to any containers it might have
        /// </param>
        public static void WaitForElementDisplayed(IWebElement containerElement, params By[] elementBy)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.WaitForElementDisplayed(containerElement, elementBy));
        }

        /// <summary>
        /// Waits for an element to be displayed on the page. Typically used for Ajax elements
        /// </summary>
        /// <param name="by">By locator to use</param>
        /// <param name="timeOut">Time in milliseconds to wait before timeout</param>
        /// <returns>IWebElement</returns>
        public static IWebElement WaitForElementDisplayed(
            By by,
            int timeOut = WebDriverConstants.DefaultTimeoutInMilliseconds)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.WaitForElementDisplayed(by, timeOut));
        }

        /// <summary>
        /// Has Selenium wait until the element with the specified identifier has become visible
        /// </summary>
        /// <param name="timeoutInMilliseconds">The maximum number of milliseconds that Selenium should wait.</param>
        /// <param name="containerElement">A WebElement containing the desired WebElement</param>
        /// <param name="elementBy">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement while the others correspond to any containers it might have
        /// </param>
        public static void WaitForElementDisplayed(
            int timeoutInMilliseconds,
            IWebElement containerElement,
            params By[] elementBy)
        {
            BrowserPool.CurrentBrowser.InvokeAction(
                wd => wd.WaitForElementDisplayed(timeoutInMilliseconds, containerElement, elementBy));
        }

        /// <summary> wait until the element has become NOT visible </summary>
        /// <param name="timeOut"> Time in milliseconds to wait before timeout </param>
        /// <param name="elementBy"> By locator to use </param>
        public static void WaitForElementNotDisplayed(int timeOut, By elementBy)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.WaitForElementNotDisplayed(timeOut, elementBy));
        }

        /// <summary>
        /// Has Selenium wait until the element with the specified identifier has become NOT visible
        /// </summary>
        /// <param name="elementBy">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement while the others correspond to any containers it might have
        /// </param>
        public static void WaitForElementNotDisplayed(params By[] elementBy)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.WaitForElementNotDisplayed(elementBy));
        }

        /// <summary>
        /// Has Selenium wait until the element with the specified identifier has become NOT visible
        /// </summary>
        /// <param name="containerElement">A WebElement containing the desired WebElement</param>
        /// <param name="elementBy">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement while the others correspond to any containers it might have
        /// </param>
        public static void WaitForElementNotDisplayed(IWebElement containerElement, By elementBy)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.WaitForElementNotDisplayed(containerElement, elementBy));
        }

        /// <summary>
        /// Waits for the specified element to no longer in the html of the page
        /// </summary>
        /// <param name="by">how to find the element</param>
        /// <param name="timeOut">length of time to wait</param>
        public static void WaitForElementNotPresent(
            By by,
            int timeOut = WebDriverConstants.DefaultNotPresentWaitInMilliseconds)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.WaitForElementNotPresent(by, timeOut));
        }

        /// <summary> Waits for an element to be displayed on the page. Typically used for Ajax elements </summary>
        /// <param name="container"> The container. </param>
        /// <param name="by"> By locator to use </param>
        /// <param name="timeOut"> Time in milliseconds to wait before timeout </param>
        public static void WaitForElementNotPresent(
            IWebElement container,
            By by,
            int timeOut = WebDriverConstants.DefaultNotPresentWaitInMilliseconds)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.WaitForElementNotPresent(container, by, timeOut));
        }

        /// <summary>
        /// Has Selenium wait until the element with the specified identifier has become Present
        /// </summary>
        /// <param name="elementBy">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement while the others correspond to any containers it might have
        /// </param>
        public static void WaitForElementPresent(params By[] elementBy)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.WaitForElementPresent(elementBy));
        }

        /// <summary>
        /// Wait until new tab will be opened and loaded
        /// </summary>
        /// <param name="tabsCount">Current tabs count</param>
        /// <param name="timeout">Timeout in seconds. 15 seconds by default</param>
        public static void WaitForNewTabLoaded(int tabsCount, int timeout = 15)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.WaitForNewTabLoaded(tabsCount, timeout));
        }

        /// <summary>
        /// Wait until new tab will be closed
        /// </summary>
        /// <param name="tabsCount">Current tabs count</param>HotFix for Indian colleagues
        /// <param name="timeout">Timeout in seconds. 15 seconds by default</param>
        public static void WaitForTabClosed(int tabsCount, int timeout = 15)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.WaitForTabClosed(tabsCount, timeout));
        }

        /// <summary> Method waits until text present in element </summary>
        /// <param name="lookedFor"> text to be found </param>
        /// <param name="by"> element </param> 
        /// <returns> The result </returns>
        public static bool WaitForTextInElement(string lookedFor, By by)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.WaitForTextInElement(lookedFor, by));
        }

        /// <summary>
        /// Waits for text to appear in a element
        /// </summary>
        /// <param name="implicitWait">The max amount of time to wait for</param>
        /// <param name="text">The text to look for</param>
        /// <param name="elementBys">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired WebElement while the others correspond to any containers it might have
        /// </param>
        /// <returns>True if the text appears, false otherwise</returns>
        public static bool WaitForTextInElement(long implicitWait, string text, params By[] elementBys)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.WaitForTextInElement(implicitWait, text, elementBys));
        }
    }
}