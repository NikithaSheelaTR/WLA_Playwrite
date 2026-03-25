namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions
{
    using System;
    using System.Collections.ObjectModel;

    using Framework.Common.UI.Interfaces;
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
        /// Creates an object for the given page type
        /// </summary>
        /// <param name="args"> The args. </param>
        /// <typeparam name="T"> The type of the object </typeparam>
        /// <returns> The instance </returns>
        public static T CreatePageInstance<T>(params object[] args) where T : ICreatablePageObject
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }

        /// <summary>
        /// Clean up cookies for current instance of the browser
        /// </summary>
        public static void DeleteAllCookies()
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.Manage().Cookies.DeleteAllCookies());
        }

        /// <summary>
        /// Return the <see cref="ReadOnlyCollection{Cookie}"/> cookie collection
        /// </summary>
        /// <returns></returns>
        public static ReadOnlyCollection<Cookie> GetCookies()
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetCookies());
        }

        /// <summary>
        /// Return the value of the cookie for the passed in cookie name.
        /// </summary>
        /// <param name="cookieName">The name of the cookie whose value will be returned</param>
        /// <returns></returns>
        public static string GetCookieValue(string cookieName)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetCookieValue(cookieName));
        }

        /// <summary>
        /// Returns the value of the "site" cookie (e.g. "b" or "pc1" for Demo)
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>
        /// Returns string.Empty in case the extraction fails.
        /// </returns>
        public static string GetSiteCookieValue()
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetSiteCookieValue());
        }

        /// <summary>
        /// Convenience method for refreshing the current page (equivalent to clicking the browser's refresh button)
        /// </summary>
        public static void RefreshPage()
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.RefreshPage());
        }
    }
}