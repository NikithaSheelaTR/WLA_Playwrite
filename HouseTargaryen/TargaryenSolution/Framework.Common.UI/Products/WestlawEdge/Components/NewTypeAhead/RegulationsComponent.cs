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
    /// The regulations component.
    /// </summary>
    public class RegulationsComponent : ContentTypeDetailsBaseComponent
    {
        private static readonly By ContainerLocator = By.Id("typeAheadContentTypesDiv");

        private static readonly By MorLinkForRegulationsLocator = By.Id("co_typeaheadFederalRegulations_moreSuggestions_button");

        private static readonly By RegulationsItemContainerLocator = By.XPath(".//div[contains(@class,'co-TRDiscover-result')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="RegulationsComponent"/> class.
        /// </summary>
        public RegulationsComponent()
        {
            this.InitializeSupportedCategories();
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// The map items to models by category.
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        /// <returns> List of Suggestion model </returns>
        protected override List<TrdSuggestionModel> MapItemsToModelsByCategory(NewTypeAheadCategories category)
        {
            List<TrdSuggestionModel> resultList;

            switch (category)
            {
                case NewTypeAheadCategories.OtherJurisdictions:
                    resultList = this.GetMappedModelsList<TrdSuggestionModel, TrdOtherItem>(category);
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
                category.Equals(NewTypeAheadCategories.Regulations)
                    ? this.GetItems(this.ComponentLocator, RegulationsItemContainerLocator)
                    : base.GetSuggestionElementsListByName(category);


        /// <summary>
        /// The more link is displayed under regulations.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsMoreLinkDisplayed() =>
            DriverExtensions.IsDisplayed(MorLinkForRegulationsLocator);

        /// <summary>
        /// The initialize supported categories.
        /// </summary>
        protected sealed override void InitializeSupportedCategories()
            => this.SupportedCategories = new List<NewTypeAheadCategories>
                                              {
                                                  NewTypeAheadCategories.ContentPages,
                                                  NewTypeAheadCategories.OtherJurisdictions,
                                                  NewTypeAheadCategories.Tools
                                              };
    }
}