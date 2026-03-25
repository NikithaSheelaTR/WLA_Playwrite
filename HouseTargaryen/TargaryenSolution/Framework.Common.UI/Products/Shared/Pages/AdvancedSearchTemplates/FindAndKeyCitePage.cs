namespace Framework.Common.UI.Products.Shared.Pages.AdvancedSearchTemplates
{
    using Framework.Common.UI.Products.Shared.Components.AdvancedSearchTemplates;
    using Framework.Common.UI.Products.WestLawNext.Pages;

    /// <summary>
    /// Find and KeyCite Page
    /// </summary>
    public class FindAndKeyCitePage : CommonAuthenticatedWestlawNextPage
    {
        /// <summary>
        /// Find and KeyCite Widget
        /// </summary>
        public FindAndKeyCiteComponent FindAndKeyCiteComponent { get; } = new FindAndKeyCiteComponent();
    }
}