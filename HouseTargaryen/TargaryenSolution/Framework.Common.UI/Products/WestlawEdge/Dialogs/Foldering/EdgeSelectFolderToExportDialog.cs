using Framework.Common.UI.Products.Shared.Dialogs;
using Framework.Common.UI.Products.Shared.Dialogs.Foldering;
using Framework.Common.UI.Products.WestlawEdge.Components.Folders;
using OpenQA.Selenium;

namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Foldering
{
    /// <summary>
    /// EdgeSelectFolderToExportDialog
    /// </summary>
    public class EdgeSelectFolderToExportDialog : BaseModuleRegressionDialog
    {
        private static readonly By ClickExportNextButtonLocator = By.Id("coid_deliveryExportNext");

        private static readonly By RootFolderLocator = By.Id("co_foldering_exportFolder_folderData");

        /// <summary>
        /// Folder Tree Component
        /// </summary>
        public EdgeFolderTreeComponent FolderTreeComponent { get; } = new EdgeFolderTreeComponent(RootFolderLocator);

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