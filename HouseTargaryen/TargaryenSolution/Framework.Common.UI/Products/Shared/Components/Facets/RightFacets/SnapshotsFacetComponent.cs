namespace Framework.Common.UI.Products.Shared.Components.Facets.RightFacets
{
    using Framework.Common.UI.Products.Shared.Enums.Snapshot;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The snapshots component.
    /// </summary>
    public class SnapshotsFacetComponent : BaseModuleRegressionComponent
    {
        private static readonly By AkaSectionLocator =
            By.XPath("//div[@class='co_snapshotOverviewItem' and ./strong[contains(text(),'AKA')]]");

        private static readonly By TopicSectionLocator =
            By.XPath("//div[contains(@class,'co_snapshotOverviewItem')]/strong[contains(text(),'Topic')]");

        private static readonly By AttorneySnapshotLocator =
            By.XPath("id('snapshotBox')//div[contains(@id,'co_attorneySnapshot_')]");

        private static readonly By StateSnapshotLocator =
            By.XPath("id('snapshotBox')//div[contains(@class,'co_snapshotJurisdictionItem')]");

        private static readonly By CitationSectionLocator =
            By.XPath("//div[contains(@class,'co_snapshotOverviewItem')]/strong[contains(text(),'Citation')]");

        private static readonly By CompanyIndicatorLocator = By.Id("cobalt_search_results_businessinvestigator1");

        private static readonly By CompanyNewsLocator = By.Id("co_companyNews");

        private static readonly By CompanySnapshotLocator =
            By.XPath("id('snapshotBox')//a[contains(@id,'co_companyName')]");

        private static readonly By CompanySummaryLinkLocator = By.XPath("//*[contains(./@id, 'co_companySummary_')]");

        private static readonly By CompanySummaryLocator = By.Id("companySummary");

        private static readonly By CompanyTickerLocator = By.Id("co_companyStockTicker");

        private static readonly By CompanyViewMoreInfoContainer =
            By.XPath("id('snapshotBox')//*[contains(@id, 'co_viewMoreInfoContainer_')]");

        private static readonly By ExpertNameLocator = By.XPath("id('snapshotBox')//a[contains(@id,'co_personName')]");

        private static readonly By ExpertSnapshotLocator =
            By.XPath("id('snapshotBox')//div[contains(@id,'co_expertSnapshot_')]");

        private static readonly By FamousCaseSnapshotLocator =
            By.XPath("id('snapshotBox')//a[contains(@id,'co_famousCaseTitle')]");

        private static readonly By JudgeSnapshotLocator =
            By.XPath("id('snapshotBox')//div[contains(@id,'co_judgeSnapshot_')]");

        private static readonly By PersonReportContainer =
            By.XPath("id('snapshotBox')//*[contains(@id,'co_reportContainer_')]");

        private static readonly By PopularNameSnapshotlocator =
            By.CssSelector("div.co_snapshotOverviewItem h4 a#co_popularNameTitle");

        private static readonly By PublicLawComponentLocator = By.Id("popularNamePublicLaws");

        private static readonly By ViewMoreLinkLocator =
            By.XPath("id('snapshotBox')//div[contains(@class,'co_snapshotViewMoreToggle')]");

        private static readonly By ViewReportsLinkLocator =
            By.XPath("id('snapshotBox')//a[contains(@id,'co_reportLink')]");

        private static readonly By ContainerLocator = By.Id("snapshotBox");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click on the name
        /// </summary>
        /// <returns> The <see cref="ExpertProfilePage"/>. </returns>
        public T ClickName<T>() where T : Framework.Common.UI.Interfaces.ICreatablePageObject
        {
            DriverExtensions.Click(ExpertNameLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }
        
        /// <summary>
        /// Get expert name
        /// </summary>
        public string GetExpertName() => DriverExtensions.WaitForElement(ExpertNameLocator).Text;

        /// <summary>
        /// Verify that AKA is displayed
        /// </summary>
        /// <returns> True if AKA is displayed, false otherwise </returns>
        public bool IsAkaDisplayed() => DriverExtensions.IsDisplayed(AkaSectionLocator, 5);

        /// <summary>
        /// Returns true if CitingCases is displayed
        /// </summary>
        /// <returns> True if Citing Cases is displayed, false otherwise </returns>
        public bool IsCitingCasesDisplayed() => DriverExtensions.IsDisplayed(CitationSectionLocator, 5);

        /// <summary>
        /// Verify that Topic is displayed
        /// </summary>
        /// <returns> True if Topic is displayed, false otherwise </returns>
        public bool IsTopicDisplayed() => DriverExtensions.IsDisplayed(TopicSectionLocator, 5);

        /// <summary>
        /// Returns true if company news is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsCompanyNewsDisplayed() => DriverExtensions.IsDisplayed(CompanyNewsLocator, 5);

        /// <summary>
        /// Returns true if stock ticker is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsCompanyStockTickerDisplayed() => DriverExtensions.IsDisplayed(CompanyTickerLocator, 5);

        /// <summary>
        /// The is company summary displayed.
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsCompanySummaryDisplayed() => DriverExtensions.IsDisplayed(CompanySummaryLocator, 5);

        /// <summary>
        /// Returns true if Public Law is displayed
        /// </summary>
        /// <returns> True if public Law is displayed, false otherwise </returns>
        public bool IsPublicLawDisplayed() => DriverExtensions.IsDisplayed(PublicLawComponentLocator, 5);

        /// <summary>
        /// Returns true if doc count is displayed
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSnapshotAdditionalSecDisplayed(SnapshotType type) =>
            DriverExtensions.IsDisplayed(
                type == SnapshotType.Companies ? CompanyViewMoreInfoContainer : PersonReportContainer, 5);

        /// <summary>
        /// Returns true if the specified snapshot type is displayed
        /// </summary>
        /// <param name="type"> Snapshot type </param>
        /// <returns> True if snapshot displayed, false otherwise </returns>
        public bool IsSnapshotDisplayed(SnapshotType type)
        {
            bool isSnapshotDisplayed = false;
            switch (type)
            {
                case SnapshotType.Attorney:
                    isSnapshotDisplayed = DriverExtensions.IsDisplayed(AttorneySnapshotLocator, 5);
                    break;
                case SnapshotType.Judge:
                    isSnapshotDisplayed = DriverExtensions.IsDisplayed(JudgeSnapshotLocator, 5);
                    break;
                case SnapshotType.Experts:
                    isSnapshotDisplayed = DriverExtensions.IsDisplayed(ExpertSnapshotLocator, 5);
                    break;
                case SnapshotType.Companies:
                    isSnapshotDisplayed = DriverExtensions.IsDisplayed(CompanySnapshotLocator, 5);
                    break;
                case SnapshotType.FamousCases:
                    isSnapshotDisplayed = DriverExtensions.IsDisplayed(FamousCaseSnapshotLocator, 5);
                    break;
                case SnapshotType.LegalConcepts:
                    isSnapshotDisplayed = DriverExtensions.IsDisplayed(AttorneySnapshotLocator, 5);
                    break;
                case SnapshotType.PopularNames:
                    isSnapshotDisplayed = DriverExtensions.IsDisplayed(PopularNameSnapshotlocator, 5);
                    break;
                case SnapshotType.States:
                    isSnapshotDisplayed = DriverExtensions.IsDisplayed(StateSnapshotLocator, 5);
                    break;
            }

            return isSnapshotDisplayed;
        }

        /// <summary>
        /// The is view full summary link direct correctly.
        /// </summary>
        /// <returns> True if direction is correct, false otherwise </returns>
        public bool IsViewFullSummaryLinkDirectCorrectly()
        {
            DriverExtensions.WaitForElement(CompanySummaryLinkLocator).CustomClick();
            return DriverExtensions.IsDisplayed(CompanyIndicatorLocator, 60);
        }

        /// <summary>
        /// Click on the view more link in the snapshot section
        /// </summary>
        public void ViewMoreLinkClick()
        {
            DriverExtensions.WaitForElement(ViewMoreLinkLocator).CustomClick();
            DriverExtensions.WaitForTextInElement(5, "View less about this person", ViewMoreLinkLocator);
        }

        /// <summary>
        /// Click on the 'View Reports' link
        /// </summary>
        /// <returns> The <see cref="ExpertProfileReportsPage"/>. </returns>
        public ExpertProfileReportsPage ViewReportsLinkClick()
        {
            IWebElement viewReportsLink = DriverExtensions.WaitForElement(ViewReportsLinkLocator);
            viewReportsLink.WaitForElementEnabled();
            DriverExtensions.Click(viewReportsLink);
            return new ExpertProfileReportsPage();
        }
    }
}