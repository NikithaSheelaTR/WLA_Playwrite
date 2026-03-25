namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions
{
    using Framework.Common.UI.Utils.Browser;

    using OpenQA.Selenium;

    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// The driver extensions.
    /// </summary>
    public static partial class DriverExtensions
    {
        /// <summary>
        /// Hover over an element
        /// </summary>
        /// <param name="by">how to find the element</param>
        public static void Hover(By by)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.Hover(by));
        }

        /// <summary>
        /// Hover over an element using JavaScript
        /// </summary>
        /// <param name="by">how to find the element</param>
        public static void HoverUsingJavaScript(By by)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.HoverUsingJavaScript(by));
        }
    }
}