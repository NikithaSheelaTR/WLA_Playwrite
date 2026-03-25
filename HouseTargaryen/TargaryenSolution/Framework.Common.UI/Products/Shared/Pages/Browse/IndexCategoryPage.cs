namespace Framework.Common.UI.Products.Shared.Pages.Browse
{
    using Framework.Common.UI.Products.Shared.Components.AlphabeticalIndex;
    using Framework.Common.UI.Products.Shared.DropDowns;

    /// <summary>
    /// Index Category Page
    /// </summary>
    public class IndexCategoryPage : CommonBrowsePage
    {   
        /// <summary>
        /// Delivery Widget
        /// </summary>
        public DeliveryDropdown DeliveryDropdown { get; } = new DeliveryDropdown();

        /// <summary>
        /// Alphabetical index component
        /// </summary>
        public AlphabeticalIndexComponent AlphabeticalIndex { get; } = new AlphabeticalIndexComponent();
    }
}