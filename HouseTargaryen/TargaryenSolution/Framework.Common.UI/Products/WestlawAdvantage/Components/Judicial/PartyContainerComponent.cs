namespace Framework.Common.UI.Products.WestlawAdvantage.Components.Judicial
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// Party Container component
    /// </summary>
    public class PartyContainerComponent : BaseItem
    {
        private static readonly By DocumentTitleButtonLocator = By.XPath(".//span[contains(@class, 'buttonDocumentName')]//saf-button");
        private static readonly By NoCitationMessageLabelLocator = By.XPath(".//span[contains(@class, 'buttonDocumentName')]//following-sibling::div");
        private static readonly By CountLabelLocator = By.XPath(".//saf-badge");

        /// <summary>
        /// Initializes a new instance of the <see cref="PartyContainerComponent"/> class.
        /// </summary>
        /// <param name="container">container</param>
        public PartyContainerComponent(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// Document Title button
        /// </summary>
        public IButton DocumentTitleButton => new Button(this.Container, DocumentTitleButtonLocator);

        /// <summary>
        /// No citation message label
        /// </summary>
        public ILabel NoCitationMessageLabel => new Label(this.Container, NoCitationMessageLabelLocator);

        /// <summary>
        /// Count label
        /// </summary>
        public ILabel CountLabel => new Label(this.Container, DocumentTitleButtonLocator, CountLabelLocator);
    }
}
