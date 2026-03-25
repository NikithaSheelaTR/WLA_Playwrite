namespace Framework.Common.UI.Products.Shared.Managers.Foldering
{
    using System.Collections.Generic;
    using System.Threading;
    using Framework.Common.UI.Products.Shared.Dialogs.Foldering;
    using Framework.Common.UI.Products.Shared.Enums.Foldering;
    using Framework.Common.UI.Products.Shared.Models.GridModels;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Products.WestLawNext.Enums.Toolbars;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.DataModel.Security;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils;

    /// <summary>
    /// Research Organizer Manager
    /// </summary>
    public class WestlawResearchOrganizerManager : BaseResearchOrganizerManager
    {
        private static WestlawResearchOrganizerManager instance;

        private WestlawResearchOrganizerManager()
        {
        }

        /// <summary>
        /// Returns instance instance
        /// </summary>
        /// <returns> The <see cref="EdgeNavigationManager"/>. </returns>
        public static WestlawResearchOrganizerManager Instance => instance ?? (instance = new WestlawResearchOrganizerManager());

        private static ResearchOrganizerPage ResearchOrganizerPage => new ResearchOrganizerPage();

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
            ResearchOrganizerPage.LeftFolder.Options.SelectOption<CopyFolderDialog>(FolderOptions.Copy)
                  .CopyFolder<ResearchOrganizerPage>(destinationFolder);
            Thread.Sleep(4000); // wait for the notification message to appear
            return ResearchOrganizerPage.FolderGrid.GetNotificationMessage();
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
            return ResearchOrganizerPage.SelectGridItemAndOpenSaveToFolderDialog(gridModel, sourceFolder).CopyToFolder<ResearchOrganizerPage>(destinationFolder).Header.GetInfoMessage();
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
                                        .CopyToFolder<ResearchOrganizerPage>(destinationFolder).Header.GetInfoMessage(wait: true, timeOut: 80000);
        }

        /// <summary>
        /// Creates new folder
        /// </summary>
        /// <param name="newFolderName">The name of a new folder</param>
        /// <param name="parentFolder">Parent folder</param>
        /// <param name="folderPath">The path of the folder</param>
        public override void CreateFolder(string newFolderName, string parentFolder, string folderPath = "")
        {
            Logger.LogDebug($"Click 'New' button and create {newFolderName} folder in {parentFolder}.");
            ResearchOrganizerPage.LeftFolder.ClickNewFolderButton().CreateNewFolder(newFolderName, parentFolder, folderPath);
        }

        /// <summary> Moves document item to another folder</summary>
        /// <param name="gridModel">Grid item model to move</param>
        /// <param name="documentFolder">The place of document (folder)</param>
        /// <param name="destinationFolder">The place (folder, too) for copy of the folder</param>
        /// <returns>The text of a message</returns>
        public override string MoveDocumentFromFolderToAnotherOne(FolderGridModel gridModel, string documentFolder, string destinationFolder)
        {
            Logger.LogDebug($"Move document from {documentFolder} folder.");
            return ResearchOrganizerPage.SelectGridItemAndOpenSaveToFolderDialog(gridModel, documentFolder).MoveToFolder<ResearchOrganizerPage>(destinationFolder).Header.GetInfoMessage();
        }

        /// <summary>
        /// Moves documents to another folder
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
                                        .MoveToFolder<ResearchOrganizerPage>(destinationFolder).Header.GetInfoMessage();
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
            ResearchOrganizerPage.LeftFolder.FolderTree.SelectFolderByName(folderToMove);
            ResearchOrganizerPage.LeftFolder.Options.SelectOption<MoveFolderDialog>(FolderOptions.Move)
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
                ResearchOrganizerPage.CreateNewFolder(folder);
            }
        }

        /// <summary> Opens a WestlawNext documents directly by guid and saves it to folder</summary>
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

        /// <summary> Opens a WestlawNext document directly by guid and saves it to the folder</summary>
        /// <typeparam name="T"> Page Object </typeparam>
        /// <param name="guid"> List of document GUIDs </param>
        /// <param name="folderName">The name of the folder</param>
        /// <param name="isKm"> True if document is KM</param>
        /// <returns> DocumentPage object </returns>
        public override T SaveDocumentToFolderDirectly<T>(string guid, string folderName, bool isKm = false)
        {
            Logger.LogDebug($"Navigate to document (GUID - {guid}) and save it to {folderName} folder.");
            return WestlawNavigationManager.Instance.NavigateToDocumentDirectly<CommonDocumentPage>(guid, isKm).Toolbar.ClickToolbarElement<SaveToFolderDialog>(ToolbarElements.SaveToFolder).SaveToFolder<T>(folderName);
        }

        /// <summary> Deletes a folder if it exists</summary>
        /// <param name="folderName">The name of the folder</param>
        /// <returns> DocumentPage object </returns>
        public CommonSearchHomePage DeleteFolderIfExists(string folderName) => ResearchOrganizerPage.DeleteFolderIfExists(folderName);
    }
}
