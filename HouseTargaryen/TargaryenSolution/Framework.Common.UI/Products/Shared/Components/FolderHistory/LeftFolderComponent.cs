namespace Framework.Common.UI.Products.Shared.Components.FolderHistory
{
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Components.Folder;
    using Framework.Common.UI.Products.Shared.Dialogs.Foldering;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// LeftFolderGridComponents inherits from the BaseModuleRegressionComponent and including the search within functions 
    /// </summary>
    public class LeftFolderComponent : BaseModuleRegressionComponent
    {
        private static readonly By NewFolderButtonLocator = By.XPath("//li[contains(@class, 'co_addFolderButton')] | //li[@class='co_createNewFolder']/button");

        private static readonly By TreeRootLocator = By.Id("cobalt_ro_myFolders_folderTree_root");

        private static readonly By ContainerLocator = By.Id("co_researchOrganizerNavigationContainer");

        /// <summary>
        /// component for folder tree component
        /// </summary>
        public FolderTreeComponent FolderTree { get; private set; } = new FolderTreeComponent(TreeRootLocator);

        /// <summary>
        /// The Narrow Pane (left side of search results page)
        /// </summary>
        public NarrowPaneComponent NarrowPane { get; set; } = new NarrowPaneComponent();

        /// <summary>
        /// component for folder's option dropdown
        /// </summary>
        public FolderOptionsDropdown Options => new FolderOptionsDropdown();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Create new folder
        /// </summary>
        /// <returns>
        /// The <see cref="NewFolderDialog"/>.
        /// </returns>
        public NewFolderDialog ClickNewFolderButton()
        {
            DriverExtensions.WaitForElement(NewFolderButtonLocator).Click();
            return new NewFolderDialog();
        }
    }
}