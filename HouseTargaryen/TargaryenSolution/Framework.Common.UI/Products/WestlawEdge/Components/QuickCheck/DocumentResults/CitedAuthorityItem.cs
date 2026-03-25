namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestlawEdge.Components.Judicial;
    using Framework.Common.UI.Products.WestlawEdge.Elements.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Enums.QuickCheck;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The cited authority item.
    /// </summary>
    public class CitedAuthorityItem : QuickCheckBaseItem
    {
        private static readonly By WarningSnippetLocator = By.XPath(".//div[contains(@class,'DA-KCWarningSnippet ') and not(contains(@class,'Container'))]");

        private static readonly By BadgeLocator = By.XPath(".//div[@class='DA-Badges']/div");

        private static readonly By SeeAllHistoryLocator = By.XPath(".//*[contains(text(), 'See all history')]");

        private static readonly By CitedByComponentLocator = By.XPath(".//div[@class='DA-CitedByParty']");

        private static readonly By CitationLocator = By.XPath(".//div[@class='co_searchResults_citation']/span[1]");

        private static readonly By EffectiveDateLocator = By.XPath(".//div[@class='co_searchResults_citation']/span[2]");

        private static readonly By CodeSetNameLocator = By.XPath(".//div[@class='co_searchResults_citation']/span[3]");

        private static readonly By TitleDescriptionLocator = By.XPath(".//div[@class='co_searchResults_citation']/span[4]");

        private static readonly By MostRecentLinkLocator = By.XPath(".//li/*[contains(text(), 'Most recent')]");

        private static readonly By DistinguishedLinkLocator = By.XPath(".//li/*[contains(text(), 'Distinguished')]");

        private static readonly By AllLinkLocator = By.XPath(".//li/*[contains(text(), 'All')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="CitedAuthorityItem"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public CitedAuthorityItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// The all negative treatment link.
        /// </summary>
        public ILink AllLink => new NegativeTreatmentLink(this.Container, AllLinkLocator);

        /// <summary>
        /// CitationLabel
        /// </summary>
        public ILabel CitationLabel => new Label(this.Container, CitationLocator);

        /// <summary>
        /// CodeSetNameLabel
        /// </summary>
        public ILabel CodeSetNameLabel => new Label(this.Container, CodeSetNameLocator);

        /// <summary>
        /// The distinguished negative treatment link.
        /// </summary>
        public ILink DistinguishedLink => new NegativeTreatmentLink(this.Container, DistinguishedLinkLocator);

        /// <summary>
        /// EffectiveDateLabel
        /// </summary>
        public ILabel EffectiveDateLabel => new Label(this.Container, EffectiveDateLocator);

        /// <summary>
        /// The most recent negative treatment link.
        /// </summary>
        public ILink MostRecentLink => new NegativeTreatmentLink(this.Container, MostRecentLinkLocator);

        /// <summary>
        /// The see all history treatment link.
        /// </summary>
        public ILink SeeAllHistoryLink => new Link(this.Container, SeeAllHistoryLocator);

        /// <summary>
        /// TitleDescriptionLabel
        /// </summary>
        public ILabel TitleDescriptionLabel => new Label(this.Container, TitleDescriptionLocator);

        /// <summary>
        /// Gets the list of badges.
        /// </summary>
        public IReadOnlyCollection<ILabel> BadgeLabels => new ElementsCollection<Label>(this.Container, BadgeLocator);

        /// <summary>
        /// Cited by component.
        /// </summary>
        public CitedByComponent CitedBy => new CitedByComponent(DriverExtensions.WaitForElement(this.Container, CitedByComponentLocator));

        /// <summary>
        /// Get warning snippet component
        /// </summary>
        /// <param name="snippetType">Type of snippet</param>
        /// <returns>An instance of WanringSnippetComponent</returns>
        public WarningSnippetComponent GetWarningSnippetComponent(QuickCheckKcSnippets snippetType) 
            => this.GetWarningSnippetComponents().First(component => component.Type.Equals(snippetType));

        /// <summary>
        /// Get warning snippets component
        /// </summary>
        /// <returns>List of Warning snippets component</returns>
        public List<WarningSnippetComponent> GetWarningSnippetComponents() => DriverExtensions.GetElements(this.Container, WarningSnippetLocator)
            .Select(el => new WarningSnippetComponent(el)).ToList();         
    }
}