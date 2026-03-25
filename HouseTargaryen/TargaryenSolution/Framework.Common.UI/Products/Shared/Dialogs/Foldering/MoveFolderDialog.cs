namespace Framework.Common.UI.Products.Shared.Dialogs.Foldering
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Folder;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// This class deals with the actions for moving folder across various folder trees
    /// </summary>
    public class MoveFolderDialog : BaseModuleRegressionDialog
    {
        private static readonly By ConfirmationMessageLocator =
            By.XPath("//div[(@class='co_infoBox_message') and (contains(text(),'moved to')) or (contains(text(),'été déplacé'))]");

        private static readonly By FoldersOnModalLocator =
            By.XPath(
                "//div[@id='coid_lightboxOverlay' and not(contains(@class, 'co_hideState'))]/div[contains(@class, 'co_folderAction')]");

        private static readonly By MoveButtonLocator = By.XPath("//div[@id='coid_lightboxOverlay']//input[@value='Move'] | //button[contains(@class, 'copyMoveFolder_move')] | //button[text()='Move' or text()='Déplacer']");

        private static readonly By MoveDialogTitleLocator =
            By.XPath("//div[@id='coid_lightboxOverlay']//h2[contains(.,'Move ') or contains(.,'Déplacer')] | //div[@id='coid_lightboxOverlay']//h2[contains(.,'Move ')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="MoveFolderDialog"/> class. 
        /// construct the MoveToFolder Widget
        /// </summary>
        public MoveFolderDialog()
        {
            DriverExtensions.WaitForElement(MoveDialogTitleLocator);
        }

        /// <summary>
        /// Folder Tree Component
        /// </summary>
        public FolderTreeComponent FolderTreeComponent { get; } = new FolderTreeComponent(FoldersOnModalLocator);

        /// <summary>
        /// Move Button
        /// </summary>
        public IButton MoveButton => new Button(MoveButtonLocator);

        /// <summary>
        /// Clicks Move link on the Move widget for moving the folder to destination folder
        /// </summary>
        /// <param name="destinationFolderToMove">
        /// destination to move folder to
        /// </param>
        public void MoveFolder(string destinationFolderToMove)
        {
            this.FolderTreeComponent.SelectFolderByName(destinationFolderToMove);
            this.MoveSelectedFolder();
        }

        /// <summary>
        /// Clicks Move link on the Move widget for moving the folder to destination folder
        /// </summary>
        /// <param name="destinationFolderPath">
        /// destination to move folder to
        /// </param>
        public void MoveToFolderByPath(string destinationFolderPath)
        {
            this.FolderTreeComponent.SelectFolderByPath(destinationFolderPath);
            this.MoveSelectedFolder();
        }

        /// <summary>
        /// Clicks Move link and waits for the confirmation message
        /// </summary>
        private void MoveSelectedFolder()
        {
            this.MoveButton.Click();
            DriverExtensions.WaitForElementDisplayed(ConfirmationMessageLocator);
        }
    }
}