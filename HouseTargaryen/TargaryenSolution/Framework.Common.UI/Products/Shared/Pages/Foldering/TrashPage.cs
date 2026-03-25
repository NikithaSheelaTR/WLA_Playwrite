namespace Framework.Common.UI.Products.Shared.Pages.Foldering
{
    using Framework.Common.UI.Products.Shared.Components.FolderHistory;
    using Framework.Common.UI.Products.Shared.Components.Toolbar;
    using Framework.Common.UI.Products.Shared.Dialogs.Foldering;
    using Framework.Common.UI.Products.WestLawNext.Enums.Toolbars;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Trash Page with methods around actions performed on the Trash page.
    /// </summary>
    public class TrashPage : CommonAuthenticatedWestlawNextPage
    {
        private static readonly By TitleTrashLocator = By.XPath("//div/h1[@class='co_folderTitle' and contains(.,'Trash')]");

        private static readonly By EmptyTrashButtonlocator = By.XPath("//li[@class='co_emptyTrashButton']/a");

        /// <summary>
        /// Initializes a new instance of the <see cref="TrashPage"/> class. 
        /// </summary>
        public TrashPage()
        {
            DriverExtensions.WaitForElementPresent(TitleTrashLocator);
        }
         
        /// <summary>
        /// Gets. The trash grid component.
        /// </summary>
        public virtual FolderGridComponent GridComponent { get; } = new FolderGridComponent();

        /// <summary>
        /// Left Trash tree section
        /// </summary>
        public LeftFolderComponent LeftFolderGrid { get; } = new LeftFolderComponent();

        /// <summary>
        /// Toolbar with buttons
        /// </summary>
        public Toolbar Toolbar { get; } = new Toolbar();

        /// <summary>
        /// This method deletes items from Trash. 
        /// </summary>
        /// <param name="numberOfItemsToDelete">
        /// The number Of Items To Delete.
        /// </param>
        public virtual void DeleteItemsFromTrash(int numberOfItemsToDelete)
        {
            this.GridComponent.SelectGridItemsRange(numberOfItemsToDelete);
            this.Toolbar.ClickToolbarElement<ConfirmationDialog>(ToolbarElements.Delete).ClickOkButton<TrashPage>();
        }

        /// <summary>
        /// Clicks the empty trash button in the navigation toolbar 
        /// </summary>
        public void EmptyTrash() =>
            this.Toolbar.ClickToolbarElement<ConfirmationDialog>(ToolbarElements.EmptyTrash).ClickOkButton<TrashPage>();

        /// <summary>
        /// Checks to see if Empty Trash link is in Enabled mode.
        /// </summary>
        /// <returns>true if the Empty trash is Enabled.</returns>
        public bool IsEmptyTrashOptionDisplayed() => DriverExtensions.IsDisplayed(EmptyTrashButtonlocator, 5);
    }
}