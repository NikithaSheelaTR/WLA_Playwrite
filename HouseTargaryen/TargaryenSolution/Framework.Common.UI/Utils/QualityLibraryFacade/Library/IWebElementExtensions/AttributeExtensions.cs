namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// The IWebElement Extensions
    /// </summary>
    public static partial class ElementExtensions
    {
        /// <summary>
        /// The get computed style property value.
        /// </summary>
        /// <param name="element"> The element </param>
        /// <param name="propertyName"> The property name </param>
        /// <param name="pseudoElt"> The pseudo elt </param>
        /// <returns> Computed style property value </returns>
        public static string GetComputedStylePropertyValue(this IWebElement element, string propertyName, string pseudoElt = null)
        {
            return
                DriverExtensions.ExecuteScript(
                    "return window.getComputedStyle(arguments[0], arguments[1]).getPropertyValue(arguments[2]);",
                    element,
                    pseudoElt,
                    propertyName).ToString();
        }

        /// <summary>
        /// The get element scroll width
        /// </summary>
        /// <param name="element"> The element </param>
        /// <returns> Element scroll width </returns>
        public static int GetElementScrollWidth(this IWebElement element)
        {
            return int.Parse(DriverExtensions.ExecuteScript("return arguments[0].scrollWidth;", element).ToString());
        }

        /// <summary>
        /// The get element scroll height
        /// </summary>
        /// <param name="element"> The element </param>
        /// <returns> Element scroll height </returns>
        public static int GetElementScrollHight(this IWebElement element)
        {
            return int.Parse(DriverExtensions.ExecuteScript("return arguments[0].scrollHeight;", element).ToString());
        }

        /// <summary>
        /// Get element top position
        /// </summary>
        /// <param name="element"> The element </param>
        /// <returns> Element top position </returns>
        public static double GetElementTopPosition(this IWebElement element)
        {
            return
                double.Parse(
                    DriverExtensions.ExecuteScript("return arguments[0].getBoundingClientRect().top", element)
                                    .ToString());
        }
    }
}
