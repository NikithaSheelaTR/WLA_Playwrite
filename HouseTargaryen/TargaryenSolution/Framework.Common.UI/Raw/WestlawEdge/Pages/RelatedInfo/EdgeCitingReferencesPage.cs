namespace Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo
{
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawAdvantage.Components;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Indigo Citing References Page
    /// </summary>
    public class EdgeCitingReferencesPage : EdgeTabPage
    {
        private static readonly By CitingReferencesTitleLocator = By.Id("co_categoryLabelContainer");
        private static readonly By ResultContainerLocator = By.Id("co_contentColumn");
        private static readonly By FacetPaneElementLocator = By.Id("co_website_searchFacets");
        private static readonly By DetailDropdownLocator = By.Id("co_docToolbarDetailWidget");
        private static readonly By ResultTableHeadersTitleLocator = By.XPath("//table[@class='co_detailsTable a11yTableSortable']/thead//th");
        private static readonly By SummarizeCitingReferenceButtonLocator = By.XPath("//saf-button[text()='Summarize citing references']");
        
        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeCitingReferencesPage"/> class 
        /// </summary>
        public EdgeCitingReferencesPage()
        {
            this.Toolbar.DetailDropdown = new DetailDropdown(DetailDropdownLocator);
        }

        ///<summary>
        ///Summarize citing references button 
        ///</summary>
        public IButton SummarizeCitingReferenceButton => new Button(SummarizeCitingReferenceButtonLocator);
        /// <summary>
        /// ResultTableHeadersTitle
        /// </summary>
        public IReadOnlyCollection<ILabel> ResultTableHeadersTitle => new ElementsCollection<Label>(ResultTableHeadersTitleLocator);

        /// <summary>
        /// Ri Narrow Pane
        /// </summary>
        public new EdgeRiNarrowPaneComponent RiNarrowPane { get; } = new EdgeRiNarrowPaneComponent();

        /// <summary>
        /// Determines whether Citing References page is opened.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCitingReferencesPage()
            => DriverExtensions.WaitForElement(CitingReferencesTitleLocator).Text.StartsWith("Citing References");

        /// <summary>
        /// Is Citing References Grid Displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsCitingReferencesGridElementDisplayed() => DriverExtensions.IsDisplayed(ResultContainerLocator);

        /// <summary>
        /// Is Facet Pane Displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public new bool IsFacetPaneDisplayed() => DriverExtensions.IsDisplayed(FacetPaneElementLocator);

        /// <summary>
        /// Gets the delivery dropdown.
        /// </summary>
        public virtual DeliveryDropdown DeliveryDropdown => new DeliveryDropdown();

        /// <summary> 
        /// Citing references summary component  
        /// </summary>
        public CitingReferenceSummaryComponent CitingReferenceSummary { get; protected set; } = new CitingReferenceSummaryComponent();
    }
}
