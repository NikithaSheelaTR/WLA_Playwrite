namespace Framework.Common.UI.Products.WestlawEdge.Components.NewTypeAhead
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Raw.WestlawEdge.Enums.NewTypeAhead;
    using Framework.Common.UI.Raw.WestlawEdge.Items.TrDiscover;
    using Framework.Common.UI.Raw.WestlawEdge.Models.TrDiscover;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// The statutes component.
    /// </summary>
    public class StatutesAndCourtRulesComponent : ContentTypeDetailsBaseComponent
    {
        private const string FederalAndStateLckMask = ".//div[contains(@ng-repeat,{0})]";

        private const string MoreLinkLctMask = "//h2[text() = '{0}']//following-sibling::*[contains(@id,'moreSuggestions_button') and contains(@class, 'Statutes')]";

        private static readonly By ContainerLocator = By.Id("typeAheadContentTypesDiv");

        /// <summary>
        /// Initializes a new instance of the <see cref="StatutesAndCourtRulesComponent"/> class.
        /// </summary>
        public StatutesAndCourtRulesComponent()
        {
            this.InitializeSupportedCategories();
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click on More Link in category
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        public override void ClickMoreLink(NewTypeAheadCategories category)
            => DriverExtensions.GetElement(By.XPath(string.Format(MoreLinkLctMask, this.CategoriesMap[category].Text))).Click();

        /// <summary>
        /// Is More link displayed
        /// </summary>
        /// <param name="category">
        /// the category
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsMoreLinkDisplayed(NewTypeAheadCategories category)
            => DriverExtensions.IsDisplayed(By.XPath(string.Format(MoreLinkLctMask, this.CategoriesMap[category].Text)));

        /// <summary>
        /// The initialize supported categories.
        /// </summary>
        protected override sealed void InitializeSupportedCategories()
            =>
            this.SupportedCategories =
                new List<NewTypeAheadCategories>
                    {
                        NewTypeAheadCategories.Federal,
                        NewTypeAheadCategories.State,
                        NewTypeAheadCategories.ContentPages,
                        NewTypeAheadCategories.Tools
                    };

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
                case NewTypeAheadCategories.Federal:
                    resultList = this.GetMappedModelsList<TrdSuggestionModel, TrdStateAndFederalItem>(category);
                    break;
                case NewTypeAheadCategories.State:
                    resultList = this.GetMappedModelsList<TrdSuggestionModel, TrdStateAndFederalItem>(category);
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

    }
}