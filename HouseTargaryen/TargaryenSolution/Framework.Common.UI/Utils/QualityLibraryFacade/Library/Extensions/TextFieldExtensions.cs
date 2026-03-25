namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions
{
    using Framework.Common.UI.Utils.Browser;

    using OpenQA.Selenium;

    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// The driver extensions.
    /// </summary>
    public partial class DriverExtensions
    {
        /// <summary>
        /// Sets the text in a text field
        /// </summary>
        /// <param name="text">
        /// The String to set the text as
        /// </param>
        /// <param name="elementBy">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// text field while the others correspond to any containers it might have
        /// </param>
        public static void SetTextField(string text, params By[] elementBy)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.SetTextField(text, elementBy));
        }

        /// <summary>
        /// Sets the text in a text field
        /// </summary>
        /// <param name="containerElement">The container web element</param>
        /// <param name="elementBy">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// text field while the others correspond to any containers it might have
        /// </param>
        /// <param name="text">The String to set the text as</param>
        public static void SetTextField(string text, IWebElement containerElement, params By[] elementBy)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.SetTextField(text, containerElement, elementBy));
        }
    }
}