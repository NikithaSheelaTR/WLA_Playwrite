namespace Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacetusing;
    using Framework.Common.UI.Products.Shared.Dialogs.AdvancedSearch;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items.Facets;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.WestLawNext.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Component representing the select content/search all content widget/bar that appears at the left side
    /// Facets filtering
    /// Additional Components - References / Inheritors - all facets
    ///     - SearchWithinResults (new component / class) 
    ///     - Jurisdiction (new component / class) 
    ///     - Date (new component / class)
    ///     - DepthOfTreatment (new component / class)
    ///     - HeadnoteTopics (new component / class)
    ///     - Date (new component / class)
    ///     - TreatmentStatus (new component / class)
    ///     - ReportedStatus (new component / class)
    /// </summary>
    public class NarrowPaneComponent : BaseModuleRegressionComponent
    {
        private static readonly By FacetTitleLocator = By.XPath("//*[@id='co_searchWithinWidget_header'] | //*[@id='co_narrowResultsBy']//fieldset/legend/span | //*[@id='co_facetHeaderannotated']");

        private static readonly By SelectMultipleFiltersFirstContainerLocator = By.Id("co_multifacet_selector_1");

        private static readonly By SelectMultipleFiltersSecondContainerLocator = By.Id("co_multifacet_selector_2");

        private static readonly By NarrowFacetComponentLocator = By.CssSelector("#co_narrowResultsBy, #co_narrowRelatedResultsBy");

        private static readonly By ApplyFiltersTopButtonLocator = By.XPath("//button[starts-with(normalize-space(text()),'Apply')] | //a[starts-with(normalize-space(text()),'Apply')] | .//button//span[contains(text(),'Apply')]");

        private static readonly By CancelMultiFacetsButtonLocator = By.XPath("//button[starts-with(normalize-space(@class),'co_multifacet_cancel')] | //a[starts-with(normalize-space(text()),'Cancel')]");

        private static readonly By SelectMultipleFiltersButtonLocator = By.XPath("//button[starts-with(normalize-space(text()),'Select Multiple Filters')] | //a[starts-with(normalize-space(text()),'Select Multiple Filters')]");

        private static readonly By UndoFiltersLinklocator = By.CssSelector("#co_undoAllSelections>a .co_buttonUndo:not([disabled]), .co_btnBack:not([disabled]), .co_btnGray:not([disabled])");

        private static readonly By FilterLabelLocator = By.XPath("//*[@id='cobalt_ro_history_facets_header']/span/strong");

        private static readonly By DateButtonLocator = By.XPath("//button[@class='a11yDateWidget-button co_defaultBtn'] | //div[@id='co_search_advancedSearch_inner']//li[@id='co_search_advancedSearch_listItem_cb_DATE']//button[@class='a11yDateWidget-button co_defaultBtn']");

        private EnumPropertyMapper<Facet, WebElementInfo> facetMap;

        /// <summary>
        /// Annotated Documents Facet on Search result page
        /// </summary>
        public AnnotatedDocumentsFacetComponent AnnotatedDocumentsFacet { get; } = new AnnotatedDocumentsFacetComponent();

        /// <summary>
        /// Attorney Facet component
        /// </summary>
        public AttorneyFacetComponent AttorneyFacet { get; } = new AttorneyFacetComponent();

        /// <summary>
        /// AwardFacetComponent
        /// </summary>
        public AwardFacetComponent AwardFacet { get; } = new AwardFacetComponent();

        /// <summary>
        /// CourtLevelFacetComponent
        /// </summary>
        public CourtLevelFacetComponent CourtLevelFacet { get; } = new CourtLevelFacetComponent();

        /// <summary>
        /// Date Facet Component
        /// </summary>
        public DateFacetComponent DateFacet { get; } = new DateFacetComponent();

        /// <summary>
        /// Party Facet Component
        /// </summary>
        public PartyFacetComponent PartyFacet { get; } = new PartyFacetComponent();

        /// <summary>
        /// ContentTypeFacetComponent
        /// </summary>
        public ContentTypeFacetComponent ContentTypeFacet { get; } = new ContentTypeFacetComponent();

        /// <summary>
        /// Docket Facet Component
        /// </summary>
        public DocketNumberFacetComponent DocketFacet { get; } = new DocketNumberFacetComponent();

        /// <summary>
        /// DocumentTypeFacetoptionComponent
        /// </summary>
        public DocumentTypeFacetOptionComponent DocumentTypeFacetOption { get; } = new DocumentTypeFacetOptionComponent();

        /// <summary>
        /// DocumentTypeFacetComponent
        /// </summary>
        public DocumentTypeFacetComponent DocumentTypeFacet { get; } = new DocumentTypeFacetComponent();

        /// <summary>
        /// DocumentsInFoldersFacetComponent
        /// </summary>
        public DocumentsInFoldersFacetComponent DocumentsInFoldersFacet { get; } = new DocumentsInFoldersFacetComponent();

        /// <summary>
        /// ExpertNameFacetComponent
        /// </summary>
        public ExpertNameFacetComponent ExpertNameFacet { get; } = new ExpertNameFacetComponent();

        /// <summary>
        /// Depth Of Treatment Facet component
        /// </summary>
        public DepthOfTreatmentFacetComponent DepthOfTreatmentFacet { get; } = new DepthOfTreatmentFacetComponent();

        /// <summary>
        /// Directly Cited Facet
        /// </summary>
        public DirectlyCitedFacetComponent DirectlyCitedFacet { get; } = new DirectlyCitedFacetComponent();

        /// <summary>
        /// Formerly Cited Status Facet component
        /// </summary>
        public FormerlyCitedStatusFacetComponent FormerlyCitedStatusFacet { get; } = new FormerlyCitedStatusFacetComponent();

        /// <summary>
        /// FormTypeFacetComponent
        /// </summary>
        public FormTypeFacetComponent FormTypeFacet { get; } = new FormTypeFacetComponent();

        /// <summary>
        /// FavoritesFacetComponent
        /// </summary>
        public FavoritesFacetComponent FavoritesFacet { get; } = new FavoritesFacetComponent();

        /// <summary>
        /// Headnote Topics Facet Component
        /// </summary>
        public HeadnoteTopicsFacetComponent HeadnoteTopicsFacet { get; } = new HeadnoteTopicsFacetComponent();

        /// <summary>
        /// Judge Facet Component
        /// </summary>
        public JudgeFacetComponent JudgeFacet { get; } = new JudgeFacetComponent();

        /// <summary>
        /// Jurisdiction Facet component
        /// </summary>
        public JurisdictionFacetComponent JurisdictionFacet { get; } = new JurisdictionFacetComponent();

        /// <summary>
        /// Law Firm Facet Component
        /// </summary>
        public LawFirmFacetComponent LawFirmFacet { get; } = new LawFirmFacetComponent();

        /// <summary>
        /// ReferencesInNotesOfDecisionsFacet
        /// </summary>
        public ReferencesInNotesOfDecisionsFacetComponent ReferencesInNotesOfDecisionsFacet { get; } = new ReferencesInNotesOfDecisionsFacetComponent();

        /// <summary>
        /// Notes Of Decisions Topics Facet Component
        /// </summary>
        public NotesOfDecisionsTopicsFacetComponent NotesOfDecisionsTopicsFacet { get; } = new NotesOfDecisionsTopicsFacetComponent();

        /// <summary>
        /// Official Headnote Facet Component
        /// </summary>
        public OfficialHeadnoteFacetComponent OfficialHeadnoteFacet { get; } = new OfficialHeadnoteFacetComponent();

        /// <summary>
        /// Publication Name Facet Component
        /// </summary>
        public PublicationNameFacetComponent PublicationNameFacet { get; } = new PublicationNameFacetComponent();

        /// <summary>
        /// Reported Status Facet Component
        /// </summary>
        public ReportedStatusFacetComponent ReportedStatusFacet { get; } = new ReportedStatusFacetComponent();

        /// <summary>
        /// Search Within Facet Component
        /// </summary>
        public SearchWithinFacetComponent SearchWithinFacet { get; } = new SearchWithinFacetComponent();

        /// <summary>
        /// Subsections Facet Component
        /// </summary>
        public SubsectionsFacetComponent SubsectionsFacet { get; } = new SubsectionsFacetComponent();

        /// <summary>
        /// StatusFacetComponent
        /// </summary>
        public StatusFacetComponent StatusFacet { get; } = new StatusFacetComponent();

        /// <summary>
        /// StatuteTitleFacetComponent
        /// </summary>
        public StatuteTitleFacetComponent StatuteTitleFacet { get; } = new StatuteTitleFacetComponent();

        /// <summary>
        /// Subsections Facet Component
        /// </summary>
        public KeyNumberFacetComponent KeyNumberFacet { get; } = new KeyNumberFacetComponent();

        /// <summary>
        /// TopicFacetComponent
        /// </summary>
        public TopicFacetComponent TopicFacet { get; } = new TopicFacetComponent();

        /// <summary>
        /// TypeFacetComponent
        /// </summary>
        public TypeFacetComponent TypeFacet { get; } = new TypeFacetComponent();

        /// <summary>
        /// PublicationSeriesFacetComponent
        /// </summary>
        public PublicationSeriesFacetComponent PublicationSeriesFacet { get; } = new PublicationSeriesFacetComponent();

        /// <summary>
        /// PublicationTypeFacetComponent
        /// </summary>
        public PublicationTypeFacetComponent PublicationTypeFacet { get; } = new PublicationTypeFacetComponent();

        /// <summary>
        /// Treatment Status Facet Component
        /// </summary>
        public TreatmentStatusFacetComponent TreatmentStatusFacet { get; } = new TreatmentStatusFacetComponent();

        /// <summary>
        /// Viewed Facet Component
        /// </summary>
        public ViewedFacetComponent ViewedFacet { get; } = new ViewedFacetComponent();

        /// <summary>
        /// Issuer Location Of Incorporation Facet Component
        /// </summary>
        public IssuerLocationOfIncorporationFacetComponent IssuerLocationFacet { get; } = new IssuerLocationOfIncorporationFacetComponent();

        /// <summary>
        /// Search Other Sources Links Component
        /// Placed under search facets on the left side
        /// </summary>
        public SearchOtherSourcesFacetComponent SearchOtherSourcesFacet { get; set; } = new SearchOtherSourcesFacetComponent();

        /// <summary>
        /// SubscriptionFacetComponent
        /// </summary>
        public SubscriptionFacetComponent SubscriptionFacet { get; set; } = new SubscriptionFacetComponent();

        /// <summary>
        /// SearchTermsFacetComponent
        /// </summary>
        public SearchTermsFacetComponent SearchTermsFacet { get; set; } = new SearchTermsFacetComponent();

        /// <summary>
        /// DocumentTypeFacetComponent
        /// </summary>
        public DocumentTypeFacetOptionComponent DocumentTypeOptionFacet { get; } = new DocumentTypeFacetOptionComponent();

        /// <summary>
        /// DateDialogue
        /// </summary>
        public DateDialogue DateOptionDialog => new DateDialogue();

        /// <summary>
        ///  Filter label on the filter panel on current history page
        /// </summary>
        public ILabel FilterLabel => new Label(FilterLabelLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => NarrowFacetComponentLocator;

        /// <summary>
        /// Gets the Facet enumeration to BaseTextModel map.
        /// </summary>
        protected EnumPropertyMapper<Facet, WebElementInfo> FacetMap
            => this.facetMap = this.facetMap ?? EnumPropertyModelCache.GetMap<Facet, WebElementInfo>();

        /// <summary>
        /// Gets a list of all of the facets present on the search results page
        /// </summary>
        /// <returns>a list of all of the currently displayed facets</returns>
        public List<Facet> GetFacetsList()
            => DriverExtensions.GetElements(FacetTitleLocator).ToList().Select(e => e.Text).ToList()
                .Select(facet => this.FacetMap.ToList().FirstOrDefault(elem => facet.GetSubstringBeforeSpecialCharacter('\r').Equals(elem.Value.Text)).Key).ToList();

        /// <summary>
        /// Apply Filters Button
        /// </summary>
        public IButton ApplyFiltersButton => new Button(ApplyFiltersTopButtonLocator);

        /// <summary>
        ///  Date Button 
        /// </summary>
        public IButton DateButton => new Button(DateButtonLocator);

        /// <summary>
        /// Click apply filter button - return new page
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> The New Page Object </returns>
        public T ClickApplyFiltersButton<T>() where T : ICreatablePageObject
        {
            IWebElement button = DriverExtensions.WaitForElementDisplayed(ApplyFiltersTopButtonLocator);
            button.ScrollToElement();
            button.Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on the Undo Filters button/link to remove applied facets
        /// </summary>
        /// <typeparam name="T"> The type of the resulting search results page </typeparam>
        /// <returns> The resulting search results page </returns>
        public T ClickUndoFiltersButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(UndoFiltersLinklocator).CustomClick();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Verify that First Select Multiple Filters button is displayed
        /// </summary>
        /// <returns> True if first select multiple filters button is displayed, false otherwise </returns>
        public bool IsFirstSelectMultipleFiltersButtonDisplayed()
            => DriverExtensions.IsDisplayed(SelectMultipleFiltersFirstContainerLocator);

        /// <summary>
        /// Verify that Second Select Multiple Filters button is displayed
        /// </summary>
        /// <returns> True if second select multiple filters button is displayed, false otherwise </returns>
        public bool IsSecondSelectMultipleFiltersButtonDisplayed()
            => DriverExtensions.IsDisplayed(SelectMultipleFiltersSecondContainerLocator);

        /// todo remove iwebelement from the public method
        /// <summary>
        /// Get all the options in the dialog
        /// </summary>
        /// <param name="availableTopics"> The available Topics. </param>
        /// <returns> Dialog items list </returns>
        public List<DialogOptionItem> GetAllDialogOptions(IWebElement availableTopics)
        {
            availableTopics.WaitForElementDisplayed();
            return availableTopics.FindElements(By.XPath(".//li")).Select(elem => new DialogOptionItem(elem)).ToList();
        }

        /// <summary>
        /// Get Title Of Apply Filters Button
        /// </summary>
        /// <returns> Title Of Apply Filters Button </returns>
        public string GetTitleOfApplyFiltersButton()
            => DriverExtensions.WaitForElement(ApplyFiltersTopButtonLocator).GetAttribute("title");

        /// <summary>
        /// Is Apply Filters Button Enabled
        /// </summary>
        /// <returns> True if Apply Filters Button is enabled, false otherwise </returns>
        public bool IsApplyFiltersButtonDisplayed() => DriverExtensions.IsDisplayed(ApplyFiltersTopButtonLocator);

        /// <summary>
        /// Verify that undo filters button is displayed.
        /// </summary>
        /// <returns> True if the button is displayed, false otherwise </returns>
        public bool IsUndoFiltersButtonDisplayed() => DriverExtensions.IsDisplayed(UndoFiltersLinklocator);

        /// <summary>
        /// Is Cancel Button Displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCancelButtonDisplayed() => DriverExtensions.IsDisplayed(CancelMultiFacetsButtonLocator);

        /// <summary>
        /// Is Select Multiple Filters Button Displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsSelectMultipleFiltersButtonDisplayed()
            => DriverExtensions.IsDisplayed(SelectMultipleFiltersButtonLocator);

        /// <summary>
        /// Apply Multiple Filters Mode
        /// </summary>
        public void ApplyMultipleFiltersMode()
        {
            if (this.IsSelectMultipleFiltersButtonDisplayed())
            {
                DriverExtensions.WaitForElement(SelectMultipleFiltersButtonLocator).Click();
            }

            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Cancel Multiple Filters Mode
        /// </summary>
        public void CancelMultipleFiltersMode()
        {
            if (this.IsCancelButtonDisplayed())
            {
                DriverExtensions.GetElement(CancelMultiFacetsButtonLocator).Click();
            }

            DriverExtensions.WaitForJavaScript();
        }
    }
}