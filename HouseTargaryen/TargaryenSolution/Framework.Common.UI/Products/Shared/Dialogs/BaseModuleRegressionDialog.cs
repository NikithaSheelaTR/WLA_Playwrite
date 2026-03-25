namespace Framework.Common.UI.Products.Shared.Dialogs
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Pages;

    using OpenQA.Selenium;

    /// <summary>
    /// Base component object for module regression suites
    /// </summary>
    public abstract class BaseModuleRegressionDialog : BaseWebObject, ICreatablePageObject
    {
        /// <summary>
        /// The element click.
        /// </summary>
        /// <param name="elementLocator"> The element locator. </param>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        protected T ClickElement<T>(By elementLocator) where T : ICreatablePageObject
        {
            this.ClickElement(elementLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// The element click.
        /// </summary>
        /// <param name="element"> The element. </param>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        protected T ClickElement<T>(IWebElement element) where T : ICreatablePageObject
        {
            this.ClickElement(element);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// The element click.
        /// </summary>
        /// <param name="parentElement"> The parent element. </param>
        /// <param name="locators"> The locators. </param>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        protected T ClickElement<T>(IWebElement parentElement, params By[] locators) where T : ICreatablePageObject
        {
            this.ClickElement(parentElement, locators);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// The element click.
        /// </summary>
        /// <param name="elementLocator"> The element locator. </param>
        protected void ClickElement(By elementLocator) => this.ClickElement(DriverExtensions.WaitForElement(elementLocator));

        /// <summary>
        /// The element click.
        /// </summary>
        /// <param name="element">The element.</param>
        protected void ClickElement(IWebElement element)
        {
            element.WaitForElementDisplayed();
            DriverExtensions.Click(element);
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// The element click.
        /// </summary>
        /// <param name="parentElement">The parent element.</param>
        /// <param name="locators">The locators.</param>
        protected void ClickElement(IWebElement parentElement, params By[] locators) 
            => this.ClickElement(DriverExtensions.GetElement(parentElement, locators));

        /// <summary>
        /// Waits for the dialog to not longer show the processing spinner
        /// </summary>
        /// <param name="timeOut"> Time Out </param>
        /// <param name="elementLocator"> Element locator </param>
        /// <typeparam name="T"> T type </typeparam>
        /// <returns> T page </returns>
        protected T WaitForUpdateComplete<T>(int timeOut, By elementLocator) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementNotDisplayed(timeOut, elementLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}