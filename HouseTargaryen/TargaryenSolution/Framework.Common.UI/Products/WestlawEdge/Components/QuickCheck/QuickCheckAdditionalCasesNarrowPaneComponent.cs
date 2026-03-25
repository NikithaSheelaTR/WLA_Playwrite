namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The doc analyzer additional cases narrow pane component.
    /// </summary>
    public class QuickCheckAdditionalCasesNarrowPaneComponent
    {
        private static readonly By SelectMultipleFiltersToggleLocator = By.XPath("//*[@id='SlideToggle_']");
        private static readonly By ApplyFiltersButtonLocator = By.XPath("//button[@class='co_multifacet_apply']");
        private static readonly By ClearButtonLocator = By.CssSelector("#co_undoAllSelections>a .co_buttonUndo, .co_btnBack, .co_btnGray, .SearchFacet-buttonUndo");
        private static readonly By FacetTitleLocator = By.XPath("//*[@class='SearchFacet-buttonToggle']//span[@class = 'SearchFacet-buttonText']");
        private static readonly By SearchWithinFacetLoctor = By.XPath("//section[contains(@class, 'keyword')]/parent::div");
        private static readonly By ReportedFacetLocator = By.XPath("//section[contains(@class, 'reported')]/parent::div");
        
        /// <summary>
        /// The search within facet.
        /// </summary>
        public EdgeSearchWithinFacetComponent SearchWithinFacet => new EdgeSearchWithinFacetComponent();

        /// <summary>
        /// The reported status facet.
        /// </summary>
        public ReportedStatusFacetComponent ReportedStatusFacet => new ReportedStatusFacetComponent(ReportedFacetLocator);

        /// <summary>
        /// The select multiple checkbox.
        /// </summary>
        public ICheckBox SelectMultipleCheckbox => new CheckBox(SelectMultipleFiltersToggleLocator);

        /// <summary>
        /// Gets the clear button.
        /// </summary>
        public IButton ClearButton => new Button(ClearButtonLocator);

        /// <summary>
        /// Apply filters button.
        /// </summary>
        public IButton ApplyButton => new Button(ApplyFiltersButtonLocator);

        /// <summary>
        /// The descriptions.
        /// </summary>
        public IReadOnlyCollection<ILabel> FacetLabels => new ElementsCollection<Label>(FacetTitleLocator);

        /// <summary>
        /// The is search within facet displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSearchWithinFacetDisplayed() => DriverExtensions.IsDisplayed(SearchWithinFacetLoctor);     
    }
}