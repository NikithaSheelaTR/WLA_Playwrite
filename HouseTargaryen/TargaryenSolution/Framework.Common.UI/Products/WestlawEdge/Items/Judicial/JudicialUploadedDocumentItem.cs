namespace Framework.Common.UI.Products.WestlawEdge.Items.Judicial
{
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// assign document to party component
    /// </summary>
    public class JudicialUploadedDocumentItem : BaseItem
    {
        private static readonly By DocumentNameLocator = By.XPath(".//span[contains(@class,'DA-JudicialFilesListItem') or contains(@class,'DA-JudicialUploadTitle')]");

        private static readonly By DeleteFileLocator = By.XPath(".//button[@class='co_dock_trash DA-FileRemove']");

        private static readonly By SelectedItemLocator = By.XPath(".//span[@class='co_accessibilityFocus']");

        private static readonly By DropSlotLocator = By.ClassName("DA-Party-DropZone");

        /// <summary>
        /// Initializes a new instance of the <see cref="JudicialUploadedDocumentItem"/> class.
        /// </summary>
        /// <param name="container">container</param>
        public JudicialUploadedDocumentItem(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// Delete file button
        /// </summary>
        public IButton DeleteFileButton => new Button(this.Container, DeleteFileLocator);

        /// <summary>
        /// file name
        /// </summary>
        public string Name => DriverExtensions.GetElement(this.Container, DocumentNameLocator).GetAttribute("title");

        /// <summary>
        /// Verify is assigned to party
        /// </summary>
        /// <returns>true if slot is free</returns>
        public bool Assigned => DriverExtensions.IsDisplayed(this.Container, DocumentNameLocator);

        /// <summary>
        /// Verify if document is selected 
        /// </summary>
        public bool Selected => DriverExtensions.IsDisplayed(this.Container, SelectedItemLocator);

        /// <summary>
        /// Assign current item to party by drag and drop
        /// </summary>
        /// <param name="slot">slot to assign</param>
        public void AssignToByDragAndDrop(JudicialUploadedDocumentItem slot)
        {
            if (!this.Selected)
            {
                this.Select();
            }

            DriverExtensions.CustomDragAndDrop(this.GetDraggableItem(), slot.GetDraggableItem());
        }

        /// <summary>
        /// Assign current item to party by click
        /// </summary>
        /// <param name="slot">slot to assign</param>
        public void AssignToByClick(JudicialUploadedDocumentItem slot)
        {
            if (!this.Selected)
            {
                this.Select();
            }
            slot.GetDraggableItem().Click();
        }

        /// <summary>
        /// Select item
        /// </summary>
        public void Select() => this.GetDraggableItem().Click();

        /// <summary>
        /// Get Draggable item
        /// </summary>
        /// <returns>this item</returns>
        internal IWebElement GetDraggableItem()
        {
            return DriverExtensions.GetElements(this.Container, DocumentNameLocator).Any()
                       ? DriverExtensions.GetElement(this.Container, DocumentNameLocator)
                       : DriverExtensions.GetElement(this.Container, DropSlotLocator);
        }
    }
}