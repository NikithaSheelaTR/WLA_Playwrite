namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions
{
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// The IWebElement Extensions
    /// </summary>
    public static partial class ElementExtensions
    {
        /// <summary>
        /// Checks to see if the specified element is displayed
        /// </summary>
        /// <param name="element">The IWebElement being checked</param>
        /// <returns>True if the element is visible and False if it is not</returns>
        public static bool IsDisplayed(this IWebElement element)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.IsDisplayed(element));
        }

        /// <summary>
        /// Determines if the element is scrolled in view to the user in the browser window
        /// </summary>
        /// <param name="element"> The element to check the position of </param>
        /// <returns>True if the element is in view, false otherwise</returns>
        public static bool IsElementInView(this IWebElement element)
        {
            const string Script = @" var innerHtml = $(arguments[0]).html();
                                     if (!innerHtml)
                                     {
                                      $(arguments[0]).append('Fake');
                                     } 
                                     var offset = $(arguments[0]).offset();

                                     var posY = offset.top - $(window).scrollTop();
                                     var posX = offset.left - $(window).scrollLeft(); 
                                     var offsetWidth = parseInt($(arguments[0]).css('padding-left').replace('px', ''));
                                     var offsetHeight = parseInt($(arguments[0]).css('padding-top').replace('px', ''));
                                     var adjustedPosX = posX+offsetWidth+1;
                                     var adjustedPosY = posY+offsetHeight+1;
                                     var parentBlockElement = $(arguments[0]).parents().filter(function() {return $(this).css('display') === 'block';}).first()[0];
                                     var self = document.elementFromPoint(adjustedPosX, adjustedPosY) === arguments[0];
                                     var parent = document.elementFromPoint(adjustedPosX, adjustedPosY) === parentBlockElement;
                                     var child = arguments[0].contains(document.elementFromPoint(adjustedPosX, adjustedPosY));
                                     if (!innerHtml)
                                     {
                                      $(arguments[0]).empty();
                                     }
                                     return self || parent || child;";
            return (bool)DriverExtensions.ExecuteScript(Script, element);
        }
    }
}
