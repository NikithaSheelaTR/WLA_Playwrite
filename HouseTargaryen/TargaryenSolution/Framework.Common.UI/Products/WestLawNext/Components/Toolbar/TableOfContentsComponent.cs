namespace Framework.Common.UI.Products.WestLawNext.Components.Toolbar
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Contains all methods pertaining to the Terms of Condition widget in the toolbar
    /// </summary>
    public class TableOfContentsComponent : BaseModuleRegressionPage
    {
        private static readonly By NextSectionArrowLocator = By.Id("co_document_tocNavigationNextSection");

        private static readonly By PreviousSectionArrowLocator = By.Id("co_document_tocNavigationPreviousSection"); 

        private static readonly By TableOfContentsLinkLocator = By.Id("co_tocLink");

        private const string ContentsLinkLocator = "//div[@id='co_docTocOverlay']//a[text()='{0}']";

        /// <summary>
        /// Determines if the next button is present
        /// </summary>
        /// <returns>returns if the next button is present</returns>
        public bool IsNextArrowDisplayed() => DriverExtensions.IsDisplayed(NextSectionArrowLocator);

        /// <summary>
        /// Determines if the next button is displayed
        /// </summary>
        /// <returns>returns if the next button is displayed</returns>
        public bool IsPreviousArrowDisplayed() => DriverExtensions.IsDisplayed(PreviousSectionArrowLocator);

        /// <summary>
        /// Determines if the Table of contents link is displayed
        /// </summary>
        /// <returns>returns if the link is displayed</returns>
        public bool IsTableOfContentsLinkDisplayed() => DriverExtensions.IsDisplayed(TableOfContentsLinkLocator);

        /// <summary>
        /// Navigate to a link By link text
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="link">string link</param>
        /// <returns>T Page</returns>
        public T NavigateToDocument<T>(string link) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(TableOfContentsLinkLocator).Click();
            DriverExtensions.WaitForElement(By.XPath(string.Format(ContentsLinkLocator, link))).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks on the TOC Next Document Button in the document toolbar
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>T Page </returns>
        public T NextDocument<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(NextSectionArrowLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks on the TOC Previous Document Button in the document toolbar
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>T Page</returns>
        public T PreviousDocument<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(PreviousSectionArrowLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}