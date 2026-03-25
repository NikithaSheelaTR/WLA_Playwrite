namespace Framework.Common.UI.Raw.WestlawEdge.Pages.LegalAnalaytics
{
    using Framework.Common.UI.Products.WestlawEdge.Components.LegalAnalytics;

    /// <summary>
    /// Litigation Analytics Search page
    /// </summary>
    public class AnalyticsSearchPage : EdgeCommonAuthenticatedWestlawNextPage
    {
        /// <summary>
        /// Gets the header.
        /// </summary>
        public LitigationAnalyticsHeaderComponent HeaderPanel { get; } = new LitigationAnalyticsHeaderComponent();
    }
}
