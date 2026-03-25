namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Enums.Facets;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Award Range Facet 
    /// </summary>
    public class AwardRangeFacetComponent : BaseModuleRegressionComponent
    {
        private static readonly By AwardRangeFacetLocator = By.Id("SearchFacetAllXBoxes-awardRangeHeader");

        private static readonly By ContainerLocator = By.Id("facet_div_awardRange");

        private EnumPropertyMapper<AwardRange, AwardRangeInfo> awardsMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the AwardRange enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<AwardRange, AwardRangeInfo> AwardsMapOptionMap =>
            this.awardsMap = this.awardsMap ?? EnumPropertyModelCache.GetMap<AwardRange, AwardRangeInfo>(
                                 "Edge",
                                 @"Resources/EnumPropertyMaps/WestlawEdge/Facets");

        /// <summary>
        /// Get the count of all of the award ranges
        /// </summary>
        /// <returns> A dictionary pairing AwardRange keys and the count values </returns>
        public Dictionary<AwardRange, int> GetAwardRangeCounts()
        {
            var rangeCounts = new Dictionary<AwardRange, int>();
            Array awardRangeList = Enum.GetValues(typeof(AwardRange));
            this.ClickDateDropdown();

            foreach (AwardRange range in awardRangeList)
            {
                rangeCounts.Add(range, this.GetCount(By.XPath(this.AwardsMapOptionMap[range].OptionCountLocatorString)));
            }

            this.ClickDateDropdown();
            return rangeCounts;
        }

        /// <summary>
        /// Expand // Collapse Date Facet Dropdown
        /// </summary>
        private void ClickDateDropdown() => DriverExtensions.GetElement(AwardRangeFacetLocator).CustomClick();

        /// <summary>
        /// Get Count
        /// </summary>
        /// <param name="elementLocator">locator</param>
        /// <returns>int</returns>
        private int GetCount(By elementLocator) => DriverExtensions.WaitForElement(elementLocator).Text.ConvertCountToInt();
    }
}
