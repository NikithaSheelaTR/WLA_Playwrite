namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.AALP;
    using OpenQA.Selenium;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    /// <summary>
    /// Conversation history component
    /// </summary>
    public class ConversationHistoryComponent: BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class='CS-navigation']");
        private static readonly By ConversationHistoryStateLocator = By.XPath("./ancestor::*[contains(@id, 'conversation')]/div[contains(@class, 'CS-container')]");
        private static readonly By ConversationItemLocator = By.XPath(".//li");
        private static readonly By ConversationHistoryExpandButtonLocator = By.XPath(".//button[contains(@class, 'CS-chat-control')]");
        private static readonly By ConversationHistoryCollapseButtonLocator = By.XPath(".//button[contains(@class, 'CS-chat-controls-close-button')]");
        private static readonly By GoToFullHistoryLinkLocator = By.XPath(".//*[@class='CS-thread-history-full']");
        private static readonly By ConversationHistoryButtonLocator = By.XPath("//*[@id='PanelContent']/div[1]/div/button");
        private static readonly By TooltipLabelLocator = By.XPath("//*[@id='history-button-tooltip']");
        /// <summary>
        /// Conversations list
        /// </summary>
        public ItemsCollection<ConversationItem> Conversations => new ItemsCollection<ConversationItem>(this.ComponentLocator, ConversationItemLocator);        

        /// <summary>
        /// Go to full history link
        /// </summary>
        public ILink GoToFullHistoryLink => new Link(this.ComponentLocator, GoToFullHistoryLinkLocator);

        /// <summary>
        /// Conversaton history expand button
        /// </summary>
        public IButton ExpandButton => new Button(this.ComponentLocator, ConversationHistoryExpandButtonLocator);

        /// <summary>
        /// Conversaton history collapse button
        /// </summary>
        public IButton CollapseButton => new Button(this.ComponentLocator, ConversationHistoryCollapseButtonLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Is conversation history expanded
        /// </summary>
        public bool IsCollapsed() => DriverExtensions.GetAttribute("class", this.ComponentLocator, ConversationHistoryStateLocator).Contains("CS-container-minimized");

        /// <summary>
        /// Conversaton history button
        /// </summary>
        public IButton HistoryButton => new Button(this.ComponentLocator, ConversationHistoryButtonLocator);

        /// <summary>
        /// Tooltip label
        /// </summary>
        public ILabel TooltipLabel => new Label(this.ComponentLocator, TooltipLabelLocator);
    }
}
