namespace Framework.Common.UI.Raw.WestlawEdge.Components.TrDiscover.TrdSearchResultPage
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// ApplyedFiltersMessageComponent
    /// </summary>
    public class ApplyedFiltersMessageComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("coid_website_messagePlaceholder");
        private static readonly By FacetBoxMessageLocator = By.XPath("//div[@class = 'FacetBoxMessageContainer']/h3");
        private static readonly By InapplicableFiltersMessageLocator = By.XPath("//span[@class = 'FacetBoxContent']/span");

        /// <summary>
        /// The container.
        /// </summary>
        private static IWebElement Container => DriverExtensions.WaitForElementDisplayed(ContainerLocator);

        /// <summary>
        /// Get info message
        /// </summary>
        public List<string> GetInapplicablePreviousFilters()
            => DriverExtensions.GetElements(Container, InapplicableFiltersMessageLocator).Select(el => el.GetText()).ToList();

        /// <summary>
        /// Are filters applied
        /// </summary>
        public bool AreAllFiltersApplied() => DriverExtensions.IsDisplayed(Container, InapplicableFiltersMessageLocator);
    }
}
