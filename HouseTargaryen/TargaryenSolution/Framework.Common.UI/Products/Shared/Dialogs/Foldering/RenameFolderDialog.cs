namespace Framework.Common.UI.Products.Shared.Dialogs.Foldering
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Rename folder Widget
    /// </summary>
    public class RenameFolderDialog : BaseModuleRegressionDialog
    {
        private static readonly By OkButtonLocator = By.XPath("//div[@id='coid_lightboxOverlay']//*[contains(@class,'co_dropdownBox_ok')]");

        private static readonly By RenameFolderLocator = By.Id("cobalt_ro_folder_action_textbox");

        /// <summary>
        /// Initializes a new instance of the <see cref="RenameFolderDialog"/> class. 
        /// Constructs the Rename folder Widget and waits for the OK button on the widget to load
        /// </summary>
        public RenameFolderDialog()
        {
            DriverExtensions.WaitForElement(OkButtonLocator);
        }

        /// <summary>
        /// Rename the selected folder with given label
        /// Prior to calling this method, folder to rename should be selected
        /// </summary>
        /// <param name="renameFolderText">
        /// The rename Folder Text.
        /// </param>
        /// <typeparam name="T">Page Instance</typeparam>
        /// <returns>Page.</returns>
        public T RenameFolder<T>(string renameFolderText) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(RenameFolderLocator).SetTextField(renameFolderText);
            DriverExtensions.WaitForJavaScript();

            return this.ClickElement<T>(OkButtonLocator);
        }
    }
}