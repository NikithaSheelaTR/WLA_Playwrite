namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.PrecisionFilters
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.WestlawEdge.Elements;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components;
    using Framework.Common.UI.Products.WestlawEdgePremium.DropDowns;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Base Precision Filters tab component
    /// </summary>
    public abstract class BasePrecisionFiltersTabComponent : BaseTabComponent
    {
        private static readonly By AdditionalFiltersSectionLocator = By.XPath(".//*[@class='PrecisionSearch-moreMatches'] | .//*[@class='Typeahead-wrapper']/div[not(contains(@class, 'PrecisionSearch-list'))]");
        private static readonly By BackToTopButtonLocator = By.XPath(".//div[@id='co_backToTop']//button");
        private static readonly By FiltersSectionLocator = By.XPath(".//*[@id='coid_precisionsearch_primaryfilter'] | .//*[contains(@class, 'PrecisionSearchModal-contentList')]//*[@class='PrecisionSearch-list' or @role='tree'] | .//*[@class='Typeahead-wrapper']/div[@class='PrecisionSearch-list']");
        private static readonly By SearchTextboxLocator = By.XPath(".//*[@id='co_facet_searchBox']");
        private static readonly By SortDropdownLocator = By.XPath("//div[contains(@class, 'Tab-panel--show')]//div[@class='PrecisionSearchModal-sort']");
        private static readonly By TextboxLabelLocator = By.XPath(".//div[contains(@class, 'contentHeading')]//div");
        private static readonly By XClearButtonLocator = By.XPath("./following-sibling::*[@class='Typeahead-clearButton']");        
        private static readonly By ZeroStateMessageLocator = By.XPath(".//*[@class='TypeAhead-noResults']");
        
        /// <summary>
        /// Back to top button
        /// </summary>
        public IButton BackToTopButton => new Button(this.ComponentLocator, BackToTopButtonLocator);

        /// <summary>
        /// Textbox label
        /// </summary>
        public ILabel TextboxLabel => new Label(this.ComponentLocator, TextboxLabelLocator);

        /// <summary>
        /// Zero state mesasge label
        /// </summary>
        public ILabel ZeroStateMessageLabel => new Label(this.ComponentLocator, ZeroStateMessageLocator);

        /// <summary>
        /// Search textbox
        /// </summary>
        public ITextbox SearchTextbox => new TextboxWithClearButton(DriverExtensions.GetElement(this.ComponentLocator, SearchTextboxLocator), XClearButtonLocator);

        /// <summary>
        /// The detail level dropdown.
        /// </summary>
        public PrecisionFilterSortDropdown SortDropdown => new PrecisionFilterSortDropdown(DriverExtensions.WaitForElement(SortDropdownLocator));

        /// <summary>
        /// Matches
        /// </summary>
        public PrecisionMatchesComponent Matches => new PrecisionMatchesComponent(new ByChained(this.ComponentLocator, FiltersSectionLocator));

        /// <summary>
        /// Additional matches
        /// </summary>
        public PrecisionAdditionalMatchesComponent AdditionalMatches => new PrecisionAdditionalMatchesComponent(new ByChained(this.ComponentLocator, AdditionalFiltersSectionLocator));
    }
}
