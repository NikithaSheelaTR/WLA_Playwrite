namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Foldering
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Folder;
    using Framework.Common.UI.Products.Shared.Dialogs.Foldering;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Components.Folders;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// EdgeNewFolderDialog
    /// </summary>
    public class EdgeNewFolderDialog : NewFolderDialog
    {
        private static readonly By TreeRootLocator = By.XPath("//div[./div[@class = 'cobalt_ro_matters_folderTree_editingAction']]");
        private static readonly By AddFolderInfoMessageLocator = By.XPath("//div[contains(@class, 'AddFolderInformational')]");
        private static readonly By AddNoteToNewFolderButtonLocator = By.XPath(".//button[@id = 'saveToAddDescLinkId']");
        private static readonly By AddNoteTextFieldLocator = By.XPath(".//textarea[@id = 'saveTo_descriptionWidget']");
        private static readonly By DeleteNoteButtonLocator = By.Id(".//div[@class = 'saveTo_descriptionWidget_delete']");

        /// <summary>
        /// Folder Tree Component 
        /// </summary>
        public override FolderTreeComponent FolderTreeComponent { get; } = new EdgeFolderTreeComponent(TreeRootLocator);

        /// <summary>
        /// Info message that appears when user tries to add new folder as reviewer
        /// </summary>
        public ILabel AddFolderInfoMessage => new Label(AddFolderInfoMessageLocator);

        /// <summary>
        /// Add note to new folder button 
        /// </summary>
        public IButton AddNoteToNewFolderButton => new Button(this.Container, AddNoteToNewFolderButtonLocator);

        /// <summary>
        /// Add Note textbox
        /// </summary>
        public ITextbox AddNoteTextbox => new Textbox(this.Container, AddNoteTextFieldLocator);

        /// <summary>
        /// Delete button
        /// </summary>
        public IButton DeleteNoteButton => new Button(this.Container, DeleteNoteButtonLocator);

        /// <summary>
        /// Click AddNoteToNewFolder link and Enter Text
        /// </summary>
        public void AddNoteToNewFolder(string text)
        {
            this.AddNoteToNewFolderButton.Click<EdgeNewFolderDialog>();
            this.AddNoteTextbox.SetText(text);
        }

        private IWebElement Container => DriverExtensions.WaitForElement(By.XPath(EnumPropertyModelCache.GetMap<Dialogs, WebElementInfo>()[Dialogs.NewFolderDialog].LocatorString));
    }
}
