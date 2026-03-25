namespace Framework.Common.UI.Products.Shared.Dialogs.HomePage
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Dialog that pops up when the footer link that says "Improve WestlawNext" is clicked
    /// </summary>
    public class FeedbackDialog : BaseModuleRegressionDialog
    {
        private static readonly By FeedbackTextAreaLocator = By.Id("co_pageSurveyComment");
        private static readonly By SubmitButtonLocator = By.Id("co_pageSurveySubmit");
        private static readonly By SurveyWhatToPreserveCommentTextAreaLocator = By.Id("co_pageSurveyWhatToPreserveComment");
        private static readonly By InfoMessageLocator = By.XPath("./following-sibling::*[contains(@class,'co_feedbackForm_hint')][1]");
        private static readonly By SurveyCommentErrorMessageLocator = By.Id("co_pageSurveyCommentErrorMessage");
        private static readonly By SurveyWhatToPreserveCommentErrorMessageLocator = By.Id("co_pageSurveyWhatToPreserveCommentErrorMessage");

        /// <summary>
        /// Submits the textbox and closes the dialog.
        /// </summary>
        /// <returns> The <see cref="FeedbackDialog"/>. </returns>
        public ImproveWestlawDialog ClickSubmit()
        {
            this.ClickElement(SubmitButtonLocator);
            return new ImproveWestlawDialog();
        }

        /// <summary>
        /// Clears the feedback box and inputs the given text in 'Tell us how to improve your experience on the site' field
        /// </summary>
        /// <param name="feedbackText">
        /// The feedback Text.
        /// </param>
        public void EnterSurveyCommentText(string feedbackText)
            => DriverExtensions.WaitForElement(FeedbackTextAreaLocator).SendKeys(feedbackText);

        /// <summary>
        /// Clears the feedback box and inputs the given text in 'Tell us what works well within Westlaw and should be preserved:' field
        /// </summary>
        /// <param name="feedbackText">
        /// The feedback Text.
        /// </param>
        public void EnterSurveyWhatToPreserveCommentText(string feedbackText)
            => DriverExtensions.WaitForElement(SurveyWhatToPreserveCommentTextAreaLocator).SendKeys(feedbackText);

        /// <summary>
        /// The get survey what to preserve comment text.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetSurveyWhatToPreserveCommentText()
            => DriverExtensions.GetText(SurveyWhatToPreserveCommentTextAreaLocator);

        /// <summary>
        /// The get survey comment text.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetSurveyCommentText()
            => DriverExtensions.GetText(FeedbackTextAreaLocator);

        /// <summary>
        /// The remove one character from survey what to preserve comment.
        /// </summary>
        public void RemoveLastCharacterFromSurveyWhatToPreserveComment() =>
            DriverExtensions.GetElement(SurveyWhatToPreserveCommentTextAreaLocator).SendKeys(Keys.Backspace);

        /// <summary>
        /// The remove one character from survey comment.
        /// </summary>
        public void RemoveLastCharacterFromSurveyComment() =>
            DriverExtensions.GetElement(FeedbackTextAreaLocator).SendKeys(Keys.Backspace);

        /// <summary>
        /// Is info message for Survey Comment displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsInfoMessageForSurveyCommentDisplayed()
            => DriverExtensions.GetElement(FeedbackTextAreaLocator, InfoMessageLocator).Displayed;

        /// <summary>
        /// Is info message for Survey What To Preserve displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsInfoMessageForSurveyWhatToPreserveDisplayed()
            => DriverExtensions.GetElement(SurveyWhatToPreserveCommentTextAreaLocator, InfoMessageLocator).Displayed;

        /// <summary>
        /// Is error message for Survey Comment appears
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsErrorMessageForSurveyCommentAppears()
            => DriverExtensions.IsDisplayed(SurveyCommentErrorMessageLocator);

        /// <summary>
        /// Is error message for Survey What To Preserve appears
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsErrorMessageForSurveyWhatToPreserveAppears()
            => DriverExtensions.IsDisplayed(SurveyWhatToPreserveCommentErrorMessageLocator);

        /// <summary>
        /// The get info message for survey what to preserve comment text.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetInfoMessageForSurveyWhatToPreserveCommentText()
            => DriverExtensions.GetText(SurveyCommentErrorMessageLocator);

        /// <summary>
        /// The get info message for survey comment text.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetInfoMessageForSurveyCommentText()
            => DriverExtensions.GetText(SurveyWhatToPreserveCommentErrorMessageLocator);
    }
}