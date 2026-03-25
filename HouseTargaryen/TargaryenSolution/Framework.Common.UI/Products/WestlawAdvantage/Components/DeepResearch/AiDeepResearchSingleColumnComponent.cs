namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using OpenQA.Selenium;

    /// <summary>
    /// AI Deep Research Single column component
    /// </summary>
    public class AiDeepResearchSingleColumnComponent : BaseModuleRegressionComponent
    {
        private static readonly By SimpleAnswerContainerLocator = By.XPath("//div[contains(@class,'SimpleAnswerPage-module__pageWrapper')]");
        private static readonly By ProgressBarLabelLocator = By.XPath("//saf-progress-v3[@data-testid='thinking-progress-bar' or @data-testid='answer-progress-bar']");
        private static readonly By GenerateFullReportButtonLocator = By.XPath("//button[@data-testid='request-report-response-button']");
        private static readonly By AlertMessageLabelLocator = By.XPath(".//saf-alert-v3[@data-testid='next-action-error-alert']");
        private static readonly By DownloadReportButtonLocator = By.XPath("//saf-button-v3[@data-testid='delivery-button']");
        private static readonly By CitationLinksLocator = By.XPath(".//a[contains(@class,'SimpleInlineCitationLink-module__citationLink')]");
        private static readonly By OutOfScopeMessageLocator = By.XPath(".//saf-alert-v3[@data-testid='message-alert']");
        
        /// <summary>
        /// Progress bar label
        /// </summary>
        public ILabel ProgressBarLabel => new Label(ProgressBarLabelLocator);

        /// <summary>
        /// Generate a full report button (forced report link)
        /// </summary>
        public IButton GenerateFullReportButton => new Button(this.ComponentLocator, GenerateFullReportButtonLocator);

        /// <summary>
        /// Alert Message Label
        /// </summary>
        public ILabel AlertMessageLabel => new Label(AlertMessageLabelLocator);

        /// <summary>
        /// Download Report button
        /// </summary>
        public IButton DownloadReportButton => new Button(this.ComponentLocator, DownloadReportButtonLocator);

        /// <summary>
        /// List of citation links
        /// </summary>
        public IReadOnlyCollection<ILink> CitationLinks => new ElementsCollection<Link>(this.ComponentLocator, CitationLinksLocator);

        /// <summary>
        /// Out of scope message label
        /// </summary>
        public IButton OutOfScopeMessageLabel => new Button(this.ComponentLocator, OutOfScopeMessageLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => SimpleAnswerContainerLocator;
    }
}



