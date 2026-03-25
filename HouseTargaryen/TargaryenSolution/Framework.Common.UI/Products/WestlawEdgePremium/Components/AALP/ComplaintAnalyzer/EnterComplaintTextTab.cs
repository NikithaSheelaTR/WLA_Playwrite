namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.ComplaintAnalyzer
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using OpenQA.Selenium;

    /// <summary>
    /// Complaint Analyzer - Enter Complaint Text Tab
    /// </summary>
    public class EnterComplaintTextTab : BaseComplaintAnalyzerUploadPageTab
    {
        private static readonly By TabContainerLocator = By.XPath("//saf-tab-panel-v3[@data-testid='enter-complaint-panel'] | //saf-tab-panel[@data-testid='enter-complaint-panel']");
        private static readonly By EnterComplaintTextboxLocator = By.XPath(".//saf-text-area-v3[@data-testid='complaint-text-area'] | .//saf-text-area[@data-testid='complaint-text-area']");
        private static readonly By RemainingCharacterLabelLocator = By.XPath(".//span[@data-testId='remaining-character-count']");

        /// <summary>
        /// Question textbox
        /// </summary>
        public ITextbox EnterComplaintTextbox => new Textbox(this.ComponentLocator, EnterComplaintTextboxLocator);

        /// <summary>
        /// Remaining Character Count
        /// </summary>
        public ILabel RemainingCharacterLabel => new Label(this.ComponentLocator, RemainingCharacterLabelLocator);

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Enter complaint text";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TabContainerLocator;
    }
}
