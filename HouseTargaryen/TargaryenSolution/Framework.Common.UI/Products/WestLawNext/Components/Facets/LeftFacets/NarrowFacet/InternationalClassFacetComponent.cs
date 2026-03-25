
namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// International Class Facet Component
    /// </summary>
    public class InternationalClassFacetComponent : BaseLinkFacetComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[contains(@id,'facet_div_') and contains(@id,'ClassInternational')]");

        private static readonly By CheckBoxItemsTextLocator = By.XPath(".//span[@class='co_docHeaderTitleLine']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Facets Check box text
        /// </summary>
        public List <string> CheckBoxItemsText() => DriverExtensions.GetElements(ContainerLocator,CheckBoxItemsTextLocator).Select(item => new Label(item).Text).ToList();
    }
}
