namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions
{
    using System;

    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    using TRGR.Quality.QedArsenal.QualityLibrary.Core.Enums.WebDriver;
    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// The IWebElement Extensions
    /// </summary>
    public static partial class ElementExtensions
    {
        /// <summary>
        /// Used to directly trigger a mouse event. 
        /// Unlike TriggerMouseEvent method it uses a location of an element and clicks directly on the point
        /// </summary>
        /// <param name="targetElement"> targetElement </param>
        /// <param name="eventToTrigger"> eventToTrigger </param>
        public static void TriggerMouseEventByPoint<T>(this IWebElement targetElement, T eventToTrigger) where T : struct
        {
            int clientX = ((RemoteWebElement)targetElement).LocationOnScreenOnceScrolledIntoView.X + (targetElement.Size.Width / 2);
            int clientY = ((RemoteWebElement)targetElement).LocationOnScreenOnceScrolledIntoView.Y + (targetElement.Size.Height / 2);

            string script = "var event = new MouseEvent('" + (eventToTrigger as Enum).GetAttribute<MouseEventAttribute>().Event + "', {'view': window,'bubbles': true,'cancelable': true, 'clientX': " + clientX + ", 'clientY': " + clientY + "});var el = document.elementFromPoint(" + clientX + "," + clientY + "); el.dispatchEvent(event);";

            DriverExtensions.ExecuteScript(script);
        }

        /// <summary>
        /// Used to directly trigger a mouse event.  NOTE - This should only be used as a last resort and is to be avoided 
        /// at all costs, as it will lead to unstable test cases.  There are very few scenarios where this is absolutely
        /// required, and it should only be used in those few situations.
        /// </summary>
        /// <param name="targetElement"> targetElement </param>
        /// <param name="eventToTrigger"> eventToTrigger </param>
        public static void TriggerMouseEvent(this IWebElement targetElement, MouseEventType eventToTrigger)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.TriggerMouseEvent(eventToTrigger, targetElement));
        }
    }
}
