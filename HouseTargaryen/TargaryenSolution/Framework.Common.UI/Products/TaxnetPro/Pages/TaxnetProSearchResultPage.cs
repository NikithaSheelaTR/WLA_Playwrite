namespace Framework.Common.UI.Products.TaxnetPro.Pages
{
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Products.TaxnetPro.Components;
    using Framework.Common.UI.Products.TaxnetPro.Components.Toolbar;
    using Framework.Common.UI.Products.TaxnetPro.Components.NarrowPane;
    using Framework.Common.UI.Products.TaxnetPro.Components.ResultList;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;

    using OpenQA.Selenium;

    /// <summary>
    /// Taxnet Pro search result page
    /// </summary>
    public class TaxnetProSearchResultPage : EdgeCommonSearchResultPage
    {
        private static readonly By NoResultsFoundMessageLocator = By.XPath("//div[@id='cobalt_search_no_results']");
        private static readonly By TypeaheadDialogWindowLocator = By.Id("typeaheadcontainer");

        /// <summary>
        /// Taxnet Pro Filter Component
        /// </summary>
        public TaxnetProSearchFacetsFilterComponent Filter { get; } = new TaxnetProSearchFacetsFilterComponent();

        /// <summary>
        /// Taxnet Pro Your Search Component
        /// </summary>
        public TaxnetProYourSearchComponent YourSearch { get; } = new TaxnetProYourSearchComponent();

        /// <summary>
        /// Taxnet Pro Search Options Tab Component
        /// </summary>
        public SearchOptionsTabComponent SearchOptionsTab { get; } = new SearchOptionsTabComponent();

        /// <summary>
        /// The Narrow Pane (left side of search results page)
        /// </summary>
        public NarrowPaneComponent LeftNarrowPane { get; set; } = new NarrowPaneComponent();

        /// <summary>
        /// Snippet Info Onboadring Component
        /// </summary>
        public SnippetDisplayInfoBoxComponent SnippetInfo { get; set; } = new SnippetDisplayInfoBoxComponent();

        /// <summary>
        /// Taxnet Pro Tool bar component
        /// </summary>
        public TaxnetProToolbarComponent TaxnetProToolbar { get; protected set; } = new TaxnetProToolbarComponent();

        /// <summary>
        /// Gets the text of No results found message
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetNoResultsFoundMessage() =>
            DriverExtensions.WaitForElement(NoResultsFoundMessageLocator).GetText();

        /// <summary>
        /// Typeahead dialog window is displayed
        /// </summary>
        /// <returns></returns>
        public bool IsTypeaheadDialogWindowDisplayed() =>
            DriverExtensions.IsDisplayed(TypeaheadDialogWindowLocator);
    }
}