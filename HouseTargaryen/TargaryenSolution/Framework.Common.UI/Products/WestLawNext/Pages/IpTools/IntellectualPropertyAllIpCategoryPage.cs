namespace Framework.Common.UI.Products.WestLawNext.Pages.IpTools
{
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Products.WestLawNext.Components.BrowsePage;
    using Framework.Common.UI.Products.WestLawNext.Components.IpTools;

    /// <summary>
    /// Intellectual Property All Ip Tab Category Page
    /// ToDo: Identify structure
    /// Intellectual Property Resources
    /// IP Office Filings
    /// Tools
    /// Practitioner Insights for Intellectual Property
    /// My Alerts
    /// News and Insight from REUTERS LEGAL
    /// Practical Law Connect Tasks
    /// Featured Publications
    /// Most Popular NewsGlobal Ip Pages and Components (Common)
    /// </summary>
    public class IntellectualPropertyAllIpCategoryPage : IntellectualPropertyCategoryPage
    {
        /// <summary>
        /// Intellectual Property Resources (Links with checkboxes component)
        /// </summary>
        public IntellectualPropertyResourcesBrowseCheckboxComponent IntellectualPropertyResources { get; } = new IntellectualPropertyResourcesBrowseCheckboxComponent();

        /// <summary>
        /// Ip Offic Filings Links Component
        /// </summary>
        public IpOfficeFilingsLinksComponent IpOfficeFilingsLinks { get; } = new IpOfficeFilingsLinksComponent();
    }
}