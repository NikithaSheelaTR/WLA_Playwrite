namespace Framework.Common.UI.Products.WestLawNextCanada.Components
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using OpenQA.Selenium;

    /// <summary>
    /// TrilliumLeftNarrowPaneComponent
    /// </summary>
    public class TrilliumLeftNarrowPane : BaseModuleRegressionComponent
    {
        private static readonly By TrilliumNarrowPaneLocator = By.XPath(".//div[@class='co_genericBoxContent']");
        private static readonly By ShowInPlanCheckBoxLocator = By.XPath("//input[contains(@id,'co_facetItem_MetaDataInplanFacet')]//following::label");
        private static readonly By MoreProductsLinkLocator = By.XPath(".//li[@id='MetaDataBrandFacethierarcyclientanchor']");
        private static readonly By CriminalLawCheckBoxLocator = By.XPath(".//input[contains(@id,'facet_hierarchy_MetaDataBrandFacet')]//following::input[contains(@aria-label,'Criminal Procedure')]");
        private static readonly By FacetTitlesLocator = By.XPath(".//div[@id='facet_div_MetaDataPublicationTypeFacet']//descendant::label");
        private static readonly By OverviewLabelLocator = By.XPath(".//*[@id='co_search_contentNav_count_ALL_li']");
        private static readonly By TextsAndAnnotationsLabelLocator = By.XPath(".//*[@id='co_search_contentNav_count_CAN_TEXTSANDANNOTATIONS_li']");
        private static readonly By ElooseleafsLabelLocator = By.XPath(".//*[@id='co_search_contentNav_count_CAN_EREFERENCE_li']");
        private static readonly By FilterLeftRailLabelLocator = By.XPath(".//div[@id='co_website_searchFacets']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TrilliumNarrowPaneLocator;

        /// <summary>
        /// ShowInPlan Checkbox
        /// </summary>
        public ICheckBox ShowInPlanCheckBox => new CheckBox(this.ComponentLocator, ShowInPlanCheckBoxLocator);

        /// <summary>
        /// More Products link
        /// </summary>
        public ILink MoreProductsLink => new Link(this.ComponentLocator, MoreProductsLinkLocator);

        /// <summary>
        /// Criminal Law Checkbox
        /// </summary>
        public ICheckBox CriminalLawCheckBox => new CheckBox(this.ComponentLocator, CriminalLawCheckBoxLocator);

        /// <summary>
        /// Facet Titles Checkbox
        /// </summary>
        public ICheckBox FacetTitlesCheckBox => new CheckBox(this.ComponentLocator, FacetTitlesLocator);

        /// <summary>
        /// Overview Label
        /// </summary>
        public ILabel OverviewLabel => new Label(this.ComponentLocator, OverviewLabelLocator);

        /// <summary>
        /// Texts And Annotations Label
        /// </summary>
        public ILabel TextsAndAnnotationsLabel => new Label(this.ComponentLocator, TextsAndAnnotationsLabelLocator);

        /// <summary>
        /// Elooseleafs Label
        /// </summary>
        public ILabel ElooseleafsLabel => new Label(this.ComponentLocator, ElooseleafsLabelLocator);

        /// <summary>
        /// View  Label
        /// </summary>
        public ILabel FilterLeftRailLabel => new Label(this.ComponentLocator, FilterLeftRailLabelLocator);            
    }
}
