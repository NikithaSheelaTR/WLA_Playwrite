namespace Framework.Common.UI.Products.WestLawNext.Components
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// YourSearchComponent
    /// </summary>
    public class YourSearchComponent : BaseModuleRegressionComponent
    {
        private static readonly By ExpandArrowLocator = By.Id("co_search_advancedSearch_summaryToggle");

        private static readonly By EditLinkLocator = By.Id("co_search_advancedSearch_summaryEdit");

        private static readonly By SummaryTextLocator = By.Id("co_search_advancedSearch_summaryHeaderText");

        private static readonly By ContainerLocator = By.Id("co_search_advancedSearch_summaryHeader");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets text from summary header
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetSummaryText()
        {
            IWebElement link = DriverExtensions.WaitForElement(ExpandArrowLocator);
            if (!link.GetAttribute("class").Equals("co_dropdownArrowCollapsed"))
            {
                link.Click();
            }

            return DriverExtensions.WaitForElement(SummaryTextLocator).GetText();
        }

        /// <summary>
        /// Clicks on Edit link
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>New instance of T page</returns>
        public T ClickEditLink<T>() where T : CommonAdvancedSearchPage
        {
            DriverExtensions.WaitForElement(EditLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}