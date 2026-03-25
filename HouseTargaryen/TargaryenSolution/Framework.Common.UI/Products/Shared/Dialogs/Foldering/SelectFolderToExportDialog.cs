namespace Framework.Common.UI.Products.Shared.Dialogs.Foldering
{
    using Framework.Common.UI.Products.Shared.Components.Folder;

    using OpenQA.Selenium;

    /// <summary>
    /// ExportWidget page 
    /// </summary>
    public class SelectFolderToExportDialog : BaseModuleRegressionDialog
    {
        private static readonly By ClickExportNextButtonLocator = By.Id("coid_deliveryExportNext");

        private static readonly By RootFolderLocator = By.Id("co_foldering_exportFolder_folderData");

        /// <summary>
        /// Folder Tree Component
        /// </summary>
        public FolderTreeComponent FolderTreeComponent { get; } = new FolderTreeComponent(RootFolderLocator);

        /// <summary>
        /// Clicking Next button on the export widget to proceed for the actual Export
        /// </summary>
        /// <returns>
        /// The <see cref="ExportDialog"/>.
        /// </returns>
        public ExportDialog ClickNextOnExportDialog() =>
            this.ClickElement<ExportDialog>(ClickExportNextButtonLocator);
    }
}