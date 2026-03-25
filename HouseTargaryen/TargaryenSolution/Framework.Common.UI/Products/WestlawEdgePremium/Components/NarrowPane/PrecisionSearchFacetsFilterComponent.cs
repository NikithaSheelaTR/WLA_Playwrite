namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.NarrowPane
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel;

    using OpenQA.Selenium;

    /// <summary>
    /// Precison component with facets for search
    /// </summary>
    public class PrecisionSearchFacetsFilterComponent : NewEdgeSearchFacetsFilterComponent
    {
        private static readonly By PrecisionFacetHeaderLabelLocator = By.XPath("//div[@class='Athens-facets-heading-wrapper']");
        private static readonly By ShowPrecisionFiltersButtonLocator = By.XPath("//*[contains(@class, 'addCircle')]");
        private static readonly By HidePrecisionFiltersButtonLocator = By.XPath("//*[contains(@class, 'removeCircle')]");
        private static readonly By PrecisionFacetLabelLocator = By.XPath("//*[contains(@class, 'SearchFacetHierarchy-athens')]//*[@class='SearchFacet-buttonText']");
        private static readonly By AreaOfLawFacetLocator = By.XPath("//*[@id='facet_div_athensAreaOfLaw']");
        private static readonly By CauseOfActionFacetLocator = By.XPath("//*[@id='facet_div_athensCauseOfAction']");
        private static readonly By CivilCriminalFacetLocator = By.XPath("//*[@id='facet_div_civil_criminal']");
        private static readonly By FactPatternFacetLocator = By.XPath("//*[@id='facet_div_athensFactPattern']");
        private static readonly By GoverningLawFacetLocator = By.XPath("//*[@id='facet_div_athensGoverningLaw']");
        private static readonly By IndustryTypeFacetLocator = By.XPath("//*[@id='facet_div_athensIndustryType']");
        private static readonly By InfoIconButtonLocator = By.XPath("//*[@title='More information about precision filters']");
        private static readonly By LegalIssueFacetLocator = By.XPath("//*[@id='facet_div_athensLegalIssue']");
        private static readonly By MotionTypeFacetLocator = By.XPath("//*[@id='facet_div_athensMotionType']");
        private static readonly By PartyTypeFacetLocator = By.XPath("//*[@id='facet_div_athensPartyType']");

        /// <summary>
        /// Precision facet labels
        /// </summary>
        public IReadOnlyCollection<ILabel> PrecisionFacetLabels => new ElementsCollection<Label>(PrecisionFacetLabelLocator);

        /// <summary>
        /// Header label for Precision facets
        /// </summary>
        public ILabel PrecisionFacetHeaderLabel => new Label(PrecisionFacetHeaderLabelLocator);

        /// <summary>
        /// Show Precision filters button
        /// </summary>
        public IButton ShowPrecisionFiltersButton => new Button(ShowPrecisionFiltersButtonLocator);

        /// <summary>
        /// Hide Precision filters button
        /// </summary>
        public IButton HidePrecisionFiltersButton => new Button(HidePrecisionFiltersButtonLocator);

        /// <summary>
        /// Info icon button
        /// </summary>
        public IButton InfoIconButton => new Button(InfoIconButtonLocator);

        /// <summary>
        /// Area of law Facet
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent AreaOfLawFacet => new EdgeBaseFacetWithAppearingDialogComponent(AreaOfLawFacetLocator);

        /// <summary>
        /// Party Type Facet
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent PartyTypeFacet => new EdgeBaseFacetWithAppearingDialogComponent(PartyTypeFacetLocator);

        /// <summary>
        /// Fact Pattern Facet
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent FactPatternFacet => new EdgeBaseFacetWithAppearingDialogComponent(FactPatternFacetLocator);

        /// <summary>
        /// Cause Of Action Facet
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent CauseOfActionFacet => new EdgeBaseFacetWithAppearingDialogComponent(CauseOfActionFacetLocator);

        /// <summary>
        /// Legal Issue Facet
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent LegalIssueFacet => new EdgeBaseFacetWithAppearingDialogComponent(LegalIssueFacetLocator);

        /// <summary>
        /// Governing Law Facet
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent GoverningLawFacet => new EdgeBaseFacetWithAppearingDialogComponent(GoverningLawFacetLocator);

        /// <summary>
        /// Industry Type Facet
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent IndustryTypeFacet => new EdgeBaseFacetWithAppearingDialogComponent(IndustryTypeFacetLocator);

        /// <summary>
        /// Motion Type Facet
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent MotionTypeFacet => new EdgeBaseFacetWithAppearingDialogComponent(MotionTypeFacetLocator);

        /// <summary>
        /// Precision Search Within Facet
        /// </summary>
        public PrecisionSearchWithinFacetComponent PrecisionSearchWithinFacet => new PrecisionSearchWithinFacetComponent();

        /// <summary>
        /// Civil / Criminal Facet
        /// </summary>
        public BaseSearchHierarchyFacetComponent CivilCriminalFacet => new BaseSearchHierarchyFacetComponent(CivilCriminalFacetLocator);
    }
}
