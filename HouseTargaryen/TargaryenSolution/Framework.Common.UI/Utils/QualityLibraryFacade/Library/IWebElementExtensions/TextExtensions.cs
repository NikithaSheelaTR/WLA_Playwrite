namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions
{
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    using TRGR.Quality.QedArsenal.QualityLibrary.Core.Enums.WebDriver;
    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// The IWebElement Extensions
    /// </summary>
    public static partial class ElementExtensions
    {
        /// <summary>
        /// Returns the text contained within the specified element.
        /// If the element is an input field, it will return the value of the input field.
        /// </summary>
        /// <param name="element">The WebElement that you want the text of</param>
        /// <returns>A String representing the text contained within the specified element</returns>
        public static string GetText(this IWebElement element)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetText(element));
        }

        /// <summary>
        /// Get text from IWebElement
        /// </summary>
        /// <param name="element">The WebElement to get the text of</param>
        /// <param name="textSearchOptionsQl">The options to use when getting the text</param>
        /// <returns> Text </returns>
        public static string GetText(this IWebElement element, params TextSearchOption[] textSearchOptionsQl)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetText(element, textSearchOptionsQl));
        }

        /// <summary>
        /// Method for highlighting part of text
        /// </summary>
        /// <param name="element"> element </param>
        /// <param name="textNodeIndex"> Text Node index </param>
        /// <returns> Highlighted text </returns>
        public static string HighlightText(this IWebElement element, int textNodeIndex = 0)
        {
            string javascript =
                "var doc = document;" +
                "var elementToHighlight = arguments[0];" +
                "var range;" +
                "var selection;" +
                "if (doc.body.createTextRange)" +
                "{" +
                "    range = doc.body.createTextRange();" +
                "    range.moveToElementText(elementToHighlight);" +
                "    range.select();" +
                "}" +
                "else if (window.getSelection)" +
                "{" +
                "    var text;" +
                "    if(elementToHighlight.splitText){" +
                "       text = elementToHighlight;" +
                "    } " +
                "    else {  " +
                "       text = elementToHighlight.childNodes[" + textNodeIndex + "];" +
                "    } " +
                "    selection = window.getSelection();" +
                "    range = doc.createRange();" +
                "    range.selectNodeContents(text);" +
                "    selection.removeAllRanges();" +
                "    selection.addRange(range);" +
                "}";

            DriverExtensions.ExecuteScript(javascript, element);
            return DriverExtensions.GetHighlightedHtml();
        }
    }
}
