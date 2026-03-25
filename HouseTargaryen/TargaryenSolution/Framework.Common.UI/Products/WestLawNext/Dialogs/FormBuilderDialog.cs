namespace Framework.Common.UI.Products.WestLawNext.Dialogs
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.Browser;

    using OpenQA.Selenium;

    /// <summary>
    /// Form Builder Dialog
    /// </summary>
    public class FormBuilderDialog : BaseModuleRegressionDialog
    {
        private static readonly By FormBuilderContinueButtonLocator = By.Id("coid_assembleForm_continue");

        /// <summary>
        /// The click form builder continue.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string ClickFormBuilderContinueAndGetUrl()
        {
            this.ClickAndOpenNewBrowserTab<FormBuilderDialog>(FormBuilderContinueButtonLocator, "NewTab");
            return BrowserPool.CurrentBrowser.Url;
        }
    }
}