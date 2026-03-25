namespace Framework.Common.UI.Products.WestLawNextCanada.Dialogs.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using OpenQA.Selenium;

    /// <summary>
    /// Cited Authority Full View Dialog
    /// </summary>
    public class CitedAuthorityFullViewDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.Id("coid_da_plainTextDialog");
        private static readonly By LegalIssueLabelLocator = By.XPath(".//label[@for='legalIssueInput_modal']");
        private static readonly By LegalIssueTextboxLocator = By.Id("legalIssueInput_modal");
        private static readonly By PlainTextLabelLocator = By.XPath(".//label[@for='plainTextInput_modal']");
        private static readonly By PlainTextTextboxLocator = By.Id("plainTextInput_modal");
        private static readonly By StartAnalysisButtonLocator = By.XPath(".//button[text()='Start analysis']");

        /// <summary>
        /// Gets the heading textbox.
        /// </summary>
        public ILabel LegalIssueLabel => new Label(ContainerLocator, LegalIssueLabelLocator);

        /// <summary>
        /// Gets the legal issue textbox.
        /// </summary>
        public ITextbox LegalIssueTextbox => new Textbox(ContainerLocator, LegalIssueTextboxLocator);

        /// <summary>
        /// Plain text label.
        /// </summary>
        public ILabel PlainTextLabel => new Label(ContainerLocator, PlainTextLabelLocator);

        /// <summary>
        /// Gets the PLain textbox.
        /// </summary>
        public ITextbox PlainTextTextbox => new Textbox(ContainerLocator, PlainTextTextboxLocator);

        /// <summary>
        /// Start analysis button.
        /// </summary>
        public IButton StartAnalysisButton => new Button(ContainerLocator, StartAnalysisButtonLocator);
    }
}
