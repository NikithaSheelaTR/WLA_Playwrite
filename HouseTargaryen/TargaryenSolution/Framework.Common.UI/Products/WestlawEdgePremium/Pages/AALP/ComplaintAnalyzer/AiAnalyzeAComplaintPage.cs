namespace Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP.ComplaintAnalyzer
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.ComplaintAnalyzer;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using OpenQA.Selenium;

    /// <summary>
    /// Ai Analyze a Complaint Page
    /// </summary>
    public class AiAnalyzeAComplaintPage : EdgeCommonAuthenticatedWestlawNextPage
    {
        private static readonly By BackToStartLinkLocator = By.XPath("//saf-anchor-v3[@data-testid='back-to-start-link'] | //saf-anchor[@data-testid='back-to-start-link']");

        /// <summary>
        /// Get the Complaint Analyzer Tab Panel
        /// </summary>
        public ComplaintAnalyzerTabPanel ComplaintAnalyzerTabPanel { get; } = new ComplaintAnalyzerTabPanel();

        /// <summary>
        /// Back to start link
        /// </summary>
        public ILink BackToStartLink => new Link(BackToStartLinkLocator);
    }
}
