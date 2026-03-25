namespace Framework.Common.UI.Products.WestlawEdge.Components.Folders
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Raw.WestlawEdge.Items.Folders;

    using OpenQA.Selenium;

    /// <summary>
    /// Collaborators and roles component
    /// </summary>
    public class EdgeCollaboratorsAndRolesComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//form[@class = 'co_shareFolder_collaboratorsAndRolesForm']");
        private static readonly By ItemsLocator = By.XPath(".//div[@class = 'SharedWithTableBody']//div[@role ='row']");
        
        /// <summary>
        ///  Get list of collaborators and roles grid items
        /// </summary>
        /// <returns>list of collaborators and roles grid items</returns>
        public ItemsCollection<CollaboratorsAndRolesGridItem> GridItems =>
            new ItemsCollection<CollaboratorsAndRolesGridItem>(this.ComponentLocator, ItemsLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}