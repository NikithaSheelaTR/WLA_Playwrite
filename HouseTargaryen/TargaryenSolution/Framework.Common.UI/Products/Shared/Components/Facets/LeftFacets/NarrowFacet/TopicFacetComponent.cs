namespace Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.WestLawNext.Enums.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.DataModel;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Topic Facet Component
    /// </summary>
    public class TopicFacetComponent : MoreLinkFacetComponent
    {
        // LctMask should have " because facet can have ' in its name
        private const string FacetExpandButtonLctMask = "//label[text()=\"{0}\"]/../a[@class='co_facet_expand']";

        private static readonly By ContainerLocator = By.CssSelector("#facet_div_MetaDataTopicFacet, #facet_div_topic");

        private EnumPropertyMapper<TopicFacet, BaseTextModel> topicFacetMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// PublicationTypeMap
        /// </summary>
        private EnumPropertyMapper<TopicFacet, BaseTextModel> TopicFacetMap
            => this.topicFacetMap = this.topicFacetMap ?? EnumPropertyModelCache.GetMap<TopicFacet, BaseTextModel>();

        /// <summary>
        /// Gets the list of Topic facets
        /// </summary>
        /// <returns>The list of facet names</returns>
        public List<string> GetTopicSubFacetsList() => DriverExtensions.GetElements(this.ComponentLocator, By.TagName("label")).Select(s => s.Text).ToList();

        /// <summary>
        /// Apply the specified Topic facet
        /// </summary>
        /// <param name="topicFacetOption"> Topic type </param>
        public void ExpandTopicFacet(TopicFacet topicFacetOption)
            => DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(FacetExpandButtonLctMask, this.TopicFacetMap[topicFacetOption].Text)))
                               .CustomClick();

        /// <summary>
        /// Checks that the specified facet has expand button
        /// </summary>
        /// <param name="topicFacetOption">The topic Facet Option.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsExpandButtonDisplayed(TopicFacet topicFacetOption)
            => DriverExtensions.IsDisplayed(By.XPath(string.Format(FacetExpandButtonLctMask, this.TopicFacetMap[topicFacetOption].Text)), 5);

        /// <summary>
        /// Apply the specified checkbox
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="topicFacetOption">The topicFacetOption.</param>
        /// <param name="setTo">setTo</param>
        /// <returns>The new instance of T page.</returns>
        public T SetCheckbox<T>(TopicFacet topicFacetOption, bool setTo = true) where T : ICreatablePageObject
            => this.SetCheckbox<T>(this.TopicFacetMap[topicFacetOption].Text, setTo);
    }
}