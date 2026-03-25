using System.Drawing;

namespace OpenQA.Selenium.Remote
{
    /// <summary>
    /// Stub for RemoteWebElement. Provides LocationOnScreenOnceScrolledIntoView property.
    /// Used in MouseEventExtensions for TriggerMouseEventByPoint.
    /// </summary>
    public class RemoteWebElement
    {
        /// <summary>
        /// Gets the point where the element would be when scrolled into view.
        /// </summary>
        public Point LocationOnScreenOnceScrolledIntoView { get; set; }
    }
}
