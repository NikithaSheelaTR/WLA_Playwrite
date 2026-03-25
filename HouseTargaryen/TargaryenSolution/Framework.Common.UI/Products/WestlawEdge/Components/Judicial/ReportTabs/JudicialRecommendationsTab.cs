namespace Framework.Common.UI.Products.WestlawEdge.Components.Judicial.ReportTabs
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Components;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdge.Components.Judicial;
    using Framework.Common.UI.Products.WestlawEdge.Components.Judicial.DocumentResults;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.ReportTabs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Judicial recommendations tab
    /// </summary>
    public sealed class JudicialRecommendationsTab : BaseQuickCheckTabComponent
    {
        private static readonly By RecommendationsTabLocator = By.XPath("//div[contains(@class, 'DA-Party')]");
        private static readonly By DocumentSectionLocator = By.ClassName("DA-DocumentSection");
        private static readonly By OmittedByBothResultListLocator = By.XPath("//div[@class='co_searchResultsList']");
        private static readonly By OmittedByBothResultItemLocator = By.XPath("./ul/li");
        private static readonly By JurisdictionsInfoLabelLocator = By.XPath("//div[contains(@class, 'DA-jurisdictionInfo')]//*[@class='co_infoBox_message']");

        /// <summary>
        /// Selected Jurisdictions info box message label
        /// </summary>
        public ILabel JurisdictionsInfoLabel => new Label(JurisdictionsInfoLabelLocator);

        /// <summary>
        /// Gets the narrow pane.
        /// </summary>
        public RecommendationsNarrowPaneComponent NarrowPane { get; } = new RecommendationsNarrowPaneComponent();

        /// <summary>
        /// Gets uploaded document sections for party
        /// </summary>
        public ItemsCollection<JudicialRecommendationsResultsComponent> UploadedDocumentSections => new ItemsCollection<JudicialRecommendationsResultsComponent>(this.ComponentLocator,DocumentSectionLocator);
       
        /// <summary>
        /// Gets all headings for party
        /// </summary>
        public List<IssueItemContainerComponent> AllHeadings => this.UploadedDocumentSections.SelectMany(section => section.Headings).ToList();

        /// <summary>
        /// Gets all cases for party
        /// </summary>
        public List<RecommendationItem> AllCases => this.AllHeadings.SelectMany(heading => heading.Cases).ToList();

        /// <summary>
        /// Gets all secondary sources for party
        /// </summary>
        public List<QuickCheckBaseItem> AllSecondarySources => this.AllHeadings.SelectMany(heading => heading.SecondarySources).ToList();

        /// <summary>
        /// Gets all briefs and memoranda for party
        /// </summary>
        public List<QuickCheckBaseItem> AllBriefsAndMemoranda => this.AllHeadings.SelectMany(heading => heading.BriefsAndMemoranda).ToList();

        /// <summary>
        /// Gets all recommendations for party
        /// </summary>
        public List<RecommendationItem> AllRecommendations => this.AllHeadings.SelectMany(heading => heading.AllRecommendations).ToList();

        /// <summary>
        /// The omitted by both cases.
        /// </summary>
        public IItemsCollection<RecommendationItem> OmittedByBothCases =>
            new ItemsCollection<RecommendationItem>(OmittedByBothResultListLocator, OmittedByBothResultItemLocator);

        /// <summary>
        /// Party switcher component
        /// </summary>
        public JudicialRecommendationsTabPartySwitcherComponent<JudicialRecommendationsTab> PartySwitcher =>
            new JudicialRecommendationsTabPartySwitcherComponent<JudicialRecommendationsTab>();

        /// <summary>
        /// component locator
        /// </summary>
        protected override By ComponentLocator => RecommendationsTabLocator;

        /// <summary>
        /// this tab name
        /// </summary>
        protected override string TabName => DriverExtensions.GetElement(this.ComponentLocator).Text;
    }
}
