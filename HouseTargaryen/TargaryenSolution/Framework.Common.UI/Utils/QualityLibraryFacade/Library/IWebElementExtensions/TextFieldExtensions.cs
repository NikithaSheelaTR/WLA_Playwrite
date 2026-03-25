namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions
{
    using Framework.Common.UI.Utils.Browser;

    using OpenQA.Selenium;

    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// The IWebElement Extensions
    /// </summary>
    public static partial class ElementExtensions
    {
        /// <summary>
        /// Clear text in the text field using ctrl + a + delete
        /// </summary>
        /// <param name="element"> Text field element </param>
        public static void ClearUsingButtons(this IWebElement element)
        {
            element.SendKeys(Keys.Control + "a" + Keys.Delete);
        }

        /// <summary>
        /// Sets the text in a text field
        /// </summary>
        /// <param name="element"> The web element for the text field </param>
        /// <param name="text"> The String to set the text as </param>
        public static void SetTextField(this IWebElement element, string text)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.SetTextField(text, element));
        }

        /// <summary>
        /// Sends the keys with a 50 milliseconds delay between the keystrokes
        /// </summary>
        /// <param name="element">The element to send the keystrokes to</param>
        /// <param name="text">The text to enter into the element</param>
        public static void SendKeysSlow(this IWebElement element, string text)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.SendKeysSlow(element, text));
        }
    }
}
