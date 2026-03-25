namespace Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    /// <summary>
    ///Document type option Facet Component
    /// </summary>
    public class DocumentTypeFacetOptionComponent : BaseFacetCheckboxComponent
    {
        private static readonly By ContainerLocator = By.Id("co_leftColumn");

        private static readonly By ListOfChildLabelsLocator = By.XPath(".//*[@class='co_facet_tree']//descendant::input");

        private static readonly By DocumentTitleLabelLocator = By.XPath(".//span[@id='titleInfo']/a");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// FacetOption Checkbox
        /// </summary>
        public IReadOnlyCollection<ICheckBox> FacetOptionCheckbox => new ElementsCollection<CheckBox>(this.ComponentLocator, ListOfChildLabelsLocator);

        /// <summary>
        ///  DocumentTitle Label
        /// </summary>
        public ILabel DocumentTitleLabel => new Label(DocumentTitleLabelLocator);   
    }
}
