namespace Framework.Common.UI.Products.TaxnetPro.Components.NarrowPane
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Areas of Interest facet component
    /// </summary>
    public class AreasOfInterestFacetComponent : EdgeBaseFacetComponent
    {
        private const string AreasOfInterestLctMask = "//div[@id='facet_div_areasOfInterest']//label[text()='{0}']/preceding-sibling::input  | //div[@id='facet_div_areasOfInterest']//label[text()='{0}']//../*[@class='co_treeItemSelection']";

        private static readonly By ContainerLocator = By.XPath("//div[@id='facet_div_language']");

        private static readonly By AreasOfInterestOptionsLocator =
            By.XPath("//div[@id='facet_div_areasOfInterest']//input | //div[@id='facet_div_areasOfInterest']//ul/li/input  | //div[@id='facet_div_areasOfInterest']//*[@class='co_treeItemSelection']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Clicks random Areas of Interest option
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>new page instance</returns>
        public T ClickRandomAreasOfInterestOption<T>()
            where T : ICreatablePageObject
        {
            this.GetAreasOfInterestOptions().ElementAt(new Random().Next(0, this.GetAreasOfInterestOptions().Count - 1))
                .Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Apply Areas of Interest facet
        /// </summary>
        /// <typeparam name="T">Page type</typeparam>
        /// <param name="areaOfInterest">area Of Interest</param>
        /// <param name="state">state</param>
        /// <returns> a new search result page object </returns>
        public T ApplyFacet<T>(string areaOfInterest, bool state)
            where T : ICreatablePageObject
        {
            DriverExtensions.SetCheckbox(By.XPath(string.Format(AreasOfInterestLctMask, areaOfInterest)), state);
            DriverExtensions.WaitForJavaScript();

            return DriverExtensions.CreatePageInstance<T>();
        }

        private IReadOnlyCollection<IWebElement> GetAreasOfInterestOptions() =>
            DriverExtensions.GetElements(AreasOfInterestOptionsLocator);
    }
}