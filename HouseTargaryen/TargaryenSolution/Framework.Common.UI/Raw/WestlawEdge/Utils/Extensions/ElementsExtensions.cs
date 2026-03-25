namespace Framework.Common.UI.Raw.WestlawEdge.Utils.Extensions
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// ElementsExtensions
    /// </summary>
    public static class ElementsExtensions
    {
        /// <summary>
        /// Get Full CSS Path for IWebElement
        /// </summary>
        /// <param name="element">IWebElement</param>
        /// <returns>By</returns>
        public static By GetCssLocator(this IWebElement element)
        {
            const string Script = @"function previousElementSibling (element) {
                                      if (element.previousElementSibling !== 'undefined') {
                                        return element.previousElementSibling;
                                      } else {
                                        // Loop through ignoring anything not an element
                                        while (element = element.previousSibling) {
                                          if (element.nodeType === 1) {
                                            return element;
                                          }
                                        }
                                      }
                                    }
                                    function getPath (element) {
                                      // False on non-elements
                                      if (!(element instanceof HTMLElement)) { return false; }
                                      var path = [];
                                      while (element.nodeType === Node.ELEMENT_NODE) {
                                        var selector = element.nodeName;
                                        if (element.id) { selector += ('#' + element.id); }
                                        else {
                                          // Walk backwards until there is no previous sibling
                                          var sibling = element;
                                          // Will hold nodeName to join for adjacent selection
                                          var siblingSelectors = [];
                                          while (sibling !== null && sibling.nodeType === Node.ELEMENT_NODE) {
                                            siblingSelectors.unshift(sibling.nodeName);
                                            sibling = previousElementSibling(sibling);
                                          }
                                          // :first-child does not apply to HTML
                                          if (siblingSelectors[0] !== 'HTML') {
                                            siblingSelectors[0] = siblingSelectors[0] + ':first-child';
                                          }
                                          selector = siblingSelectors.join(' + ');
                                        }
                                        path.unshift(selector);
                                        element = element.parentNode;
                                      }
                                      return path.join(' > ');
                                     }
                                    return getPath(arguments[0])";
            return By.CssSelector((string)DriverExtensions.ExecuteScript(Script, element));
        }

        /// <summary>
        /// Gets Inner Html of IWebElement
        /// </summary>
        /// <param name="element"> The element to check the position of </param>
        /// <returns>True if the element is in view, false otherwise</returns>
        public static string InnerHtml(this IWebElement element)
        {
            return (string)DriverExtensions.ExecuteScript("return $(arguments[0]).html();", element);
        }
    }
}