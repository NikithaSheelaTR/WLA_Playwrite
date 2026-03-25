namespace Framework.Common.UI.Interfaces
{
    using Framework.Common.UI.Enums;
    using Framework.Common.UI.Products.Concourse.Pages;
    using Framework.Common.UI.Products.Shared.Enums;

    /// <summary>
    /// ResearchOrganizerPage interface
    /// </summary>
    public interface IResearchOrganizerPage : ICreatablePageObject
    {
        /// <summary>
        /// selects all checkboxes and clicks delete icon on the Folder Grid
        /// </summary>
        void ClearFolderGrid();

        /// <summary>
        /// Clicks the concourse link on matter pages.
        /// </summary>
        /// <returns>The <see cref="ConcourseHomePage"/>.</returns>
        ConcourseHomePage ClickConcourseLink();

        /// <summary>
        /// This method waits for the New link found in the left pane
        /// and allows the user to click the New link to bring up the 
        /// New Folder Dialog
        /// </summary>
        /// <param name="folderName"> Folder name </param>
        void CreateNewFolder(string folderName);

        /// <summary>
        /// Drag and Drop from one web element to another 
        /// </summary>
        /// <param name="sourceItemName">
        /// the title of the item from the table that is to be dragged
        /// </param>
        /// <param name="destinationFolderName">
        /// folder name where the item(s) will be dropped at
        /// </param>
        /// <param name="isCopy">
        /// true if the drag and drop is used for Copy action
        /// </param>
        /// <param name="destinationFolderPath">destination Folder PATH</param>
        /// <returns>
        /// The <see cref="string"/> message
        /// </returns>
        string DragAndDropGridItemToFolderingTree(
            string sourceItemName,
            string destinationFolderName,
            bool isCopy,
            string destinationFolderPath);

        /// <summary>
        /// Checks if the concourse link exists.
        /// </summary>
        /// <returns> True if link is displayed, false otherwise. </returns>
        bool IsConcourseLinkDisplayed();

        /// <summary>
        /// Checks for if matter folders appeared.
        /// </summary>
        /// <returns>If the matters folders displayed within the timeout period.</returns>
        bool IsMatterFolderDisplayed();

        /// <summary>
        /// Checks whether the message of "Hourly charges are suspended while on this page" is displayed
        /// </summary>
        /// <returns>
        /// display status
        /// </returns>
        bool IsMessageHourlyChargesDisplayed();

        /// <summary>
        /// Checks if the upload file icon is on the screen after waiting for it to load.
        /// Allows some wait time, because matters folders can be really slow.
        /// </summary>
        /// <returns>If the icon is on the screen.</returns>
        bool IsUploadIconDisplayed();

        /// <summary>
        /// Deletes the given document
        /// </summary>
        /// <param name="document"> Document to delete </param>
        void DeleteDocment(string document);

        /// <summary>
        /// Determines whether the specified folder exists in the current active folder
        /// </summary>
        /// <param name="folderName">The name of the folder to find</param>
        /// <returns>True if the folder exist</returns>
        bool IsFolderExist(string folderName);

        /// <summary>
        /// Returns the common error message
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetCommonErrorMessage();

        /// <summary>
        /// Copy or Move an item to a folder in the RecentFoldersDialog using Drag and Drop
        /// </summary>
        /// <param name="targetFolder">The name of Target Folder.</param>
        /// <param name="gridItemTitle">The element to drag.</param>
        /// <param name="copyOrMoveEnum">The drag And Drop Enum.</param>
        /// <returns>The <see cref="string"/>.</returns>
        string DragAndDropGridItemToRecentFolder(
            string targetFolder,
            string gridItemTitle,
            CopyOrMoveEnum copyOrMoveEnum);
    }
}