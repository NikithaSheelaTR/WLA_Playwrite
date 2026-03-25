namespace Framework.Common.UI.Utils.Core
{
    using OpenQA.Selenium;

    /// <summary>
    /// Constants used by the various WebDriver extensions.
    /// </summary>
    public class WebDriverConstants
    {
        /// <summary>
        /// The default timeout used when waiting for an element
        /// </summary>
        public const int DefaultTimeoutInMilliseconds = 30000;

        /// <summary>
        /// The default wait time to verify an element is NOT present
        /// </summary>
        public const int DefaultNotPresentWaitInMilliseconds = 10000;

        /// <summary>
        /// The default container element to use if one is not specified
        /// </summary>
        public static readonly By DefaultContainerElementBy = By.TagName("body");
    }
}
