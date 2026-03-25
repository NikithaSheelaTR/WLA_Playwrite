namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.ReportTabs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Toolbar.FooterToolbar;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults;
    using Framework.Common.UI.Products.WestlawEdge.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The Language/Quotation analysis tab.
    /// </summary>
    public class LanguageQuotationAnalysisTab : BaseQuickCheckTabComponent
    {
        private static readonly By HighlightsToggleLocator = By.XPath("//*[contains(@id,'SlideToggle_DA')]");
        private static readonly By ResultListLocator = By.XPath(".//div[@class='co_issueItemHeader']//ancestor::ul");
        private static readonly By ResultItemLocator = By.XPath("./li");
        private static readonly By TabContainerLocator = By.XPath("//div[contains(@class, 'DA-QuotationPage')]");
 
        /// <summary>
        /// The narrow pane.
        /// </summary>
        public QuotationAnalysisNarrowPaneComponent NarrowPane { get; } = new QuotationAnalysisNarrowPaneComponent();

        /// <summary>
        /// The pagination component.
        /// </summary>
        public PaginationFooterComponent PaginationComponent { get; } = new PaginationFooterComponent();

        /// <summary>
        /// The difference and pincite error toggle.
        /// </summary>
        public ICheckBox HighlightsToggle => new CheckBox(HighlightsToggleLocator);

        /// <summary>
        /// The result list.
        /// </summary>
        public virtual QuickCheckItemsCollection<QuotationAnalysisItem> ResultList =>
            new QuickCheckItemsCollection<QuotationAnalysisItem>(
                DriverExtensions.GetElement(this.ComponentLocator, ResultListLocator), ResultItemLocator);
        
        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TabContainerLocator;

        /// <summary>
        /// The difference and pincite error toggle.
        /// </summary>
        /// <param name="desiredState"></param>
        public void SetHighlightsToggle(string desiredState)
        {
            if (DriverExtensions.GetAttribute("aria-pressed", HighlightsToggleLocator) != desiredState)
            {
                DriverExtensions.Click(HighlightsToggleLocator);
                DriverExtensions.GetAttribute("aria-checked", HighlightsToggleLocator).Equals(desiredState);
            }
        }

    }
}