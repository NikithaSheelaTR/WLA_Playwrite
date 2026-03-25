namespace Framework.Common.UI.Products.WestlawEdge.Components.RightPanel
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    /// <summary>
    /// Right panel result list recommendation item
    /// </summary>
    public sealed class RightPanelResultListItem : RecommendationItem
    {
        private static readonly By ShowMoreButtonLocator = By.XPath(".//button[text() = 'Show less']");
        private static readonly By ShowLessButtonLocator = By.XPath(".//button[text() = 'Show less']");
        private static readonly By MetadataLabelLocator = By.XPath(".//div[contains(@class, 'co_searchResults_citation DA-DocMetadata')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="RightPanelResultListItem"/> class. 
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public RightPanelResultListItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Show more button.
        /// </summary>
        public IButton ShowMoreButton => new Button(this.Container, ShowMoreButtonLocator);

        /// <summary>
        /// Show less button.
        /// </summary>
        public IButton ShowLessButton => new Button(this.Container, ShowLessButtonLocator);

        /// <summary> 
        /// Metadata label.
        /// </summary>
        public ILabel MetadataLabel => new Label(this.Container, MetadataLabelLocator);
    }
}
