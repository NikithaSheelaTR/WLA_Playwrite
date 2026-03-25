namespace Framework.Common.UI.Products.WestlawAdvantage.Components.Judicial
{
    using Framework.Common.UI.Interfaces.Components;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    /// <summary>
    /// Citation Issues component
    /// </summary>
    public class CitationIssuesComponent : BaseModuleRegressionComponent
    {
        private static readonly By PartyContainerLocator = By.XPath(".//div[contains(@class,'partyContainer')]");
        private static readonly By TitleLabelLocator = By.XPath(".//h2");

        /// <summary>
        /// Initializes a new instance of the <see cref="CitationIssuesComponent"/> class.
        /// </summary>
        /// <param name="containerLocator">
        /// The container.
        /// </param>
        public CitationIssuesComponent(By containerLocator)
        {
            this.ComponentLocator = containerLocator;
        }

        /// <summary>
        /// Title label
        /// </summary>
        public ILabel TitleLabel => new Label(this.ComponentLocator, TitleLabelLocator);

        /// <summary>
        /// Party Issue Boxes
        /// </summary>
        public IItemsCollection<PartyContainerComponent> PartyIssueBoxes => 
            new ItemsCollection<PartyContainerComponent>(this.ComponentLocator, PartyContainerLocator);

        /// <summary>
        /// Component locator.
        /// </summary>
        protected override By ComponentLocator { get; }
    }
}
