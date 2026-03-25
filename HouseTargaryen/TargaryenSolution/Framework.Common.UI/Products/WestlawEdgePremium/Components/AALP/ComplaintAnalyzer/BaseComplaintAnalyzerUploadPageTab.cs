namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.ComplaintAnalyzer
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;

    /// <summary>
    /// Complaint Analyzer Base Upload Page Tab
    /// </summary>
    public abstract class BaseComplaintAnalyzerUploadPageTab : BaseTabComponent
    {
        private static readonly By SafeAnalysisButtonLocator = By.XPath(".//saf-button-v3[@data-testid='submit-analysis-button'] | .//saf-button[@data-testid='submit-analysis-button']");

        /// <summary>
        /// Safe Analysis Button
        /// </summary>
        public IButton SafeAnalysisButton => new Button(this.ComponentLocator, SafeAnalysisButtonLocator);

        /// <summary>
        /// Tab Name
        /// </summary>
        protected override string TabName => "Tab";
    }
}
