namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions
{
    using Framework.Common.UI.Utils.Browser;

    using OpenQA.Selenium;

    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// The driver extensions.
    /// </summary>
    public static partial class DriverExtensions
    {
        /// <summary>
        /// Determines if the radio button is selected
        /// </summary>
        /// <param name="elementBys">elementBy</param>
        /// <returns>true or false</returns>
        public static bool IsRadioButtonSelected(params By[] elementBys)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.IsRadioButtonSelected(elementBys));
        }

        /// <summary>
        /// Determines if the radio button is selected
        /// </summary>
        /// <param name="container"> The container. </param>
        /// <param name="elementBys"> elementBy </param>
        /// <returns> true or false/// </returns>
        public static bool IsRadioButtonSelected(IWebElement container, params By[] elementBys)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.IsRadioButtonSelected(container, elementBys));
        }
    }
}