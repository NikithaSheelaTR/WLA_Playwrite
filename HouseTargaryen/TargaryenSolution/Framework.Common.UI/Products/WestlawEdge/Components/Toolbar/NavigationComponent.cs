namespace Framework.Common.UI.Products.WestlawEdge.Components.Toolbar
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Contains all methods pertaining to the NavigationComponent
    /// </summary>
    public class NavigationComponent : BaseModuleRegressionComponent
    {
        private static readonly By NextResultButtonLocator = By.XPath("//*[@id='co_documentFooterResultsNavigationNext' or @id='co_documentResultsNavigationNext']");

        private static readonly By PreviousResultButtonLocator = By.XPath("//*[@id='co_documentFooterResultsNavigationPrevious' or @id='co_documentResultsNavigationPrevious']");

        private static readonly By NavigationNumberTextLocator = By.XPath("//div[@id='co_documentFooterResultsNavigation' or @id='co_documentResultsNavigation']//strong[1]");

        private static readonly By NavigationDocumentCountLocator = By.XPath("//div[@id='co_documentFooterResultsNavigation' or @id='co_documentResultsNavigation']//strong[2]");

        private static readonly By ContainerLocator = By.XPath("//*[@id='co_documentFooterResultsNavigation' or @id='co_documentResultsNavigation']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Clicks Next Button in doc to doc navigation
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>New instance of T page</returns>
        public T ClickNextDocumentButton<T>()
            where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(NextResultButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks Previous Button in doc to doc navigation
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>New instance of T page</returns>
        public T ClickPreviousDocumentButton<T>()
            where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(PreviousResultButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Checks if the NextDocButton is Enabled
        /// </summary>
        /// <returns>Whether or not the NextDocButton is enabled</returns>
        public bool IsNextResultButtonEnabled()
            => !DriverExtensions.GetElement(NextResultButtonLocator).GetAttribute("class").Contains("co_disabled");

        /// <summary>
        /// Gets the current doc navigation number number
        /// </summary>
        /// <returns>The navigation number of the current document</returns>
        public string GetCurrentDocumentNavigationNumber() => DriverExtensions.WaitForElement(NavigationNumberTextLocator).Text;

        /// <summary>
        /// Gets the count of navigation documents
        /// </summary>
        /// <returns>The navigation documents count</returns>
        public int GetNavigationDocumentsCount() =>
            DriverExtensions.WaitForElement(NavigationDocumentCountLocator).Text.ConvertCountToInt();

        /// <summary>
        /// Checks if the PreviousDocButton is Enabled
        /// </summary>
        /// <returns>Whether or not the PreviousDocButton is enabled</returns>
        public bool IsPreviousResultButtonEnabled()
            => !DriverExtensions.GetElement(PreviousResultButtonLocator).GetAttribute("class").Contains("co_disabled");


        /// <summary>
        /// Checks if the NavigationComponent is Displayed
        /// </summary>
        /// <returns>Whether or not the NavigationComponent is displayed</returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, 5);
    }
}