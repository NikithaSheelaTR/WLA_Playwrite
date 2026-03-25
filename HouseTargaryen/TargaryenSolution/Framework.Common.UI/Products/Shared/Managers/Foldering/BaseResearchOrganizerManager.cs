namespace Framework.Common.UI.Products.Shared.Managers.Foldering
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Models.GridModels;

    /// <summary>
    /// Base Research Organizer Manager class
    /// </summary>
    public abstract class BaseResearchOrganizerManager
    {
        /// <summary>
        /// Clears Folder grid if exists
        /// </summary>
        /// <param name="folder">folder name</param>
        public abstract void ClearFolder(string folder);

        /// <summary>
        /// Makes a copy of a folder
        /// </summary>
        /// <param name="folderForCopy">Folder, which was copied</param>
        /// <param name="destinationFolder">The place (folder, too) for copy of the folder</param>
        /// <returns>The text of a message</returns>
        public abstract string CopyFolder(string folderForCopy, string destinationFolder);

        /// <summary>
        /// Moves document from folder to another one
        /// </summary>
        /// <param name="gridModel">Grid item model to copy</param>
        /// <param name="sourceFolder">Folder, which was copied</param>
        /// <param name="destinationFolder">The place (folder, too) for copy of the folder</param>
        /// <returns>The text of a message</returns>
        public abstract string CopyFolderItem(FolderGridModel gridModel, string sourceFolder, string destinationFolder);

        /// <summary>
        /// Copies documents to another folder
        /// </summary>
        /// <param name="documentFolder">The place of document (folder)</param>
        /// <param name="destinationFolder">The place (folder, too) for copy of the folder</param>
        /// <param name="lastItemNumber">last Items number</param>
        /// <param name="firstItemNumber">first item number</param>
        /// <returns>The text of a message</returns>
        public abstract string CopyDocumentRangeToFolder(
            string documentFolder,
            string destinationFolder,
            int lastItemNumber,
            int firstItemNumber = 0);

        /// <summary>
        /// Creates new folder
        /// </summary>
        /// <param name="newFolderName">The name of a new folder</param>
        /// <param name="parentFolder">Parent folder</param>
        /// <param name="folderPath">The path of the folder</param>
        public abstract void CreateFolder(string newFolderName, string parentFolder, string folderPath = "");

        /// <summary> Moves document items to another folder</summary>
        /// <param name="gridModel"> Grid item model </param>
        /// <param name="documentFolder">The place of document (folder)</param>
        /// <param name="destinationFolder"> The place for copy of the folder</param>
        /// <returns>The text of a message</returns>
        public abstract string MoveDocumentFromFolderToAnotherOne(FolderGridModel gridModel, string documentFolder, string destinationFolder);

        /// <summary> Moves document items to another folder</summary>
        /// <param name="documentFolder">The place of document (folder)</param>
        /// <param name="destinationFolder">The place (folder, too) for copy of the folder</param>
        /// <param name="lastItemNumber">last Items number</param>
        /// <param name="firstItemNumber">first item number</param>
        /// <returns>The text of a message</returns>
        public abstract string MoveDocumentRangeToFolder(
            string documentFolder,
            string destinationFolder,
            int lastItemNumber,
            int firstItemNumber = 0);

        /// <summary>
        /// Moves a folder
        /// </summary>
        /// <param name="folderToMove">Folder, which was copied</param>
        /// <param name="destinationFolder">The place (folder, too) for copy of the folder</param>
        /// <returns>The text of a message</returns>
        public abstract string MoveFolder(string folderToMove, string destinationFolder);

        /// <summary>
        /// Open the folder in the 'my folders' folder tree view by the given name. If the folder doesn't exist, it gets created.
        /// </summary>
        /// <param name="folder">Folder to open</param>
        public abstract void CreateFolderIfNotExist(string folder);

        /// <summary> Opens a WestlawNext documents directly by guid and saves it to the folder</summary>
        /// <typeparam name="T"> Page Object </typeparam>
        /// <param name="guidList"> List of document GUIDs </param>
        /// <param name="folderName">The name of the folder</param>
        /// <param name="isKm"> True if document is KM</param>
        /// <returns> DocumentPage object </returns>
        public abstract T SaveDocumentListToFolderDirectly<T>(List<string> guidList, string folderName = "", bool isKm = false) where T : ICommonDocumentPage;

        /// <summary> Opens a WestlawNext document directly by guid and saves it to the folder</summary>
        /// <typeparam name="T"> Page Object </typeparam>
        /// <param name="guid"> List of document GUIDs </param>
        /// <param name="folderName">The name of the folder</param>
        /// <param name="isKm"> True if document is KM</param>
        /// <returns> DocumentPage object </returns>
        public abstract T SaveDocumentToFolderDirectly<T>(string guid, string folderName, bool isKm = false) where T : ICommonDocumentPage;
    }
}
