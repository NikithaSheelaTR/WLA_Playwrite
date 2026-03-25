namespace Framework.Common.UI.Products.Shared.Managers.Foldering
{
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Dialogs.Contacts;
    using Framework.Common.UI.Products.Shared.Dialogs.Foldering;
    using Framework.Common.UI.Products.Shared.Models.GridModels;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Foldering;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Folders;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Toolbars;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.DataModel.Security;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils;

    /// <summary>
    /// Indigo Research Organizer Manager class.
    /// </summary>
    public class EdgeResearchOrganizerManager : BaseResearchOrganizerManager
    {
        private static EdgeResearchOrganizerManager instance;

        private EdgeResearchOrganizerManager()
        {
        }

        /// <summary>
        /// Returns instance instance
        /// </summary>
        /// <returns> The <see cref="EdgeNavigationManager"/>. </returns>
        public static EdgeResearchOrganizerManager Instance => instance ?? (instance = new EdgeResearchOrganizerManager());

        private static EdgeResearchOrganizerPage ResearchOrganizerPage => new EdgeResearchOrganizerPage();

        /// <summary>
        /// Clears Folder grid if exists
        /// </summary>
        /// <param name="folder">folder name</param>
        public override void ClearFolder(string folder)
        {
            Logger.LogDebug($"Clear folder grid of {folder} folder.");
            ResearchOrganizerPage.LeftFolder.FolderTree.SelectFolderByName(folder);
            ResearchOrganizerPage.ClearFolderGrid();
        }

        /// <summary>
        /// Makes a copy of a folder
        /// </summary>
        /// <param name="folderForCopy">Folder, which was copied</param>
        /// <param name="destinationFolder">The place (folder, too) for copy of the folder</param>
        /// <returns>The text of a message</returns>
        public override string CopyFolder(string folderForCopy, string destinationFolder)
        {
            Logger.LogDebug($"Select 'Copy' option from 'Options' dropdown and move the folder to {destinationFolder} folder.");
            ResearchOrganizerPage.LeftFolder.FolderTree.SelectFolderByName(folderForCopy);
            return ResearchOrganizerPage.FolderHeader.ActionsMenu.SelectOption<EdgeCopyMoveFolderDialog>(ActionsMenuOption.CopyMove).CopyFolder<EdgeResearchOrganizerPage>(destinationFolder).FolderGrid.GetNotificationMessage();
        }

        /// <summary>
        /// Moves document from folder to another one
        /// </summary>
        /// <param name="gridModel">Grid item model to copy</param>
        /// <param name="sourceFolder">Folder, which was copied</param>
        /// <param name="destinationFolder">The place (folder, too) for copy of the folder</param>
        /// <returns>The text of a message</returns>
        public override string CopyFolderItem(FolderGridModel gridModel, string sourceFolder, string destinationFolder)
        {
            Logger.LogDebug($"Copy document from {sourceFolder} folder.");
            return ResearchOrganizerPage.SelectGridItemAndOpenSaveToFolderDialog(gridModel, sourceFolder).MoveToFolder<EdgeResearchOrganizerPage>(destinationFolder).Header.GetInfoMessage();
        }

        /// <summary>
        /// Copies documents to another folder
        /// </summary>
        /// <param name="documentFolder">The place of document (folder)</param>
        /// <param name="destinationFolder">The place (folder, too) for copy of the folder</param>
        /// <param name="lastItemNumber">last Items number</param>
        /// <param name="firstItemNumber">first item number</param>
        /// <returns>The text of a message</returns>
        public override string CopyDocumentRangeToFolder(string documentFolder, string destinationFolder, int lastItemNumber, int firstItemNumber = 0)
        {
            Logger.LogDebug($"Copy {lastItemNumber - firstItemNumber} documents range from grid component.");
            return ResearchOrganizerPage.SelectGridItemRangeAndOpenSaveToFolderDialog(documentFolder, lastItemNumber, firstItemNumber)
                                        .CopyToFolder<EdgeResearchOrganizerPage>(destinationFolder).Header.GetInfoMessage();
        }

        /// <summary>
        /// Creates new folder
        /// </summary>
        /// <param name="newFolderName">The name of a new folder</param>
        /// <param name="parentFolder">Parent folder</param>
        /// <param name="folderPath">The path of the folder</param>
        public override void CreateFolder(string newFolderName, string parentFolder, string folderPath = "")
        {
            Logger.LogDebug($"Click 'New' button and create {newFolderName} folder in {parentFolder} folder.");
            ResearchOrganizerPage.EdgeToolbar.ClickToolbarElement<EdgeNewFolderDialog>(EdgeToolbarElements.NewFolder).CreateNewFolder(newFolderName, parentFolder, folderPath);
        }

        /// <summary>
        /// The  "share folder" dialog invoked for the folder that has been shared and the one that has not look and function a bit differend:
        /// (1) if the folder has been shared (aka FolderSharingDialog), there is the "Add" button, that one needs to be clicked to invoke the "Contacts" dialog;
        /// (2) if the folder has not been shared (aka ShareFolderDialog), but one can click the "Contacts" link at once; 
        /// TODO: double-check with the "Concourse" and "Folderer" guys whether or not it is possible to "merge" AddPeopleFolderShareDialog and ShareFolderDialog into a single class? 
        /// </summary>
        /// <param name="folderName">The name of a folder</param>
        /// <returns>The <see cref="ContactsDialog"/>.</returns>
        public ContactsDialog InvokeContactsDialogFromShareFolderDialog(string folderName)
        {
            Logger.LogDebug($"Share {folderName} folder.");
            ResearchOrganizerPage.LeftFolder.FolderTree.SelectFolderByName(folderName);
            return ResearchOrganizerPage.LeftFolder.FolderTree.IsFolderShared(folderName)
                       ? ResearchOrganizerPage.FolderHeader.ActionsMenu
                                   .SelectOption<EdgeFolderSharingDialog>(ActionsMenuOption.FolderSharing)
                                   .ClickAddButton().ClickContactsLink()
                       : ResearchOrganizerPage.FolderHeader .ActionsMenu
                                   .SelectOption<EdgeShareFolderDialog>(ActionsMenuOption.ShareFolder)
                                   .ContactsToggle.ToggleState<ContactsDialog>(true);
        }

        /// <summary>
        /// Move a document to another folder
        /// </summary>
        /// <param name="gridModel">Folder, which was copied</param>
        /// <param name="documentFolder">The place of document (folder)</param>
        /// <param name="destinationFolder">The place (folder, too) for copy of the folder</param>
        /// <returns>The text of a message</returns>
        public override string MoveDocumentFromFolderToAnotherOne(FolderGridModel gridModel, string documentFolder, string destinationFolder)
        {
            Logger.LogDebug($"Move grid item from {documentFolder} folder.");
            return ResearchOrganizerPage.SelectGridItemAndOpenSaveToFolderDialog(gridModel, documentFolder).MoveToFolder<EdgeResearchOrganizerPage>(destinationFolder).Header.GetInfoMessage();
        }

        /// <summary>
        /// Move documents to another folder
        /// </summary>
        /// <param name="documentFolder">The place of document (folder)</param>
        /// <param name="destinationFolder">The place (folder, too) for copy of the folder</param>
        /// <param name="lastItemNumber">last Items number</param>
        /// <param name="firstItemNumber">first item number</param>
        /// <returns>The text of a message</returns>
        public override string MoveDocumentRangeToFolder(string documentFolder, string destinationFolder, int lastItemNumber, int firstItemNumber = 0)
        {
            Logger.LogDebug($"Move {lastItemNumber - firstItemNumber} documents range from grid component.");
            return ResearchOrganizerPage.SelectGridItemRangeAndOpenSaveToFolderDialog(documentFolder, lastItemNumber, firstItemNumber)
                                        .MoveToFolder<EdgeResearchOrganizerPage>(destinationFolder).Header.GetInfoMessage();
        }

        /// <summary>
        /// Moves a folder
        /// </summary>
        /// <param name="folderToMove">Folder, which was copied</param>
        /// <param name="destinationFolder">The place (folder, too) for copy of the folder</param>
        /// <returns>The text of a message</returns>
        public override string MoveFolder(string folderToMove, string destinationFolder)
        {
            Logger.LogDebug($"Select 'Move' option from 'Options' dropdown and move the folder to {destinationFolder} folder.");
            ResearchOrganizerPage.TreepaneExpand();
            ResearchOrganizerPage.LeftFolder.FolderTree.SelectFolderByName(folderToMove);
            ResearchOrganizerPage.FolderHeader.ActionsMenu.SelectOption<EdgeCopyMoveFolderDialog>(ActionsMenuOption.CopyMove)
                      .MoveFolder(destinationFolder);
            return ResearchOrganizerPage.FolderGrid.GetNotificationMessage();
        }

        /// <summary>
        /// Open the folder in the 'my folders' folder tree view by the given name. If the folder doesn't exist, it gets created.
        /// </summary>
        /// <param name="folder">Folder to open</param>
        public override void CreateFolderIfNotExist(string folder)
        {
            if (ResearchOrganizerPage.LeftFolder.FolderTree.IsFolderExist(folder))
            {
                Logger.LogDebug($"Select {folder} folder.");
                ResearchOrganizerPage.LeftFolder.FolderTree.SelectFolderByName(folder);
            }
            else
            {
                Logger.LogDebug($"Create {folder} folder.");
                this.CreateFolder(folder,string.Empty);
            }
        }

        /// <summary> Opens a WestlawNext documents directly by guid and saves it to the folder</summary>
        /// <typeparam name="T"> Page Object </typeparam>
        /// <param name="guidList"> List of document GUIDs </param>
        /// <param name="folderName">The name of the folder</param>
        /// <param name="isKm"> True if document is KM</param>
        /// <returns> DocumentPage object </returns>
        public override T SaveDocumentListToFolderDirectly<T>(List<string> guidList, string folderName = "", bool isKm = false)
        {
            folderName = string.IsNullOrEmpty(folderName) ? $"{CredentialPool.GetFirstOrDefaultUser<WlnUserInfo>().FirstName}'s Research" : folderName;
            guidList.ForEach(guid => this.SaveDocumentToFolderDirectly<T>(guid, folderName, isKm));

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary> Opens a WestlawNext document directly by guid and saves it to root folder</summary>
        /// <typeparam name="T"> Page Object </typeparam>
        /// <param name="guid"> List of document GUIDs </param>
        /// <param name="folderName">The name of the folder</param>
        /// <param name="isKm"> True if document is KM</param>
        /// <returns> DocumentPage object </returns>
        public override T SaveDocumentToFolderDirectly<T>(string guid, string folderName, bool isKm = false)
        {
            Logger.LogDebug($"Navigate to document (GUID - {guid}) and save it to {folderName} folder.");
            return EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(guid, isKm).Toolbar.ClickToolbarElement<EdgeSaveToFolderDialog>(EdgeToolbarElements.SaveToFolder).SaveToFolder<T>(folderName);
        }

        /// <summary> Deletes the folder by name if exists </summary>
        /// <param name="folderName"> Folder name to be deleted </param>
        /// <returns> Home Page </returns>
        public EdgeHomePage DeleteFolderIfPresent(string folderName)
        {
            var edgeResearchOrganizerPage = EdgeNavigationManager.Instance.GoToFoldersPage<EdgeResearchOrganizerPage>();
            if (edgeResearchOrganizerPage.LeftFolder.FolderTree.IsFolderExist(folderName))
            {
                edgeResearchOrganizerPage.LeftFolder.FolderTree.SelectFolderByName(folderName);
                var deleteFolder = edgeResearchOrganizerPage.FolderHeader.ActionsMenu.SelectOption<DeleteFolderDialog>(ActionsMenuOption.Delete);
                deleteFolder.DeleteFolder();
            }

            return edgeResearchOrganizerPage.Header.ClickLogo<EdgeHomePage>();
        }
    }
}
