namespace Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.CoCounsel;

    /// <summary>
    /// Ai Assistant Page
    /// </summary>
    public class AiAssistedResearchPage : EdgeCommonAuthenticatedWestlawNextPage
    {
        private static readonly By FolderMessageLabelLocator = By.XPath("//*[@class='co_foldering_popupMessageContainer']//*[@class='co_infoBox_message']");
        private static readonly By FolderedContentLabelLocator = By.XPath("//div[@class='cobalt_ro_documentDescription']/span");

        /// <summary>
        /// Conversation history
        /// </summary>
        public ConversationHistoryComponent ConversationHistory { get; } = new ConversationHistoryComponent();

        /// <summary>
        /// Toolbar
        /// </summary>
        public Toolbar Toolbar { get; } = new Toolbar();

        /// <summary>
        /// Chat
        /// </summary>
        public ChatComponent Chat { get; } = new ChatComponent();

        /// <summary>
        /// Query box
        /// </summary>
        public QueryBoxComponent QueryBox { get; } = new QueryBoxComponent();

        /// <summary>
        /// Usage debug
        /// </summary>
        public UsageDebugComponent UsageDebug { get; } = new UsageDebugComponent();

        /// <summary>
        /// Folder message
        /// </summary>
        public ILabel FolderMessageLabel => new Label(FolderMessageLabelLocator);

        /// <summary>
        /// Folder content label
        /// </summary>
        public ILabel FolderContentLabel => new Label(FolderedContentLabelLocator);
    }
}
