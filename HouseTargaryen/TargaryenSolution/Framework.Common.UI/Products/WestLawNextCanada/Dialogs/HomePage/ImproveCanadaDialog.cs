namespace Framework.Common.UI.Products.WestLawNextCanada.Dialogs.HomePage
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;

    using OpenQA.Selenium;

    /// <summary>
    /// Improve Westlaw Edge Canada dialog
    /// </summary>
    /// <seealso cref="BaseModuleRegressionDialog" />
    public class ImproveCanadaDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//div[contains(@class, 'co_overlayBox_exitFeedbackForm')]");

        private static readonly By ContactSupportNumberTextLocator = By.XPath("//div[text()=' Toll Free (Canada & US): 1-800-387-5164 - select option 3']");

        private static readonly By DialogTitleLocator = By.XPath("//h2[contains(@id , 'coid_lightboxAriaLabel')]");

        private static readonly By FeedbackCancelButtonLocator = By.Id("co_pageSurveyCancel");

        private static readonly By FeedbackClosePopupButtonLocator = By.Id("co_overlayBox_exitFeedbackForm_closeLink");

        private static readonly By FeedbackTitleTextLocator = By.Id("qa_feedback_title");

        private static readonly By FeedbackUsefulRadioButtonLocator = By.Id("qa_feedback_useful");

        private static readonly By FeedbackErrorRadioButtonLocator = By.Id("qa_feedback_error");

        private static readonly By FeedbackGeneralRadioButtonLocator = By.Id("qa_feedback_general");

        private static readonly By FeedbackSurveyCommentTextBoxLocator = By.Id("co_pageSurveyComment");

        private static readonly By FeedbackSubmitButtonLocator = By.Id("co_pageSurveySubmit");

        private static readonly By FeedbackSuccessMessageLocator = By.XPath("//p[text()='Thank you for your feedback']");

        private static readonly By FeedbackWarningMessageLocator = By.XPath("//div[@class = 'co_infoBox_message' and text()='Please complete comments field']");

        /// <summary>
        /// Gets the feedback cancel button.
        /// </summary>
        public IButton FeedbackCancelButton => new Button(ContainerLocator, FeedbackCancelButtonLocator);

        /// <summary>
        /// Gets the feedback close popup button.
        /// </summary>
        public IButton FeedbackClosePopupButton => new Button(ContainerLocator, FeedbackClosePopupButtonLocator);

        /// <summary>
        /// Gets the feedback submit button.
        /// </summary>
        public IButton FeedbackSubmitButton => new Button(ContainerLocator, FeedbackSubmitButtonLocator);

        /// <summary>
        /// Gets the dialog title.
        /// </summary>
        public ILabel DialogTitle => new Label(ContainerLocator, DialogTitleLocator);

        /// <summary>
        /// Gets the feedback title text.
        /// </summary>
        public ILabel FeedbackTitleText => new Label(ContainerLocator, FeedbackTitleTextLocator);

        /// <summary>
        /// Gets the feedback warning message.
        /// </summary>
        public ILabel FeedbackWarningMessage => new Label(ContainerLocator, FeedbackWarningMessageLocator);

        /// <summary>
        /// Gets the feedback success message.
        /// </summary>
        public ILabel FeedbackSuccessMessage => new Label(ContainerLocator, FeedbackSuccessMessageLocator);

        /// <summary>
        /// Gets the contact support number text.
        /// </summary>
        public ILabel ContactSupportNumberText => new Label(ContainerLocator, ContactSupportNumberTextLocator);

        /// <summary>
        /// The feedback useful RadioButton
        /// </summary>
        public IRadiobutton FeedbackUsefulRadioButton => new Radiobutton(ContainerLocator, FeedbackUsefulRadioButtonLocator);

        /// <summary>
        /// Gets the feedback error RadioButton.
        /// </summary>
        public IRadiobutton FeedbackErrorRadioButton => new Radiobutton(ContainerLocator, FeedbackErrorRadioButtonLocator);

        /// <summary>
        /// Gets the feedback general RadioButton.
        /// </summary>
        public IRadiobutton FeedbackGeneralRadioButton => new Radiobutton(ContainerLocator, FeedbackGeneralRadioButtonLocator);

        /// <summary>
        /// Gets the feedback survey comment text box.
        /// </summary>
        public ITextbox FeedbackSurveyCommentTextBox => new Textbox(ContainerLocator, FeedbackSurveyCommentTextBoxLocator);
    }
}
