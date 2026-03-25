namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Foldering
{
    using Framework.Common.UI.Products.Shared.Components.Folder;
    using Framework.Common.UI.Products.Shared.Dialogs.Foldering;
    using Framework.Common.UI.Products.WestlawEdge.Components.Folders;
    using OpenQA.Selenium;

    /// <summary>
    /// EdgeSaveToFolderDialog
    /// </summary>
    public class EdgeSaveToFolderDialog : SaveToFolderDialog
    {
        private static readonly By SaveToFolderDialogLocator =
            By.XPath("//div[@id = 'coid_lightboxOverlay' and not(contains(@class, 'co_hideState'))] /div[contains(@class, 'co_folderAction')]");
        
        /// <summary>
        /// Gets The folder tree component.
        /// </summary>
        public override FolderTreeComponent FolderTreeComponent { get; } = new EdgeFolderTreeComponent(SaveToFolderDialogLocator);
    }
}