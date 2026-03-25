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
        /// Acts as a click wrapper for WebElements to prevent inconsistent clicking in Internet Explorer
        /// </summary>
        /// <param name="element">The WebElement being clicked</param>
        public static void Click(IWebElement element)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => CustomExtensions.Click(wd, element));
        }

        /// <summary>
        /// Clicks on element even it's out of view
        /// </summary>
        /// <param name="elementBys">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement while the others correspond to any containers it might have
        /// </param>
        public static void Click(params By[] elementBys)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => CustomExtensions.Click(wd, elementBys));
        }

        /// <summary>
        /// Click to the specified By identifier within the specified container ByElement
        /// </summary>
        /// <param name="container">container</param>
        /// <param name="elementBys">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement while the others correspond to any containers it might have
        /// </param>
        public static void Click(IWebElement container, params By[] elementBys)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.Click(container, elementBys));
        }

        /// <summary>
        /// Simulates a Javascript click on the specified element
        /// </summary>
        /// <param name="elementBys">A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement while the others correspond to any containers it might have</param>
        public static void JavascriptClick(params By[] elementBys)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.JavascriptClick(elementBys));
        }

        /// <summary>
        /// Double click on an element
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        public static void DoubleClick(IWebElement element)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.DoubleClick(element));
        }
    }
}