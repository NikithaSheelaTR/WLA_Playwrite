namespace Framework.Common.UI.Products.WestLawNext.Pages.IpTools
{
    using Framework.Common.UI.Products.WestLawNext.Components.IpTools;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// IP Tools Page
    /// </summary>
    public class IpToolsPage : TabPage
    {
        private static readonly By IpToolsClaimsHistoryLocator = By.Id("coid_relatedInfo_dashboard_element_IPTools_ClaimsHistory");

        private static readonly By IpToolsOverviewRiContentLocator = By.Id("coid_website_relatedInformationContent");

        private static readonly By IpToolsPatentFamilyTimelineLocator = By.Id("coid_relatedInfo_dashboard_element_IPTools_PatentFamilyTimeline");

        private static readonly By IpToolsPatentFamilyTreeLocator = By.Id("coid_relatedInfo_dashboard_element_IPTools_PatentFamilyTree");

        private static readonly By IpToolsPatentFileHistoryLocator = By.Id("coid_relatedInfo_dashboard_element_IPTools_PatentFileHistory");

        private static readonly By IpToolsPatentTextMateLocator = By.Id("coid_relatedInfo_dashboard_element_IPTools_ClaimsLocator");

        private static readonly By IpToolsReferencesCitedSectionLocator = By.Id("coid_relatedInfo_dashboard_element_IPTools_ReferencesCited");
        
        /// <summary>
        /// Claims History
        /// </summary>
        public DashboardComponent ClaimsHistory => new DashboardComponent(IpToolsClaimsHistoryLocator);

        /// <summary>
        /// Patent FamilyTime line
        /// </summary>
        public DashboardComponent PatentFamilyTimeline => new DashboardComponent(IpToolsPatentFamilyTimelineLocator);

        /// <summary>
        /// Patent FamilyTree
        /// </summary>
        public DashboardComponent PatentFamilyTree => new DashboardComponent(IpToolsPatentFamilyTreeLocator);

        /// <summary>
        /// Patent FileHistory
        /// </summary>
        public DashboardComponent PatentFileHistory => new DashboardComponent(IpToolsPatentFileHistoryLocator);

        /// <summary>
        /// Patent TextMate 
        /// </summary>
        public DashboardComponent PatentTextMate => new DashboardComponent(IpToolsPatentTextMateLocator);

        /// <summary>
        /// References Cited
        /// </summary>
        public DashboardComponent ReferencesCited => new DashboardComponent(IpToolsReferencesCitedSectionLocator);

        /// <summary>
        /// Is IPTools Page Displayed
        /// </summary>
        /// <returns>True if IPToolsPage is displayed, otherwise false</returns>
        public bool IsIpToolsPageDisplayed() => DriverExtensions.IsDisplayed(IpToolsOverviewRiContentLocator, 5);
    }
}