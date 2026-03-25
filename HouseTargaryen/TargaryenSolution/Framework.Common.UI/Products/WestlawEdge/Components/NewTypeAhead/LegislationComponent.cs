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
    /// Legislation component
    /// </summary>
    public class LegislationComponent : ContentTypeDetailsBaseComponent
    {
        private const string MoreLinkLctMask = "//*[contains(@id,'{0}') and contains(@id,'moreSuggestions_button')]";

        private static readonly By ContainerLocator = By.XPath("//div[@legislation-view]");
        private static readonly By LegislationItemContainerLocator = By.XPath(".//div[contains(@class,'co-TRDiscover-result')]");
        
        /// <summary>
        /// Initializes a new instance of the <see cref="LegislationComponent"/> class.
        /// </summary>
        public LegislationComponent()
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
            DriverExtensions.GetElement(By.XPath(string.Format(MoreLinkLctMask, this.CategoriesMap[category].Text)))
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
            DriverExtensions.IsDisplayed(By.XPath(string.Format(MoreLinkLctMask, this.CategoriesMap[category].Text)));

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
                category.Equals(NewTypeAheadCategories.Legislation)
                    ? this.GetItems(this.ComponentLocator, LegislationItemContainerLocator)
                    : base.GetSuggestionElementsListByName(category);

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
                case NewTypeAheadCategories.Legislation:
                    resultList = this.GetMappedModelsList<TrdSuggestionModel, TrdLegislationItem>(category);
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
        /// The initialize supported categories.
        /// </summary>
        protected override sealed void InitializeSupportedCategories()
            =>
                this.SupportedCategories =
                    new List<NewTypeAheadCategories>
                        {
                            NewTypeAheadCategories.Legislation,
                            NewTypeAheadCategories.ContentPages
                        };
    }
}
