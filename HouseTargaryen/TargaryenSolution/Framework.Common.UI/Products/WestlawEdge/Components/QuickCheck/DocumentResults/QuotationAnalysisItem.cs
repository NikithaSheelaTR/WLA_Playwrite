namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.QuickCheck;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Quotation analysis item.
    /// </summary>
    public class QuotationAnalysisItem : QuickCheckBaseItem
    {
        private static readonly By DifferencesBadgeLocator = By.XPath(".//span[@class='DA-DifferencesLabel']");

        private static readonly By DocumentQuoteLocator = By.XPath(".//td[@class='DA-QuoteContainer-matched']//div/a");
        private static readonly By DocumentPreQuoteLocator = By.XPath(".//td[@class='DA-PreQuoteContainer-matched']//a");
        private static readonly By DocumentPostQuoteLocator = By.XPath(".//td[@class='DA-PostQuoteContainer-matched']//a");

        private static readonly By EmptyDocumentQuoteLocator = By.XPath(".//div[@class='DA-QuotationNoMatch']");

        private static readonly By HighlightedDocumentQuoteTermLocator = By.XPath(".//td[@class='DA-QuoteContainer-matched']//span[@class='co_quoteMatch']");
        private static readonly By HighlightedTermLocator = By.XPath(".//span[@class='co_quoteMatch']");

        private static readonly By IdLocator = By.XPath(".//div[@class ='co_issueItemHeader']//input");

        private static readonly By JudicialQuoteHeaderLocator = By.XPath(".//div[@class ='DA-DocumentLink']//ancestor::a");

        private static readonly By QuotationsLeftSideLinksLocators = By.XPath(".//td[@class='DA-PostQuoteContainer-submitted']//a");
        private static readonly By QuoteHeaderLocator = By.XPath(".//div[@class ='DA-DocumentLink']/a");

        private static readonly By PinCiteArrowsMarkedTermLocator = By.XPath(".//span[starts-with(text(), '<<*') and contains(text(),'*>>')]");
        private static readonly By PinCiteErrorRedBoxLocator = By.XPath(".//td[contains(@class, 'submitted')]//span[@class='co_quotePinCite']"); 

        private static readonly By TitleLocator = By.XPath(".//div[@class ='DA-DocumentLink']");

        private static readonly By UserQuoteLocator = By.XPath(".//td[@class='DA-QuoteContainer-submitted']//div");
        private static readonly By UserPreQuoteLocator = By.XPath(".//td[@class='DA-PreQuoteContainer-submitted']/div");
        private static readonly By UserPostQuoteLocator = By.XPath(".//td[@class='DA-PostQuoteContainer-submitted']/div");

        private static readonly By UserCiteLinkQuoteDocumentLocator = By.XPath(".//td[contains(@class, 'submitted')]//a[contains(@href, 'anchor')]");
        private static readonly By UserCiteLinkLocator = By.XPath(".//td[contains(@class, 'submitted')]//a[not(contains(@href, 'anchor'))]");

        private static readonly By ParaphrasesResultsLocator = By.XPath("//li[@class= 'DA-QuoteSection']");
        private static readonly By AiDisclosureLabelsLocator = By.XPath("//div[contains(@class, 'bsCardAiGenerated')]");
        private static readonly By PreQuoteParaphraseLocator = By.XPath("//div[contains(@class, 'DA-PreQuoteContainer-submitted')]//div[contains(@class,'DA-PreQuote')]/p");
        private static readonly By PostQuoteParaphraseLocator = By.XPath("//div[contains(@class, 'DA-PostQuoteContainer-submitted')]//div[contains(@class,'DA-PostQuote')]/p");

        private static readonly By PreQuoteTextLinkLocator = By.XPath(".//div[@class= 'DA-PreQuote']//a");
        private static readonly By PostQuoteTextLinkLocator = By.XPath(".//div[@class= 'DA-PostQuote']//a");
        private static readonly By QuoteTextLocator = By.XPath("//div[@class= 'DA-SearchTermContainer']");

        /// <summary>
        /// Initializes a new instance of the <see cref="QuotationAnalysisItem"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public QuotationAnalysisItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Potential mischaracterization component
        /// </summary>
        public PotentialMischaracterizationComponent PotentialMischaracterizationBox => new PotentialMischaracterizationComponent(this.Container);
        
        /// <summary> Differences badge </summary>
        public ILabel DifferencesBadgeLabel => new Label(this.Container, DifferencesBadgeLocator);
        
        /// <summary> Document quote link </summary>
        public ILink DocumentQuoteLink => new Link(this.Container, DocumentQuoteLocator);

        /// <summary> Document pre quote label </summary>
        public ILink DocumentPreQuoteLink => new Link(this.Container, DocumentPreQuoteLocator);

        /// <summary> Document post quote label </summary>
        public ILink DocumentPostQuoteLink => new Link(this.Container, DocumentPostQuoteLocator);

        /// <summary> Unverified/empty document quote </summary>
        public ILabel EmptyDocumentQuoteLabel => new Label(this.Container, EmptyDocumentQuoteLocator);

        /// <summary> Judicial Quote Header Link </summary>
        public ILink JudicialQuoteHeaderLink => new Link(this.Container, JudicialQuoteHeaderLocator);

        /// <summary> Quote header link </summary>
        public ILink QuoteHeaderLink => new Link(this.Container, QuoteHeaderLocator);

        /// <summary> Title link </summary>
        public ILink TitleLabel => new Link(this.Container, TitleLocator);

        /// <summary> User pre quote label </summary>
        public ILabel UserPreQuoteLabel => new Label(this.Container, UserPreQuoteLocator);

        /// <summary> User post quote label </summary>
        public ILabel UserPostQuoteLabel => new Label(this.Container, UserPostQuoteLocator);

        /// <summary> User quote label </summary>
        public ILabel UserQuoteLabel => new Label(this.Container, UserQuoteLocator);

        /// <summary> Pre quote paraphrase label </summary>
        public ILabel PreQuoteParaphraseLabel => new Label(this.Container, PreQuoteParaphraseLocator);

        /// <summary> Post quote paraphrase label </summary>
        public ILabel PostQuoteParaphraseLabel => new Label(this.Container, PostQuoteParaphraseLocator);

        /// <summary> User cite link to the document from the related quotation link </summary>
        public ILink UserCiteQuoteDocumentLink => new Link(this.Container, UserCiteLinkQuoteDocumentLocator);

        /// <summary> User cite link to the document that is not the related to the document from the quotation link </summary>
        public ILink UserCiteLink => new Link(this.Container, UserCiteLinkLocator);

        /// <summary>
        /// Pre-Text Links from each quotation
        /// </summary>
        public ILink PreQuoteTextLink => new Link(this.Container, PreQuoteTextLinkLocator);

        /// <summary>
        /// Post-Text Links from each quotation
        /// </summary>
        public ILink PostQuoteTextLink => new Link(this.Container, PostQuoteTextLinkLocator);

        /// <summary>
        /// Quote Labels from each quotation
        /// </summary>
        public IReadOnlyCollection<ILabel> QuoteTextLabels => new ElementsCollection<Label>(this.Container, QuoteTextLocator);

        /// <summary>
        /// AI Disclosure labels
        /// </summary>
        public IReadOnlyCollection<ILabel> AiDisclosureLabels => new ElementsCollection<Label>(this.Container, AiDisclosureLabelsLocator);

        /// <summary>
        /// Pincite error labels
        /// </summary>
        public IReadOnlyCollection<ILabel> PinCiteErrorLabels => new ElementsCollection<Label>(this.Container, PinCiteErrorRedBoxLocator);

        /// <summary>
        /// Paraphrases Result labels
        /// </summary>
        public IReadOnlyCollection<ILabel> ParaphrasesResultsLabels => new ElementsCollection<Label>(this.Container, ParaphrasesResultsLocator);

        /// <summary>
        /// Left side links
        /// </summary>
        public IReadOnlyCollection<ILink> QuotationsLeftSideLinks => new ElementsCollection<Link>(this.Container, QuotationsLeftSideLinksLocators);

        /// <summary> The value of doc guid attribute </summary>
        public new string DocGuid =>
            this.DocumentQuoteLink.Displayed ? this.DocumentQuoteLink.GetAttribute("href").ExtractWestlawGuid() : string.Empty;
        
        /// <summary>
        /// Get highlighted text in Westlaw document quotations.
        /// </summary>
        /// <returns>List highlighted words in Westlaw document quotations</returns>
        public List<string> GetHighlightedDocumentQuoteTermsText()
        {
            IList<IWebElement> highlightedDocumentQuoteTerms = DriverExtensions.GetElements(this.Container, HighlightedDocumentQuoteTermLocator);
            return highlightedDocumentQuoteTerms.Any() ? highlightedDocumentQuoteTerms.Select(item => item.Text).ToList() : new List<string> {""};
        }

        /// <summary>
        /// Get item id.
        /// </summary>
        /// <returns>Id</returns>
        public string GetId() => DriverExtensions.GetElement(this.Container, IdLocator).GetAttribute("id");

        /// <summary>
        /// Is highlighted terms displayed
        /// </summary>
        /// <returns>True if it is displayed, false - otherwise</returns>
        public bool IsHighlightedTermsDisplayed() => DriverExtensions.GetElements(this.Container, HighlightedTermLocator).Any(item => item.Displayed);

        /// <summary>
        /// Is arrows marked terms displayed
        /// </summary>
        /// <returns>True if it is displayed, false - otherwise</returns>
        public bool IsPinCiteArrowsMarkedTermsDisplayed() =>
            DriverExtensions.GetElements(this.Container, PinCiteArrowsMarkedTermLocator).Any(item => item.Displayed);
    }
}