namespace Framework.Common.UI.Products.WestlawEdge.Components.NewTypeAhead
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components.Thesaurus;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.NewTypeAhead;
    using Framework.Common.UI.Raw.WestlawEdge.Items.TrDiscover;
    using Framework.Common.UI.Raw.WestlawEdge.Models.TrDiscover;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Suggestions component.
    /// </summary>
    public class SuggestionsComponent : ContentTypeDetailsBaseComponent
    {
        private const string MoreLinkLctMask = "//button[@id = 'crsw_typeahead{0}_moreSuggestions_button']";

        private static readonly By ContainerLocator = By.XPath("//div[@id = 'contentTypeDetailsContainer' or @id = 'typeAheadContentTypesDiv']");

        private static readonly By SnapshotsItemContainerLocator = By.XPath(".//span[@id='Snapshots_ContentType']//div[@class='co-Typeahead-result']");

        private static readonly By WestlawAnswersItemContainerLocator = By.XPath(".//span[@id='WestlawAnswers_ContentType']//ul[@id='co_questions']//a | .//a[contains(@class,'SearchSuggestionWestlawAnswer')]");

        private static readonly By ThesaurusContainerLocator = By.XPath(".//div[@class='co-Typeahead-section co-Typeahead-categoryThesaurus']");

        private static readonly By HighlightingLocator = By.XPath(".//span[contains(@class,'co_searchTerm')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="SuggestionsComponent"/> class.
        /// </summary>
        public SuggestionsComponent()
        {
            this.InitializeSupportedCategories();
        }

        /// <summary>
        /// Thesaurus component in suggestions
        /// </summary>
        public ThesaurusComponent ThesaurusComponent { get; } = new ThesaurusComponent();

        /// <summary>
        /// Gets the highlighted terms list.
        /// </summary>
        public IReadOnlyCollection<ILabel> HighlightedTermsList =>
            new ElementsCollection<Label>(this.ComponentLocator, HighlightingLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Is container with Theaurus displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, ThesaurusContainerLocator);

        /// <summary>
        /// Is More link displayed
        /// </summary>
        /// <param name="category">
        /// The category
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsMoreLinkDisplayed(NewTypeAheadCategories category) => DriverExtensions.IsDisplayed(By.XPath(string.Format(MoreLinkLctMask, category)));

        /// <summary>
        /// Get suggestion elements list by name.
        /// </summary>
        /// <param name="category">New Type ahead category</param>
        /// <returns> Count of suggestions</returns>
        public int GetCountOfSuggestionListByName(NewTypeAheadCategories category) => 
            this.GetSuggestionElementsListByName(category).Count(item => item.Displayed);

        /// <summary>
        /// Click on More Link in category
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        public override void ClickMoreLink(NewTypeAheadCategories category)
            => DriverExtensions.GetElement(By.XPath(string.Format(MoreLinkLctMask, category))).Click();

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
        {
            IList<IWebElement> resultList;

            switch (category)
            {
                case NewTypeAheadCategories.Snapshots:
                    resultList = this.GetItems(this.ComponentLocator, SnapshotsItemContainerLocator);
                    break;
                case NewTypeAheadCategories.WestlawAnswers:
                    resultList = this.GetItems(this.ComponentLocator, WestlawAnswersItemContainerLocator);
                    break;
                default:
                    resultList = base.GetSuggestionElementsListByName(category);
                    break;
            }

            return resultList;
        }

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
                case NewTypeAheadCategories.WestlawAnswers:
                    resultList = this.GetMappedModelsList<TrdSuggestionModel, TrdWestlawAnswersItem>(category);
                    break;
                case NewTypeAheadCategories.Snapshots:
                    resultList = this.GetMappedModelsList<TrdSuggestionModel, TrdSnapshotsItem>(category);
                    break;
                case NewTypeAheadCategories.SearchSuggestions:
                    resultList = this.GetMappedModelsList<TrdSuggestionModel, TrdSearchSuggestionItem>(category);
                    break;
                default:
                    resultList = this.GetMappedModelsList<TrdSuggestionModel, TrdBaseCategoryItem>(category);
                    break;
            }

            return resultList;
        }

        /// <summary>
        /// The initialize supported categories.
        /// </summary>
        protected override void InitializeSupportedCategories()
            =>
                this.SupportedCategories =
                new List<NewTypeAheadCategories>
                    {
                        NewTypeAheadCategories.WestlawAnswers,
                        NewTypeAheadCategories.ContentPages,
                        NewTypeAheadCategories.SearchSuggestions,
                        NewTypeAheadCategories.Snapshots,
                        NewTypeAheadCategories.Tools,
                        NewTypeAheadCategories.LitigationAnalytics
                    };
    }
}