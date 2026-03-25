namespace Framework.Common.UI.Utils.QualityLibraryFacade.Custom
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using OpenQA.Selenium;
    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// The custom extensions.
    /// </summary>
    public static partial class CustomExtensions
    {
        /// <summary>
        /// Returns a list of IWebElements corresponding to the specified By identifier within the specified container IWebElement
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        /// <param name="container">
        /// An IWebElement representing the container of the desired IWebElements
        /// </param>
        /// <param name="elementsBy">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElements while the others correspond to any containers it might have
        /// </param>
        /// <param name="condition">
        /// The Condition to filter elements.
        /// </param>
        /// <returns>
        /// A list of IWebElements corresponding to the specified By identifier within the specified container IWebElement
        /// </returns>
        public static IList<IWebElement> GetElements(this IWebDriver driver, IWebElement container, By elementsBy, Func<IWebElement, bool> condition)
        {
            return driver.GetElements(container, elementsBy).Where(condition).ToArray();
        }

        /// <summary>
        /// Get element that currently has focus
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static IWebElement GetFocusedElement(this IWebDriver driver)
        {
            string script = "return window.document.activeElement";
            return (IWebElement)driver.ExecuteScript(script);
        }
    }
}
