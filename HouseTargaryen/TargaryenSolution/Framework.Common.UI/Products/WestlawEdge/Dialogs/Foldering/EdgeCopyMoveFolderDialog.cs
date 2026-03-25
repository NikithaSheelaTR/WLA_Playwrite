namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Foldering
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Folder;
    using Framework.Common.UI.Products.Shared.Dialogs.Foldering;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.WestlawEdge.Components.Folders;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using OpenQA.Selenium;

    /// <summary>
    /// This class deals with the actions for moving and copying folder across various folder trees
    /// </summary>
    public class EdgeCopyMoveFolderDialog : CopyFolderDialog
    {
        private static readonly By FoldersDialogLocator =
            By.XPath(
                "//div[@id = 'coid_lightboxOverlay' and not(contains(@class, 'co_hideState'))] /div[contains(@class, 'co_folderAction')]");
        private static readonly By MoveButtonLocator = By.XPath("//div[@id='coid_lightboxOverlay']//input[@value='Move'] | //button[contains(@class, 'copyMoveFolder_move')]");
        private static readonly By NewFolderButtonLocator = By.XPath("//button[@class = 'co_saveToNewFolder']");
        private static readonly By AddNoteToFolderButtonLocator = By.XPath("//button[@id = 'saveToAddDescLinkId']");
        private static readonly By AddNoteTextFieldLocator = By.Id("saveTo_descriptionWidget");
        private static readonly By DeleteNoteButtonLocator = By.Id("saveTo_descriptionWidget_delete");
        
        /// <summary>
        /// Folder Tree Component 
        /// </summary>
        public override FolderTreeComponent FolderTreeComponent { get; } = new EdgeFolderTreeComponent(FoldersDialogLocator);
        
        /// <summary>
        /// Move button
        /// </summary>
        public IButton MoveButton => new Button(MoveButtonLocator);

        /// <summary>
        /// New folder button
        /// </summary>
        public IButton NewFolderButton => new Button(NewFolderButtonLocator);

        /// <summary>
        /// Add note to folder button 
        /// </summary>
        public IButton AddNoteToFolderButton => new Button(AddNoteToFolderButtonLocator);

        /// <summary>
        /// Add Note textbox
        /// </summary>
        public ITextbox AddNoteTextbox => new Textbox(AddNoteTextFieldLocator);

        /// <summary>
        /// Delete button
        /// </summary>
        public IButton DeleteNoteButton => new Button(DeleteNoteButtonLocator);

        /// <summary>
        /// Click AddNoteToFolder link and Enter Text
        /// </summary>
        public void AddNoteToFolder(string text)
        {
            this.AddNoteToFolderButton.Click<EdgeCopyMoveFolderDialog>();
            this.AddNoteTextbox.SetText(text);
        }

        /// <summary>
        /// Clicks Move link on the Move widget for moving the folder to destination folder
        /// </summary>
        /// <param name="destinationFolderToMove">
        /// destination to move folder to
        /// </param>
        public EdgeResearchOrganizerPage MoveFolder(string destinationFolderToMove)
        {
            this.FolderTreeComponent.SelectFolderByName(destinationFolderToMove);
            return this.MoveButton.Click<EdgeResearchOrganizerPage>();
        }
    }
}