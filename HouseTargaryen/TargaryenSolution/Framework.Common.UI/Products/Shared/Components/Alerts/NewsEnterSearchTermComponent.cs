namespace Framework.Common.UI.Products.Shared.Components.Alerts
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Alerts.NewsEnterSearchTermComponents;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.Alerts;
    using Framework.Common.UI.Products.Shared.Enums.Search;

    using OpenQA.Selenium;

    /// <summary>
    /// WestClip News Enter Search Term Section
    /// </summary>
    public class NewsEnterSearchTermComponent : EnterSearchTermsComponent
    {
        private static readonly By DocumentLengthDropdownLocator = By.Id("co_search_alertSearchPanelDocLength");

        private static readonly By ContainerLocator = By.Id("co_search_alertSearchPanelOtherFormFieldsBox");

        private static readonly By SortOrderDropdownLocator = By.XPath("//select[@id='co_search_alertSearchPanelSortTypes']");

        /// <summary>
        /// Exclude These Document Types Component
        /// </summary>
        public ExcludeTheseDocumentTypesComponent ExcludeTheseDocumentTypes { get; private set; } = new ExcludeTheseDocumentTypesComponent();

        /// <summary>
        /// Sort Order drop down
        /// </summary>
        public IDropdown<AlertSortOrderOptions> SortOrder { get; set; } = new Dropdown<AlertSortOrderOptions>(SortOrderDropdownLocator);

        /// <summary>
        /// Document Length Dropdown
        /// </summary>
        public IDropdown<DocumentLengthOptions> DocumentLength { get; set; } =
            new Dropdown<DocumentLengthOptions>(DocumentLengthDropdownLocator) { AdditionalInfo = "Alert" };

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}