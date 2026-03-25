namespace Framework.Common.UI.Products.Patron.Dialogs
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Dialog that pops up when the session is either about to time out, or times out
    /// </summary>
    public class PatronSessionTimeoutDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContinueButtonLocator =
            By.XPath("//div[@class='co_overlayBox_optionsBottom']/ul/li/input[@value='Continue']");

        private static readonly By LoginLinkLocator = By.LinkText("Sign on");

        private static readonly By SessionTimeoutContainerLocator = By.Id("co_websiteUltraUserTimeout");

        private static readonly By SessionWarningContainerLocator = By.Id("co_websiteTimeoutWarning");

        /// <summary>
        /// Clicks the link to sign back on found on the session timeout dialog
        /// </summary>
        public void ClickSessionTimeoutSignOnLink()
            => DriverExtensions.Click(DriverExtensions.GetElement(SessionTimeoutContainerLocator), LoginLinkLocator);

        /// <summary>
        /// Dismisses the session warning screen by hitting continue.
        /// </summary>
        public void DismissSessionWarningScreen() => this.ClickElement(ContinueButtonLocator);

        /// <summary>
        /// Waits for the session timeout popup to appear for the time input.
        /// </summary>
        /// <returns>True if the popup appeared, false if timed out waiting</returns>
        public virtual bool WaitForSessionTimeoutScreen(int secondsToWait)
            => DriverExtensions.IsDisplayed(SessionTimeoutContainerLocator, secondsToWait);

        /// <summary>
        /// Waits for the session warning to appear for the time input.
        /// </summary>
        /// <param name="secondsToWait">Seconds to wait for the warning screen to appear</param>
        /// <returns>True if the popup appeared, false if timed out waiting</returns>
        public virtual bool WaitForSessionWarningScreen(int secondsToWait)
            => DriverExtensions.IsDisplayed(SessionWarningContainerLocator, secondsToWait);
    }
}