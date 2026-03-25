namespace Framework.Common.UI.Products.Shared.Dialogs
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Dialog that pops up when the session is paused (for billing purposes)
    /// </summary>
    public class SessionPauseDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContinueButtonLocator = By.Id("co_suspendBillingContinueButton");

        private static readonly By SessionPauseContainerLocator = By.Id("co_suspendBilling");
        
        /// <summary>
        /// Dismisses the session warning screen by hitting continue.
        /// </summary>
        /// <typeparam name="T">page object</typeparam>
        /// <returns>new page object</returns>
        public T ClickContinueSessionButton<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(ContinueButtonLocator);

        /// <summary>
        /// Waits for the session paused dialog to appear for the time input.
        /// </summary>
        /// <returns>True if the popup appeared, false if timed out waiting</returns>
        public virtual bool WaitForSessionPausedDialog()
        {
            DriverExtensions.ScrollToTop();
            return DriverExtensions.IsDisplayed(SessionPauseContainerLocator, 10);
        }
    }
}