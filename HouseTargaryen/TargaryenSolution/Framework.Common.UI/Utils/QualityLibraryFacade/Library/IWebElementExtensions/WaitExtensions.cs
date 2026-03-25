namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions
{
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// The IWebElement Extensions
    /// </summary>
    public static partial class ElementExtensions
    {
        /// <summary>
        /// Has Selenium wait until the element with the specified identifier has become visible
        /// </summary>
        /// <param name="element">
        /// The element to wait for.
        /// </param>
        public static void WaitForElementDisplayed(this IWebElement element)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.WaitForElementDisplayed(element));
        }

        /// <summary>
        /// Has Selenium wait until the element with the specified identifier has become visible
        /// </summary>
        /// <param name="element">
        /// The element to wait for.
        /// </param>
        /// <param name="timeoutInMilliseconds">
        /// The maximum number of milliseconds that Selenium should wait.
        /// </param>
        public static void WaitForElementDisplayed(this IWebElement element, int timeoutInMilliseconds)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.WaitForElementDisplayed(timeoutInMilliseconds, element));
        }

        /// <summary>
        /// Has Selenium wait until the element has become enabled
        /// </summary>
        /// <param name="element">Element</param>
        /// <param name="timeoutInMilliseconds">Timeout. Default value 20 sec.</param>
        public static void WaitForElementEnabled(this IWebElement element, int timeoutInMilliseconds = 2000)
        {
            DriverExtensions.WaitForCondition(condition => element.Enabled, timeoutInMilliseconds);
        }

        /// <summary>
        /// Has Selenium wait until the element with the specified identifier has become NOT visible
        /// </summary>
        /// <param name="element">A WebElement</param>
        public static void WaitForElementNotDisplayed(this IWebElement element)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.WaitForElementNotDisplayed(element));
        }
    }
}
