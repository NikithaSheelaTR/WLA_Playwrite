namespace Framework.Common.UI.Products.WestLawNext.Dialogs
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Info Message Dialog
    /// </summary>
    public class InfoMessageDialog : BaseModuleRegressionDialog
    {
        private static readonly By InfoMessageDialogLocator = By.XPath("//div[contains(@class, '_popupMessageContainer')]");

        private static readonly By UndoDeleteLocator = By.XPath(".//div/span/*[contains(.,'Undo')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="InfoMessageDialog"/> class.
        /// </summary>
        public InfoMessageDialog()           
        {
            DriverExtensions.WaitForElementDisplayed(InfoMessageDialogLocator);
        }

        /// <summary>
        /// Retrieve deleted document
        /// </summary>
        /// <typeparam name="T">Page Object</typeparam>
        /// <returns>The instance of a page</returns>
        public T ClickUndoLink<T>()
            where T : ICreatablePageObject => this.ClickElement<T>(DriverExtensions.GetElement(InfoMessageDialogLocator, UndoDeleteLocator));

        /// <summary>
        /// Gets Folder Action Message
        /// </summary>
        /// <returns>Text of the message</returns>
        public string GetMessageText() => DriverExtensions.GetText(InfoMessageDialogLocator);
    }
}
