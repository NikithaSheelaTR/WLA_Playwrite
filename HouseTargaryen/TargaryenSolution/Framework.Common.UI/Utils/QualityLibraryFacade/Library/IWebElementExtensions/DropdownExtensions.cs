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
        /// Sets the selected value of the specified dropdown to the specified option
        /// </summary>
        /// <param name="element">The dropdown WebElement being set</param>
        /// <param name="option">The option that should be selected (Can either be the displayed text of the option or the value attribute)</param>
        public static void SetDropdown(this IWebElement element, string option)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.SetDropdown(option, element));
        }
    }
}
