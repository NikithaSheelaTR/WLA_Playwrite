namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Foldering
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Dialogs.Foldering;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using static System.Windows.Forms.LinkLabel;

    /// <summary>
    /// Edge Save to Folder Widget with Add note to item specific feature 
    /// </summary>
    public class EdgeSaveToFolderWithNoteDialog : EdgeSaveToFolderDialog
    {
        private static readonly By AddNoteToItemLinkLocator = By.Id("saveToAddDescLinkId");
        private static readonly By AddNoteTextFieldLocator = By.Id("saveTo_descriptionWidget");
        private static readonly By DeleteNoteButtonLocator = By.Id("saveTo_descriptionWidget_delete");

        /// <summary>
        /// Add Note To Item link 
        /// </summary>
        public IButton AddNoteToItemLinkButton => new Button(AddNoteToItemLinkLocator);

        /// <summary>
        /// Add Note textbox
        /// </summary>
        public ITextbox AddNoteTextbox => new Textbox(AddNoteTextFieldLocator);

        /// <summary>
        /// Delete button
        /// </summary>
        public IButton DeleteNoteButton => new Button(DeleteNoteButtonLocator);

        /// <summary>
        /// Click AddNoteToItemLink and Enter Text
        /// </summary>
        public void AddNoteToItem(string text)
        {
            this.AddNoteToItemLinkButton.Click<EdgeSaveToFolderWithNoteDialog>();
            this.AddNoteTextbox.SetText(text);
        }
    }
}
