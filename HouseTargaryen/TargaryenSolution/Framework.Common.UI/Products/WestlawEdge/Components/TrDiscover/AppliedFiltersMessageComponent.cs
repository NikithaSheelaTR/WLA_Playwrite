namespace Framework.Common.UI.Products.WestlawEdge.Components.TrDiscover
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Applied Filters Message Component
    /// </summary>
    public class AppliedFiltersMessageComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("coid_website_messagePlaceholder");
        private static readonly By InapplicableFiltersMessageLocator = By.XPath(".//span[@class = 'FacetBoxContent']/span");
        private static readonly By PreviousFiltersAppliedMessageLocator = By.XPath(".//h3");

        /// <summary>
        /// Label with text 'Your previous filters have been applied.'
        /// </summary>
        public ILabel PreviousFiltersAppliedLabel => new Label(this.ComponentLocator, PreviousFiltersAppliedMessageLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Are filters applied
        /// </summary>
        public bool AreAllFiltersApplied() => DriverExtensions.IsDisplayed(this.ComponentLocator, InapplicableFiltersMessageLocator);

        /// <summary>
        /// Get info message
        /// </summary>
        public List<string> GetInapplicablePreviousFilters()
            => DriverExtensions.GetElements(this.ComponentLocator, InapplicableFiltersMessageLocator).Select(el => el.GetText()).ToList();
    }
}