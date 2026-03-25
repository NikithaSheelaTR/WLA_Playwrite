
namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using OpenQA.Selenium;

    /// <summary>
    /// Indigo  Intellectual ptoperty Recent Filters Facet Component
    /// </summary>
    public class EdgeIpSearchFacetsFilterComponent : EdgeBaseFacetsFilterComponent
    {
        private static readonly By DesignTypeFacetContainerLocator = By.XPath("//div[@id='facet_div_trademarkDesignType']");

        private static readonly By DesignCategoryFacetLocator = By.XPath("//div[contains(@id,'facet_div_') and contains(@id,'DesignCategory')]");

        private static readonly By ExaminerFacetContainerLocator = By.XPath("//div[@id='facet_div_patentsExaminer']");

        private static readonly By OwnerFacetContainerLocator = By.XPath("//div[contains(@id, 'facet_div_') and contains(@id,'Owner')]");

        private static readonly By InventorFacetContainerLocator = By.XPath("//div[@id='facet_div_ipInventor']");

        private static readonly By InternationalClassFacetLocator = By.XPath("//div[contains(@id,'facet_div_') and contains(@id,'ClassInternational')]");

        private static readonly By JurisdictionFacetContainerLocator = By.XPath("//div[@id='facet_div_ipJurisdiction']");

	    private static readonly By PatentTypeFacetContainerLocator = By.XPath("//div[@id='facet_div_patentsPatentType']");

        private static readonly By StatusFacetContainerLocator = By.XPath("//div[contains(@id, 'facet_div_') and contains(@id,'Status')]");

        private static readonly By ViennaCodeFacetContainerLocator = By.XPath("//div[contains(@id, 'facet_div_') and contains(@id,'ViennaCode')]");

        /// <summary>
        ///Design Type Facet
        /// </summary>
        public BaseSearchHierarchyFacetComponent DesignTypeFacet  => new BaseSearchHierarchyFacetComponent(DesignTypeFacetContainerLocator);

        /// <summary>
        ///Design Category Facet
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent DesignCategoryFacet  => new EdgeBaseFacetWithAppearingDialogComponent(DesignCategoryFacetLocator, "FacetsWithDialogs");

        /// <summary>
        ///Filed Date Facet
        /// </summary>
        public DateFacetComponent FiledDateFacet { get; } = new DateFacetComponent("Filed Date", "");

        /// <summary>
        ///Granted Date Facet
        /// </summary>
        public DateFacetComponent GrantedDateFacet { get; } = new DateFacetComponent("Granted Date", "");

        /// <summary>
        ///Publication Date Facet
        /// </summary>
        public DateFacetComponent PublicationDateFacet { get; } = new DateFacetComponent("Publication Date", "");


        /// <summary>
        ///Published Date Facet
        /// </summary>
        public DateFacetComponent PublishedDateFacet { get; } = new DateFacetComponent("Published Date", "");

        /// <summary>
        ///Priority Date Facet
        /// </summary>
        public DateFacetComponent PriorityDateFacet { get; } = new DateFacetComponent("Priority Date", "");

        /// <summary>
        ///Registration Date Facet
        /// </summary>
        public DateFacetComponent RegistrationDateFacet { get; } = new DateFacetComponent("Registration Date", "");

        /// <summary>
        /// Examiner Facet
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent ExaminerFacet => new EdgeBaseFacetWithAppearingDialogComponent(ExaminerFacetContainerLocator, "FacetsWithDialogs");

        /// <summary>
        /// Inventor Facet
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent InventorFacet => new EdgeBaseFacetWithAppearingDialogComponent(InventorFacetContainerLocator, "FacetsWithDialogs");

        /// <summary>
        /// International  Class Facet
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent InternationalClassFacet => new EdgeBaseFacetWithAppearingDialogComponent(InternationalClassFacetLocator, "FacetsWithDialogs");

        /// <summary>
        /// Owner Facet
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent OwnerFacet => new EdgeBaseFacetWithAppearingDialogComponent(OwnerFacetContainerLocator, "FacetsWithDialogs");

        /// <summary>
        /// Status Facet
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent StatusFacet => new EdgeBaseFacetWithAppearingDialogComponent(StatusFacetContainerLocator, "FacetsWithDialogs");

        /// <summary>
        /// Jurisdiction Facet
        /// </summary>
        public EdgeIpCheckboxTreeFacetComponent JurisdictionCheckboxTreeFacet => new EdgeIpCheckboxTreeFacetComponent(JurisdictionFacetContainerLocator);

	    /// <summary>
	    /// Patent Type Facet
	    /// </summary>
	    public EdgeIpCheckboxTreeFacetComponent PatentTypeCheckboxTreeFacet => new EdgeIpCheckboxTreeFacetComponent(PatentTypeFacetContainerLocator);

        /// <summary>
        /// Vienna Code Facet
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent ViennaCodeFacet => new EdgeBaseFacetWithAppearingDialogComponent(ViennaCodeFacetContainerLocator, "FacetsWithDialogs");
	}
}
