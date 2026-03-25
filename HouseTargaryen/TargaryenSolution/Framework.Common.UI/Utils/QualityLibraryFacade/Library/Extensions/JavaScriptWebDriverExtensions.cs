namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions
{
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.Core;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Custom;
    using Framework.Core.DataModel.Configuration.Constants;

    using OpenQA.Selenium;

    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// Facade for driver extensions in QualityLibrary
    /// </summary>
    public static partial class DriverExtensions
    {
        /// <summary>
        /// Simulates a JavaScript click on the specified element
        /// </summary>
        /// <param name="by">how to find the element</param>
        public static void ClickUsingJavaScript(By by)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.ClickUsingJavaScript(by));
        }

        /// <summary>
        /// Executes the given script on the page
        /// </summary>
        /// <param name="script">The JavaScript to run</param>
        /// <param name="arguments">Arguments to send into the script</param>
        /// <returns></returns>
        public static object ExecuteScript(string script, params object[] arguments)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.ExecuteScript(script, arguments));
        }

        /// <summary>
        /// Allows access to an IJavaScriptExecutor object for this driver
        /// </summary>
        /// <returns>the IJavaScriptExecutor</returns>
        public static IJavaScriptExecutor Scripts()
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.Scripts());
        }

        /// <summary>
        /// Wait for JQuery animations to be done
        /// </summary>
        /// <param name="timeoutInMilliseconds">length of time to wait for animations</param>
        /// <returns>result</returns>
        public static bool WaitForAnimation(int timeoutInMilliseconds = WebDriverConstants.DefaultTimeoutInMilliseconds)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.WaitForAnimation(timeoutInMilliseconds));
        }

        /// <summary>
        /// Waits for jQuery to be inactive and any animation to be done
        /// </summary>
        /// <param name="timeoutInMilliseconds">length of time to wait for javascript</param>
        public static bool WaitForJavaScript(
            int timeoutInMilliseconds = WebDriverConstants.DefaultTimeoutInMilliseconds)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.WaitForJavaScript(timeoutInMilliseconds));
        }

        /// <summary>
        /// Waits up to the specified implicit wait for all forms of Javascript to finish executing
        /// </summary>
        /// <param name="timeoutInMilliseconds">The maximum number of milliseconds that Selenium should wait</param>
        public static bool WaitForPageLoad(int timeoutInMilliseconds = WebDriverConstants.DefaultTimeoutInMilliseconds)
        {
            if (BrowserPool.CurrentBrowser.BrowserInfo.Family
                == TestClientFamily.Firefox)
            {
                BrowserPool.CurrentBrowser.InvokeAction(wd => wd.GeckoWait());
            }

            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.WaitForPageLoad(timeoutInMilliseconds));
        }
    }
}