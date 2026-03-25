namespace Framework.Common.UI.Products.WestLawNext.Pages.IpTools
{
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Products.WestLawNext.Components.IpTools;

    /// <summary>
    /// Intellectual Property Trademark Tab Category Page
    /// ToDo: Identify structure
    /// </summary>
    public class IntellectualPropertyTrademarkCategoryPage : IntellectualPropertyCategoryPage
    {
        /// <summary>
        /// Resources browse component
        /// </summary>
        public ResourcesSectionsCollectionComponent ResourcesComponent { get; }  = new ResourcesSectionsCollectionComponent();

        /// <summary>
        /// Trademark Office Filings Links Component
        /// </summary>
        public TrademarkOfficeFilingsLinksComponent TrademarkOfficeFilingsLinks { get; } = new TrademarkOfficeFilingsLinksComponent();
    }
}