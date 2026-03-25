namespace Framework.Common.UI.Products.WestLawNext.Pages.IpTools
{
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Products.WestLawNext.Components.IpTools;

    /// <summary>
    /// Intellectual Property Patent Tab Category Page
    /// ToDo: Identify structure
    /// </summary>
    public class IntellectualPropertyPatentCategoryPage : IntellectualPropertyCategoryPage
    {
        /// <summary>
        /// Resources browse component
        /// </summary>
        public ResourcesSectionsCollectionComponent ResourcesComponent { get; } =
            new ResourcesSectionsCollectionComponent();

        /// <summary>
        /// Patent Office Filings Links Component
        /// </summary>
        public PatentOfficeFilingsLinksComponent PatentOfficeFilingsLinks { get; } =
            new PatentOfficeFilingsLinksComponent();
    }
}