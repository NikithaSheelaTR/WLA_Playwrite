namespace Framework.Common.UI.Products.WestlawEdge.Components.RightPanel
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Interfaces.Elements;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Raw.WestlawEdge.Items.Folders;

    /// <summary>
    /// AI Summary Right panel component
    /// </summary>
    public class AiSummaryRightPanelComponent : BaseEdgeRightPanelComponent
    {
        private static readonly By AiSummaryContainerLocator = By.XPath("//div[@id='co_aiSummaryPanel']");
        private static readonly By AiSummaryZeroStateMessageLocator = By.XPath("//div[@id='co_aiSummaryPanel']//div[@class='AISummary-zeroState']");
        private static readonly By ElementInTheListOfAiSummariesLocator = By.CssSelector("div.DocumentPanel-content div.AISummary-container");

        /// <summary>
        /// AI Summary zero state message label
        /// </summary>
        public ILabel ZeroStateLabel => new Label(AiSummaryZeroStateMessageLocator);

        /// <summary>
        /// Verify that component is displayed
        /// </summary>
        /// <returns> True if component is displayed, false otherwise </returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(AiSummaryContainerLocator);

        /// <summary>
        /// List of existing AI summaries
        /// </summary>
        public ItemsCollection<AiSummaryItem> ListOfAiSummaries => new ItemsCollection<AiSummaryItem>(this.ComponentLocator, ElementInTheListOfAiSummariesLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => AiSummaryContainerLocator;
    }
}
