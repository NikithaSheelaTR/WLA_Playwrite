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
        /// Simulates a JavaScript click on the specified element
        /// </summary>
        /// <param name="element"> The WebElement that is being clicked</param>
        public static void JavascriptClick(this IWebElement element) =>
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.JavascriptClick(element));

        /// <summary>
        /// The click using mouse.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        public static void ClickUsingMouse(this IWebElement element) =>
            BrowserPool.CurrentBrowser.ActionInstance.Click(element).Build().Perform();

        /// <summary>
        /// Custom click for 
        /// </summary>
        /// <param name="element">The WebElement being clicked</param>
        public static void CustomClick(this IWebElement element)
        {
            try
            {
                element.Click();
            }
            catch (WebDriverException)
            {
                element.ScrollToElementCenter();
                element.Click();
            }
        }

        /// <summary>
        /// Execute click asynchronous without waiting for response
        /// </summary>
        /// <param name="element"> Element to click </param>
        public static void ClickUsingJavaScriptAsync(this IWebElement element) =>
            DriverExtensions.ExecuteScript(
                "var el=arguments[0]; setTimeout(function() { el.click(); }, 100);",
                element);
    }
}
