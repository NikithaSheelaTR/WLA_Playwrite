namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.NarrowPane;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.AALP;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
    using Framework.Common.UI.Products.Shared.Elements;
    using System.Collections.Generic;
    
    /// <summary>
    /// Search result component
    /// </summary>
    public class ParallelSearchResultComponent : BaseModuleRegressionComponent
    {
        private static readonly By ResultsContainerLocator = By.XPath("//div[contains(@class,'__parallelSearchResults--')]");
        private static readonly By ResultCountLabelLocator = By.XPath(".//h2[contains(@class,'paralleSearchResultsCount')]");
        private static readonly By CasesItemLocator = By.XPath("//div[contains(@class,'__parallelSearchResults--')]/ul/li");
        private static readonly By SelectAllLocator = By.XPath("//saf-checkbox[@id='parallelSearch-selectAll']");
        private const string SelectAllCheckboxScript = "return(arguments[0].shadowRoot.querySelector('input[id=control]'));";
        private static readonly By SelectedCountLocator = By.XPath("//span[contains(@class, '__selectedCount')]");
        private static readonly By CheckBoxLocator = By.XPath("//div[contains(@class, '__parallelSearchResultsItemCheckbox')]/saf-checkbox");
        private const string CheckboxScript = "return(arguments[0].shadowRoot.querySelector('input[id=control]'));";
        private static readonly By HighlightedTextLocator = By.XPath("//span[@class='co_searchTerm']");
        private static readonly By NoResultFoundLocator = By.XPath("//div[contains(@class, '__zeroStateContainer')]/h2/saf-text");
        private static readonly By ClearAllFiltersLocator = By.XPath("//div[contains(@class, '__zeroStateContainer')]/saf-button");
        private static readonly By PurpleHighlightLocator = By.XPath("//span[@class='co_searchTerm co_keyword']");
        private static readonly By GoToSnippetLocator = By.XPath("//saf-anchor[contains(@class, '__parallelSearchSnippetLink')]");
        private const string GoToSnippetScript = "return(arguments[0].shadowRoot.querySelector('a'));";

        /// <summary>
        /// Query box Component
        /// </summary>
        public ParallelSearchQueryBoxComponent QueryBox { get; } = new ParallelSearchQueryBoxComponent();

        /// <summary>
        /// Toolbar
        /// </summary>
        public ParallelSearchToolbar Toolbar { get; } = new ParallelSearchToolbar();

        /// <summary>
        /// Filter Facet Component
        /// </summary>
        public ParallelSearchFilterFacetComponent FilterFacet { get; } = new ParallelSearchFilterFacetComponent();

        /// <summary>
        /// Result Count Label
        /// </summary>
        public ILabel ResultCountLabel => new Label(this.ComponentLocator, ResultCountLabelLocator);

        /// <summary>
        /// Cases items
        /// </summary>
        /// <returns>List of Cases answers</returns>
        public ItemsCollection<ParallelSearchResultItem> CasesItems =>
            new ItemsCollection<ParallelSearchResultItem>(this.ComponentLocator, new ByChained(this.ComponentLocator, CasesItemLocator));

        /// <summary>
        /// Selected Label
        /// </summary>
        public ILabel SelectedLabel => new Label(this.ComponentLocator, SelectedCountLocator);

        /// <summary>
        /// Highlighted Text Links
        /// </summary>     
        public ILink HighlightedTextLink => new Link(this.ComponentLocator, HighlightedTextLocator);

        /// <summary>
        /// No results found text
        /// </summary>
        public ILabel NoResultFoundLabel => new Label(NoResultFoundLocator);

        /// <summary>
        /// Clear all filters button
        /// </summary>
        public IButton ClearAllFiltersButton => new Button(ClearAllFiltersLocator);

        /// <summary>
        /// Purple highlighted labels
        /// </summary>
        public IReadOnlyCollection<ILabel> PurpleHighlightedLabels => new ElementsCollection<Label>(this.ComponentLocator, PurpleHighlightLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ResultsContainerLocator;

        /// <summary>
        /// Select All checkbox
        /// </summary>
        public void SelectAllCheckbox()
        {
            IWebElement selectAllElement = DriverExtensions.GetElement(SelectAllLocator);// Shadow host 
            IWebElement selectAll = (IWebElement)DriverExtensions.ExecuteScript(SelectAllCheckboxScript, selectAllElement);
            selectAll.Click();
        }
                
        /// <summary>
        /// check/uncheck checkbox
        /// </summary>
        public void ClickFirstCheckbox()
        {
            IWebElement checkboxElement = DriverExtensions.GetElement(CheckBoxLocator);// Shadow host 
            IWebElement Checkbox = (IWebElement)DriverExtensions.ExecuteScript(CheckboxScript, checkboxElement);
            Checkbox.Click();
        }

        /// <summary>
        /// Go to snippet link element
        /// </summary>
        /// <return>
        /// Return go to snippet link element
        /// </return>
        public IWebElement GoToSnippet1LinkElement()
        {
            IWebElement GoToSnippetElement = DriverExtensions.GetElement(GoToSnippetLocator);
            IWebElement GoToSnippet1 = (IWebElement)DriverExtensions.ExecuteScript(GoToSnippetScript, GoToSnippetElement);
            return GoToSnippet1;
        }
    }
}
