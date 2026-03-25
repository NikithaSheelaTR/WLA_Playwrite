namespace Framework.Common.UI.Products.Shared.Dialogs.Foldering
{
    using System;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Folder;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// New Save to Folder Widget
    /// </summary>
    public class SaveToFolderDialog : BaseModuleRegressionDialog
    {
        private static readonly By CancelButtonLocator = By.CssSelector("a.co_dropdownBox_cancel");

        private static readonly By CopyButtonLocator = By.XPath("//div[@class='co_lightboxOverlay']//input[contains(@class, 'co_saveToDoCopy')]");

        private static readonly By MoveButtonLocator = By.XPath("//div[@class='co_lightboxOverlay']//input[contains(@class, 'co_saveToDoMove')]");

        private static readonly By NewFolderLinkLocator = By.XPath("//div[@class = 'co_lightboxOverlay']//a[@class = 'co_saveToNewFolder']");

        private static readonly By SaveButtonLocator = By.XPath("//div[contains(@class, 'co_lightboxOverlay')]//input[contains(@class, 'co_saveToDoSave')]");

        private static readonly By SaveToFolderDialogLocator =
            By.XPath(
                "//div[@id = 'coid_lightboxOverlay' and not(contains(@class, 'co_hideState'))] /div[contains(@class, 'co_folderAction')]");
        
        /// <summary>
        /// Gets. The folder tree component.
        /// </summary>
        public virtual FolderTreeComponent FolderTreeComponent { get; } = new FolderTreeComponent(SaveToFolderDialogLocator);

        /// <summary>
        /// Click on copy folder button
        /// </summary>
        /// <typeparam name="T">
        /// Page instance
        /// </typeparam>
        /// <returns>
        /// PageInstance
        /// </returns>
        public T ClickCopyButton<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(CopyButtonLocator);

        /// <summary>
        /// Click on move folder button
        /// </summary>
        /// <typeparam name="T">
        /// Page instance
        /// </typeparam>
        /// <returns>
        /// PageInstance
        /// </returns>
        public T ClickMoveButton<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(MoveButtonLocator);

        /// <summary>
        /// Click on new folder button
        /// </summary>
        /// <typeparam name="T">
        /// Page instance
        /// </typeparam>
        /// <returns>
        /// PageInstance
        /// </returns>
        public T ClickNewFolderButton<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(NewFolderLinkLocator);

        /// <summary>
        /// Click Save Button
        /// </summary>
        /// <typeparam name="T">
        /// Page instance
        /// </typeparam>
        /// <returns>
        /// PageInstance
        /// </returns>
        public T ClickSaveButton<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(SaveButtonLocator);


        /// <summary>
        /// Copies folder and returns action message
        /// </summary>
        /// <param name="folderName">
        /// folder name
        /// </param>
        /// <typeparam name="T">
        /// Page instance
        /// </typeparam>
        /// <returns>
        /// The page.
        /// </returns>
        public T CopyToFolder<T>(string folderName) where T : ICreatablePageObject
        {
            this.FolderTreeComponent.SelectFolderByName(folderName);
            return this.ClickCopyButton<T>();
        }

        /// <summary>
        /// Copies folder and returns action message
        /// </summary>
        /// <param name="folderName">
        /// folder name
        /// </param>
        /// <typeparam name="T">
        /// Page instance
        /// </typeparam>
        /// <returns>
        /// The page.
        /// </returns>
        public T MoveToFolder<T>(string folderName) where T : ICreatablePageObject
        {
            this.FolderTreeComponent.SelectFolderByName(folderName);
            return this.ClickMoveButton<T>();
        }

        /// <summary>
        /// Check to see if Save button is disabled
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSaveButtonDisabled() => this.IsButtonDisabled(SaveButtonLocator);

        /// <summary>
        /// Check to see if Save to Copy button is disabled
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSaveToCopyButtonDisabled() => this.IsButtonDisabled(CopyButtonLocator);

        /// <summary>
        /// Check to see if Save to Move button is disabled
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSaveToMoveButtonDisabled() => this.IsButtonDisabled(MoveButtonLocator);

        /// <summary>
        /// Press the cancel button
        /// </summary>
        public void PressCancelButton() 
            => DriverExtensions.GetElement(SaveToFolderDialogLocator, CancelButtonLocator).Click();

        /// <summary>
        /// Selects a folder from the folder tree and press OK to save
        /// </summary>
        /// <typeparam name="T">instance</typeparam>
        /// <param name="foldername">The <see cref="string"/></param>
        /// <returns>Page instance</returns>
        public T SaveToFolder<T>(string foldername) where T : ICreatablePageObject
        {
            this.SaveToFolder(foldername);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Timely variant for BLT Tests
        /// </summary>
        /// <param name="foldername">The <see cref="string"/></param>
        public void SaveToFolder(string foldername)
        {
            this.FolderTreeComponent.SelectFolderByName(foldername);
            DriverExtensions.WaitForElement(SaveButtonLocator).Click();
        }

        private bool IsButtonDisabled(By buttonLocator) =>
             DriverExtensions.WaitForElement(buttonLocator).GetAttribute("class").Contains("disable", StringComparison.InvariantCultureIgnoreCase);
    }
}