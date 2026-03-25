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
        /// Returns the specified HTML attribute for the specified element
        /// </summary>
        /// <param name="attributeName">The name of the HTML attribute you would like to retrieve</param>
        /// <param name="elementBy">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement to get the attribute of while the others correspond to any containers it might have
        /// </param>
        /// <returns>A String representing the specified HTML attribute for the specified element</returns>
        public static string GetAttribute(string attributeName, params By[] elementBy)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetAttribute(attributeName, elementBy));
        }

        /// <summary>
        /// Returns the specified HTML attribute for the specified element
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="container"></param>
        /// <param name="elementBy">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement to get the attribute of while the others correspond to any containers it might have
        /// </param>
        /// <returns></returns>
        public static string GetAttribute(string attributeName, IWebElement container, params By[] elementBy)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetAttribute(attributeName, container, elementBy));
        }

        /// <summary>
        /// Gets all text of a element regardless of if the text is hidden
        /// </summary>
        /// <param name="container">The container WebElement</param>
        /// <param name="elementBys">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement to get the text of while the others correspond to any containers it might have</param>
        /// <returns>The text</returns>
        public static string GetHiddenText(this IWebElement container, params By[] elementBys)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetHiddenText(container, elementBys));
        }

        /// <summary>
        /// Returns the text contained within the specified element.
        /// If the element is an input field, it will return the value of the input field.
        /// </summary>
        /// <param name="elementBy">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement to get the text of while the others correspond to any containers it might have
        /// </param>
        /// <returns>
        /// The text
        /// </returns>
        public static string GetText(params By[] elementBy)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetText(elementBy));
        }
    }
}