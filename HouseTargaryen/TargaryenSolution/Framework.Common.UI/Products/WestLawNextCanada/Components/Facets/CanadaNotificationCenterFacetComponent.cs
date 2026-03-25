namespace Framework.Common.UI.Products.WestLawNextCanada.Components.Facets
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Notification Center Facet component
    /// </summary>
    public class CanadaNotificationCenterFacetComponent : EdgeNarrowPaneComponent
    {
        private static readonly By NotificationTabFacetListLocator = By.XPath("//ul[@class= 'NotificationTabFacetGroup-list']//button");

        /// <summary>
        /// Gets the List of Notification tab facets
        /// </summary>
        /// <returns> Returns the List of Notification tab facets </returns>
        public List<string> GetNotificationTabFacets() => DriverExtensions.GetElements(NotificationTabFacetListLocator).Select(facetElement => facetElement.Text).ToList();
    }
}
