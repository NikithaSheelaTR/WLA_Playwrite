namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;

    /// <summary>
    /// Component representing the select content/search all content widget/bar that appears at the left side for Ip content
    /// Facets filtering
    /// </summary>
    public class IpNarrowPaneComponent : NarrowPaneComponent
    {
        /// <summary>
        /// Filing Date Facet Component
        /// </summary>
        public DateFacetComponent FilingDateFacet => new DateFacetComponent("Filed Date");

        /// <summary>
        /// Granted Date Facet Component
        /// </summary>
        public DateFacetComponent GrantedDateFacet => new DateFacetComponent("Granted Date");

        /// <summary>
        /// Publication Date Facet Component
        /// </summary>
        public DateFacetComponent PublicationDateFacet => new DateFacetComponent("Publication Date");

        /// <summary>
        /// Published Date Facet Component
        /// </summary>
        public DateFacetComponent PublishedDateFacet => new DateFacetComponent("Published Date");

        /// <summary>
        /// Registration Date Facet Component
        /// </summary>
        public DateFacetComponent RegistrationDateFacet => new DateFacetComponent("Registration Date");

        /// <summary>
        /// Priority Date Facet Component
        /// </summary>
        public DateFacetComponent PriorityDateFacet => new DateFacetComponent("Priority Date");

        /// <summary>
        /// Design type Facet Component
        /// </summary>
        public DesignTypeFacetComponent DesignTypeFacet => new DesignTypeFacetComponent();

        /// <summary>
        /// Design category Facet Component
        /// </summary>
        public DesignCategoryFacetComponent DesignCategoryFacet => new DesignCategoryFacetComponent();

        /// <summary>
        /// Inventor Facet Component
        /// </summary>
        public InventorFacetComponent InventorFacet => new InventorFacetComponent();

        /// <summary>
        /// Owner Facet Component
        /// </summary>
        public OwnerFacetComponent OwnerFacet => new OwnerFacetComponent();

        /// <summary>
        /// Examiner Facet Component
        /// </summary>
        public ExaminerFacetComponent ExaminerFacet => new ExaminerFacetComponent();

        /// <summary>
        /// International Class Facet Component
        /// </summary>
        public InternationalClassFacetComponent InternationalClassFacet => new InternationalClassFacetComponent();

        /// <summary>
        /// Special jurisdiction facet fo IP content
        /// </summary>
        public new IpJurisdictionFacetComponent JurisdictionFacet => new IpJurisdictionFacetComponent();

		/// <summary>
		/// Patent Type Facet
		/// </summary>
		public IpPatentTypeFacetComponent PatentTypeFacet => new IpPatentTypeFacetComponent();

        /// <summary>
        /// Tsdr Status Facet
        /// </summary>
        public TsdrStatusFacetComponent TsdrStatusFacet => new TsdrStatusFacetComponent();

        /// <summary>
        /// Vienna Code Facet
        /// </summary>
        public ViennaCodeFacetComponent ViennaCodeFacet => new ViennaCodeFacetComponent();
    }
}
