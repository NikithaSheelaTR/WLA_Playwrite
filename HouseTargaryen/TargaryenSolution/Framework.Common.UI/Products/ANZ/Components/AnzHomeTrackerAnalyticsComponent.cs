namespace Framework.Common.UI.Products.ANZ.Components
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.ANZ.Enums;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Enums.HomePage;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// ANZ Tracker Analytics component
    /// </summary>
    public class AnzHomeTrackerAnalyticsComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='co_mainContainer']//*[@class='Access-point Access-trackers'] | //*[@id='co_mainContainer']//*[@class='Access-point Access-trackers-au']");

        private static readonly By TrackersLinkLocator = By.XPath(".//*[contains(@class,'Access-point-content')]//a");

        /// <summary>
        /// Trackers Link Elements
        /// </summary>
        public IReadOnlyCollection<ILabel> TrackerLink => new ElementsCollection<Label>(ComponentLocator, TrackersLinkLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the Trackers Link to TrackerAnalyticsInfo map.
        /// </summary>
        protected EnumPropertyMapper<AnzHomeTrackerAnalytics, WebElementInfo> TrackerAnalyticsMap =>
                EnumPropertyModelCache.GetMap<AnzHomeTrackerAnalytics, WebElementInfo>(
                               string.Empty,
                               @"Resources/EnumPropertyMaps");        

        ///<summary>
        /// Click on Trackers link
        /// </summary>
        /// <param name="tracker">
        /// Trackers Link
        /// </param>
        /// <returns>The page instance.</returns>
        public T ClickOnTrackerAnalyticsLink<T>(AnzHomeTrackerAnalytics tracker) where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(ContainerLocator, By.XPath(this.TrackerAnalyticsMap[tracker].LocatorString)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

    }
}
