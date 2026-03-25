namespace Framework.Common.UI.Products.WestLawNextCanada.Components.SearchQuestionAnswer
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestlawEdge.Components.SearchQuestionAnswerComponent;

    using OpenQA.Selenium;

    /// <summary>
    /// The Search Question Answer component.
    /// </summary>
    public class CanadaSearchQuestionAnswerComponent : EdgeSearchQuestionAnswerComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[contains(@id,'coid_website_questionAndAnswer')]");

        private static readonly By JurisdictionLinkLocator = By.XPath("//a[@class = 'co_searchResults_jurisdiction']");

        /// <summary>
        /// The Question and Answer Result list component
        /// </summary>
        public CanadaSearchQuestionAnswerResultListComponent CanadaQnAResultList { get; } =
            new CanadaSearchQuestionAnswerResultListComponent();

        /// <summary>
        /// Gets all the jurisdiction links.
        /// </summary>
        public IReadOnlyCollection<ILink> AllJurisdictionLinks =>
            new ElementsCollection<Link>(this.ComponentLocator, JurisdictionLinkLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}