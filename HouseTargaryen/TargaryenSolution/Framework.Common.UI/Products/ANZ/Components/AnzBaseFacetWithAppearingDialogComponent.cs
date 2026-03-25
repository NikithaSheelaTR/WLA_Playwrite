namespace Framework.Common.UI.Products.ANZ.Components
{
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// AnzBaseFacetWithAppearing Dialog Component
    /// </summary>
    public class AnzBaseFacetWithAppearingDialogComponent : EdgeBaseFacetWithAppearingDialogComponent
    {
        private static readonly By AppliedFacetItemLocator = By.XPath(".//li");

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicationVolumeFacetComponent"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public AnzBaseFacetWithAppearingDialogComponent(By componentLocator)
            : base(componentLocator)
        {
        }

        /// <summary>
        /// Get all applied facet items text
        /// </summary>
        /// <returns>List of applied options text</returns>
        public IEnumerable<string> GetAllAppliedItemsText() => DriverExtensions.GetElements(this.ComponentLocator, AppliedFacetItemLocator)
                .ToList().Select(el => el.Text);
    }
}
