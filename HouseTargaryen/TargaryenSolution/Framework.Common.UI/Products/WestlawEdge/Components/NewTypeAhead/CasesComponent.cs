namespace Framework.Common.UI.Products.WestlawEdge.Components.NewTypeAhead
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.NewTypeAhead;
    using Framework.Common.UI.Raw.WestlawEdge.Items.TrDiscover;
    using Framework.Common.UI.Raw.WestlawEdge.Models.TrDiscover;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The cases component.
    /// </summary>
    public class CasesComponent : ContentTypeDetailsBaseComponent
    {
        private const string MoreLinkLctMask = "//*[contains(@id,'{0}') and contains(@id,'moreSuggestions_button')]";

        private const string FederalAndStateLckMask = ".//div[contains(@ng-repeat,{0})]";

        private static readonly By ContainerLocator = By.XPath("//div[@class ='co-TRDiscover-casesView' or @id = 'typeAheadContentTypesDiv']");
        private static readonly By CasesItemContainerLocator = By.XPath(".//div[contains(@class,'co-TRDiscover-result')] | .//*[@class='co-Typeahead-result']");
        private static readonly By SuggestionItemsLocator = By.XPath("//ul[@id='co_categoryItems']/li");

        /// <summary>
        /// Initializes a new instance of the <see cref="CasesComponent"/> class.
        /// </summary>
        public CasesComponent()
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
        /// Get Suggestions from Type A head which are only shown
        /// </summary>
        /// <returns>List of titles shown on dialog</returns>
        public List<string> GetSuggestionTitlesShown() =>
            this.GetSuggestionItemsShown().Select(item => item.Text).ToList();

        /// <summary>
        /// Click on Suggestion with given index
        /// </summary>
        /// <typeparam name="T">Page instance to create</typeparam>
        /// <param name="index">Index to click</param>
        /// <returns>Page instance</returns>
        public T ClickOnSuggestionByIndex<T>(int index) where T : ICreatablePageObject
        {
            this.GetSuggestionItemsShown().ElementAt(index).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get Suggestion items which are shown
        /// </summary>
        /// <returns>List of webelements</returns>
        protected IEnumerable<IWebElement> GetSuggestionItemsShown() =>
            DriverExtensions.GetElements(SuggestionItemsLocator)
                .Where(item => !item.GetAttribute("class").Contains("co_hideState"));

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
                        NewTypeAheadCategories.Cases,
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
                case NewTypeAheadCategories.State:
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
        {
            switch (category)
            {                
                case NewTypeAheadCategories.State:
                    return this.GetItems(FederalAndStateLckMask, this.CategoriesMap[category].Text.ToLower());
                case NewTypeAheadCategories.Cases:
                    return this.GetItems(this.ComponentLocator, CasesItemContainerLocator);
                default:
                    return base.GetSuggestionElementsListByName(category);
            }
        }
    }
}