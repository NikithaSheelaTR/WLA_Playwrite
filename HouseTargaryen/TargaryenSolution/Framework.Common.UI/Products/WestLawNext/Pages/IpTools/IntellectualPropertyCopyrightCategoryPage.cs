namespace Framework.Common.UI.Products.WestLawNext.Pages.IpTools
{
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Products.WestLawNext.Components.IpTools;

    /// <summary>
    /// Intellectual Property Copyright Tab Category Page
    /// ToDo: Identify structure
    /// </summary>
    public class IntellectualPropertyCopyrightCategoryPage : IntellectualPropertyCategoryPage
    {
        /// <summary>
        /// Resources browse component
        /// </summary>
        public ResourcesSectionsCollectionComponent ResourcesComponent { get; }  = new ResourcesSectionsCollectionComponent();

        /// <summary>
        /// Copyright Office Registrations Links Component
        /// </summary>
        public CopyrightOfficeRegistrationsLinksComponent CopyrightOfficeRegistrationsLinks { get; } = new CopyrightOfficeRegistrationsLinksComponent();
    }
}