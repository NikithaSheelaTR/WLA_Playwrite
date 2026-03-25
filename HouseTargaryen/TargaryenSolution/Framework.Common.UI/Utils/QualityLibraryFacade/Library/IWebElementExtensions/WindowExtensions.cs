namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions
{
    using System.Drawing;
    using System.IO;

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
        /// Get screenshot of the WebElement
        /// </summary>
        /// <param name="webElement"> IWebElement </param>
        /// <returns> <see cref="Bitmap"/> Image </returns>
        public static Bitmap GetWebElementScreenshot(IWebElement webElement)
        {
            BrowserPool.CurrentBrowser.SwitchToFullScreenMode(); // for position adjustment
            Bitmap screenBitmap;
            try
            {
                BrowserPool.CurrentBrowser.InvokeAction(wd => wd.ScrollTo(webElement));
                Screenshot screenshot = BrowserPool.CurrentBrowser.GetScreenshot();
                using (var stream = new MemoryStream(screenshot.AsByteArray))
                {
                    screenBitmap = new Bitmap(stream);
                }

                Rectangle rect = new Rectangle(webElement.Location, webElement.Size);
                return screenBitmap.Clone(rect, screenBitmap.PixelFormat);
            }
            finally
            {
                BrowserPool.CurrentBrowser.Maximize(); // return to maximized mode
            }
        }

        /// <summary>
        /// Scrolls to the element center
        /// </summary>
        /// <param name="element">element to scroll to</param>
        public static void ScrollToElementCenter(this IWebElement element)
        {
            DriverExtensions.ExecuteScript("arguments[0].scrollIntoView({block: 'center', inline: 'center'})", element);
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Scrolls the given Web Element to the center of the page
        /// </summary>
        /// <param name="element">
        /// The element to scroll to
        /// </param>
        public static void ScrollToElement(this IWebElement element)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.ScrollTo(element));
        }

        /// <summary>
        /// Focus on the specified element
        /// </summary>
        /// <param name="element">The WebElement being focused</param>
        public static void Focus(this IWebElement element)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.Focus(element));
        }
    }
}
