namespace Framework.Common.UI.Utils.QualityLibraryFacade.Custom
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using Framework.Core.Utils.Execution;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// CustomExtensions
    /// </summary>
    public static partial class CustomExtensions
    {
        /// <summary>
        /// Wait Page Load for gecko driver
        /// </summary>
        /// <param name="driver">driver</param>
        public static void GeckoWait(this IWebDriver driver)
        {
            if ((bool)driver.ExecuteScript("return navigator.userAgent.toLowerCase().indexOf('gecko/') === -1"))
            {
                return;
            }

            const int Tick = 50;
            const int Timeout = 2000;
            for (int i = 0; i * Tick < Timeout; i++)
            {
                if (driver.ExecuteScript("return document.readyState").ToString() != "complete")
                {
                    Console.WriteLine("GeckoWait " + driver.Url);
                    driver.WaitForPageLoad();
                }

                lock (driver)
                {
                    Thread.Sleep(Tick);
                }
            }
        }

        /// <summary>Safe Get Element</summary>
        /// <param name="driver">driver</param>
        /// <param name="locator">The locator.</param>
        /// <returns>result element</returns>
        public static IWebElement SafeGetElement(this IWebDriver driver, By locator)
        {
            IReadOnlyCollection<IWebElement> temp = driver.GetElements(locator);
            return temp.Count > 0 ? temp.First() : null;
        }

        /// <summary>Safe Get Element</summary>
        /// <param name="driver">driver</param>
        /// <param name="parent">parent</param>
        /// <param name="locator">locator</param>
        /// <returns>result element</returns>
        public static IWebElement SafeGetElement(this IWebDriver driver, IWebElement parent, By locator)
        {
            IList<IWebElement> temp = driver.GetElements(parent, locator);
            return temp.Count > 0 ? temp.First() : null;
        }

        /// <summary>
        /// Try to get element
        /// </summary>
        /// <param name="driver">driver</param>
        /// <param name="element"> element</param>
        /// <param name="webElement"> result element </param>
        /// <returns>result</returns>
        public static bool TryGetElement(this IWebDriver driver, By element, out IWebElement webElement)
        {
            IWebElement el = null;
            SafeMethodExecutor.Execute(() => el = driver.GetElement(element));
            webElement = el;
            return el != null;
        }

        /// <summary>
        /// Try to get element
        /// </summary>
        /// <param name="driver"> The driver. </param>
        /// <param name="parent"> parent element </param>
        /// <param name="element"> selector </param>
        /// <param name="webElement"> result element </param>
        /// <returns> result </returns>
        public static bool TryGetElement(
            this IWebDriver driver,
            IWebElement parent,
            By element,
            out IWebElement webElement)
        {
            IWebElement el = null;
            SafeMethodExecutor.Execute(() => el = driver.GetElement(parent, element));
            webElement = el;
            return el != null;
        }

        /// <summary>
        /// Has Selenium wait until the element has become enabled
        /// </summary>
        /// <param name="driver">Driver</param>
        /// <param name="element">Element</param>
        /// <param name="timeoutInMilliseconds">Timeout. Default value 20 sec.</param>
        public static void WaitForElementEnabled(
            this IWebDriver driver,
            IWebElement element,
            int timeoutInMilliseconds = 2000)
        {
            CustomExtensions.ExecuteWithoutImplicitTimeout(
                driver,
                () =>
                    new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeoutInMilliseconds)).Until(
                        x => element.Enabled));
        }

        /// <summary>
        /// Has Selenium wait until the element with the specified identifier has become NOT visible
        /// </summary>
        /// <param name="driver">
        /// the current <see cref="IWebDriver"/> (note that this is an extension method, 
        /// so it should be called as driver.method(), do not pass the driver as an argument
        /// </param>
        /// <param name="timeoutInMilliseconds">  The maximum number of milliseconds that Selenium should wait </param>
        /// <param name="containerElement"> A WebElement containing the desired WebElement </param>
        /// <param name="elementBy"> A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement while the others correspond to any containers it might have
        /// </param>
        public static void WaitForElementNotDisplayed(
            this IWebDriver driver,
            int timeoutInMilliseconds,
            IWebElement containerElement,
            params By[] elementBy)
        {
            CustomExtensions.ExecuteWithoutImplicitTimeout(
                driver,
                () =>
                    new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeoutInMilliseconds)).Until(
                        x => !driver.IsDisplayed(containerElement, elementBy)));
        }

        /// <summary>
        /// Wait until new tab will be opened and loaded
        /// </summary>
        /// <param name="driver">Selenium Webdriver</param>
        /// <param name="tabsCount">Current tabs count</param>
        /// <param name="timeout">Timeout in seconds. 15 seconds by default</param>
        public static void WaitForNewTabLoaded(this IWebDriver driver, int tabsCount, int timeout = 15)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeout));
            wait.Until(d => driver.WindowHandles.Count == tabsCount + 1);
            driver.WaitForPageLoad();
        }

        /// <summary>
        /// Wait until new tab will be closed
        /// </summary>
        /// <param name="driver">Selenium Webdriver</param>
        /// <param name="tabsCount">Current tabs count</param>
        /// <param name="timeout">Timeout in seconds. 15 seconds by default</param>
        public static void WaitForTabClosed(this IWebDriver driver, int tabsCount, int timeout = 15)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeout));
            wait.Until(d => driver.WindowHandles.Count == tabsCount - 1);
        }

        /// <summary>
        /// The wait for state.
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <param name="timeOut">
        /// The time out.
        /// </param>
        public static void WaitForCondition(this IWebDriver driver, Func<object, bool> condition, int timeOut = 30)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
            wait.Until(condition);
        }

        private static void ExecuteWithoutImplicitTimeout(IWebDriver driver, Action action)
        {
            if (action != null)
            {
                try
                {
                    driver.DisableTimeout();
                    action();
                }
                finally
                {
                    driver.SetTimeout(WaitExtensions.CurrentImplicitWait);
                }
            }
        }
    }
}