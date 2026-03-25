namespace Framework.Common.UI.Products.WestLawNext.Pages
{
    using System.Linq;
    using System.Threading;
    using Framework.Common.UI.Enums;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Concourse.Pages;
    using Framework.Common.UI.Products.Shared.Components.Folder;
    using Framework.Common.UI.Products.Shared.Components.FolderHistory;
    using Framework.Common.UI.Products.Shared.Components.Toolbar;
    using Framework.Common.UI.Products.Shared.Dialogs.Foldering;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Enums.Foldering;
    using Framework.Common.UI.Products.Shared.Models.GridModels;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Dialogs;
    using Framework.Common.UI.Products.WestLawNext.Dialogs.Header;
    using Framework.Common.UI.Products.WestLawNext.Enums.Toolbars;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils;

    using OpenQA.Selenium;

    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Utils.Core;

    /// <summary>
    /// Folder Page
    /// </summary>
    public class ResearchOrganizerPage : CommonAuthenticatedWestlawNextPage, IResearchOrganizerPage
    {
        private const string DocumentCheckboxLctMask = "//a[text()={0}]/parent::div/parent::td/preceding-sibling::td//input";

        private const string FolderTreeElementLctMask = "//div[@id='co_researchFolderTree']//div[contains(@class,'co_tree_element') or contains(@class,'TreeButton')]//*[text()={0}]";

        private const string ItemElementLctMask = "//tbody//tr/td[@class='co_detailsTable_content']//a[contains(.,{0})]";

        private static readonly By CommonErrorMessageLocator = By.XPath("//div[@id='coid_lightboxOverlay']//div[contains(@id,'error')]");

        private static readonly By ConcourseLinkLocator = By.XPath("//a[text()='Concourse Matter Room']");

        private static readonly By DeleteDocumentButtonLocator = By.Id("cobalt_ro_detail_trash");

        private static readonly By FoldersLinkLocator = By.ClassName("cobalt_foldering_ro_folderlink");

        private static readonly By MattersFoldersLocator = By.XPath("//span[@class='TreeViewItemName']");

        private static readonly By MessageHourlyChargesSuspendedLocator = By.XPath("//div[@id='co_hourlyBillingSuspendedMessage' and contains(., 'Hourly charges are suspended while on this page.')]");

        private static readonly By MessageOperationOnItemsCompletedLocator =
            By.XPath("//div[contains(@class,'co_foldering_popupMessageContainer')]//div[@class='co_infoBox_message']");

        private static readonly By UploadIconLocator = By.CssSelector("a.co_uploadFile_icon");

        private static readonly By HighQButtonLocator = By.XPath("//button[@id='coid_foldersHighQLogo']");

        private static readonly By HighQInfoboxContainerLocator = By.XPath("//div[contains(@class,'HighQ')]//*[@class='co_infoBox_inner']");

        /// <summary>
        /// Gets and sets folder analysis section (appeares as Smart Folders)
        /// </summary>
        public FolderAnalysisRightPaneComponent FolderAnalysisRightPane { get; } = new FolderAnalysisRightPaneComponent();

        /// <summary>
        /// Gets and sets center research section (overrides the ResearchOrganizer page)
        /// </summary>
        public FolderGridComponent FolderGrid { get; } = new FolderGridComponent();

        /// <summary>
        /// Gets and sets Left research organizer section (options Component is on the left side of the ResearchOrganizer page)
        /// </summary>
        public LeftFolderComponent LeftFolder { get; } = new LeftFolderComponent();

        /// <summary>
        /// Toolbar component
        /// </summary>
        public Toolbar Toolbar { get; set; } = new Toolbar();

        /// <summary>
        /// HighQ logo button locator
        /// </summary>
        public IButton HighQButton => new Button(HighQButtonLocator);

        /// <summary>
        /// HighQ logo infobox 
        /// </summary>
        public IInfoBoxWithLink HighQInfoBox => new InfoBoxWithLink(HighQInfoboxContainerLocator);

        /// <summary>
        /// selects all checkboxes and clicks delete icon on the Folder Grid
        /// </summary>
        public void ClearFolderGrid()
        {
            this.FolderGrid.ClickSelectAllCheckBox();
            this.Toolbar.ClickToolbarElement(ToolbarElements.Delete);
        }

        /// <summary>
        /// Clicks the concourse link on matter pages.
        /// </summary>
        /// <returns>The <see cref="ConcourseHomePage"/>.</returns>
        public ConcourseHomePage ClickConcourseLink()
        {
            DriverExtensions.WaitForElement(ConcourseLinkLocator).Click();
            return new ConcourseHomePage();
        }

        /// <summary>
        /// This method waits for the New link found in the left pane
        /// and allows the user to click the New link to bring up the 
        /// New Folder Dialog
        /// </summary>
        /// <param name="folderName"> Folder name </param>
        public virtual void CreateNewFolder(string folderName)
        {
            NewFolderDialog folderDialog = this.LeftFolder.ClickNewFolderButton();
            folderDialog.EnterFolderName(folderName);
            folderDialog.OkButton.Click<ResearchOrganizerPage>();
        }

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
        public string DragAndDropGridItemToFolderingTree(
            string sourceItemName,
            string destinationFolderName,
            bool isCopy,
            string destinationFolderPath = "")
        {
            if (string.IsNullOrEmpty(destinationFolderPath))
            {
                this.LeftFolder.FolderTree.ExpandFolderTree(destinationFolderName);
            }
            else
            {
                this.LeftFolder.FolderTree.ExpandFolderTreeByPath(destinationFolderPath);
            }

            DriverExtensions.DragAndDropWithoutWaitTime(
                DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(FolderTreeElementLctMask, destinationFolderName)),
                DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(ItemElementLctMask, sourceItemName)));

            if (DriverExtensions.IsElementPresent(MessageOperationOnItemsCompletedLocator, 5000))
            {
                return DriverExtensions.WaitForElementDisplayed(MessageOperationOnItemsCompletedLocator).Text;
            }
            if (this.IsDisplayed(Dialogs.DragAndDropMoveOrCopyDialog))
            {
                var copyOrMoveDialog = new DragAndDropMoveOrCopyDialog();
                if (isCopy)
                {
                    copyOrMoveDialog.ClickCopyButton(false);
                }
                else
                {
                    copyOrMoveDialog.ClickMoveButton(false);
                }
            }

            return DriverExtensions.WaitForElementDisplayed(MessageOperationOnItemsCompletedLocator, 180000).Text;
        }

        /// <summary>
        /// Checks if the concourse link exists.
        /// </summary>
        /// <returns> True if link is displayed, false otherwise. </returns>
        public bool IsConcourseLinkDisplayed() => DriverExtensions.IsDisplayed(ConcourseLinkLocator, 5);

        /// <summary>
        /// Checks for if matter folders appeared.
        /// </summary>
        /// <returns>If the matters folders displayed within the timeout period.</returns>
        public bool IsMatterFolderDisplayed() => DriverExtensions.IsDisplayed(MattersFoldersLocator, 5);

        /// <summary>
        /// Checks whether the message of "Hourly charges are suspended while on this page" is displayed
        /// </summary>
        /// <returns>
        /// display status
        /// </returns>
        public bool IsMessageHourlyChargesDisplayed() =>
            DriverExtensions.IsElementPresent(MessageHourlyChargesSuspendedLocator)
            && DriverExtensions.IsDisplayed(MessageHourlyChargesSuspendedLocator, 5);

        /// <summary>
        /// Checks if the upload file icon is on the screen after waiting for it to load.
        /// Allows some wait time, because matters folders can be really slow.
        /// </summary>
        /// <returns>If the icon is on the screen.</returns>
        public bool IsUploadIconDisplayed() => DriverExtensions.IsDisplayed(UploadIconLocator, 5);

        #region Methods from Website Mobile

        /// <summary>
        /// Deletes the given document
        /// </summary>
        /// <param name="document"> Document to delete </param>
        public void DeleteDocment(string document)
        {
            DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(DocumentCheckboxLctMask, document)).Click();
            DriverExtensions.WaitForElement(DeleteDocumentButtonLocator).Click();
        }

        /// <summary>
        /// Determines whether the specified folder exists in the current active folder
        /// </summary>
        /// <param name="folderName">The name of the folder to find</param>
        /// <returns>True if the folder exist</returns>
        public bool IsFolderExist(string folderName)
            => DriverExtensions.GetElements(FoldersLinkLocator).Any(folder => folder.Text == folderName);
        #endregion

        /// <summary>
        /// Returns the common error message
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetCommonErrorMessage() => DriverExtensions.GetText(CommonErrorMessageLocator);

        /// <summary>
        /// Save document to another folder (needed for move and copy methods)
        /// </summary>
        /// <param name="gridModel"> Grid item model </param>
        /// <param name="documentFolder">The place of document (folder)</param>
        /// <returns>The <see cref="SaveToFolderDialog"/></returns>
        public virtual SaveToFolderDialog SelectGridItemAndOpenSaveToFolderDialog(FolderGridModel gridModel, string documentFolder)
        {
            this.LeftFolder.FolderTree.SelectFolderByName(documentFolder);
            this.FolderGrid.SelectItemByName(gridModel.Title);
            return this.Toolbar.ClickToolbarElement<SaveToFolderDialog>(ToolbarElements.SaveToFolder);
        }

        /// <summary>
        /// Saves document range to folder (needed for copy and move methods)
        /// </summary>
        /// <param name="documentFolder">The place of document (folder)</param>
        /// <param name="lastItemNumber">last Items number</param>
        /// <param name="firstItemNumber">first item number</param>
        /// <returns>The <see cref="SaveToFolderDialog"/></returns>
        public virtual SaveToFolderDialog SelectGridItemRangeAndOpenSaveToFolderDialog(string documentFolder, int lastItemNumber, int firstItemNumber)
        {
            this.LeftFolder.FolderTree.SelectFolderByName(documentFolder);
            this.FolderGrid.SelectGridItemsRange(lastItemNumber, firstItemNumber);
            return this.Toolbar.ClickToolbarElement<SaveToFolderDialog>(ToolbarElements.SaveToFolder);
        }

        /// <summary>
        /// Copy or Move an item to a folder in the RecentFoldersDialog using Drag and Drop
        /// </summary>
        /// <param name="targetFolder">The name of Target Folder.</param>
        /// <param name="gridItemTitle">The element to drag.</param>
        /// <param name="copyOrMoveEnum">The drag And Drop Enum.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public virtual string DragAndDropGridItemToRecentFolder(
            string targetFolder,
            string gridItemTitle,
            CopyOrMoveEnum copyOrMoveEnum = CopyOrMoveEnum.Copy)
        {
            DriverExtensions.DragAndHold(
                this.Header.GetFoldersLinkElement(),
                DriverExtensions.WaitForElement(By.PartialLinkText(gridItemTitle)));

            this.DragAndDropToFolder(
                new RecentFoldersDialog().GetFolderElement(targetFolder),
                DriverExtensions.WaitForElement(By.PartialLinkText(gridItemTitle)),
                copyOrMoveEnum);

            return this.Header.GetInfoMessage(wait:true);
        }

        /// <summary>
        /// Delete a folder if it exists
        /// </summary>
        public CommonSearchHomePage DeleteFolderIfExists(string folderName)
        {
            if (this.LeftFolder.FolderTree.IsFolderExist(folderName))
            {
                this.LeftFolder.FolderTree.SelectFolderByName(folderName);
                var deleteFolderModal = this.LeftFolder.Options.SelectOption<DeleteFolderDialog>(FolderOptions.Delete);
                deleteFolderModal.DeleteFolder();
                Logger.LogInfo("The following folder has been deleted:" + folderName);
            }

            return this.Header.ClickLogo<CommonSearchHomePage>();
        }
    }
}