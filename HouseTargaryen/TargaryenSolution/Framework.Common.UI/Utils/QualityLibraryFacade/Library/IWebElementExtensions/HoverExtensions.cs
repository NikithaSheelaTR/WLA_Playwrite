namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions
{
    using Framework.Common.UI.Utils.Browser;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;

    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// The IWebElement Extensions
    /// </summary>
    public static partial class ElementExtensions
    {
        /// <summary>
        /// Hovers over the specified element
        /// </summary>
        /// <param name="element">The WebElement being hovered over</param>
        public static void SeleniumHover(IWebElement element)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.SeleniumHover(element));
        }

        /// <summary>
        /// Hovers to the specified element
        /// </summary>
        /// <param name="element">IWebElement</param>
        /// <param name="offsetX">The horizontal offset to which to move the mouse.</param>
        /// <param name="offsetY">The vertical offset to which to move the mouse.</param>
        public static void SeleniumHover(this IWebElement element, int offsetX = 1, int offsetY = 1)
        {
            Actions builder = BrowserPool.CurrentBrowser.ActionInstance;
            builder.MoveToElement(element).MoveByOffset(offsetX, offsetY);
            builder.Build().Perform();
        }

        /// <summary>
        /// Hovers off of the specified element
        /// </summary>
        /// <param name="element">IWebElement</param>
        /// <param name="offsetX">The horizontal offset to which to move the mouse.</param>
        /// <param name="offsetY">The vertical offset to which to move the mouse.</param>
        public static void SeleniumHoverOut(this IWebElement element, int offsetX = 5, int offsetY = 5)
        {
            var builder = BrowserPool.CurrentBrowser.ActionInstance;
            builder.MoveToElement(element).MoveByOffset(-offsetX, -offsetY);
            builder.Build().Perform();
        }

        /// <summary>
        /// Hovers over the specified element
        /// </summary>
        /// <param name="element">The WebElement being hovered over</param>
        public static void Hover(this IWebElement element)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.Hover(element));
        }

        /// <summary>
        /// Hovers off of the specified element
        /// </summary>
        /// <param name="element">The WebElement being hovered off of</param>
        public static void HoverOut(this IWebElement element)
        {
            BrowserPool.CurrentBrowser.InvokeAction(wd => wd.HoverOut(element));
        }
    }
}
