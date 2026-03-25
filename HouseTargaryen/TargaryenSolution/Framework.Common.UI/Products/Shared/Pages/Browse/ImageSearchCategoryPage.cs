namespace Framework.Common.UI.Products.Shared.Pages.Browse
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.CategoryPage;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;

    /// <summary>
    /// Image Search Page
    /// </summary>
    public class ImageSearchCategoryPage : CommonBrowsePage
    {
        private static readonly By SearchButtonLocator = By.XPath("//button[@id='tmis_search_button']");
        private static readonly By JurisdictionFilterLabelLocator =
            By.XPath("//h3[@class='TrademarkImageSearch-header']");
        private static readonly By PageTitleLabelLocator = By.XPath("//h1[contains(@id,'PageLabel')]");

        /// <summary>
        /// Page Title Label
        /// </summary>
        public ILabel PageTitleLabel => new Label(PageTitleLabelLocator);

        /// <summary>
        /// Jurisdiction filter label
        /// </summary>
        public ILabel JurisdictionFilterLabel => new Label(JurisdictionFilterLabelLocator);
        
        /// <summary>
        /// Search button
        /// </summary>
        public IButton SearchButton => new Button(SearchButtonLocator);

        /// <summary>
        /// Trademark Image Upload Component
        /// </summary>
        public TrademarkImageUploadComponent ImageUploadComponent => new TrademarkImageUploadComponent();

        /// <summary>
        /// Jurisdiction Filter
        /// </summary>
        public TrademarksJurisdictionFilterComponent TrademarksJurisdictionFilter => new TrademarksJurisdictionFilterComponent();

        /// <summary>
        /// Trademark ImageVariants
        /// </summary>
        public TrademarkImageVariantsComponent TrademarkImageVariantsComponent => new TrademarkImageVariantsComponent();

    }
}
