namespace Framework.Common.UI.Products.WestlawEdge.Components.Judicial.ReportTabs
{
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces.Components;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Toolbar.FooterToolbar;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdge.Components.Judicial;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.ReportTabs;
    using Framework.Common.UI.Products.WestlawEdge.Elements.Judicial;
    using Framework.Common.UI.Products.WestlawEdge.Items;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Judicial cited authority tab
    /// </summary>
    public sealed class JudicialCitedAuthorityTab : BaseQuickCheckTabComponent
    {
        private static readonly By TabContainerLocator = By.ClassName("DA-CitedAuthorityContainer");
        private static readonly By ResultListLocator = By.XPath(".//div[@class='co_searchResultsList']");
        private static readonly By GroupContainerLocator = By.XPath("//div[./div[@class='DA-GroupedContentHeader']]");
        private static readonly By ResultItemLocator = By.XPath(".//div[@class = 'DA-KCWarning' or @class='DA-TOACase']");
        private static readonly By SelectAllLocator = By.XPath("//input[@class = 'DA-GroupedContentInput']");

        /// <summary>
        /// Judicial cited authority footer component
        /// </summary>
        public PaginationFooterComponent PaginationComponent => new PaginationFooterComponent();

        /// <summary>
        /// Gets the judicial cited authority narrow pane
        /// </summary>
        public CitedAuthorityNarrowPaneComponent NarrowPane { get; } = new CitedAuthorityNarrowPaneComponent();

        /// <summary>
        /// The result list.
        /// </summary>
        public QuickCheckItemsCollection<CitedAuthorityItem> ResultList =>
            new QuickCheckItemsCollection<CitedAuthorityItem>(
                new ByChained(this.ComponentLocator, ResultListLocator), ResultItemLocator, "div");

        /// <summary>
        /// Actual only for All cited authority content type
        /// </summary>
        public IItemsCollection<ToaGroupSectionComponent> GroupSections
            => new ItemsCollection<ToaGroupSectionComponent>(this.ComponentLocator, GroupContainerLocator);

        /// <summary>
        /// SelectAllCheckboxList
        /// </summary>
        public IReadOnlyCollection<ICheckBox> SelectAllCheckboxList => new ElementsCollection<JudicialCheckBox>(SelectAllLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TabContainerLocator;  
    }
}
