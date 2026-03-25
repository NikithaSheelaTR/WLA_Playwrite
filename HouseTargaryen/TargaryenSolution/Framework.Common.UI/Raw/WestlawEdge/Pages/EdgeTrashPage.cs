namespace Framework.Common.UI.Raw.WestlawEdge.Pages
{
    using Framework.Common.UI.Products.Shared.Components.FolderHistory;
    using Framework.Common.UI.Products.Shared.Dialogs.Foldering;
    using Framework.Common.UI.Products.Shared.Pages.Foldering;
    using Framework.Common.UI.Products.WestlawEdge.Components;
    using Framework.Common.UI.Products.WestlawEdge.Components.FolderHistory;
    using Framework.Common.UI.Products.WestlawEdge.Components.Folders;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Folders;

    /// <summary>
    /// Indigo Trash Page.
    /// </summary>
    public class EdgeTrashPage : TrashPage
    {
        /// <summary>
        /// Gets. The trash grid component.
        /// </summary>
        public override FolderGridComponent GridComponent { get; } = new EdgeFolderGridComponent();

        /// <summary>
        /// Gets the header.
        /// </summary>
        public new EdgeHeaderComponent Header { get; } = new EdgeHeaderComponent();

        /// <summary>
        /// Gets the doc toolbar.
        /// </summary>
        public new EdgeToolbarComponent Toolbar { get; } = new EdgeToolbarComponent();

        /// <summary>
        /// Quick access
        /// </summary>
        public QuickAccessComponent QuickAccess { get; } = new QuickAccessComponent();

        /// <summary>
        /// This method deletes items from Trash. 
        /// </summary>
        /// <param name="numberOfItemsToDelete">
        /// The number Of Items To Delete.
        /// </param>
        public override void DeleteItemsFromTrash(int numberOfItemsToDelete)
        {
            this.GridComponent.SelectGridItemsRange(numberOfItemsToDelete);
            this.Toolbar.TrashDropdown.SelectOption<ConfirmationDialog>(TrashMenuOption.PermanentlyDelete)
                .ClickOkButton<EdgeTrashPage>();
        }
    }
}