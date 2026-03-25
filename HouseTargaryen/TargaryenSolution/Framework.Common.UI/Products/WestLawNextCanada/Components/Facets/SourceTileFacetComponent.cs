namespace Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacetusing
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Source Tile Facet Component
    /// </summary>
    public class SourceTileFacetComponent : BaseFacetCheckboxComponent
    {
        private static readonly By ContainerLocator = By.CssSelector("#facet_div_sourceTitle, #facet_div_MetaDataBrandFacet");
        private const string ChildLabelCheckboxLctMask = ".//li[.//label[text()='{0}']]//input | .//li[.//label[text()='{0}']]//span[@class='co_treeItemSelection']";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Source tile facet checkbox element by label name
        /// </summary>
        /// <param name="labelName">Label Name</param>
        /// <returns>Checkbox element</returns>
        public ICheckBox SourceTileCheckbox(string labelName) => 
            new CheckBox(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(ChildLabelCheckboxLctMask, labelName))));
    }
}
