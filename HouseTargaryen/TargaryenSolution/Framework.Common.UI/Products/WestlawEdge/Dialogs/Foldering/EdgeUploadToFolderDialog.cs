namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Foldering
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Items.Folders;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Upload to folder dialog
    /// </summary>
    public class EdgeUploadToFolderDialog : BaseModuleRegressionDialog
    {
        private static readonly By ChooseFileButtonLocator = By.Id("UploadModalFileUpload");
        private static readonly By UploadButtonLocator = By.XPath(".//input[@value = 'Upload']");
        private static readonly By CloseButtonLocator = By.XPath(".//input[@value = 'Close']");
        private static readonly By AddNoteButtonLocator = By.XPath(".//button[@class = 'FileListItemAddNoteButton']");
        private static readonly By ItemsLocator = By.XPath(".//div[@class = 'FileListItem']");

        /// <summary>
        /// Choose file button
        /// </summary>
        public IButton ChooseFileButton => new Button(ChooseFileButtonLocator);

        /// <summary>
        /// Upload button
        /// </summary>
        public IButton UploadButton => new Button(this.Container, UploadButtonLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(this.Container, CloseButtonLocator);

        /// <summary>
        /// Add note button
        /// </summary>
        public IButton AddNoteButton => new Button(this.Container, AddNoteButtonLocator);

        /// <summary>
        ///  Get list of uploaded items
        /// </summary>
        /// <returns>list of uploaded items</returns>
        public ItemsCollection<UploadedItem> UploadItems => new ItemsCollection<UploadedItem>(this.Container, ItemsLocator);
        
        private IWebElement Container =>
            DriverExtensions.WaitForElement(
                By.XPath(
                    EnumPropertyModelCache.GetMap<Dialogs, WebElementInfo>()[Dialogs.UploadToFolder].LocatorString));
    }
}
