namespace Framework.Common.UI.Products.Shared.Dialogs.CustomPages
{
    using Framework.Common.UI.Interfaces;

    using OpenQA.Selenium;

    /// <summary>
    /// Base class for Custom Page sharing widget
    /// </summary>
    public abstract class ShareCustomPageBaseDialog : BaseModuleRegressionDialog
    {
        /// <summary>
        /// Close dialog window button
        /// </summary>
        protected static readonly By CloseDialogButtonLocator = By.Id("co_CustomPagesShareLightBox_Close");

        /// <summary>
        /// Close sharing modal dialog
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The new instance of T page.</returns>
        public T ClickCloseButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(CloseDialogButtonLocator);
    }
}