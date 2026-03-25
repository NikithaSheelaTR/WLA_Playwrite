namespace Framework.Common.UI.Products.WestlawEdgePremium.Items.AALP.CoCounsel
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// CoCounsel Recent Chat Item
    /// </summary>
    public class CoCounselRecentChatItem : BaseItem
    {
        private static readonly By ChatLabelLocator = By.XPath(".//span[contains(@id, 'chat-text')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="CoCounselRecentChatItem"/> class.
        /// </summary>
        /// <param name="containerElement">
        /// The container locator.
        /// </param>
        public CoCounselRecentChatItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Recent Chat button
        /// </summary>
        public IButton RecentChatButton => new Button(this.Container, ChatLabelLocator);

        /// <summary>
        /// Chat label
        /// </summary>
        public ILabel ChatLabel => new Label(this.Container, ChatLabelLocator);
    }
}
