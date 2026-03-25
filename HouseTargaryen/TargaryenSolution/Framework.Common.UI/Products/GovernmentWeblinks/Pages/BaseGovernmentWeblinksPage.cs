namespace Framework.Common.UI.Products.GovernmentWeblinks.Pages
{
    using Framework.Common.UI.Products.GovernmentWeblinks.Components;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Weblinks base government page class
    /// </summary>
    public class BaseGovernmentWeblinksPage : BaseModuleRegressionPage
    {
        private static readonly By TitleLocator = By.XPath("//div[@id='co_contentColumn']//h1");

        /// <summary>
        /// Footer component
        /// </summary>
        public WeblinksFooterComponent Footer { get; } = new WeblinksFooterComponent();

        /// <summary>
        /// Header component
        /// </summary>
        public WeblinksHeaderComponent Header { get; } = new WeblinksHeaderComponent();

        /// <summary>
        /// Left Column component
        /// </summary>
        public LeftColumnComponent LeftColumn { get; } = new LeftColumnComponent();

        /// <summary>
        /// Gets the title of the page
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetTitle() => DriverExtensions.GetText(TitleLocator);

        /// <summary>
        /// Is the title of the page displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsTitleDisplayed() => DriverExtensions.IsDisplayed(TitleLocator, 5);
    }
}