namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;

    /// <summary>
    /// Explore more Column component
    /// </summary>
    public class ExploreMoreColumnComponent : BaseItem
    {
        private static readonly By ExploreMoreHeaderColumnLabelLocator = By.XPath("//th[contains(@id, 'verifier-table-explore')]");
        private static readonly By ExploreMoreContentLocator = By.XPath(".//td[contains(@headers, 'verifier-table-explore')]");
        private static readonly By ExploreMoreCiitingReferencesLinkLocator = By.XPath(".//dt[contains(text(), 'Citing References')]/following-sibling::dd/saf-anchor-v3/saf-icon-v3");
        private static readonly By ExploreMoreColumnKeyNumbersLinkLocator = By.XPath(".//dt[text() = 'Key Numbers']/following-sibling::dd");
        private static readonly By ExploreMoreColumnPrecisionResearchLinkLocator = By.XPath(".//dt[text() = 'Precision Research']/following-sibling::dd");
        private static readonly By ExploreMoreParallelSearchLinkLocator = By.XPath(".//dt[contains(text(), 'Parallel Search')]/following-sibling::dd/saf-anchor-v3/saf-icon-v3");

        /// <summary>
        /// Initializes a new instance of the <see cref="ExploreMoreColumnComponent"/> class.
        /// </summary>
        /// <param name="containerElement">
        /// The container locator.
        /// </param>
        public ExploreMoreColumnComponent(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Explore more column label
        /// </summary>
        public ILabel ExploreMoreLabel => new Label(this.Container, ExploreMoreHeaderColumnLabelLocator);

        /// <summary>
        /// List of Citing References links
        /// </summary>
        public IReadOnlyCollection<ILink> CitingReferencesLinks => new ElementsCollection<Link>(this.Container, ExploreMoreContentLocator, ExploreMoreCiitingReferencesLinkLocator);

        /// <summary>
        /// List of Key Numbers Links
        /// </summary>
        public IReadOnlyCollection<ILink> KeyNumbersLinks => new ElementsCollection<Link>(this.Container, ExploreMoreContentLocator, ExploreMoreColumnKeyNumbersLinkLocator);

        /// <summary>
        /// List of Presicion Research Links
        /// </summary>
        public IReadOnlyCollection<ILink> PresicionResearchLinks => new ElementsCollection<Link>(this.Container, ExploreMoreContentLocator, ExploreMoreColumnPrecisionResearchLinkLocator);

        /// <summary>
        /// List of Parallel Search links
        /// </summary>
        public IReadOnlyCollection<ILink> ParallelSearchLinks => new ElementsCollection<Link>(this.Container, ExploreMoreContentLocator, ExploreMoreParallelSearchLinkLocator);
    }
}
