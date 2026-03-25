namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Custom;

    using OpenQA.Selenium;

    using TRGR.Quality.QedArsenal.QualityLibrary.Core.Enums.WebDriver;
    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// The driver extensions.
    /// </summary>
    public static partial class DriverExtensions
    {
        /// <summary>
        /// Performs a GetElements with the given By on all of the parent elements and returns the union of their children elements.
        /// </summary>
        /// <param name="parentElements">List of parent elements to get the children of</param>
        /// <param name="childrenBy">The By to get the children with</param>
        /// <returns>The list of children WebElements</returns>
        public static IList<IWebElement> GetAllChildrenElements(IList<IWebElement> parentElements, By childrenBy)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetAllChildrenElements(parentElements, childrenBy));
        }

        /// <summary>
        /// Returns the desired element corresponding to the specified By identifier
        /// </summary>
        /// <param name="elementBy">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement while the others correspond to any containers it might have
        /// </param>
        /// <returns>
        /// An IWebElement matching the specified By identifier
        /// </returns>
        public static IWebElement GetElement(params By[] elementBy)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetElement(elementBy));
        }

        /// <summary>
        /// Gets Element, using By identifier
        /// </summary>
        /// <param name="elementBy">elementBy
        /// </param>
        /// <returns>
        /// The <see cref="IWebElement"/>.
        /// </returns>
        public static IWebElement GetElement(By elementBy)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetElement(elementBy));
        }

        /// <summary>
        /// Returns the desired element corresponding to the specified By identifier within the specified containing element
        /// </summary>
        /// <param name="container">
        /// An IWebElement representing the the container of the desired IWebElement
        /// </param>
        /// <param name="elementBy">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElement while the others correspond to any containers it might have
        /// </param>
        /// <returns>
        /// The <see cref="IWebElement"/>.
        /// </returns>
        public static IWebElement GetElement(IWebElement container, params By[] elementBy)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetElement(container, elementBy));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="optionsQl"></param>
        /// <param name="elementBys"></param>
        /// <returns></returns>
        public static IWebElement GetElementByText(
            string searchText,
            TextSearchOption[] optionsQl,
            params By[] elementBys)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetElementByText(searchText, optionsQl, elementBys));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="optionsQl"></param>
        /// <param name="container"></param>
        /// <param name="elementBys"></param>
        /// <returns></returns>
        public static IWebElement GetElementByText(
            string searchText,
            TextSearchOption[] optionsQl,
            IWebElement container,
            params By[] elementBys)
        {
            return
                BrowserPool.CurrentBrowser.InvokeFunc(
                    wd => wd.GetElementByText(searchText, optionsQl, container, elementBys));
        }

        /// <summary>
        /// Returns a list of IWebElements corresponding to the specified By identifier
        /// </summary>
        /// <param name="elementsBys">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElements while the others correspond to any containers it might have
        /// </param>
        /// <returns>
        /// A list of IWebElements corresponding to the specified By identifier
        /// </returns>
        public static IList<IWebElement> GetElements(params By[] elementsBys)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetElements(elementsBys));
        }

        /// <summary>
        /// wrapper for FindElements
        /// </summary>
        /// <param name="elementsBys">elementsBy
        /// </param>
        /// <returns>
        /// The collection
        /// </returns>
        public static IReadOnlyCollection<IWebElement> GetElements(By elementsBys)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd =>wd.GetElements(elementsBys));
        }

        /// <summary>
        /// wrapper for FindElements with additional filter condition.
        /// </summary>
        /// <param name="elementsBy">elementsBy
        /// </param>
        /// <param name="condition">The Condition to filter elements.</param>
        /// <returns>
        /// The collection
        /// </returns>
        public static IReadOnlyCollection<IWebElement> GetElements(By elementsBy, Func<IWebElement, bool> condition)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetElements(elementsBy)).Where(condition).ToArray();
        }

        /// <summary>
        /// Returns a list of IWebElements corresponding to the specified By identifier within the specified container IWebElement
        /// </summary>
        /// <param name="container">
        /// An IWebElement representing the container of the desired IWebElements
        /// </param>
        /// <param name="elementsBys">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElements while the others correspond to any containers it might have
        /// </param>
        /// <returns>
        /// A list of IWebElements corresponding to the specified By identifier within the specified container IWebElement
        /// </returns>
        public static IList<IWebElement> GetElements(IWebElement container, params By[] elementsBys)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetElements(container, elementsBys));
        }

        /// <summary>
        /// Returns a list of IWebElements corresponding to the specified By identifier within the specified container IWebElement
        /// </summary>
        /// <param name="container">
        /// An IWebElement representing the container of the desired IWebElements
        /// </param>
        /// <param name="elementsBy">
        /// A variable number of WebElement By identifier arguments. The last argument corresponds to the desired 
        /// WebElements while the others correspond to any containers it might have
        /// </param>
        /// <param name="condition">The Condition to filter elements.</param>
        /// <returns>
        /// A list of IWebElements corresponding to the specified By identifier within the specified container IWebElement
        /// </returns>
        public static IList<IWebElement> GetElements(IWebElement container, By elementsBy, Func<IWebElement, bool> condition)
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetElements(container, elementsBy, condition));
        }

        /// <summary>
        /// Gets Elements By Text
        /// </summary>
        /// <param name="searchText">searchText
        /// </param>
        /// <param name="optionsQl">options
        /// </param>
        /// <param name="elementBys">elementBy
        /// </param>
        /// <returns>
        /// The list
        /// </returns>
        public static List<IWebElement> GetElementsByText(
            string searchText,
            TextSearchOption[] optionsQl,
            params By[] elementBys)
        {
            return
                BrowserPool.CurrentBrowser.InvokeFunc(
                    wd =>
                        wd.GetElementsByText(
                            searchText,
                            optionsQl,
                            elementBys));
        }

        /// <summary>
        /// Get element that currently has focus
        /// </summary>
        /// <returns></returns>
        public static IWebElement GetFocusedElement()
        {
            return BrowserPool.CurrentBrowser.InvokeFunc(wd => wd.GetFocusedElement());
        }
    }
}