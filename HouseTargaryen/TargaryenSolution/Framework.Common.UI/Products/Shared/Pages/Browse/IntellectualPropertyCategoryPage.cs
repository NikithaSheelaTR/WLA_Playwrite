namespace Framework.Common.UI.Products.Shared.Pages.Browse
{
    using Framework.Common.UI.Products.WestLawNext.Components.IpTools;

    /// <summary>
    /// Intellectual Property Category Page
    /// </summary>
    public class IntellectualPropertyCategoryPage : CommonBrowsePage
    {
        /// <summary>
        /// Browse Selector Component
        /// </summary>
        public IpBrowseSelectorComponent BrowseSelectorComponent { get; } = new IpBrowseSelectorComponent();

        /// <summary>
        /// Select Content For Search Component
        /// </summary>
        public SpecifyContentToSearchComponent SpecifyContentToSearchComponent { get; } = new SpecifyContentToSearchComponent();

        /// <summary>
        /// Tools Links Component
        /// </summary>
        public ToolsLinksComponent ToolsLinkComponent { get; } = new ToolsLinksComponent();

        /// <summary>
        /// Practical Law Connect Tasks Links Component
        /// </summary>
        public PracticalLawConnectTasksLinksComponent PracticalLawConnectTasksLinksComponent { get; } = new PracticalLawConnectTasksLinksComponent();
    }
}