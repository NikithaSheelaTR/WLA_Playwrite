namespace Framework.Common.UI.Products.Shared.Dialogs.Dockets
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;

    /// <summary>
    /// The Update Requests dialog that appears when clicking the Update button for a docket
    /// </summary>
    public class SingleDocketUpdateRequestsDialog : BaseDocketUpdateDialog
    {
        private static readonly By UpdateProcessingMessageLocator = By.Id("co_docketUpdateWaitMessageContent");

        /// <summary>
        /// Processing update message label
        /// </summary>
        public ILabel UpdateProcessingMessageLabel => new Label(UpdateProcessingMessageLocator);

        /// <summary>
        /// Waits for the dockets update to complete
        /// </summary>
        /// <typeparam name="T"> T type </typeparam>
        /// <returns> T page </returns>
        public T WaitForUpdateComplete<T>() where T : ICreatablePageObject
            => this.WaitForUpdateComplete<T>(200000);
    }
}