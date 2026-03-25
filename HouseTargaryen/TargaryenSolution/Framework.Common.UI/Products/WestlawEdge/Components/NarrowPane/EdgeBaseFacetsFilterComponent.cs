namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// Indigo base component for facets
    /// </summary>
    public class EdgeBaseFacetsFilterComponent : BaseModuleRegressionComponent
    {
        private const string ButtonSearchFacetLctMask = "//span[@class='SearchFacet-buttonText'and contains(text(),{0})]";
        private const string AppliedFacetLctMask = "//div[contains(@class,'co_divider co_entry_facet SearchFacet')]//span[@class='SearchFacet-buttonText'and contains(text(),{0})]";
        private const string PricingRangeButtonTextLctMask = "//div[@id='facet_div_{0}']//span[@class='SearchFacet-buttonText']";
        private const string SelectButtonLctMask = "//button[contains(@class, 'Search') and ./span[contains(text(), {0})]]";

        private static readonly By FacetTitleLocator = By.XPath("//button[@class='SearchFacet-buttonToggle']//span[@class = 'SearchFacet-buttonText']");

        private static readonly By SelectMultipleFiltersToggleLocator = By.XPath("//div[@class='SlideToggle-thumb-container']");

        private static readonly By SearchWithinFacetLoctor = By.Id("facet_div_keyword");
        private static readonly By ApplyFiltersButtonLocator = By.XPath("//button[@class='co_multifacet_apply']");
        private static readonly By ClearButtonLocator = By.CssSelector("#co_undoAllSelections>a .co_buttonUndo, .co_btnBack:not([disabled]),.co_btnGray:not([disabled]), .SearchFacet-buttonUndo");        
        private static readonly By FilterComponentLocator = By.CssSelector("#co_narrowResultsBy, #co_narrowRelatedResultsBy");
        private static readonly By AreasOfLawFacetLocator = By.CssSelector("#facet_div_areasOfLaw");
        private static readonly By AttorneyFacetLocator = By.CssSelector("#facet_div_attorney, #facet_div_trd_attorney");
        private static readonly By JudgeFacetLocator = By.CssSelector("#facet_div_judge, #facet_div_trd_judge");
        private static readonly By AuthorFacetLocator = By.CssSelector("#facet_div_Author, #facet_div_wlncSubSearchAuthor");
        private static readonly By LawFirmFacetLocator = By.CssSelector("#facet_div_lawfirm, #facet_div_trd_lawfirm");
        private static readonly By AnnotatedFacetLocator = By.XPath("//div[@id = 'facet_div_annotated' or @id ='facet_div_QW5ub3RhdGVkIERvY3VtZW50cw_e_e' or @id ='facet_div_annotateddocuments']");
        private static readonly By ContentTypeFacetLocator = By.CssSelector("#facet_div_Q29udGVudCBUeXBlcw_e_e");
        private static readonly By ContentTypeHierarchyFacetLocator = By.XPath("//div[contains(@id, 'facet_div_') and .//span[.='Content Type']]");
        private static readonly By StatuteTitleFacetLocator = By.CssSelector("#facet_div_title");
        private static readonly By PracticeAreaFacetLocator = By.XPath("//*[@id = 'facet_div_trd_practicearea'] | //*[@id = 'facet_div_topic'] | //*[@id = 'facet_div_practiceArea']");
        private static readonly By PublicationSeriesFacetLocator = By.CssSelector("#facet_div_MetaDataBrandFacet");
        private static readonly By PublicationTypeFacetLocator = By.CssSelector("#facet_div_MetaDataPublicationTypeFacet, #facet_div_publicationType");
        private static readonly By DocumentsInFoldersFacetLocator = By.XPath("//div[contains(@id, 'facet_div')][.//*[text() = 'Documents in Folders' or text()='Documents dans des dossiers']]");
        private static readonly By ContactFacetContainerLocator = By.XPath("//div[@id='facet_div_notificationCenterContact']");
        private static readonly By AppliedContactFacetContainerLocator = By.XPath("//div[@id='facet_div_notificationCenterContact' and contains(@class,'co_divider co_entry_facet')]");
        private static readonly By TypeFacetLocator = By.XPath("//div[@id='facet_div_Type']");
        private static readonly By ProceduralPostureFacetLocator = By.CssSelector("#facet_div_proceduralPosture");
        private static readonly By KeyNumberFacetContainerLocator = By.XPath("//div[@id = 'facet_div_trd_keynumber' or @id = 'facet_div_keynumber' or @id='facet_div_customDigestKeyNumber']");
        private static readonly By JurisdictionTitleFacetLocator = By.XPath("//div[@id = 'facet_div_wlncTreeLegisStatuteTitle']");
        private static readonly By IssuerLocationFacetContainerLocator = By.XPath("//div[@id='facet_div_agreementsIssuerLocationOfIncorporation']");     

        /// <summary>
        /// Annotated Documents Facet
        /// </summary>
        public AnnotatedDocumentsFacetComponent AnnotatedDocumentsFacet =>
            new AnnotatedDocumentsFacetComponent(AnnotatedFacetLocator);

        /// <summary>
        /// Areas of Law Facet
        /// </summary>
        public BaseSearchHierarchyFacetComponent AreasOfLawFacet =>
            new BaseSearchHierarchyFacetComponent(AreasOfLawFacetLocator);

        /// <summary>
        /// Attorney Facet
        /// </summary>
        public BaseLpaFacetComponent AttorneyFacet => new BaseLpaFacetComponent(AttorneyFacetLocator);

        /// <summary>
        /// The contacts facet on the Notifications center page.
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent AppliedContactsFacet =>
            new EdgeBaseFacetWithAppearingDialogComponent(AppliedContactFacetContainerLocator);

        /// <summary>
        /// The contacts facet on the Notifications center page.
        /// </summary>
        public BaseSearchHierarchyFacetComponent ContactsFacet =>
            new BaseSearchHierarchyFacetComponent(ContactFacetContainerLocator);

        /// <summary>
        /// Content Type Hierarchy Facet Component
        /// </summary>
        public BaseSearchHierarchyFacetComponent ContentTypeFacet =>
            new BaseSearchHierarchyFacetComponent(ContentTypeHierarchyFacetLocator);

        /// <summary>
        /// Documents In Folders Facet
        /// </summary>
        public BaseSearchHierarchyFacetComponent DocumentsInFoldersFacet =>
            new BaseSearchHierarchyFacetComponent(DocumentsInFoldersFacetLocator);

        /// <summary>
        /// Judge Facet
        /// </summary>
        public BaseLpaFacetComponent JudgeFacet => new BaseLpaFacetComponent(JudgeFacetLocator);

        /// <summary>
        /// Author Facet
        /// </summary>
        public BaseEditableOptionFacetComponent AuthorFacet => new BaseEditableOptionFacetComponent(AuthorFacetLocator);

        /// <summary>
        /// Content Type Facet
        /// </summary>
        public EdgeContentTypeFacetComponent EdgeContentTypeFacet =>
            new EdgeContentTypeFacetComponent(ContentTypeFacetLocator);

        /// <summary>
        /// Issuer Location Of Incorporation Facet
        /// </summary>
        public BaseSearchHierarchyFacetComponent IssuerLocationFacet =>
            new BaseSearchHierarchyFacetComponent(IssuerLocationFacetContainerLocator);

        /// <summary>
        /// Jurisdiction/Title Facet (Facet with dialog)
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent JurisdictionTitleFacet => new EdgeBaseFacetWithAppearingDialogComponent(JurisdictionTitleFacetLocator, "FacetsWithDialogs");

        /// <summary>
        /// Key Number Facet
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent KeyNumberFacet => new EdgeBaseFacetWithAppearingDialogComponent(KeyNumberFacetContainerLocator, "FacetsWithDialogs");

        /// <summary>
        /// Law Firm Facet
        /// </summary>
        public BaseLpaFacetComponent LawFirmFacet => new BaseLpaFacetComponent(LawFirmFacetLocator);

        /// <summary>
        /// The notification type facet.
        /// </summary>
        public NotificationTypeFacetComponent NotificationTypeFacet => new NotificationTypeFacetComponent();

        /// <summary>
        /// Practice Area Facet
        /// </summary>
        public BaseSearchHierarchyFacetComponent PracticeAreaFacetComponent => new BaseSearchHierarchyFacetComponent(PracticeAreaFacetLocator);

        /// <summary>
        /// Procedural Posture Facet Component
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent ProceduralPostureFacet => new EdgeBaseFacetWithAppearingDialogComponent(ProceduralPostureFacetLocator, "FacetsWithDialogs");

        /// <summary>
        /// Publication Type Facet
        /// </summary>
        public BaseSearchHierarchyFacetComponent PublicationTypeFacet =>
            new BaseSearchHierarchyFacetComponent(PublicationTypeFacetLocator);

        /// <summary>
        /// Publication Series Facet
        /// </summary>
        public BaseSearchHierarchyFacetComponent PublicationSeriesFacet =>
            new BaseSearchHierarchyFacetComponent(PublicationSeriesFacetLocator);

        /// <summary>
        /// Search Within Facet
        /// </summary>
        public EdgeSearchWithinFacetComponent SearchWithinFacet => new EdgeSearchWithinFacetComponent();

        /// <summary>
        /// Statute Title Facet
        /// </summary>
        public BaseSearchHierarchyFacetComponent StatuteTitleFacet =>
            new BaseSearchHierarchyFacetComponent(StatuteTitleFacetLocator);

        /// <summary>
        /// Type Facet Component
        /// </summary>
        public BaseSearchHierarchyFacetComponent TypeFacet => new BaseSearchHierarchyFacetComponent(TypeFacetLocator);

        /// <summary>
        /// Previously Viewed Facet
        /// </summary>
        public PreviouslyViewedFacetComponent PreviouslyViewedFacet { get; } = new PreviouslyViewedFacetComponent();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => FilterComponentLocator;

        /// <summary>
        /// Cancel multiple filters mode, if button displayed
        /// </summary>
        public void CancelMultipleFiltersModeIfDisplayed() => this.ApplySelectMultipleFilters(false);

        /// <summary>
        /// Click Apply Filters Button
        /// </summary>
        /// <typeparam name="T"> T </typeparam>
        /// <returns> T page </returns>
        public T ClickApplyFiltersButton<T>()
            where T : ICreatablePageObject
        {
            if (IsApplyButtonDisplayed())
            {
                DriverExtensions.Click(DriverExtensions.WaitForElement(ApplyFiltersButtonLocator));
            }

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks clear button.
        /// </summary>
        /// <typeparam name="T"> T page </typeparam>
        /// <returns> T page. </returns>
        public T ClickClearButton<T>()
            where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(ClearButtonLocator).CustomClick();
            DriverExtensions.WaitForAnimation();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Gets a list of all of the facets present on the search results page
        /// </summary>
        /// <returns>a list of all of the currently displayed facets</returns>
        public List<string> GetFacetsList() =>
            DriverExtensions.GetElements(FacetTitleLocator).Select(el => el.Text).ToList();

        /// <summary>
        /// Gets the title of the pricing range facet
        /// </summary>
        /// <param name="type">Search facet type</param>
        /// <returns>Title</returns>
        public string GetPricingRangeTitle(string type) => DriverExtensions
            .WaitForElement(By.XPath(string.Format(PricingRangeButtonTextLctMask, type))).GetText();

        /// <summary>
        /// Is Button Search Facet Displayed 
        /// (like Jurisdiction, Date, Reported Status, Practice Area, Judge, Attorney, Law firm, Party, Docket Number, Viewed in the last 30 days, Documents in Folders, Annotated Documents
        /// </summary>
        /// <param name="facetName"> The facet Name.
        /// </param>
        /// <returns>
        /// true if present
        /// </returns>
        public bool IsButtonSearchFacetDisplayed(string facetName) =>
            DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(ButtonSearchFacetLctMask, facetName));

        /// <summary>
        /// Verifies that the clear button is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the clear button is displayed. </returns>
        public bool IsClearButtonDisplayed() => DriverExtensions.IsDisplayed(ClearButtonLocator);

        /// <summary>
        /// Verifies that the apply button is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the apply button is displayed. </returns>
        public bool IsApplyButtonDisplayed() => DriverExtensions.IsDisplayed(ApplyFiltersButtonLocator);

        /// <summary>
        /// Verifies that facet relates to applied type.(Contacts facet, Key Number, Procedural Posture)
        /// </summary>
        /// <param name="facetName"> The facet name. </param>
        /// <returns> The <see cref="bool"/>. True if facet relates to applied type. </returns>
        public bool IsFacetAppliedType(string facetName) =>
            DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(AppliedFacetLctMask, facetName));

        /// <summary>
        /// Is Search Within Facet Displayed
        /// </summary>
        /// <returns>true if present</returns>
        public bool IsSearchWithinFacetDisplayed() => DriverExtensions.IsDisplayed(SearchWithinFacetLoctor);

        /// <summary>
        /// The select facet by name.
        /// </summary>
        /// <param name="facetName"> The facet name. </param>
        /// <typeparam name="T"> T </typeparam>
        /// <returns> T page </returns>
        public T SelectFacetByName<T>(string facetName)
            where T : ICreatablePageObject
        {
            By selectLinkLocator = SafeXpath.BySafeXpath(SelectButtonLctMask, facetName);
            DriverExtensions.WaitForElementDisplayed(selectLinkLocator);
            DriverExtensions.GetElement(selectLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// SelectMultipleToggle
        /// </summary>
        public bool IsSelectMultipleToggleDisplayed() => DriverExtensions.IsDisplayed(SelectMultipleFiltersToggleLocator);

        /// <summary>
        /// Apply filters
        /// </summary>
        public bool IsApplyFiltersButtonDisplayed() => DriverExtensions.IsDisplayed(ApplyFiltersButtonLocator, 5);

        /// <summary>
        /// Click SelectMultipleFiltersButton if it is displayed
        /// </summary>
        public void SelectMultipleFilters() => this.ApplySelectMultipleFilters(true);

        /// <summary>
        ///  Apply 'Select Multiple filters' if it is not selected and isSelected  = true 
        ///  Cancel 'Select Multiple filters' if it is  selected and isSelected  = false 
        /// </summary>
        /// <param name="isSelect">state</param>
        private void ApplySelectMultipleFilters(bool isSelect)
        {
            if (isSelect && !DriverExtensions.IsDisplayed(ApplyFiltersButtonLocator, 5)
                || !isSelect && DriverExtensions.IsDisplayed(ApplyFiltersButtonLocator, 5))
            {
                DriverExtensions.WaitForElement(SelectMultipleFiltersToggleLocator).Click();
                DriverExtensions.WaitForJavaScript();
            }
        }
           
        }
    }