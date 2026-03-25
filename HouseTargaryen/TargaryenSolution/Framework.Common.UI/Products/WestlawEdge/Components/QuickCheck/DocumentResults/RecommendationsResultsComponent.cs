namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck;

    using OpenQA.Selenium;

    /// <summary>
    /// Document analyzer=>Recommendations page=>Recommendations tab=>Results pane to the right.
    /// </summary>
    public class RecommendationsResultsComponent : BaseItem
    {
        private static readonly By ItemsCountLocator = By.ClassName("DA-HeaderToolbar");

        private static readonly By ItemsSelectedLocator = By.XPath("//li[@class='co_navItemsSelected']");

        private static readonly By IssueItemContainerLocator = By.XPath(".//div[@class='co_issueItem']");

        /// <summary>
        /// Initializes a new instance of the <see cref="RecommendationsResultsComponent"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public RecommendationsResultsComponent(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// Issue items containers. Everything that's under the blue collapsible header.
        /// </summary>
        public ItemsCollection<IssueItemContainerComponent> Headings => new ItemsCollection<IssueItemContainerComponent>(this.Container, IssueItemContainerLocator);

        /// <summary>
        ///Selected items count label
        /// </summary>
        public ILabel SelectedItemsCountLabel => new Label(ItemsSelectedLocator);

        /// <summary>
        /// Highlighted text heading.
        /// </summary>
        public ILabel AllItemsCountLabel => new Label(ItemsCountLocator);
    }
}