namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.WestlawEdge.Elements;

    using OpenQA.Selenium;

    /// <summary>
    /// Upload dialog=>Click Text with cited authorities link.
    /// </summary>
    public class TextWithCitedAuthorityDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.Id("coid_da_plainTextDialog");

        private static readonly By LegalIssueLocator = By.Id("legalIssueInput");

        private static readonly By TextWithCitedAuthorityLocator = By.Id("plainTextInput");

        private static readonly By ContinueButtonLocator = By.ClassName("co_primaryBtn");

        private static readonly By CancelButtonLocator = By.ClassName("co_linkBlue");

        private static readonly By XButtonLocator = By.ClassName("co_iconBtn");

        private static readonly By ErrorMessageLocator = By.ClassName("co_errorMessage");

        /// <summary>
        /// Gets the cancel button.
        /// </summary>
        public IButton CancelButton => new Button(ContainerLocator, CancelButtonLocator);

        /// <summary>
        /// Gets the continue button.
        /// </summary>
        public IButton ContinueButton => new Button(ContainerLocator, ContinueButtonLocator);

        /// <summary>
        /// Gets the x button.
        /// </summary>
        public IButton XButton => new Button(ContainerLocator, XButtonLocator);

        /// <summary>
        /// Gets the heading textbox.
        /// </summary>
        public ITextbox HeadingTextbox => new Textbox(ContainerLocator, LegalIssueLocator);

        /// <summary>
        /// Gets the plain text textbox.
        /// </summary>
        public ITextbox PlainTextTextbox => new SearchWithinTextbox(ContainerLocator, TextWithCitedAuthorityLocator);

        /// <summary>
        /// Error message label
        /// </summary>
        public ILabel ErrorMessageLabel => new Label(ContainerLocator, ErrorMessageLocator);        
    }
}