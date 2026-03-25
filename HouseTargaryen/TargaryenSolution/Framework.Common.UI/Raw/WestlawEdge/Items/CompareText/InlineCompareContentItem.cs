namespace Framework.Common.UI.Raw.WestlawEdge.Items.CompareText
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// Inline compare content item on Compare View tab
    /// </summary>
    public class InlineCompareContentItem : BaseItem
    {
        private static readonly By PrimaryBadgeLocator = By.XPath(".//span[@class = 'Badge badge--mutedIndigo']");
        private static readonly By TitleLocator = By.XPath(".//span[@class = 'inlineCompareTitle']");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="InlineCompareContentItem"/> class.
        /// </summary>
        public InlineCompareContentItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Title
        /// </summary>
        public ILabel Title => new Label(this.Container, TitleLocator);

        /// <summary>
        /// Primary badge
        /// </summary>
        public ILabel PrimaryBadge => new Label(this.Container, PrimaryBadgeLocator);
    }
}
