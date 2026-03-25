namespace Framework.Common.UI.Products.WestLawNext.Dialogs
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Move Or Copy Dialog
    /// </summary>
    public class DragAndDropMoveOrCopyDialog : BaseModuleRegressionDialog
    {
        private static readonly By DragnDropCopyOptionLocator = By.Id("co_dragDropBox_button_copy");

        private static readonly By DragnDropCopyOrMoveDialogLocator = By.Id("co_dragDropBox_button_move");

        /// <summary>
        /// ClickCopyButton
        /// </summary>
        public void ClickCopyButton(bool WaitForPageLoad = true)
        {
            if (WaitForPageLoad)
                base.ClickElement(DragnDropCopyOptionLocator);
            else
                this.ClickElement(DragnDropCopyOptionLocator);
        }
        /// <summary>
        /// ClickMoveButton
        /// </summary>
        public void ClickMoveButton(bool WaitForPageLoad = true)
        {
            if (WaitForPageLoad)
                base.ClickElement(DragnDropCopyOrMoveDialogLocator);
            else
                this.ClickElement(DragnDropCopyOrMoveDialogLocator);
        }

        /// <summary>
        /// Click the element and wont wait for page load.
        /// </summary>
        /// <param name="element">The element.</param>
        protected new void ClickElement(By element)
        {
            DriverExtensions.WaitForElementDisplayed(element);
            DriverExtensions.Click(element);
        }
    }
}