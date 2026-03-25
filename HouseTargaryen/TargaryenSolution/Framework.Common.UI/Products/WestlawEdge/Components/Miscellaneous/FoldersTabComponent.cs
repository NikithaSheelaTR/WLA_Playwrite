namespace Framework.Common.UI.Products.WestlawEdge.Components.Miscellaneous
{
    using Framework.Common.UI.Products.WestLawNext.Components;
    using OpenQA.Selenium;

    /// <summary>
    /// Folders tab component (homepage)
    /// </summary>
    public class FoldersTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("tab_FoldersPaneId");

        /// <summary>
        /// Gets the HistoryContentComponent
        /// </summary>
        public FoldersContentComponent FoldersContentComponent { get; } = new FoldersContentComponent();

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Folders";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
