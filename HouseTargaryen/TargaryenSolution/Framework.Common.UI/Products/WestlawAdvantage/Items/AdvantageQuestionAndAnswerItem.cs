using System.Collections.Generic;
using Framework.Common.UI.Interfaces.Elements;
using Framework.Common.UI.Products.Shared.Elements;
using Framework.Common.UI.Products.Shared.Elements.Labels;
using Framework.Common.UI.Products.Shared.Elements.Links;
using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;
using OpenQA.Selenium;

namespace Framework.Common.UI.Products.WestlawAdvantage.Components.HomePage
{
    /// <summary>
    /// Advantage Tools Item as a part of Browse Component - Tools List
    /// </summary>
    public class AdvantageQuestionAndAnswerItem : AiAssistedResearchQuestionAndAnswerItem
    {
        private static readonly By ContentTypesLocator = By.Id("tab-content-types");
        private static readonly By ResultsContainerLocator = By.Id("co_fermiSearchResult_data");
        private static readonly By StatutesResultsTitleLinkLocator = By.XPath(".//a[contains(@id, 'cobalt_result_statute_title')]");
        private static readonly By GlobalSearchResultsPageHeadingLocator = By.Id("co_resultsPageLabel");
        private static readonly By StatutesAndCourtRulesLinkLocator = By.XPath("//a[@id='co_search_contentNav_link_STATUTE']");

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvantageToolsTabItem"/> class. 
        /// </summary>
        /// <param name="containerElement"> The container Element. </param>
        public AdvantageQuestionAndAnswerItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Westlaw Advantage Global Search Results Page Heading Label
        /// </summary>
        public ILabel GlobalSearchResultsPageHeading => new Label(GlobalSearchResultsPageHeadingLocator);

        /// <summary>
        /// Statutes and court rules link
        /// </summary>
        public ILink StatutesAndCourtRulesLink => new Link(StatutesAndCourtRulesLinkLocator);

        /// <summary>
        /// Jump links
        /// </summary>
        public IReadOnlyCollection<ILink> StatutesResultsTitleLink => new ElementsCollection<Link>(this.Container, StatutesResultsTitleLinkLocator);
    }
}