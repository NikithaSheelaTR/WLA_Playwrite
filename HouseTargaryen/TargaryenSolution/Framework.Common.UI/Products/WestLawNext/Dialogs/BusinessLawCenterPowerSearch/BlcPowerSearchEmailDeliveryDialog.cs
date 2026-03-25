namespace Framework.Common.UI.Products.WestLawNext.Dialogs.BusinessLawCenterPowerSearch
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The email delivery popup.
    /// </summary>
    public class BlcPowerSearchEmailDeliveryDialog : BlcPowerSearchDeliveryDialog
    {
        private static readonly By BasicsDeliveryFormatPdfLocator =
            By.XPath("//div[contains(@class,'co_overlayBox_container')]//div[@class='co_deliveryContentRight']//option[text()='PDF']");

        private static readonly By BasicsDeliveryFormatWordLocator =
            By.XPath("//div[contains(@class,'co_overlayBox_container')]//div[@class='co_deliveryContentRight']//option[text()='Word Processor (RTF)']");

        private static readonly By BasicsDeliveryOptionAsMultipleLocator =
            By.XPath("//div[contains(@class,'co_overlayBox_container')]//div[@class='co_deliveryContentRight']//option[text()='Multiple Files (zip)']");

        private static readonly By EmailAddressLocator = By.Id("co_delivery_emailAddress");

        private static readonly By EmailButtonLocator =
            By.XPath("//div[@ng-hide='isProcessing || showConfirmationMessage']//input[@value='Email' and @role='button']");

        private static readonly By EmailMessageLocator =
            By.XPath("//div[contains(@class,'co_overlayBox_container')]/div[contains(@class, 'co_deliveryWaitMessageItemTitle ng-binding') and contains(text(), 'The item(s) will be emailed.')]");

        private static readonly By EmailNoteLocator = By.Id("co_delivery_note");

        private static readonly By EmailSubjectLocator = By.XPath("//input[contains(@id,'co_delivery_subject')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="BlcPowerSearchEmailDeliveryDialog"/> class.
        /// </summary>
        public BlcPowerSearchEmailDeliveryDialog()
        {
            DriverExtensions.WaitForElement(EmailButtonLocator);
        }

        /// <summary>
        /// ClickOnEmail - Select the Email button in pop up.
        /// </summary>
        public void ClickOnEmail() => DriverExtensions.WaitForElement(EmailButtonLocator).Click();

        /// <summary>
        /// Gets a delivery message
        /// </summary>
        /// <returns>delivery message</returns>
        public bool IsMessageEmailSuccessDislayed()
        {
            bool messageElementDisplayed = DriverExtensions.IsDisplayed(EmailMessageLocator, 5);
            DriverExtensions.WaitForElementNotDisplayed(EmailMessageLocator);
            return messageElementDisplayed;
        } 

        /// <summary>
        /// The set delivery format pdf.
        /// </summary>
        public void SetDeliveryFormatPdf() => DriverExtensions.WaitForElement(BasicsDeliveryFormatPdfLocator).Click();

        /// <summary>
        /// Sets the delivery type to MS Word.
        /// </summary>
        public void SetDeliveryFormatWord() => DriverExtensions.WaitForElement(BasicsDeliveryFormatWordLocator).Click();

        /// <summary>
        /// Sets the delivery type to multiple .zip archive.
        /// </summary>
        public void SetDeliveryTypeToMultiple() => DriverExtensions.WaitForElement(BasicsDeliveryOptionAsMultipleLocator).Click();

        /// <summary>
        /// SetEmailAddress  - Set Email Address.
        /// </summary>
        /// <param name="address">The address.</param>
        public void SetEmailAddress(string address) => DriverExtensions.SetTextField(address, EmailAddressLocator);

        /// <summary>
        /// The set email note.
        /// </summary>
        /// <param name="note">The note.</param>
        public void SetEmailNote(string note) => DriverExtensions.SetTextField(note, EmailNoteLocator);

        /// <summary>
        /// The set email subject.
        /// </summary>
        /// <param name="subject">The subject.</param>
        public void SetEmailSubject(string subject) => DriverExtensions.SetTextField(subject, EmailSubjectLocator);
    }
}