namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacetusing;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;
    using OpenQA.Selenium;

    /// <summary>
    /// Indigo component with facets for search
    /// </summary>
    public class EdgeSearchFacetsFilterComponent : EdgeBaseFacetsFilterComponent
    {
        private static readonly By DocketNumberFacetLocator = By.CssSelector("#facet_div_docket");
        private static readonly By DocTypeFacetLocator = By.XPath("//div[@id='facet_div_testimonyType']");
        private static readonly By FavoritesFacetLocator = By.Id("facet_div_MetaDataFavoritesFacet");
        private static readonly By FilterByLabelLocator = By.Id("facet_div_athensOtherFilters");

        private static readonly By JurisdictionFacetLocator = By.CssSelector(
                "#facet_div_jurisdiction,#facet_div_Jurisdiction,#facet_div_AdminJurisdiction,#facet_div_trd_jurisdiction,#facet_div_MetaDataJurisdictionFacet, #facet_div_aunzFacetSetViewJurisdictionFacet,#facet_div_wlncJurisdiction,#facet_div_canadianjurisdiction,#facet_div_laRefLegislationJurisdictionSummarizationFacetConfig");
        private static readonly By LanguageFacetLocator = By.CssSelector("#facet_div_laRefLegislationLanguageSummarizationFacetConfig");
        private static readonly By LegislationTypeFacetLocator = By.CssSelector("#facet_div_laRefLegislationTypeSummarizationFacetConfig");

        private static readonly By HistoryEventFacetLocator = By.XPath("//*[@id='facet_div_Event']");

        private static readonly By LegalPractitionerFacetLocator = By.Id("facet_div_legalPractitioner");       
        private static readonly By PartyNameFacetLocator = By.CssSelector("#facet_div_trd_party, #facet_div_party");
        private static readonly By PopularFiltersLabelLocator = By.Id("facet_div_athensPopularFilters");
        private static readonly By PublicationNameFacetLocator = By.CssSelector(
            "#SearchFacetMultipleXBoxes-publicationHeader, #facet_div_aunzFacetViewSetPublicationNameFacet, #SearchFacetMultipleXBoxes-publicationNameHeader");

        private static readonly By RepealedStatusFacetLocator = By.CssSelector("#facet_div_repealedStatus");
        private static readonly By ReportedStatusFacetLocator =
            By.CssSelector("#facet_div_reported, #facet_div_trd_reported");

        private static readonly By SubscriptionFacetLocator = By.CssSelector("#facet_div_MetaDataInplanFacet");

        private static readonly By TopicFacetLocator = By.CssSelector(
            "#facet_div_MetaDataTopicFacet, #facet_div_MetaDataTopicFacet, #facet_div_topic, #co_facetHeadertrd_topic");

        private static readonly By LegislationTitleLocator = By.CssSelector(
            "#facet_div_Title-HeaderTitle, #co_facetHeaderTitle");

        private static readonly By AbridgmentClassificationFacetLocator = By.CssSelector("#facet_div_wlncMetaDocAbridgmentClassification");
        private static readonly By SubjectAreaFacetLocator = By.CssSelector("#facet_div_subjectArea");
        private static readonly By USClassNumberFacetLocator = By.XPath("//div[@id = 'facet_div_usClassNumber']");

        /// <summary>
        /// Popular filters label
        /// </summary>
        public ILabel PopularFiltersLabel => new Label(PopularFiltersLabelLocator);

        /// <summary>
        /// Filter by label
        /// </summary>
        public ILabel FilterByLabel => new Label(FilterByLabelLocator);

        /// <summary>
        /// Award Range Facet
        /// </summary>
        public AwardRangeFacetComponent AwardRangeFacet { get; } = new AwardRangeFacetComponent();

        /// <summary>
        /// Date Facet
        /// </summary>
        public DateFacetComponent DateFacet { get; } = new DateFacetComponent();

        /// <summary>
        /// Point in Time Facet
        /// </summary>
        public PointInTimeDateFacetComponent PointInTimeFacet => new PointInTimeDateFacetComponent();

        /// <summary>
        /// Docket Number Facet
        /// </summary>
        public BaseEditableOptionFacetComponent DocketNumberFacet => new BaseEditableOptionFacetComponent(DocketNumberFacetLocator);

        /// <summary>
        /// Document Type Facet Component
        /// </summary>
        public BaseSearchHierarchyFacetComponent DocumentTypeFacet =>
            new BaseSearchHierarchyFacetComponent(DocTypeFacetLocator);

        /// <summary>
        /// History Event Facet Component
        /// </summary>
        public BaseSearchHierarchyFacetComponent HistoryEventFacet =>
            new BaseSearchHierarchyFacetComponent(HistoryEventFacetLocator);

        /// <summary>
        /// Form Type Facet
        /// </summary>
        public FormTypeFacetComponent FormTypeFacet { get; } = new FormTypeFacetComponent();

        /// <summary>
        /// Jurisdiction Facet
        /// </summary>
        public BaseSearchHierarchyFacetComponent JurisdictionFacet =>
            new BaseSearchHierarchyFacetComponent(JurisdictionFacetLocator);

        /// <summary>
        /// Language Facet
        /// </summary>
        public BaseSearchHierarchyFacetComponent LanguageFacet =>
            new BaseSearchHierarchyFacetComponent(LanguageFacetLocator);

        /// <summary>
        /// Legislation type Facet
        /// </summary>
        public BaseSearchHierarchyFacetComponent LegislationTypeFacet =>
            new BaseSearchHierarchyFacetComponent(LegislationTypeFacetLocator);

        /// <summary>
        /// Topic facet component
        /// </summary>
        public BaseEditableOptionFacetComponent PartyNameFacet => new BaseEditableOptionFacetComponent(PartyNameFacetLocator);

        /// <summary>
        /// Publication Name Facet
        /// </summary>
        public PublicationNameFacetComponent PublicationNameFacet =>
            new PublicationNameFacetComponent(PublicationNameFacetLocator);

        /// <summary>
        /// Reported Status Facet
        /// </summary>
        public ReportedStatusFacetComponent ReportedStatusFacet =>
            new ReportedStatusFacetComponent(ReportedStatusFacetLocator);

        /// <summary>
        /// Search Other Sources Links Component
        /// Placed under search facets on the left side
        /// </summary>
        public SearchOtherSourcesFacetComponent SearchOtherSourcesFacet { get; set; } =
            new SearchOtherSourcesFacetComponent();

        /// <summary>
        /// Subscription Facet Component
        /// </summary>
        public BaseSearchHierarchyFacetComponent SubscriptionFacet =>
            new BaseSearchHierarchyFacetComponent(SubscriptionFacetLocator);

        /// <summary>
        /// Topic hierarchy facet component
        /// </summary>
        public BaseSearchHierarchyFacetComponent TopicHierarchyFacet =>
            new BaseSearchHierarchyFacetComponent(TopicFacetLocator);

        /// <summary>
        /// Recent Filters facet component
        /// </summary>
        public EdgeRecentFiltersFacetComponent RecentFiltersFacet { get; } = new EdgeRecentFiltersFacetComponent();

        /// <summary>
        /// Repealed Status facet Component
        /// </summary>
        public BaseSearchHierarchyFacetComponent RepealedStatusFacet =>
            new BaseSearchHierarchyFacetComponent(RepealedStatusFacetLocator);

        /// <summary>
        /// Legal Practitioner Facet
        /// </summary>
        public BaseEditableOptionFacetComponent LegalPractitionerFacet = new BaseEditableOptionFacetComponent(LegalPractitionerFacetLocator);

        /// <summary>
        /// FavoritesFacet
        /// </summary>
        public BaseSearchHierarchyFacetComponent FavoritesFacet => new BaseSearchHierarchyFacetComponent(FavoritesFacetLocator);

        /// <summary>
        /// Legislation Title Facet
        /// </summary>
        public LegislationTitleFacetComponent LegislationTitleFacet =>
            new LegislationTitleFacetComponent(LegislationTitleLocator);

        /// <summary>
        /// Abridgment Classification Facet
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent AbridgmentClassificationFacet =>
            new EdgeBaseFacetWithAppearingDialogComponent(AbridgmentClassificationFacetLocator);

        /// <summary>
        /// Subject Area Facet
        /// </summary>
        public BaseSearchHierarchyFacetComponent SubjectAreaFacet =>
            new BaseSearchHierarchyFacetComponent(SubjectAreaFacetLocator);

        /// <summary>
        /// U.S. Class Number Facet 
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent USClassNumberFacet =>
            new EdgeBaseFacetWithAppearingDialogComponent(USClassNumberFacetLocator);


        /// <summary>
        /// Source Tile Facet Component
        /// </summary>
        public SourceTileFacetComponent SourceTileFacet { get; } = new SourceTileFacetComponent();
    }
}