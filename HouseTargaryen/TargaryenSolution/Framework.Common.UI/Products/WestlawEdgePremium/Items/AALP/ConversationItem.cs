namespace Framework.Common.UI.Products.WestlawEdgePremium.Items.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// Conversation item
    /// </summary>
    public class ConversationItem : BaseItem
    {
        private static readonly By ActiveLabelLocator = By.XPath(".//*[@class='CS-thread-read-only']");
        private static readonly By TimeLabelLocator = By.XPath(".//time[@datetime]");
        private static readonly By ContentLabelLocator = By.XPath(".//span[contains(text(), 'Content:')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="ConversationItem"/> class.
        /// </summary>
        /// <param name="containerElement">
        /// The container locator.
        /// </param>
        public ConversationItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Conversation button
        /// </summary>
        public IButton ConversationButton => new Button(this.Container);

        /// <summary>
        /// Cpntent label
        /// </summary>
        public ILabel ContentLabel => new Label(this.Container, ContentLabelLocator);

        /// <summary>
        /// Time label
        /// </summary>
        public ILabel TimeLabel => new Label(this.Container, TimeLabelLocator);

        /// <summary>
        /// Active label
        /// </summary>
        public ILabel ActiveLabel => new Label(this.Container, ActiveLabelLocator);
    }
}
