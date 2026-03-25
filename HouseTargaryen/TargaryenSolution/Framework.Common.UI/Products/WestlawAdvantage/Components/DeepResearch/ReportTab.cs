namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using OpenQA.Selenium;

    /// <summary>
    /// Report Tab
    /// </summary>
    public class ReportTab : BaseBrowseTabPanelComponent
    {
        private static readonly By TabContainerLocator = By.XPath("//div[@data-testid='report-answer-panel-reporttab']");

        /// <summary>
        /// Deep Research Result Report Tab Left Column Component
        /// </summary>
        public AiDeepResearchReportLeftColumnComponent LeftColumnComponent { get; } = new AiDeepResearchReportLeftColumnComponent();

        /// <summary>
        /// Deep Research Result Report Tab Right Column Component
        /// </summary>
        public AiDeepResearchReportRightColumnComponent RightColumnComponent { get; } = new AiDeepResearchReportRightColumnComponent();

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Report";
        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TabContainerLocator;
    }
}
