namespace Framework.Common.UI.Utils.QualityLibraryFacade.Custom
{
    using System.Drawing;
    using System.IO;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// The custom extensions.
    /// </summary>
    public static partial class CustomExtensions
    {
        /// <summary>
        /// The scroll page to bottom.
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        public static void ScrollPageToBottom(this IWebDriver driver)
        {
            driver.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
        }

        /// <summary>
        /// ScrollToElementInsideContainer
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="container"></param>
        /// <param name="elementToScroll"></param>
        /// <param name="offset"></param>
        public static void ScrollToElementInsideContainer(this IWebDriver driver, By container, By elementToScroll, int offset = 0)
        {
            driver.ExecuteScript(
                "$(arguments[0]).animate({ scrollTop: ($(arguments[1]).offset().top)-arguments[2] })",
                driver.GetElement(container),
                driver.GetElement(elementToScroll),
                offset);
            driver.WaitForJavaScript();
        }

        /// <summary>
        /// Get screenshot of the WebElement
        /// </summary>
        /// <param name="driver"> The driver. </param>
        /// <param name="webElement"> IWebElement </param>
        /// <returns> <see cref="Bitmap"/> Image </returns>
        public static Bitmap GetWebElementBitmap(this IWebDriver driver, IWebElement webElement)
        {
            driver.Manage().Window.FullScreen(); // for position adjustment
            DriverExtensions.ScrollTo(webElement, By.XPath("*"));
            Screenshot screenshoot = OpenQA.Selenium.Support.Extensions.WebDriverExtensions.TakeScreenshot(driver);
            driver.Manage().Window.Maximize(); // return to maximized mode
            Bitmap screenBitmap;
            using (var stream = new MemoryStream(screenshoot.AsByteArray))
            {
                screenBitmap = new Bitmap(stream);
            }

            var rect = new Rectangle(webElement.Location, webElement.Size);
            return screenBitmap.Clone(rect, screenBitmap.PixelFormat);
        }

        /// <summary>
        /// The scroll to container top.
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="offset">
        /// The y.
        /// </param>
        public static void ScrollToContainerTop(this IWebDriver driver, By container, int offset = 0)
        {
            driver.ExecuteScript(
                "$(arguments[0]).animate({ scrollTop: arguments[1] })",
                driver.GetElement(container),
                offset);
            driver.WaitForJavaScript();
        }
    }
}