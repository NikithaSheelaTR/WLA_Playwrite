namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.HomePage
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.AssistedResearch;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using OpenQA.Selenium;

    /// <summary>
    /// Canada AI Assisted Research Page
    /// </summary>
    public class CanadaAiArPage : EdgeCommonAuthenticatedWestlawNextPage
    {
        private static readonly By ResearchHistoryLocator = By.XPath("//button[contains(@aria-label,'Research History')]");
        private static readonly By FullHistoryLinkLocator = By.ClassName("CS-thread-history-full");

        /// <summary>
        /// Chat Component
        /// </summary>
        public ChatComponent Chat { get; } = new ChatComponent();

        /// <summary>
        /// Query box
        /// </summary>
        public QueryBoxComponent QueryBox { get; } = new QueryBoxComponent();

        /// <summary> 
        /// Toolbar component  
        /// </summary>
        public CanadaAiArToolbar Toolbar { get; } = new CanadaAiArToolbar();

        /// <summary>
        /// Full History Link
        /// </summary>
        public ILink FullHistoryLink => new Link(FullHistoryLinkLocator);

        /// <summary>
        /// Research History button
        /// </summary>
        public IButton ResearchHistoryButton => new Button(ResearchHistoryLocator);
    }
}