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
        /// Returns the IWebElement that is one level in the DOM above the specified element
        /// </summary>
        /// <param name="element">
        /// The element you want the container of
        /// </param>
        /// <returns>
        /// The IWebElement that is one level in the DOM above the specified element
        /// </returns>
        public static IWebElement GetParentElement(this IWebElement element)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetParentElement(element));
        }

        /// <summary>
        /// Returns a WebElement above the specified element in the DOM that matches the specified CSS
        /// </summary>
        /// <param name="element"> The element you want the container of </param>
        /// <param name="containerCss"> The CSS that the desired element matches </param>
        /// <returns> An IWebElement above the specified element in the DOM that matches the specified CSS </returns>
        public static IWebElement GetParentElement(this IWebElement element, string containerCss)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetParentElement(element, containerCss));
        }
    }
}
