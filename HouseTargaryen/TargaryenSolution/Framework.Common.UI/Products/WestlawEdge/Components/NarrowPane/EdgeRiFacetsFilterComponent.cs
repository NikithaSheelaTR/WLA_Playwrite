namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane
{
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;
    using OpenQA.Selenium;

    /// <summary>
    /// Indigo component with facets for Related Info
    /// </summary>
    public class EdgeRiFacetsFilterComponent : EdgeBaseFacetsFilterComponent
    {
        private static readonly By ReportedStatusFacetLocator = By.XPath("//div[@id='facet_div_reportedstatus']");
        private static readonly By SubsectionFacetContainerLocator = By.XPath("//div[@class = 'co_divider co_entry_facet' and .//*[@id='SearchFacetHierarchy-SubsectionHeader']]");
        private static readonly By TreatmentTypeFacetLocator = By.XPath("//section[contains(@class, 'TreatmentType')]");
        private static readonly By TreatmentStatusFacetLocator = By.XPath("//div[@class = 'co_divider co_entry_facet' and .//*[@id='SearchFacetMultipleXBoxes-TreatmentStatusHeader']]");
        private static readonly By SubsectionOrClauseFacetLocator = By.XPath("//section[contains(@class, 'CanadianSubsection')]");
        private static readonly By PatentTypeFacetLocator = By.XPath("//section[contains(@class,'PatentType')]");
        private static readonly By FormerlyCitedStatusFacetLocator = By.XPath("//div[@class = 'co_divider co_entry_facet' and .//*[@id='SearchFacetMultipleXBoxes-FormerlyCitedStatusHeader']]");
        private static readonly By DocumentTypeFacetLocator = By.XPath("//div[@id='facet_div_experttestimonysubdoctype']");
        private static readonly By DirectlyCitedFacetLocator = By.XPath("//section[contains(@class,'DirectlyCited')]");
        private static readonly By AgencyAdministrativeMaterialsLocator = By.XPath("//section[contains(@class,'AgencyAdminMaterials')]");
        private static readonly By CourtLevelFacetLocator = By.XPath("//section[contains(@class,'CourtLevel')]");
        private static readonly By RiJurisdictionFacetLocator = By.XPath(" //div[starts-with(@id, 'facet_div') and contains(@id,'jurisdiction')]");
        private static readonly By RiAbridgmentClassificationFacetLocator = By.XPath("//div[@class = 'co_divider co_entry_facet' and .//*[contains(@id, 'AbridgmentTopicsHeader')]]");
        private static readonly By ReferencedInNodFacetLocator = By.XPath("//div[@class = 'co_divider co_entry_facet' and .//*[@id='SearchFacetMultipleXBoxes-HasNODHeader']]");
        private static readonly By PartyFacetLocator = By.XPath("//div[@id='facet_div_party']");
        private static readonly By DocketNumberFacetLocator = By.XPath("//div[@id='facet_div_docket']");
        private static readonly By KeyNatureOfSuitFacetLocator = By.XPath("//div[@id='facet_div_keynatureofsuit']");
        private static readonly By OfficialHeadnotesLocator = By.XPath("//section[contains(@class,'OfficialHeadnotes')]");
        private static readonly By PublicationNameFacetLocator = By.XPath("//div[@class = 'co_divider co_entry_facet' and .//*[@id='SearchFacetCollector-PublicationNameHeader']]");
        private static readonly By NotesOfDecisionsFacetLocator = By.XPath("//div[@class = 'co_divider co_entry_facet' and .//*[@id='SearchFacetCollector-NODTopicsHeader']]");
        private static readonly By HeadnoteFacetLocator = By.XPath("//*[@id='facet_div_westheadnotetopics']");

        /// <summary>
        /// Agency and Administrative Materials Facet
        /// </summary>
        public BaseSearchHierarchyFacetComponent AgencyAdministrativeMaterialsFacet =>
            new BaseSearchHierarchyFacetComponent(AgencyAdministrativeMaterialsLocator);

        /// <summary>
        /// Court Level Facet
        /// </summary>
        public BaseSearchHierarchyFacetComponent CourtLevelFacet =>
            new BaseSearchHierarchyFacetComponent(CourtLevelFacetLocator);

        /// <summary>
        /// Date facet for RI
        /// </summary>
        public DateFacetComponent DateFacet => new DateFacetComponent("Ri");

        /// <summary>
        /// Depth Of Treatment Facet component
        /// </summary>
        public RiDepthOfTreatmentFacetComponent DepthOfTreatmentFacet => new RiDepthOfTreatmentFacetComponent();

        /// <summary>
        /// Directly Cited Facet
        /// </summary>
        public BaseSearchHierarchyFacetComponent DirectlyCitedFacet =>
            new BaseSearchHierarchyFacetComponent(DirectlyCitedFacetLocator);

        /// <summary>
        /// Document Type Facet 
        /// </summary>
        public BaseSearchHierarchyFacetComponent DocumentTypeFacet =>
            new BaseSearchHierarchyFacetComponent(DocumentTypeFacetLocator);

        /// <summary>
        /// Docket Number Of Suit Facet 
        /// </summary>
        public BaseEditableOptionFacetComponent DocketNumberFacet => new BaseEditableOptionFacetComponent(DocketNumberFacetLocator);

        /// <summary>
        /// Directly Cited Facet
        /// </summary>
        public BaseSearchHierarchyFacetComponent FormerlyCitedStatusFacet =>
            new BaseSearchHierarchyFacetComponent(FormerlyCitedStatusFacetLocator);

        /// <summary>
        /// Jurisdiction Facet component
        /// </summary>
        public BaseSearchHierarchyFacetComponent JurisdictionFacet =>
            new BaseSearchHierarchyFacetComponent(RiJurisdictionFacetLocator);

        /// <summary>
        /// Abridgement Classification Facet component
        /// </summary>
        public BaseSearchHierarchyFacetComponent AbridgementClassificationFacet =>
            new BaseSearchHierarchyFacetComponent(RiAbridgmentClassificationFacetLocator);

        /// <summary>
        /// Headnote Topics Facet Component
        /// </summary>
        public RiHeadnoteTopicsFacetComponent HeadnoteTopicsFacet => new RiHeadnoteTopicsFacetComponent(HeadnoteFacetLocator);

        /// <summary>
        /// Key Nature Of Suit Facet 
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent KeyNatureOfSuitFacet => new EdgeBaseFacetWithAppearingDialogComponent(KeyNatureOfSuitFacetLocator);

        /// <summary>
        /// Publication Type Facet
        /// </summary>
        public RiNotesOfDecisionsTopicsFacetComponent NotesOfDecisionsFacet =>
            new RiNotesOfDecisionsTopicsFacetComponent(NotesOfDecisionsFacetLocator);

        /// <summary>
        /// Official HeadNotes Facet component
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent OfficialHeadnoteFacet => new EdgeBaseFacetWithAppearingDialogComponent(OfficialHeadnotesLocator);

        /// <summary>
        /// Party Facet 
        /// </summary>
        public BaseEditableOptionFacetComponent PartyFacet => new BaseEditableOptionFacetComponent(PartyFacetLocator);

        /// <summary>
        /// PatentType Facet component
        /// </summary>
        public BaseSearchHierarchyFacetComponent PatentTypeFacet =>
            new BaseSearchHierarchyFacetComponent(PatentTypeFacetLocator);

        /// <summary>
        /// Publication Type Facet
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent PublicationNameFacet => new EdgeBaseFacetWithAppearingDialogComponent(PublicationNameFacetLocator);

        /// <summary>
        /// Publication Type Facet
        /// </summary>
        public RiReferencedInNodFacetComponent ReferencedInNodFacet =>
            new RiReferencedInNodFacetComponent(ReferencedInNodFacetLocator);

        /// <summary>
        /// Reported Status Facet
        /// </summary>
        public BaseSearchHierarchyFacetComponent ReportedStatusFacet =>
            new BaseSearchHierarchyFacetComponent(ReportedStatusFacetLocator);

        /// <summary>
        /// Subsections Facet Component
        /// </summary>
        public BaseSearchHierarchyFacetComponent SubsectionsFacet =>
            new BaseSearchHierarchyFacetComponent(SubsectionFacetContainerLocator);

        /// <summary>
        /// Subsection Or Clause Facet 
        /// </summary>
        public BaseSearchHierarchyFacetComponent SubsectionOrClauseFacet =>
            new BaseSearchHierarchyFacetComponent(SubsectionOrClauseFacetLocator);

        /// <summary>
        /// Treatment Status Facet Component
        /// </summary>
        public BaseSearchHierarchyFacetComponent TreatmentStatusFacet =>
            new BaseSearchHierarchyFacetComponent(TreatmentStatusFacetLocator);

        /// <summary>
        /// Subsection Or Clause Facet 
        /// </summary>
        public BaseSearchHierarchyFacetComponent TreatmentTypeFacet =>
            new BaseSearchHierarchyFacetComponent(TreatmentTypeFacetLocator);
    }
}