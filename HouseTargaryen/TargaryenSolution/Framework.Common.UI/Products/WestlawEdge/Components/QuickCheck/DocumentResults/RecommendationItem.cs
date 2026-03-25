namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Toggles;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Document analyzer=>=>Recommendations page=>Recommendations tab=>Results pane to the right.
    /// The component to represent the issue item under the collapsible Heading component.
    /// </summary>
    public class RecommendationItem : QuickCheckBaseItem
    {
        // Purple color
        private const string ColorForSearchWithinTermString = "rgba(190, 190, 252, 1)";

        private static readonly By BadgeLocator = By.XPath(".//div[@class='DA-Badges']/div");

        private static readonly By RelevantPortionLocator = By.XPath(".//ul[@class='DA-RelevantPortion']//a");

        private static readonly By OutcomeLocator = By.XPath(".//div[@class='DA-Outcome']/div");

        private static readonly By ToggleStateLocator = By.XPath(".//ancestor::button");

        private static readonly By CitationComponentLocator = By.XPath(".//ul[@class='DA-RecommendationOverruling--links']//li");

        private static readonly By SynopsisExpanderLocator = By.XPath(".//div[@class='DA-Synopsis']//button");

        private static readonly By ExpandedSynopsisExpanderLocator = By.XPath(".//div[@class='DA-Synopsis']//span[contains(text(),'Hide synopsis')]");

        private static readonly By SynopsisTextLocator = By.XPath(".//div[contains(@class,'co_synopsis')]");

        private static readonly By SearchWithinTermLocator = By.XPath(".//span[@class='co_searchTerm co_keyword']");

        private static readonly By SnippetLocator = By.XPath(".//div[@class='DA-RecommendationRelatedText']/div");

        private static readonly By SnippetToggleLocator = By.XPath(".//div[@class='DA-RecommendationRelatedText']//*[contains(@class,'icon_downCaret-blue')]");

        private static readonly By SnippetLinkLocator = By.XPath(".//div[@class='DA-RecommendationRelatedText']//a");


        /// <summary>
        /// Initializes a new instance of the <see cref="RecommendationItem"/> class. 
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public RecommendationItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Recommendation Case Detail Component info
        /// </summary>
        public RecommendationCaseDetailComponent CaseDetails => new RecommendationCaseDetailComponent(this.Container);

        /// <summary>
        /// Related citations
        /// </summary>
        public ItemsCollection<RelatedCitationItem> RelatedCitations => new ItemsCollection<RelatedCitationItem>(this.Container, CitationComponentLocator);

        /// <summary>
        /// Related portion links
        /// </summary>
        public IReadOnlyCollection<ILink> RelevantPortionLinks => new ElementsCollection<Link>(this.Container, RelevantPortionLocator);

        /// <summary>
        /// Snippet links
        /// </summary>
        public IReadOnlyCollection<ILink> SnippetLinks => new ElementsCollection<Link>(this.Container, SnippetLinkLocator);
  
        /// <summary>
        /// Badge labels
        /// </summary>
        public IReadOnlyCollection<ILabel> BadgeLabels => new ElementsCollection<Label>(this.Container, BadgeLocator);

        /// <summary>
        /// Snippet toggle
        /// </summary>
        public IToggle SnippetToggle =>  new Toggle(this.Container, SnippetToggleLocator, ToggleStateLocator, "aria-expanded", "true");

        /// <summary>
        /// Synopsis label
        /// </summary>
        public ILabel SynopsisLabel => new Label(this.Container, SynopsisTextLocator);

        /// <summary>
        /// Outcome label
        /// </summary>
        public ILabel OutcomeLabel => new Label(this.Container, OutcomeLocator);

        /// <summary>
        /// Snippet label
        /// </summary>
        public ILabel SnippetLabel => new Label(this.Container, SnippetLocator);

        /// <summary>
        /// Synopsis expander button
        /// </summary>
        public IButton SynopsisExpanderButton => new Button(this.Container, SynopsisExpanderLocator);

        /// <summary>
        /// The search terms.
        /// </summary>
        public IEnumerable<string> SearchWithinTerms =>
            DriverExtensions.GetElements(this.Container, SearchWithinTermLocator).Any()
                ? DriverExtensions.GetElements(this.Container, SearchWithinTermLocator).Where(
                    elem => elem.GetCssValue("background-color")
                                .Equals(ColorForSearchWithinTermString)).Select(el => el.Text)
                : new List<string>();

        /// <summary>
        /// Expands/collapses the synopsis section.
        /// </summary>
        /// <param name="expand">Use true if synopsis needs to be expanded.</param>
        public void ToggleSynopsis(bool expand)
        {
            bool isSynopsisExpanded = DriverExtensions.IsElementPresent(
                this.Container,
                ExpandedSynopsisExpanderLocator,
                3000);
            if (isSynopsisExpanded != expand)
            {
                DriverExtensions.GetElement(this.Container, SynopsisExpanderLocator).Click();
            }
        }            
    }
}