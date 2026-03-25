namespace Framework.Common.UI.Products.Shared.Dialogs.Foldering
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Folder;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// This class models the lightbox that is displayed when the New link is clicked on the Folder page.
    /// </summary>
    public class NewFolderDialog : BaseModuleRegressionDialog
    {
        private static readonly By FolderDialogLocator = By.XPath("//div[@class = 'co_lightboxOverlay']//div[@role = 'dialog'] | //div[@class = 'co_lightboxOverlay']//div[@role = 'document']");

        private static readonly By FolderNameTextboxLocator =
            By.CssSelector("div.co_overlayBox_content form input#cobalt_ro_folder_action_textbox");

        private static readonly By FolderRootLocator =
            By.XPath(
                "//div[@class='co_overlayBox_container co_folderAction co_new_folderAction']//div[@class='co_scrollWrapper co_foldering_tree_scrollWrapper']");

        private static readonly By OkButtonLocator = By.XPath("//button[contains(@class,'co_dropdownBox_ok')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="NewFolderDialog"/> class.
        /// </summary>
        public NewFolderDialog()
        {
            DriverExtensions.WaitForElementDisplayed(FolderDialogLocator);
        }

        /// <summary>
        /// Folder Tree Component 
        /// </summary>
        public virtual FolderTreeComponent FolderTreeComponent { get; } = new FolderTreeComponent(FolderRootLocator);

        /// <summary>
        /// OK button
        /// </summary>
        public IButton OkButton => new Button(OkButtonLocator);

        /// <summary> 
        /// Creates a new folder 
        /// </summary>
        /// <param name="newFoldername"> name for the new folder </param>
        public void CreateNewFolder(string newFoldername)
        {
            this.EnterFolderName(newFoldername);
            this.ClickElement(OkButtonLocator);
        }

        /// <summary>
        /// Method clicks the OK button
        /// </summary>
        /// <param name="newFoldername"> name for the new folder </param>
        /// <param name="parentFolder"> parent folder (default is the root level - if none is provided) </param>
        /// <returns> ResearchOrganizerPage object </returns>
        public void CreateNewFolder(string newFoldername, string parentFolder)
        {
            if (!string.IsNullOrEmpty(parentFolder))
            {
                this.FolderTreeComponent.SelectFolderByName(parentFolder);
            }

            this.CreateNewFolder(newFoldername);
        }

        /// <summary>
        /// Method clicks the OK button
        /// </summary>
        /// <param name="newFoldername"> name for the new folder </param>
        /// <param name="parentFolder"> parent folder (default is the root level - if none is provided) </param>
        /// <param name="folderPath"> folder path </param>
        /// <returns> ResearchOrganizerPage object </returns>
        public void CreateNewFolder(string newFoldername, string parentFolder, string folderPath)
        {
            if (!string.IsNullOrEmpty(folderPath))
            {
                if (!string.IsNullOrEmpty(parentFolder))
                {
                    this.FolderTreeComponent.SelectFolderByPath(folderPath + parentFolder);
                }
            }
            else if (!string.IsNullOrEmpty(parentFolder))
            {
                this.FolderTreeComponent.SelectFolderByName(parentFolder);
            }

            this.CreateNewFolder(newFoldername);
        }

        /// <summary>
        /// Method accepts and enters the folder name in the textbox of the new folder lightbox
        /// </summary>
        /// <param name="folderName"> Folder Name </param>
        public NewFolderDialog EnterFolderName(string folderName)
        {
            DriverExtensions.SetTextField(folderName, FolderNameTextboxLocator);
            return this;
        }
    }
}