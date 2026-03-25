namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// Sources Tab
    /// </summary>
    public class SourcesTab : BaseBrowseTabPanelComponent
    {
        private static readonly By TabContainerLocator = By.XPath("//div[@data-testid='report-answer-panel-sourcestab']");
        private static readonly By SourcesResultItemsLocator = By.XPath(".//li[contains(@data-testid, 'cited-source-item')]");
        private static readonly By PLResourceSummaryLocator = By.XPath(".//h3[@data-testid='additional-sources-heading']/following-sibling::div//h5[contains(@class, 'CitationCard-module__citationCardTitle')]");

        /// <summary>
        /// Sources result items list
        /// </summary>
        /// <returns>List of Sources result items</returns>
        public ItemsCollection<SourcesResultItem> SourcesResultItems => new ItemsCollection<SourcesResultItem>(this.ComponentLocator, SourcesResultItemsLocator);

        /// <summary>
        /// PL resource summary labels
        /// </summary>
        /// <returns>List of PL resource summary labels</returns>
        public IReadOnlyCollection<ILabel> PLResourceSummaryLabels => new ElementsCollection<Label>(this.ComponentLocator, PLResourceSummaryLocator);

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Sources";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TabContainerLocator;
    }
}


