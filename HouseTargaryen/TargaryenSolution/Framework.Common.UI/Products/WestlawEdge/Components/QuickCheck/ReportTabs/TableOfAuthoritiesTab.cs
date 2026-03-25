namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.ReportTabs
{
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces.Components;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults;
    using Framework.Common.UI.Products.WestlawEdge.Items;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// The table of authorities tab.
    /// </summary>
    public class TableOfAuthoritiesTab : BaseQuickCheckTabComponent
    {
        private static readonly By TabContainerLocator = By.ClassName("DA-TOAPage");
        private static readonly By ResultListLocator = By.XPath(".//*[@class='co_searchResultsList']");
        private static readonly By ResultItemLocator = By.XPath(".//*[@class='DA-TOACase']");           
        private static readonly By GroupContainerLocator = By.XPath("//div[./div[@class='DA-GroupedContentHeader']]");
        private static readonly By CitationIssuesMessageLocator = By.XPath(".//*[contains(@class, 'contentPotentialCitationErrorsHeader')]");
        private static readonly By CitationIssuesZeroMessageLocator = By.XPath(".//*[contains(@class, 'contentPotentialCitationErrorsContain')]/div");

        /// <summary>
        /// Potential Citation Errors Message Label
        /// </summary>
        public ILabel CitationIssuesMessageLabel = new Label(TabContainerLocator, CitationIssuesMessageLocator);

        /// <summary>
        /// Zero Citation Issues Message Label
        /// </summary>
        public ILabel CitationIssuesZeroMessageLabel = new Label(TabContainerLocator, CitationIssuesZeroMessageLocator);

        /// <summary>
        /// The result list.
        /// </summary>
        public QuickCheckItemsCollection<QuickCheckBaseItem> ResultList =>
            new QuickCheckItemsCollection<QuickCheckBaseItem>(new ByChained(this.ComponentLocator, ResultListLocator), ResultItemLocator, "div");

        /// <summary>
        /// Actual only for All cited authority content type
        /// </summary>
        public IItemsCollection<ToaGroupSectionComponent> GroupSections
            => new ItemsCollection<ToaGroupSectionComponent>(this.ComponentLocator, GroupContainerLocator);

        /// <summary>
        /// The narrow pane.
        /// </summary>
        public RecommendationsNarrowPaneComponent NarrowPane => new RecommendationsNarrowPaneComponent();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TabContainerLocator;
    }
}