namespace Framework.Common.UI.Products.WestLawNext.Pages
{
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Components;
    using Framework.Common.UI.Products.WestLawNext.Enums.Toolbars;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Practical law page
    /// </summary>
    public class PracticalLawPage : CommonAuthenticatedWestlawNextPage
    {
        private static readonly By BestPortionArrowLocator = By.ClassName("co_pinpointIcon");

        private EnumPropertyMapper<SearchTermNavigationOption, WebElementInfo> termOptions;

        /// <summary>
        /// Gets the header.
        /// </summary>
        public new EdgeHeaderComponent Header { get; } = new EdgeHeaderComponent();

        /// <summary>
        /// TermOptions Dropdown
        /// </summary>
        public EnumPropertyMapper<SearchTermNavigationOption, WebElementInfo> TermOptions
            =>
                this.termOptions =
                    this.termOptions ?? EnumPropertyModelCache.GetMap<SearchTermNavigationOption, WebElementInfo>();

        /// <summary>
        /// Verify if best portion green arrow is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsBestPortionArrowDisplayed() => DriverExtensions.IsDisplayed(BestPortionArrowLocator, 5);

        /// <summary>
        /// Verify if certain option is present in Terms Navigation dropdown
        /// </summary>
        /// <param name="option"> Option </param>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsTermsNavigationOptionDisplayed(SearchTermNavigationOption option)
            => DriverExtensions.IsDisplayed(By.XPath(this.TermOptions[option].LocatorString), 5);
    }
}