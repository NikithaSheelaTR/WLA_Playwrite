namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// An omitted by both result item
    /// </summary>
    public class OmittedByBothItem : QuickCheckBaseItem
    {
        private static readonly By SynopsisHoldingsSectionLocator = By.XPath(".//div[@class='DA-OmittedByBothOutcome']");

        private static readonly By HeadingLocator = By.XPath(".//div[contains(@class, 'DA-OmittedByBothRecItemHeading')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="OmittedByBothItem"/> class. 
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public OmittedByBothItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Headings
        /// </summary>
        public ItemsCollection<OmittedByBothHeadingComponent> Headings => new ItemsCollection<OmittedByBothHeadingComponent>(this.Container, HeadingLocator);

        /// <summary>
        /// Gets synopsis holdings section text.
        /// </summary>
        public ILabel SynopsisHoldingsLabel => new Label(this.Container, SynopsisHoldingsSectionLocator);
    }
}
