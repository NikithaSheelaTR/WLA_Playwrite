namespace Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck
{
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck;

    using OpenQA.Selenium;

    /// <summary>
    /// The quick check landing page.
    /// </summary>
    public class QuickCheckLandingPage : QuickCheckBasePage
    {
        private static readonly By CheckWorkTileLocator = By.XPath("//div[contains(@class,'co_column check-work')]");
        private static readonly By JudicialTileLocator = By.XPath("//div[contains(@class,'co_column judicial')]");
        private static readonly By ComplaintAnalyserTileLocator = By.XPath("//saf-card-v3[contains(@class,'__uploadCard')]//*[text()='Complaint analysis'] | //saf-card[contains(@class,'__uploadCard')]//*[text()='Complaint analysis']");

        /// <summary>
        /// Gets the check work tile.
        /// </summary>
        public QuickCheckLandingTileComponent CheckWorkTile { get; } = new QuickCheckLandingTileComponent(CheckWorkTileLocator);

        /// <summary>
        /// Gets the Judicial tile.
        /// </summary>
        public QuickCheckLandingTileComponent JudicialTile { get; } = new QuickCheckLandingTileComponent(JudicialTileLocator);

        /// <summary>
        /// Gets the Complaint Analyser tile.
        /// </summary>
        public QuickCheckLandingTileComponent ComplaintAnalyserTile { get; } = new QuickCheckLandingTileComponent(ComplaintAnalyserTileLocator);
    }
}