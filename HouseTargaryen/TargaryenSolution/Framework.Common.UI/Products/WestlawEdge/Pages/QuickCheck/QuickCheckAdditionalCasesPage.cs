namespace Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.SelectAll;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.Toolbar;
    using Framework.Common.UI.Products.WestlawEdge.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// The document analyzer additional cases page.
    /// </summary>
    public class QuickCheckAdditionalCasesPage : QuickCheckBasePage
    {
        private static readonly By ResultListLocator = By.ClassName("co_searchResultsList");
        private static readonly By ResultItemLocator = By.XPath(".//li[@class = 'DA-AdditionalCasesItem']");
        private static readonly By ReturnToReportLinkLocator = By.XPath("//*[@title='Back to report']");
        private static readonly By HeadingLocator = By.ClassName("DA-AdditionalCasesHeader");
        private static readonly By OriginalRecommendationsComponentLocator = By.ClassName("DA-OriginalReport");
        private static readonly By ToolbarLocator = By.XPath("//div[@class = 'DA-HeaderToolbar'] | //div[@class='co_navTools']");
        private static readonly By SelectAllComponentLocator = By.XPath("//ul[@class = 'co_navOptions']");

        /// <summary>
        /// The original recommendations component.
        /// </summary>
        public QuickCheckOriginalRecommendationsComponent OriginalRecommendationsComponent =>
            new QuickCheckOriginalRecommendationsComponent(OriginalRecommendationsComponentLocator);

        /// <summary>
        /// Select all Component 
        /// </summary>
        public SelectAllComponent SelectAllComponent => new SelectAllComponent(SelectAllComponentLocator);

        /// <summary>
        /// The result list.
        /// </summary>
        public QuickCheckItemsCollection<RecommendationItem> ResultList => new QuickCheckItemsCollection<RecommendationItem>(ResultListLocator, ResultItemLocator, "li");

        /// <summary>
        /// The toolbar.
        /// </summary>
        public QuickCheckToolbar Toolbar => new QuickCheckToolbar(DriverExtensions.WaitForElement(ToolbarLocator));

        /// <summary>
        /// Gets the narrow pane.
        /// </summary>
        public QuickCheckAdditionalCasesNarrowPaneComponent NarrowPane { get; } = new QuickCheckAdditionalCasesNarrowPaneComponent();

        /// <summary>
        /// Return to report link
        /// </summary>
        public ILink ReturnToReportLink => new Link(ReturnToReportLinkLocator);

        /// <summary>
        /// Heading label
        /// </summary>
        public ILabel HeadingLabel => new Label(HeadingLocator);
    }
}