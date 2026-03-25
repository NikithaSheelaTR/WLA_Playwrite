namespace Framework.Common.UI.Raw.WestlawEdge.Items.Folders
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// Each item represents item in the list of AI summaries
    /// </summary>
    public class AiSummaryItem : BaseItem
    {
        private static readonly By AiSummaryTitleLocator = By.CssSelector("h3.Heading4");
        private static readonly By AiSummaryContentLocator = By.CssSelector("h3.AISummary-content");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="AiSummaryItem"/> class. 
        /// </summary>
        /// <param name="container">Container</param>
        public AiSummaryItem(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// Label shows current Summary's title
        /// </summary>
        public ILabel CurrentAiSummaryTitleLabel => new Label(this.Container, AiSummaryTitleLocator);

        /// <summary>
        /// Label shows current Summary's content
        /// </summary>
        public ILabel CurrentAiSummaryContentLabel => new Label(this.Container, AiSummaryContentLocator);
    }
}
