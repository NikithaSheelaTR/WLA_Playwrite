namespace Framework.Common.UI.Products.WestLawNextCanada.Components.NewTypeAhead
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.WestlawEdge.Components.NewTypeAhead;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.NewTypeAhead;
    using Framework.Common.UI.Raw.WestlawEdge.Items.TrDiscover;
    using Framework.Common.UI.Raw.WestlawEdge.Models.TrDiscover;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The legal topics component
    /// </summary>
    public class LegalTopicsComponent : ContentTypeDetailsBaseComponent
    {
        private const string MoreLinkLctMask = "//li[@id='LegalTopics_TypeAheadContentTypeListItem']//*[contains(@id,'moreSuggestions_button')]";
        private static readonly By ContainerLocator = By.XPath("//li[@id='LegalTopics_TypeAheadContentTypeListItem']");
        private static readonly By LegalTopicsItemContainerLocator = By.XPath("//a[contains(@class,'co-Typeahead-legalTopicsAnchorItem')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="LegalTopicsComponent"/> class.
        /// </summary>
        public LegalTopicsComponent()
        {
            this.InitializeSupportedCategories();
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// The click more link.
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        public override void ClickMoreLink(NewTypeAheadCategories category) =>
            DriverExtensions.GetElement(By.XPath(MoreLinkLctMask))
                            .Click();

        /// <summary>
        /// The is more link displayed.
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsMoreLinkDisplayed(NewTypeAheadCategories category) =>
            DriverExtensions.IsDisplayed(By.XPath(MoreLinkLctMask));

        /// <summary>
        /// Get suggestion elements list by name.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public IList<string> GetSuggestionElementListByName(NewTypeAheadCategories category)
        {
            var suggestionsList = new List<string>();
            foreach (var item in this.GetSuggestionElementsListByName(category))
            {
                if (item.Displayed)
                {
                    suggestionsList.Add(item.Text);
                }
            }

            return suggestionsList;
        }

        /// <summary>
        /// The initialize supported categories.
        /// </summary>
        protected override sealed void InitializeSupportedCategories()
            =>
            this.SupportedCategories =
                new List<NewTypeAheadCategories>
                    {
                        NewTypeAheadCategories.SearchSuggestions,
                        NewTypeAheadCategories.ContentPages,
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
                case NewTypeAheadCategories.SearchSuggestions:
                case NewTypeAheadCategories.ContentPages:
                case NewTypeAheadCategories.Cases:
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
                category.Equals(NewTypeAheadCategories.LegalTopics)
                    ? this.GetItems(this.ComponentLocator, LegalTopicsItemContainerLocator)
                    : base.GetSuggestionElementsListByName(category);
    }
}
