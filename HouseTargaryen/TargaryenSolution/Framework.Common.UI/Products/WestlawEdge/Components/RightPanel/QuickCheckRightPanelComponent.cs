namespace Framework.Common.UI.Products.WestlawEdge.Components.RightPanel
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.SelectAll;
    using Framework.Common.UI.Products.Shared.Components.Toolbar.CustomToolbars;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Quick Check Right panel component
    /// </summary>
    public class QuickCheckRightPanelComponent : BaseEdgeRightPanelComponent
    {
        private static readonly By FullReportLinkLocator = By.XPath(".//div[@class='DA-HeadingCases']//a");
        private static readonly By ContainerLocator = By.XPath("//div[@id='co_quickCheckPanel']");
        private static readonly By ZeroStateMessageLocator = By.XPath(".//div[@class = 'DANoContent']");
        private static readonly By SelectAllComponentLocator = By.XPath("//ul[@class = 'co_navOptions']");
        private static readonly By ResultListLocator = By.XPath(".//*[@class = 'co_issueList']");
        private static readonly By ResultItemLocator = By.XPath("./li");
        private static readonly By ToolbarLocator = By.XPath(".//div[@class='DocumentPanel-toolBar']");

        /// <summary>
        /// Quick Check Right panel toolbar.
        /// </summary>
        public RightPanelQuickCheckToolbar Toolbar => new RightPanelQuickCheckToolbar(DriverExtensions.WaitForElement(new ByChained(this.ComponentLocator, ToolbarLocator)));

        /// <summary>
        /// Drag and drop element to folder
        /// </summary>
        /// <param name="itemToDrag">item to drop to folder</param>
        /// <param name="targetFolder">folder</param>
        /// <param name="itemToHold">item to hold</param>
        public void DragAndDropResultListItemToRecentFolder(RightPanelResultListItem itemToDrag, IWebElement itemToHold,
                                                            string targetFolder)
        {
            DriverExtensions.DragAndHold(
                itemToHold,
                itemToDrag.GetDraggableElement());

            DriverExtensions.DragAndDrop(new EdgeRecentFoldersDialog().GetFolderElement(targetFolder), 
                itemToDrag.GetDraggableElement());
        }

        /// <summary>
        /// Verify that component is displayed
        /// </summary>
        /// <returns> True if component is displayed, false otherwise </returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator);

        /// <summary>
        ///  Get list of Recommendation items
        /// </summary>
        /// <returns>list of Recommendation items</returns>
        public ItemsCollection<RightPanelResultListItem> RecommendationItems => new ItemsCollection<RightPanelResultListItem>(new ByChained(this.ComponentLocator, ResultListLocator), ResultItemLocator);

        /// <summary>
        /// Gets the Select all component.
        /// </summary>
        public SelectAllComponent SelectAllComponent { get; } = new SelectAllComponent(SelectAllComponentLocator);

        /// <summary>
        /// See full report link
        /// </summary>
        public ILink SeeFullReportLink => new Link(this.ComponentLocator, FullReportLinkLocator);

        /// <summary>
        /// Quick Check zero state message label
        /// </summary>
        public ILabel ZeroStateLabel => new Label(this.ComponentLocator, ZeroStateMessageLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
