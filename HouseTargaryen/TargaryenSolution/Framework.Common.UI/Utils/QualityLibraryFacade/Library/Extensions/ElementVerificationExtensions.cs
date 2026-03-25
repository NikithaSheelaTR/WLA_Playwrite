namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions
{
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.Core;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Custom;

    using OpenQA.Selenium;

    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// The driver extensions.
    /// </summary>
    public static partial class DriverExtensions
    {
        /// <summary>
        /// Waits for an element to be not displayed on the page.
        /// </summary>
        /// <param name="by">
        /// By locator to use
        /// </param>
        /// <returns>
        /// true or false
        /// </returns>
        public static bool AreNotDisplayed(By by)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.AreNotDisplayed(by));
        }

        /// <summary>
        /// Checks to see if the specified element is visible
        /// </summary>
        /// <param name="elementBy">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement while the others correspond to any containers it might have
        /// </param>
        /// <returns>
        /// True if the element is visible and False if it is not
        /// </returns>
        public static bool IsDisplayed(params By[] elementBy)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.IsDisplayed(elementBy));
        }

        /// <summary>
        /// Checks to see if the specified element is visible
        /// </summary>
        /// <param name="containerElement">
        /// A WebElement containing the desired WebElement
        /// </param>
        /// <param name="elementBy">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement while the others correspond to any containers it might have
        /// </param>
        /// <returns>
        /// True if the element is visible and False if it is not
        /// </returns>
        public static bool IsDisplayed(IWebElement containerElement, params By[] elementBy)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.IsDisplayed(containerElement, elementBy));
        }

        /// <summary>
        /// Waits for an element to be displayed on the page. Typically used for Ajax elements
        /// </summary>
        /// <param name="by">
        /// By locator to use
        /// </param>
        /// <param name="timeoutInSeconds">
        /// Time in seconds to wait before timeout
        /// </param>
        /// <returns>
        /// true or false
        /// </returns>
        public static bool IsDisplayed(By by, int timeoutInSeconds = 0)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.IsElementDisplayed(by, timeoutInSeconds));
        }

        /// <summary>
        /// Checks to see if the specified element is present (regardless of visibility)
        /// </summary>
        /// <param name="elementBy">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement while the others correspond to any containers it might have
        /// </param>
        /// <returns>
        /// True if the element exists on the page and False if it does not
        /// </returns>
        public static bool IsElementPresent(params By[] elementBy)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.IsElementPresent(elementBy));
        }

        /// <summary>
        /// Checks to see if the specified element is present (regardless of visibility)
        /// </summary>
        /// <param name="containerElement">
        /// A WebElement containing the desired WebElement
        /// </param>
        /// <param name="elementBy">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement while the others correspond to any containers it might have
        /// </param>
        /// <returns>
        /// True if the element exists on the page and False if it does not
        /// </returns>
        public static bool IsElementPresent(IWebElement containerElement, params By[] elementBy)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.IsElementPresent(containerElement, elementBy));
        }

        /// <summary>
        /// Checks to see if the specified element is present (regardless of visibility)
        /// </summary>
        /// <param name="elementBy">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement while the others correspond to any containers it might have
        /// </param>
        /// <param name="timeout">
        /// The number of milliseconds to wait for the element to exist
        /// </param>
        /// <returns>
        /// True if the element exists on the page and False if it does not
        /// </returns>
        public static bool IsElementPresent(
            By elementBy,
            int timeout = WebDriverConstants.DefaultNotPresentWaitInMilliseconds)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.IsElementPresent(elementBy, timeout));
        }

        /// <summary>
        /// Checks to see if the specified element is present (regardless of visibility)
        /// </summary>
        /// <param name="containerElement">
        /// A WebElement containing the desired WebElement
        /// </param>
        /// <param name="elementBy">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement while the others correspond to any containers it might have
        /// </param>
        /// <param name="timeout">timeout
        /// </param>
        /// <returns>
        /// True if the element exists on the page and False if it does not
        /// </returns>
        public static bool IsElementPresent(
            IWebElement containerElement,
            By elementBy,
            int timeout = WebDriverConstants.DefaultNotPresentWaitInMilliseconds)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(
                wd => wd.IsElementPresent(containerElement, elementBy, timeout));
        }

        /// <summary>
        /// Checks to see if the specified element is enabled 
        /// </summary>
        /// <param name="by">by</param>
        /// <returns>true or false</returns>
        public static bool IsEnabled(By by)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.IsEnabled(by));
        }

        /// <summary>
        /// The is scrollable element in view.
        /// </summary>
        /// <param name="elementWithinScrollBy">
        /// The element within scroll by.
        /// </param>
        /// <param name="divWithScrollBy">
        /// The div with scroll by.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsScrollableElementInView(By elementWithinScrollBy, By divWithScrollBy)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.IsScrollableElementInView(elementWithinScrollBy, divWithScrollBy));
        }
    }
}