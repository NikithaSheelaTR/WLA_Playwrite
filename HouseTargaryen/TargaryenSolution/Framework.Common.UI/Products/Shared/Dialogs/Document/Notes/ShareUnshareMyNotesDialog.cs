namespace Framework.Common.UI.Products.Shared.Dialogs.Document.Notes
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Describes dialog window 'Share All My Annotations' and 'Stop Sharing My Annotations'
    /// </summary>
    public class ShareUnshareMyNotesDialog : BaseModuleRegressionDialog
    {
        private const string LightboxHeaderLctMask = "//div[@id='co_sharedNotesLightbox']//h3[text()='{0}'] | //div[@id='co_sharedNotesLightbox']//h2[text()='{0}']";

        private static readonly By CloseLinkLocator = By.XPath("//a[@id='shareCancelButton']//strong[text()='Close']");

        private static readonly By ContinueButtonLocator = By.XPath("//input[@id='co_shareAll' and @value='Continue']");

        private static readonly By InfoMessageLocator = By.XPath("//div[@class='co_infoBox_content']//div");

        private static readonly By StopSharingButtonLocator =
            By.XPath("//input[@id='co_stopSharing' and @value='Stop Sharing']");

        /// <summary>
        /// Click 'Close' button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New Instance of Page Object </returns>
        public T ClickCloseButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(CloseLinkLocator);

        /// <summary>
        /// Click 'Continue' button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New Instance of Page Object </returns>
        public T ClickContinueButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(ContinueButtonLocator);

        /// <summary>
        /// Click 'Stop Sharing' button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New Instance of Page Object </returns>
        public T ClickStopSharingButton<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(StopSharingButtonLocator);

        /// <summary>
        /// Verify 'Close' button is displayed
        /// </summary>
        /// <returns>True if 'Close' button is displayed, false otherwise</returns>
        public bool IsCloseButtonDisplayed() => DriverExtensions.IsDisplayed(CloseLinkLocator, 5);

        /// <summary>
        /// Verify 'Continue' button is displayed
        /// </summary>
        /// <returns> True if 'Continue' button is displayed, false otherwise </returns>
        public bool IsContinueButtonDisplayed() => DriverExtensions.IsDisplayed(ContinueButtonLocator, 5);

        /// <summary>
        /// Verify message in 'Share All My Notes' dialog is displayed
        /// </summary>
        /// <param name="message">Message to compare</param>
        /// <returns> True if message is correct, false otherwise </returns>
        public bool IsMessageDisplayed(string message)
            =>
                DriverExtensions.IsDisplayed(InfoMessageLocator, 5)
                && DriverExtensions.GetText(InfoMessageLocator).Equals(message);

        /// <summary>
        /// Verify 'Share All My Notes' dialog is displayed
        /// </summary>
        /// <param name="header">The header.</param>
        /// <returns> True if dialog is displayed, false otherwise </returns>
        public bool IsNotesDialogDisplayed(string header)
            => DriverExtensions.IsDisplayed(By.XPath(string.Format(LightboxHeaderLctMask, header)), 5);

        /// <summary>
        /// Verify 'Stop Sharing' button is displayed
        /// </summary>
        /// <returns>True if 'Stop Sharing' button is displayed, false otherwise</returns>
        public bool IsStopSharingButtonDisplayed() => DriverExtensions.IsDisplayed(StopSharingButtonLocator, 5);
    }
}