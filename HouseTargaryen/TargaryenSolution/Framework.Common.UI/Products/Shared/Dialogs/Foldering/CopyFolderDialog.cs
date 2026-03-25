namespace Framework.Common.UI.Products.Shared.Dialogs.Foldering
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Folder;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    using OpenQA.Selenium;

    /// <summary>
    /// This class deals with the actions for copying folder across various folder trees
    /// - Title (GetTitle)
    /// - Folder Tree Component
    /// - Copy Button (Click)
    /// - Cancel Button (Click)
    /// - Close Button (Click)
    /// </summary>
    public class CopyFolderDialog : BaseModuleRegressionDialog
    {
        private static readonly By CancelButtonLocator = By.XPath("//a[contains(@class, 'co_dropdownBox_cancel')] | //button[contains(@class, 'Cancel')]");

        private static readonly By CopyButtonLocator = By.XPath("//*[contains(@class, 'co_dropdownBox_ok')] | //button[contains(@class, 'copyMoveFolder_copy')] | //button[text()='Copy']");

        private static readonly By FoldersDialogLocator =
            By.XPath(
                "//div[@id = 'coid_lightboxOverlay' and not(contains(@class, 'co_hideState'))] /div[contains(@class, 'co_folderAction')]");
        
        /// <summary>
        /// Gets Folder Tree Component 
        /// </summary>
        public virtual FolderTreeComponent FolderTreeComponent { get; } = new FolderTreeComponent(FoldersDialogLocator);

        /// <summary>
        /// Cancel button
        /// </summary>
        public IButton CancelButton => new Button(CancelButtonLocator);

        /// <summary>
        /// Copy button
        /// </summary>
        public IButton CopyButton => new Button(CopyButtonLocator);

        /// <summary>
        /// copies the folder to destination folder
        /// </summary>
        /// <param name="destinationCopyFolderName">
        /// The destination Copy Folder Name.
        /// </param>
        /// <typeparam name="T">Page to return</typeparam>
        /// <returns>Page instance</returns>
        public T CopyFolder<T>(string destinationCopyFolderName) where T : ICreatablePageObject
        {
            this.FolderTreeComponent.SelectFolderByName(destinationCopyFolderName);
            return this.CopyButton.Click<T>();
        }

        /// <summary>
        /// copies the folder to destination folder
        /// </summary>
        /// <param name="destinationCopyFolderPath">
        /// The destination Copy Folder Name.
        /// </param>
        /// <typeparam name="T">Page to return</typeparam>
        /// <returns>Page instance</returns>
        public T CopyToFolderByPath<T>(string destinationCopyFolderPath) where T : ICreatablePageObject
        {
            this.FolderTreeComponent.SelectFolderByPath(destinationCopyFolderPath);
            return this.CopyButton.Click<T>();
        }
    }
}