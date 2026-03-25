namespace Framework.Common.UI.Raw.WestlawEdge.Pages.GradingTool
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestlawEdge.DropDowns.GradingTool;
    using Framework.Common.UI.Products.WestlawEdge.Enums.GradingTool;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The indigo grading tool new query page.
    /// </summary>
    public class EdgeNewQueryPage : BaseModuleRegressionPage
    {
        private static readonly By PageTitleLocator = By.XPath("//h1[text()='New Query']");
        private static readonly By QueryTextBoxLocator = By.ClassName("co_gradingToolSearchInput");
        private static readonly By CreateNewQueryButtonLocator = By.XPath("//button[text()='Create New Query']");
        private static readonly By SelectExperimentDropdownLocator = By.XPath("//select[@class='co_experimentOptions']");
        private static readonly By ContentTypeDropdownLocator = By.XPath("//select[@class='co_contentTypeOptions']");

        /// <summary>
        /// Gets the select experiment dropdown.
        /// </summary>
        public IDropdown<string> SelectExperimentDropdown { get; } = new Dropdown(SelectExperimentDropdownLocator);

        /// <summary>
        /// Gets the content type dropdown.
        /// </summary>
        public IDropdown<ContentType> ContentTypeDropdown { get; } =
            new Dropdown<ContentType>(ContentTypeDropdownLocator)
                {
                    SourceFolder = @"Resources/EnumPropertyMaps/WestlawEdge/Content"
                };

        /// <summary>
        /// Verifies that the page title is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if page title is displayed. </returns>
        public bool IsPageTitleDisplayed() => DriverExtensions.IsDisplayed(PageTitleLocator);

        /// <summary>
        /// Clicks create new query button.
        /// </summary>
        /// <returns> The <see cref="EdgeGradingToolSearchResultPage"/>. </returns>
        public EdgeGradingToolSearchResultPage ClickCreateNewQueryButton()
        {
            DriverExtensions.Click(CreateNewQueryButtonLocator);
            return new EdgeGradingToolSearchResultPage();
        }

        /// <summary>
        /// Enters query.
        /// </summary>
        /// <param name="query"> The query. </param>
        /// <returns> The <see cref="EdgeNewQueryPage"/>. </returns>
        public EdgeNewQueryPage EnterQuery(string query)
        {
            DriverExtensions.SetTextField(query, QueryTextBoxLocator);
            return this;
        }
    }
}
