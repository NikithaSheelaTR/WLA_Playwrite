namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// Item inside the accrodion item
    /// </summary>
    public class AiClaimsSubHeadingItem : BaseItem
    {
        private static readonly By HeadingLabelLocator = By.XPath(".//h6[@class='CS-ai-result-card-heading']/parent::div | .//span[contains(@class,'Content-module__citation__')]/ancestor::h6");
        private static readonly By BadgeLabelLocator = By.XPath(".//div[contains(@class, 'Content-module__citationIconContent')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="AiClaimsSubHeadingItem"/> class.
        /// </summary>
        /// <param name="container">
        /// The container locator.
        /// </param>
        public AiClaimsSubHeadingItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Heading label (title + badge)
        /// </summary>
        public ILabel HeadingLabel => new Label(this.Container, HeadingLabelLocator);

        /// <summary>
        /// Badge label
        /// </summary>
        public ILabel BadgeLabel => new Label(this.Container, HeadingLabelLocator, BadgeLabelLocator);
    }
}
