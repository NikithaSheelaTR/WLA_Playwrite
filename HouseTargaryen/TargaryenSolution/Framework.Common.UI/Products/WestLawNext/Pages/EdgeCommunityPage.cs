namespace Framework.Common.UI.Products.WestLawNext.Pages
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestlawEdge.Components;

    /// <summary>
    /// Edge community page
    /// </summary>
    public class EdgeCommunityPage : CommunityPage
    {    
        /// <summary>
        /// Edge Header Component
        /// </summary>
        public new EdgeHeaderComponent Header { get; protected set; } = new EdgeHeaderComponent();
    }
}
