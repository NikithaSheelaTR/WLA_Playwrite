
namespace Framework.Common.UI.Products.Shared.Components.Document
{
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Items.FileHistory;
    using OpenQA.Selenium;

    /// <summary>
    /// The Pdf document table component
    /// </summary>
    public class FileHistoryTableComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//table[@id='pdfDocumentTable']");

        private static readonly By FileHistoryTableItemLocator = By.XPath(".//tr");

        /// <summary>
        /// The  Pdf document table component
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Pdf table items list 
        /// </summary>
        public ItemsCollection<FileHistoryTableItem> TableItems => new ItemsCollection<FileHistoryTableItem>(ComponentLocator, FileHistoryTableItemLocator);
    }
}
