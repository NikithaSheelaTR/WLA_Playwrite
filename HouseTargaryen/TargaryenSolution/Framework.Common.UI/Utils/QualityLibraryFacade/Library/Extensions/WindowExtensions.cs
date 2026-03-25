namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions
{
    using System;

    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Custom;

    using OpenQA.Selenium;

    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// The driver extensions.
    /// </summary>
    public static partial class DriverExtensions
    {
        /// <summary>
        /// Uses window.scrollTo to scroll to a specified element. 
        /// </summary>
        /// <param name="by">how to find the element</param>
        /// <param name="offset">used to increase or decrease the vertical scroll - helps when a header blocks the element we're looking for</param>
        public static void ScrollIntoView(By by, int offset = 0)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.ScrollIntoView(by, offset));
        }

        /// <summary>
        /// Scroll page to bottom
        /// </summary>
        public static void ScrollPageToBottom()
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.ScrollPageToBottom());
        }

        /// <summary>
        /// Scrolls the given Web Element to the center of the page
        /// </summary>
        /// <param name="by">
        /// The element to scroll to
        /// </param>
        public static void ScrollTo(By by)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.ScrollTo(DriverExtensions.GetElement(by)));
        }

        /// <summary>
        /// The scroll to.
        /// </summary>
        /// <param name="containerElement">
        /// The container element.
        /// </param>
        /// <param name="by">
        /// The by.
        /// </param>
        public static void ScrollTo(IWebElement containerElement, By by)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.ScrollTo(DriverExtensions.GetElement(containerElement, by)));
        }

        /// <summary>
        /// Scrolls window to the top of the page
        /// </summary>
        public static void ScrollToTop()
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.ScrollToTop());
        }

        /// <summary>
        /// The scroll to element inside container.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="elementToScroll">
        /// The element to scroll.
        /// </param>
        /// <param name="offset">
        /// The offset.
        /// </param>
        public static void ScrollToElementInsideContainer(By container, By elementToScroll, int offset = 0)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.ScrollToElementInsideContainer(container, elementToScroll, offset));
        }

        /// <summary>
        /// The scroll to container top.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="offset"></param>
        public static void ScrollToContainerTop(By container, int offset = 0)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.ScrollToContainerTop(container, offset));
        }

        /// <summary>
        /// Verify is page scrolled in to the top
        /// </summary>
        /// <returns>true if scrolled, false otherwise</returns>
        public static bool IsPageScrolledToTop()
        {
            int pixelsFromTop = Convert.ToInt32(DriverExtensions.ExecuteScript(@"return $(window).scrollTop();"));
            return pixelsFromTop == 0;
        }
    }
}