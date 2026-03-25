namespace Framework.Common.UI.Products.Shared.Dialogs.Delivery
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Invalid Page Ranges Dialog (appears, when invalid page ranges symbols are typed)
    /// </summary>
    public class InvalidPageRangesDialog : BaseModuleRegressionDialog
    {
        private static readonly By OkButtonLocator = By.Id("coid_continue_selectionButton");
        private static readonly By DialogMessageTextLocator = By.Id("co_deliveryInvalidStarPagesMessage");

        /// <summary>
        /// Clicks Ok button
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>
        /// The T</returns>
        public T ClickOkButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(OkButtonLocator);

        /// <summary>
        /// Gets a dialog message text.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetDialogMessageText() => DriverExtensions.GetElement(DialogMessageTextLocator).Text;
    }
}