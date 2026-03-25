namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions
{
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
        /// Returns the immediate text of the specified element and not the text of its children
        /// </summary>
        /// <param name="elementBy">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement to get the text of while the others correspond to any containers it might have
        /// </param>
        /// <returns>The text</returns>
        public static string GetImmediateText(params By[] elementBy)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetImmediateText(elementBy)).Trim();
        }

        /// <summary>
        /// Returns the immediate text of the specified element and not the text of its children
        /// </summary>
        /// <param name="container">The container WebElement</param>
        /// <param name="elementBy">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement to get the text of while the others correspond to any containers it might have
        /// </param>
        /// <returns>The text</returns>
        public static string GetImmediateText(IWebElement container, params By[] elementBy)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetImmediateText(container, elementBy));
        }

        /// <summary>
        /// The get text safe.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="elementBy">
        /// The element by.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetTextSafe(IWebElement container, By elementBy)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetTextSafe(container, elementBy));
        }

        /// <summary>
        /// Get text
        /// </summary>
        /// <param name="by"></param>
        /// <param name="baseElement"></param>
        /// <param name="timeOut">time out in milliseconds</param>
        /// <returns></returns>
        public static string GetText(By by, IWebElement baseElement, int timeOut)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetText(by, baseElement, timeOut));
        }

        /// <summary>
        /// Method for highlighting part of text
        /// </summary>
        /// <param name="element1"> </param>
        /// <param name="element2"> </param>
        /// <returns>
        /// The string
        /// </returns>
        public static string HighlightMultipleNodes(IWebElement element1, IWebElement element2)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => CustomExtensions.HighlightMultipleNodes(wd, element1, element2));
        }

        /// <summary>
        /// Get Highlighted text in html
        /// </summary>
        /// <returns> Highlighted text </returns>
        public static string GetHighlightedHtml()
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => CustomExtensions.GetHighlightedHtml(wd));
        }


        /// <summary>
        /// Method which checks if text present in element
        /// </summary>
        /// <param name="by"></param>
        /// <param name="text">
        /// text to be found
        /// </param>
        /// <returns>
        /// The result
        /// </returns>
        public static bool IsTextInElement(By by, string text)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.IsTextInElement(by, text));
        }

        /// <summary>
        /// Method which checks if text present on Page
        /// </summary>
        /// <param name="text">
        /// text to be found
        /// </param>
        /// <returns>
        /// The result
        /// </returns>
        public static bool IsTextOnPage(string text)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.IsTextOnPage(text));
        }
    }
}