namespace Framework.Common.UI.Products.Shared.Dialogs.Dockets
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// The Update Requests dialog that appears when clicking the Update button for a docket
    /// </summary>
    public class BaseDocketUpdateDialog : BaseModuleRegressionDialog
    {
        private static readonly By LoadingSpinnerLocator = By.Id("co_docketUpdateWaitProgress");

        private static readonly By MessageInfoBoxLocator = By.ClassName("co_infoBox");

        private static readonly By MessageInfoBoxMessageLocator = By.ClassName("co_infoBox_message");

        private static readonly By UpdateRequestsDialogLocator = By.Id("co_docketsWaitDialog");

        /// <summary>
        /// Gets the message text of the info box after the update completes.
        /// </summary>
        /// <returns> Message after update </returns>
        public string GetUpdateCompleteMessage()
            =>
                DriverExtensions.GetText(
                    new ByChained(UpdateRequestsDialogLocator, MessageInfoBoxLocator, MessageInfoBoxMessageLocator));

        /// <summary>
        /// Determines if the docket update completed successfully
        /// </summary>
        /// <returns> True if successful, false otherwise </returns>
        public bool IsDocketUpdateSuccessful()
            =>
                DriverExtensions.GetAttribute(
                                    "class",
                                    new ByChained(UpdateRequestsDialogLocator, MessageInfoBoxLocator))
                                .Contains("success");

        /// <summary>
        /// Waits for the dockets update to complete
        /// </summary>
        /// <param name="timeOut"> Time Out </param>
        /// <typeparam name="T"> T type </typeparam>
        /// <returns> T page </returns>
        public T WaitForUpdateComplete<T>(int timeOut) where T : ICreatablePageObject
            => this.WaitForUpdateComplete<T>(timeOut, LoadingSpinnerLocator);
    }
}