using OpenQA.Selenium;
using TRGR.Quality.QedArsenal.QualityLibrary.Core.Enums.WebDriver;

namespace TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions
{
    /// <summary>
    /// Extension methods for mouse event triggers via IWebDriver.
    /// </summary>
    public static class MouseEventExtensions
    {
        public static void TriggerMouseEvent(this IWebDriver driver, MouseEventType eventToTrigger, IWebElement targetElement)
        {
            string eventName = eventToTrigger.ToString().ToLower();

            // Try to get the MouseEventAttribute for the proper event name
            var memberInfo = typeof(MouseEventType).GetMember(eventToTrigger.ToString());
            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(MouseEventAttribute), false);
                if (attrs.Length > 0)
                    eventName = ((MouseEventAttribute)attrs[0]).Event;
            }

            string script = $"var event = new MouseEvent('{eventName}', {{'view': window, 'bubbles': true, 'cancelable': true}}); arguments[0].dispatchEvent(event);";
            ((IJavaScriptExecutor)driver).ExecuteScript(script, targetElement);
        }
    }
}
