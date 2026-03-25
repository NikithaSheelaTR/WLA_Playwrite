namespace Framework.Common.UI.Products.WestLawNext.Pages.IpTools
{
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Products.WestLawNext.Components.IpTools;

    /// <summary>
    /// Patents And Applications Category Page
    /// </summary>
    public class PatentsAndApplicationsCategoryPage : CheckboxBrowsePage
    {
        /// <summary>
        /// Select Content For Search Component
        /// </summary>
        public SpecifyContentToSearchComponent SpecifyContentToSearchComponent { get; } = new SpecifyContentToSearchComponent();
    }
}
