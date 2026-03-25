namespace Framework.Common.UI.Products.ANZ.Pages
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.ANZ.Components;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Browse Volume page
    /// </summary>
    public class AnzBrowseVolumePage : EdgeCommonBrowsePage
    {
        private static readonly By PageTitleLocator = By.XPath("//div[@class='co_browseHeaderContent']/h1");

        private static readonly By LoadingSpinnerLocator = By.CssSelector(".co_loading, .co_search_ajaxLoading");

        /// <summary>
        /// Initializes a new instance of the <see cref="AnzBrowseVolumePage"/> class.
        /// </summary>
        public AnzBrowseVolumePage()
        {
            // wait while spinner disappear
            DriverExtensions.WaitForElementNotDisplayed(LoadingSpinnerLocator);
        }

        /// <summary>
        /// Header component
        /// </summary>
        public new EdgeHeaderComponent Header { get; } = new EdgeHeaderComponent();

        /// <summary>
        /// Browse Show Hide Checkboxes Component
        /// </summary>
        public BrowseShowHideCheckboxesComponent BrowseShowHideCheckboxes { get; } = new BrowseShowHideCheckboxesComponent();

        /// <summary>
        /// Browse Volume Content Component
        /// </summary>
        public BrowseVolumeContentComponent BrowseVolumeContent { get; } = new BrowseVolumeContentComponent();

        /// <summary>
        /// Page title text
        /// </summary>
        public ILabel PageTitle => new Label(PageTitleLocator);
    }
}
