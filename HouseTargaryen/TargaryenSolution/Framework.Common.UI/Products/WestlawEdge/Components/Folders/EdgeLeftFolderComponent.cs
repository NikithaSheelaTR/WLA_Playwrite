namespace Framework.Common.UI.Products.WestlawEdge.Components.Folders
{
    using Framework.Common.UI.Products.Shared.Components.FolderHistory;
    using OpenQA.Selenium;

    /// <summary>
    /// EdgeLeftFolderComponent
    /// </summary>
    public class EdgeLeftFolderComponent : LeftFolderComponent
    {
        private static readonly By TreeRootLocator = By.Id("cobalt_ro_myFolders_folderTree_root");

        /// <summary>
        /// Folder redesign folder tree
        /// </summary>
        public new EdgeFolderTreeComponent FolderTree { get; } = new EdgeFolderTreeComponent(TreeRootLocator);
    }
}