namespace Framework.Common.UI.Products.TaxnetPro.Components.Toolbar
{
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;

    /// <summary>
    /// Taxnet Pro toolbar component
    /// </summary>
    public class TaxnetProToolbarComponent : EdgeToolbarComponent
    {
        /// <summary>
        /// Term Navigation Component
        /// </summary>
        public TaxnetProTermNavigationComponent TaxnetProTermNavigation { get; } = new TaxnetProTermNavigationComponent();

        /// <summary>
        /// Subscribed Content Dropdown
        /// </summary>
        public SubscribedContentDropdown SubscribedContentDropdown { get; set; } = new SubscribedContentDropdown();
    }
}
