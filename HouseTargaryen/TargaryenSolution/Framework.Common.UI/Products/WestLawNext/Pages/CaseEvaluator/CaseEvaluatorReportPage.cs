namespace Framework.Common.UI.Products.WestLawNext.Pages.CaseEvaluator
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.CaseEvaluatorTab;
    using Framework.Common.UI.Products.Shared.Components.Toolbar;
    using Framework.Common.UI.Products.Shared.Dialogs.Foldering;
    using Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator;
    using Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator.TableComponents;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// CaseEvaluatorReportPage
    /// </summary>
    public class CaseEvaluatorReportPage : CommonAuthenticatedWestlawNextPage
    {
        private const string ExpectedPageTitle = "Case Evaluator Report";

        private static readonly By CaseElauatorContentContainerLocator = By.Id("co_caseEvaluator");

        private static readonly By CaseEvaluatorReportBuildetLinkLocator = By.LinkText("Case Evaluator Report Builder");

        private static readonly By EditCriteriaLinkLocator = By.XPath("//li[@id='co_docToolbarViewEditLinks']//a[text()='Edit Criteria']");

        private static readonly By PageTitleLocator = By.Id("co_docHeaderTitleLine");
        
        private static readonly By SaveToFolderButtonLocator = By.XPath("//li[@id='co_saveToContainer' or @id='co_docToolbarAddToFolder' or @id='co_docToolbarSaveToWidget']/div[@class='co_saveTo']/*[@class='co_dropdownTitle']");
       
        private static readonly By VisibleTableTitlesLocator = By.XPath("//div[@id='co_caseEvaluator']/div[not(contains(@class, 'ng-hide'))]//h3");

        private AppellateCourtDocumentsTableComponent appellateCourtDocumentsTable;

        private AwardsByCountyTableComponent awardsByCounty;

        private AwardsLargestTableComponent awardsByLargest;

        private AwardsByPartyTableComponent awardsByParty;

        private ExpertDistributionTableComponent expertDistribution;

        private ExpertsByExpertiseTableComponent expertByExpertise;

        private TrialCourtMemorandaTableComponent memoranda;

        private VerdictAndSettlementsTableComponent verdictAndSettlements;

        /// <summary>
        /// Initializes a new instance of the <see cref="CaseEvaluatorReportPage"/> class.
        /// </summary>
        public CaseEvaluatorReportPage()
        {
            DriverExtensions.WaitForElementDisplayed(CaseElauatorContentContainerLocator);
        }

        /// <summary>
        /// appellate court documents table
        /// </summary>
        public AppellateCourtDocumentsTableComponent AppellateCourtDocumentsTable
        {
            get
            {
                return this.appellateCourtDocumentsTable ?? new AppellateCourtDocumentsTableComponent();
            }

            protected set
            {
                this.appellateCourtDocumentsTable = value;
            }
        }

        /// <summary>
        /// the awards by county table
        /// </summary>
        public AwardsByCountyTableComponent AwardsByCountyTable
        {
            get
            {
                return this.awardsByCounty ?? new AwardsByCountyTableComponent();
            }

            protected set
            {
                this.awardsByCounty = value;
            }
        }

        /// <summary>
        /// the awards by largest table
        /// </summary>
        public AwardsLargestTableComponent AwardsByLargestTable
        {
            get
            {
                return this.awardsByLargest ?? new AwardsLargestTableComponent();
            }

            protected set
            {
                this.awardsByLargest = value;
            }
        }

        /// <summary>
        /// the awards by party table
        /// </summary>
        public AwardsByPartyTableComponent AwardsByPartyTable
        {
            get
            {
                return this.awardsByParty ?? new AwardsByPartyTableComponent();
            }

            protected set
            {
                this.awardsByParty = value;
            }
        }

        /// <summary>
        /// the expert distribution table
        /// </summary>
        public ExpertDistributionTableComponent ExpertDistributionTable
        {
            get
            {
                return this.expertDistribution ?? new ExpertDistributionTableComponent();
            }

            protected set
            {
                this.expertDistribution = value;
            }
        }

        /// <summary>
        /// the experts by expertise table
        /// </summary>
        public ExpertsByExpertiseTableComponent ExpertsByExpertiseTable
        {
            get
            {
                return this.expertByExpertise ?? new ExpertsByExpertiseTableComponent();
            }

            protected set
            {
                this.expertByExpertise = value;
            }
        }

        /// <summary>
        /// the filter date range component
        /// </summary>
        public FilterDateRangeComponent FilterDateRange { get; protected set; } = new FilterDateRangeComponent();

        /// <summary>
        /// the table of contents (tab object)
        /// </summary>
        public ReportPageTableOfContentsComponent TableOfContents { get; protected set; } = new ReportPageTableOfContentsComponent();

        /// <summary>
        /// Toolbar
        /// </summary>
        public Toolbar Toolbar { get; } = new Toolbar();

        /// <summary>
        /// trial court memoranda table
        /// </summary>
        public TrialCourtMemorandaTableComponent TrialCourtMemorandaTable
        {
            get
            {
                return this.memoranda ?? new TrialCourtMemorandaTableComponent();
            }

            protected set
            {
                this.memoranda = value;
            }
        }

        /// <summary>
        /// verdict and settlements table
        /// </summary>
        public VerdictAndSettlementsTableComponent VerdictAndSettlementsTable
        {
            get
            {
                return this.verdictAndSettlements ?? new VerdictAndSettlementsTableComponent();
            }

            protected set
            {
                this.verdictAndSettlements = value;
            }
        }


        /// <summary>
        /// component for view criteria dropdown
        /// </summary>
        public ViewCriteriaComponent ViewCriteria { get; protected set; } = new ViewCriteriaComponent();

        /// <summary>
        /// Clicking the link of Case Evaluator Report Build 
        /// </summary>
        /// <returns>The <see cref="CaseEvaluatorReportBuilderPage"/>.</returns>
        public T ClickCaseEvaluatorReportBuilder<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(CaseEvaluatorReportBuildetLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// click the edit criteria link in toolbar
        /// </summary>
        /// <returns>new input criteria page</returns>
        public T ClickEditCriteria<T>() where T: ICreatablePageObject
        {
            DriverExtensions.WaitForElement(EditCriteriaLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click save to folder
        /// </summary>
        /// <returns>The <see cref="SaveToFolderDialog"/>.</returns>
        public T ClickSaveToFolder<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForElement(SaveToFolderButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// gets list of all table names visible to user
        /// </summary>
        /// <returns>List tablesStrings</returns>
        public List<string> GetVisibleTablesTitleList()
            => DriverExtensions.GetElements(VisibleTableTitlesLocator).Select(el => el.Text).ToList();

        /// <summary>
        /// verifies is the current page is actually the report page
        /// </summary>
        /// <returns>bool page</returns>
        public bool IsReportPage() => DriverExtensions.IsTextInElement(PageTitleLocator, ExpectedPageTitle);
    }
}