namespace Framework.Common.UI.Products.WestlawEdge.Components.Miscellaneous
{
    using Framework.Common.UI.Products.Shared.Components;
    using OpenQA.Selenium;

    /// <summary>
    /// Folders Widget Content Component
    /// </summary>
    public class FoldersContentComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("panel_FoldersPaneId");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
