namespace Framework.Common.UI.Products.WestlawEdge.Components.NewTypeAhead
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Raw.WestlawEdge.Enums.NewTypeAhead;
    using Framework.Common.UI.Raw.WestlawEdge.Items.TrDiscover;
    using Framework.Common.UI.Raw.WestlawEdge.Models.TrDiscover;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The secondary sources component.
    /// </summary>
    public class SecondarySourcesComponent : ContentTypeDetailsBaseComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class ='co-TRDiscover-secondarySourcesView' or @id = 'typeAheadContentTypesDiv']");

        private static readonly By SecondarySourcesItemContainerLocator = By.XPath(".//div[contains(@class,'co-TRDiscover-result')] | .//div[@class = 'co-Typeahead-categorySecondarySources']//li/a");

        private static readonly By MoreLinkLocator = By.XPath("//*[@id ='co_typeaheadSecondarySources_moreSuggestions_button' or @id='co_typeaheadAnalyticals_moreSuggestions_button']");

        /// <summary>
        /// Initializes a new instance of the <see cref="SecondarySourcesComponent"/> class.
        /// </summary>
        public SecondarySourcesComponent()
        {
            this.InitializeSupportedCategories();
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click om More Link in category
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsMoreLinkDisplayed(NewTypeAheadCategories category)
            => DriverExtensions.IsDisplayed(MoreLinkLocator);

        /// <summary>
        /// Click on More Link in category
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        public override void ClickMoreLink(NewTypeAheadCategories category)
            => DriverExtensions.GetElement(this.ComponentLocator, MoreLinkLocator).Click();

        /// <summary>
        /// The map items to models by category.
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        /// <returns>
        /// The <see cref="T:List"/>.
        /// </returns>
        protected override List<TrdSuggestionModel> MapItemsToModelsByCategory(NewTypeAheadCategories category)
        {
            List<TrdSuggestionModel> resultList;

            switch (category)
            {
                case NewTypeAheadCategories.SecondarySources:
                    resultList = this.GetMappedModelsList<TrdSuggestionModel, TrdSecondarySourcesItem>(category);
                    break;
                default:
                    resultList =
                        this.GetSuggestionElementsListByName(category)
                            .Select(x => new TrdSuggestionModel { Text = x.Text })
                            .ToList();
                    break;
            }

            return resultList;
        }

        /// <summary>
        /// The get suggestion elements list by name.
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        /// <returns>
        /// The <see cref="T:IList"/>.
        /// </returns>
        protected override IList<IWebElement> GetSuggestionElementsListByName(NewTypeAheadCategories category)
            =>
                category.Equals(NewTypeAheadCategories.SecondarySources)
                    ? this.GetItems(this.ComponentLocator, SecondarySourcesItemContainerLocator)
                    : base.GetSuggestionElementsListByName(category);

        /// <summary>
        /// The initialize supported categories.
        /// </summary>
        protected override sealed void InitializeSupportedCategories()
            =>
            this.SupportedCategories =
                new List<NewTypeAheadCategories>
                    {
                        NewTypeAheadCategories.ContentPages,
                        NewTypeAheadCategories.SecondarySources,
                        NewTypeAheadCategories.Tools
                    };
    }
}