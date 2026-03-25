namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Enums.Facets;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// AwardFacetComponent
    /// </summary>
    public class AwardFacetComponent : BaseFacetCheckboxComponent
    {
        private const string CheckboxLctMask = ".//li[./label[contains(text(),'{0}')]]/input";

        private static readonly By ContainerLocator = By.Id("facet_div_awardRange");

        private EnumPropertyMapper<AwardRange, WebElementInfo> awardsMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the AwardRange enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<AwardRange, WebElementInfo> AwardsMap
            => this.awardsMap = this.awardsMap ?? EnumPropertyModelCache.GetMap<AwardRange, WebElementInfo>();

        /// <summary>
        /// Apply the specified checkbox
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="awardRage"> Award range to apply </param>
        /// <param name="setTo">setTo</param>
        /// <returns>The new instance of T page.</returns>
        public T SetCheckbox<T>(AwardRange awardRage, bool setTo = true) where T : ICreatablePageObject
            => this.SetCheckbox<T>(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(CheckboxLctMask, this.AwardsMap[awardRage].Text))), setTo);

        /// <summary>
        /// Get the count of all of the award ranges
        /// </summary>
        /// <returns> A dictionary pairing AwardRange keys and the count values </returns>
        public Dictionary<AwardRange, int> GetAwardRangeCounts()
        {
            var rangeCounts = new Dictionary<AwardRange, int>();
            Enum.GetValues(typeof(AwardRange)).Cast<AwardRange>().ToList()
                .ForEach(awardRange => rangeCounts.Add(awardRange, this.GetCheckboxCount(string.Format(CheckboxLctMask, this.AwardsMap[awardRange].Text))));

            return rangeCounts;
        }
    }
}