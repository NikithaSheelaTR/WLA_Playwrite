namespace Framework.Common.UI.Products.Shared.Dialogs.Foldering
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Shows up when you try and add an email to share with that hasn't accepted and invite before
    /// </summary>
    public class FolderSharingExternalInviteDialog : BaseModuleRegressionDialog
    {
        private static readonly By BackButtonLocator = By.Id("co_folderingShareFolderGoBack");

        private static readonly By ContinueButtonLocator = By.Id("co_folderingShareFolderContinue");

        private static readonly By EmailValidationMessageLocator =
            By.XPath(
                "//div[contains(@id,'coid_inviteexternal_error') and not(contains(@class, 'co_hideState'))]//div[@class='co_infoBox_message']");

        private static readonly By InviteResponseEmailTextLocator = By.Id("coid_inviteexternal_email");

        /// <summary>
        /// Initializes a new instance of the <see cref="FolderSharingExternalInviteDialog"/> class.
        /// </summary>
        public FolderSharingExternalInviteDialog()
            
        {
            DriverExtensions.WaitForElement(BackButtonLocator);
        }

        /// <summary>
        /// Clicks the Back button
        /// </summary>
        /// <returns> New instance of the T page </returns>
        public T ClickBackButton<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(BackButtonLocator);

        /// <summary>
        /// Clicks the Continue button
        /// </summary>
        /// <returns> New instance of the T page </returns>
        public T ClickContinueButton<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(ContinueButtonLocator);

        /// <summary>
        /// Clicks the Continue button
        /// </summary>
        /// <returns> New instance of the T page </returns>
        public void ClickContinueButton() => this.ClickElement(ContinueButtonLocator);

        /// <summary>
        /// Gets email address
        /// </summary>
        /// <returns> Email address </returns>
        public string GetEmailAddress() => DriverExtensions.GetText(InviteResponseEmailTextLocator);

        /// <summary>
        /// Gets the email validation message, if there is one
        /// </summary>
        /// <returns>Email validation message or "" if there isn't one</returns>
        public string GetEmailValidationMessage()
            =>
                DriverExtensions.IsElementPresent(EmailValidationMessageLocator)
                    ? DriverExtensions.GetElement(EmailValidationMessageLocator).Text
                    : string.Empty;

        /// <summary>
        /// Set text to the search input
        /// </summary>
        /// <param name="text"> Text for input </param>
        public void SetEmailAddress(string text) => DriverExtensions.SetTextField(text, InviteResponseEmailTextLocator);
    }
}