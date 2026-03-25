namespace Framework.Common.UI.Products.WestlawEdge.Components.Folders
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.DropDowns;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// EdgeFolderHeaderComponent
    /// </summary>
    public class EdgeFolderHeaderComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[./div[@id = 'co_folderTitleWrapper']]");
        private static readonly By FolderTitleLocator = By.XPath(".//h1[@class = 'co_folderTitle']");
        private static readonly By NoteLocator = By.XPath(".//div[@class = 'Foldering--Note']");
        private static readonly By SocialIconButtonLocator = By.XPath(".//button[(@id = 'co_shareManagerBtn')]");

        /// <summary>
        /// Add note to item component 
        /// </summary>
        public EdgeAddNoteToItemComponent AddNoteToItem => new EdgeAddNoteToItemComponent(DriverExtensions.WaitForElement(this.ComponentLocator));

        /// <summary>
        /// Actions menu dropdown
        /// </summary>
        public ActionsMenuDropdown ActionsMenu => new ActionsMenuDropdown(DriverExtensions.GetElement(this.ComponentLocator));

        /// <summary>
        /// Folder title label
        /// </summary>
        public ILabel FolderTitle => new Label(this.ComponentLocator, FolderTitleLocator);

        /// <summary>
        /// Note to folder
        /// </summary>
        public ILabel Note => new Label(this.ComponentLocator, NoteLocator);

        /// <summary>
        /// Social icon button
        /// </summary>
        public IButton SocialIconButton => new Button(this.ComponentLocator, SocialIconButtonLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}