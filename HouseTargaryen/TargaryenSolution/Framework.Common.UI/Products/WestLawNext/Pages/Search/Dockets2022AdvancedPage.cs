namespace Framework.Common.UI.Products.WestLawNext.Pages.Search
{
    using Framework.Common.UI.Products.Shared.Components.Docket;

    /// <summary>
    /// Dockets 2022 advanced page
    /// TODO: Replace with DocketsAdvancedSearchPage once Searchable PDF feature is released 
    /// </summary>
    public class Dockets2022AdvancedPage : CommonAdvancedSearchPage
    {
        /// <summary>
        /// Browse Component
        /// </summary>
        public DocketsAdvancedTabPanel TabPanel { get; } = new DocketsAdvancedTabPanel();
    }
}
