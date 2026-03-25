namespace Framework.Common.UI.Products.WestLawNextCanada.Components.QuickCheck.ReportTabs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.ReportTabs;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Canada Recommendations Tab for Quick Check feature in Westlaw Edge Canada.
    /// </summary>
    public class CanadaRecommendationsTab : RecommendationsTab
    {
        private static readonly By NavigateHeadingsButtonLocator = By.Id("co_outlineButton");
        private static readonly By ItemHeaderLocator = By.XPath("//div[@class='co_issueItemHeader']//label");
        private static readonly By ToolbarTitleLabelLocator = By.XPath("//div[@class='DA-HeaderToolbar']/h2");

        /// <summary>
        /// Navigate Headings button .
        /// </summary>
        public IButton NavigateHeadingsButton => new Button(NavigateHeadingsButtonLocator);

        /// <summary>
        /// Toolbar Title Label.
        /// </summary>
        public ILabel ToolbarTitleLabel => new Label(ToolbarTitleLabelLocator);

        /// <summary>
        /// Item Header Labels.
        /// </summary>
        public IReadOnlyCollection<ILabel> ItemHeaderLabels => new ElementsCollection<Label>(ItemHeaderLocator);

        /// <summary>
        /// Checks if the header title is present in the Recommendations tab.
        /// </summary>
        /// <param name="headerTitle"></param>
        /// <returns>true if is in view</returns>
        public bool IsHeaderTitleInView(string headerTitle)
        {
            return ItemHeaderLabels.FirstOrDefault(label => label.GetAttribute("title").Equals(headerTitle)).IsInView;
        }

        /// <summary>
        /// Gets the narrow pane.
        /// </summary>
        public new NarrowPaneComponent NarrowPane { get; } = new NarrowPaneComponent();
    }
}