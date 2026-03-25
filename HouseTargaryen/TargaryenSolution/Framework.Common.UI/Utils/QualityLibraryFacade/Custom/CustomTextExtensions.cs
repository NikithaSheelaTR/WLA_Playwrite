namespace Framework.Common.UI.Utils.QualityLibraryFacade.Custom
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// Custom Action extensions
    /// </summary>
    public static partial class CustomExtensions
    {
        /// <summary>
        /// HighlightMultipleNodes
        /// </summary>
        /// <param name="driver">the current <see cref="IWebDriver"/> (note that this is an extension method, 
        /// so it should be called as driver.method(), do not pass the driver as an argument</param> 
        /// <param name="startElement"></param>
        /// <param name="endElement"></param>
        /// <returns></returns>
        public static string HighlightMultipleNodes(this IWebDriver driver, IWebElement startElement, IWebElement endElement)
        {
            string javascript =
                    "var doc = document;" +
                    "var startElement = arguments[0];" +
                    "var endElement = arguments[1];" +

                    "var startNode = doc.createTextNode(\"\");" +
                    "var endNode = doc.createTextNode(\"\");" +

                    "startElement.parentNode.insertBefore(startNode, startElement);" +
                    "endElement.parentNode.appendChild(endNode);" +

                    "var selection = window.getSelection();" +
                    "var range = doc.createRange();" +
                    "range.setStart(startNode, 0);" +
                    "range.setEnd(endNode, 0);" +
                    "selection.removeAllRanges();" +
                    "selection.addRange(range);";

            driver.ExecuteScript(javascript, startElement, endElement);

            return driver.GetHighlightedHtml();
        }

        /// <summary>
        /// index of first element in the array with empty highlighting.
        /// </summary>
        /// <returns> string index </returns>
        public static string GetIndexOfFirstEmptySnippetHighlight()
        {
            // Javascript to return index of first element in the array with empty highlighting.
            return (string)DriverExtensions.ExecuteScript("return $.inArray(\"\", $(\"span.co_searchTerm\").map(function(){return $.trim($(this).text());}).get());").ToString();
        }

        /// <summary>
        /// The get text safe.
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="elementBy">
        /// The element by.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetTextSafe(this IWebDriver driver, IWebElement container, By elementBy)
        {
            IWebElement webElement;
            DriverExtensions.TryGetElement(container, elementBy, out webElement);
            return webElement != null ? webElement.Text : string.Empty;
        }

        /// <summary>
        /// GetHighlightedHtml
        /// </summary>
        /// <param name="driver">the current <see cref="IWebDriver"/> (note that this is an extension method, 
        /// so it should be called as driver.method(), do not pass the driver as an argument</param> 
        /// <returns></returns>
        public static string GetHighlightedHtml(this IWebDriver driver)
        {
            string javascript = "var html = \"\";" +
                                      "if (typeof window.getSelection != \"undefined\")" +
                                      "{" +
                                      "	var sel = window.getSelection();" +
                                      "	if (sel.rangeCount)" +
                                      "	{" +
                                      "   	var container = document.createElement(\"div\");" +
                                      "   	for (var i = 0, len = sel.rangeCount; i < len; ++i)" +
                                      "		{" +
                                      "       	container.appendChild(sel.getRangeAt(i).cloneContents());" +
                                      "		}" +
                                      "		html = container.innerHTML;" +
                                      "	}" +
                                      "}" +
                                      "else if (typeof document.selection != \"undefined\")" +
                                      "{" +
                                      "	if (document.selection.type == \"Text\")" +
                                      "	{" +
                                      "		html = document.selection.createRange().htmlText;" +
                                      "	}" +
                                      "}" +
                                      " return html;";

            return (string)driver.ExecuteScript(javascript);
        }
    }
}